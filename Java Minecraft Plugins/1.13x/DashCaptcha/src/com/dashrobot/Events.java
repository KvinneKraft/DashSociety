
// Author: Dashie
// Version: 1.0


package com.dashrobot;


import org.bukkit.event.inventory.InventoryClickEvent;
import org.bukkit.event.inventory.InventoryCloseEvent;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.inventory.meta.FireworkMeta;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.inventory.Inventory;
import org.bukkit.inventory.ItemStack;
import org.bukkit.event.EventHandler;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.Firework;
import org.bukkit.FireworkEffect;
import org.bukkit.event.Listener;
import org.bukkit.entity.Player;
import java.util.ArrayList;
import org.bukkit.Material;
import org.bukkit.Location;
import org.bukkit.Bukkit;
import java.util.HashMap;
import java.util.Random;
import org.bukkit.Color;
import java.util.List;


//---//
//---// Events Handler Class:
//---//

public class Events implements Listener
{   
    
    //---//
    //---// Dash GUI Methods:
    //---//
    
    public ItemStack get_random_item()
    {
        Material material = pattern_items.get(new Random().nextInt(pattern_items.size()));
        
        if(material.equals(Material.DIAMOND))
        {
            material = Material.EMERALD_BLOCK;
        };
        
        return new ItemStack(material, 1);
    };
    
    
    List<Player> inventory_cache = new ArrayList<>();
    
    String captcha_title;
    
    
    public void open_captcha_dialog(Player p)
    {
        if((!verification_cache.containsKey(p)) || (inventory_cache.contains(p)))
        {
            if(inventory_cache.contains(p))
            {
                inventory_cache.remove(p);
            };
            
            return;
        };
        
        String str = captcha_title.replace("%item%", verification_cache.get(p).toString().replace("_", " ").toLowerCase());
        
        Inventory captcha_dialog = Bukkit.getServer().createInventory(p, 9, str);
        Integer key_index = new Random().nextInt(inventory_slots) + 1;
        
        for(int id = 0; id < inventory_slots; id += 1)
        {
            ItemStack item = get_random_item();
            
            if(id == key_index)
            {
                item.setType(verification_cache.get(p));
            };
            
            ItemMeta item_meta = item.getItemMeta();
            
            item_meta.setCustomModelData(2020);
            item.setItemMeta(item_meta);
            
            captcha_dialog.setItem(id, item);
        };

        p.openInventory(captcha_dialog);
       
        inventory_cache.add(p);
    };    
    
    
    //---//
    //---// Event Handler Methods:
    //---//
    
    HashMap<Player, Material> verification_cache = new HashMap<>();    
    HashMap<Player, Integer> captcha_cache = new HashMap<>();      
    
    List<Material> pattern_items = new ArrayList<Material>();    
    List<Material> key_items = new ArrayList<>();
    
    String timeout_kick_message;
    
    int inventory_slots, verification_timeout;
    
    
    @EventHandler
    public void onPlayerJoin(PlayerJoinEvent e)
    {
        Player p = e.getPlayer();

        Bukkit.getScheduler().scheduleSyncDelayedTask(Captcha.plugin, 
            new Runnable()
            {
                @Override
                public void run()
                {
                    if((captcha_cache.containsKey(p)) && (verification_cache.containsKey(p)))
                    {
                        p.kickPlayer(timeout_kick_message);
                        clear_essence(p);
                        return;
                    };
                };
            }, 
            
            verification_timeout //* 20
        );   
        
        Material material = key_items.get(new Random().nextInt(key_items.size()));
        
        verification_cache.put(p, material);
        
        open_captcha_dialog(p);
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
        
        Player p = (Player)e.getPlayer();
        
        if(verification_cache.containsKey(p))
        {
            Bukkit.getScheduler().scheduleSyncDelayedTask(Captcha.plugin, 
                new Runnable() 
                {
                    @Override
                    public void run()
                    {
                        open_captcha_dialog(p);       
                    };
                }, 
                
                5
            );
        };
    };
    
    
    boolean summon_fireworks, summon_lightning, send_as_title;
    String captcha_complete_message;
    
    
    private void clear_essence(Player p)
    {
        if(verification_cache.containsKey(p))
        {
            verification_cache.remove(p);
        };
        
        if(captcha_cache.containsKey(p))
        {
            captcha_cache.remove(p);
        };        
    };
    
    
    private void grant_access(Player p)
    {
        clear_essence(p);
        
        if((summon_fireworks) || (summon_lightning))
        {
            Location location = p.getLocation();
            
            p.setInvulnerable(true);
            
            if(summon_fireworks)
            {
                Firework firework = (Firework)location.getWorld().spawnEntity(location, EntityType.FIREWORK);
                FireworkMeta firework_meta = firework.getFireworkMeta();
            
                Random rand = new Random();
                
                int r = rand.nextInt(255) + 1;
                int g = rand.nextInt(255) + 1;
                int b = rand.nextInt(255) + 1;
                
                Color firework_color = Color.fromRGB(r, g, b);
            
                firework_meta.addEffect(FireworkEffect.builder().withColor(firework_color).withFlicker().withTrail().with(FireworkEffect.Type.BURST).flicker(true).build());
                firework.setFireworkMeta(firework_meta);   
                
                firework.detonate();
            };
        
            if(summon_lightning)
            {
                location.getWorld().strikeLightningEffect(location);
            };
            
            p.setInvulnerable(false);
        };
        
        p.closeInventory();              
        
        if(!send_as_title)
        {
            p.sendMessage(captcha_complete_message);
        }
        
        else
        {
            p.sendTitle("", captcha_complete_message);
        };
    };
    
    
    @EventHandler
    public void onInventoryClick(InventoryClickEvent e)
    {
        Player p = (Player) e.getWhoClicked();
        
        if(captcha_cache.containsKey(p))
        {
            if(captcha_cache.get(p) > maximum_attempts)
            {
                p.kickPlayer(maximum_succeed_message);
                clear_essence(p);
                return;
            };
        };
        
        ItemStack i = e.getCurrentItem();
        
        if((i == null) || (!i.hasItemMeta()) || (!i.getItemMeta().hasCustomModelData()) || (i.getItemMeta().getCustomModelData() != (2020)))
        {
            return;
        }
        
        e.setCancelled(true);        
        
        if(i.getType().equals(verification_cache.get(p)))
        {
            grant_access(p);
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
        clear_essence(p);
    };
};
