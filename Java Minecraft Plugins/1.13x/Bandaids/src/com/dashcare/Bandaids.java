
// Author: Dashie
// Version: 1.0

package com.dashcare;


import java.util.ArrayList;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.Effect;
import org.bukkit.Location;
import org.bukkit.Material;
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


public class Bandaids extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    
    public static CommandsHandler commands = new CommandsHandler();
    public static EventsHandler events = new EventsHandler();
    
    
    @Override
    public void onEnable()
    {
        Lunaris.print("Loading the plugin ....");
        
        saveDefaultConfig();
        
        plugin = this;
               
        commands.refresh_data();
        events.refresh_data();
        
        refresh_data();        
        
        Lunaris.print("Plugin has been loaded!");
    };
    
    
    @Override
    public void onDisable()
    {
        Lunaris.print("Plugin has been disabled!");
    };
  
    
    static List<String> bandaid_lore = new ArrayList<>();
    static Material bandaid_material;    
    static String bandaid_name;
    
    
    public void refresh_data()
    {
        plugin.reloadConfig();
        config = plugin.getConfig();
        
        bandaid_lore = config.getStringList("bandaid-properties.bandaid-lore");
        for(Integer id = 0; id < bandaid_lore.size(); id += 1)
        {
            bandaid_lore.set(id, Lunaris.colors(bandaid_lore.get(id)));
        };
        
        bandaid_material = Material.getMaterial(config.getString("bandaid-properties.bandaid-material").toUpperCase().replace(" ", "_"));
        if(bandaid_material == null)
        {
            Lunaris.print("The given material is invalid, setting it to PAPER!");
            bandaid_material = Material.PAPER;
        };
        
        bandaid_name = Lunaris.colors(config.getString("bandaid-properties.bandaid-name"));
    };
    
    public static ItemStack get_bandaid(Integer amount)
    {
        ItemStack bandaid = new ItemStack(bandaid_material, amount);
        ItemMeta bandaid_meta = bandaid.getItemMeta();
        
        bandaid_meta.setCustomModelData(2020);
        bandaid_meta.setDisplayName(bandaid_name);
        bandaid_meta.setLore(bandaid_lore);
        
        return bandaid;
    };
};



class EventsHandler implements Listener
{
    List<PotionEffect> potion_effects = new ArrayList<>();
    
    boolean summon_lightning, summon_fireworks;
    
    String use_permission, deny_message, apply_message;
    
    
    public void refresh_data()
    {
        Bandaids.plugin.reloadConfig();;
        Bandaids.config = Bandaids.plugin.getConfig();
        
        summon_lightning = Bandaids.config.getBoolean("bandaid-properties.summon-lightning");
        summon_fireworks = Bandaids.config.getBoolean("bandaid-properties.summon-fireworks");
        
        use_permission = Lunaris.colors(Bandaids.config.getString("bandaid-properties.use-permission"));
        apply_message = Lunaris.colors(Bandaids.config.getString("bandaid-properties.apply-message"));
        deny_message = Lunaris.colors(Bandaids.config.getString("bandaid-properties.permission-deny-message"));
        
        if(potion_effects.size() > 0)
        {
            potion_effects.clear();
        };
        
        for(String str : Bandaids.config.getStringList("bandaid-properties.bandaid-effects"))
        {
            String[] arr = str.toUpperCase().split(" ");
            
            if(arr.length < 3)
            {
                Lunaris.print("Found invalid bandaid effect format, skipping ....");
                continue;
            };
            
            PotionEffectType effect = PotionEffectType.getByName(arr[0]);
            
            if(effect == null)
            {
                Lunaris.print("Found invalid bandaid effect type, skipping ....");
                continue;
            };
            
            Integer amplifier = Integer.valueOf(arr[1]);
            Integer duration = Integer.valueOf(arr[2]);            
            
            if((duration == null) || (amplifier == null))
            {
                Lunaris.print("You must specify a correct amplifier and or duration.");
                continue;
            }            
            
            else if(amplifier < 1)
            {
                Lunaris.print("Amplifier for bandaid effect is too low, skipping ....");
                continue;
            }
            
            else if(duration < 1)
            {
                Lunaris.print("The duration is too low, skipping ....");
                continue;
            };
            
            potion_effects.add(new PotionEffect(effect, amplifier, duration));
        };
    };
    
    
    @EventHandler
    public void onInteract(PlayerInteractEvent e)
    {
        ItemStack item = new ItemStack(e.getItem().getType(), 1);
        
        if((item == null) || (!item.hasItemMeta()) || (item.getItemMeta().getCustomModelData() != 2020))
        {
            return;
        };
        
        Player p = e.getPlayer();
        
        if(!p.hasPermission(use_permission))
        {
            p.sendMessage(deny_message);
            return;
        }
        
        else if(potion_effects.size() > 0)
        {
            p.addPotionEffects(potion_effects);
        };
        
        p.getInventory().remove(item);        
        
        if((summon_lightning) || (summon_fireworks))
        {
            Location location = p.getLocation();
            
            p.setInvulnerable(true);
            
            if(summon_lightning)
            {
                location.getWorld().strikeLightningEffect(location);
            };
            
            if(summon_fireworks)
            {
                //Spawn fireworks<<<
            };
            
            p.setInvulnerable(false);
        };
        
        p.sendMessage(apply_message);
    };
};



class Lunaris
{
    public static void print(String str)
    {
        System.out.println("(Dash Bandaids): " + str);
    };
    
    
    public static String colors(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
};
