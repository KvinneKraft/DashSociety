// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket.utils;

import org.bukkit.entity.Player;
import org.bukkit.inventory.meta.ItemMeta;
import java.util.Iterator;
import java.util.HashSet;
import java.util.EnumSet;
import java.util.HashMap;
import java.util.Map;
import java.util.List;
import org.bukkit.inventory.ItemStack;
import org.bukkit.Material;
import java.util.Set;
import codes.biscuit.genbucket.GenBucket;

public class ConfigValues
{
    private GenBucket main;
    private long blockSpeed;
    private Set<Material> ignoredMaterials;
    private Set<Material> deleteBlacklist;
    private String giveMessage;
    private String recieveMessage;
    private String noPermissionCommandMessage;
    private String noPermissionPlaceMessage;
    private String noFactionMessage;
    private String onlyClaimMessage;
    private String placeNormalMessage;
    private String placeInfiniteMessage;
    private String notEnoughMoneyPlaceMessage;
    private String notEnoughMoneyBuyMessage;
    private String wrongDirectionMessage;
    private String noSpaceBuyMessage;
    private String cannotPlaceYLevelMessage;
    private String buyConfirmationMessage;
    private String nearbyPlayerMessage;
    private ItemStack exitItemStack;
    private ItemStack fillItemStack;
    private String guiTitle;
    private String exitName;
    private String fillName;
    private List<String> exitLore;
    private List<String> fillLore;
    private List<Integer> exitSlots;
    private Set<String> recipeBuckets;
    private ItemStack gravityBlock;
    
    public ConfigValues(final GenBucket main) {
        this.main = main;
    }
    
