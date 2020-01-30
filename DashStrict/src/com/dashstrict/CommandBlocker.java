
package com.dashstrict;

import com.dashcraft.tools.Tools;
import com.dashstrict.DashStrict;
import java.util.List;

import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.player.PlayerCommandPreprocessEvent;


public class CommandBlocker
{
    FileConfiguration config = DashStrict.config;
    Tools tools = new Tools();
    
    String bypassPerm = config.getString("command-block.bypass-permission");
    String deniedMssg = tools.transStr(config.getString("command-block.denied-message"));
    
    List<String> deniedCmds = config.getStringList("command-block.commands");
    
    public void verifyCommand(PlayerCommandPreprocessEvent e)
    {
        String command = e.getMessage().toLowerCase().replace(":", " ").split(" ")[0];
        Player player = e.getPlayer();
        
        if((deniedCmds.contains(command)) && (!player.hasPermission(bypassPerm)))
        {
            player.sendMessage(deniedMssg);
            e.setCancelled(true);
            
            return;
        };
    };
};
