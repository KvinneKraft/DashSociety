

// Author: Dashie
// Version: 1.0


package com.dashmobs;


import org.bukkit.enchantments.Enchantment;
import org.bukkit.inventory.ItemStack;
import org.bukkit.potion.PotionEffect;
import org.bukkit.entity.EntityType;
import java.util.ArrayList;
import java.util.HashMap;
import org.bukkit.World;
import java.util.List;


public class md
{
    static HashMap<String, List<Enchantment>> enchantments = new HashMap<>();
    static HashMap<String, List<ItemStack>> gears = new HashMap<>();
    static HashMap<String, List<Integer>> level_range = new HashMap<>();
  
    static HashMap<String, Integer> max_enchants = new HashMap<>();    
    
    static List<PotionEffect> effects = new ArrayList<>();            
    static List<EntityType> mob_types = new ArrayList<>();        
    static List<World> allowed_worlds = new ArrayList<>();
    
    static Integer spawn_chance, max_health, min_health;
};
