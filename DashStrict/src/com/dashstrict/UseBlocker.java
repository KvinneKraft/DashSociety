
package com.dashstrict;

import com.dashcraft.tools.Tools;
import java.util.ArrayList;
import java.util.List;
import org.bukkit.Material;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.player.PlayerInteractEvent;


public class UseBlocker
{
    FileConfiguration config = DashStrict.config;
    
    List<String> badItemsRaw = config.getStringList("use-blocker.items");
    List<Material> badItems = new ArrayList<Material>();
    
    List<String> badBlocksRaw = config.getStringList("use-blocker.blocks");
    List<Material> badBlocks = new ArrayList<Material>();
    
    Tools dashcore = new Tools();
    
    String deniedMessage = dashcore.transStr(config.getString("use-blocker.denied-message"));
    String bypassPermission = config.getString("use-blocker.bypass-permission");
    
    public void verifyUse(PlayerInteractEvent e)
    {
        if(e.getPlayer().hasPermission(bypassPermission))
            return;
        
        Material material;
        Player player = e.getPlayer();
        
        if((e.getClickedBlock() != null) && (!e.getClickedBlock().equals(Material.AIR)))
        {
            material = e.getClickedBlock().getType();
            
            if(badBlocks.contains(material))
            {
                player.sendMessage(deniedMessage);
                e.setCancelled(true);
            };
        }
        
        else
        if((e.getItem() != null) && (!e.getItem().getType().equals(Material.AIR)))
        {
            material = e.getItem().getType();
            
            if(badItems.contains(material))
            {
                player.sendMessage(deniedMessage);
                e.setCancelled(true);
            };
        };
    };
};
