// 
// Decompiled by Procyon v0.5.36
// 

package codes.biscuit.genbucket;

import codes.biscuit.genbucket.hooks.MetricsLite;
import org.bukkit.command.TabCompleter;
import codes.biscuit.genbucket.commands.GenBucketAdminCommand;
import org.bukkit.command.CommandExecutor;
import codes.biscuit.genbucket.commands.GenBucketCommand;
import org.bukkit.plugin.Plugin;
import org.bukkit.event.Listener;
import codes.biscuit.genbucket.listeners.PlayerListener;
import java.util.regex.Pattern;
import org.bukkit.Bukkit;
import codes.biscuit.genbucket.utils.BucketManager;
import codes.biscuit.genbucket.hooks.HookUtils;
import codes.biscuit.genbucket.utils.Utils;
import codes.biscuit.genbucket.utils.ConfigValues;
import org.bukkit.plugin.java.JavaPlugin;

public class GenBucket extends JavaPlugin
{
    private ConfigValues configValues;
    private Utils utils;
    private HookUtils hookUtils;
    private BucketManager bucketManager;
    private int minecraftVersion;
    
    public GenBucket() {
        this.minecraftVersion = -1;
    }
    
    public void onEnable() {
        if (this.minecraftVersion == -1) {
            String mcVersion = Bukkit.getBukkitVersion().split(Pattern.quote("-"))[0].split(Pattern.quote("."))[1];
            if (mcVersion.contains(".")) {
                mcVersion = mcVersion.split(Pattern.quote("."))[0];
            }
            this.minecraftVersion = Integer.valueOf(mcVersion);
        }
        this.bucketManager = new BucketManager();
        this.utils = new Utils(this);
        this.configValues = new ConfigValues(this);
        Bukkit.getPluginManager().registerEvents((Listener)new PlayerListener(this), (Plugin)this);
        this.getCommand("genbucket").setExecutor((CommandExecutor)new GenBucketCommand(this));
        final GenBucketAdminCommand gbaCommand = new GenBucketAdminCommand(this);
        this.getCommand("genbucketadmin").setExecutor((CommandExecutor)gbaCommand);
        this.getCommand("genbucketadmin").setTabCompleter((TabCompleter)gbaCommand);
        this.saveDefaultConfig();
        this.hookUtils = new HookUtils(this);
        this.utils.registerRecipes();
        this.utils.updateConfig();
        this.configValues.loadBuckets();
        new MetricsLite((Plugin)this);
    }
    
    public ConfigValues getConfigValues() {
        return this.configValues;
    }
    
    public Utils getUtils() {
        return this.utils;
    }
    
    public HookUtils getHookUtils() {
        return this.hookUtils;
    }
    
    public BucketManager getBucketManager() {
        return this.bucketManager;
    }
    
    public boolean serverIsBeforeFlattening() {
        return this.minecraftVersion < 13;
    }
    
    public boolean serverIsAfterOffhand() {
        return this.minecraftVersion > 8;
    }
}
