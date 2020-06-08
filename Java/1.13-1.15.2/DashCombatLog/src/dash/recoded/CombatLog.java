// Author: Dashie
// Version: 1.0

package dash.recoded;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import org.bukkit.ChatColor;
import org.bukkit.Color;
import org.bukkit.FireworkEffect;
import org.bukkit.Location;
import org.bukkit.Material;
import org.bukkit.block.Chest;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Entity;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.Firework;
import org.bukkit.entity.Player;
import org.bukkit.entity.Projectile;
import org.bukkit.entity.Skeleton;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.EntityDamageByEntityEvent;
import org.bukkit.event.entity.EntityDeathEvent;
import org.bukkit.event.entity.PlayerDeathEvent;
import org.bukkit.event.player.PlayerCommandPreprocessEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.inventory.ItemStack;
import org.bukkit.inventory.meta.FireworkMeta;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.scheduler.BukkitTask;

public class CombatLog extends JavaPlugin implements Listener, CommandExecutor
{
    private final List<Boolean> switc = new ArrayList<>();    
    
    private final List<String> mssgs = new ArrayList<>();
    private final List<String> perms = new ArrayList<>();
    private final List<String> cmdwl = new ArrayList<>();
    
    private int timer = 0;
    
    private void clear_caches()
    {
        if (player_inv.size() > 0)
        {
            player_inv.clear();
            
            for (Map.Entry<Player, List<Skeleton>> entry : mob_list.entrySet())
            {
                for (final Skeleton skelly : entry.getValue())
                {
                    skelly.remove();
                };                
            };
        };
        
        if (mob_list.size() > 0)
        {
            mob_list.clear();
        };         
    };    
    
    private void LoadConfiguration()
    {
        saveDefaultConfig();
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        if (mssgs.size() > 0)
        {
            mssgs.clear();
        };
        
        mssgs.add(color(config.getString("messages.tag-message")));
        mssgs.add(color(config.getString("messages.combat-deny-message")));
        mssgs.add(color(config.getString("messages.broadcast-message")));        
        mssgs.add(color(config.getString("messages.expire-message")));
        mssgs.add(color(config.getString("punishment.death-logout.skeleton-name-format")));
        
        if (switc.size() > 0)
        {
            switc.clear();
        };
        
        switc.add(config.getBoolean("messages.broadcast-global"));
        switc.add(config.getBoolean("commands.block-commands"));
        switc.add(config.getBoolean("punishment.death-logout.enabled"));
        switc.add(config.getBoolean("punishment.disable-fly"));
        switc.add(config.getBoolean("punishment.death-logout.skeleton-prop"));
        switc.add(config.getBoolean("punishment.death-logout.skeleton-armour"));
        
        if (perms.size() > 0)
        {
            perms.clear();
        };
        
        perms.add(config.getString("commands.bypass-permission"));
        perms.add(config.getString("punishment.bypass-permission"));
        
        if (cmdwl.size() > 0)
        {
            cmdwl.clear();
        };
        
        cmdwl.addAll(config.getStringList("commands.command-whitelist"));
        
        cmdwl.add("/cl");
        cmdwl.add("/combatlog");
        cmdwl.add("/dashlog");
        
        timer = config.getInt("timer") * 20;
        
        if (switc.get(4))
        {
            clear_caches();
        };        
    };
    
    @Override public void onEnable()
    {
        print("Loading ....");
        
        print("--------------------------------"); 
        print("GitHub: https://github.com/KvinneKraft");        
        print("Email: KvinneKraft@protonmail.com");
        print("--------------------------------");        
        print("Plugin has been made by Dashie A.K.A. KvinneKraft");
        print("--------------------------------");
        
        LoadConfiguration();
        
        getServer().getPluginManager().registerEvents(this, plugin);
        getCommand("combatlog").setExecutor(this);
        
        print("Active!");
    };
    
