
// Author: Dashie
// Version: 1.0

package com.dashnarok;


import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

class DashCore
{
    public String transText(String msg)
    {
        return ChatColor.translateAlternateColorCodes('&', msg);
    };
    
    public void Sys(String msg)
    {
        System.out.println("[Dashnarok]: " + transText(msg));
    };
};

public class DashJoin extends JavaPlugin
{
    DashCore xxx = new DashCore();
    
    public static FileConfiguration config;    
    public static JavaPlugin plugin;
    
    @Override
    public void onEnable()
    {
        xxx.Sys("Enabling ....");
        
        config = this.getConfig();
        plugin = (JavaPlugin)this;
        
        
        
        xxx.Sys("Plugin is now running!");
    };
    
    @Override
    public void onDisable()
    {
        xxx.Sys("The plugin is now Disabled!");
    };
};
