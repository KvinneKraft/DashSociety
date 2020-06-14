
// Author: Dashie
// Version: 1.0

package dash.recoded;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Random;
import org.bukkit.Bukkit;
import org.bukkit.ChatColor;
import org.bukkit.Color;
import org.bukkit.FireworkEffect;
import org.bukkit.Material;
import org.bukkit.Sound;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.inventory.InventoryClickEvent;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.inventory.Inventory;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;

public class Captcha extends JavaPlugin
{
    FileConfiguration config = null;
    JavaPlugin plugin = null;
    
    protected static class Fireworks 
    {       
        static boolean do_fireworks, random_firework_types, random_firework_color;        
        
        static final List<FireworkEffect.Type> firework_types = new ArrayList<>();       
        static final List<Color> rgb_combinations = new ArrayList<>();   
        
        static String permission;
    };

    protected static class Sounds 
    {
        static boolean do_completion_sound = false;
       
        static String permission;
        static Sound completion_sound;
    };
    
    protected static class Lightning
    {
        static boolean do_lightning = false;
    };
    
    protected static class Mechanism
    {
        protected static class  Security
        {
            static Integer maximum_attempts, attempt_timeout;
            static boolean lock_ip_address;
            
            protected static class Restrictions
            {
                static boolean disable_chat, disable_movement, disable_inventory_interaction, disable_damage, prevent_kill_aura;
            };
            
            protected static class PotionEffects
            {
                static final List<PotionEffect> potion_effects = new ArrayList<>();
                static boolean apply_potion_effects;
            };
        };
        
        protected static class Interface
        {
            static String title;
            
            protected static class NormalItems
            {
                static final List<ItemStack> items = new ArrayList<>();                
                static final List<String> lore = new ArrayList<>();
                static String display_name;
            };
            
            protected static class KeyItems
            {
                static final List<ItemStack> items = new ArrayList<>();                
                static final List<String> lore = new ArrayList<>();
                static String display_name;                
            };
        };
    };
    
