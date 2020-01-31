
package com.dashrays;

import java.util.List;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.block.BlockBreakEvent;
import org.bukkit.event.player.PlayerJoinEvent;

public class EventsHandler implements Listener
{
    FileConfiguration config = Luna.getGlobalConfig();
    
    List<String> blocks = config.getStringList("properties.blocks");
    
    String notify_message = Luna.transStr(config.getString("properties.notify-message"));
    String notify_permiss = config.getString("properties.notify-permission");
    
    @EventHandler
    public void onBlockBreak(BlockBreakEvent e)
    {
        
    };
    
    boolean default_toggle = config.getBoolean("properties.default-toggle-enabled");
    //Default Toggle Message to notify player upon join
    @EventHandler
    public void onJoin(PlayerJoinEvent e)
    {
        
    };
};
