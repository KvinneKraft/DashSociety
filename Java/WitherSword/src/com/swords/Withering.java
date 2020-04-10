
// Author: Dashie
// Version: 1.0

package com.swords;

import java.util.Arrays;
import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.command.CommandExecutor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Entity;
import org.bukkit.entity.LivingEntity;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.EntityDamageByEntityEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;

public class Withering extends JavaPlugin implements Listener, CommandExecutor
{
    private String admin_permission = null;
    
    private int duration, amplifier;
    
    private void loadConfigData()
    {
        plugin.reloadConfig();
        config = plugin.getConfig();
        
        admin_permission = config.getString("command-permission");
        
        amplifier = config.getInt("potion-amplifier");        
        duration = config.getInt("potion-duration");
    };
    
    private FileConfiguration config = (FileConfiguration) null;
    private JavaPlugin plugin = (JavaPlugin) null;
    
    @Override public void onEnable()
    {
        print("Plugin is loading ....");
        
        saveDefaultConfig();
        loadConfigData();
        
        ItemMeta sword_meta = sword_item.getItemMeta();
        
        sword_meta.setDisplayName(color("&8Sword O\' Withering"));
        sword_meta.setLore(Arrays.asList(new String[] { "&7&oWither your enemies away!" }));
        
        sword_item.setItemMeta(sword_meta);
        
        getServer().getPluginManager().registerEvents(this, plugin);
        getCommand("withersword").setExecutor(plugin);
        
        print("Plugin has been loaded!");
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
    
    private final ItemStack sword_item = new ItemStack(Material.STONE_SWORD, 1);
    
    @EventHandler public void onEntityDamage(EntityDamageByEntityEvent e)
    {
        final Entity entity = e.getDamager();
        
        if(!(entity instanceof Player) || !(e.getEntity() instanceof LivingEntity))
        {
            return;
        };
        
        final LivingEntity living_entity = (LivingEntity) entity;
        
        living_entity.addPotionEffect(new PotionEffect(PotionEffectType.CONFUSION, duration, amplifier));        
        living_entity.addPotionEffect(new PotionEffect(PotionEffectType.WITHER, duration, amplifier));
        living_entity.addPotionEffect(new PotionEffect(PotionEffectType.POISON, duration, amplifier));
        living_entity.addPotionEffect(new PotionEffect(PotionEffectType.HUNGER, duration, amplifier));
        living_entity.addPotionEffect(new PotionEffect(PotionEffectType.SLOW, duration, amplifier));         
    };
    
    private String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    private void print(String str)
    {
        System.out.println("(Wither Swords): " + str);
    };
};