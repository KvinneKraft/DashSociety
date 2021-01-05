// Author: Dashie
// Version: 1.0

package com.kvinnekraft;

import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.NamespacedKey;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.enchantments.Enchantment;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;

import javax.xml.stream.events.Namespace;
import java.util.ArrayList;
import java.util.List;

public final class PotionObtainablesX extends JavaPlugin
{
    class EventListener implements Listener
    {
        @EventHandler
        final void onItemInteract(final PlayerInteractEvent e)
        {
            final Player p = e.getPlayer();

            if (e.getItem() != null && items.contains(e.getItem()))
            {
                final int index = items.indexOf(e.getItem());

                if (!permissions.get(index).equalsIgnoreCase("none"))
                {
                    if (!p.hasPermission(permissions.get(index)))
                    {
                        return;
                    }
                }

                if (!messages.get(index).equalsIgnoreCase("none"))
                {
                    p.sendMessage(messages.get(index));
                }

                if (effects.get(index).size() > 0)
                {
                    p.addPotionEffects(effects.get(index));
                }

                if (commands.get(index).size() > 0)
                {
                    getServer().getScheduler().runTaskAsynchronously
                    (
                        plugin,

                        () ->
                        {
                            for (String command : commands.get(index))
                            {
                                if (command.length() > 1)
                                {
                                    command = command.replace("%p%", p.getName());

                                    if (command.charAt(0) == '/')
                                    {
                                        command = command.substring(1);
                                        p.performCommand(command);
                                    }

                                    else if (command.charAt(0) == '~')
                                    {
                                        command = command.substring(1);
                                        getServer().dispatchCommand(getServer().getConsoleSender(), command);
                                    }
                                }
                            }
                        }
                    );
                }
            }
        }
    }

    private PotionEffectType getPotionEffectType(final String data)
    {
        return (PotionEffectType.getByName(data));
    }

    private Enchantment getEnchantment(final String data)
    {
        return (Enchantment.getByKey(NamespacedKey.minecraft(data)));
    }

    private Integer getInteger(final String data)
    {
        try
        {
            return Integer.valueOf(data);
        }

        catch (final Exception e)
        {
            return -1;
        }
    }

    final List<List<PotionEffect>> effects = new ArrayList<>();
    final List<List<String>> commands = new ArrayList<>();

    final List<ItemStack> items = new ArrayList<>();

    final List<String> permissions = new ArrayList<>();
    final List<String> messages = new ArrayList<>();
    final List<String> names = new ArrayList<>();

    private void loadSettings()
    {
        saveDefaultConfig();

        try
        {
            final FileConfiguration config = getConfig();

            effects.clear();
            items.clear();
            commands.clear();
            permissions.clear();
            messages.clear();
            names.clear();

            for (int k = 1; ;k += 1)
            {
                String node = "core-tweaks.potion-items.";

                if (config.contains(node + k))
                {
                    node = node + k;

                    try
                    {
                        try
                        {
                            final ItemStack item = new ItemStack(Material.AIR, 1);
                            final ItemMeta meta = item.getItemMeta();

                            try
                            {
                                final String name = color(config.getString(node + ".item.name"));
                                names.add(ChatColor.stripColor(name.toLowerCase()));

                                meta.setDisplayName(name);

                                final List<String> lore = new ArrayList<>();

                                for (final String line : config.getStringList(node + ".item.lore"))
                                {
                                    lore.add(color(line));
                                }

                                meta.setLore(lore);

                                item.setType(Material.valueOf(config.getString(node + ".item.type").toUpperCase().replace(" ", "_")));
                            }

                            catch (final Exception e)
                            {
                                throw new Exception("type || name || lore");
                            }

                            try
                            {//add in another try-catch to allow the correct enchantments to be added regardless.
                                for (final String line : config.getStringList(node + ".item.enchantments"))
                                {
                                    final String[] enchantmentData = line.split(" ");

                                    final Enchantment enchantment = getEnchantment(enchantmentData[0]);
                                    final Integer level = getInteger(enchantmentData[1]);

                                    if (level < 1 || enchantment == null)
                                    {
                                        throw new Exception("!");
                                    }

                                    item.addUnsafeEnchantment(enchantment, level);
                                }
                            }

                            catch (final Exception e)
                            {
                                throw new Exception("enchantments");
                            }

                            item.setItemMeta(meta);

                            items.add(item);
                        }

                        catch (final Exception e)
                        {
                            throw new Exception(node + ".item." + e.getMessage());
                        }

                        try
                        {
                            permissions.add(config.getString(node + ".consumption.permission"));
                            messages.add(color(config.getString(node + ".consumption.message")));

                            try
                            {
                                final List<PotionEffect> effects = new ArrayList<>();

                                for (final String line : config.getStringList(node + ".consumption.effects"))
                                {
                                    final String[] effectData = line.split(" ");

                                    final PotionEffectType effect = getPotionEffectType(effectData[0]);
                                    final Integer amplifier = getInteger(effectData[1]);
                                    final Integer duration = getInteger(effectData[2]);

                                    if (effect == null || amplifier < 1 || duration < 1)
                                    {
                                        throw new Exception("!");
                                    }

                                    effects.add(new PotionEffect(effect, duration * 20, amplifier));
                                }

                                this.effects.add(effects);
                            }

                            catch (final Exception e)
                            {
                                throw new Exception("effects");
                            }

                            commands.add(config.getStringList(node + ".consumption.commands"));
                        }

                        catch (final Exception e)
                        {
                            throw new Exception(node + ".consumption." + e.getMessage());
                        }
                    }

                    catch (final Exception e)
                    {
                        print("Invalid configuration found at: " + e.getMessage() + " Skipping....");
                    }

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
        System.out.println("(Potion Obtainables X): " + data);
    }
}