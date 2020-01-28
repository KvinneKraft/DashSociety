
// Author: Dashie
// Version: 1.0

package com.dashnarok;


import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import org.bukkit.Bukkit;
import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.player.PlayerJoinEvent;
import org.bukkit.event.player.PlayerQuitEvent;
import org.bukkit.plugin.java.JavaPlugin;

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
    
    @Override
    public void onEnable()
    {
        xxx.Sys("Enabling ....");
        
        config = this.getConfig();
        plugin = (JavaPlugin)this;        
        
        if(!config.getBoolean("enabled"))
        {
            xxx.Sys("The plugin is disabled in the config.");
            plugin.getPluginLoader().disablePlugin(plugin);
        };
        
        this.getServer().getPluginManager().registerEvents(new Events(), this);
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
    DashCore xxx = new DashCore();
    
    FileConfiguration config = DashJoin.config;
    
    String Error1 = xxx.transText("&cCorrect syntax: &9/dashnarok set [silent-join|first-join|join|quit] <message>");
    String Error2 = xxx.transText("&cPlease select one of these: Silent-Join, First-Join, Join or Quit");
    String Error3 = xxx.transText("&cYou are not supposed to do this, are you?");
    String Error4 = xxx.transText("&cYou must specify a message like so: /dashnarok set join &aThe player %player% has joined!");
    
    String AdminPermission = config.getString("properties.admin-permission");
    
    List<String> valid_options = Arrays.asList(
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
        
        if(p.hasPermission(AdminPermission))
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
        
        switch(valid_options.indexOf(option))
        {
            case 0:
            {
                
                break;
            }
            
            case 1:
            {
                break;
            }
            
            case 2:
            {
                break;
            }
            
            case 3:
            {
                break;
            }
        };
            
        return true;
    };
};

class KvinneKraft 
{
    FileConfiguration config = DashJoin.config;
    DashCore xxx = new DashCore();
    
    boolean fireworks = config.getBoolean("properties.dash-effects.fireworks.enabled");    
    boolean potions = config.getBoolean("properties.dash-effects.potions.enabled");
    
    boolean fireworktypebigball;
    boolean fireworktypesmallball;
    boolean fireworktypeburst;
    boolean fireworktypecreeper;
    boolean fireworktypestar;
    boolean fireworktypefade;
    boolean fireworktypeflickering;
    
    String fw_node = "dash-effects.fireworks.";
    
    List<Boolean> firework_types = Arrays.asList(
        new Boolean[]
        {
            config.getBoolean(fw_node + "big-ball"),
            config.getBoolean(fw_node + "small-ball"),
            config.getBoolean(fw_node + "burst-ball"),
            config.getBoolean(fw_node + "creeper-ball"),
            config.getBoolean(fw_node + "star-ball"),
            config.getBoolean(fw_node + "fade-ball"),
            config.getBoolean(fw_node + "flickering-ball"),
        }
    );      

    Integer fireworksummonamount = config.getInt(fw_node + "summon-multiplier");    
    
    List<String> rgb_colours = config.getStringList(fw_node + "rgb-color-range");

    
};

//
// To-Do:
//
// - Test Plugin
// - Use Separate Files
// - Add in Fireworks
// - Add in Potion Effects
// - Add in Beginner Kits
// - Add in Beginner Money
//
// I have written this code on such a crappy laptop...
// I will have to wait until I get back home from work ;D
//

class Events implements Listener
{
    FileConfiguration config = DashJoin.config;    
    DashCore xxx = new DashCore();
    
    List<String> messages = new ArrayList<>();

    String silentjoinp = config.getString("properties.silent-join.permission");    
    
    boolean silentjoin = config.getBoolean("properties.silent-join.enabled");
    
    int ON_FIRST_JOIN = 0, ON_JOIN = 1, ON_SILENT_JOIN = 3;
    @EventHandler
    public void onJoin(PlayerJoinEvent e)
    {
        LoadConfig();
        
        Player p = e.getPlayer();
        String m = "none";
        
        if(p.hasPlayedBefore())
        {
            m = messages.get(ON_FIRST_JOIN);
        }
        
        else
        if(p.hasPermission(silentjoinp))
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
        
        e.setJoinMessage(m);
    };
    
    int ON_QUIT = 2;
    @EventHandler
    public void onQuit(PlayerQuitEvent e)
    {
        LoadConfig();
        
        e.setQuitMessage(messages.get(ON_QUIT).replace("%player%", e.getPlayer().getName()));
    };
    
    private void LoadConfig()
    {
        if(messages.size() < 3)
            return;
        
        messages = config.getStringList("properties.messages");
        
        for(int id = 0; id < messages.size(); id += 1)
            messages.set(id, xxx.transText(messages.get(id)));
    };
    
    private void UpdateConfig()
    {
        if(messages.size() < 3)
            return;
        
        config.set("properties.messages", messages);
        DashJoin.plugin.saveConfig();
    };    
};