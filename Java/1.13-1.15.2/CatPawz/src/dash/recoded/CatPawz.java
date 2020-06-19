
// Author: Dashie
// Version: 1.0

package dash.recoded;

import org.bukkit.ChatColor;
import org.bukkit.Particle;
import org.bukkit.Sound;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.Listener;
import org.bukkit.plugin.java.JavaPlugin;

public class CatPawz extends JavaPlugin
{
    FileConfiguration config;
    JavaPlugin plugin;
    
    void ReloadPlugin()
    {
        if (plugin != this)
        {
            plugin = (JavaPlugin) this;
        };
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
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