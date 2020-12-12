namespace DashSocietyF
{
    public class CommandHandler
    {
	public static void Helper()
	{
	    string message =
	    (
		"&e-========================================-\r\n" +
		"    &f( &cConsole Commands&f )\r\n" +
		"&f-==:  &6!help  &f-=-  &7SHOW THIS MESSAGE.\r\n" +
		"&f-==:  &6!cler  &f-=-  &7CLEAR THE CONSOLE.\r\n" +
		"&e-========================================-\r\n" +
		"    &f( &cUtilization Commands &f)\r\n" +
		"&f-==:  &6!ping  &f-=-  &7PING A HOST MAN.\r\n" +
		"&f-==:  &6!  &f-=-  &7\r\n" +
		"&e-========================================-\r\n" 
	    );

	    Tool.TranslateColors(message);
	}

	public static void Handler(string[] args)
	{
	    var a = args[0].ToLower();

	    if (a == "!help")
	    {
		Helper();
	    }

	    else if (a == "!cler" || a == "!cls" || a == "!clear")
	    {
		StartScreen.Show();
	    }

	    else if (a == "!ping")
	    {
		Pinger.Run(args);
	    };
	}
    }
}
