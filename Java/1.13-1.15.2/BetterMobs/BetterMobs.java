
// Author: Dashie
// Version: 1.0

package dash.recoded;

public class BetterMobs extends JavaPlugin implements Listener,CommandExecutor
{
    @Override public void onEnable()
    {
        print("The plugin is loading ....");

        LoadConfiguration();

        getCommand("bettermobs").setExecutor(this);
        getServer().getPluginManager().registerEvents(this, plugin);

        print("The plugin has been loaded!");
    };

    private FileConfiguration config = (FileConfiguration) null;
    private final JavaPlugin plugin = (JavaPlugin) this;

    private void LoadConfiguration()
    {
        saveDefaultConfig();

        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();

        
    };

    @Override public void onDisable()
    {
        print("The plugin has been disabled!");
    };

    private void print(final String line)
    {
        System.out.println("(Better Mobs): " + line);
    };

    private String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
};