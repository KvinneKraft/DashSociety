
// Author: Dashie
// Version: 1.0

package DashSmelter.src.com.dashsmelty;

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
    
    List<String> rawMeltRecipes = config.getStringList("meltables");

    public void RegisterRecipes()
    {        
        for(String rawRecipe : rawMeltRecipes)
        {
            String[] recipe = rawRecipe.split(" ");
                
            if(recipe.length < 3)
                continue;
                
            Material source_item = Material.getMaterial(recipe[0]);
            Material result_item = Material.getMaterial(recipe[1]);
                
            Integer result_amount = Integer.valueOf(recipe[2]);

            Bukkit.getServer().addRecipe(new FurnaceRecipe(new ItemStack(result_item, result_amount), source_item));
        };
    };
};
