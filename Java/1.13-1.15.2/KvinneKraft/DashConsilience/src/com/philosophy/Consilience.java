
// Since this is a plugin for my own minecraft server. (kvinnekraft.serverminer.com) I will be 
// using static (hard coded) values, because it is only for personal use. ^

// Author: Dashie
// Version: 1.0

package com.philosophy;

import net.milkbowl.vault.economy.Economy;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public class Consilience extends JavaPlugin
{
    public static FileConfiguration config = (FileConfiguration) null;
    public static JavaPlugin plugin = (JavaPlugin) null;
    public static Economy econ = (Economy) null;
    
    @Override public void onEnable()
    {
        Freya.print("Doing some necessities ....");
        
        plugin = (JavaPlugin) this;
        econ = (Economy) getServer().getServicesManager().getRegistration(Economy.class).getProvider();
        
        Configuration.LoadConfiguration();
        
        getServer().getPluginManager().registerEvents(new EventsHandler(), plugin);        
        
        final String[] commands = new String[] 
        { 
            "discord", "github", "shop", "staff", "rules", "bee"
        };
       
        final SimplisticHandler simplistic = new SimplisticHandler();        
        
        for (String command : commands)
        {
            getCommand(command).setExecutor(simplistic);
        };
        
        getCommand("spawn").setExecutor(new Spawn());        
        
        Freya.print("Done!");
    };
    
    @Override public void onDisable()
    {
        Freya.print("Oh, I have been terminated.");
    };
};