    public void loadBuckets() {
        this.main.getBucketManager().getBuckets().clear();
        if (this.main.getConfig().contains("items")) {
            for (final String bucketId : this.main.getConfig().getConfigurationSection("items").getKeys(false)) {
                final Bucket bucket = this.main.getBucketManager().createBucket(bucketId);
                final ItemStack item = this.main.getUtils().itemFromString(this.main.getConfig().getString("items." + bucketId + ".item.material").toUpperCase());
                ItemMeta itemMeta = item.getItemMeta();
                itemMeta.setDisplayName(Utils.color(this.main.getConfig().getString("items." + bucketId + ".item.name")));
                itemMeta.setLore((List)Utils.colorLore(this.main.getConfig().getStringList("items." + bucketId + ".item.lore")));
                item.setItemMeta(itemMeta);
                if (this.main.getConfig().getBoolean("items." + bucketId + ".item.glow")) {
                    this.main.getUtils().addGlow(item);
                }
                bucket.setItem(item);
                bucket.setBlockItem(this.main.getUtils().itemFromString(this.main.getConfig().getString("items." + bucketId + ".block.material")));
                try {
                    bucket.setDirection(Bucket.Direction.valueOf(this.main.getConfig().getString("items." + bucketId + ".block.direction").toUpperCase()));
                }
                catch (IllegalArgumentException ex) {
                    this.main.getBucketManager().getBuckets().remove(bucketId);
                    continue;
                }
                bucket.setByChunk(this.main.getConfig().getBoolean("items." + bucketId + ".block.count-by-chunk"));
                bucket.setPatch(this.main.getConfig().getBoolean("items." + bucketId + ".block.patch"));
                bucket.setDelete(this.main.getConfig().getBoolean("items." + bucketId + ".block.delete"));
                final ItemStack guiItem = this.main.getUtils().itemFromString(this.main.getConfig().getString("items." + bucketId + ".gui.material").toUpperCase());
                itemMeta = guiItem.getItemMeta();
                itemMeta.setDisplayName(Utils.color(this.main.getConfig().getString("items." + bucketId + ".gui.name")));
                itemMeta.setLore((List)Utils.colorLore(this.main.getConfig().getStringList("items." + bucketId + ".gui.lore")));
                guiItem.setItemMeta(itemMeta);
                if (this.main.getConfig().getBoolean("items." + bucketId + ".gui.glow")) {
                    this.main.getUtils().addGlow(guiItem);
                }
                bucket.setGuiItem(guiItem);
                bucket.setSlot(this.main.getConfig().getInt("items." + bucketId + ".gui.slot"));
                bucket.setBuyPrice(this.main.getConfig().getDouble("items." + bucketId + ".buy-price"));
                bucket.setPlacePrice(this.main.getConfig().getDouble("items." + bucketId + ".place-price"));
                bucket.setInfinite(this.main.getConfig().getBoolean("items." + bucketId + ".infinite"));
                bucket.setIngredients(null);
                final Map<Character, ItemStack> ingredients = new HashMap<Character, ItemStack>();
                if (this.main.getConfig().contains("recipes." + bucketId + ".symbols")) {
                    for (final String ingredientSymbol : this.main.getConfig().getConfigurationSection("recipes." + bucketId + ".symbols").getKeys(false)) {
                        final ItemStack ingredientItem = this.main.getUtils().itemFromString(this.main.getConfig().getString("recipes." + bucketId + ".symbols." + ingredientSymbol));
                        ingredients.put(ingredientSymbol.toCharArray()[0], ingredientItem);
                    }
                    if (ingredients.size() > 0) {
                        bucket.setIngredients(ingredients);
                    }
                }
                bucket.setRecipeShape(null);
                if (this.main.getConfig().contains("recipes." + bucketId + ".recipe")) {
                    final List<String> shapeList = (List<String>)this.main.getConfig().getStringList("recipes." + bucketId + ".recipe");
                    if (shapeList.size() == 3) {
                        bucket.setRecipeShape(shapeList);
                    }
                }
                if (this.main.getConfig().contains("recipes." + bucketId + ".outcome-amount")) {
                    bucket.setRecipeAmount(this.main.getConfig().getInt("recipes." + bucketId + ".outcome-amount"));
                }
                else {
                    bucket.setRecipeAmount(0);
                }
                this.exitSlots = (List<Integer>)this.main.getConfig().getIntegerList("gui.exit.slots");
            }
        }
        double bps = this.main.getConfig().getDouble("speed");
        if (bps < 1.0) {
            bps = 1.0;
        }
        else if (bps > 20.0) {
            bps = 20.0;
        }
        this.blockSpeed = Math.round(1.0 / bps * 20.0);
        this.ignoredMaterials = EnumSet.noneOf(Material.class);
        for (final String rawMaterial : this.main.getConfig().getStringList("ignored-blocks")) {
            try {
                this.ignoredMaterials.add(Material.valueOf(rawMaterial));
            }
            catch (Exception ex2) {}
        }
        this.deleteBlacklist = EnumSet.noneOf(Material.class);
        for (final String rawMaterial : this.main.getConfig().getStringList("delete-blacklist")) {
            try {
                this.deleteBlacklist.add(Material.valueOf(rawMaterial));
            }
            catch (Exception ex3) {}
        }
        this.giveMessage = Utils.color(this.main.getConfig().getString("messages.give"));
        this.recieveMessage = Utils.color(this.main.getConfig().getString("messages.receive"));
        this.noPermissionCommandMessage = Utils.color(this.main.getConfig().getString("messages.no-permission-command"));
        this.noPermissionPlaceMessage = Utils.color(this.main.getConfig().getString("messages.no-permission-place"));
        this.noFactionMessage = Utils.color(this.main.getConfig().getString("messages.no-faction"));
        this.onlyClaimMessage = Utils.color(this.main.getConfig().getString("messages.cannot-place-claim"));
        this.placeNormalMessage = Utils.color(this.main.getConfig().getString("messages.place-message-regular"));
        this.placeInfiniteMessage = Utils.color(this.main.getConfig().getString("messages.place-message-infinite"));
        this.notEnoughMoneyPlaceMessage = Utils.color(this.main.getConfig().getString("messages.not-enough-money-place"));
        this.notEnoughMoneyBuyMessage = Utils.color(this.main.getConfig().getString("messages.not-enough-money-shop"));
        this.wrongDirectionMessage = Utils.color(this.main.getConfig().getString("messages.wrong-direction"));
        this.noSpaceBuyMessage = Utils.color(this.main.getConfig().getString("messages.not-enough-space-buy"));
        this.cannotPlaceYLevelMessage = Utils.color(this.main.getConfig().getString("messages.cannot-place-y-level"));
        this.buyConfirmationMessage = Utils.color(this.main.getConfig().getString("messages.buy-success"));
        this.nearbyPlayerMessage = Utils.color(this.main.getConfig().getString("messages.cannot-place-nearby-players"));
        this.exitItemStack = this.main.getUtils().itemFromString(this.main.getConfig().getString("gui.exit.item"));
        this.fillItemStack = this.main.getUtils().itemFromString(this.main.getConfig().getString("gui.fill.item"));
        this.guiTitle = Utils.color(this.main.getConfig().getString("gui.title"));
        this.exitName = Utils.color(this.main.getConfig().getString("gui.exit.name"));
        this.fillName = Utils.color(this.main.getConfig().getString("gui.fill.name"));
        this.exitLore = Utils.colorLore(this.main.getConfig().getStringList("gui.exit.lore"));
        this.fillLore = Utils.colorLore(this.main.getConfig().getStringList("gui.fill.lore"));
        if (this.main.getConfig().getConfigurationSection("recipes") != null) {
            this.recipeBuckets = new HashSet<String>();
            for (final String key : this.main.getConfig().getConfigurationSection("recipes").getKeys(false)) {
                if (this.main.getBucketManager().getBuckets().containsKey(key)) {
                    this.recipeBuckets.add(key);
                }
            }
        }
        else {
            this.recipeBuckets = null;
        }
        this.gravityBlock = this.main.getUtils().itemFromString(this.main.getConfig().getString("gravity-block"));
    }
    
