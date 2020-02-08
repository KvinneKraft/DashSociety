
// Author: Dashie
// Version: 1.0


package com.dashstrict;


import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.command.Command;

import org.bukkit.event.Listener;

import org.bukkit.plugin.java.JavaPlugin;


public class Strict extends JavaPlugin
{
    @Override
    public void onEnable()
    {
        
    };
    
    @Override
    public void onDisable()
    {
        
    }
    

    /********************************/    
    /*(Dashies Events Handler Class)*/
    /********************************/
    
    class EventsHandler implements Listener
    {
        
    };
    
    
    /**********************************/    
    /*(Dashies Commands Handler Class)*/
    /**********************************/    
    
    class CommandsHandler implements CommandExecutor
    {
        @Override
        public boolean onCommand(CommandSender s, Command c, String a, String[] as)
        {
            return true;
        };
    };
};
