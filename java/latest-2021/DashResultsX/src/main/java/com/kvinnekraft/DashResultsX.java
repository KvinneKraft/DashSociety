package com.kvinnekraft;

import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.NamespacedKey;
import org.bukkit.block.Furnace;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.enchantments.Enchantment;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.enchantment.EnchantItemEvent;
import org.bukkit.event.inventory.*;
import org.bukkit.inventory.*;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;

import java.util.*;

public class DashResultsX extends JavaPlugin
{
    final JavaPlugin plugin = this;

    final void ShutdownPlugin(String why)
    {
        getServer().getPluginManager().disablePlugin(plugin);
        print(why);
    }

    // ArrayList.size() < 1 == EMPTY -> Disabled
    final List<PotionEffect> BrewingStandPotions = new ArrayList<>();
    final List<Material> CraftingTableItems = new ArrayList<>();
    final List<Material> FurnaceOutputItems = new ArrayList<>();
    final List<String> ItemRenameNames = new ArrayList<>();

    final Map<Enchantment, Integer> Enchantments = new HashMap<>();

    final void loadSettings()
    {
        final FileConfiguration config = plugin.getConfig();

        try
        {
            final List<String> nodes = Arrays.asList("brewing-stand", "crafting-table", "anvil-rename", "enchantment-table", "furnace-output");

            for (final String node : nodes)
            {
                if (!config.getBoolean(node + ".disable-" + node))
                {
                    continue;
                }

                for (final String material : config.getStringList(node + ".prevent"))
                {
                    try
                    {
                        final String[] part = material.split(":");

                        switch (nodes.indexOf(node) + 1)
                        {
                            case 1:
                                final PotionEffect potion = new PotionEffect(PotionEffectType.getByName(part[0]), 10, Integer.valueOf(part[1]));
                                BrewingStandPotions.add(potion);
                                break;
                            case 2:
                                final Material itemthing = Material.valueOf(part[0]);
                                CraftingTableItems.add(itemthing);
                                break;
                            case 3:
                                final Enchantment enchantment = Enchantment.getByKey(NamespacedKey.minecraft(part[0]));
                                Enchantments.put(enchantment, Integer.valueOf(part[1]));
                                break;
                            case 4:
                                final String message = part[0].toLowerCase();
                                ItemRenameNames.add(message);
                                break;
                            case 5:
                                final Material otheritem = Material.valueOf(part[0]);
                                FurnaceOutputItems.add(otheritem);
                                break;
                        }
                    }

                    catch (final Exception e)
                    {
                        print("Invalid node: " + node + ".prevent -> " + material + " ! Skipping ....");
                    }
                }
            }
        }

        catch (final Exception e)
        {
            ShutdownPlugin("An error has occurred while initializing settings. (Internally)");
        }
    }

    public final class EventListener implements Listener
    {
        @EventHandler public final void onPlayerCraft(final CraftItemEvent e)
        {
            final Player p = (Player) e.getViewers().get(0);

            if (CraftingTableItems.size() > 0 && !p.isOp())
            {
                final CraftingInventory inventory = e.getInventory();

                if (inventory.getResult() != null)
                {
                    if (CraftingTableItems.contains(inventory.getResult().getType()))
                    {
                        p.sendMessage(color("&cYou may not craft this!"));
                        e.setCancelled(true);
                    }
                }
            }
        }

        @EventHandler public final void onPlayerEnchant(final EnchantItemEvent e)
        {
            final Player p = (Player) e.getViewers().get(0);

            if (Enchantments.size() > 0 && !p.isOp())
            {
                //
            }
        }

        @EventHandler public final void onPlayerAnvil(final PrepareAnvilEvent e)
        {
            final Player p = (Player) e.getViewers().get(0);

            if (ItemRenameNames.size() > 0 && !p.isOp())
            {
                //
            }
        }

        @EventHandler public final void onPlayerCook(final InventoryClickEvent e)
        {
            if (!(e.getViewers().get(0) instanceof Player))
            {
                return;
            }

            final Inventory inventory = e.getClickedInventory();
            final Player p = (Player) e.getViewers().get(0);

            if (inventory instanceof FurnaceInventory)
            {
                if (FurnaceOutputItems.size() > 0)
                {
                    final FurnaceInventory furnace = (FurnaceInventory) inventory;

                    if (furnace.getResult() != null)
                    {
                        final Material material = furnace.getResult().getType();

                        if (FurnaceOutputItems.contains(material))
                        {
                            p.sendMessage(color("&cYou may not put this in here!"));
                            p.closeInventory();

                            e.setCancelled(true);
                        }
                    }
                }
            }

            else if (inventory instanceof BrewerInventory)
            {
                if (BrewingStandPotions.size() > 0 && !p.isOp())
                {
                    // Check Potion in Inventory <---
                }
            }
        }
    }

    boolean mayAutoReload = true, opsBypass = true;
    Integer reloadInterval = 5;

    @Override public final void onEnable()
    {
        try
        {
            final FileConfiguration config = plugin.getConfig();

            mayAutoReload = config.getBoolean("properties.auto-reload");

            if (mayAutoReload)
            {
                try
                {
                    reloadInterval = config.getInt("properties.reload-interval") * 20;

                    getServer().getScheduler().runTaskTimerAsynchronously
                    (
                        plugin,

                        this::loadSettings,

                        reloadInterval,
                        reloadInterval
                    );

                    print("Auto-Reload is now running asynchronously ....");
                }

                catch (final Exception e)
                {
                    ShutdownPlugin("An error has occurred while auto-loading your configuration. Shutting down....");
                }
            }

            else
            {
                loadSettings();
            }
        }

        catch (final Exception e)
        {
            ShutdownPlugin("An error has occurred whilst trying to initialize the plugin.  Shutting down....");
        }

        print("Author: Dashie");
        print("Version: 1.0");
        print("Github: https://github.com/KvinneKraft");
        print("Email: KvinneKraft@protonmail.com");
    }

    @Override public final void onDisable()
    {
        getServer().getScheduler().cancelTasks(plugin);
        print("I am dead!");
    }

    final String color(String data)
    {
        return ChatColor.translateAlternateColorCodes('&', data);
    }

    final void print(String data)
    {
        System.out.println("(Dash Results X): " + data);
    }
}