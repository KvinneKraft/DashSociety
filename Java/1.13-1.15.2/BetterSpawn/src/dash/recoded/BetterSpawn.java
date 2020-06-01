
// Author: Dashie
// Version: 1.0

package dash.recoded;

import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public class BetterSpawn extends JavaPlugin
{
    FileConfiguration config = (FileConfiguration) null;
    JavaPlugin plugin = (JavaPlugin) null;//I care about the looks rather than the final thing.
    
    void LoadConfiguration()
    {
        if (plugin == null)
        {
            plugin = (JavaPlugin) this;
        };
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        
    };
    
    @Override public void onEnable()
    {
        print("I am starting ....");
        
        LoadConfiguration();
        
        print("I am now running!");
    };
    
    @Override public void onDisable()
    {
        print("I am now inactive!");
    };
    
    void print(final String line)
    {
        System.out.println("(Better Spawn)> " + line);
    };
    
    String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
};