
// Author: Dashie
// Version: 1.0

package com.kvinnekraft;

import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public final class NextGenRTP extends JavaPlugin
{
    FileConfiguration config;
    JavaPlugin plugin;

    protected void LoadConfiguration()
    {
        if (plugin != this)
        {
            plugin = (JavaPlugin) this;
        };

        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();


    };

    @Override public void onEnable()
    {
        print("I am crawling out of my nest ....");

        LoadConfiguration();

        print("");
        print("I am now out of my nest!");
    };

    @Override public void onDisable()
    {
        getServer().getScheduler().cancelTasks(plugin);
        print("I am now dead!");
    };

    protected String color(final String d)
    {
        return ChatColor.translateAlternateColorCodes('&', d);
    };

    protected void print(final String d)
    {
        System.out.println("(Next Gen RTP): " + d);
    };
};
