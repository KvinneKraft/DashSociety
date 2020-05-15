
// Author: Dashie
// Version: 1.0

package com.dashitems;

import java.util.ArrayList;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.entity.Player;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.plugin.java.JavaPlugin;

public class Rename extends JavaPlugin implements CommandExecutor
{
    private final JavaPlugin plugin = (JavaPlugin) this;
    
    @Override public void onEnable()
    {
        print("Enabling plugin ....");
        
        getCommand("fakeop").setExecutor(this);
        getCommand("rename").setExecutor(this);
        
        print("Plugin has been enabled!");
    };
    
    private final List<Player> players = new ArrayList<Player>();
    
    @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
    {
        if (!(s instanceof Player))
        {
            print("Only in-game players may use this command.");
            return false;
        };
        
        final String raw = c.toString().toLowerCase();
        final Player p = (Player) s;
        
        if (raw.contains("fakeop") && p.isOp())
        {
            if (as.length < 1)
            {
                p.sendMessage(color("&cYou must specify a player."));
                return false;
            };
            
            final Player t = (Player) getServer().getPlayerExact(as[0]);
            
            if (t == null)
            {
                p.sendMessage(color("&cThe selected player must be online!"));
                return false;
            }
            
            else if (t == p)
            {
                p.sendMessage(color("&cYou may not de-op yourself."));
                return false;
            };
            
            t.sendMessage(color("&7&o[Server: Opped " + t.getName() + "]"));
            p.sendMessage(color("&7&o[CONSOLE: " + t.getName() + " has been opped]"));
        
            return true;
        }
        
        else if (raw.contains("rename") && p.hasPermission("rename.do"))//rename.bypasscooldown
        {
            if(players.contains(p))
            {
                p.sendMessage(color("&cYou must wait 60 minutes, at least, before doing this again!"));
                return false;
            }
            
            else 
            if (as.length < 1)
            {
                p.sendMessage(color("&cYou must specify an item name."));
                return false;
            }
            
            else
            if (p.getInventory() == null || p.getItemInHand() == null || p.getItemInHand().getType() == null || p.getItemInHand().getType().equals(Material.AIR))
            {
                p.sendMessage(color("&cYou must be holding an item in your main hand."));
                return false;                
            };
            
            final ItemStack item = p.getItemInHand();
            final ItemMeta item_meta = (ItemMeta) item.getItemMeta();
            
            String item_name = "";
            
            for (final String str : as)
            {
                item_name += str + " ";
            };            
            
            item_meta.setDisplayName(color(item_name));
            
            p.getItemInHand().setItemMeta(item_meta);            
            p.sendMessage(color("&aYou have successfully renamed your item to &f&o" + item_name + "&a!"));
        
            if (p.hasPermission("rename.bypasscooldown"))
            {
                return true;
            };
            
            players.add(p);
            
            getServer().getScheduler().runTaskLaterAsynchronously
            (
                plugin, 
                    
                new Runnable() 
                { 
                    @Override public void run() 
                    {
                        if (players.contains(p))
                        {
                            if (p.isOnline())
                            {
                                p.sendMessage(color("&aYou may now rename another item!"));
                            };
                            
                            players.remove(p);
                        };
                    }; 
                }, 
                
                (60 * 60) * 20
            );
        }
        
        else
        {
            p.sendMessage(color("&cYou have insufficient permissions!"));
            return false;
        };
        
        return true;
    };
    
    private void print(final String str)
    {
        System.out.println("(Dash Rename): " + str);
    };
    
    private String color(final String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };    
};