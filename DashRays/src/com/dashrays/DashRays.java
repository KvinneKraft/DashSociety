
package com.dashrays;

import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public class DashRays extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    @Override
    public void onEnable()
    {
        Luna.print("Initializing the Dash Rays plugin ....");
        
        saveDefaultConfig();
        
        plugin = this;        
        config = getConfig();
        
        // Register Events:
        getServer().getPluginManager().registerEvents(new EventsHandler(), this);
        
        Luna.print("The Dash Rays plugin has been loaded successfully!");
    };
    
    @Override
    public void onDisable()
    {
        Luna.print("Aw, the plugin has been disabled.");
    };
};