    public Long getBlockSpeedDelay() {
        return this.blockSpeed;
    }
    
    public Set<Material> getIgnoredBlockList() {
        return this.ignoredMaterials;
    }
    
    public boolean giveShouldDropItem() {
        return this.main.getConfig().getBoolean("give-drop-item-if-full");
    }
    
    public boolean shopShouldDropItem() {
        return this.main.getConfig().getBoolean("shop-drop-item-if-full");
    }
    
    public String getGiveMessage(final Player p, final int amount, final Bucket bucket) {
        return this.giveMessage.replace("{player}", p.getName()).replace("{amount}", String.valueOf(amount)).replace("{bucket}", bucket.getId());
    }
    
    public String getReceiveMessage(final int amount, final double price, final Bucket bucket) {
        return this.recieveMessage.replace("{amount}", String.valueOf(amount)).replace("{price}", String.valueOf(price)).replace("{amount}", String.valueOf(amount)).replace("{bucket}", bucket.getId());
    }
    
    public String getNoPermissionCommandMessage() {
        return this.noPermissionCommandMessage;
    }
    
    public String getNoPermissionPlaceMessage() {
        return this.noPermissionPlaceMessage;
    }
    
    public String getNoFactionMessage() {
        return this.noFactionMessage;
    }
    
    public String getOnlyClaimMessage() {
        return this.onlyClaimMessage;
    }
    
    public String getPlaceNormalMessage(final double money) {
        return this.placeNormalMessage.replace("{money}", String.valueOf(money));
    }
    
    public String getPlaceInfiniteMessage(final double money) {
        return this.placeInfiniteMessage.replace("{money}", String.valueOf(money));
    }
    
    public String notEnoughMoneyPlaceMessage(final double money) {
        return this.notEnoughMoneyPlaceMessage.replace("{cost}", String.valueOf(money));
    }
    
    public String notEnoughMoneyBuyMessage(final double money) {
        return this.notEnoughMoneyBuyMessage.replace("{cost}", String.valueOf(money));
    }
    
    public String getWrongDirectionMessage() {
        return this.wrongDirectionMessage;
    }
    
