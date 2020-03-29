
package com.weeds;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.plugin.java.JavaPlugin;

public class Strains extends JavaPlugin
{
    private String color(String str) { return ChatColor.translateAlternateColorCodes('&', str); };
    private void print(String str) { System.out.println("(Strainables): " + str); };
    
    private FileConfiguration config;
    private JavaPlugin plugin;
    
    @Override public void onEnable()
    {
        print("Plugin is loading .....");
        
        config = (FileConfiguration) getConfig();
        plugin = (JavaPlugin) this;
        
        load();
        
        print("Plugin has been loaded!");
    };
    
    private String consume_permission, command_permission, strains_list;
    private int consume_cooldown;
    
    private final List<String> strain_lores = new ArrayList<>();
    private final List<Player> player_cache = new ArrayList<>();
    
    private final List<String> keywords = Arrays.asList
    (
        new String[] 
        {
            "birthday_cake_kush", "og_kush", "buddha_kush", "green_crack_kush", "gorilla_glue", "purple_haze", "silver_haze", "super_silver_haze", 
            "purple_diesel", "strawberry_kush", "banana_kush", "water_melon_kush", "cookie_kush", "bubble_gum", "cheese", "afghan_kush", "dash_kush",
            "potato_salad_kush",
        }
    );
    
    private void load()
    {
        command_permission = config.getString("properties.permissions.command-permission");
        consume_permission = config.getString("properties.permissions.consume-permission");
        
        consume_cooldown = config.getInt("properties.optionals.consume-cooldown") * 20;
        
        if(strain_lores.size() > 0) strain_lores.clear();
        if(player_cache.size() > 0) player_cache.clear();
        
        for ( String lore : config.getStringList("properties.modifications.strain-lores") )
        {
            strain_lores.add(lore);
        };
        
        strains_list = color(config.getString("properties.strains"));
    };
    
    @Override public void onDisable()
    {
        print("The plugin has been disabled!");
    };
};