
// Author: Dashie
// Version: 1.0

package com.dashmoney;


import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;


public class Moon
{
    public static String transStr(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    public static FileConfiguration getGlobalConfig()
    {
        return DashMoney.config;
    };
    
    public static JavaPlugin getGlobalPlugin()
    {
        return DashMoney.plugin;
    };
    
    public static void print(String str)
    {
        System.out.println(transStr("(Dash Money): " + str));
    };
};
