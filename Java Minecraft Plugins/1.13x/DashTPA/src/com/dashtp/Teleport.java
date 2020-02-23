

// Author : Dashie
// Version : 1.0


package com.dashtp;


import org.bukkit.ChatColor;
import org.bukkit.command.CommandExecutor;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.command.CommandSender;
import org.bukkit.command.Command;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;


public class Teleport extends JavaPlugin implements CommandExecutor
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    
    @Override
    public void onEnable()
    {
        print("enabling ....");
        
        saveDefaultConfig();
        
        config = (FileConfiguration)getConfig();
        plugin = (JavaPlugin)this;
        
        getCommand("dashtptoggle").setExecutor(plugin);        
        getCommand("dashtpa").setExecutor(plugin);
        getCommand("dashtp").setExecutor(plugin);
        
        print("-------------------------");
        print("Author: Dashie");
        print("Version: 1.0");
        print("-------------------------");
        print("Email: KvinneKraft@protonmail.com");
        print("Github: https://github.com/KvinneKraft");
        print("-------------------------");
        
        print("enabled!");
    };
    
    
    final boolean f = false, t = true;
    
    final String usage_correction = color("&cThis command was not recognized.");
    
    
    @Override
    public boolean onCommand(CommandSender s, Command c, String a, String[] as)
    {
       
        if(!(s instanceof Player))
            return f;
        
        try
        {
            Player p = (Player) s;
            a = c.toString().toLowerCase();
            
            if(a.equals("dashtpa"))
            {
                
            }
            
            else if(a.equals("dashtp"))
            {
                
            }
            
            else if(a.equals("dashtptoggle"))
            {
                
            }
            
            else
            {
                p.sendMessage(usage_correction);
            };
            
            return t;
        }
        
        catch (Exception e)
        {
            return f;
        }
    };
    
    
    private void print(String str)
    {
        System.out.println("(DashTPA): " + str);
    };
    
    private String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
};
