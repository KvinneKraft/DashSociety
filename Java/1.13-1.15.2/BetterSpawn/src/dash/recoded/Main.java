package dash.recoded;

//
// I am very busy with life right now so, yes, updates are coming through quite slowely, but do not worry
// they will be shooting all over the place like a turd flying out of my anus when I am back at my computer
// like I was before I got busy.
//

public class Main extends JavaPlugin implements Listener,CommandExecutor
{
    @Override public void onEnable()
    {
        print("I am crawling up ....");

        LoadConfiguration();

        print("I am now alive!");
    };

    FileConfiguration config = (FileConfiguration) null;
    final JavaPlugin plugin = (JavaPlugin) this;

    void LoadConfiguration()
    {
        plugin.reloadConfig();
        config = (FileConfiguration) plugin.getConfig();


    };

    class Events
    {
        @EventHandler public void onPlayerJoin(final PlayerJoinEvent e)
        {
            // Join Message, Teleport to spawn, always teleport to spawn, effects, first gifts, rewards
        };

        @EventHandler public void onPlayerQuit(final PlayerQuitEvent e)
        {
            // Quit Message
        };

        @EventHandler public void onPlayerDeath(final PlayerDeathEvent e)
        {
            // Teleport to Spawn or player home if exists
        };
    };

    class Commands
    {
        @Override public boolean onCommand(final CommandSender s, final Command c, final String a, final String[] as)
        {
            if (!(s instanceof Player))
            {
                print("Executor != Player == true");
                print("Access Denied!");
                return;
            };

            final Player p = (Player) s;
            
            // SetSpawn, GoSpawn & Spawn and Reload

            return true;
        };
    };

    @Override public void onDisable()
    {
        print("Oh, I am now dead!");
    };

    void print(final String line)
    {
        System.out.println("(Better Spawn): " + line);
    };

    String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };
};