
package com.dashie;

import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.Listener;
import org.bukkit.plugin.java.JavaPlugin;

//(To-Do):
//- Link the Moony Library up with this.
//- Add Command Support for the Config (base command=dashheads)
//- Make everything configurable

// <Description>
//
// A completely revamped version of Dash Money.
//
// With more functionality, less crap and a more
// user-friendly way of use.

public class DashHeads extends JavaPlugin
{   
    public FileConfiguration config = getConfig();
    public JavaPlugin plugin = this;
    
    @Override
    public void onEnable()
    {
        // Enable Message here
        saveDefaultConfig();
        
        this.getServer().getPluginManager().registerEvents(new EventsHandler(), plugin);
        getCommand("dashheads").setExecutor(new CommandsHandler());
        
    };
    
    class CommandsHandler implements CommandExecutor
    {
        // Efficient way to handle Messages here.
        
        @Override
        public boolean onCommand(CommandSender s, Command c, String a, String[] as)
        {
            if(!(s instanceof Player))
                return false;
            
            // Further processing here.
            
            return true;
        };
    };
    
    @Override
    public void onDisable()
    {
        // Disable Message here
    };
    
    class EventsHandler implements Listener
    {
        // Event Handlers here (EntityDeath, PlayerDeath ect)
    };
};

class Moony
{
    public static void Print(String str)
    {
        System.out.println("(Dash Heads): " + str);
    };
    
    public static String transStr(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
};