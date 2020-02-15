// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.utils;

import org.bukkit.plugin.Plugin;
import java.net.URLConnection;
import net.md_5.bungee.api.chat.BaseComponent;
import net.md_5.bungee.api.chat.ClickEvent;
import net.md_5.bungee.api.chat.TextComponent;
import java.util.regex.Pattern;
import java.io.Reader;
import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.net.URL;
import org.bukkit.scheduler.BukkitRunnable;
import org.bukkit.entity.Player;
import java.util.Iterator;
import org.bukkit.inventory.ShapedRecipe;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.inventory.ItemFlag;
import org.bukkit.enchantments.Enchantment;
import org.bukkit.Material;
import org.bukkit.inventory.ItemStack;
import net.md_5.bungee.api.ChatColor;
import java.util.HashMap;
import java.util.ArrayList;
import codes.biscuit.genbucket.timers.GenningTimer;
import org.bukkit.Location;
import java.util.Map;
import org.bukkit.inventory.Recipe;
import java.util.List;
import codes.biscuit.genbucket.GenBucket;

public class Utils
{
    private GenBucket main;
    private List<Recipe> recipeList;
    private Map<Location, GenningTimer> currentGens;
    
    public Utils(final GenBucket main) {
        this.recipeList = new ArrayList<Recipe>();
        this.currentGens = new HashMap<Location, GenningTimer>();
        this.main = main;
    }
    
    static String color(final String text) {
        return ChatColor.translateAlternateColorCodes('&', text);
    }
    
    static List<String> colorLore(final List<String> lore) {
        for (int i = 0; i < lore.size(); ++i) {
            lore.set(i, color(lore.get(i)));
        }
        return lore;
    }
    
    ItemStack itemFromString(final String rawItem) {
        String[] rawSplit;
        if (rawItem.contains(":")) {
            rawSplit = rawItem.split(":");
        }
        else {
            rawSplit = new String[] { rawItem };
        }
        Material material;
        try {
            material = Material.valueOf(rawSplit[0]);
        }
        catch (IllegalArgumentException ex) {
            material = Material.DIRT;
        }
        short damage = 0;
        if (rawSplit.length > 1) {
            try {
                damage = Short.valueOf(rawSplit[1]);
            }
            catch (IllegalArgumentException ex2) {}
        }
        return new ItemStack(material, 1, damage);
    }
    
    public ItemStack addGlow(final ItemStack item) {
        item.addUnsafeEnchantment(Enchantment.LUCK, 1);
        final ItemMeta meta = item.getItemMeta();
        meta.addItemFlags(new ItemFlag[] { ItemFlag.HIDE_ENCHANTS });
        item.setItemMeta(meta);
        return item;
    }
    
    public void registerRecipes() {
        if (this.main.getConfigValues().getRecipeBuckets() != null) {
            for (final String bucketID : this.main.getConfigValues().getRecipeBuckets()) {
                final Bucket bucket = this.main.getBucketManager().getBucket(bucketID);
                final ItemStack item = bucket.getItem();
                item.setAmount(bucket.getRecipeAmount());
                final ShapedRecipe newRecipe = new ShapedRecipe(item);
                if (bucket.getRecipeShape() != null) {
                    newRecipe.shape(new String[] { bucket.getRecipeShape().get(0), bucket.getRecipeShape().get(1), bucket.getRecipeShape().get(2) });
                    if (bucket.getIngredients() == null) {
                        continue;
                    }
                    for (final Map.Entry<Character, ItemStack> ingredient : bucket.getIngredients().entrySet()) {
                        final char symbol = ingredient.getKey();
                        final ItemStack ingredientItem = ingredient.getValue();
                        if (ingredientItem.getData().getData() != 1) {
                            newRecipe.setIngredient(symbol, ingredientItem.getData());
                        }
                        else {
                            newRecipe.setIngredient(symbol, ingredientItem.getType());
                        }
                    }
                    this.main.getServer().addRecipe((Recipe)newRecipe);
                    this.recipeList.add((Recipe)newRecipe);
                }
            }
        }
    }
    
