
package com.crossbow;

import java.util.ArrayList;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public class CrossBow extends JavaPlugin
{
    private FileConfiguration config;
    private JavaPlugin plugin;
    
    private String color(String str) { return ChatColor.translateAlternateColorCodes('&', str); };
    private void print(String str) { System.out.println("(Dash Bowz): " + str); };
    
    @Override public void onEnable()
    {
        
        
        saveDefaultConfig();
        
        config = (FileConfiguration) getConfig();
        plugin = (JavaPlugin) this;
        
        
    };
    
    private boolean lightning, explosion, explosion_blockbreak, fireworks, poison, wither, hunger, rat;
    private double lightning_chance, explosion_chance, fireworks_chance, poison_chance, wither_chance, hunger_chance, rat_chance;        
    
    private String use_permission, admin_permission, display_name;

    private final List<String> bow_lore = new ArrayList<>();    
    
    private void load()
    {
        plugin.reloadConfig();
        config = plugin.getConfig();
        
        use_permission = config.getString("properties.permissions.use-permission");
        admin_permission = config.getString("properties.permissions.admin-permission");
        
        lightning = config.getBoolean("properties.effects.lightning");
        explosion = config.getBoolean("properties.effects.explosion");
        fireworks = config.getBoolean("properties.effects.fireworks");
        
        lightning_chance = config.getDouble("properties.effects.lightning-chance");
        explosion_chance = config.getDouble("properties.effects.explosion-chance");
        fireworks_chance = config.getDouble("properties.effects.fireworks-chance");
        
        poison = config.getBoolean("properties.effects.poison");
        wither = config.getBoolean("properties.effects.wither");
        hunger = config.getBoolean("properties.effects.hunger");
        
        poison_chance = config.getDouble("properties.effects.poison-chance");
        wither_chance = config.getDouble("properties.effects.wither-chance");
        hunger_chance = config.getDouble("properties.effects.hunger-chance");
        
        rat_chance = config.getDouble("properties.effects.rat-chance");
        rat = config.getBoolean("properties.effects.rat");        
        
        if(bow_lore.size() > 1)
        {
            bow_lore.clear();
        };
        
        for (String str : config.getStringList("properties.bow-meta.display-lore"))
        {
            bow_lore.add(color(str));
        };
        
        display_name = color(config.getString("properties.bow-meta.display-name"));
    };
    
    @Override public void onDisable()
    {
        
    };
};
