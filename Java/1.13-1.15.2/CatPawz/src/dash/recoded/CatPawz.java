
// Author: Dashie
// Version: 1.0

package dash.recoded;

import java.util.ArrayList;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.Particle;
import org.bukkit.Sound;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.Listener;
import org.bukkit.inventory.ItemStack;
import org.bukkit.plugin.java.JavaPlugin;

public class CatPawz extends JavaPlugin
{
    FileConfiguration config;
    JavaPlugin plugin;
    
    protected class Verifier
    {
        Material isMaterial(final String material)
        {
            try
            {
                Material.getMaterial(material);
            }
            
            catch (final Exception e)
            {
                return null;
            }
        };
        
        Sound isSound(final String sound)
        {
            try
            {
                return Sound.valueOf(sound);
            }
            
            catch (final Exception e)
            {
                return null;
            }
        };
        
        Particle isParticle(final String particle)
        {
            try
            {
                return Particle.valueOf(particle);
            }
            
            catch (final Exception e)
            {
                return null;
            }
        };
        
        Integer isInteger(final String integer)
        {
            try
            {
                return Integer.parseInt(integer);
            }
            
            catch (final Exception e)
            {
                return -1;
            }
        };
    };
    
    protected static class CatWand 
    {
        protected static class Properties
        {
            final static List<Particle> particles = new ArrayList<>();          
            final static ItemStack wand_material = new ItemStack(Material.BLAZE_ROD, 1);            
            
            static int particle_density = 0;            
            
            static Sound meow_shoot_sound = null;            
            static Sound meow_sound = null;  
            
            static String permission = "";                        
        };
    };
    
    void ReloadPlugin()
    {
        if (plugin != this)
        {
            plugin = (JavaPlugin) this;
        };
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        final Verifier verify = new Verifier();
        
        
    };
    
    @Override public void onEnable()
    {
        print("I am trying to swim ....");
        
        ReloadPlugin();
        
        getServer().getPluginManager().registerEvents(new Events(), plugin);            
        getCommand("catpawz").setExecutor(new Commands());                  
        
        print
        (
            (
                "\n-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-" +
                "Author: Dashie" +
                "Version: 1.0" +
                "Github: https://github.com/KvinneKraft" +
                "Email: KvinneKraft@protonmail.com" +
                "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-"
            )
        );
        
        print("Okay, I am swimming now.");
    };
    
    protected class Events implements Listener
    {
        
    };
    
    protected class Commands implements CommandExecutor
    {
        @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
        {
            if (!(s instanceof Player))
            {
                print("You may only do this as a player!");
                return false;
            };
            
            final Player p = (Player) s;
            
            return true;
        };
    };
    
    @Override public void onDisable()
    {
        print("I am now Dead!");
    };
    
    void print(final String line)
    {
        System.out.println("(Cat Pawz): " + line);
    };
    
    String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
};