    public String getNoSpaceBuyMessage() {
        return this.noSpaceBuyMessage;
    }
    
    public String getCannotPlaceYLevelMessage() {
        return this.cannotPlaceYLevelMessage;
    }
    
    public String getBuyConfirmationMessage(final int amount) {
        return this.buyConfirmationMessage.replace("{amount}", String.valueOf(amount));
    }
    
    public String getNearbyPlayerMessage() {
        return this.nearbyPlayerMessage;
    }
    
    public int getVerticalTravel() {
        return this.main.getConfig().getInt("vertical-travel");
    }
    
    public int getHorizontalTravel() {
        return this.main.getConfig().getInt("horizontal-travel");
    }
    
    public boolean needsFaction() {
        return this.main.getConfig().getBoolean("factions.requires-faction");
    }
    
    public boolean cantPlaceWilderness() {
        return !this.main.getConfig().getBoolean("factions.can-place-wilderness");
    }
    
    public boolean isFactionsHookEnabled() {
        return this.main.getConfig().getBoolean("hooks.factions");
    }
    
    public boolean isWorldGuardHookEnabled() {
        return this.main.getConfig().getBoolean("hooks.worldguard");
    }
    
    public boolean isWorldBorderHookEnabled() {
        return this.main.getConfig().getBoolean("hooks.worldborder");
    }
    
    public boolean isCoreProtectHookEnabled() {
        return this.main.getConfig().getBoolean("hooks.coreprotect");
    }
    
    public boolean isGUIEnabled() {
        return this.main.getConfig().getBoolean("gui.enabled");
    }
    
    public int getGUIRows() {
        return this.main.getConfig().getInt("gui.rows");
    }
    
    public boolean addBlockUnderGravity() {
        return this.main.getConfig().getBoolean("add-block-under-gravity-blocks");
    }
    
    public ItemStack getGravityBlock() {
        return this.gravityBlock;
    }
    
    public ItemStack getExitItemStack() {
        return this.exitItemStack;
    }
    
    public ItemStack getFillItemStack() {
        return this.fillItemStack;
    }
    
    public String getGUITitle() {
        return this.guiTitle;
    }
    
    public String getExitName() {
        return this.exitName;
    }
    
    public String getFillName() {
        return this.fillName;
    }
    
    public List<String> getExitLore() {
        return this.exitLore;
    }
    
    public List<String> getFillLore() {
        return this.fillLore;
    }
    
    public List<Integer> getExitSlots() {
        return this.exitSlots;
    }
    
    public boolean isExitGlowing() {
        return this.main.getConfig().getBoolean("gui.exit.glow");
    }
    
    public boolean isFillGlowing() {
        return this.main.getConfig().getBoolean("gui.fill.glow");
    }
    
    public int getBulkBuyAmount() {
        return this.main.getConfig().getInt("gui.bulk-buy-amount");
    }
    
    public boolean showUpdateMessage() {
        return this.main.getConfig().getBoolean("show-update-messages");
    }
    
    public int getMaxY() {
        return this.main.getConfig().getInt("height-limit");
    }
    
    public int getMaxChunks() {
        return this.main.getConfig().getInt("chunk-travel");
    }
    
    Set<String> getRecipeBuckets() {
        return this.recipeBuckets;
    }
    
    double getConfigVersion() {
        return this.main.getConfig().getDouble("config-version");
    }
    
    public boolean bucketsDisappearDrop() {
        return this.main.getConfig().getBoolean("infinite-buckets-disappear");
    }
    
    public double getPlayerCheckRadius() {
        return this.main.getConfig().getDouble("player-check-radius");
    }
    
    public double getSpongeCheckRadius() {
        return this.main.getConfig().getDouble("sponge-check-radius");
    }
    
    public boolean cancellingEnabled() {
        return this.main.getConfig().getBoolean("enable-cancelling");
    }
    
    public Set<Material> getDeleteBlacklist() {
        return this.deleteBlacklist;
    }
}
