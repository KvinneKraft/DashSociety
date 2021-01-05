// Author: Dashie
// Version: 1.0

package com.kvinnekraft;

import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.plugin.java.JavaPlugin;

public final class QuickEatX extends JavaPlugin
{
    private void loadSettings()
    {
        saveDefaultConfig();

        try
        {

        }

        catch (final Exception e)
        {
            shutdownPlugin("Invalid configuration detected.");
        }
    }

    final JavaPlugin plugin = this;

    @Override
    public final void onEnable()
    {
        try
        {
            final FileConfiguration config = getConfig();

            // Insta Drink
            // Insta Eat
            // Next up: Dash Captcha

            loadSettings();
        }

        catch (final Exception e)
        {
            shutdownPlugin("The plugin failed to initialize.  Shutting down ....");
        }

        print("Author: Dashie");
        print("Version: 1.0");
        print("Github: https://github.com/KvinneKraft");
        print("Email: KvinneKraft@protonmail.com");
    }

    private void shutdownPlugin(final String reason)
    {
        print(reason);
        getServer().getPluginManager().disablePlugin(plugin);
    }

    @Override
    public final void onDisable()
    {
        getServer().getScheduler().cancelTasks(plugin);
        print("I am dead!");
    }

    private String color(final String data)
    {
        return ChatColor.translateAlternateColorCodes('&', data);
    }

    private void print(final String data)
    {
        System.out.println("(QuickEatX): " + data);
    }
}