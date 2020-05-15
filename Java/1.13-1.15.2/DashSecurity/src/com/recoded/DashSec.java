package com.recoded;

import org.bukkit.ChatColor;

public class DashSec
{
    public static void print(final String line)
    {
        System.out.println("(Dash Security): " + line);
    };
    
    public static String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
};