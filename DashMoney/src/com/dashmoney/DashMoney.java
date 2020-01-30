
// Author: Dashie
// Version: 1.0

package com.dashmoney;


import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;



public class DashMoney extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    String Author = "Dashie";
    String PluginName = "Dash Money";
    String Version = "1.0";
    
    @Override
    public void onEnable()
    {
        Moon.print("Initializing plugin ....");
        
        saveDefaultConfig();
        
        config = getConfig();
        plugin = (JavaPlugin)this;
        
        Moon.print(Author);
        Moon.print(PluginName);
        Moon.print(Version);
        
        Moon.print("Plugin is now running!");
    };
    
    @Override
    public void onDisable()
    {
        
    };
};