    public void reloadRecipes() {
        final List<Recipe> oldRecipes = new ArrayList<Recipe>();
        final Iterator<Recipe> allRecipes = (Iterator<Recipe>)this.main.getServer().recipeIterator();
        while (allRecipes.hasNext()) {
            final Recipe recipe = allRecipes.next();
            if (!this.recipeList.contains(recipe)) {
                oldRecipes.add(recipe);
            }
        }
        this.main.getServer().clearRecipes();
        this.recipeList.clear();
        final Iterator<Recipe> iterator = oldRecipes.iterator();
        while (iterator.hasNext()) {
            final Recipe recipe = iterator.next();
            this.main.getServer().addRecipe(recipe);
        }
        this.registerRecipes();
    }
    
    public void updateConfig() {
        if (this.main.getConfigValues().getConfigVersion() < 1.3) {
            final Map<String, Object> oldValues = new HashMap<String, Object>();
            for (final String oldKey : this.main.getConfig().getKeys(true)) {
                oldValues.put(oldKey, this.main.getConfig().get(oldKey));
            }
            this.main.saveResource("config.yml", true);
            this.main.reloadConfig();
            for (final String newKey : this.main.getConfig().getKeys(true)) {
                if (oldValues.containsKey(newKey)) {
                    this.main.getConfig().set(newKey, oldValues.get(newKey));
                }
            }
            this.main.getConfig().set("config-version", (Object)1.3);
            this.main.saveConfig();
        }
    }
    
    public void checkUpdates(final Player p) {
        new BukkitRunnable() {
            public void run() {
                try {
                    final URL url = new URL("https://api.spigotmc.org/legacy/update.php?resource=63651");
                    final URLConnection connection = url.openConnection();
                    connection.setReadTimeout(5000);
                    connection.addRequestProperty("User-Agent", "GenBucket update checker");
                    connection.setDoOutput(true);
                    final BufferedReader reader = new BufferedReader(new InputStreamReader(connection.getInputStream()));
                    final String newestVersion = reader.readLine();
                    reader.close();
                    final List<Integer> newestVersionNumbers = new ArrayList<Integer>();
                    final List<Integer> thisVersionNumbers = new ArrayList<Integer>();
                    try {
                        for (final String s : newestVersion.split(Pattern.quote("."))) {
                            newestVersionNumbers.add(Integer.parseInt(s));
                        }
                        for (final String s : Utils.this.main.getDescription().getVersion().split(Pattern.quote("."))) {
                            thisVersionNumbers.add(Integer.parseInt(s));
                        }
                    }
                    catch (Exception ex) {
                        return;
                    }
                    for (int i = 0; i < 3; ++i) {
                        if (newestVersionNumbers.get(i) != null && thisVersionNumbers.get(i) != null) {
                            if (newestVersionNumbers.get(i) > thisVersionNumbers.get(i)) {
                                final TextComponent newVersion = new TextComponent("A new version of " + Utils.this.main.getDescription().getName() + ", " + newestVersion + " is available. Download it by clicking here.");
                                newVersion.setColor(ChatColor.RED);
                                newVersion.setClickEvent(new ClickEvent(ClickEvent.Action.OPEN_URL, "https://www.spigotmc.org/resources/genbucket-1-8-1-13-turn-any-block-into-a-wall-or-floor.63651/"));
                                p.spigot().sendMessage((BaseComponent)newVersion);
                                return;
                            }
                            if (thisVersionNumbers.get(i) > newestVersionNumbers.get(i)) {
                                p.sendMessage(ChatColor.RED + "You are running a development version of " + Utils.this.main.getDescription().getName() + ", " + Utils.this.main.getDescription().getVersion() + ". The latest online version is " + newestVersion + ".");
                                return;
                            }
                        }
                    }
                }
                catch (Exception ex2) {}
            }
        }.runTaskAsynchronously((Plugin)this.main);
    }
    
    public Map<Location, GenningTimer> getCurrentGens() {
        return this.currentGens;
    }
}
