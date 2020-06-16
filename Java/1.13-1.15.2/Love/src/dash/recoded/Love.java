
// Author: Dashie
// Version: 1.0

package dash.recoded;

import org.bukkit.ChatColor;
import org.bukkit.Sound;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.Listener;
import org.bukkit.plugin.java.JavaPlugin;

public class Love extends JavaPlugin
{
    @Override public void onEnable()
    {
        print("Charging up ....");
        
        LoadConfiguration();
        
        getCommand("love").setExecutor(this);
        getServer().getPluginManager().registerEvents(new Events(), plugin);
        
        print
        (
            "\n" +
            "-=-=-=-=-=-=-=-=-=-=-=-=-=-" +
            "Author: Dashie" +
            "Version: 1.0" +
            "Email: KvinneKraft@protonmail.com" +
            "Github: https://github.com/KvinneKraft" +
            "-=-=-=-=-=-=-=-=-=-=-=-=-=-"
        );
        
        print("I am alive!");
    };
    
    protected static class Properties
    {
        protected static class Marriage
        {
            static boolean broadcast_globally, play_sound;
            static int minimum_radius;            
            
            static String permission, proposal_pending, proposal_request_receive, proposal_request_send, proposal_accept_receiver, proposal_accept_sender, proposal_deny_sender, proposal_deny_receiver, married_message_receiver, married_message_sender, married_message_global;
            static Sound accept_sound, denied_sound;
        };
        
        protected static class Kisses
        {
            static int minimum_radius, give_delay;
            
            static String permission, receive_message, give_message;            
            static Sound receive_sound, give_sound; 
        };
        
        protected static class Huggers
        {
            static int minimum_radius, give_delay;
        
            static String permission, receive_message, give_message;
            static Sound receive_sound, give_sound;
        };
    };
    
    FileConfiguration config;
    JavaPlugin plugin;
    
    void LoadConfiguration()
    {
        if (plugin == null)
            plugin = (JavaPlugin) this;
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
        
        
    };
    
    protected class Events implements Listener
    {
        
    };
    
    protected class Commands implements CommandExecutor
    {
        @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
        {
            if (!(s instanceof Player))
            {
                print("Only players may execute this command!");
                return false;
            };
            
            return true;
        };
    };
    
    String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
    
    void print(final String line)
    {
        System.out.println("(Better Love): " + line);
    };
    
    @Override public void onDisable()
    {
        if (getServer().getScheduler().getActiveWorkers().size() > 0) getServer().getScheduler().cancelTasks(plugin);
        print("I died!");
    };
};