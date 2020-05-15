
package com.eventplugin;

import org.bukkit.ChatColor;
import org.bukkit.Server;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.plugin.java.JavaPlugin;

public class EventPlugin extends JavaPlugin implements Listener
{
    public void print(String str)
    {
        System.out.println("(Event Plugin): " + str);
    };
    
    public String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    private final Server server = getServer();
    
    @Override public void onEnable()
    {
        print("The plugin is starting ....");
        
        server.getPluginManager().registerEvents(this, this);
        
        print("The plugin has been started and is now running!");
    };
    
    private final String first_join_message = color("&aThe player %p% &ahas joined the server for the first time!");
    private final String join_message = color("&aThe player &e%p% &ahas joined the server!");
    private final String quit_message = color("&cThe player &4%p% &chas left the server!");
    
    @EventHandler public void onPlayerJoin(PlayerJoinEvent e)
    {
        final Player p = (Player) e.getPlayer();
        
        if(p.hasPlayedBefore())
        {
            e.setJoinMessage(join_message.replace("%p%", p.getName()));
        }
        
        else
        {
            e.setJoinMessage(first_join_message.replace("%p%", p.getName()));
        };
    };
    
    @EventHandler public void onPlayerQuit(PlayerQuitEvent e)
    {
        final Player p = (Player) e.getPlayer();
        
        e.setQuitMessage(quit_message.replace("%p%", p.getName()));
    };
    
    @Override public void onDisable()
    {
        print("The plugin has been disabled!");
    };
};