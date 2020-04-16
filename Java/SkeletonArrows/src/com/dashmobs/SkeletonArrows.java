
// Author: Dashie
// Version: 1.0

package com.dashmobs;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Random;
import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.ProjectileHitEvent;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;

public class SkeletonArrows extends JavaPlugin implements Listener
{
    @Override public void onEnable() 
    { 
        print("Plugin is loading ....");
        
        LoadConfiguration();
        
        getServer().getPluginManager().registerEvents(this, plugin);
        
        print("---------------------");
        print("Email: KvinneKraft@protonmail.com");
        print("---------------------");
        print("Website: https://pugpawz.com");
        print("Github: https://github.com/KvinneKraft");
        print("---------------------");
        
        print("Plugin has been enabled!");
    };
    
    private FileConfiguration config = (FileConfiguration) null;
    private final JavaPlugin plugin = (JavaPlugin) this;
    
    private boolean isInt(String str)
    {
        try
        {
            Integer.valueOf(str);
            return true;
        }
        
        catch (Exception e)
        {
            return false;
        }
    };
    
    private void LoadConfiguration()
    {
        saveDefaultConfig();
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        if(mobs.size() < 1)
        {
            mobs.addAll
            (
                Arrays.asList
                (
                    new EntityType[] 
                    {
                        EntityType.SKELETON,
                        EntityType.STRAY,
                    }
                )
            );
        };
        if(effects.size() > 0)
        {
            effects.clear();
        };
        
        for (final String str : config.getStringList("properties.possibilities"))
        {
            final String[] effect = str.toUpperCase().split(" ");
            
            if(effect.length < 3)
            {
                print("The format in the configuration file is invalid. Skipping ....");
                continue;
            };
            
            final PotionEffectType type = PotionEffectType.getByName(effect[0]);
            
            if(type == null)
            {
                print("The specified effects " + effect[0] + " is invalid. Skipping ....");
                continue;
            };
            
            if(!isInt(effect[1]) || !isInt(effect[2]))
            {
                print("The format in the configuration file is invalid. Skipping ....");
                continue;
            };
            
            final int amplifier = Integer.valueOf(effect[1]);
            final int duration = Integer.valueOf(effect[2]) * 20;
            
            effects.add(new PotionEffect(type, duration, amplifier));
        };
        
        if(effects.size() < 1)
        {
            print("No effects were found, this means that no special effects will be applied upon arrow impact!");
        };
        
        probability = (int) config.getInt("properties.probability");
    };
    
    private final List<PotionEffect> effects = new ArrayList<>();
    private final List<EntityType> mobs = new ArrayList<>();    
    
    private int probability;
    
    @EventHandler public void EntityHitByEntity(ProjectileHitEvent e)
    {
        if(e.getEntity() == null || (!(e.getEntity() instanceof Player)) || e.getEntity().getShooter() == null || !mobs.contains(e.getEntity().getShooter()))
        {
            return;
        };
        
        if(new Random().nextInt(101) <= probability)
        {
            final Player p = (Player) e.getEntity().getShooter();
            p.addPotionEffect(effects.get(new Random().nextInt(effects.size())));
        };
    };
    
    @Override public void onDisable() { print("Plugin has been disabled!"); };
    
    private String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    private void print(String str)
    {
        System.out.println("(Skeleton Arrows): " + str);
    };
};