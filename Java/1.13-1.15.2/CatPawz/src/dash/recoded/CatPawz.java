
// Author: Dashie
// Version: 1.0

package dash.recoded;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.Material;
import org.bukkit.Particle;
import org.bukkit.Sound;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.entity.Snowball;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.ProjectileHitEvent;
import org.bukkit.event.player.PlayerInteractEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.ItemMeta;
import org.bukkit.plugin.java.JavaPlugin;

public class CatPawz extends JavaPlugin
{
    FileConfiguration config;
    JavaPlugin plugin;
    
    protected class Verifier
    {
        Material isMaterial(final String material)
        {
            try
            {
                return Material.getMaterial(material);
            }
            
            catch (final Exception e)
            {
                return null;
            }
        };
        
        Sound isSound(final String sound)
        {
            try
            {
                return Sound.valueOf(sound);
            }
            
            catch (final Exception e)
            {
                return null;
            }
        };
        
        Particle isParticle(final String particle)
        {
            try
            {
                return Particle.valueOf(particle);
            }
            
            catch (final Exception e)
            {
                return null;
            }
        };
        
        Integer isInteger(final String integer)
        {
            try
            {
                return Integer.parseInt(integer);
            }
            
            catch (final Exception e)
            {
                return -1;
            }
        };
    };
    
    protected static class CatWand 
    {
        protected static class Properties
        {
            final static List<Particle> particles = new ArrayList<>();          
            final static ItemStack wand_material = new ItemStack(Material.BLAZE_ROD, 1);            
            
            static int particle_density = 0;            
            
            static Sound meow_shoot_sound = null;            
            static Sound meow_sound = null;  
            
            static String permission = "";                        
        };
    };
    
    final Verifier verify = new Verifier();
    
    void ReloadPlugin()
    {
        if (plugin != this)
        {
            plugin = (JavaPlugin) this;
        };
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        final Exception error = new Exception("Errur");        
        
        try
        {
            final Material wand_substance = verify.isMaterial(config.getString("cat-wand.wand-material"));
            
            if (wand_substance == null)
            {
                throw error;
            };
            
            final ItemMeta wand_meta = (ItemMeta) CatWand.Properties.wand_material.getItemMeta();
            
            wand_meta.setLore(Arrays.asList(new String[]{color(config.getString("cat-wand.wand-lore"))}));            
            wand_meta.setDisplayName(color(config.getString("cat-wand.wand-display-name")));
            
            CatWand.Properties.wand_material.setItemMeta(wand_meta);
        }
        
        catch (final Exception e)
        {
            print("ERROR: Invalid properties detected for the Meow Wand in the config.yml!");
        };
        
        CatWand.Properties.particle_density = verify.isInteger(config.getString("cat-wand.meow-particle-density"));
        
        if (CatWand.Properties.particle_density < 1)
        {
            print("ERROR: An invalid property has been detected for the particle density in the config.yml!");
            CatWand.Properties.particle_density = 30;
        };
        
        CatWand.Properties.particles.clear();
        
        for (final String line : config.getStringList("cat-wand.meow-particles"))
        {
            final Particle particle = verify.isParticle(line);
            
            if (particle == null)
            {
                print("ERROR: An invalid property has been detected for the meow particles in the config.yml! Skipping ....");
                continue;
            };
            
            CatWand.Properties.particles.add(particle);
        };
        
        Sound meow_b = verify.isSound(config.getString("cat-wand.meow-shoot-sound"));
        
        if (meow_b == null)
        {
            print("ERROR: An invalid property has been detected for the meow sound in the config.yml! Using the default one!");
            meow_b = Sound.ENTITY_CAT_HISS;
        };
        
        CatWand.Properties.meow_shoot_sound = meow_b;        
        
        Sound meow_a = verify.isSound(config.getString("cat-wand.meow-sound"));
        
        if (meow_a == null)
        {
            print("ERROR: An invalid property has been detected for the meow sound in the config.yml! Using the default one!");
            meow_a = Sound.ENTITY_CAT_PURR;
        };
        
        CatWand.Properties.meow_sound = meow_a;
        CatWand.Properties.permission = config.getString("cat-wand.wand-permission");
        
        admin_permission = config.getString("globalized.admin-permission");
    };
    
