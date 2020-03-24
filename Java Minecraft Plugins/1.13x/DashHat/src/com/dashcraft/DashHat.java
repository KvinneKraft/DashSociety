

// Author: Dashie
// Version: 1.0


package com.dashcraft;


import java.util.ArrayList;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffectType;


public class DashHat extends JavaPlugin implements CommandExecutor
{
    public static void print(String str)
    {
        System.out.println("(Dash Hat): " + str);
    };
    
    public static String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    @Override public void onEnable()
    {
        print("Initializing plugin configurations and what not ....");
        
        saveDefaultConfig();
        
        config = (FileConfiguration) getConfig();
        plugin = (JavaPlugin) this;
        
        getCommand("dashhat").setExecutor(plugin);
        
        print("I am done, I am now running!");
    };
    
    @Override public void onDisable()
    {
        print("Plugin has now been disabled.");
    };
    
    private List<Player> players = new ArrayList<>();
    
    private String admin_permission, hat_permission;
    private int hat_cooldown;
    
    private void load()
    {
        plugin.reloadConfig();
        config = plugin.getConfig();
        
        admin_permission = config.getString("properties.admin-permission");        
        hat_permission = config.getString("properties.hat-permission");
        hat_cooldown = config.getInt("properties.hat-cooldown");
    };
    
    @Override public boolean onCommand(CommandSender s, Command c, String a, String[] as)
    {
        if(!(s instanceof Player))
            return false;
        
        Player p = (Player) s;
        
        if (as.length >= 1 && as[0].equalsIgnoreCase("reload") && p.hasPermission(admin_permission))
        {
            
        }
            
        else if ((p.hasPermission(hat_permission) || p.hasPermission(admin_permission)))
        {
            
        };
        
        return true;
    };
};
