
// Author: Dashie
// Version: 1.0

package com.breakables;

import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public class Boomy extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    @Override public void onEnable()
    {
        print("Plugin is initializing ....");
        
        print("Plugin has been enabled!");
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
    
    public static String color(final String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };    
    
    public static void print(final String str)
    {
        System.out.println("(TNT vs Bedrock): " + str);
    };
};