    void LoadConfiguration()
    {
        saveDefaultConfig();
        
        if (plugin == null)
            plugin = (JavaPlugin) this;
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
    
        Lightning.do_lightning = config.getBoolean("modules.lightning-switch");
        
        final Exception error = new Exception("error");
        final Exception skip = new Exception("skip");
        
        try /*Firework Module*/
        {
            Fireworks.do_fireworks = config.getBoolean("modules.fireworks.enabled");
            
            if (!Fireworks.do_fireworks)
            {
                throw skip;
            };
            
            Fireworks.random_firework_types = config.getBoolean("modules.fireworks.effect-randomizer");
            
            if (!Fireworks.random_firework_types) 
            {
                for (final String type : config.getStringList("modules.fireworks.effects"))
                {
                    final String arr[] = type.replace(" ", "").toUpperCase().split(",");
                    
                    try
                    {
                        for (final String suspect : arr)
                        {
                            final FireworkEffect.Type buff = FireworkEffect.Type.valueOf(suspect);
                            
                            if (type == null)
                            {
                                throw error;
                            };
                            
                            Fireworks.firework_types.add(buff);
                        };
                    }
                    
                    catch (final Exception e)
                    {
                        print("Invalid effect type settings found. Skipping ....");
                    };
                };
                
                if (Fireworks.firework_types.size() < 1)
                {
                    print("Insufficient Firework Types found, using randomizer!");
                    Fireworks.random_firework_types = true;
                };
            };
            
            Fireworks.random_firework_color = config.getBoolean("modules.fireworks.rgb-randomizer");
            
            if (!Fireworks.random_firework_color)
            {
                for (final String code : config.getStringList("modules.fireworks.rbg-colours"))
                {
                    final String arr[] = code.replace(" ", "").split(",");
     
                    try
                    {
                        if (arr.length < 3)
                        {
                            print("Invalid R.G.B. colour codes found. Skipping ....");
                            continue;
                        };
                        
                        final int[] rgb = {255,255,255};
                        
                        for (int i = 0; i < 3; i += 1)
                            rgb[i] = Integer.parseInt(arr[i]);
                        
                        Fireworks.rgb_combinations.add
                        (
                            Color.fromRGB
                            (
                                rgb[0], 
                                rgb[1], 
                                rgb[2]
                            )
                        );
                    }
                    
                    catch (final Exception e)
                    {
                        print("Invalid R.G.B. colour format has been found in the configuration file. Skipping ....");
                    };
                };
                
                if (Fireworks.rgb_combinations.size() < 1)
                {
                    print("Insufficient R.G.B. combinations found, using randomizer!");
                    Fireworks.random_firework_color = true;
                };
            };
            
            Fireworks.permission = config.getString("modules.fireworks.permission");
        }
        
        catch (final Exception e)
        {
            if (!e.toString().equals("skip"))
            {
                print("Invalid fireworks settings were detected in the configuration file. Disabling this module!");
                Fireworks.do_fireworks = false;
            };
        }; /* End of Firework Module */
        
        try /*Sound Module*/
        {
            Sounds.do_completion_sound = config.getBoolean("modules.sounds.enabled");

            if (Sounds.do_completion_sound)
            {
                Sounds.completion_sound = Sound.valueOf(config.getString("modules.sounds.completion-sound"));

                if (Sounds.completion_sound == null)
                {
                    throw error;
                };
                
                Sounds.permission = config.getString("modules.sounds.permission");
            };
        }
        
        catch (final Exception e)
        {
            print("Invalid sound has been found in the configuration file. Using the default one!");
            Sounds.completion_sound = Sound.ENTITY_PLAYER_LEVELUP;
        }; /* End of Sound Module */   
        
        try /*Mechanism*/
        {
            try /*Security*/
            {
                Mechanism.Security.maximum_attempts = config.getInt("mechanism.maximum-attempts");                    
                Mechanism.Security.attempt_timeout = config.getInt("mechanism.attempt-timeout");

                Mechanism.Security.lock_ip_address = config.getBoolean("mechanism.lock-ip-address");                
                
                try /*Restrictions*/
                {
                    Mechanism.Security.Restrictions.disable_chat = config.getBoolean("mechanism.security.restrictions.disable-chat");
                    Mechanism.Security.Restrictions.disable_movement = config.getBoolean("mechanism.security.restrictions.disable-movement");
                    Mechanism.Security.Restrictions.disable_inventory_interaction = config.getBoolean("mechanism.security.restrictions.disable-inventory-interaction");
                    Mechanism.Security.Restrictions.disable_damage = config.getBoolean("mechanism.security.restrictions.disable-damage");                    
                    Mechanism.Security.Restrictions.prevent_kill_aura = config.getBoolean("mechanism.security.restrictions.prevent-kill-aura");
                }
                
                catch (final Exception e)
                {
                    throw error;
                }; /*End of Restrictions*/
                
                try /*Potions*/
                {
                    Mechanism.Security.PotionEffects.apply_potion_effects = config.getBoolean("mechanism.security.potion-appliance.enabled");
                    
                    if (Mechanism.Security.PotionEffects.apply_potion_effects)
                    {
                        if (Mechanism.Security.PotionEffects.potion_effects.size() >= 1)
                        {
                            Mechanism.Security.PotionEffects.potion_effects.clear();
                        };
                        
                        for (final String effect : config.getStringList("mechanism.security.potion-appliance.effects"))
                        {
                            final PotionEffect finalized = new PotionEffect(PotionEffectType.getByName(effect), 99999, 1);
                            
                            if (finalized == null)
                            {
                                throw error;
                            };
                            
                            Mechanism.Security.PotionEffects.potion_effects.add(finalized);
                        };
                    };
                }
                
                catch (final Exception e)
                {
                    print("Invalid settings have been found in the potion-appliance section of the plugin.yml. Disabling this feature ....");
                    Mechanism.Security.PotionEffects.apply_potion_effects = false;
                }; /*End of Potions*/                
            }
            
            catch (final Exception e)
            {
                print("There was an error in the security part of the config.yml. Certain features may not function as expected!");
            }; /*End of Security*/
            
            try /*Interface*/
            {
                Mechanism.Interface.title = color(config.getString("mechanism.interface.title"));
                
                Mechanism.Interface.NormalItems.items.clear();
                Mechanism.Interface.NormalItems.lore.clear();
                
                Mechanism.Interface.NormalItems.display_name = color(config.getString("mechanism.interface.none-key-items.display-name"));                
                Mechanism.Interface.NormalItems.lore.add(color(config.getString("mechanism.interface.none-key-items.lore")));                
                
                for (final String item : config.getStringList("mechanism.interface.none-key-items.items"))
                {
                    try
                    {
                        final ItemStack substance = new ItemStack(Material.getMaterial(item), 1);
                        
                        if (substance == null)
                        {
                            throw error;
                        };
                        
                        final ItemMeta meta = (ItemMeta) substance.getItemMeta();

                        meta.setDisplayName(Mechanism.Interface.NormalItems.display_name);
                        meta.setLore(Mechanism.Interface.NormalItems.lore);

                        substance.setItemMeta(meta);
                        
                        Mechanism.Interface.NormalItems.items.add(substance);
                    }
                    
                    catch (final Exception e)
                    {
                        print("There was an invalid item found in the none-key-items section in the config.yml! Skipping ....");
                    };
                };                
                
                Mechanism.Interface.KeyItems.items.clear();
                Mechanism.Interface.KeyItems.lore.clear();
                
                Mechanism.Interface.KeyItems.display_name = color(config.getString("mechanism.interface.key-items.display-name"));                
                Mechanism.Interface.KeyItems.lore.add(color(config.getString("mechanism.interface.key-items.lore")));
                
                for (final String item : config.getStringList("mechanism.interface.key-items.items"))
                {
                    try
                    {
                        final ItemStack substance = new ItemStack(Material.getMaterial(item), 1);
                        
                        if (substance == null)
                        {
                            throw error;
                        };
                        
                        final ItemMeta meta = (ItemMeta) substance.getItemMeta();

                        meta.setDisplayName(Mechanism.Interface.KeyItems.display_name);
                        meta.setLore(Mechanism.Interface.KeyItems.lore);

                        substance.setItemMeta(meta);                        
                        
                        Mechanism.Interface.KeyItems.items.add(substance);
                    }
                    
                    catch (final Exception e)
                    {
                        print("There was an invalid item found in the key-items section in the config.yml! Skipping ....");
                    };
                };
            }
            
            catch (final Exception e)
            {
                print("There was an error in the interface part of the config.yml. Certain features may not function as expected!");
            }; /*End of Interface*/
        }
        
        catch (final Exception e)
        {
            
        }; /*End of Mechanism*/
    };
    
