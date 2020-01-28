
// Author: Dashie
// Version: 1.0

package com.dashnarok;


import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Random;
import org.bukkit.Bukkit;
import org.bukkit.ChatColor;
import org.bukkit.Color;
import org.bukkit.FireworkEffect;
import org.bukkit.Location;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.EntityType;
import org.bukkit.entity.Firework;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.inventory.meta.FireworkMeta;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;
import org.bukkit.potion.PotionEffectType;

class DashCore
{
    public String transText(String msg)
    {
        return ChatColor.translateAlternateColorCodes('&', msg);
    };
    
    public void Sys(String msg)
    {
        System.out.println("[Dashnarok]: " + transText(msg));
    };
};

public class DashJoin extends JavaPlugin
{
    DashCore xxx = new DashCore();
    
    public static FileConfiguration config;    
    public static JavaPlugin plugin;    
    
    public static Events EventHandler;
    
    @Override
    public void onEnable()
    {
        xxx.Sys("Enabling ....");
        
        saveDefaultConfig();
        
        plugin = (JavaPlugin)this;  
        config = getConfig();
        
        EventHandler = new Events();
        
        if(!config.getBoolean("enabled"))
        {
            xxx.Sys("The plugin is disabled in the config.");
            plugin.getPluginLoader().disablePlugin(plugin);
        };
        
        this.getServer().getPluginManager().registerEvents(EventHandler, this);
        plugin.getCommand("dashnarok").setExecutor(new CommandHandler());
        
        xxx.Sys("Plugin is now running!");
    };
    
    @Override
    public void onDisable()
    {
        xxx.Sys("The plugin has now been disabled!");
    };
};

class CommandHandler implements CommandExecutor
{
    FileConfiguration config = DashJoin.config;    
    DashCore xxx = new DashCore();
    
    final String Error1 = xxx.transText("&cCorrect syntax: &9/dashnarok set [silent-join|first-join|join|quit] <message>");
    final String Error2 = xxx.transText("&cPlease select one of these: Silent-Join, First-Join, Join or Quit");
    final String Error3 = xxx.transText("&cYou are not supposed to do this, are you?");
    final String Error4 = xxx.transText("&cYou must specify a message like so: /dashnarok set join &aThe player %player% has joined!");
    
    final String AdminPermission = config.getString("properties.admin-permission");
    
    final List<String> valid_options = Arrays.asList(
        new String[] 
        { 
            "silent-join", "first-join", "join", "quit" 
        }
    );
    
    @Override
    public boolean onCommand(CommandSender sender, Command command, String label, String[] args)
    {
        if(!(sender instanceof Player))
        {
            xxx.Sys("&cI am sorry but you can not execute this command through the console.");
            return false;
        }
        
        Player p = (Player)sender;
        
        if(!p.hasPermission(AdminPermission))
        {
            p.sendMessage(Error3);
            return false;
        }
        
        else if(args.length < 2)
        {
            p.sendMessage(Error1);
            return false;
        }
        
        else if(args.length < 3)
        {
            p.sendMessage(Error4);
            return false;
        };
        
        String option = args[1].toLowerCase();
        
        if(!valid_options.contains(option))
        {
            p.sendMessage(Error2);
            return false;
        };
        
        DashJoin.EventHandler.messages.set(valid_options.indexOf(option), xxx.transText(args[2]));
        DashJoin.EventHandler.UpdateConfig();
            
        return true;
    };
};

class KvinneKraft 
{
    FileConfiguration config = DashJoin.config;
    DashCore xxx = new DashCore();

    final String fw_node = "properties.dash-effects.fireworks.";
    final List<String> firework_rgbc = config.getStringList(fw_node + "rgb-color-range");
   
    final int firework_multiplier = config.getInt(fw_node + "summon-multiplier");    
    final boolean fireworks_enab = config.getBoolean(fw_node + "enabled");    

    final String po_node = "properties.dash-effects.potions.";
    
    final List<PotionEffect> potions_effs = new ArrayList<>();    
    final List<String> potions_raw = config.getStringList(po_node + "effects");
    
    final boolean potions_prts = config.getBoolean(po_node + "particles");
    final boolean potions_ambt = config.getBoolean(po_node + "ambient");
    final boolean potions_icon = config.getBoolean(po_node + "icon");  
    final boolean potions_enab = config.getBoolean(po_node + "enabled");
    
