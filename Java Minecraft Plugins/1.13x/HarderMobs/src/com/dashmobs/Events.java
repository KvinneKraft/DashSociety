

// Author: Dashie
// Version: 1.0


package com.dashmobs;


import org.bukkit.event.entity.CreatureSpawnEvent;
import org.bukkit.event.EventHandler;
import org.bukkit.entity.EntityType;
import org.bukkit.event.Listener;
import java.util.ArrayList;
import java.util.List;
import org.bukkit.entity.LivingEntity;


public class Events implements Listener
{
    List<EntityType> harder_mob_types = new ArrayList<>();
    
    
    @EventHandler
    public void onChunkGenerationMobSpawn(CreatureSpawnEvent e)
    {
        LivingEntity moby = e.getEntity();
        
        if(!harder_mob_types.contains(moby.getType()))
        {
            return;
        };
    };
};
