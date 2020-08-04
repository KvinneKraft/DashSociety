
// Author: Dashie
// Version: 1.0

package com.kvinnekraft;

import org.bukkit.ChatColor;
import org.bukkit.Sound;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Entity;
import org.bukkit.entity.EntityType;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.EntityDeathEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;

public final class MoreDrops extends JavaPlugin
{
    FileConfiguration config;
    JavaPlugin plugin;

    @Override public void onEnable()
    {
        print("I am getting up, hold on a second ....");

        LoadConfiguration();

        print("Author: Dashie");
        print("Version: 1.0");
        print("Email: KvinneKraft@protonmail.com");
        print("Github: https://github.com/KvinneKraft");

        getServer().getPluginManager().registerEvents(new Events(), plugin);
        getCommand("moredrops").setExecutor(new Commands());

        print("You have enabled me!");
    };

    protected final List<List<PotionEffect>> potion_effects = new  ArrayList<>();
    protected final List<List<ItemStack>> mob_drops = new ArrayList<>();
    protected final List<List<Sound>> sound_effects = new ArrayList<>();
    protected final List<String> mob_types = new ArrayList<>();

    protected final void LoadConfiguration()
    {
        saveDefaultConfig();

        if (plugin != this)
        {
            plugin = this;
        };

        plugin.reloadConfig();
        config = plugin.getConfig();

        try
        {
            for (int k = 1 ; ; k += 1)
            {
                if (config.contains("mob-drops." + k))
                {
                    String node = "mob-drops." + k + ".";

                    try
                    {
                        final String mob_type = config.getString(node + "mob-type").toUpperCase().replace(" ", "_");
                        final EntityType entity = EntityType.valueOf(mob_type);

                        mob_types.add(entity.toString());
                    }

                    catch (final Exception e)
                    {
                        print("An invalid mob type was found at " + node + "mob-type !");
                        continue;
                    };

                    final List<Sound> sounds = new ArrayList<>();

                    for (final String data : config.getStringList(node + "sound-effects"))
                    {
                        try
                        {
                            Sound sound = Sound.valueOf(data);
                            sounds.add(sound);
                        }

                        catch (final Exception e)
                        {
                            print("An invalid sound effect was found at " + node + "sound-effects -> " + data + " !");
                        };
                    };

                    sound_effects.add(sounds);

                    final List<PotionEffect> potions = new ArrayList<>();

                    for (final String data : config.getStringList(node + "potion-effects"))
                    {
                        try
                        {
                            final List<String> components = Arrays.asList(data.toUpperCase().split(" "));
                            final PotionEffectType type = PotionEffectType.getByName(components.get(0));

                            final int intensity = Integer.parseInt(components.get(2));
                            final int span = Integer.parseInt(components.get(1)) * 20;

                            final PotionEffect effect = new PotionEffect(type, span, intensity);

                            potions.add(effect);
                        }

                        catch (final Exception e)
                        {
                            print("An invalid potion effect was found at " + node + "potion-effects -> " + data + " !");
                        };
                    };

                    potion_effects.add(potions);

                    continue;
                };

                // Item Drops

                break;
            }
        }

        catch (final Exception e)
        {
            print("An unknown error has occurred, perhaps send the following to me at KvinneKraft@protonmail.com if you want to get this issue fixed!\n" + e.getMessage() + "\n");
        };
    };

    protected final class Events implements Listener
    {
        @EventHandler protected final void onEntityDeath(final EntityDeathEvent e)
        {
            final Entity entity = e.getEntity();

            if (mob_types.contains(entity.getType().toString()))
            {
                final Integer i = mob_types.indexOf(entity.getType().toString());

                if (potion_effects.get(i).size() > 1)
                {

                };

                if (mob_drops.get(i).size() > 1)
                {

                };

                if (sound_effects.get(i).size() > 1)
                {

                };
            };
        };
    };

    protected final class Commands implements CommandExecutor
    {
        @Override public final boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
        {
            return false;
        };
    };

    @Override public void onDisable()
    {
        if (getServer().getScheduler().getActiveWorkers().size() > 0)
        {
            getServer().getScheduler().cancelTasks(plugin);
        };

        print("Aw, you have disabled me!");
    };

    protected final String color(final String data)
    {
        return ChatColor.translateAlternateColorCodes('&', data);
    };

    protected final void print(final String data)
    {
        System.out.println("(More Drops): " + data);
    };
};

