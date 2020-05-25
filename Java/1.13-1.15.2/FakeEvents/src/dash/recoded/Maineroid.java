package dash.recoded;

public class Maineroid extends JavaPlugin implements CommandExecutor
{
    @Override public void onEnable()
    {
        print("I am getting up ....");

        

        print("I am now alive!");
    };

    @Override public void onDisable()
    {
        print("I am no longer alive!");
    };


    private String color(final String line)
    {
        return ChatColor.translateAlternateColorCodes('&', line);
    };

    private void print(final String line)
    {
        System.out.println("(Fake Events): " + line);
    };
};