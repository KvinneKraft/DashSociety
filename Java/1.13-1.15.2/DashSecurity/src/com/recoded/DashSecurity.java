// Author: Dashie
// Version: 1.0

package com.recoded;

public class DashSecurity extends JavaPlugin
{
    private final DashSec sec = new DashSec();
    
    @Override public void onEnable()
    {
        sec.print("Plugin is being enabled ....");
        
        
        
        sec.print("Plugin has been enabled!");
    };
    
    public static FileConfiguration config = (FileConfiguration) null;
    public static JavaPlugin plugin = (JavaPlugin) this;
    
    @Override public void onDisable()
    {
        sec.print("Plugin has been disabled!");
    };
};