    public void Krafter(Player p)
    {
        if(fireworks_enab)
        {            
            for(int m = 0; m < firework_multiplier; m += 1)
            {    
                Random rand = new Random();

                int i = rand.nextInt(firework_rgbc.size());
            
                String rgb_key = firework_rgbc.get(i);
            
                int r = 0, g = 0, b = 0;                       
            
                if(rgb_key.equalsIgnoreCase("%all%"))
                {
                    r = rand.nextInt(255);
                    g = rand.nextInt(255);
                    b = rand.nextInt(255);
                }
            
                else
                {
                    String[] f = rgb_key.replace(" ", "").split(",");
                
                    r = Integer.valueOf(f[0]);
                    g = Integer.valueOf(f[1]);
                    g = Integer.valueOf(f[2]);
                };
            
                Color firework_color = Color.fromBGR(r, g, b);
            
                Location location = p.getLocation();

                Firework firework = (Firework)location.getWorld().spawnEntity(location, EntityType.FIREWORK);
                FireworkMeta firework_meta = firework.getFireworkMeta();
            
                // To-Do: Add random Effect thingy.
            
                firework_meta.addEffect(FireworkEffect.builder().withColor(firework_color).withFlicker().withTrail().with(FireworkEffect.Type.BURST).flicker(true).build());
                firework.setFireworkMeta(firework_meta);
            
                p.setInvulnerable(true);
            
                firework.detonate();
            
                p.setInvulnerable(false);
            };
        };
        
        if(potions_enab)
        {               
            if(potions_effs.size() < 1)
            {     
                for(String raw : potions_raw)
                {
                    String[] eff = raw.split(" ");
                    
                    PotionEffectType effect_type = PotionEffectType.getByName(eff[0]);
       
                    int effect_amplifier = Integer.valueOf(eff[1]);
                    int effect_duration = Integer.valueOf(eff[2])*20; 
                    
                    potions_effs.add(new PotionEffect(effect_type, effect_duration, effect_amplifier, potions_ambt, potions_prts, potions_icon));
                };
            };  
            
            p.addPotionEffects(potions_effs);
        };      
    };
};

//
// To-Do:
//
// - Use Separate Files
// - Add in Beginner Kits
// - Add in Beginner Money
//
// I have written this code on such a crappy laptop...
// I will have to wait until I get back home from work ;D
//

class Events implements Listener
{
    static FileConfiguration config = DashJoin.config;    
    static KvinneKraft Kvinne = new KvinneKraft();   
    static DashCore xxx = new DashCore();
    
    static List<String> messages = new ArrayList<>();

    final String silentjoinp = config.getString("properties.silent-join.permission");    
    final boolean silentjoin = config.getBoolean("properties.silent-join.enabled");
    
    final int ON_FIRST_JOIN = 0, ON_JOIN = 1, ON_SILENT_JOIN = 3;
    @EventHandler
    public void onJoin(PlayerJoinEvent e)
    {
        LoadConfig();
        
        Player p = e.getPlayer();
        String m = "none";
        
        if(!p.hasPlayedBefore())
        {
            m = messages.get(ON_FIRST_JOIN);
        }
        
        else
        if((p.hasPermission(silentjoinp)) && (silentjoin))
        {
            m = null;
            
            Bukkit.getScheduler().runTaskAsynchronously(DashJoin.plugin, 
            () -> 
                {
                    for(Player s : Bukkit.getOnlinePlayers())
                    {
                        if(s.hasPermission(silentjoinp))
                        {
                            s.sendMessage(messages.get(ON_SILENT_JOIN).replace("%player%", p.getName()));
                        };
                    };
                }
            );
        }
        
        else
        {
            m = messages.get(ON_JOIN);
        };
        
        if(m != null)
            m = m.replace("%player%", p.getName());
        
        Kvinne.Krafter(p);
        e.setJoinMessage(m);
    };
    
    final int ON_QUIT = 2;
    @EventHandler
    public void onQuit(PlayerQuitEvent e)
    {
        LoadConfig();
        
        e.setQuitMessage(messages.get(ON_QUIT).replace("%player%", e.getPlayer().getName()));
    };
    
    public static void LoadConfig()
    {
        if(messages.size() >= 4)
            return;
        
        messages = config.getStringList("properties.messages");
        
        for(int id = 0; id < messages.size(); id += 1)
            messages.set(id, xxx.transText(messages.get(id)));
    };
    
    public static void UpdateConfig()
    {
        if(messages.size() >= 4)
            return;
        
        config.set("properties.messages", messages);
        DashJoin.plugin.saveConfig();
    };    
};