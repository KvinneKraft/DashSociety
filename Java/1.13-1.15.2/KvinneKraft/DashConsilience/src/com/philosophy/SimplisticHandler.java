
// Author: Dashie
// Version: 1.0

package com.philosophy;

import java.util.List;
import org.bukkit.Bukkit;
import org.bukkit.Location;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.entity.Bee;
import org.bukkit.entity.Entity;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.Player;

public class SimplisticHandler implements CommandExecutor
{
    private final Location staff_house_location = new Location(Bukkit.getWorld("world"), 232.864, 71.50000, -256.045);
    private final Location rules_house_location = new Location(Bukkit.getWorld("world"), 240.939, 69, -212.038);
    
    @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
    {
        if (!(s instanceof Player))
        {
            return false;
        };
        
        final String l = c.toString().toLowerCase();
        final Player p = (Player) s;        
        
        if(l.contains("github"))
        {
            p.sendMessage(Freya.color("&eHey there, you can find some of my work at &dhttps://github.com/KvinneKraft &e!"));
        }
        
        else if (l.contains("shop"))
        {
            p.chat("/warp shop");
        }
        
        else if (l.contains("staff"))
        {
            p.sendMessage(Freya.color("&aYou have been teleported to the &estaff house&a!"));
            p.teleport(staff_house_location);
        }
        
        else if (l.contains("rules"))
        {
            p.sendMessage(Freya.color("&aYou have been teleported to the &erule house&a!"));
            p.teleport(rules_house_location);
        }
        
        else if (l.contains("bee") && p.isOp())
        {
            final List<Entity> mobs = p.getNearbyEntities(50, 50, 50);            
            
            Bukkit.getScheduler().runTaskAsynchronously
            (
                Consilience.plugin,
                    
                new Runnable()
                {
                    @Override public void run()
                    {
                        boolean hasFound = false;
                        
                        for (Entity entity : mobs)
                        {
                            if (entity instanceof Bee)
                            {
                                if (entity.getCustomName() != null && entity.getCustomName().length() >= 4)
                                {   
                                    ((Bee) entity).remove();
                                    
                                    if (!hasFound)
                                    {
                                        p.sendMessage(Freya.color("&cKilled ze wusps!"));                                        
                                    };                                    
                                    
                                    hasFound = true;
                                };
                            };
                        };
                        
                        if (hasFound)
                            return;

                        Bukkit.getScheduler().runTask
                        (
                            Consilience.plugin, 

                            new Runnable() 
                            { 
                                @Override public void run() 
                                {
                                    final Bee bee = (Bee) p.getWorld().spawnEntity(p.getLocation(), EntityType.BEE);

                                    bee.setCustomNameVisible(false);
                                    bee.setCustomName(p.getName());
                                    
                                    bee.setRemoveWhenFarAway(true);
                                    bee.setInvulnerable(true);   
                                    
                                    bee.setTarget(p);
                                }; 
                            }
                        );
                        
                        p.sendMessage(Freya.color("&aSummoned ze &6B&ee&6e&ee&6e&a!"));
                    };
                }
            );
        }
        
        else
        {
            p.sendMessage(Freya.color("&eHm, if you want to go to our discord, head over to &dhttps://discord.gg/3WvU9mF &e!"));
        };
        
        return true;
    };
};