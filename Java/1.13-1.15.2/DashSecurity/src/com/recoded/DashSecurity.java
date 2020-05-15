// Author: Dashie
// Version: 1.0

package com.recoded;

import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public class DashSecurity extends JavaPlugin
{
    public static FileConfiguration config = (FileConfiguration) null;
    public static JavaPlugin plugin = (JavaPlugin) null;
    
    private final DashSec sec = new DashSec();
    
    @Override public void onEnable()
    {
        sec.print("Loading the plugin ....");
        
        saveDefaultConfig();
        
        config = (FileConfiguration) getConfig();
        plugin = (JavaPlugin) this;
        
        sec.print("The plugin has been loaded!");
    };
    
    @Override public void onDisable()
    {
        getServer().getScheduler().cancelTasks(plugin);
     
        sec.print("The plugin has been disabled!");
    };
};