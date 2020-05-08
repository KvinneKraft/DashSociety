
// Author: Dashie
// Version: 1.0

package com.philosophy;

import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.CreatureSpawnEvent;
import org.bukkit.event.entity.EntityDamageByEntityEvent;
import org.bukkit.event.entity.PlayerDeathEvent;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.event.player.PlayerRespawnEvent;

public class EventsHandler implements Listener
{
    @EventHandler public void onJoin(final PlayerJoinEvent e)
    {
        Authenticate.onPlayerJoin(e);
    };
    
    @EventHandler public void onQuit(final PlayerQuitEvent e)
    {
        Authenticate.onPlayerQuit(e);
    };
    
    @EventHandler public void onPlayerDeath(final PlayerDeathEvent e)
    {
        DashHeads.onDeath(e);
    };
    
    @EventHandler public void onPlayerRespawn(final PlayerRespawnEvent e)
    {
        Spawn.onPlayerRespawn(e);
    };
    
    @EventHandler public void onEntityDamageByEntity(final EntityDamageByEntityEvent e)
    {
        Organisms.onEntityAttackSkelly(e);
    };
    
    @EventHandler public void onCreatureSpawn(final CreatureSpawnEvent e)
    {
        Organisms.onEntitySpawn(e);
    };
};