    private void startCombatTag(final Player p)
    {
        if (p.hasPermission(perms.get(1)))
        {
            return;
        };     
        
        if (tags.containsKey(p) || tims.containsKey(p))
        {
            getServer().getScheduler().getActiveWorkers().remove(tags.get(p));
            tags.get(p).cancel();
            
            getServer().getScheduler().getActiveWorkers().remove(tims.get(p));
            tims.get(p).cancel();          
        }

        else
        {
            if (switc.get(3))
            {
                p.setFlying(false);
            };                       
            
            p.sendMessage(mssgs.get(0));
        };
        
        try
        {   
            final BukkitTask task1 = getServer().getScheduler().runTaskLater
            (
                plugin,

                new Runnable()
                {
                    @Override public void run()
                    {
                        if (tags.containsKey(p))
                        {
                            getServer().getScheduler().getActiveWorkers().remove(tims.get(p));                            
                            
                            tims.get(p).cancel();                            
                            tims.remove(p);                            
                            
                            if (p.isOnline())
                                p.sendMessage(mssgs.get(3));                            
                            
                            tags.remove(p);                                  
                        };
                    };
                },

                timer
            );

            timers.put(p, timer / 20);            
            
            final BukkitTask task2 = getServer().getScheduler().runTaskTimer
            (
                plugin,

                new Runnable()
                {
                    @Override public void run()
                    {      
                        timers.put(p, timers.get(p) - 1);
                    };
                },
                
                20,
                20
            );
                  
            tags.put(p, task1);            
            tims.put(p, task2);              
        }
        
        catch (final Exception e)
        {
            print("A fatal error has occurred!");
        };
    };
    
    private final HashMap<Player, BukkitTask> tags = new HashMap<>(); 
    private final HashMap<Player, BukkitTask> tims = new HashMap<>();
    
    @EventHandler private void onPlayerAttack(final EntityDamageByEntityEvent e)
    {
        if (!(e.getEntity() instanceof Player) || e.getDamage() <= 0)
        {
            return;
        }
        
        Player victim, attacker = (Player) e.getEntity();

        if (e.getDamager() instanceof Projectile)
        {
            final Projectile proj = (Projectile) e.getDamager();
            
            if (!(proj.getShooter() instanceof Player))
            {
                return;
            };
            
            victim = (Player) proj.getShooter();
        }
        
        else if (!(e.getDamager() instanceof Player))
        {
            return;
        }
        
        else
        {
            victim = (Player) e.getDamager();
        };
        
        if (!victim.equals(attacker))
        {
            startCombatTag(attacker);
            startCombatTag(victim);
        };
    };
    
    @EventHandler private void onPlayerLogout(final PlayerQuitEvent e)
    {
        final Player p = (Player) e.getPlayer();
        
        if (switc.get(2) && tags.containsKey(p))
        {
            if (switc.get(0))
            {
                getServer().broadcastMessage(mssgs.get(2).replace("%player%", p.getName()));
            };
            
            if (switc.get(4))
            {
                final Skeleton skelly = (Skeleton) p.getWorld().spawn(p.getLocation(), Skeleton.class);
                
                print("Hey");
                
                skelly.setSeed(0);
                
                if (switc.get(5))
                {
                    final List<ItemStack> armour = Arrays.asList
                    (
                        p.getEquipment().getArmorContents()
                    );
                    
                    if (armour.size() > 0)
                    {
                        skelly.getEquipment().setArmorContents
                        (
                            (ItemStack[]) armour.toArray()
                        );
                        
                        skelly.getEquipment().setHelmetDropChance(10000);
                        skelly.getEquipment().setChestplateDropChance(10000);
                        skelly.getEquipment().setLeggingsDropChance(10000);
                        skelly.getEquipment().setBootsDropChance(10000);
                    };
                };
                
                skelly.setCustomNameVisible(true);
                skelly.setCustomName(mssgs.get(4));
                
                final double health = 20;
                
                skelly.setMaxHealth(health);
                skelly.setHealth(health);
                
                final ItemStack[] player_contents = (ItemStack[]) p.getInventory().getContents();
                
                if (player_contents.length > 0)
                {
                    player_inv.put(p, player_contents);
                };
                
                List<Skeleton> listy = new ArrayList<>();
                
                if (mob_list.containsKey(p))
                {
                    listy = mob_list.get(p);
                };
                
                listy.add(skelly);
                
                mob_list.put(p, listy);
            };
            
            p.setHealth(0);            
        };
    };
    
    final HashMap<Player, List<Skeleton>> mob_list = new HashMap<>();
    final HashMap<Player, ItemStack[]> player_inv = new HashMap<>();
    
