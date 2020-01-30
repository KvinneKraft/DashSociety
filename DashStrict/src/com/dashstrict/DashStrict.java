
package com.dashstrict;

import com.dashcraft.tools.Tools;
import com.dashstrict.commands.CommandHandlers;
import com.dashstrict.events.EventHandlers;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.Plugin;
import org.bukkit.plugin.java.JavaPlugin;


public class DashStrict extends JavaPlugin
{    
    public static FileConfiguration config;
    public static Plugin plugin;
    
    public Tools tools = new Tools();
    
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
