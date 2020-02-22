

// Author: Dashie
// Version: 1.0


package com.dashmobs;


import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.enchantments.Enchantment;
import org.bukkit.inventory.ItemStack;
import java.util.ArrayList;
import org.bukkit.Material;
import java.util.List;


public class DashManager
{   
    private static boolean isMaterial(String str)
    {
        return Material.valueOf(str) != null;
    };
    
    
    private static boolean isEnchant(String str)
    {
        return Enchantment.getByName(str) != null;
    };
    
    
    private static boolean isInteger(String str)
    {
        try
        {
            Integer.valueOf(str);   
            return true;
        }
        
        catch (Exception e)
        {
            return false;
        }
    };
    
    
    public static void reload_plugin()
    {
        HarderMobs.plugin.reloadConfig();
        HarderMobs.config = HarderMobs.plugin.getConfig();
        
        FileConfiguration config = HarderMobs.config;
        
        String[] gear_types = new String[] { "helmets", "chestplates", "leggings", "boots", "weapons" };
        
        for(String type : gear_types)
        {
            List<ItemStack> gear_cache = new ArrayList<>();
            
            for(String str : config.getStringList("harder-mobs.mob-properties.gear." + type))
            {
                if(!isMaterial(str))
                {
                    HarderMobs.print("Invalid material type: " + str + " ! Skipping ....");
                    continue;
                };
                
                gear_cache.add(new ItemStack(Material.valueOf(str), 1));
            };
            
            if(gear_cache.size() > 0)
            {
                md.gears.put(type, gear_cache);
            };
            
            List<Enchantment> enchant_cache = new ArrayList<>();
            List<Integer> level_cache = new ArrayList<>();
            
            for(String str : config.getStringList("harder-mobs.mob-properties.enchantments.gear." + type))
            {
                String[] arr = str.split(" ");
                
                if((arr.length != 2) || (!isInteger(arr[1])))
                {
                    HarderMobs.print("Invalid enchantment format found, skipping ....");
                    continue;
                };
                
                String enchant = str.split(" ")[0];
                
                if(!isEnchant(enchant))
                {
                    HarderMobs.print("Invalid enchantment type: " + enchant + " ! Skipping ....");
                    continue;
                };
                
                enchant_cache.add(Enchantment.getByName(enchant));
                level_cache.add(Integer.valueOf(arr[1]));
            };
            
            if(enchant_cache.size() > 0)
            {
                md.enchantments.put(type, enchant_cache);            
            };
        };
    };
};
