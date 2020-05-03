
// Author: Dashie
// Version: 1.0

package com.philosophy;

import org.bukkit.Bukkit;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.entity.Player;

public class SimplisticHandler implements CommandExecutor
{
    @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
    {
        if (!(s instanceof Player))
        {
            return false;
        };
        
        final Player p = (Player) s;
        final String l = c.toString().toLowerCase();
        
        if(l.contains("github"))
        {
            p.sendMessage(Freya.color("&eHey there, you can find some of my work at &dhttps://github.com/KvinneKraft &e!"));
        }
        
        else if (l.contains("shop"))
        {
            p.chat("/warp shop");
        }
        
        else
        {
            p.sendMessage(Freya.color("&eHm, if you want to go to our discord, head over to &dhttps://discord.gg/3WvU9mF &e!"));
        };
        
        return true;
    };
};