
// Author: Dashie
// Version: 1.0

package dash.recoded;

import java.util.ArrayList;
import java.util.List;
import org.bukkit.ChatColor;
import org.bukkit.Color;
import org.bukkit.FireworkEffect;
import org.bukkit.Sound;
import org.bukkit.command.Command;
import org.bukkit.command.CommandExecutor;
import org.bukkit.command.CommandSender;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.Listener;
import org.bukkit.inventory.Inventory;
import org.bukkit.plugin.java.JavaPlugin;

public class Captcha extends JavaPlugin
{
    FileConfiguration config = null;
    JavaPlugin plugin = null;
    
    // Order variables correctly later on^
    
    protected static class Sounds 
    {
        static boolean do_completion_sound = false;
       
        static String permission;
        static Sound completion_sound;
    };
    
    protected static class Fireworks 
    {
        static boolean do_fireworks = false;

        static final List<FireworkEffect.Type> firework_types = new ArrayList<>();
        static boolean random_firework_types = true;

        static final List<Color> rgb_combinations = new ArrayList<>();
        static boolean random_firework_color = true;            
        
        static String permission;
    };
    
    protected static class Lightning
    {
        static boolean do_lightning = false;
    };
    
    void LoadConfiguration()
    {
        saveDefaultConfig();
        
        if (plugin == null)
            plugin = (JavaPlugin) this;
        
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();
    
        Lightning.do_lightning = config.getBoolean("modules.lightning-switch");
        
        final Exception error = new Exception("error");
        final Exception skip = new Exception("skip");
        
        try /*Firework Module*/
        {
            Fireworks.do_fireworks = config.getBoolean("modules.fireworks.enabled");
            
            if (!Fireworks.do_fireworks)
            {
                throw skip;
            };
            
            Fireworks.random_firework_types = config.getBoolean("modules.fireworks.effect-randomizer");
            
            if (!Fireworks.random_firework_types) 
            {
                for (final String type : config.getStringList("modules.fireworks.effects"))
                {
                    final String arr[] = type.replace(" ", "").toUpperCase().split(",");
                    
                    try
                    {
                        for (final String suspect : arr)
                        {
                            final FireworkEffect.Type buff = FireworkEffect.Type.valueOf(suspect);
                            
                            if (type == null)
                            {
                                throw error;
                            };
                            
                            Fireworks.firework_types.add(buff);
                        };
                    }
                    
                    catch (final Exception e)
                    {
                        print("Invalid effect type settings found. Skipping ....");
                    };
                };
                
                if (Fireworks.firework_types.size() < 1)
                {
                    print("Insufficient Firework Types found, using randomizer!");
                    Fireworks.random_firework_types = true;
                };
            };
            
            Fireworks.random_firework_color = config.getBoolean("modules.fireworks.rgb-randomizer");
            
            if (!Fireworks.random_firework_color)
            {
                for (final String code : config.getStringList("modules.fireworks.rbg-colours"))
                {
                    final String arr[] = code.replace(" ", "").split(",");
     
                    try
                    {
                        if (arr.length < 3)
                        {
                            print("Invalid R.G.B. colour codes found. Skipping ....");
                            continue;
                        };
                        
                        final int[] rgb = {255,255,255};
                        
                        for (int i = 0; i < 3; i += 1)
                            rgb[i] = Integer.parseInt(arr[i]);
                        
                        Fireworks.rgb_combinations.add
                        (
                            Color.fromRGB
                            (
                                rgb[0], 
                                rgb[1], 
                                rgb[2]
                            )
                        );
                    }
                    
                    catch (final Exception e)
                    {
                        print("Invalid R.G.B. colour format has been found in the configuration file. Skipping ....");
                    };
                };
                
                if (Fireworks.rgb_combinations.size() < 1)
                {
                    print("Insufficient R.G.B. combinations found, using randomizer!");
                    Fireworks.random_firework_color = true;
                };
            };
            
            Fireworks.permission = config.getString("modules.fireworks.permission");
        }
        
        catch (final Exception e)
        {
            if (!e.toString().equals("skip"))
            {
                print("Invalid fireworks settings were detected in the configuration file. Disabling this module!");
                Fireworks.do_fireworks = false;
            };
        }; /* End of Firework Module */
        
        try /*Sound Module*/
        {
            Sounds.do_completion_sound = config.getBoolean("modules.sounds.enabled");

            if (Sounds.do_completion_sound)
            {
                Sounds.completion_sound = Sound.valueOf(config.getString("modules.sounds.completion-sound"));

                if (Sounds.completion_sound == null)
                {
                    throw error;
                };
                
                Sounds.permission = config.getString("modules.sounds.permission");
            };
        }
        
        catch (final Exception e)
        {
            print("Invalid sound has been found in the configuration file. Using the default one!");
            Sounds.completion_sound = Sound.ENTITY_PLAYER_LEVELUP;
        }; /* End of Sound Module */   
        
        internal.InitializeGUI();
    };
    
    final Internal internal = new Internal();    
    
    protected class Internal 
    {
        Inventory inventory = null;
        
        void InitializeGUI()
        {
            
        };
    };    
    
    @Override public void onEnable()
    {
        print("Trying to catch my breath here, hold on ....");
        
        LoadConfiguration();
        
        getServer().getPluginManager().registerEvents(new Events(), plugin);
        getCommand("dashcaptcha").setExecutor(new Commands());
        
        print
        (
            (
                "\n-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n" +
                "Author: Dashie A.K.A. KvinneKraft\n" +
                "Version: 2.0\n" +
                "Email: KvinneKraft@protonmail.com\n" +
                "Github: https://github.com/KvinneKraft\n" +
                "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-"
            )
        );
        
        print("I am now breathing!");
    };
    
    protected class Commands implements CommandExecutor
    {
        @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
        {
            if (!(s instanceof Player))
            {
                print("I may only be commanded by a player!");
                return false;
            };
            
            /*Abracadabra here*/
            
            return true;
        };
    };    
    
    protected class Events implements Listener
    {
        /*More Abracadabra here*/
    };
    
    @Override public void onDisable()
    {
        print("I think that I died?");
    };
    
    String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
    
    void print(final String line)
    {
        System.out.println("(Better Captcha): " + line);
    };
};