// Author: Dashie
// Version: 1.0

package com.kvinnekraft;

import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.inventory.ItemStack;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;

import java.util.ArrayList;
import java.util.List;

public final class PotionObtainablesX extends JavaPlugin
{
    final List<PotionEffect> effects = new ArrayList<>();
    final List<ItemStack> items = new ArrayList<>();

    final List<String> permissions = new ArrayList<>();
    final List<String> messages = new ArrayList<>();
    final List<String> commands = new ArrayList<>();

    private void loadSettings()
    {
        saveDefaultConfig();

        try
        {
            final FileConfiguration config = getConfig();

            for (int k = 0; ;k += 1)
            {
                String node = "core-tweaks.potion-items.";

                if (config.contains(node + "1"))
                {
                    node = node + k;



                    continue;
                }

                break;
            }
        }

        catch (final Exception e)
        {
            shutdownPlugin("Invalid configuration detected.");
        }
    }

    final JavaPlugin plugin = this;

    boolean autoReload = true;
    int reloadInterval = 5;

    @Override
    public final void onEnable()
    {
        try
        {
            final FileConfiguration config = getConfig();

            autoReload = config.getBoolean("startup-tweaks.auto-reload");

            if (autoReload)
            {
                reloadInterval = config.getInt("startup-tweaks.reload-interval") * 20;

                getServer().getScheduler().runTaskTimerAsynchronously
                (
                    plugin,

                    this::loadSettings,

                    reloadInterval,
                    reloadInterval
                );
            }

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
        System.out.println("(No Store X): " + data);
    }
}