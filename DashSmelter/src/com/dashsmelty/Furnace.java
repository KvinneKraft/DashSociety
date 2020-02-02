
// Author: Dashie
// Version: 1.0

package com.dashsmelty;

import java.util.List;
import org.bukkit.Bukkit;
import org.bukkit.Material;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.event.Listener;
import org.bukkit.inventory.FurnaceRecipe;
import org.bukkit.inventory.ItemStack;

public class Furnace implements Listener
{
    FileConfiguration config = Moon.getGlobalConfig();
    
    List<String> raw_melt_recipes = config.getStringList("meltables");
    
    public boolean doesSourceExist(String source)
    {
        for(String recipe : raw_melt_recipes)
        {
            if(recipe.split(" ")[0].toLowerCase().equals(source.toLowerCase()))
            {
                return true;
            };
        };
        
        return false;
    };
    
    public int indexOfSource(String source)
    {
        for(int i = 0; i < raw_melt_recipes.size(); i += 1)
        {
            if(raw_melt_recipes.get(i).split(" ")[0].toLowerCase().equals(source.toLowerCase()))
            {
                return i;
            };
        };
        
        return -1;
    };
    
    public void updateMeltRecipes()
    {
        config.set("meltables", raw_melt_recipes);
        
        Moon.SaveConfig();
        Moon.ReloadConfig();
        
        RegisterRecipes();
        
        return;
    };
    
    public void RegisterRecipes()
    {
        Bukkit.getScheduler().runTaskAsynchronously(Moon.getGlobalPlugin(), 
            new Runnable()
            {
                @Override
                public void run()
                {
                    for(String raw_recipe : raw_melt_recipes)
                    {
                        String[] recipe = raw_recipe.split(" ");
                
                        if(recipe.length < 3)
                            continue;
                
                        Material source_item = Material.getMaterial(recipe[0]);
                        Material result_item = Material.getMaterial(recipe[1]);
                
                        Integer result_amount = Integer.valueOf(recipe[2]);

                        Bukkit.getServer().resetRecipes();         
            
                        ItemStack result_stack = new ItemStack(result_item, result_amount);
                        FurnaceRecipe furnace_recipe = new FurnaceRecipe(result_stack, source_item);
            
                        Bukkit.getServer().addRecipe(furnace_recipe);
                    };
                };
            }
        );
    };
};
