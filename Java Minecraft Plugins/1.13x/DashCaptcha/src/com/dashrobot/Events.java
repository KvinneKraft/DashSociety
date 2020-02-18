
// Author: Dashie
// Version: 1.0


package com.dashrobot;


import org.bukkit.event.inventory.InventoryClickEvent;
import org.bukkit.event.inventory.InventoryCloseEvent;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.inventory.Inventory;
import org.bukkit.inventory.ItemStack;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.entity.Player;
import org.bukkit.Material;
import org.bukkit.Bukkit;
import java.util.HashMap;


//---//
//---// Events Handler Class:
//---//

public class Events implements Listener
{   
    
    //---//
    //---// Dash GUI Methods:
    //---//
    
    public Inventory open_captcha_dialog(Player p)
    {
        Inventory captcha_dialog = Bukkit.getServer().createInventory(p, 9, Captcha.color("&c&lClick the Apple:"));
        
        for(int id = 1; id <= inventory_slots; id += 1)//Not indexes?
        {
            ItemStack item = new ItemStack(Material.ENCHANTED_GOLDEN_APPLE);
            
            if(id != 5)
            {
                item.setType(Material.APPLE);
                continue;
            };
            
            ItemMeta item_meta = item.getItemMeta();
            
            item_meta.setCustomModelData(2020);
            item.setItemMeta(item_meta);
            
            captcha_dialog.addItem(item);
        };

        return captcha_dialog;
    };    
    
    
    //---//
    //---// Event Handler Methods:
    //---//
    
    HashMap<Player, Integer> captcha_cache = new HashMap<>();     
    String timeout_kick_message;
    
    int inventory_slots, verification_timeout;
    
    
    @EventHandler
    public void onPlayerJoin(PlayerJoinEvent e)
    {
        Player p = e.getPlayer();

        Bukkit.getScheduler().runTaskLaterAsynchronously(Captcha.plugin, 
            new Runnable()
            {
                @Override
                public void run()
                {
                    if(captcha_cache.containsKey(p))
                    {
                        p.kickPlayer(timeout_kick_message);
                    };
                };
            }, 
            
            verification_timeout * 20
        );
        
        p.openInventory(open_captcha_dialog(p));
    };
    
    
    Material verification_material;           
    String maximum_succeed_message;        
    
    int maximum_attempts;        
    
    
    @EventHandler
    public void onPlayerCloseInventory(InventoryCloseEvent e)
    {
        if((e.getViewers() == null) || (e.getViewers().size() < 1) || (e.getInventory().getContents().length != inventory_slots))
        {
            return;
        };
            
        ItemStack i = e.getInventory().getItem(0);
        
        if((i == null) || (!i.hasItemMeta()) || (!i.getItemMeta().hasCustomModelData()) || (i.getItemMeta().getCustomModelData() != (2020)))
        {
            return;
        };
        
        Player p = (Player) e.getViewers().get(0);
        p.openInventory(open_captcha_dialog(p));
    };
    
    
    @EventHandler
    public void onInventoryClick(InventoryClickEvent e)
    {
        Player p = (Player) e.getWhoClicked();
        if(captcha_cache.containsKey(p))
        {
            if(captcha_cache.get(p) >= maximum_attempts)
            {
                p.kickPlayer(maximum_succeed_message);
                captcha_cache.remove(p);
                
                return;
            };
        };
        
        ItemStack i = e.getCurrentItem();
        
        if((i == null) || (!i.hasItemMeta()) || (!i.getItemMeta().hasCustomModelData()) || (i.getItemMeta().getCustomModelData() != (2020)))
        {
            return;
        }
        
        e.setCancelled(true);        
        
        if(i.getType().equals(verification_material))
        {
            captcha_cache.remove(p);
            p.closeInventory();
            
            return ;
        };
        
        Integer new_count = 1;
        
        if(captcha_cache.containsKey(p))
        {
            new_count += captcha_cache.get(p);
        };
        
        captcha_cache.put(p, new_count);
    };
    
    
    @EventHandler
    public void onPlayerQuit(PlayerQuitEvent e)
    {
        Player p = e.getPlayer();
        
        if(captcha_cache.containsKey(p))
        {
            captcha_cache.remove(p);
        };
    };
};
