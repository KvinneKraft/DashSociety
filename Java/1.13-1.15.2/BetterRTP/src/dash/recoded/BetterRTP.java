
// Author: Dashie
// Version: 1.0

package dash.recoded;

import java.util.ArrayList;
import java.util.List;
import java.util.Random;
import org.bukkit.ChatColor;
import org.bukkit.Color;
import org.bukkit.FireworkEffect;
import org.bukkit.Location;
import org.bukkit.Material;
import org.bukkit.Sound;
import org.bukkit.World;
import org.bukkit.command.Command;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.Firework;
import org.bukkit.entity.Player;
import org.bukkit.inventory.meta.FireworkMeta;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;

public class BetterRTP extends JavaPlugin
{
    @Override public void onEnable()
    {
        print("Plugin is being enabled ....");
        
        LoadConfiguration();
        
        getCommand("rtp").setExecutor(this);
        
        print("Plugin has been enabled.");
    };
    
    private FileConfiguration config = (FileConfiguration) null;
    private final JavaPlugin plugin = (JavaPlugin) this;
    
    private boolean isInteger(final String value)
    {
        try
        {
            Integer.valueOf(value);
            return true;
        }
        
        catch (final Exception e)
        {
            return false;
        }
    };
    
    private void LoadConfiguration()
    {
        saveDefaultConfig();
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        minx = config.getDouble("coordinates.min-x");
        maxx = config.getDouble("coordinates.max-x");
        minz = config.getDouble("coordinates.min-z");
        maxz = config.getDouble("coordinates.max-z");
        
        normal_permission = config.getString("permissions.teleportation-permission");
        admin_permission = config.getString("permissions.admin-permission");
        
        do_fireworks = config.getBoolean("optionals.firework-effects.enabled");
        do_potions = config.getBoolean("optionals.potion-effects.enabled");
        do_sounds = config.getBoolean("optionals.sound-effects.enabled");
        
        if (do_potions)
        {
            for (String line : config.getStringList("optionals.potion-effects"))
            {
                final String[] cache = line.split(" ");
                
                if (cache.length < 3)
                {
                    print("Invalid potion format in the config.yml!");
                    continue;
                }
                
                else if (!isInteger(cache[1]) || !(isInteger(cache[2])))
                {
                    print("Invalid amplifier and or duration in the config.yml!");
                    continue;
                }
                
                else if (PotionEffectType.getByName(cache[0].toUpperCase()) == null)
                {
                    print("Invalid potion effect type in the config.yml!");
                    continue;
                };
                
                potion_effects.add(new PotionEffect(PotionEffectType.getByName(cache[0].toUpperCase()), Integer.valueOf(cache[2]) * 20, Integer.valueOf(cache[1])));
            };
        };
        
        if (potion_effects.size() < 1)
        {
            do_potions = false;
        };
        
        if (do_sounds)
        {
            try 
            {
                sound = (Sound) Sound.valueOf(config.getString("optionals.sound-effects.sound-effect").toUpperCase());
            }
            
            catch (final Exception e)
            {
                print("Invalid sound specified in the config.yml!");
                do_sounds = false;
            }
        };
        
        for (String line : config.getStringList("optionals.world-whitelist"))
        {
            final World world = (World) getServer().getWorld(line);
            
            if (world == null)
            {
                print("Invalid world name in the config.yml!");
                continue;
            };
            
            worlds.add(world);
        };
        
        cooldown = config.getInt("optionals.teleportation-cooldown") * 20;
        send_title = config.getBoolean("optionals.send_title");
    };
    
    private final List<PotionEffect> potion_effects = new ArrayList<>();    
    private final List<Player> players = new ArrayList<>();
    private final List<World> worlds = new ArrayList<>();
    
    private boolean do_potions, do_fireworks, do_sounds, send_title;    
    private double minx, maxx, minz, maxz;
    private int cooldown;
    
    private String normal_permission, admin_permission;    
    private Sound sound;
    
