package dash.recoded;

import java.util.ArrayList;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.Sound;
import org.bukkit.command.CommandExecutor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;

public class bandaids extends JavaPlugin implements CommandExecutor, Listener
{
    private FileConfiguration config = (FileConfiguration) null;
    private final JavaPlugin plugin = (JavaPlugin) this;    
    
    @Override public void onEnable()
    {
        print("Plugin is loading ....");
        
        getServer().getPluginManager().registerEvents(this, plugin);        
        getCommand("betterbandaids").setExecutor(this);
        
        LoadConfiguration();
        
        print("Plugin has been loaded!");
    };
    
    private String admn_p, cons_p;
    
    private void LoadConfiguration()
    {
        saveDefaultConfig();
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        admn_p = config.getString("bandaids.permissions.admin");
        cons_p = config.getString("bandaids.permissions.use");
        
        try
        {
            sound = Sound.valueOf(config.getString("bandaids.medicine.sound-effect").toUpperCase().replace(" ", "_"));
            
            if (sound == null)
            {
                throw new Exception("Oofity");
            };
        }
        
        catch (final Exception e)
        {   
            print("The sound provided in the configuration file (config.yml) seems invalid!");
            print("Now using the default one (ENTITY_CHICKEN_STEP)!");
        };
        
        for (final String raw : config.getStringList("bandaids.medicine.effects"))
        {
            final String arr[] = raw.toUpperCase().split(" ");
            
            if (arr.length < 3)
            {
                print("Invalid potion effect found in the configuration file (config.yml), skipping!");
                continue;
            };
            
            try
            {
                PotionEffect effect = new PotionEffect(PotionEffectType.getByName(arr[0].toUpperCase()), Integer.valueOf(arr[1]), Integer.valueOf(arr[2]));
                
                if (effect == null)
                {
                    throw new Exception("Oofity");
                };
                
                effects.add(effect);
            }
            
            catch (final Exception e)
            {
                print("Invalid potion effect found in the configuration file (config.yml), skipping!");
            };
        };
        
        final ItemMeta meta = (ItemMeta) bandaid.getItemMeta();
        
        meta.setDisplayName(color(config.getString("bandaids.item.name")));
        
        final List<String> lore = new ArrayList<>();
        
        for (final String line : config.getStringList("bandaids.item.lore"))
        {
            lore.add(color(line));
        };
        
        if (lore.size() > 0)
        {
            meta.setLore(lore);
        };
        
        bandaid.setItemMeta(meta);
        
        if (messages.size() > 0)
        {
            messages.clear();
        };
        
        messages.add(color(config.getString("bandaids.messages.applied")));
        messages.add(color(config.getString("bandaids.messages.denied")));
        messages.add(color(config.getString("bandaids.messages.cooldown")));
        messages.add(color(config.getString("bandaids.messages.expired")));
        
        coldown = config.getInt("bandaids.medicine.cooldown") * 20;
    };
    
    private ItemStack bandaid = new ItemStack(Material.PAPER, 1);
    
    private final List<PotionEffect> effects = new ArrayList<>();    
    private final List<String> messages = new ArrayList<>();
    private final List<Player> players = new ArrayList<>();
    
    private Sound sound;    
    private int coldown;
    
    @EventHandler public void onPlayerInteract(final PlayerInteractEvent e)
    {
        final Player p = (Player) e.getPlayer();
        
        if (p.getInventory().getItemInMainHand() != bandaid)
        {
            return;
        }
        
        else if (!p.hasPermission(cons_p))
        {
            p.sendMessage(messages.get(1));
            return;
        }
        
        else if (players.contains(p))
        {
            p.sendMessage(messages.get(2));
            return;
        };
             
        p.sendMessage(messages.get(0));
        
        getServer().getScheduler().runTaskLater
        (
            plugin, 
                
            new Runnable()
            { 
                @Override
                public void run()
                {
                    p.getInventory().getItemInMainHand().setAmount(p.getInventory().getItemInMainHand().getAmount() - 1);
                };
            },
           
            5
        );
        
        p.playSound(p.getLocation(), sound, 20, 20);
        p.addPotionEffects(effects);
        
        if (p.isOp() || p.hasPermission(admn_p))
        {
            return;
        };
        
        getServer().getScheduler().runTaskLater
        (
            plugin, 
                
            new Runnable()
            {
                @Override
                public void run()
                {
                    if (players.contains(p))
                    {
                        if (p.isOnline())
                        {
                            p.sendMessage(color(messages.get(3)));
                        };
                        
                        players.remove(p);
                    };
                };
            }, 
                
            coldown
        );
        
        players.add(p);
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
    
    private String color(String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
    
    private void print(String line)
    {
        System.out.println("(Better Bandaids): " + line);
    };
};