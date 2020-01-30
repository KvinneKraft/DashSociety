
package com.dashcraft.tools;

import org.bukkit.ChatColor;


public class Tools
{
    public String transStr(String msg)
    {
        return ChatColor.translateAlternateColorCodes('&', msg);
    };
    
    public void print(String msg)
    {
        System.out.println(transStr("(DashStrict): " + msg));
    };
};