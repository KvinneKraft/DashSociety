
// Author: Dashie
// Version: 1.0

package com.dashcollection;


import org.bukkit.ChatColor;
import org.bukkit.Server;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.scheduler.BukkitTask;


public class Session extends JavaPlugin
{
    static FileConfiguration config;
    static JavaPlugin plugin;  
    
    static CommandsHandler commands;    
    static EventsHandler events; 
    static Moony moon;      
    
    @Override
    public void onEnable()
    {
        moon.print("Loading plugin ....");        
        
        server = getServer();        
        plugin = this;
        
        saveDefaultConfig();
        refreshDashData();        
        
        commands = new CommandsHandler();
        events = new EventsHandler();
        moon = new Moony();
        
        getServer().getPluginManager().registerEvents(events, plugin);
        getCommand("dashsession").setExecutor(commands);
        
        moon.print("Plugin has been loaded!");
    };
    
    static Server server;
    
    public static void refreshDashData()
    {
        for(BukkitTask runnable : events.runnables)
        {
            runnable.cancel();
        };
        
        events.runnables.clear();
        events.p_uuid.clear();
        
        plugin.reloadConfig();
        config = plugin.getConfig();

        for(String str : config.getStringList("reward-properties.rewards"))
        {
            String[] arr = str.split("(chance):");
            
            if(arr.length < 2)
            {
                moon.print("Invalid format received. Skipping ....");
                return;
            };
            
            Double chance = Double.valueOf(arr[1]);
            
            if(chance == null)
            {
                moon.print("Invalid chance specified in config. Skipping ....");
                return;
            };
            
            String command = arr[0];            
            
            events.randomizer.add(chance, command);
        };
        
        events.reward_permission = config.getString("reward-properties.reward-permission");
        events.reward_interval = config.getInt("reward-properties.reward-interval");       
        events.reward_message = moon.transStr(config.getString("reward-properties.reward-message"));          
        
        for(Player p : server.getOnlinePlayers())
        {
            if(!p.hasPermission(events.reward_permission))
            {
                return;
            };
                
            events.add_reward_task(p, p.getUniqueId());
        };
        
        commands.command_permission = config.getString("optional-properties.command-permission");
    };
    
    @Override
    public void onDisable()
    {
        moon.print("Plugin has been disabled!");
    };
    
    
    class Moony
    {
        public void print(String str)
        {
            System.out.println("(Dash Session): " + str);
        };
        
        public String transStr(String str)
        {
            return ChatColor.translateAlternateColorCodes('&', str);
        };
    };
};
