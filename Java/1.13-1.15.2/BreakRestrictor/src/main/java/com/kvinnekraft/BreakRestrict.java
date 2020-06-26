package com.kvinnekraft;

import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.event.Listener;
import org.bukkit.plugin.java.JavaPlugin;

public final class BreakRestrict extends JavaPlugin
{
    FileConfiguration config;
    JavaPlugin plugin;

    protected void LoadConfiguration()
    {

    };

    @Override public void onEnable()
    {
        print("A.I. is booting up ....");

        LoadConfiguration();

        getServer().getPluginManager().registerEvents(new Events(), plugin);
        getCommand("breakrestrictor").setExecutor(new Commands());

        print("\n-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\nAuthor: Dashie\nVersion: 1.0\nEmail: KvinneKraft@protonmail.com\nGithub: https://github.com/KvinneKraft \n-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");

        print("A.I. is now running!");
    };

    protected class Commands implements CommandExecutor
    {
        @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
        {
            return true;
        };
    };

    protected class Events implements Listener
    {
    };

    @Override public void onDisable()
    {
        print("A.I. is now offline!");
    };

    protected String color(final String d)
    {
        return ChatColor.translateAlternateColorCodes('&', d);
    };

    protected void print(final String d)
    {
        System.out.println("(Break Restrict): " + d);
    };
};