    protected class Events implements Listener
    {
        protected class Cache
        { 
            final HashMap<Player, Integer> player_attempts = new HashMap<>();
            final HashMap<Player, ItemStack> player_keys = new HashMap<>();            
            final HashMap<Player, Inventory> player_guis = new HashMap<>();                                
            final HashMap<Player, String> player_ips = new HashMap<>();
        };
        
        // Remove player-ip spot when the player solves the captcha.
        // 
        
        final Cache cache = new Cache();
        
        @EventHandler public void onPlayerQuit(final PlayerQuitEvent e)
        {
            final Player p = (Player) e.getPlayer();
            
            if (cache.player_attempts.containsKey(p))
            {
                if (Mechanism.Security.lock_ip_address) cache.player_ips.remove(p);                
                
                cache.player_attempts.remove(p);
                cache.player_guis.remove(p);
                cache.player_keys.remove(p);            
            };
        };
        
        // Disable things when locked into captcha
        // Finish Completion
        // Inventory Close reappearance
        // Inventory Close reshuffle ?
 
        @EventHandler public void onPlayerInteract(final InventoryClickEvent e)
        {
            if(!(e.getWhoClicked() instanceof Player))
            {
                return;
            };
            
            final Player p = (Player) e.getWhoClicked();
            
            if (cache.player_guis.containsKey(p))
            {
                if (e.getCurrentItem().equals(cache.player_keys.get(p)))
                {
                    // Completion
                    
                    if (Mechanism.Security.lock_ip_address) cache.player_ips.remove(p);                    
                    
                    cache.player_attempts.remove(p);
                    cache.player_guis.remove(p);
                    cache.player_keys.remove(p);
                    
                    p.closeInventory();
                }

                else
                {
                    int attempts = 1;

                    if (cache.player_attempts.containsKey(p))
                    {
                        attempts = cache.player_attempts.get(p);

                        if (attempts >= Mechanism.Security.maximum_attempts)
                        {
                            p.kickPlayer(color("&cYou have exceeded the maximum attempts!\nYou may relog in order to retry."));
                        };

                        attempts += 1;
                    };            

                    cache.player_attempts.put(p, attempts);
                };
                
                if (Mechanism.Security.Restrictions.disable_inventory_interaction)
                {
                    e.setCancelled(true);
                };
            };
        };
        
