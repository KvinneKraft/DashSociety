
// Author: Dashie
// Version: 1.0

package com.dashsmelty;

import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;

public class Moon
{
    public static String transStr(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    public static FileConfiguration getGlobalConfig()
    {
        return DashSmelter.config;
    };
    
    public static void print(String str)
    {
        System.out.println(transStr("(Dash Smelter): " + str));
    };
};