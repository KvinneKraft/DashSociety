

// Author: Dashie
// Version: 1.0


package com.dashmobs;


import org.bukkit.event.entity.CreatureSpawnEvent;
import org.bukkit.entity.LivingEntity;
import org.bukkit.inventory.ItemStack;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import java.util.Random;
import org.bukkit.enchantments.Enchantment;


public class Events implements Listener
{
    @EventHandler
    public void onChunkGenerationMobSpawn(CreatureSpawnEvent e)
    {
        final LivingEntity moby = e.getEntity();
        
        if((!md.allowed_worlds.contains(moby.getWorld())) || (!md.mob_types.contains(moby.getType())))
        {
            return;
        };
        
        final Random rand = new Random();
        
        int probability_key = rand.nextInt(500);
        
        if(probability_key > md.spawn_chance)
        {
            return;
        };
        
        final ItemStack helmet = md.gears.get("helmets").get(rand.nextInt(md.gears.get("helmets").size()));        
        final ItemStack chestplate = md.gears.get("chestplates").get(rand.nextInt(md.gears.get("chestplates").size()));
        final ItemStack legging = md.gears.get("leggings").get(rand.nextInt(md.gears.get("leggings").size()));
        final ItemStack boot = md.gears.get("boots").get(rand.nextInt(md.gears.get("boots").size()));
        final ItemStack sword = md.gears.get("swords").get(rand.nextInt(md.gears.get("swords").size()));
        
        addEnchantments("helmets", helmet);
        addEnchantments("chestplates", chestplate);
        addEnchantments("leggings", legging);
        addEnchantments("boots", boot);
        addEnchantments("swords", sword);
        
        moby.getEquipment().setHelmet(helmet);
        moby.getEquipment().setChestplate(chestplate);
        moby.getEquipment().setLeggings(legging);
        moby.getEquipment().setBoots(boot);
        moby.getEquipment().setItemInMainHand(sword);
        
        // Add potion effects
        // Add random health
        // Add name tag
    };
    
    
    private void addEnchantments(String type, ItemStack item)
    {
        Random rand = new Random();
        
        for(int key_id = 0; key_id < rand.nextInt(md.max_enchants.get(type)) + 1; key_id += 1)
        {
            Enchantment etype = md.enchantments.get(type).get(rand.nextInt(md.enchantments.get(type).size()));
            
            int max_level = md.level_range.get(type).get(1);
            int min_level = md.level_range.get(type).get(0);
            
            Integer elevel = rand.nextInt((max_level - min_level) + 1) + min_level;
            
            item.addUnsafeEnchantment(etype, elevel);
        };        
    };
};
