
package com.dashstrict;

import com.dashstrict.commands.CommandHandlers;
import com.dashstrict.events.EventHandlers;
import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.Plugin;
import org.bukkit.plugin.java.JavaPlugin;


class Tools
{
    public String transStr(String msg)
    {
        return ChatColor.translateAlternateColorCodes('&', msg);
    };
    
    public void print(String msg)
    {
        System.out.println(transStr("(DashStrict): " + msg));
    };
};


public class DashStrict extends JavaPlugin
{
    public static FileConfiguration config;
    public static Plugin plugin;
    
    public static Tools tools = new Tools();
    
    @Override
    public void onEnable()
    {
        tools.print("Loading the plugin ....");
        
        saveDefaultConfig();
        
        config = (FileConfiguration)getConfig();
        plugin = (Plugin)this;
        
        getServer().getPluginManager().registerEvents(new EventHandlers(), plugin);
        getCommand("dashstrict").setExecutor(new CommandHandlers());
        
        tools.print("Plugin is now running!");
    };
    
    @Override
    public void onDisable()
    {
        tools.print("The plugin is now disabled.");
    };
};
