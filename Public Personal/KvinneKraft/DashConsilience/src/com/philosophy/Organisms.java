
// Author: Dashie
// Version: 1.0

package com.philosophy;

import java.util.Arrays;
import java.util.List;
import java.util.Random;
import org.bukkit.entity.Arrow;
import org.bukkit.entity.Player;
import org.bukkit.entity.Skeleton;
import org.bukkit.entity.Stray;
import org.bukkit.event.entity.CreatureSpawnEvent;
import org.bukkit.event.entity.EntityDamageByEntityEvent;
import org.bukkit.event.entity.EntityDamageEvent;
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
        
        else if (!e.getCause().equals(EntityDamageEvent.DamageCause.PROJECTILE))
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
    
    public static void onEntitySpawn(final CreatureSpawnEvent e)
    {
        
    };
};
