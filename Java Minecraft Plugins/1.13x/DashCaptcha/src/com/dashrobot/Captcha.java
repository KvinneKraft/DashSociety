
// Author: Dashie
// Version: 1.0


package com.dashrobot;


import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.ChatColor;


//---//
//---// Main Captcha Class:
//---//

public class Captcha extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    
    //---//
    //---// Core Methods: 
    //---//
    
    @Override
    public void onEnable()
    {
        print("The plugin is being enabled ....");
        
        
        
        print("The plugin has been enabled!");
    };
    
    
    @Override
    public void onDisable()
    {
        print("The plugin has been disabled!");
    };
    
    
    //---//
    //---// Dash Utilities:
    //---//
    
    public static void print(String str)
    {
        System.out.println("(Dash Captcha): " + str);
    };
    
    
    public static String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
};
