
// Author: Dashie
// Version: 1.0

package com.philosophy;

import org.bukkit.Location;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.event.player.PlayerRespawnEvent;

public class Authenticate implements Listener
{
    @EventHandler public void onJoin(PlayerJoinEvent e)
    {
        final Player p = (Player) e.getPlayer();
        
        if (!p.hasPlayedBefore())
        {
            e.setJoinMessage(Freya.color("&eWelcome the newbie &a&l" + p.getName() + " &e!"));
            
            p.sendMessage(Freya.color("&7&oSssh, since it is your first time I will be giving you 5000$"));
            p.sendMessage(Freya.color("&aYou have received 5000$ !"));
            
            Consilience.econ.depositPlayer(p, 5000);
            
            p.teleport(new Location(e.getPlayer().getWorld(), Spawn.x, Spawn.y - 2, Spawn.z));
        }
        
        else
        {
            e.setJoinMessage(Freya.color("&a+= &a&l" + p.getName()));
        };
    };
    
    @EventHandler public void onQuit(PlayerQuitEvent e)
    {
        e.setQuitMessage(Freya.color("&c-= &c&l" + e.getPlayer().getName()));
    };
    
    @EventHandler public void onPlayerRespawn(PlayerRespawnEvent e)
    {
        e.setRespawnLocation(new Location(e.getPlayer().getWorld(), Spawn.x, Spawn.y, Spawn.z));
    };
};