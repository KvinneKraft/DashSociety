// Author: Dashie
// Version: 1.0

package com.dashsession;


import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Random;
import org.bukkit.ChatColor;
import org.bukkit.Color;
import org.bukkit.FireworkEffect;
import org.bukkit.Location;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.Firework;
import org.bukkit.inventory.meta.FireworkMeta;
import org.bukkit.plugin.java.JavaPlugin;


public class Login extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    @Override
    public void onEnable()
    {
        Lunaris.print("Plugin is loading ....");
        
        plugin = this;
        plugin.saveDefaultConfig();
        
        Lunaris.print("Author: Dashie");
        Lunaris.print("Version: 1.0");
        Lunaris.print("Email: KvinneKraft@protonmail.com");
        Lunaris.print("Github: https://github.com/KvinneKraft");
        
        Lunaris.print("Plugin has been loaded!");
    };
    
    
    @Override
    public void onDisable()
    {
        Lunaris.print("Plugin has been disabled!");
    };    
};



class Lunaris
{
    public static void print(String str)
    {
        System.out.println("(Dash Bandaids): " + str);
    };
    
    
    public static String colors(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    
    static List<FireworkEffect.Type> firework_effect_types = new ArrayList<>(
        Arrays.asList(
            new FireworkEffect.Type[]
            {
                FireworkEffect.Type.BALL, 
                FireworkEffect.Type.BALL_LARGE,
                FireworkEffect.Type.BURST,
                FireworkEffect.Type.CREEPER,
                FireworkEffect.Type.STAR,
            }
        )
    );
    
    
    public static void dashworks(Integer[] rgb, Location location)
    {
        Color color = Color.fromRGB(rgb[0], rgb[1], rgb[2]);
        
        Firework firework = (Firework) location.getWorld().spawnEntity(location, EntityType.FIREWORK);
        FireworkMeta firework_meta = firework.getFireworkMeta();
        
        FireworkEffect.Type firework_effect_type = firework_effect_types.get(new Random().nextInt(firework_effect_types.size()));
        firework_meta.addEffect(FireworkEffect.builder().withColor(color).withFlicker().withTrail().with(firework_effect_type).flicker(true).build());
        
        firework.setFireworkMeta(firework_meta);
        firework.detonate();
    };
};