        @EventHandler public void onPlayerJoin(final PlayerJoinEvent e)
        {   
            final Player p = (Player) e.getPlayer();                                 
            
            getServer().getScheduler().runTaskAsynchronously
            (
                plugin,
                    
                new Runnable()
                {
                    @Override public void run()
                    {
                        final Inventory gui = getInventory(p);
                       
                        cache.player_guis.put(p, gui);
                        
                        if (Mechanism.Security.lock_ip_address)
                        {
                            cache.player_ips.put(p, p.getAddress().getAddress().toString());
                        };
                        
                        cache.player_keys.put(p, cache.player_keys.get(p));                        
                         
                        getServer().getScheduler().runTaskLater
                        (
                            plugin, 
                                
                            new Runnable() 
                            { 
                                @Override public void run() 
                                { 
                                    if (Mechanism.Security.PotionEffects.apply_potion_effects)
                                    {
                                        p.addPotionEffects(Mechanism.Security.PotionEffects.potion_effects);
                                    };
                                    
                                    p.openInventory(gui);
                                   
                                    getServer().getScheduler().runTaskLater
                                    (
                                        plugin, 
                                            
                                        new Runnable() 
                                        {
                                            @Override public void run()
                                            {
                                                if (p.isOnline())
                                                {
                                                    p.kickPlayer(color("&cYou took to long to solve the captcha.\nYou may relog in order to retry."));
                                                };
                                            };
                                        }, 
                                        
                                        Mechanism.Security.attempt_timeout * 20
                                    );
                                }; 
                            },
                            
                            1
                        );
                    };
                }
            );
        };
        
        private Inventory getInventory(final Player p)
        {
            final Random rand = new Random();
            final int sacred_entry = rand.nextInt(24);

            final Inventory gui = Bukkit.createInventory(null, 24, Mechanism.Interface.title);            
            
            for (int normal_size = rand.nextInt(Mechanism.Interface.NormalItems.items.size()), entry = 0; entry < 24; entry += 1)
            {
                if (entry == sacred_entry)
                {
                    gui.setItem(sacred_entry, Mechanism.Interface.KeyItems.items.get(Mechanism.Interface.KeyItems.items.size()));
                    cache.player_keys.put(p, gui.getItem(sacred_entry));
                    
                    continue;
                };

                gui.setItem(entry, Mechanism.Interface.NormalItems.items.get(normal_size));                
            };            
            
            return gui;
        };
    };    
    
    @Override public void onEnable()
    {
        print("Trying to catch my breath here, hold on ....");
        
        LoadConfiguration();
        
        getServer().getPluginManager().registerEvents(new Events(), plugin);
        getCommand("dashcaptcha").setExecutor(new Commands());
        
        print
        (
            (
                "\n-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n" +
                "Author: Dashie A.K.A. KvinneKraft\n" +
                "Version: 2.0\n" +
                "Email: KvinneKraft@protonmail.com\n" +
                "Github: https://github.com/KvinneKraft\n" +
                "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-"
            )
        );
        
        print("I am now breathing!");
    };
    
    protected class Commands implements CommandExecutor
    {
        @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
        {
            if (!(s instanceof Player))
            {
                print("I may only be commanded by a player!");
                return false;
            };
            
            /*Abracadabra here*/
            
            return true;
        };
    };    
    
    @Override public void onDisable()
    {
        print("I think that I died?");
    };
    
    String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
    
    void print(final String line)
    {
        System.out.println("(Better Captcha): " + line);
    };
};