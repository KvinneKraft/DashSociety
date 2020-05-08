
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

public class DeathTweaks extends JavaPlugin implements Listener, CommandExecutor
{
    @Override public void onEnable()
    {
        print("Plugin is being enabled ....");
        
        LoadConfiguration();
        
        print("Plugin has been enabled.");
    };
    
    private FileConfiguration config = (FileConfiguration) null;
    private final JavaPlugin plugin = (JavaPlugin) this;
    
    private void LoadConfiguration()
    {
        saveDefaultConfig();
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        // Register command and events handler.
        
        // A chance at obtaining a potion effects
        // A chance at spawning a zombie upon death
    };
    
    private String admin_permission;
    
    @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
    {
        if (!(s instanceof Player))
        {
            print("You may only do this as a player!");
            return false;
        };
        
        // reload command
        
        return true;
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