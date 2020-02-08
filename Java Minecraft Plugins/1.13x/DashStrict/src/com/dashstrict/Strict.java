
// Author: Dashie
// Version: 1.0


package com.dashstrict;


import com.dashstrict.extensions.ItemCraftEvent;

import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.command.Command;

import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;

import org.bukkit.entity.Player;

import org.bukkit.event.Listener;
import org.bukkit.event.EventHandler;
import org.bukkit.event.inventory.CraftItemEvent;

import org.bukkit.plugin.java.JavaPlugin;


public class Strict extends JavaPlugin
{
    /***************************/
    /*(Dashies Toggle Handlers)*/
    /***************************/
    
    public static FileConfiguration config;    
    public static JavaPlugin plugin;
    
    DashUtils luna = new DashUtils();    
    
    public static CommandsHandler commands;    
    public static EventsHandler events;
    
    @Override
    public void onEnable()
    {
        luna.print("Loading DashStrict 1.0 ....");
        
        saveDefaultConfig();
        
        config = getConfig();
        plugin = this;
        
        commands = new CommandsHandler();
        events = new EventsHandler();
        
        luna.print("DashStrict 1.0 has been loaded in!");
    };
    
    @Override
    public void onDisable()
    {
        luna.print("The plugin is now disabled.");
    }
    

    /********************************/    
    /*(Dashies Events Handler Class)*/
    /********************************/
    
    class EventsHandler implements Listener
    {
        public String action_not_allowed_message;
        
        public void LoadEventData()
        {
            config = getConfig();
            
            action_not_allowed_message = luna.transStr(config.getString("plugin-messages.action-not-allowed"));
        };
        
        /*******************************/        
        /*(Dashies Craft Event Handler)*/
        /*******************************/
        
        @EventHandler
        public void onItemCraft(CraftItemEvent e)
        {
            Player p = (Player) e.getViewers().get(0);
            
            if((p == null) | (!(p instanceof Player)))
            {
                return;
            };
            
            ItemCraftEvent.onCraft(e);
        };
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
    
    
    /*************************/
    /*(Dashies Utility Class)*/
    /*************************/    
    
    class DashUtils 
    {
        public void print(String str)
        {
            System.out.println("(Dash Strict): " + str);
        };
        
        public String transStr(String str)
        {
            return ChatColor.translateAlternateColorCodes('&', str);
        };
    };
};
