
// Author: Dashie
// Version: 1.0

package com.dashness;

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
        List<String> err = config.getStringList("erorr-messages");
        List<String> scc = config.getStringList("success-messages");
        
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
            };
            
            a = as[0].toLowerCase();
            
            if(a.equals("go"))
            {
                // teleport
            }
            
            else if(a.equals("reload"))
            {
                
            }
            
            else if(a.equals("setrange"))
            {
                
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
