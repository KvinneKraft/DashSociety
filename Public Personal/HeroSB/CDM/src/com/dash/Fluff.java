
// Author: Dashie
// Version: 1.0

package com.dash;

import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.PlayerDeathEvent;
import org.bukkit.plugin.java.JavaPlugin;

public class Fluff extends JavaPlugin implements Listener
{
    private FileConfiguration config = (FileConfiguration) null;
    
    @Override public void onEnable()
    {
        print("Loading plugin ....");
        
        saveDefaultConfig();
        
        config = (FileConfiguration) getConfig();
        death_message = color(config.getString("properties.death-message"));
        
        getServer().getPluginManager().registerEvents(this, this);
        
        print("Plugin has been loaded!");
    };
    
    private String death_message;
    
    @EventHandler public void onDeath(PlayerDeathEvent e)
    {
        final Player p = (Player) e.getEntity();
        
        if(!(e.getEntity().getKiller() instanceof Player))
        {
            e.setDeathMessage(color("&cThe player &b" + p.getName() + " &cdied!"));
        }
        
        else
        {
            final Player k = (Player) e.getEntity().getKiller();
            String weapon = "stick";
            
            if(k.getInventory().getItemInMainHand() == null || k.getInventory().getItemInMainHand().equals(Material.AIR))
            {
                weapon = "bare hand";
            }
            
            else
            {
                weapon = k.getInventory().getItemInMainHand().getType().toString().toLowerCase().replace("_", " ");
            };
            
            e.setDeathMessage(death_message.replace("%p%", p.getName()).replace("%b%", k.getName()).replace("%w%", weapon));
        };
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
    
    private void print(final String str)
    {
        System.out.println("(CDM): " + str);
    };
    
    private String color(final String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
};