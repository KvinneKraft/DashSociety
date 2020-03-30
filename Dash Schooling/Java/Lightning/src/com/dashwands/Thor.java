
// Author: Dashie
// Version: 1.0

package com.dashwands;

import java.util.Arrays;
import org.bukkit.ChatColor;
import org.bukkit.Location;
import org.bukkit.Material;
import org.bukkit.command.CommandExecutor;
import org.bukkit.enchantments.Enchantment;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.plugin.java.JavaPlugin;

public class Thor extends JavaPlugin implements Listener, CommandExecutor
{
    private String color(String str) { return ChatColor.translateAlternateColorCodes('&', str); };
    private void print(String str) { System.out.println("(Lightning): " + str); };
    
    @Override public void onEnable()
    {
        print("Plugin is loading ....");
        
        getServer().getPluginManager().registerEvents(this, this);
        
        loadData();
        
        print("Plugin has been loaded!");
    };
    
    private void loadData()
    {
        ItemMeta item_meta = hammer_item.getItemMeta();
        
        item_meta.setDisplayName(color("&b&l&nT&b&lhor's &3&l&nH&3&lammer"));
        
        item_meta.setLore
        (
            Arrays.asList
            (
                new String[] 
                {
                    color("&a&oMade by those who you have never"), 
                    color("&a&oheard of, Dashie, the one and only."),
                    
                    "", 
                    
                    color("&a&oThis hammer shall defeat any hostile!"),
                }
            )
        );
        
        item_meta.setUnbreakable(true);
        
        item_meta.addEnchant(Enchantment.FIRE_ASPECT, 4, true);        
        item_meta.addEnchant(Enchantment.DAMAGE_ALL, 4, true);
        item_meta.addEnchant(Enchantment.THORNS, 32, true);
        
        hammer_item.setItemMeta(item_meta);
    };
    
    private final ItemStack hammer_item = new ItemStack(Material.DIAMOND_AXE, 1);
    
    @EventHandler public void onPlayerInteract(PlayerInteractEvent e)
    {
        if(e.getItem() == null || !e.getItem().equals(hammer_item))
        {
            return;
        };
        
        final Player p = (Player) e.getPlayer();
        final Location location = p.getTargetBlock(null, 60).getLocation();
        
        location.getWorld().strikeLightning(location);
        location.getWorld().createExplosion(location, 32);
        
        p.sendMessage(color("&6You have called upon Thor, lightning has been struck down from the sky!"));
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
};