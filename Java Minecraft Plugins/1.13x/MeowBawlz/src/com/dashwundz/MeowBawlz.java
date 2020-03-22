

// Author: Dashie
// Version: 1.0


package com.dashwundz;


import java.util.ArrayList;
import java.util.List;
import net.md_5.bungee.api.ChatColor;
import org.bukkit.Bukkit;
import org.bukkit.Location;
import org.bukkit.Material;
import org.bukkit.Sound;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.entity.Snowball;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.ProjectileHitEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.plugin.java.JavaPlugin;


public class MeowBawlz extends JavaPlugin
{
    private String color(String str) { return ChatColor.translateAlternateColorCodes('&', str); };
    private void print(String str) { System.out.println("(Meow Bawlz): " + str); };
    
    private Commands command = new Commands();
    private Events events = new Events();
    
    private FileConfiguration config;
    private JavaPlugin plugin;
    
    @Override public void onEnable()
    {
        print("The plugin is loading ....");
        
        saveDefaultConfig();
        
        config = (FileConfiguration) getConfig();
        plugin = (JavaPlugin) this; 
        
        reload();
        
        getServer().getPluginManager().registerEvents(events, plugin);
        getCommand("meowballs").setExecutor(command);
        
        print("The plugin is now done loading and has now been enabled!");
    };
    
    private void reload()
    {
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        bypass_cooldown = config.getBoolean("properties.admin-bypass-cooldown");
        
        prohibited_message = color(config.getString("properties.messages.prohibited-message"));
        shoot_message = color(config.getString("properties.messages.shoot-message"));
        await_message = color(config.getString("properties.messages.await-message"));
        again_message = color(config.getString("properties.messages.again-message"));
        
        if(shoot_message.length() < 1 || await_message.length() < 1 || again_message.length() < 1)
        {
            print("You have not set a message for shoot-message, again-message or await-message! Check config.yml!");
        };
        
        shoot_permission = config.getString("properties.shoot-permission");
        admin_permission = config.getString("properties.admin-permission");
        
        if(shoot_permission.length() < 1 || admin_permission.length() < 1)
        {
            print("You have not set a permission for either shoot-permission or and admin-permission! Check config.yml!");
        };
        
        shoot_cooldown = config.getInt("properties.shoot-cooldown") * 20;
    
        wand_name = config.getString("properties.wand-meta.wand-name");
        
        Material wi = Material.getMaterial(config.getString("wand-item"));
        
        if(wi == null)
        {
            print("Ehm, the item " + wi.toString() + " is invalid, using BLAZE_ROD instead! Check config.yml!");
            wi = Material.BLAZE_ROD;
        };
        
        wand_item = new ItemStack(wi, 1);
        
        
    };
    
    private String shoot_permission, admin_permission, shoot_message, await_message, again_message, prohibited_message;
    
    private boolean bypass_cooldown;
    private int shoot_cooldown;    
    
    class Events implements Listener 
    {
        List<Player> players = new ArrayList<>();
        
        @EventHandler public void ProjectileHit(ProjectileHitEvent e)
        {
            if(!(e.getEntity() instanceof Snowball))
            {
                return;
            };
            
            Snowball snowball = (Snowball) e.getEntity();
            
            if(snowball.getCustomName() == null || !snowball.getCustomName().equals("meowball") || !(e.getEntity().getShooter() instanceof Player))
            {
                return;
            };
            
            Player p = (Player) e.getEntity().getShooter();
            
            if(!p.hasPermission(shoot_permission) && !p.hasPermission(admin_permission))
            {
                p.sendMessage(prohibited_message);
                return ;
            }
            
            else if(players.contains(p))
            {
                p.sendMessage(await_message);
                return ;
            }
            
            else if((!p.hasPermission(admin_permission)) || (p.hasPermission(admin_permission) && !bypass_cooldown))
            {
                players.add(p);
            };
            
            try
            {
                Location location = snowball.getLocation();
            
                location.getWorld().playSound(location, Sound.ENTITY_CAT_AMBIENT, 60, 60);                
            } 
            
            catch (Exception d)
            {
                // Nothing;
            }
            
            Bukkit.getServer().getScheduler().runTaskLater(plugin, 
                new Runnable() 
                { 
                    @Override public void run() 
                    {
                        if(players.contains(p))
                        {
                            players.remove(p);
                            
                            if(p.isOnline())
                            {
                                p.sendMessage(again_message);
                            };
                        };
                    }; 
                }, 
                
                shoot_cooldown
            );
        };
    };
    
    private List<String> wand_lore = new ArrayList<>();
    
    private ItemStack wand_item;    
    private String wand_name;
    
    class Commands implements CommandExecutor
    {
        @Override public boolean onCommand(CommandSender s, Command c, String a, String[] as)
        {
            return true;
        };
    };
    
    @Override public void onDisable()
    {
        print("The plugin has been disabled!");
    };
};
