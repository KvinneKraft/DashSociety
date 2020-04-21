
// Author: Dashie
// Version: 1.0

package discordc;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.List;
import java.util.Random;
import org.bukkit.ChatColor;
import org.bukkit.Sound;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.entity.Player;
import org.bukkit.plugin.java.JavaPlugin;

public class DiscordC extends JavaPlugin implements CommandExecutor
{
    @Override public void onEnable()
    {// I am so sleepy and tired ;c//https://discord.gg/JR8t3bT
        print("Loading plugin ....");
        
        getCommand("disc").setExecutor(this);        
        getCommand("dev").setExecutor(this);
        
        print("The plugin has been loaded!");
    };
    
    private final List<Sound> sounds = Arrays.asList
    (
        new Sound[]
        {
            Sound.CAT_HISS,
            Sound.CAT_HIT,
            Sound.CAT_MEOW,
            Sound.CAT_PURR,
            Sound.CAT_PURREOW,
            Sound.CLICK,
        }
    );
    
    @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
    {
        if(!(s instanceof Player))
        {
            print("You should only use this as an in-game player.");
            return false;
        };
        
        final String d = c.toString().toLowerCase();
        final Player p = (Player) s;
        
        if(d.contains("dev"))
        {
            p.sendMessage(color("&a------------------------------"));            
            p.sendMessage(color("&aHeya, I am Dashie, the Developer of this &7&o(and many more) &aserver!"));
            p.sendMessage(color("&a------------------------------"));
            p.sendMessage(color("&aSee my open source projects at: &ehttps://github.com/KvinneKraft"));
            p.sendMessage(color("&aOr contact me at: &eKvinneKraft@protonmail.com"));
            p.sendMessage(color("&a------------------------------"));        
        }
        
        else
        {
            p.sendMessage(color("&aCheck our discord at: &e&lhttps://discord.gg/JR8t3bT &a!"));
        };
        
        p.playSound(p.getLocation(), sounds.get(new Random().nextInt(sounds.size())), 30, 30);
        
        return true;
    };
    
    @Override public void onDisable()
    {
        print("The plugin has been disabled!");
    };
    
    private String color(String str)
    {
        return ChatColor.translateAlternateColorCodes('&', str);
    };
    
    private void print(String str)
    {
        System.out.println("(DiscordC): " + str);
    };
};