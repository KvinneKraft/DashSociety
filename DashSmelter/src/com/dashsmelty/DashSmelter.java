
// Author: Dashie
// Version: 1.0

package com.dashsmelty;

import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public class DashSmelter extends JavaPlugin
{
    public static FileConfiguration config;
    
    @Override
    public void onEnable()
    {
        Moon.print("The plugin is currently being initialized ....");
        
        saveDefaultConfig();
        
        config = getConfig();
        
        Furnace furnace = new Furnace();
        furnace.RegisterRecipes();
        
        Moon.print("The plugin is now running!");
    };
    
    @Override
    public void onDisable()
    {
        Moon.print("The plugin has been disabled.");
    };
};
