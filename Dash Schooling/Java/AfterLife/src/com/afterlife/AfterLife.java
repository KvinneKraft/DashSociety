
// Author: Dashie
// Version: 1.0

package com.afterlife;

import org.bukkit.ChatColor;
import org.bukkit.Location;
import org.bukkit.World;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.Player;
import org.bukkit.entity.Zombie;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.PlayerDeathEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;

public class AfterLife extends JavaPlugin implements CommandExecutor, Listener
{
    private String color(String str) { return ChatColor.translateAlternateColorCodes('&', str); };
    private void print(String str) { System.out.println("(After Life): " + str); };
    
    private FileConfiguration config = (FileConfiguration) null;
    private final JavaPlugin plugin = (JavaPlugin) this;
    
    private void loadConfiguration()
    {
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        admin_permission = config.getString("parasites.permissions.admin-permission");
        use_permission = config.getString("parasites.permissions.use-permission");
        
        afterlife_message = color(config.getString("parasites.messages.afterlife-message"));
        name_tag = color(config.getString("parasites.meta-info.name-tag"));
        
        potion_buffs = config.getBoolean("parasites.meta-info.potion-buffs");
        lightning = config.getBoolean("parasites.meta-info.lightning");
    };
    
    private String admin_permission, use_permission, afterlife_message, name_tag;
    private Boolean lightning, potion_buffs, isEnabled = true;
    
    @Override public void onEnable() 
    {
        print("Plugin is loading ....");
        
        saveDefaultConfig();
        
        config = (FileConfiguration) getConfig();
        
        loadConfiguration();
        
        getServer().getPluginManager().registerEvents(this, plugin);
        getCommand("afterlife").setExecutor(plugin);
        
        print("Plugin has been loaded!");
    };
    
    @EventHandler public void onPlayerDeath(final PlayerDeathEvent e)
    {
        final Player p = (Player) e.getEntity();
        
        if ( !isEnabled || !p.hasPermission(use_permission) )
        {
            return;
        };
        
        final Location l = p.getLocation();
        final World w = p.getWorld();
        
        if ( lightning )
        {
            w.strikeLightningEffect(l);
        };
        
        final Zombie zombie = (Zombie) w.spawnEntity(l, EntityType.ZOMBIE);        
        
        if ( potion_buffs )
        {
            zombie.addPotionEffect(new PotionEffect(PotionEffectType.DAMAGE_RESISTANCE, 30 * 20, 2));
            zombie.addPotionEffect(new PotionEffect(PotionEffectType.REGENERATION, 30 * 20, 2));
            zombie.addPotionEffect(new PotionEffect(PotionEffectType.INCREASE_DAMAGE, 30 * 20, 2));
        };
        
        zombie.setCustomNameVisible(true);        
        zombie.setCustomName(name_tag.replace("%p%", p.getName()));
        
        final ItemStack i = p.getInventory().getItemInMainHand();
        
        if(i != null)
        {
           zombie.getEquipment().setItemInMainHand(i);
           zombie.getEquipment().setItemInMainHandDropChance(50);
        };
        
        zombie.setMaxHealth(40);
        zombie.setHealth(40);
        
        return;
    };
    
    @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
    {
        if ( !(s instanceof Player) )
        {
            print("I am sorry, but only a player may use this command!");
            return false;
        };
        
        final Player p = (Player) s;
        
        if ( !p.hasPermission(admin_permission) )
        {
            p.sendMessage(color("&cYou are not allowed to use this command!"));
            return false;
        }
        
        else if (as.length >= 1)
        {
            if (as[0].equalsIgnoreCase("reload"))
            {
                p.sendMessage(color("&e>>> &aReloading configuration data ...."));
                
                loadConfiguration();
                
                p.sendMessage(color("&e>>> &aSuccessfully reloaded configuration data!"));
                
                return true;
            }
            
            else if (as[0].equalsIgnoreCase("toggle")) 
            {
                p.sendMessage(color("&7&oNote that this is only effective until the plugin gets disabled."));
                
                if (isEnabled)
                {
                    p.sendMessage(color("&aThe plugin is now disabled."));
                    isEnabled = false;
                }
                
                else
                {
                    p.sendMessage(color("&aThe plugin is now enabled."));
                    isEnabled = true;
                };
                
                return true;
            };
        };
        
        p.sendMessage(color("&cCorrect usage: /afterlife [toggle | reload]"));
        
        return true;
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
};
