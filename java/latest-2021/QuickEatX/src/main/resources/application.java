/*
Plugin Developed & Maintained by Xemu
 */
package me.xemu.simplebroadcast;
 
import org.bukkit.Bukkit;
import org.bukkit.ChatColor;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.plugin.java.JavaPlugin;
 
public final class SimpleBroadcast extends JavaPlugin implements CommandExecutor, Listener
{
    protected String permissionMessage;
    protected String permission;
    protected String msg;

    private void print(final String msg)
    {
        System.out.println("[UltimateHelpPage] " + msg);
    };
 
    private String translate(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
 
    @Override public void onEnable()
    {
        saveDefaultConfig();

        final FileConfiguration cfg = getConfig();
        
        permissionMessage = cfg.getString("NoPermissionMessage");
        permission = cfg.getString("Permission");

        getCommand("bc").setExecutor(this);

        //cfg.options().copyDefaults(true);
        //this.saveConfig();
 
        getLogger().info("Plugin Enabled");
    };
 
    @Override public void onDisable() 
    {
        getLogger().info("Plugin Disabled");
    };
 
    @Override public boolean onCommand(final CommandSender sender, final Command command, final String label, final String[] args)
    {
        if(sender instanceof Player)
        {
            final Player player = (Player) sender;

            if(player.hasPermission(permission))
            {
                if(args.length > 0)
                {
                    for (int i = 0; i < args.length; i++)
                    {
                        msg = msg + args[i] + " ";
 
                        for (String string : cfg.getStringList("Broadcast"))
                        {
                            // Intern placeholders
                            string = string.replace("<message>", msg).replace("<sender>", player.getName());
 
                            // Broadcast
                            Bukkit.getServer().broadcastMessage(translate(string));
                        };
                    };
                };
            } 
            
            else 
            {
                player.sendMessage(translate(permissionMessage));
            };
        };
        return true;
    };
 
};