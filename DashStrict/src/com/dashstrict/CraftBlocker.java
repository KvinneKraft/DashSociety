
package com.dashstrict;

import com.dashcraft.tools.Tools;
import java.util.ArrayList;
import java.util.List;
import org.bukkit.Bukkit;
import org.bukkit.Material;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.inventory.PrepareItemCraftEvent;
import org.bukkit.inventory.ItemStack;


public class CraftBlocker
{
    FileConfiguration config = DashStrict.config;
    
    List<String> rawBadItems = config.getStringList("craft-blocker.items");
    List<Material> badItems = new ArrayList<Material>();
    
    Tools dashcore = new Tools();
    
    String denyMessage = dashcore.transStr(config.getString("craft-blocker.denied-message"));
    String bypassPermission = config.getString("craft-blocker.bypass-permission");
    
    public void verifyRecipe(PrepareItemCraftEvent e)
    {
        if(!(e.getViewers().get(0) instanceof Player))
            return;
        
        Player player = (Player)e.getViewers().get(0);

        if(player.hasPermission(bypassPermission))
            return;
        
        if((e.getRecipe() == null) || (e.getRecipe().getResult() == null) || (e.getRecipe().getResult().getType() == null))
            return;
        
        if(badItems.size() < 1)
            for(String item : rawBadItems)
                badItems.add(Material.getMaterial(item));
        
        if(badItems.contains(e.getRecipe().getResult().getType()))
        {
            e.getInventory().setResult(new ItemStack(Material.AIR));
            
            player.sendMessage(denyMessage);
            player.closeInventory();
        };
    };
};
