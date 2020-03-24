

// Author: Dashie
// Version: 1.0


package com.dashcraft;


import org.bukkit.ChatColor;
import org.bukkit.plugin.java.JavaPlugin;


public class DashHat extends JavaPlugin
{
    public static void print(String str)
    {
        System.out.println("(Dash Hat): " + str);
    };
    
    public static String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    @Override public void onEnable()
    {
        
    };
    
    @Override public void onDisable()
    {
        
    };
};
