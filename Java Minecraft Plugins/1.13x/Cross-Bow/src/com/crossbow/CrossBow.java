
package com.crossbow;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import org.bukkit.ChatColor;
import org.bukkit.Color;
import org.bukkit.FireworkEffect;
import org.bukkit.Location;
import org.bukkit.Material;
import org.bukkit.World;
import org.bukkit.command.CommandExecutor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.Firework;
import org.bukkit.entity.Player;
import org.bukkit.entity.Silverfish;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.EntityDamageByEntityEvent;
import org.bukkit.event.entity.ProjectileHitEvent;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.FireworkMeta;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.plugin.java.JavaPlugin;

public class CrossBow extends JavaPlugin implements Listener, CommandExecutor
{
    private FileConfiguration config;
    private JavaPlugin plugin;
    
    private String color(String str) { return ChatColor.translateAlternateColorCodes('&', str); };
    private void print(String str) { System.out.println("(Dash Bowz): " + str); };
    
    @Override public void onEnable()
    {
        print("Plugin is loading ....");
        
        saveDefaultConfig();
        
        load();
        
        config = (FileConfiguration) getConfig();
        plugin = (JavaPlugin) this;
        
        getServer().getPluginManager().registerEvents(this, plugin);
        getCommand("crossbow").setExecutor(this);
        
        print("Plugin is done loading!");
    };
    
    private boolean lightning, explosion, explosion_blockbreak, fireworks, poison, wither, hunger, rat;
    private int lightning_chance, explosion_chance, fireworks_chance, poison_chance, wither_chance, hunger_chance, rat_chance, shoot_cooldown;
    
    public void DetonateFirework(Location location, Color mcolor, Color fcolor, FireworkEffect.Type type)
    {
        Firework firework = (Firework) location.getWorld().spawnEntity(location, EntityType.FIREWORK);
        FireworkMeta firework_meta = firework.getFireworkMeta();
        
        firework_meta.addEffect(FireworkEffect.builder().withColor(mcolor).with(type).flicker(true).withFlicker().withTrail().withFade(fcolor).trail(true).build());
        
        firework.setFireworkMeta(firework_meta);
        firework.detonate();
    };        
    
    @EventHandler public void onProjectileHurt(EntityDamageByEntityEvent e)
    {
        if(!(e.getDamager() instanceof Player) || ((Player)e.getDamager()).getInventory().getItemInMainHand() != bow_item)
        {
            return;
        };
        
        final Random rand = new Random();
        final Player p = (Player) e.getDamager();
        
        if(poison && rand.nextInt(poison_chance) > poison_chance)
        {
            
        }
        
        else if(wither && rand.nextInt(wither_chance) > wither_chance)
        {
            
        }
        
        else if(hunger && rand.nextInt(hunger_chance) > hunger_chance)
        {
            
        };    
    };
    
    @EventHandler public void onProjectileHit(ProjectileHitEvent e)
    {
        if (!(e.getEntity().getShooter() instanceof Player))
        {
            return;
        };
        
        Player p = (Player) e.getEntity().getShooter();
        
        if (!p.hasPermission(use_permission) || p.getInventory().getItemInMainHand() != bow_item)
        {
            return;
        };
        
        final Location location = e.getEntity().getLocation();
        final World world = e.getEntity().getWorld();
        
        final Random rand = new Random();
        
        if(lightning && rand.nextInt(lightning_chance) > lightning_chance)
        {
            world.strikeLightning(location);
        };
        
        if(explosion && rand.nextInt(explosion_chance) > explosion_chance)
        {
            world.createExplosion(location, 2, explosion_blockbreak);
        };
        
        if(fireworks && rand.nextInt(fireworks_chance) > fireworks_chance)
        {
            Color main1 = Color.BLACK;
            Color sub1 = Color.BLACK;
            
            DetonateFirework(location, main1, sub1, FireworkEffect.Type.BALL);
            
            Color main2 = Color.PURPLE;
            Color sub2 = Color.PURPLE;
            
            DetonateFirework(location, main2, sub2, FireworkEffect.Type.BALL_LARGE);
            
            Color main3 = Color.YELLOW;
            Color sub3 = Color.YELLOW;
            
            DetonateFirework(location, main3, sub3, FireworkEffect.Type.BURST);
        };
        
        if(rat && rand.nextInt(rat_chance) > rat_chance)
        {
            Silverfish silver_fish = (Silverfish) world.spawnEntity(location, EntityType.SILVERFISH);
            
            silver_fish.setMaxHealth(40);
            silver_fish.setHealth(40);
            
            silver_fish.setCustomName(color("&7&lRat"));
            silver_fish.damage(5);
            
            silver_fish.setCustomNameVisible(rat);
        };            
    };
    
    private final List<Player> players = new ArrayList<>();
    
    @EventHandler public void onItemInteract(PlayerInteractEvent e)
    {
        Player p = (Player) e.getPlayer();
        
        if(e.getItem() == null || !p.hasPermission(use_permission) || e.getItem() != bow_item)
        {
            return;
        };
        
        if(players.contains(p))
        {
            p.sendMessage(color("&aYou are on a cooldown, please wait!"));
            e.setCancelled(true);
            
            return;
        };
        
        if(!p.hasPermission(admin_permission))
        {
            getServer().getScheduler().runTaskLater(plugin, 
                new Runnable()
                {
                    @Override public void run()
                    {
                        if(players.contains(p))
                        {
                            players.remove(p);
                            
                            if(p.isOnline())
                            {
                                p.sendMessage(color("&aYou may now shoot another arrow!"));
                            };
                        };
                    };
                }, 
                
                shoot_cooldown
            );
        };
    };
    
    private String use_permission, admin_permission, display_name;

    private final List<String> bow_lore = new ArrayList<>();    
    
    private final ItemStack bow_item = new ItemStack(Material.BOW, 1);
    
    private void load()
    {
        plugin.reloadConfig();
        config = plugin.getConfig();
        
        use_permission = config.getString("properties.permissions.use-permission");
        admin_permission = config.getString("properties.permissions.admin-permission");
        
        lightning = config.getBoolean("properties.effects.lightning");
        explosion = config.getBoolean("properties.effects.explosion");
        fireworks = config.getBoolean("properties.effects.fireworks");
        
        lightning_chance = config.getInt("properties.effects.lightning-chance");
        explosion_chance = config.getInt("properties.effects.explosion-chance");
        fireworks_chance = config.getInt("properties.effects.fireworks-chance");
        
        explosion_blockbreak = config.getBoolean("properties.effects.explosion-break-blocks");
        
        poison = config.getBoolean("properties.effects.poison");
        wither = config.getBoolean("properties.effects.wither");
        hunger = config.getBoolean("properties.effects.hunger");
        
        poison_chance = config.getInt("properties.effects.poison-chance");
        wither_chance = config.getInt("properties.effects.wither-chance");
        hunger_chance = config.getInt("properties.effects.hunger-chance");
        
        rat_chance = config.getInt("properties.effects.rat-chance");
        rat = config.getBoolean("properties.effects.rat");        
        
        if(bow_lore.size() > 1)
        {
            bow_lore.clear();
        };
        
        for (String str : config.getStringList("properties.bow-meta.display-lore"))
        {
            bow_lore.add(color(str));
        };
        
        display_name = color(config.getString("properties.bow-meta.display-name"));
        
        ItemMeta meta = bow_item.getItemMeta();
        
        meta.setDisplayName(display_name);
        meta.setLore(bow_lore);
        
        bow_item.setItemMeta(meta);
        
        shoot_cooldown = config.getInt("properties.bow-meta.shoot-cooldown") * 20;
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
};
