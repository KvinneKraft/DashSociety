
// Author: Dashie
// Version: 1.0

package com.dashcare;


import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;


public class Bandaids extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    @Override
    public void onEnable()
    {
        Lunaris.print("Loading the plugin ....");
        
        saveDefaultConfig();
        
        plugin = this;
                
        // BANDAIDSSSS
        // Commands
        // Events
        
        Lunaris.print("Plugin has been loaded!");
    };
    
    @Override
    public void onDisable()
    {
        Lunaris.print("Plugin has been disabled!");
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
