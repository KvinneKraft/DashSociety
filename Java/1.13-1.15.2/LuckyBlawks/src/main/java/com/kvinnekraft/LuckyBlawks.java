
// Author: Dashie
// Version: 1.0

package com.kvinnekraft;

import org.bukkit.ChatColor;
import org.bukkit.Location;
import org.bukkit.Material;
import org.bukkit.block.Block;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.block.BlockBreakEvent;
import org.bukkit.event.block.BlockPlaceEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.plugin.java.JavaPlugin;

import java.util.HashMap;

public class LuckyBlawks extends JavaPlugin
{
    FileConfiguration config;
    JavaPlugin plugin;

    protected class Events implements Listener
    {
        final HashMap<Location, Block> block_register = new HashMap<>();

        @EventHandler public void onBlockPlace(final BlockPlaceEvent e)
        {
            final Player p = e.getPlayer();

            if (p.hasPermission(place_permission))
            {
                if (p.getInventory().getItemInMainHand().getType().equals(lucky_block_type))
                {
                    final ItemStack block_data = p.getInventory().getItemInMainHand();

                    if (block_data.hasItemMeta())
                    {
                        try
                        {
                            if (block_data.getItemMeta().getCustomModelData() == lucky_block_id)
                            {
                                block_register.put(e.getBlock().getLocation(), e.getBlock());
                            };
                        }

                        catch (final Exception r)
                        {
                            // Nothing....
                        };
                    };
                };
            }

            else
            {
                p.sendMessage(color("&cYou are not allowed to do this!"));
                e.setCancelled(true);
            };
        };

        @EventHandler public void onBlockBreak(final BlockBreakEvent e)
        {
            if (e.getBlock().getType().equals(lucky_block_type))
            {
                final Block block = e.getBlock();

                if (block_register.containsKey(block.getLocation()))
                {
                    final Player p = e.getPlayer();

                    if (p.hasPermission(break_permission))
                    {
                        //lucky-block-function();

                        block_register.remove(block.getLocation());
                    }

                    else
                    {
                        p.sendMessage(color("&cYou are not allowed to do this!"));
                        e.setCancelled(true);
                    };
                };
            };
        };
    };

    String break_permission, place_permission, admin_permission;
    Material lucky_block_type;

    int lucky_block_id = -1;

    protected void LoadConfiguration()
    {
        if (plugin != this)
            plugin = (JavaPlugin) this;

        saveDefaultConfig();

        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();

        // Read properties

        /*
            permissions
            custom block drops
            particles
            nice format
        */
    };

    @Override public void onEnable()
    {
        print("The plugin is now loading its configuration ....");

        LoadConfiguration();

        getServer().getPluginManager().registerEvents(new Events(), plugin);
        getCommand("luckyblawks").setExecutor(new Commands());

        print("The plugin is now up and running!");
    };

    protected class Commands implements CommandExecutor
    {
        @Override public boolean onCommand(final CommandSender s, final Command c, String a, final String[] as)
        {
            if (!(s instanceof Player))
            {
                print("You may only do this as a player.");
                return false;
            };

            final Player p = (Player) s;

            if (p.hasPermission(admin_permission))
            {
                if (as.length >= 1)
                {
                    a = as[0].toLowerCase();

                    if (a.equals("reload"))
                    {
                        /************/LoadConfiguration();
                        p.sendMessage(color("&aDone!"));
                        return true;
                    };
                };

                p.sendMessage(color("&cWere you trying to type &4/luckyblawks reload &c?"));
                return false;
            };

            p.sendMessage(color("&cYou lack sufficient permissions."));
            return false;
        };
    };

    @Override public void onDisable()
    {
        print("The plugin is now no longer running.");
    };

    protected void print(final String d)
    {
        System.out.println("(Lucky Blawks): " + d);
    };

    protected String color(final String d)
    {
        return ChatColor.translateAlternateColorCodes('&', d);
    };
};
