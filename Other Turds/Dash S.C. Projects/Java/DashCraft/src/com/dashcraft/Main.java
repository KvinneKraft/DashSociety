
package com.dashcraft;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.Sound;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.entity.Player;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.plugin.java.JavaPlugin;

public class Main extends JavaPlugin implements CommandExecutor
{
    private void print(String str) 
    {
        System.out.println("(DashCraft): " + str);
    };
    
    private String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    @Override public void onEnable()
    {
        print("Loading the plugin ....");
        
        ItemMeta meta = fish_item.getItemMeta();
        
        meta.setDisplayName(color("&a&lToxicated Fish"));
        
        List<String> toxic_lore = Arrays.asList(
            new String[] 
            {
                "&aI am toxicated fish, if you eat",
                "&ame, you may die from it!",
            }
        );
        
        for (String str : toxic_lore)
        {
            toxic_lore.set(
                toxic_lore.indexOf(str), color(str)
            );
        };
        
        meta.setLore(toxic_lore);
        
        fish_item.setItemMeta(meta);
        
        getCommand("dashcraft").setExecutor(this);
        
        print("The plugin has been loaded!");
    };
    
    private final Integer cooldown = 120 * 20;
    
    private final String permission = "dashcraft.command";
    
    private final List<Player> players = new ArrayList<>();
    
    private final ItemStack fish_item = new ItemStack(Material.COD, 1);
    
    @Override public boolean onCommand(CommandSender s, Command c, String a, String[] as)
    {
        if(!(s instanceof Player))
        {
            print("You may only use this through the in-game.");
            return false;
        };
        
        final Player p = (Player) s;
        
        if(!p.hasPermission(permission))
        {
            p.playSound(p.getLocation(), Sound.ENTITY_WITHER_DEATH, 30, 30);
            p.sendMessage(color("&cYou have insufficient permissions!"));
            
            return false;
        }
        
        else if(players.contains(p))
        {
            p.sendMessage(color("&cYou are on a cooldown, you must wait!"));
            return false;
        };
        
        p.getInventory().addItem(fish_item);
        
        if(!p.isOp())
        {        
            players.add(p);

            getServer().getScheduler().runTaskLater(this, 
                new Runnable() 
                {
                    @Override public void run() 
                    { 
                        if(players.contains(p))
                        {
                            players.remove(p);

                            if(p.isOnline())
                            {
                                p.sendMessage(color("&aYou may now eat another fish!"));
                            };
                        };
                    }; 
                }, 

                cooldown
            );
        };
        
        return true;
    };
    
    @Override public void onDisable()
    {
        print("The plugin has been disabled!");
    };
};