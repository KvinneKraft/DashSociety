// Author: Dashie
// Version: 1.0

package com.recoded.login;

import com.recoded.DashSec;
import com.recoded.DashSecurity;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public class Login
{
    private final FileConfiguration config = (FileConfiguration) DashSecurity.config;
    private final JavaPlugin plugin = (JavaPlugin) DashSecurity.plugin;
    
    private final DashSec sec = new DashSec();
    
    public void Enable()
    {
        if (!config.getBoolean("dash-login.enabled"))
        {
            sec.print("Dash Login has been disabled in the configuration file (config.yml) skipping ....");
            return;
        };
    };
};
