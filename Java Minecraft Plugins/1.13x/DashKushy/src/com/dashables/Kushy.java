
// Author: Dashie
// Version: 1.0

package com.dashables;


import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.command.CommandExecutor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.plugin.java.JavaPlugin;


public class Kushy extends JavaPlugin implements Listener, CommandExecutor
{   
    private String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };    
    
    private void print(String str)
    {
        System.out.println("(Dash Kush): " + str);
    };
    
    private FileConfiguration config = (FileConfiguration) null;
    private JavaPlugin plugin = (JavaPlugin) null;
    
    @Override public void onEnable()
    {
        print("Plugin is being enabled.");
        
        config = (FileConfiguration) getConfig();
        plugin = (JavaPlugin) this;
        
        LoadConfiguration();
        
        for ( int key = 0; key < dashables.size(); key += 1 )
        {
            dashables.get(key).getItemMeta().setLore
            (
                Arrays.asList
                (
                    new String[] 
                    { 
                        color
                        (
                            dashables_lores[key]
                        ) 
                    }
                )
            );
            
            dashables.get(key).getItemMeta().setDisplayName
            (
                color
                (
                    dashables_names[key]
                )
            );
        };
        
        getServer().getPluginManager().registerEvents(this, this);
        getCommand("dashkush").setExecutor(this);
        
        print("Plugin has been enabled.");
    };
    
    private String admin_permission, consume_permission;
    
    private void LoadConfiguration()
    {
        plugin.reloadConfig();
        config = plugin.getConfig();
        
        consume_permission = config.getString("properties.permissions.consume-permission");
        admin_permission = config.getString("properties.permissions.admin-permission");
    };
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
    
    private final List<ItemStack> dashables = Arrays.asList
    (
        new ItemStack[]
        {
            new ItemStack(Material.INK_SAC, 1, (short)2),// Dash Kush
            new ItemStack(Material.INK_SAC, 1, (short)2),// White Widow
            new ItemStack(Material.INK_SAC, 1, (short)2),// Buddha Kush
            new ItemStack(Material.INK_SAC, 1, (short)2),// Pineapple Express
            new ItemStack(Material.INK_SAC, 1, (short)2),// American Pie
            new ItemStack(Material.INK_SAC, 1, (short)2),// OG Kush
        }
    );
    
    private final String[] dashables_lores = new String[]
    {
        "&a&oMy self grown strain of Kush.\r\n&c&o23% THC - 73% Sativa",
        "&f&oWhite Widow will give you a good time.\r\n&c&o17% THC - 43% Indica",
        "&6&oBe at peace with yourself and the universe.\r\n&c&o15% THC - 94% Indica",
        "&e&oDefinitely an intense high it is!\r\n&c&o27% THC - 57% Sativa",
        "&a&oI mean, it is sweet and gets you high.\r\n&c&o20% THC - 88% Indica",
        "&9&oA classic high.\r\n&c&o24% THC - 38% Sativa",
    };
    
    private final String[] dashables_names = new String[]
    {
        "&d&lD&5&la&d&ls&5&lh &a&lKush",
        "&f&lWhite &7&lWidow",
        "&6Buddha &a&lKush",
        "&e&lPineapple &6&lExpress",
        "&9&lAmerican &f&lP&4&li&f&le",
        "&c&lO.G. &a&lKush",
    };
    
    @EventHandler private void PlayerEatEvent(PlayerInteractEvent e)
    {
        ItemStack item = (ItemStack) e.getItem();        
        
        if (!dashables.contains(item))
        {
            return;
        }
        
        else if (dashables.equals(dashables.get(0)))
        {
            
        }
        
        else if (dashables.equals(dashables.get(1)))
        {
            
        }
        
        else if (dashables.equals(dashables.get(2)))
        {
            
        }

        else if (dashables.equals(dashables.get(3)))
        {
            
        }

        else if (dashables.equals(dashables.get(4)))
        {
            
        }

        else if (dashables.equals(dashables.get(5)))
        {
            
        };
    };
};
