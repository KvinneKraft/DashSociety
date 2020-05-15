
package com.configplugin;

import net.md_5.bungee.api.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.plugin.java.JavaPlugin;

public class ConfigPlugin extends JavaPlugin implements CommandExecutor
{
    private void print(String str) { System.out.println("(Config Plugin): " + str); };
    private String color(String str) { return ChatColor.translateAlternateColorCodes('&', str); };
    
    private void load()
    {
        this.reloadConfig();
        config = (FileConfiguration) this.getConfig();
        
        permission = config.getString("properties.permission");
        message = color(config.getString("properties.message"));
    };
    
    private FileConfiguration config;
    
    @Override public void onEnable()
    {
        print("Plugin is loading ....");
        
        saveDefaultConfig();
        
        getCommand("luis").setExecutor(this);
        
        load();
        
        print("Plugin has been loaded!");
    };
    
    private String permission, message;
    
    @Override public boolean onCommand(CommandSender s, Command c, String a, String[] as)
    {
        if (!(s instanceof Player))
        {
            print("You may not use this command!");
            return false;
        };
        
        final Player p = (Player) s;
        
        if (!p.hasPermission(permission))
        {
            p.sendMessage(color("&cYou are now allowed to use this!"));
            return false;
        };
        
        p.sendMessage(message);
        
        return true;
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
};