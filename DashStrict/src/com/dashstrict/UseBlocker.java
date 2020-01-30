
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
        
        if(badItems.size() < 1)
            for(String rawItem : badItemsRaw)
                badItems.add(Material.getMaterial(rawItem));
        
        if(badBlocks.size() < 1)
            for(String rawBlock : badBlocksRaw)
                badBlocks.add(Material.getMaterial(rawBlock));
        
        Player player = e.getPlayer();
        
        if((e.getClickedBlock() == null) && (e.getMaterial() == null))
            return;
        
        if(e.getClickedBlock() != null)
        {
            if(badBlocks.contains(e.getClickedBlock().getType()))
            {
                player.sendMessage(deniedMessage);
                e.setCancelled(true);
            };            
        };
        
        if(e.getMaterial() != null)
        {
            if(badItems.contains(e.getMaterial()))
            {
                player.sendMessage(deniedMessage);
                e.setCancelled(true);
            };
        };
    };
};
