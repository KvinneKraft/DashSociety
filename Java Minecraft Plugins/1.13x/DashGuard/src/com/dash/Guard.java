
// Author: Dashie
// Version: 1.0

package com.dash;

import java.util.ArrayList;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.block.BlockBreakEvent;
import org.bukkit.event.block.BlockPlaceEvent;
import org.bukkit.plugin.java.JavaPlugin;

public class Guard extends JavaPlugin implements Listener, CommandExecutor
{
    private FileConfiguration config = (FileConfiguration) null;
    private JavaPlugin plugin = (JavaPlugin) this;
    
    @Override public void onEnable()
    {
        print("Loading Dash Guard 1.0 ....");
        
        saveDefaultConfig();
        loadConfig();
        
        getServer().getPluginManager().registerEvents(this, plugin);
        getCommand("dashguard").setExecutor(this);
        
        print("Dash Guard 1.0 has been loaded!");
    };
    
    @Override public void onDisable()
    {
        print("Dash Guard 1.0 has been disabled ;c");
    };
    
    private void notifier(final Player p, final int type, final Material mate)
    {
        String tete = null;

        switch(type)
        {
            case 1: tete = "breaking"; break;            
            case 2: tete = "placing"; break;
        };

        final String message = color("&c>>> &b" + p.getName() + " &ctried " + tete + " &b" + mate + " &c!");        
        
        getServer().getScheduler().runTaskAsynchronously
        (
            plugin,

            new Runnable()
            {
                @Override public void run()
                {   
                    for ( final Player p : getServer().getOnlinePlayers() )
                    {
                        if(p.isOnline() && p.hasPermission(notify_permission))
                        {
                            getServer().getScheduler().runTask
                            (
                                plugin,

                                new Runnable()
                                {
                                    @Override public void run()
                                    {   
                                        p.sendMessage(message);
                                    };
                                }
                            );
                        };
                    };
                };
            }
        );        
    };
    
    @EventHandler public void onBlockBreak(BlockBreakEvent e)
    {
        if(e.getPlayer() == null)
        {
            return;
        };
        
        final Player p = (Player) e.getPlayer();
        
        if(p.hasPermission(bypass_break_permission))
        {
            return;
        };
        
        if(notify_staff)
        {
            notifier(p, 1, e.getBlock().getType());
        };
        
        p.sendMessage(color("&d(&d&lDash &5&lGuard&d): &cYou may not place this block!"));
        e.setCancelled(true);        
    };
    
    @EventHandler public void onBlockPlace(BlockPlaceEvent e)
    {
        if(e.getPlayer() == null)
        {
            return;
        };
        
        final Player p = (Player) e.getPlayer();
        
        if(p.hasPermission(bypass_place_permission))
        {
            return;
        };
        
        if(notify_staff)
        {
            notifier(p, 2, e.getBlock().getType());
        };
        
        p.sendMessage(color("&d(&d&lDash &5&lGuard&d): &cYou may not break this block!"));
        e.setCancelled(true);
    };
    
    private String bypass_place_permission, bypass_break_permission, admin_command_permission, notify_permission;
    private boolean notify_staff;
    
    private final List<Material> break_blacklist = new ArrayList<>();
    private final List<Material> place_blacklist = new ArrayList<>();
    
    private void loadConfig()
    {
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        bypass_place_permission = config.getString("properties.block-placement.bypass-permission");
        bypass_break_permission = config.getString("properties.block-breaking.bypass-permission");
        
        admin_command_permission = config.getString("properties.admin.command-permission");
        
        notify_permission = config.getString("properties.admin.notify-permission");        
        notify_staff = config.getBoolean("properties.admin.notify-staff");
        
        if(break_blacklist.size() > 0 || place_blacklist.size() > 0)
        {
            break_blacklist.clear();
            place_blacklist.clear();
        };
            
        for ( final String str : config.getStringList("properties.block-placement.blacklist") )
        {
            final Material mate = Material.getMaterial(str.toUpperCase().replace(" ", "_"));
            
            if (mate == null)
            {
                print("Invalid material found in the PLACE_BLACKLIST! [" + str + "]");
                continue;
            };
            
            place_blacklist.add(mate);
        };
        
        for ( final String str : config.getStringList("properties.block-breaking.blacklist") )
        {
            final Material mate = Material.getMaterial(str.toUpperCase().replace(" ", "_"));
            
            if (mate == null)
            {
                print("Invalid material found in the BREAK_BLACKLIST! [" + str + "]");
                continue;
            };
            
            break_blacklist.add(mate);            
        };
    };    
    
    private String color(final String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    private void print(final String str)
    {
        System.out.println("(Dash Guard): " + str);
    };
    
    @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
    {
        if(!(s instanceof Player))
        {
            return false;
        };
        
        Player p = (Player) s;
        
        if(!p.hasPermission(admin_command_permission))
        {
            p.sendMessage(color("&d(&d&lDash &5&lGuard&d): &cYou may not use this command!"));
            return false;
        };
        
        if(as.length >= 1)
        {
            // toggle-break | toggle-place | add-place | del-place | add-break | del-break
            
        }
        
        else
        {
            p.sendMessage(color("&d(&d&lDash &5&lGuard&d): &cCorrect usage of this command: &7/dashguard [toggle-break | toggle-place | add-place | del-place | add-break | del-break]"));
            return false;
        };
        
        return true;
    };
};
