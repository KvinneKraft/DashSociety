
// Author: Dashie
// Version: 1.0

package com.kvinnekraft;

import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public final class SkipThatNight extends JavaPlugin
{
    protected FileConfiguration config;
    protected JavaPlugin plugin;

    @Override public void onEnable()
    {

    };

    @Override public void onDisable()
    {

    };

    protected final void print(final String d)
    {
        System.out.println("(Skip That Night): " + d);
    };

    protected final String color(final String d)
    {
        return ChatColor.translateAlternateColorCodes('&', d);
    };
};