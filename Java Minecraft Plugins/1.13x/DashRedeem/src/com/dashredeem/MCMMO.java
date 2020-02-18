
// Author: Dashie
// Version: 1.0

package com.dashredeem;


import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;


public class MCMMO extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    public static CommandsHandler commands = new CommandsHandler();
    public static EventsHandler events = new EventsHandler();
    
    
    @Override
    public void onEnable()
    {
        Kvinne.print("Plugin is being enabled ....");
        
        saveDefaultConfig();
        plugin = this;
        
        getServer().getPluginManager().registerEvents(events, plugin);
        getCommand("dashredeem").setExecutor(commands);
        
        Kvinne.print("Plugin has been enabled.");
    };
    
    @Override
    public void onDisable()
    {
        Kvinne.print("Plugin has been disabled.");
    };
};


class Kvinne
{
    public static void print(String str)
    {
        System.out.println("(MCMMO Redeemables): " + str);
    };
};