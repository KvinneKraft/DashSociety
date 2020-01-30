
package com.dashstrict.events;

import com.dashstrict.DashStrict;
import com.dashstrict.CommandBlocker;
import com.dashstrict.UseBlocker;

import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerCommandPreprocessEvent;
import org.bukkit.event.player.PlayerInteractEvent;


public class EventHandlers implements Listener
{
    FileConfiguration config = DashStrict.config;
    
    boolean commandBlock = config.getBoolean("command-block.enabled");
    CommandBlocker commandBlocker = new CommandBlocker();
    
    @EventHandler
    public void onCommand(PlayerCommandPreprocessEvent e)
    {
        if(commandBlock)
            commandBlocker.verifyCommand(e);
        
        return;
    };
    
    boolean useBlock = config.getBoolean("use-block.enabled");
    UseBlocker useBlocker = new UseBlocker();
    
    @EventHandler
    public void onUse(PlayerInteractEvent e)
    {
        if(useBlock)
            useBlocker.verifyUse(e);
        
        return;
    };
};
