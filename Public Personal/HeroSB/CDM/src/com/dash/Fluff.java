
// Author: Dashie
// Version: 1.0

package com.dash;

import org.bukkit.ChatColor;
import org.bukkit.event.Listener;
import org.bukkit.plugin.java.JavaPlugin;

public class Fluff extends JavaPlugin implements Listener
{
    @Override public void onEnable()
    {
        print("Loading plugin ....");
        
        getServer().getPluginManager().registerEvents(this, this);
        
        print("Plugin has been loaded!");
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
    
    private void print(final String str)
    {
        System.out.println("(CDM): " + str);
    };
    
    private String color(final String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
};