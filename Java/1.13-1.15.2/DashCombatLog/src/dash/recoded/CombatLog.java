package dash.recoded;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.entity.Projectile;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.EntityDamageByEntityEvent;
import org.bukkit.event.entity.PlayerDeathEvent;
import org.bukkit.event.player.PlayerCommandPreprocessEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.scheduler.BukkitTask;

public class CombatLog extends JavaPlugin implements Listener, CommandExecutor
{
    private final List<Boolean> switc = new ArrayList<>();    
    
    private final List<String> mssgs = new ArrayList<>();
    private final List<String> perms = new ArrayList<>();
    private final List<String> cmdwl = new ArrayList<>();
    
    private int timer = 0;
    
    private void LoadConfiguration()
    {
        saveDefaultConfig();
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        if (mssgs.size() > 0)
        {
            mssgs.clear();
        };
        
        mssgs.add(color(config.getString("dash-combat-log.general.messages.tag-message")));
        mssgs.add(color(config.getString("dash-combat-log.general.messages.combat-deny-message")));
        mssgs.add(color(config.getString("dash-combat-log.general.messages.broadcast-message")));        
        mssgs.add(color(config.getString("dash-combat-log.general.messages.expire-message")));
        
        if (switc.size() > 0)
        {
            switc.clear();
        };
        
        switc.add(config.getBoolean("dash-combat-log.general.messages.broadcast-global"));
        switc.add(config.getBoolean("dash-combat-log.general.commands.block-commands"));
        switc.add(config.getBoolean("dash-combat-log.punishment.death-logout"));
        switc.add(config.getBoolean("dash-combat-log.punishment.disable-fly"));
        
        if (perms.size() > 0)
        {
            perms.clear();
        };
        
        perms.add(config.getString("dash-combat-log.general.commands.bypass-permission"));
        perms.add(config.getString("dash-combat-log.punishment.bypass-permission"));
        
        if (cmdwl.size() > 0)
        {
            cmdwl.clear();
        };
        
        cmdwl.addAll(config.getStringList("dash-combat-log.general.commands.command-whitelist"));
        
        cmdwl.add("/cl");
        cmdwl.add("/combatlog");
        cmdwl.add("/dashlog");
        
        timer = config.getInt("dash-combat-log.timer") * 20;
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
                            if (p.isOnline())
                            {
                                p.sendMessage(mssgs.get(3));
                            };
                          
                            getServer().getScheduler().getActiveWorkers().remove(tims.get(p));
                            tims.get(p).cancel();                            
                            
                            tags.remove(p);
                            tims.remove(p);                                  
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
            p.setHealth(0);
            
            if (switc.get(0))
            {
                getServer().broadcastMessage(mssgs.get(2).replace("%player%", p.getName()));
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