
// Author: Dashie
// Version: 1.0

package com.philosophy;

import java.util.Arrays;
import java.util.List;
import java.util.Random;
import org.bukkit.Material;
import org.bukkit.enchantments.Enchantment;
import org.bukkit.entity.Arrow;
import org.bukkit.entity.Creeper;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.LivingEntity;
import org.bukkit.entity.Player;
import org.bukkit.entity.Skeleton;
import org.bukkit.entity.Stray;
import org.bukkit.entity.TippedArrow;
import org.bukkit.entity.Zombie;
import org.bukkit.event.entity.CreatureSpawnEvent;
import org.bukkit.event.entity.EntityDamageByEntityEvent;
import org.bukkit.event.entity.EntityDamageEvent;
import org.bukkit.inventory.EntityEquipment;
import org.bukkit.inventory.ItemStack;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;

public class Organisms
{
    final static List<PotionEffect> effects = Arrays.asList
    (
        new PotionEffect[]
        {
            new PotionEffect(PotionEffectType.BLINDNESS, 8 * 20, 2),
            new PotionEffect(PotionEffectType.POISON, 8 * 20, 2),
            new PotionEffect(PotionEffectType.WITHER, 8 * 20, 2),
            new PotionEffect(PotionEffectType.HUNGER, 8 * 20, 2),
            new PotionEffect(PotionEffectType.CONFUSION, 8 * 20, 2),
        }
    );
    
    final static Random rand = new Random();
    
    public static void onEntityAttackSkelly(final EntityDamageByEntityEvent e)
    {
        if (!(e.getEntity() instanceof Player))
        {   
            return;
        }        
        
        else if (!e.getCause().equals(EntityDamageEvent.DamageCause.PROJECTILE) || !(e.getDamager() instanceof Arrow))
        {
            return;
        }
        
        else if (!(((Arrow)e.getDamager()).getShooter() instanceof Stray) && !(((Arrow)e.getDamager()).getShooter() instanceof Skeleton))
        {
            return;
        };        
        
        final Player t = (Player) e.getEntity();
        final int v = rand.nextInt(100);
        
        if (v <= 1)
        {
            e.getEntity().getWorld().strikeLightningEffect(e.getEntity().getLocation());
        }
        
        else if (v <= 20)
        {
            t.addPotionEffect
            (
                effects.get
                (
                    rand.nextInt(effects.size())
                )
            );
        };
    };
    
    final static ItemStack diamond_sword = new ItemStack(Material.DIAMOND_SWORD, 1);    
    
    final static ItemStack diamond_helmet = new ItemStack(Material.DIAMOND_HELMET, 1);
    final static ItemStack diamond_chestplate = new ItemStack(Material.DIAMOND_CHESTPLATE, 1);
    final static ItemStack diamond_leggings = new ItemStack(Material.DIAMOND_LEGGINGS, 1);
    final static ItemStack diamond_boots = new ItemStack(Material.DIAMOND_BOOTS, 1);
    
    public static void onEntitySpawn(final CreatureSpawnEvent e)
    {
        final LivingEntity entity = (LivingEntity) e.getEntity();
        
        if (entity instanceof Creeper)
        {
            if (rand.nextInt(100) < 25)
            {
                ((Creeper)entity).setPowered(true);
            }
        }
        
        else if (entity instanceof Skeleton)
        {
            if (rand.nextInt(100) < 5)
            {
                entity.getWorld().spawnEntity(e.getLocation(), EntityType.WITHER_SKELETON);
            };
        }
        
        else if (entity instanceof Zombie)
        {   
            if (rand.nextInt(100) < 95)
            {
                return;
            };
            
            if (!diamond_sword.containsEnchantment(Enchantment.DAMAGE_ALL))
            {
                diamond_sword.addUnsafeEnchantment(Enchantment.FIRE_ASPECT, 10);
                diamond_sword.addUnsafeEnchantment(Enchantment.DAMAGE_ALL, 10);
            };
            
            final EntityEquipment equip = (EntityEquipment) entity.getEquipment();
            
            equip.setHelmet(diamond_helmet);
            equip.setChestplate(diamond_chestplate);
            equip.setLeggings(diamond_leggings);
            equip.setBoots(diamond_boots);
            
            equip.setItemInMainHand(diamond_sword);
        }
    };
};
