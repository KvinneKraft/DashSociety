// Author: Dashie
// Version: 1.0

package com.kvinnekraft;

import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.block.BlockBreakEvent;
import org.bukkit.event.block.BlockPlaceEvent;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.plugin.java.JavaPlugin;

public class CommandSigns extends JavaPlugin
{
    @Override public void onEnable()
    {
        /*
        You should use HashMap<String, String>();
        Or HashMap<Unique ID, Command Data>();
        for the creation and identification of
        the command sign.

        And List<Location>(); for the placement
        locations for identification.
         */
    };

    protected class Events implements Listener
    {
        @EventHandler public void onPlayerClick(final PlayerInteractEvent e)
        {
            // Execute sign from cache
        };

        @EventHandler public void onPlayerPlace(final BlockPlaceEvent e)
        {
            // Register command sign to cache.
        };

        @EventHandler public void onPlayerBreak(final BlockBreakEvent e)
        {
            // Remove sign from cache
        };
    };

    @Override public void onDisable()
    {
        getServer().getScheduler().cancelTasks(plugin);
        print("I am now dead!");
    };
};