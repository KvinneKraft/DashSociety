
// Author: Dashie
// Version: 1.0

package com.dashnarok;


import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import org.bukkit.Bukkit;
import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.plugin.java.JavaPlugin;

class DashCore
{
    public String transText(String msg)
    {
        return ChatColor.translateAlternateColorCodes('&', msg);
    };
    
    public void Sys(String msg)
    {
        System.out.println("[Dashnarok]: " + transText(msg));
    };
};

public class DashJoin extends JavaPlugin
{
    DashCore xxx = new DashCore();
    
    public static FileConfiguration config;    
    public static JavaPlugin plugin;
    
    @Override
    public void onEnable()
    {
        xxx.Sys("Enabling ....");
        
        config = this.getConfig();
        plugin = (JavaPlugin)this;        
        
        if(!config.getBoolean("enabled"))
        {
            xxx.Sys("The plugin is disabled in the config.");
            plugin.getPluginLoader().disablePlugin(plugin);
        };
        
        this.getServer().getPluginManager().registerEvents(new Events(), this);
        
        xxx.Sys("Plugin is now running!");
    };
    
    @Override
    public void onDisable()
    {
        xxx.Sys("The plugin has now been disabled!");
    };
};

class KvinneKraft 
{
    // Fancy Effects Class
};

//
// To-Do:
//
// - Test Plugin
// - Use Separate Files
// - Add in Fireworks
// - Add in Potion Effects
// - Add in Beginner Kits
// - Add in Beginner Money
//
// I have written this code on such a crappy laptop...
// I will have to wait until I get back home from work ;D
//

class Events implements Listener
{
    FileConfiguration config = DashJoin.config;    
    DashCore xxx = new DashCore();
    
    List<String> messages = new ArrayList<>();

    boolean silentjoin = config.getBoolean("properties.silent-join.enabled");
    String silentjoinp = config.getString("properties.silent-join.permission");
    
    int ON_FIRST_JOIN = 0, ON_JOIN = 1, ON_SILENT_JOIN = 3;
    @EventHandler
    public void onJoin(PlayerJoinEvent e)
    {
        LoadConfig();
        
        Player p = e.getPlayer();
        String m = "none";
        
        if(p.hasPlayedBefore())
        {
            m = messages.get(ON_FIRST_JOIN);
        }
        
        else
        if(p.hasPermission(silentjoinp))
        {
            m = null;
            
            Bukkit.getScheduler().runTaskAsynchronously(DashJoin.plugin, 
            () -> 
                {
                    for(Player s : Bukkit.getOnlinePlayers())
                    {
                        if(s.hasPermission(silentjoinp))
                        {
                            s.sendMessage(messages.get(ON_SILENT_JOIN).replace("{player}", p.getName()));
                        };
                    };
                }
            );
        }
        
        else
        {
            m = messages.get(ON_JOIN);
        };
        
        if(m != null)
            m = m.replace("{player}", p.getName());
        
        e.setJoinMessage(m);
    };
    
    int ON_QUIT = 2;
    @EventHandler
    public void onQuit(PlayerQuitEvent e)
    {
        LoadConfig();
        
        e.setQuitMessage(messages.get(ON_QUIT).replace("{player}", e.getPlayer().getName()));
    };
    
    private void LoadConfig()
    {
        if(messages.size() < 3)
            return;
        
        messages = config.getStringList("properties.messages");
        
        for(int id = 0; id < messages.size(); id += 1)
            messages.set(id, xxx.transText(messages.get(id)));
    };
    
    private void UpdateConfig()
    {
        if(messages.size() < 3)
            return;
        
        config.set("properties.messages", messages);
        DashJoin.plugin.saveConfig();
    };    
};