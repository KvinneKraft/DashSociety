
package com.dashrays;

import java.util.ArrayList;
import java.util.List;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public class DashRays extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    public static List<String> names = new ArrayList<String>();    
    
    public static EventsHandler eventsHandler;
    
    @Override
    public void onEnable()
    {
        Luna.print("Initializing the Dash Rays plugin ....");
        
        saveDefaultConfig();
        
        plugin = this;        
        config = getConfig();
        
        eventsHandler = new EventsHandler();
        
        if(eventsHandler.blocks.size() < 1)
            eventsHandler.blocks = config.getStringList("properties.blocks");            
        
        getServer().getPluginManager().registerEvents(eventsHandler, this);
        getCommand("dashrays").setExecutor(new CommandsHandler());
                
        Luna.print("The Dash Rays plugin has been loaded successfully!");
    };
    
    @Override
    public void onDisable()
    {
        Luna.print("Aw, the plugin has been disabled.");
    };
};
