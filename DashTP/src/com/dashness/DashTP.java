
// Author: Dashie
// Version: 1.0

package com.dashness;

import java.util.ArrayList;
import java.util.List;
import net.milkbowl.vault.economy.Economy;
import org.bukkit.Bukkit;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.plugin.java.JavaPlugin;

public class DashTP extends JavaPlugin
{
    public static JavaPlugin plugin;
    public static FileConfiguration config;
    
    Economy econ;
    
    @Override
    public void onEnable()
    {
        Moony.Print("Enabling ze Dash Tp ....");
        
        if(!hasVault())
        {
            Moony.Print("VAULT could not be found, it is required!");
            this.getPluginLoader().disablePlugin(this);
        };
            
        saveDefaultConfig();
        
        config = getConfig();
        plugin = this;
        
        econ = getServer().getServicesManager().getRegistration(Economy.class).getProvider();
        
        getCommand("dashtp").setExecutor(new CommandHandler());
        
        Moony.Print("Dash Tp has been enabled ;D");
    };
    
    class CommandHandler implements CommandExecutor
    {
        List<String> err;
        List<String> scc;
        
        List<String> worlds;
        
        double maxx;
        double minx;
        
        double maxy;
        double miny;
        
        double maxz;
        double minz;
        
        int cooldown;
        
        private void UpdateConfig()
        {
            config.set("allowed-worlds", worlds);            
            
            config.set("coords.max-x", maxx);
            config.set("coords.min-x", minx);
            
            config.set("coords.max-y", maxy);
            config.set("coords.min-y", miny);            
            
            config.set("coords.max-z", maxz);
            config.set("coords.min-z", minz);
            
            config.set("cooldown", cooldown);
            
            plugin.saveConfig();
            
            return;
        };
        
        private void RefreshData()
        {
            plugin.reloadConfig();
            plugin.getConfig();
            
            config = plugin.getConfig();  
            
            worlds = config.getStringList("allowed-worlds");
            
            maxx = config.getDouble("coords.max-x");
            minx = config.getDouble("coords.min-x");
            
            maxy = config.getDouble("coords.max-y");
            miny = config.getDouble("coords.min-y");
            
            maxz = config.getDouble("coords.max-z");
            minz = config.getDouble("coords.min-z");
            
            cooldown = config.getInt("cooldown");                      
            
            return;
        };
        
        private void SetupMessages()
        {
            scc = config.getStringList("success-messages");
            err = config.getStringList("erorr-messages");
            
            for(String str : scc)
                str = Moony.transStr(str);
            
            for(String str : err)
                str = Moony.transStr(str);
            
            return; 
        };
        
        String admin_permission = config.getString("admin-permission");
        String teleport_permission = config.getString("teleport-permission");
        
        @Override
        public boolean onCommand(CommandSender s, Command c, String a, String[] as)
        {
            if(!(s instanceof Player))
                return false;
            
            Player p = (Player) s;
            
            if(as.length < 1)
            {
                p.sendMessage(err.get(0));
                return false;
            }
            
            else if((scc == null) || (err == null))
            {
                SetupMessages();
                RefreshData();                
            };
            
            a = as[0].toLowerCase();
            
            if(a.equals("go"))
            {
                // check for perm
                // teleport
            }
            
            else if(a.equals("reload"))
            {
                if(!p.hasPermission(admin_permission))
                {
                    p.sendMessage(err.get(2));
                    return false;
                };
                    
                p.sendMessage(scc.get(0));
                
                RefreshData();
                
                p.sendMessage(scc.get(1));
            }
            
            else if(a.equals("setrange"))
            {
                if(!p.hasPermission(admin_permission))
                {
                    p.sendMessage(err.get(2));
                    return false;
                }
                
                else if(as.length < 7)
                {
                    p.sendMessage(err.get(1));
                    return false;
                };
                
                List<Double> coords = new ArrayList<Double>();
                
                for(int id = 1; id < 7; id += 1)
                {
                    Double coord = Double.valueOf(as[id]);
                    
                    if(coord == null)
                    {
                        p.sendMessage(err.get(3));
                        return false;
                    };
                    
                    coords.add(coord);
                };
                
                maxx = coords.get(0);
                minx = coords.get(1);
                
                maxy = coords.get(2);
                maxy = coords.get(3);
                
                maxz = coords.get(4);
                minz = coords.get(5);
                
                UpdateConfig();
            }
            
            else if((a.equals("addworld")) || (a.equals("delworld")))
            {
                if(!p.hasPermission(admin_permission))
                {
                    p.sendMessage(err.get(2));
                    return false;
                }
                
                else if(as.length < 2)
                {
                    p.sendMessage(err.get(1));
                    return false;
                };
                
                String world = as[1];
                
                if(worlds.contains(world))
                {
                    if(a.equals("addworld"))
                    {
                        // World already exists
                    }
                    
                    else//delworld
                    {
                        // Remove the world
                    };
                }
                
                else
                {
                    if(a.equals("addworld"))
                    {
                        // Add the world
                    }
                    
                    else//delworld
                    {
                        // World does not exist.
                    };
                };
            }
            
            else
            {
                p.sendMessage(err.get(0));
            };
            
            return false;
        };
    };
    
    @Override
    public void onDisable()
    {
        Moony.Print("Dash Tp has been disabled ;c");
    };
    
    public boolean hasVault()
    {
        if(Bukkit.getServer().getPluginManager().getPlugin("Vault") == null)
            return false;
        
        else
            return true;
    };
};
