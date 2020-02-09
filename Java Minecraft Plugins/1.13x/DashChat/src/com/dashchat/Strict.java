
// Author: Dashie
// Version: 1.0

package com.dashchat;


import java.util.ArrayList;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.AsyncPlayerChatEvent;
import org.bukkit.event.player.PlayerCommandPreprocessEvent;
import org.bukkit.plugin.java.JavaPlugin;


public class Strict extends JavaPlugin
{
    FileConfiguration config;// = getConfig();
    JavaPlugin plugin = this;
    
    Moony moon = new Moony();
    
    EventsHandler events = new EventsHandler();
    CommandsHandler commands = new CommandsHandler();
    
    
    @Override
    public void onEnable()
    {
        moon.print("The plugin is being loaded ....");
        
        saveDefaultConfig();
        
        ReloadPlugin();
        
        getServer().getPluginManager().registerEvents(events, plugin);
        getCommand("dashchat").setExecutor(commands);        
        
        moon.print("Author: Dashie");
        moon.print("Version: 1.0");
        moon.print("Email: KvinneKraft@protonmail.com");
        
        moon.print("The plugin has been loaded.");
    };
    
    
    class CommandsHandler implements CommandExecutor
    {
        boolean t = true, f = false;
        
        String admin_permission;
        
        String permission_denied = moon.transstr("&cYou are not permitted to use this command, huh?");
        String invalid_syntax = moon.transstr("&7Correct Syntax: &e/dashchat [add | del | reload]");
        
        @Override
        public boolean onCommand(CommandSender s, Command c, String a, String[] as)
        {
            if(!(s instanceof Player))
                return f;
            
            Player p = (Player) s;
            
            if(!p.hasPermission(admin_permission))
            {
                p.sendMessage(permission_denied);
                return f;
            }
            
            else if(as.length < 1)
            {
                p.sendMessage(invalid_syntax);
                return f;
            };
            
            String cmd = as[0].toLowerCase();
            
            
            
            return t;
        };
    };
    
    
    private void ReloadPlugin()
    {
        plugin.reloadConfig();
        config = plugin.getConfig();
        
        events.black_listed_commands = config.getStringList("command-properties.blacklist");
        for(String str : events.black_listed_commands)
        {
            events.low_black_listed_commands.add(str.toLowerCase());
        };
        
        events.black_listed_words = config.getStringList("message-properties.blacklist");//Future update make it so it only loads this if enabled.
        for(String str : events.black_listed_words)
        {
            events.low_black_listed_words.add(str.toLowerCase());
        };
        
        commands.admin_permission = config.getString("admin-permission");
        
        events.command_blocker_enabled = config.getBoolean("command-properties.enabled");
        events.message_blocker_enabled = config.getBoolean("message-properties.enabled");
        
        events.command_detect_message = moon.transstr(config.getString("command-properties.deny-message"));
        events.word_detect_message = moon.transstr(config.getString("message-properties.deny-message"));
        
        events.deep_search_enabled = config.getBoolean("message-properties.deep-search");
    };
    
    
    class EventsHandler implements Listener
    {
        List<String> low_black_listed_commands = new ArrayList<>();        
        List<String> black_listed_commands = new ArrayList<>();
        
        boolean command_blocker_enabled, deep_search_enabled;
        
        String command_detect_message;
        
        @EventHandler
        public void onCommand(PlayerCommandPreprocessEvent e)
        {
            Player p = e.getPlayer();
            
            if((!command_blocker_enabled) || (p.hasPermission(commands.admin_permission)))
                return;
            
            String cmd = e.getMessage().split(" ")[0].toLowerCase();
            
            if(low_black_listed_commands.contains(cmd))
            {
                p.sendMessage(command_detect_message);
                p.sendTitle("", command_detect_message);
                e.setCancelled(true);
            };
        };
        
        
        List<String> low_black_listed_words = new ArrayList<>();        
        List<String> black_listed_words = new ArrayList<>();
        
        boolean message_blocker_enabled;
        
        String word_detect_message;
        
        @EventHandler
        public void onMessage(AsyncPlayerChatEvent e)
        {
            Player p = e.getPlayer();            
            
            if((!message_blocker_enabled) || (p.hasPermission(commands.admin_permission))) 
                return;
            
            if(!deep_search_enabled)
            {
                for(String str : e.getMessage().toLowerCase().split(" "))
                {
                    if(low_black_listed_words.contains(str))
                    {
                        p.sendMessage(word_detect_message);
                        p.sendTitle("", word_detect_message);                        
                        e.setCancelled(true);
                    };
                };
            }
            
            else 
            {
                for(String str : e.getMessage().toLowerCase().split(" "))
                {
                    for(String sstr : low_black_listed_words)
                    {
                        if(str.contains(sstr))
                        {
                            p.sendMessage(word_detect_message);
                            p.sendTitle("", word_detect_message);  
                            e.setCancelled(true);
                        };
                    };
                };
            };
        };
    };
    
    
    @Override
    public void onDisable()
    {
        moon.print("The plugin has been disabled.");
    };
    
    
    class Moony
    {
        public void print(String str)
        {
            System.out.println("(Dash Strict Chat): " + str);
        };
        
        public String transstr(String str)
        {
            return ChatColor.translateAlternateColorCodes('&', str);
        };
    };    
};
