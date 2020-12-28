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
		"&f-==:  &6!ping      &f-=-  &7PING A HOST MAN.\r\n" +
		"&f-==:  &6!mail      &f-=-  &7SPAM EMAILS MAN.\r\n" +
		"&f-==:  &6!portscan  &f-=-  &7SCAN FOR OPEN PORTS ON HOST." +
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
	    }

	    else if (a == "!mail")
	    {
		Mailer.Run(args);
	    }

	    else if (a == "!portscan")
	    {
		PortScanner.Run(args);
	    }
	}
    }
}
