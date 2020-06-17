
// Author: Dashie
// Version: 1.0

package dash.recoded;

import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.Listener;
import org.bukkit.plugin.java.JavaPlugin;

public class BetterKits extends JavaPlugin
{
    FileConfiguration config;
    JavaPlugin plugin;
    
    void LoadConfiguration()
    {
        saveDefaultConfig();
        
        if (plugin != this)
        {
            plugin = (JavaPlugin) this;
        };
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        
    };
    
    protected static class Caching
    {
        
    };
    
    protected static class Properties
    {
        
    };
    
    @Override public void onEnable()
    {
        print("I am crawling out of my nest .....");
        
        LoadConfiguration();
        
        getCommand("betterkits").setExecutor(new Commands());
        getServer().getPluginManager().registerEvents(new Events(), plugin);
        
        print
        (
            (
                "\n" +                    
                "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n" +
                "Author: Dashie\n" +
                "Version: 1.0\n" +
                "Contact: KvinneKraft@protonmail.com\n" +
                "Sources: https://github.com/KvinneKraft\n" +
                "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-"
            )
        );
        
        print("I am now standing right up!");
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
                print("Only players may execute this command.");
                return false;
            };
            
            final Player p = (Player) s;
            
            return true;
        };
    };
    
    @Override public void onDisable()
    {
        print("I fell down and am dead now.");
    };
    
    String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
    
    void print(final String line)
    {
        System.out.println("(Better Kits): " + line);
    };
};