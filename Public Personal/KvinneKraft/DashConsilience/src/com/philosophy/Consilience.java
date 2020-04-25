
// Since this is a plugin for my own minecraft server. (kvinnekraft.serverminer.com) I will be 
// using static (hard coded) values, because it is only for personal use. ^

// Author: Dashie
// Version: 1.0

package com.philosophy;

import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public class Consilience extends JavaPlugin
{
    public static FileConfiguration config = (FileConfiguration) null;
    public static JavaPlugin plugin = (JavaPlugin) null;
    
    @Override public void onEnable()
    {
        Freya.print("Doing some necessities ....");
        
        Fundamentals.LoadConfiguration();
        
        getCommand("spawn");
        getCommand("staff");
        getCommand("discord");
        
        Freya.print("Done!");
    };
    
    @Override public void onDisable()
    {
        Freya.print("Oh, I have been terminated.");
    };
};