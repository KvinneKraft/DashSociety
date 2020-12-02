
// Author: Dashie
// Version: 1.0

using System;

namespace DashCore
{
    public class Captcha
    {
	private readonly string charset = ("qwertyuiopasdfghjklzxcvbnm1234567890QWERTYUIOPASDFGHJKLZXCVBNM");
	private readonly int caplen = 8;

	private string request_key()
	{
	    string buffer = "";

	    Random rand = new Random();

	    for (int len = charset.Length - 1, id = 0; id < caplen; id += 1)
	    {
		buffer += charset[rand.Next(len)];
	    };

	    return buffer;
	}

	public void captcha()
	{
	    string genkey = request_key();

	    dush.say($"# Please enter these characters: {genkey}\n");

	    for (int tries = 0; tries < 4; tries += 1)
	    {
		dush.say("[Access Key]: ");

		string buffer = Console.ReadLine();

		if (buffer == genkey)
		{
		    dush.say("# Success, Press any key to continue!");
		    dush.halt();

		    break;
		}

		else if (tries + 1 > 3)
		{
		    dush.say("# You have entered the captcha wrong too many times.\n");
		    dush.say("# Press any key to exit this application.");
		    dush.halt();

		    Environment.Exit(-1);
		};

		dush.say("# invalid key!\n");
	    };
	}
    };
};