    private void DetonateFirework(Location location, Color mcolor, Color fcolor, FireworkEffect.Type type)
    {
        Firework firework = (Firework) location.getWorld().spawnEntity(location, EntityType.FIREWORK);
        FireworkMeta firework_meta = firework.getFireworkMeta();
        
        firework_meta.addEffect(FireworkEffect.builder().withColor(mcolor).with(type).flicker(true).withFlicker().withTrail().withFade(fcolor).trail(true).build());
        
        firework.setFireworkMeta(firework_meta);
        firework.detonate();
    };     
    
    @EventHandler private void onEntityDeath(final EntityDeathEvent e)
    {
        final Entity entity = (Entity) e.getEntity();
        
        if (entity instanceof Skeleton)
        {
            if (e.getEntity().getKiller() instanceof Player)
            {
                final Player p = (Player) e.getEntity().getKiller();
                
                if (mob_list.containsKey(p))
                {
                    if (mob_list.get(p).contains((Skeleton) entity))
                    {
                        mob_list.get(p).remove(mob_list.get(p).indexOf((Skeleton) entity));
                        
                        if (mob_list.get(p).size() < 1)
                        {
                            mob_list.remove(p);
                        };
                        
                        if (player_inv.containsKey(p))
                        {
                            final Location location = (Location) p.getLocation(); 
                            
                            location.getBlock().setType(Material.CHEST);
                            
                            final Chest chest = (Chest) location.getBlock(); 
                            
                            chest.setCustomName(mssgs.get(4).replace("{player}", p.getName()));
                            chest.getInventory().setContents(player_inv.get(p));
                            
                            player_inv.remove(p);                            
                            
                            p.setInvulnerable(true); DetonateFirework(location, Color.PURPLE, Color.AQUA, FireworkEffect.Type.BURST); p.setInvulnerable(false);
                        };
                    };
                };
            };
        };
    };
    
    @EventHandler private void onPlayerCommand(final PlayerCommandPreprocessEvent e)
    {
        final Player p = (Player) e.getPlayer();
        
        if (switc.get(1) && tags.containsKey(p))
        {
            if (!cmdwl.contains(e.getMessage().toLowerCase().replace(":", " ").split(" ")[0]))
            {
                p.sendMessage(mssgs.get(1));
                e.setCancelled(true);
            };
        };
    };
    
    @EventHandler private void onPlayerDeath(final PlayerDeathEvent e)
    {
        final Player p = (Player) e.getEntity();
        
        if (tags.containsKey(p))
        {
            p.sendMessage(mssgs.get(3));

            getServer().getScheduler().getActiveWorkers().remove(tags.get(p));
            tags.get(p).cancel();
            
            getServer().getScheduler().getActiveWorkers().remove(tims.get(p));
            tims.get(p).cancel();             
            
            tims.remove(p);
            tags.remove(p);
        };
    };
    
    private final HashMap<Player, Integer> timers = new HashMap<>();
    
    @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
    {
        if (!(s instanceof Player))
        {
            print("Only players may execute this command!");
            return false;
        };
        
        final Player p = (Player) s;
        
        if (!p.hasPermission("admin"))
        {
            if (tags.containsKey(p))
            {
                p.sendMessage(color("&cTime left: &4" + timers.get(p) + "&cs"));
            }
            
            else
            {
                p.sendMessage(color("&cYou are not in combat."));
            };
            
            return true;
        };
        
        if (as.length > 0)
        {
            if (as[0].equalsIgnoreCase("reload"))
            {
                p.sendMessage(color("&aReloading ...."));
                
                getServer().getScheduler().cancelTasks(plugin);
                LoadConfiguration();
                
                p.sendMessage(color("&aDone!"));
                
                return true;
            };
        };
        
        p.sendMessage(color("&cCorrect syntax: &4/combatlog reload"));
        
        return false;
    };
    
    private FileConfiguration config = (FileConfiguration) null;    
    private final JavaPlugin plugin = (JavaPlugin) this;
    
    @Override public void onDisable()
    {
        getServer().getScheduler().cancelTasks(plugin);
        
        if (switc.get(4))
        {
            clear_caches();
        };
        
        print("Plugin has been disabled ;c");
    };
    
    private void print(String line)
    {
        System.out.println("(Dash Combat Log): " + line);
    };
    
    private String color(String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
};