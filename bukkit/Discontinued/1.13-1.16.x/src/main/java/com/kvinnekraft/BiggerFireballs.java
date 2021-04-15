package com.kvinnekraft;

import org.bukkit.Bukkit;
import org.bukkit.Material;
import org.bukkit.event.Listener;
import org.bukkit.inventory.Inventory;
import org.bukkit.plugin.java.JavaPlugin;

import java.util.Arrays;
import java.util.List;

public final class BiggerFireballs extends JavaPlugin implements Listener
{
    @Override
    public void onEnable()
    {
        getServer().getPluginManager().registerEvents(this, this);

        final Inventory inv = Bukkit.createInventory(null, 27, "Inventory");

        final List<Material> items = Arrays.asList
        (
            Material.DIAMOND_HELMET,
            Material.DIAMOND_CHESTPLATE,
            Material.DIAMOND_LEGGINGS,
            Material.DIAMOND_BOOTS
        );

        final int[] ids = new int[] { 9, 9, 9, 9 };
    }

    @Override
    public void onDisable() {
        // Plugin shutdown logic
    }
}
