
// Author: Dashie
// Version: 1.0


package com.dashcraft;


import java.util.List;
import org.bukkit.Sound;
import org.bukkit.Effect;
import org.bukkit.Location;
import org.bukkit.Material;
import java.util.ArrayList;
import org.bukkit.ChatColor;
import org.bukkit.entity.Player;
import org.bukkit.event.Listener;
import org.bukkit.command.Command;
import org.bukkit.event.EventHandler;
import org.bukkit.inventory.ItemStack;
import org.bukkit.command.CommandSender;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.command.CommandExecutor;
import org.bukkit.event.inventory.CraftItemEvent;
import org.bukkit.configuration.file.FileConfiguration;;


public class CraftBlocker extends JavaPlugin
{
    public FileConfiguration config = getConfig();
    public JavaPlugin plugin = this;
    
    Moony moon = new Moony();
        
    CommandsHandler commands = new CommandsHandler();
    EventsHandler events = new EventsHandler();
   
    @Override
    public void onEnable()
    {
        moon.print("Loading Dash Craft Blocker 1.0 ....");
        
        saveDefaultConfig();
        reloadPlugin();
        
        moon.print("Author: Dashie");
        moon.print("Version: 1.0");
        moon.print("Email: KvinneKraft@protonmail.com");
        
        moon.print("Dash Craft Blocker 1.0 has been loaded!");
    };
    
    class CommandsHandler implements CommandExecutor
    {
        boolean t = true, f = false;
        
        public String access_denied_message, correct_usage_message, admin_permission;
        
        public String reloading_message = moon.transstr("&aReloading Dash Craft Blocker ....");
        public String reloaded_message = moon.transstr("&aDash Craft Blocker has been reloaded!");
        
        public String invalid_material_message = moon.transstr("&cThat is not a valid material.");
        
        public String it_already_exists = moon.transstr("&cThis item already exists in the list.");
        public String it_does_not_exist = moon.transstr("&cThis item is not in the list.");
        
        public String added_message = moon.transstr("&aAdded item to the list");
        public String delet_message = moon.transstr("&aDeleted item from the list.");
        
        // dashcraft [add | del | reload]
        
        @Override
        public boolean onCommand(CommandSender s, Command c, String a, String[] as)
        {
            if(!(s instanceof Player))
                return f;
            
            Player p = (Player) s;
            
            if(!p.hasPermission(admin_permission))
            {
                p.sendMessage(access_denied_message);
                return f;
            }
            
            a = as[0].toLowerCase();
            
            if(as.length < 1)
            {
                p.sendMessage(correct_usage_message);
            }
            
            else if((a.equals("add")) || (a.equals("del")))
            {
                if(as.length < 2)
                {
                    p.sendMessage(correct_usage_message);
                    return f;
                };
                
                Material material = Material.getMaterial(as[1].toUpperCase());
                
                if(material == null)
                {
                    p.sendMessage(invalid_material_message);
                    return f;
                }
                
                else if(events.craft_blacklist.contains(material))
                {
                    if(a.equals("del"))
                    {
                        events.craft_blacklist.remove(material);
                        p.sendMessage(delet_message);
                    }
                    
                    else
                    {
                        p.sendMessage(it_already_exists);
                    };
                }
                
                else
                {
                    if(a.equals("add"))
                    {
                        events.craft_blacklist.add(material);
                        p.sendMessage(added_message);
                    }
                    
                    else
                    {
                        p.sendMessage(it_does_not_exist);
                    };
                };
                
                config.set("item-blacklist", events.craft_blacklist);
                plugin.saveConfig();
            }
            
            else if(a.equals("reload"))
            {
                p.sendMessage(reloading_message);
                
                reloadPlugin();
                
                p.sendMessage(reloaded_message);
            }
            
            else
            {
                p.sendMessage(correct_usage_message);
            };
            
            return true;
        };
    };
    
    private void reloadPlugin()
    {
        plugin.reloadConfig();
        config = plugin.getConfig();
        
        events.are_particle_effects = config.getBoolean("blocked-action.particle-effect-settings.enabled");
        
        if(events.are_particle_effects)
        {
            events.particle = Effect.valueOf(config.getString("blocked-action.particle-effect-settings.particle-effect"));
            
            if(events.particle == null)
            {
                moon.print("Invalid particle effect specified!");
                events.are_particle_effects = false;
            };
        };
        
        events.are_sound_effects = config.getBoolean("blocked-action.particle-effect-settings.enabled");    
        
        if(events.are_sound_effects)
        {
            events.sound = Sound.valueOf(config.getString("blocked-action.sound-effect-settings.sound-type"));
            
            if(events.sound == null)
            {
                moon.print("Invalid sound effect specified!");
                events.are_sound_effects = false;
            };
        };
        
        events.block_message = moon.transstr(config.getString("blocked-action.message"));        
        
        if(events.craft_blacklist.size() > 0)
        {
            events.craft_blacklist.clear();
        };
        
        for(String mat : config.getStringList("item-blacklist"))
        {
            Material material = Material.getMaterial(mat);
            
            if(material == null)
            {
                moon.print("The material " + mat + " is invalid. Skipping ....");
                continue;
            };
            
            events.craft_blacklist.add(material);
        };
    };
    
    class EventsHandler implements Listener
    {
        public List<Material> craft_blacklist = new ArrayList<>();
        
        public boolean are_sound_effects, are_particle_effects;
        
        public String block_message;
        public Effect particle;        
        public Sound sound;
        
        @EventHandler
        public void onCraft(CraftItemEvent e)
        {
            if(!(e.getWhoClicked() instanceof Player))
                return;
            
            Player p = (Player) e.getWhoClicked();            
            
            if(p.hasPermission(commands.admin_permission))
                return;
            
            if(e.getRecipe().getResult() != null)
            {
                ItemStack item = e.getRecipe().getResult();
                
                if(craft_blacklist.contains(item.getType()))
                {
                    item.setType(Material.AIR);
                    
                    Location p_loc = p.getLocation();
                    
                    if(are_sound_effects)
                    {
                        p_loc.getWorld().playSound(p_loc, sound, 0, 0);
                    };
                    
                    if(are_particle_effects)
                    {
                        p_loc.getWorld().playEffect(p_loc, particle, 40);
                    };
                    
                    p.sendTitle("", block_message, 0, 0, 0);
                    
                    e.setCancelled(true);
                };
            };
        };
    };
    
    @Override
    public void onDisable()
    {
        moon.print("Dash Craft Blocker 1.0 has been disabled.");
    };
    
    class Moony
    {
        public void print(String str)
        {
            System.out.println("(Dash Craft Block): " + str);
        };
        
        public String transstr(String str)
        {
            return ChatColor.translateAlternateColorCodes('&', str);
        };
    };
};
