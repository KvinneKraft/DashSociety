
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
        // Reload Method
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
        // Reload Method
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
        };
        
        p.addPotionEffects(potion_effects);
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
