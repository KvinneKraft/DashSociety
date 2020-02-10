
// Author: Dashie
// Version: 1.0

package com.dashcollection;


import java.util.List;
import java.util.UUID;
import org.bukkit.Bukkit;
import java.util.ArrayList;
import org.bukkit.Bukkit;
import org.bukkit.Server;
import org.bukkit.entity.Player;
import org.bukkit.event.Listener;
import org.bukkit.event.EventHandler;
import org.bukkit.scheduler.BukkitTask;
import org.bukkit.scheduler.BukkitScheduler;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;


public class EventsHandler implements Listener
{
    RandomCollection<String> randomizer = new RandomCollection<String>(); 
    
    List<BukkitTask> runnables = new ArrayList<>();    
    List<UUID> p_uuid = new ArrayList<>();
    
    Server server = Bukkit.getServer();    
    BukkitScheduler scheduler = server.getScheduler();
    
    String reward_permission;        
    String reward_message;
    
    Integer reward_interval;
    
    public void add_reward_task(Player p, UUID uuid)
    {
        if((p_uuid.contains(uuid)) || (!p.hasPermission(reward_permission)))
        {
            return;
        };
        
        runnables.add(
            scheduler.runTaskTimerAsynchronously(Session.plugin, 
                new Runnable()
                {
                    @Override
                    public void run()
                    {
                        String command = randomizer.next().replace("%player%", p.getName());
                        
                        server.dispatchCommand(server.getConsoleSender(), command);
                        
                        p.sendTitle(" ", reward_message, 1, 20, 1);                        
                        p.sendMessage(reward_message);
                    };
                },
                
                reward_interval * 20, reward_interval * 20
            )
        );
        
        p_uuid.add(uuid);        
    };
    
    @EventHandler
    public void onJoin(PlayerJoinEvent e)
    {
        add_reward_task(e.getPlayer(), e.getPlayer().getUniqueId());
    };
    
    @EventHandler
    public void onQuit(PlayerQuitEvent e)
    {
        Player p = e.getPlayer();
        UUID uuid = p.getUniqueId();
        
        if(!p_uuid.contains(uuid))
        {
            return;
        };
        
        int id_index = p_uuid.indexOf(uuid);
        
        runnables.get(id_index).cancel();
        runnables.remove(id_index);
        
        p_uuid.remove(id_index);
    };
};
