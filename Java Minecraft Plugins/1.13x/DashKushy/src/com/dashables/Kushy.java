
// Author: Dashie
// Version: 1.0

package com.dashables;


import java.util.ArrayList;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.command.CommandExecutor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.plugin.java.JavaPlugin;


public class Kushy extends JavaPlugin implements Listener, CommandExecutor
{   
    private String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };    
    
    private void print(String str)
    {
        System.out.println("(Dash Kush): " + str);
    };
    
    private FileConfiguration config = (FileConfiguration) null;
    private JavaPlugin plugin = (JavaPlugin) null;
    
    @Override public void onEnable()
    {
        print("Plugin is being enabled.");
        
        config = (FileConfiguration) getConfig();
        plugin = (JavaPlugin) this;
        
        LoadConfiguration();
        
        getServer().getPluginManager().registerEvents(this, this);
        getCommand("dashkush").setExecutor(this);
        
        print("Plugin has been enabled.");
    };
    
    private String admin_permission, consume_permission;
    
    private void LoadConfiguration()
    {
        plugin.reloadConfig();
        config = plugin.getConfig();
        
        consume_permission = config.getString("properties.permissions.consume-permission");
        admin_permission = config.getString("properties.permissions.admin-permission");
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
    
    private final List<ItemStack> dashables = new ArrayList<>();
    
    @EventHandler private void PlayerEatEvent(PlayerInteractEvent e)
    {
        
    };
};