    private void randomly_teleport(final Player p)
    {
        if (!worlds.contains(p.getWorld()))
        {
            p.sendMessage(color("&cYou may not use this command in this world."));
            return;
        }
        
        else if (players.contains(p))
        {
            p.sendMessage(color("&cYou must wait at least " + cooldown / 20 + " seconds before doing this again."));
            return;
        };
        
        final Random rand = new Random();
        double x, y = 165, z;
        
        x = minx + (maxx - minx) * rand.nextDouble();
        z = minz + (maxz - minz) * rand.nextDouble();
        
        final Location location = new Location(p.getWorld(), x, y, z);
        boolean hasLand = false;
        
        while (!hasLand)
        {
            if (y <= 8)
            {
                p.sendMessage(color("&cNo suitable location could has been found."));
                return;
            }
            
            else
            {
                location.setY(y);
                
                if (location.getBlock().getType().equals(Material.AIR))
                {
                    location.setY(y + 1);
                    
                    if (location.getBlock().getType().equals(Material.AIR))
                    {
                        location.setY(y - 2);
                        
                        if (!location.getBlock().getType().equals(Material.AIR))
                        {
                            location.setY(y + 1);                        
                            hasLand = true;

                            continue;                              
                        };
                    };
                };
            };
            
            y -= 1;
        };
        
        final String success = color("&aYou have been teleported to &7X: " + (int) x + " &7Y: " + (int) y + " &7Z: " + (int) z + " &a!");
        
        p.sendMessage(success);
        
        if (send_title)
        {
            p.sendTitle("", success);            
        };
        
        p.teleport(location);
        
        if (do_potions && potion_effects.size() > 0)
        {
            p.addPotionEffects(potion_effects);
        };
        
        if (do_fireworks)
        {
            p.setInvulnerable(true);
            
            DetonateFirework(location, Color.fromRGB(rand.nextInt(255), rand.nextInt(255), rand.nextInt(255)), Color.fromRGB(rand.nextInt(255), rand.nextInt(255), rand.nextInt(255)), FireworkEffect.Type.BURST);
            
            p.setInvulnerable(false);
        };
        
        if (do_sounds && sound != null)
        {
            p.playSound(location, sound, 30, 30);
        };
        
        if (p.isOp() || p.hasPermission(admin_permission))
        {
            return;
        };
        
        players.add(p);
        
        getServer().getScheduler().runTaskLater
        (
            plugin, 
                
            new Runnable() 
            { 
                @Override public void run() 
                { 
                    if (!players.contains(p))
                    {
                        return;
                    };
                    
                    if (p.isOnline())
                    {
                        p.sendMessage(color("&aYou may now use &7/wild &aagain!"));
                    };
                    
                    players.remove(p);
                }; 
            }, 
            
            cooldown
        );
    };
    
    @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
    {
        if (!(s instanceof Player))
        {
            print("You may only execute this command as a player!");
            return false;
        };
        
        final Player p = (Player) s;
        
        if (as.length > 0 && p.hasPermission(admin_permission) && as[0].equalsIgnoreCase("reload"))
        {
            p.sendMessage(color("&eReloading configuration ...."));
            
            LoadConfiguration();
            
            p.sendMessage(color("&eConfiguration has been reloaded!"));
            
            return true;
        }
        
        else if (p.hasPermission(normal_permission) || p.hasPermission(admin_permission))
        {
            randomly_teleport(p);
        }
        
        else
        {
            p.sendMessage(color("&cYou have insufficient permissions!"));
            return false;
        };
        
        return true;
    };
    
    public void DetonateFirework(Location location, Color mcolor, Color fcolor, FireworkEffect.Type type)
    {
        Firework firework = (Firework) location.getWorld().spawnEntity(location, EntityType.FIREWORK);
        FireworkMeta firework_meta = firework.getFireworkMeta();
        
        firework_meta.addEffect(FireworkEffect.builder().withColor(mcolor).with(type).flicker(true).withFlicker().withTrail().withFade(fcolor).trail(true).build());
        
        firework.setFireworkMeta(firework_meta);
        firework.detonate();
    };       
    
    @Override public void onDisable()
    {
        print("Plugin has been disabled!");
    };
    
    private void print(String line)
    {
        System.out.println("(Better RTP): " + line);
    };
    
    private String color(String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
};