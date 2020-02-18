
// Author: Dashie
// Version: 1.0

package com.dashredeem;


import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.entity.Player;


class CommandsHandler implements CommandExecutor
{
    boolean f = false, t = true;
    
    @Override
    public boolean onCommand(CommandSender s, Command c, String a, String[] as)
    {
        if(!(s instanceof Player))
            return f;
        
        
        
        return f;
    };
};