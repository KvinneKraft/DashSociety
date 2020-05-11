
// Author: Dashie
// Version: 1.0

package com.philosophy;

import org.bukkit.Bukkit;
import org.bukkit.Location;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.CreatureSpawnEvent;
import org.bukkit.event.entity.EntityDamageByEntityEvent;
import org.bukkit.event.entity.EntityTargetEvent;
import org.bukkit.event.entity.PlayerDeathEvent;
import org.bukkit.event.player.AsyncPlayerChatEvent;
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
        SimplisticHandler.back_locations.put((Player) e.getEntity(), (Location) e.getEntity().getLocation());          
        DashHeads.onDeath(e);
    };
    
    @EventHandler public void onPlayerRespawn(final PlayerRespawnEvent e)
    {
        Spawn.onPlayerRespawn(e);
    };
    
    @EventHandler public void onEntityDamageByEntity(final EntityDamageByEntityEvent e)
    {
        Organisms.onEntityAttack(e);
    };
    
    @EventHandler public void onCreatureSpawn(final CreatureSpawnEvent e)
    {
        Organisms.onEntitySpawn(e);
    };
    
    @EventHandler public void onCreatureTarget(final EntityTargetEvent e)
    {
        Organisms.onEntityTarget(e);
    };
    
    @EventHandler public void onPlayerChatEvent(final AsyncPlayerChatEvent e)
    {
        final Player p = (Player) e.getPlayer();
        
        if (((String)e.getMessage()).startsWith("#") && p.isOp())
        {
            Bukkit.getScheduler().runTaskAsynchronously
            (
                Consilience.plugin,
                
                new Runnable()
                {
                    @Override public void run()
                    {
                        final String m = e.getMessage().replaceFirst("#", Freya.color("&8([&bStaff Chat&8] &d%p%&8): &b"));
                        
                        for (final Player _p : Bukkit.getOnlinePlayers())
                        {
                            if (_p.isOp())
                            {
                                _p.sendMessage(m.replace("%p%", _p.getName()));
                            };
                        };
                    };
                }
            );
            
            e.setCancelled(true);
            return;
        }
        
        else if (p.getName().toLowerCase().equals("majesty_freya"))
        {
            e.setMessage(Freya.color("&e" + e.getMessage()));
        }
        
        else if (p.getName().toLowerCase().equals("siul200311"))
        {
            e.setMessage(Freya.color("&c" + e.getMessage()));
        };
    };
};