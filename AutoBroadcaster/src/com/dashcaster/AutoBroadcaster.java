
// Coded whilst Drunk!

package com.dashcaster;

import com.dashrays.Luna;
import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import net.md_5.bungee.api.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public class AutoBroadcaster extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    
    @Override
    public void onEnable()
    {
         Luna.print("Dashies Auto Broadcaster is loading ....");
         
         saveDefaultConfig();
         
         config = getConfig();
         plugin = this;
         
         Integer interval = config.getInt("interval") * 20;         
         
         getServer().getScheduler().runTaskTimerAsynchronously(plugin, 
            new Runnable()
            {
                List<String> messages = new ArrayList<String>();        
                Random rand = new Random();
                
                @Override
                public void run()
                {
                    if(messages.size() < 1)
                        for(String message : config.getStringList("messages"))
                            messages.add(ChatColor.translateAlternateColorCodes('&', message));
                    
                    if(getServer().getOnlinePlayers().size() < 1)
                        return;
                    
                    String msg = messages.get(rand.nextInt(messages.size()));
                   
                    getServer().broadcastMessage(msg);
                };
            }, 0, interval
         );
         
         Luna.print("Dashies Auto Broadcaster is now running.");
    };
    
    @Override
    public void onDisable()
    {
        Luna.print("Dashies Auto Broadcaster has been disabled!");
    };
};
