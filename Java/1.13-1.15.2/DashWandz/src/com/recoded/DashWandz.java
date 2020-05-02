
// Author: Dashie
// Version: 1.0

package com.recoded;

import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.inventory.ItemStack;
import org.bukkit.plugin.java.JavaPlugin;

public class DashWandz extends JavaPlugin
{
    public static final ItemStack lightning = new ItemStack(Material.STICK, 1);
    public static final ItemStack fireball = new ItemStack(Material.STICK, 1);
    public static final ItemStack firework = new ItemStack(Material.STICK, 1);
    public static final ItemStack wither = new ItemStack(Material.STICK, 1);
    
    @Override public void onEnable()
    {
        print("Plugin is being enabled ....");
        
        LoadConfiguration();
        
        getServer().getPluginManager().registerEvents(new EventsHandler(), plugin);
        
        print("Plugin has been enabled.");
    };
    
    private FileConfiguration config = (FileConfiguration) null;
    private final JavaPlugin plugin = (JavaPlugin) this;
    
    private void LoadConfiguration()
    {
        saveDefaultConfig();
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
    
    private void print(String line)
    {
        System.out.println("(Better RTP): " + line);
    };
    
    private String color(String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
};