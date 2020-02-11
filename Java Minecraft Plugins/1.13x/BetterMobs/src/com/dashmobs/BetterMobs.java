
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


// To-DO:
// - /dashmobs add command
// - /dashmobs del command

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
        
        if(entities.size() > 0)
        {
            max_prices.clear();
            min_prices.clear();            
            
            entities.clear();
        };
        
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
                KvinneKraft.print("[" + arr[0] + "]: Invalid entity received. Skipping ....");
                continue;
            }
            
            else if(entities.contains(arr[0].toUpperCase()))
            {
                KvinneKraft.print("[" + arr[0] + "]: The entity was already in list. Skipping....");
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
        
        correct_use_message = KvinneKraft.transStr("&cCorrect use: &7/dashmobs [add | del | reload] <mob> <min-price> <max-price>");        
        developer_message = KvinneKraft.transStr("&eMeow Meow, I am Dashie, the Developer of this Plugin, also known as Princess_Freyja!\n\n&eGithub: &ahttps://github.com/KvinneKraft/ \n&eWebsite: &ahttps://pugpawz.com");
        
        removed_message= KvinneKraft.transStr("&aThe specified rule has been removed from the list.");
        added_message = KvinneKraft.transStr("&aThe specified rule has been added to the list.");
        
        invalid_entity_message = KvinneKraft.transStr("&cYou have specified an unknown entity!");        
        invalid_range_message = KvinneKraft.transStr("&cThe range from which the prices vary is invalid.");
        
        already_exists_message = KvinneKraft.transStr("");
        does_not_exist_message = KvinneKraft.transStr("");
    };
    
    String reloading_message, reloaded_message, developer_message, permission_denied_message, correct_use_message;
    String added_message, removed_message, invalid_entity_message, invalid_range_message, already_exists_message;
    String does_not_exist_message;
    
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
        
        else if(as.length >= 2)
        {
            if(((a.equals("add"))) || (a.equals("del")))
            {
                as[1] = as[1].toUpperCase();
            
                if((as.length >= 4) && (a.equals("add")))
                {
                    if(!Entity.isValid(as[1]))
                    {
                        p.sendMessage(invalid_entity_message);
                        return f;
                    }
                
                    else if(BetterMobs.events.entities.contains(as[1]))
                    {
                        p.sendMessage(already_exists_message);
                        return f;
                    };
                
                    Integer max_price = Integer.valueOf(as[2]);
                    Integer min_price = Integer.valueOf(as[3]);
                
                    if((min_price > max_price) || (min_price < 1) || (max_price < 1))
                    {
                        p.sendMessage(invalid_range_message);
                        return f;
                    };
                
                    BetterMobs.events.entities.add(as[1]);
                
                    BetterMobs.events.max_prices.add(max_price);
                    BetterMobs.events.min_prices.add(min_price);
            
                    p.sendMessage(added_message);
                }
            
                else//del
                {
                    if(BetterMobs.events.entities.contains(as[1]))
                    {
                        int id = BetterMobs.events.entities.indexOf(as[1]);
                       
                        BetterMobs.events.max_prices.remove(id);
                        BetterMobs.events.min_prices.remove(id);
                       
                        BetterMobs.events.entities.remove(id);

                        p.sendMessage(removed_message);
                    }
                    
                    else
                    {
                        p.sendMessage(does_not_exist_message);
                    };
                };
                
                List<String> entity_list = new ArrayList<>();
                
                for(String entity : BetterMobs.events.entities)
                {
                    int id = BetterMobs.events.entities.indexOf(entity);
                    
                    String max_price = String.valueOf(BetterMobs.events.max_prices.get(id));
                    String min_price = String.valueOf(BetterMobs.events.min_prices.get(id));
                    
                    String priced_line = entity + " " + min_price + "-" + max_price;
                    
                    entity_list.add(priced_line);
                };
                
                BetterMobs.config.set("price-properties.priced-entity-list", entity_list);
                
                BetterMobs.plugin.saveConfig();
                BetterMobs.events.refresh();
            }
        }
        
        else 
        {
            p.sendMessage(correct_use_message);
        };
        
        return t;
    };
};
