
// Author: Dashie
// Version: 1.0

package com.dashmobs;


import java.util.List;
import java.util.Random;
import java.util.ArrayList;
import org.bukkit.ChatColor;
import org.bukkit.entity.Player;
import org.bukkit.event.Listener;
import org.bukkit.command.Command;
import com.sun.tools.doclint.Entity;
import org.bukkit.event.EventHandler;
import org.bukkit.command.CommandSender;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.command.CommandExecutor;
import net.milkbowl.vault.economy.Economy;
import org.bukkit.event.entity.EntityDeathEvent;
import org.bukkit.plugin.RegisteredServiceProvider;
import org.bukkit.configuration.file.FileConfiguration;


public class BetterMobs extends JavaPlugin
{
    public static FileConfiguration config;
    public static JavaPlugin plugin;
    public static Economy econ;
    
    public static CommandsHandler commands;
    public static EventsHandler events;
    
    private boolean hasVault()
    {
        if(getServer().getPluginManager().getPlugin("Vault") == null)
        {
            return false;
        }
        
        RegisteredServiceProvider<Economy> rsp = getServer().getServicesManager().getRegistration(Economy.class);
        
        if(rsp == null)
        {
            return false;
        };
        
        econ = rsp.getProvider();
        return econ != null;
    };
    
    @Override
    public void onEnable()
    {
        KvinneKraft.print("Loading plugin ....");
        
        saveDefaultConfig();
        
        plugin = this;

        if (!hasVault()) 
        {
            KvinneKraft.print("Vault is missing, please install VAULT.");
            getServer().getPluginManager().disablePlugin(plugin);
        };
        
        events = new EventsHandler();
        events.refresh();
        
        getServer().getPluginManager().registerEvents(events, plugin);
        
        commands = new CommandsHandler();
        commands.refresh();
        
        getCommand("bettermobs").setExecutor(commands);
        
        KvinneKraft.print("Plugin has been loaded!");
    };
    
    @Override
    public void onDisable()
    {
        KvinneKraft.print("Plugin has been disabled!");
    };
};


class EventsHandler implements Listener
{    
    List<Integer> min_prices = new ArrayList<>();
    List<Integer> max_prices = new ArrayList<>();
 
    List<String> entities = new ArrayList<>();    
    
    String reward_permission, reward_message;
    
    public void refresh()
    {
        BetterMobs.plugin.reloadConfig();
        BetterMobs.config = BetterMobs.plugin.getConfig();        
        
        reward_permission = BetterMobs.config.getString("price-properties.price-permission");
        reward_message = KvinneKraft.transStr(BetterMobs.config.getString("price-properties.price-message"));
        
        for(String str : BetterMobs.config.getStringList("price-properties.priced-entity-list"))
        {
            String[] arr = str.split(" ");
            
            if(arr.length < 2)
            {
                KvinneKraft.print("Invalid format specified. Skipping ....");
                continue;
            }
                
            else if(!Entity.isValid(arr[0].toUpperCase()))
            {
                KvinneKraft.print("Invalid mob " + arr[0] + ". Skipping ....");
                continue;
            };
            
            String[] sarr = arr[1].split("-");
            
            if(sarr.length < 2)
            {
                KvinneKraft.print("[SARR LENGTH: " + sarr.length + "]: Invalid price range specified. Skipping ....");
                continue;
            };
            
            Integer min_price = Integer.valueOf(sarr[0]);
            Integer max_price = Integer.valueOf(sarr[1]);
            
            if((min_price > max_price) || (min_price < 1))
            {
                KvinneKraft.print("[" + sarr[0] + "-" + sarr[1] + "]: Minimum price must be lower than maximum price. Skipping ....");
                continue;
            };
            
            min_prices.add(min_price);
            max_prices.add(max_price);
        
            entities.add(Entity.getValue(arr[0].toUpperCase()));        
        };
    };
    
    @EventHandler
    public void onEntityDeath(EntityDeathEvent e)
    {
        if(!(e.getEntity().getKiller() instanceof Player))
        {
            return;
        };
        
        Player p = (Player) e.getEntity().getKiller();
        
        if((!p.hasPermission(reward_permission)) || (!entities.contains(e.getEntity().getType())))
        {
            return;
        };
        
        String entity_name = e.getEntity().getType().toString().toLowerCase();
        
        if(e.getEntity() instanceof Player)
        {
            entity_name = ((Player)e.getEntity()).getName();
        };
        
        int entity_id = entities.indexOf(e.getEntity().getType());
 
        int max_price = max_prices.get(entity_id);
        int min_price = min_prices.get(entity_id);
        
        int reward_cash = new Random().nextInt((max_price - min_price) + 1) + min_price;
        BetterMobs.econ.depositPlayer(p, reward_cash);
        
        String message = reward_message.replace("%m%", String.valueOf(reward_cash)).replace("%e%", entity_name);
        p.sendMessage(message);
    };
};


class KvinneKraft
{
    public static void print(String str)
    {
        System.out.println("(Dash Mobs): " + str);
    };
        
    public static String transStr(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
};


class CommandsHandler implements CommandExecutor 
{
    boolean t = true, f = false;
    boolean developer_support;
    
    String admin_permission;
    
    public void refresh()
    {
        BetterMobs.plugin.reloadConfig();
        BetterMobs.config = BetterMobs.plugin.getConfig();        
        
        permission_denied_message = KvinneKraft.transStr("&cYou lack sufficient Permissions!");
        
        developer_support = BetterMobs.config.getBoolean("optional-properties.developer-support");        
        admin_permission = BetterMobs.config.getString("optional-properties.admin-permission");
        
        reloading_message = KvinneKraft.transStr("&aDash Mobs is now reloading ....");
        reloaded_message = KvinneKraft.transStr("&aDash Mobs has been reloaded!");
        
        correct_use_message = KvinneKraft.transStr("&cCorrect use: &7/dashmobs reload");        
        developer_message = KvinneKraft.transStr("&eMeow Meow, I am Dashie, the Developer of this Plugin, also known as Princess_Freyja!\n\n&eGithub: &ahttps://github.com/KvinneKraft/ \n&eWebsite: &ahttps://pugpawz.com");
    };
    
    String reloading_message, reloaded_message, developer_message, permission_denied_message, correct_use_message;
    
    @Override
    public boolean onCommand(CommandSender s, Command c, String a, String[] as)
    {
        if(!(s instanceof Player))
            return f;
        
        Player p = (Player) s;
        
        if(!p.hasPermission(admin_permission))
        {
            if(developer_support)
            {
                p.sendMessage(developer_message);
            }
            
            else
            {
                p.sendMessage(permission_denied_message);
            };
            
            return f;
        }
        
        else if(as.length < 1)
        {
            p.sendMessage(correct_use_message);
            return f;
        };
        
        a = as[0].toLowerCase();
        
        if(a.equals("reload"))
        {
            p.sendMessage(reloading_message);
            
            BetterMobs.commands.refresh();
            refresh();
            
            p.sendMessage(reloaded_message);
        }
        
        else 
        {
            p.sendMessage(correct_use_message);
        };
        
        return t;
    };
};
