
package com.dashcure;


// Author: Dashie
// Version: 1.0


import java.util.HashMap;
import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.entity.Player;
import org.bukkit.event.Listener;
import org.bukkit.event.EventHandler;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.event.player.AsyncPlayerChatEvent;
import org.bukkit.configuration.file.FileConfiguration;


public class DashColours extends JavaPlugin
{
    public FileConfiguration config = getConfig();
    public JavaPlugin plugin = this;
    
    @Override
    public void onEnable()
    {
        Muun.print("Loading Dash Colors 1.0 ....");
        
        saveDefaultConfig();
        
        
        
        Muun.print("Dash Colors 1.0 has been loaded!");
    };
    
    HashMap<String, String> player_db = new HashMap<String, String>();
    
    class EventsHandler implements Listener
    {
        @EventHandler
        public void onPlayerChat(AsyncPlayerChatEvent e)
        {
            String player_id = e.getPlayer().getUniqueId().toString();
            
            if(!player_db.containsKey(player_id))
                return;
            
            String message = player_db.get(player_id) + e.getMessage();
            
            e.setMessage(message);
        };
    };
    
    private void LoadColors(String player_id)
    {
        Object object = config.get("color-table" + player_id);
        player_db = (HashMap)object;
    };
    
    private void SaveColors(String player_id)
    { 
        config.set("color-table" + player_id, player_db); 
        plugin.saveConfig();
        
        config = getConfig();
    };    
    // Continue tomorrow.
    class CommandsHandler implements CommandExecutor
    {
        boolean t = true, f = false;
        
        @Override
        public boolean onCommand(CommandSender s, Command c, String a, String[] as)
        {
            if(!(s instanceof Player))
                return f;
            
            Player p = (Player) s;
            
            if(!p.hasPermission(<Permission>))
            {
                // Insufficient Permissions<
                return f;
            }
            
            else if(as.length < 1)
            {
                // Correct Syntax<
                return f;
            };
            
            return t;
        };
    };
    
    @Override
    public void onDisable()
    {
        Muun.print("Saving Data ....");
        
        for(Player player : getServer().getOnlinePlayers())
        {
            SaveColors(player.getName());
        };
        
        Muun.print("Dash Colors 1.0 has been disabled!");
    };
};


class Muun
{
    public static void print(String str)
    {
        System.out.println("(Dash Colours): " + str);
    };
    
    public static String transStr(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
};
