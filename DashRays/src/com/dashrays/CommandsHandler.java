
// Author: Dashie 
// Version: 1.0

package com.dashrays;

import org.bukkit.Material;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.command.CommandExecutor;
import org.bukkit.entity.Player;

public class CommandsHandler implements CommandExecutor
{
    FileConfiguration config = Luna.getGlobalConfig();
    
    String toggle_on_message = Luna.transStr(config.getString("properties.toggle-on-message"));
    String toggle_off_message = Luna.transStr(config.getString("properties.toggle-off-message"));
    
    String added_message = Luna.transStr(config.getString("properties.add-block-message"));
    String deleted_message = Luna.transStr(config.getString("properties.del-block-message"));
    
    String block_not_listed_message = Luna.transStr(config.getString("properties.block-not-in-list-message"));
    String block_is_listed_message = Luna.transStr(config.getString("properties.block-found-message"));    
    String block_nofou_message = Luna.transStr(config.getString("properties.block-not-found-message"));
    
    String invalid_syntax = Luna.transStr(config.getString("properties.invalid-syntax-message"));
    String notify_permission = config.getString("properties.notify-permission");
    
    @Override
    public boolean onCommand(CommandSender sender, Command cmd, String arg, String[] args)
    {
        if(!(sender instanceof Player))
            return false;
        
        Player p = (Player) sender;
        
        if(args.length < 1)
        {
            p.sendMessage(invalid_syntax);
            return false;
        };
        
        arg = args[0];
        
        if(arg.equals("toggle"))
        {
            if(DashRays.names.contains(p.getName()))
            {
                p.sendMessage(toggle_off_message);
                DashRays.names.remove(p.getName());
            }
            
            else
            {
                p.sendMessage(toggle_on_message);
                DashRays.names.add(p.getName());
            };
        }
        
        else if(args.length < 2)
        {
            p.sendMessage(invalid_syntax);
            return false;
        }
        
        else if((arg.equals("add")) || (arg.equals("del")))
        {
            Material material = Material.getMaterial(args[1]);
            
            if(material == null)
            {
                p.sendMessage(block_nofou_message);
                return false;
            }
            
            if(arg.equals("add"))
            {
                if(EventsHandler.blocks.contains(material.toString()))
                {
                    p.sendMessage(block_is_listed_message);
                    return false;
                };

                EventsHandler.blocks.add(material.toString());
                Luna.updateConfig();
                
                p.sendMessage(added_message.replace("%b%", args[1]));
            }
            
            else if(arg.equals("del"))
            {
                if(!EventsHandler.blocks.contains(material.toString()))
                {
                    p.sendMessage(block_not_listed_message.replace("%b%", args[1]));
                    return false;
                };
                    
                EventsHandler.blocks.remove(material.toString());
                Luna.updateConfig();
                
                p.sendMessage(deleted_message.replace("%b%", args[1]));
            }
        }
        
        else
        {
            p.sendMessage(invalid_syntax);
            return false;
        };
            
        return true;
    };
};
