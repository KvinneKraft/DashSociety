
// Author: Dashie
// Version: 1.0

package com.dashmoney.events;


import com.dashmoney.Moon;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.event.Listener;
import org.bukkit.event.block.BlockPlaceEvent;
import org.bukkit.event.entity.EntityDeathEvent;


public class EventHandler implements Listener
{
    FileConfiguration config = Moon.getGlobalConfig();
    
    @org.bukkit.event.EventHandler
    public void onEntityDeath(EntityDeathEvent e)
    {
        
    };
    
    @org.bukkit.event.EventHandler
    public void onBlockBreak(BlockPlaceEvent e)
    {
        
    };
    
    @org.bukkit.event.EventHandler
    public void onBlockPlace(BlockPlaceEvent e)
    {
        
    };
};
