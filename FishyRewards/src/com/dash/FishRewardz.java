
// Author: Dashie
// Version: 1.0

package com.dash;

import org.bukkit.ChatColor;
import org.bukkit.Color;
import org.bukkit.FireworkEffect;
import org.bukkit.Location;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.Firework;
import org.bukkit.inventory.meta.FireworkMeta;
import org.bukkit.plugin.java.JavaPlugin;

public class FishRewardz extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    @Override
    public void onEnable()
    {
        Moony.Print("The plugin is initializing ....");
        
        saveDefaultConfig();
        
        config = getConfig();
        plugin = this;
        
        Moony.Print("The plugin is now enabled!");
    };
    
    // To-Do:
    // # Randomize Rewards
    // # Permissions
    // # Fancy Config
    // # Command Support
    // # Setup Events
    
    @Override
    public void onDisable()
    {
        Moony.Print("The plugin is now disabled.");
    };
};

class Moony
{
    public static String transStr(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    public void DetonateFirework(Location location, Color mcolor, Color fcolor, FireworkEffect.Type type)
    {
        Firework firework = (Firework) location.getWorld().spawnEntity(location, EntityType.FIREWORK);
        FireworkMeta firework_meta = firework.getFireworkMeta();
        
        firework_meta.addEffect(FireworkEffect.builder().withColor(mcolor).with(type).flicker(true).withFlicker().withTrail().withFade(fcolor).trail(true).build());
        
        firework.setFireworkMeta(firework_meta);
        firework.detonate();
    };    
    
    public static void Print(String str)
    {
        System.out.println("(Dash Fishyyy): " + str);
    };
};    