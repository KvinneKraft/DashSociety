
// Author: Dashie
// Version: 1.0

package com.recoded;

import java.util.ArrayList;
import java.util.List;
import org.bukkit.Bukkit;
import org.bukkit.Location;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.inventory.ItemStack;

public class EventsHandler implements Listener
{
    public static List<Integer> cooldowns = new ArrayList<>();
    public static List<String> perms = new ArrayList<>();
    
    public static String bypass_permission;
    
    private final List<Player> players = new ArrayList<>();
    
    @EventHandler public void onInteraction(final PlayerInteractEvent e)
    {
        final Player p = (Player) e.getPlayer();
        final ItemStack wand = (ItemStack) p.getInventory().getItemInMainHand();       
        
        if (wand == null || !DashWandz.wands.contains(wand))
        {
            return;
        }
        
        else if (!e.getAction().equals(e.getAction().RIGHT_CLICK_AIR) && !e.getAction().equals(e.getAction().RIGHT_CLICK_BLOCK))
        {
            return;
        };
            
        final int id = (int) DashWandz.wands.indexOf(wand);
  
        if (players.contains(p))
        {
            p.sendMessage(Kvinne.color("&cYou must wait at least &4" + cooldowns.get(id).toString() + " seconds &c!"));
            return;
        }        
        
        else if (!p.hasPermission(perms.get(id)))
        {
            p.sendMessage(Kvinne.color("&cYou may not use this."));
            return;
        }
        
        else if (wand.equals(DashWandz.wands.get(0)))/*Firework Wand*/
        {
            
        }
        
        else if (wand.equals(DashWandz.wands.get(1)))/*Lightning Wand*/
        {
            final Location location = (Location) p.getTargetBlockExact(100).getLocation();
            
            Bukkit.getServer().getScheduler().runTaskAsynchronously
            (
                DashWandz.plugin,
                
                new Runnable()
                {
                    @Override public void run()
                    {
                        for (int strike = 0; strike < 6; strike += 1)
                        {
                            Bukkit.getServer().getScheduler().runTask
                            (
                                DashWandz.plugin,
                                
                                new Runnable()
                                {
                                    @Override public void run()
                                    {
                                        location.getWorld().strikeLightning(location);
                                    };
                                }
                            );
                        };
                    };
                }
            );
        }
        
        else if (wand.equals(DashWandz.wands.get(2)))/*Wither Wand*/
        {
            
        }
        
        else if (wand.equals(DashWandz.wands.get(3)))/*Fireball Wand*/
        {
            
        }
        
        else
        {
            return;/*I do not even know how you would get here.*/
        };
        
        p.sendMessage(Kvinne.color("&bYou have used your &r" + DashWandz.wands.get(id).getItemMeta().getDisplayName() + "&b!"));
        
        if (p.hasPermission(bypass_permission) || cooldowns.get(id) < 1)
        {
            return;
        };
        
        players.add(p);
        
        Bukkit.getServer().getScheduler().runTaskLaterAsynchronously
        (
            DashWandz.plugin, 
                
            new Runnable() 
            { 
                @Override public void run() 
                {  
                    if (players.contains(p))
                    {
                        players.remove(p);
                    };
                    
                    if (p.isOnline())
                    {
                        p.sendMessage(Kvinne.color("&bYou may now use your &r" + DashWandz.wands.get(id).getItemMeta().getDisplayName() + " &bagain!"));
                    };
                }; 
            },
                
            cooldowns.get(id) * 20
        );
    };
};
