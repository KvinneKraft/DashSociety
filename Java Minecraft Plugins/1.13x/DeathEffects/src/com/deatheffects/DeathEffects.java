

// Author: Dashie
// Version: 1.0


package com.deatheffects;


import java.util.ArrayList;
import java.util.List;
import net.minecraft.server.v1_15_R1.SoundEffect;
import org.bukkit.ChatColor;
import org.bukkit.configuration.file.FileConfiguration;
import org.bukkit.entity.Player;
import org.bukkit.event.EventHandler;
import org.bukkit.event.Listener;
import org.bukkit.event.entity.PlayerDeathEvent;
import org.bukkit.plugin.java.JavaPlugin;
import org.bukkit.potion.PotionEffect;


class DeathEvents implements Listener
{
  boolean hasDeathMoney = false;
  boolean takeMoneyFromVictim = false;
  boolean hasDeathSounds = false;
  boolean hasDeathZombie = false;
  boolean hasDeathLightning = false;
  boolean hasHeadDrops = false;
  boolean hasDeathEffects = false;

  int takeMoneyPercentage;

  List<PotionEffect> potionEffects = new ArrayList<>();

  SoundEffect deathSound = null;

  @EventHandler
  public void onPlayerDeath(PlayerDeathEvent e)
  {
    if(!(e.getEntity().getKiller() instanceof Player))
    {
      return;
    }

    Player k = e.getEntity().getKiller();
    Player v = e.getEntity();

  };
};

public class DeathEffects extends JavaPlugin
{
  public static FileConfiguration config;
  public static JavaPlugin plugin;

  public static DeathEvents events = new DeathEvents();

  @Override public void onEnable()
  {
    Kvinne.print("Plugin is being enabled ....");

    saveDefaultConfig();

    config = (FileConfiguration) this.getConfig();
    plugin = (JavaPlugin) this;

    String[] arr = new String[] { "Author: Dashie", "Version: 1.0", "Github: https://github.com/KvinneKraft/", "Website: https://pugpawz.com/" };

    for(String str : arr)
      System.out.println(str);

    Kvinne.print("Plugin has been enabled!");
  };

  public static void load_config()
  {
    plugin.reloadConfig();
    config = plugin.getConfig();

    events.hasDeathMoney = config.getBoolean("properties.death-money.enabled");

    if(events.hasDeathMoney)
    {
      events.takeMoneyFromVictim = config.getBoolean("properties.death-money.take-from-victim");

      if(events.takeMoneyFromVictim)
      {
        events.takeMoneyPercentage = config.getInt("properties.death-money.take-percentage");
      };

      // Check for VAULT, if not, turn this option off;
    };

    events.hasDeathEffects   = config.getBoolean("properties.death-effects.enabled");

    if(events.hasDeathEffects)
    {
      // Format effects in config and see if they are valid;
    };

    events.hasDeathSounds = config.getBoolean("properties.death-sounds.enabled");

    if(events.hasDeathSounds)
    {
      // Check if sound in config is valid;
    };

    events.hasDeathZombie    = config.getBoolean("properties.death-zombie.enabled");
    events.hasDeathLightning = config.getBoolean("properties.death-lightning.enabled");
    events.hasHeadDrops      = config.getBoolean("properties.head-drops.enabled");
  };

  @Override public void onDisable()
  {
    Kvinne.print("Plugin has been disabled!");
  };
};

class Kvinne
{
  public static void print(String str)
  {
    System.out.println("(DeathEffects): " + str);
  };

  public static String color(String str)
  {
    return ChatColor.translateAlternateColorCodes('&', str);
  };
};