    @Override public void onEnable()
    {
        print("I am trying to swim ....");
        
        ReloadPlugin();
        
        getServer().getPluginManager().registerEvents(new Events(), plugin);            
        getCommand("catpawz").setExecutor(new Commands());                  
        
        print
        (
            (
                "\n-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-" +
                "Author: Dashie" +
                "Version: 1.0" +
                "Github: https://github.com/KvinneKraft" +
                "Email: KvinneKraft@protonmail.com" +
                "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-"
            )
        );
        
        print("Okay, I am swimming now.");
    };
    
    protected class Events implements Listener
    {
        @EventHandler public void onInteraction(final PlayerInteractEvent e)
        {
            final Player p = (Player) e.getPlayer();
            
            if (p.hasPermission(CatWand.Properties.permission))
            {
                if (e.getItem().equals(CatWand.Properties.wand_material))
                {
                    
                };
            };
        };
        
        @EventHandler public void onProjectileHit(final ProjectileHitEvent e)
        {
            if (e.getEntity() instanceof Snowball)
            {
                if (e.getEntity().getShooter() instanceof Player)
                {
                    final Player p = (Player) e.getEntity().getShooter();
                    
                    if (p.getInventory().getItemInMainHand().isSimilar(CatWand.Properties.wand_material))
                    {
                        // Summon Effects and what not
                    };
                };
            };
        };
    };
    
    String admin_permission;    
    
    protected class Commands implements CommandExecutor
    {
        @Override public boolean onCommand(final CommandSender s, final Command f, final String a, final String[] as)
        {
            if (!(s instanceof Player))
            {
                print("You may only do this as a player!");
                return false;
            };
            
            final Player p = (Player) s;
            
            if (p.hasPermission(admin_permission))
            {
                if (as.length > 0)
                {
                    final String c = as[0].toLowerCase();
                    
                    if (c.equals("reload"))
                    {
                        p.sendMessage(color("&aWorking on it ....")); ReloadPlugin(); p.sendMessage(color("&aDone!\n&7&oYou may have to check the console for errors if any!"));
                    }
                    
                    else if (c.equals("give"))
                    {
                        // give:0 player:1 amount:2
                        if (as.length >= 1)
                        {
                            Integer amount = 0;
                            Player recever = p;
                            
                            if (as.length >= 3)
                            {
                                recever = getServer().getPlayerExact(as[1]);
                                
                                if (recever == null)
                                {
                                    p.sendMessage(color("&cThe player must be online!"));
                                    return true;
                                };
                                
                                amount = verify.isInteger(as[2]);
                            }
                            
                            else
                            {
                                if (as.length == 1)
                                {
                                    amount = 1;
                                }
                                
                                else
                                {
                                    verify.isInteger(as[1]);                                    
                                };
                            };
                            
                            if (amount < 0)
                            {
                                p.sendMessage(color("&cThe integral value specified must be numeric!"));
                            };
                            
                            final ItemStack substance = CatWand.Properties.wand_material;
                            
                            substance.setAmount(amount);
                            p.getInventory().addItem(substance);
                            
                            if (p.equals(recever))
                            {
                                p.sendMessage(color("&aYou have given yourself " + substance.getItemMeta().getDisplayName() + " &e&l" + substance.getAmount() + "6x &a!"));
                            }
                            
                            else
                            {
                                recever.sendMessage(color("&aYou have received a" + substance.getItemMeta().getDisplayName() + "&a!"));
                                p.sendMessage(color("&aYou have given &e" + recever.getName() + "&a" + substance.getItemMeta().getDisplayName() + " &e&l" + substance.getAmount() + "6x &a!"));
                            };
                        }
                        
                        else
                        {
                            p.sendMessage(color("&cInvalid syntax. Valid syntax: &4/catpawz give <player> <amount>"));
                        };
                    };
                }
                
                else
                {
                    p.sendMessage(color("&cInvalid syntax. Valid syntax: &4/catpawz [reload | give] <player> <amount>"));
                };
                
                return true;
            }
            
            p.sendMessage(color("&cYe may not do this ye!"));
            
            return true;
        };
    };
    
    @Override public void onDisable()
    {
        print("I am now Dead!");
    };
    
    void print(final String line)
    {
        System.out.println("(Cat Pawz): " + line);
    };
    
    String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
};