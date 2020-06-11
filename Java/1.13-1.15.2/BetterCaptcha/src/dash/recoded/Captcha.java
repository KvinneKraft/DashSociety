
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

public class Captcha extends JavaPlugin
{
    FileConfiguration config = null;
    JavaPlugin plugin = null;
    
    void LoadConfiguration()
    {
        saveDefaultConfig();
        
        if (plugin == null)
            plugin = (JavaPlugin) this;
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        
    };
    
    @Override public void onEnable()
    {
        print("Trying to catch my breath here, hold on ....");
        
        print
        (
            (
                "\n-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n" +
                "Author: Dashie A.K.A. KvinneKraft\n" +
                "Version: 2.0\n" +
                "Email: KvinneKraft@protonmail.com\n" +
                "Github: https://github.com/KvinneKraft\n" +
                "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-"
            )
        );
        
        print("I am now breathing!");
    };
    
    class Commands implements CommandExecutor
    {
        @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
        {
            if (!(s instanceof Player))
            {
                print("I may only be commanded by a player!");
                return false;
            };
            
            /*Abracadabra here*/
            
            return true;
        };
    };    
    
    class Events implements Listener
    {
        /*More Abracadabra here*/
    };
    
    @Override public void onDisable()
    {
        print("I think that I died?");
    };
    
    String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
    
    void print(final String line)
    {
        System.out.println("(Better Captcha): " + line);
    };
};