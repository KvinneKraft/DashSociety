

// Author: Dashie
// Version: 1.0
 

package com.dashbows;


import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.ProjectileHitEvent;
import org.bukkit.plugin.java.JavaPlugin;


public class DashBows extends JavaPlugin implements Listener, CommandExecutor
{
    private HashMap<String, Integer> bow_keys = new HashMap<>();
    
    
    @EventHandler public void onProjectileHit(ProjectileHitEvent e)
    {
        
    };
    
    
    private String bow_list;
    
    
    @Override public boolean onCommand(CommandSender s, Command c, String a, String[] as)
    {
        //Apply later on.
        
        return true;
    };
    
    
    static FileConfiguration config;
    static JavaPlugin plugin;
    
    
    private void load_data()
    {
        bow_keys.put("firework", 500);
        bow_keys.put("lightning", 501);
        bow_keys.put("nuclear", 502);
        bow_keys.put("poison", 503);
        
        String bow_cache = color("&6(Available Dash Bows): &d");
        
        for(String bow_name : bow_keys.keySet())
        {
            bow_name = bow_name.substring(1, bow_name.length()).toUpperCase() + bow_name.substring(0, 0);
            bow_cache += bow_name + ", ";
        };
        
        bow_list = bow_cache.substring(bow_cache.length() - 2, bow_cache.length());
    };
    
    
    @Override public void onEnable()
    {
        print("Loading Dash Bows 1.0 ....");
        
        saveDefaultConfig();
        
        config = (FileConfiguration)getConfig();
        plugin = (JavaPlugin)this;
        
        getServer().getPluginManager().registerEvents(this, plugin);
        getCommand("dashbows").setExecutor(plugin);
        
        print("Dash Bows 1.0 has been loaded!");
    };
    
    
    public static void print(String str)
    {
        System.out.println("(Dash Bows): " + str);
    };
    
    public static String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
};
