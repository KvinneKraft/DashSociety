
// Author: Dashie
// Version: 1.0

package com.ridablemub;

import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;

public class Moony
{
    public static FileConfiguration getGlobalConfig()
    {
        return Phuntum.config;
    };
    
    public static String transStr(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    public static void Print(String str)
    {
        System.out.println("(Dashies Phuntumz): " + str);
    };
};