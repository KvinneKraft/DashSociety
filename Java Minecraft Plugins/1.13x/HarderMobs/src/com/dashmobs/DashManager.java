

// Author: Dashie
// Version: 1.0


package com.dashmobs;


import org.bukkit.configuration.file.FileConfiguration;


public class DashManager
{
    public static void reload_plugin()
    {
        HarderMobs.plugin.reloadConfig();
        HarderMobs.config = HarderMobs.plugin.getConfig();
        
        FileConfiguration config = HarderMobs.config;
        
        
    };
};
