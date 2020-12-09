using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DashSocietyF
{
    public class Pinger
    {
	public static void PingerHelp()
	{
	    string message =
	    (
		"&f-==:  &6-T  &8[TYPE]     &f-=-  &7THE PROTOCOL TO USE. PROTOCOL TO USE. &8(TCP, UDP and ICMP)\r\n" +
		"&f-==:  &6-P  &8[INTEGER]  &f-=-  &7THE PORT TO USE. &8(ONLY WORKS FOR TCP AND UDP)\r\n" +
		"&f-==:  &6-N  &8[INTEGER]  &f-=-  &7AMOUNT OF REQUESTS TO SEND.\r\n" +
		"&f-==:  &6-D  &8[INTEGER]  &f-=-  &7AMOUNT OF BYTES TO SEND PER REQUEST.\r\n" +
		"&f-==:  &6-L  &8[INTEGER]  &f-=-  &7REQUEST TIMEOUT.\r\n" +
		"&f-==:  &6-H  &8[HOST]     &f-=-  &7THE HOST TO PING.\r\n" +
		"&f-==============================================-\r\n" +
		"&fSyntax: &6!ping -H &e8.8.8.8 &6-T &eTCP &6-P &e80 &6-N &e4 &6-D &e74\r\n"
	    );

	    Tool.TranslateColors(message);
	}

	static string IPIsValid(string r_host)
	{
	    try
	    {
		string host = string.Empty;

		if (!IPAddress.TryParse(r_host, out IPAddress ham))
		{
		    if (!r_host.Contains("http://") && !r_host.Contains("https://")) r_host = "https://" + r_host;

		    if (!Uri.TryCreate(r_host, UriKind.RelativeOrAbsolute, out Uri bacon))
		    {
			Tool.TranslateColors("&8(&c-&8) &fInvalid URI specified.\r\n");
			return null;
		    };

		    try
		    {
			host = Dns.GetHostAddresses(bacon.Host)[0].ToString();
		    }

		    catch
		    {
			Tool.TranslateColors("&8(&c-&8) &fInvalid host specified.\r\n");
			return null;
		    };
		}

		else
		{
		    host = ham.ToString();

		    if (ham.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
		    {
			Tool.TranslateColors("&8(&c-&8) &fInvalid IPv4 specified.\r\n");
			return null;
		    };
		};

		return host;
	    }

	    catch
	    {
		Tool.TranslateColors("&8(&c-&8) &fInvalid host specified.\r\n");
		return null;
	    };
	}
	
	// Make a method for the INT validations.

	public static void Run(string[] args)
	{
	    try
	    {
		var para = args.ToList<string>();

		para.RemoveAt(0);

		for (int k = 0; k < para.Count; k += 1)
		    para[k] = para[k].ToLower();

		string Get(string a) =>
		    para[para.IndexOf(a) + 1].ToLower();

		if (para.Contains("-h"))
		{
		    string h = IPIsValid(Get("-h"));

		    if (h != null)
		    {
			var n = "4";
			var d = "75";
			var t = "icmp";
			var p = "none";
			var l = "500";

			if (para.Contains("-n"))
			{
			    n = Get("-n");
			};

			if (para.Contains("-d"))
			{
			    d = Get("-d");

			    try
			    {
				if (!int.TryParse(d, out int b))
				{
				    throw new Exception("!");
				};

				if (b > 800)
				{
				    Tool.TranslateColors("&8(&6!&8) &fYou have specified more than 800 bytes!  Are you sure you want to do this &7(Y/n)&f? ");

				    switch (Console.ReadKey().ToString().ToLower())
				    {
					case "y":
					    break;

					default:
					    Tool.TranslateColors("&8(&c!&8) &fSession aborted!");
					    return;
				    };
				};
			    }

			    catch
			    {
				Tool.TranslateColors("&8(&c-&8) &fYou must specify an integral value for the packet size!\r\n");
				return;
			    };
			};

			if (para.Contains("-t"))
			{
			    t = Get("-t");

			    switch (t.ToLower())
			    {
				case "icmp":
				case "tcp":
				case "udp":
				    Tool.TranslateColors($"&8(&a+&8) &fUsing &e{t.ToUpper()} &fnow!\r\n");
				    break;
				default:
				    Tool.TranslateColors("&8(&c-&8) &fYou must specify a valid protocol man.  &eTCP&f, &eUDP &for &eICMP!\r\n");
				    return;
			    };
			};

			if (para.Contains("-p"))
			{
			    p = Get("-p");

			    if (t == "icmp")
			    {
				Tool.TranslateColors("&8(&c-&8) &fYou may not use port specification when not using &eUDP &for &eTCP&f!\r\n");
				return;
			    };

			    try
			    {
				if (!int.TryParse(p, out int b))
				{
				    throw new Exception("!");
				};

				if (b < 1 || b > 65535)
				{
				    Tool.TranslateColors("&8(&c-&8) &fPort must be in range &e1-65535&f!\r\n");
				    return;
				};
			    }

			    catch
			    {
				Tool.TranslateColors("&8(&c-&8) &fInvalid integral value specified for &e-p\r\n");
				return;
			    };
			};

			if (para.Contains("-l"))
			{
			    l = Get("-l");
			    
			    try
			    {
				if (!int.TryParse(l, out int b))
				{
				    throw new Exception("!");
				};
			    }

			    catch
			    {
				Tool.TranslateColors("&b(&c-&8) &fInvalid integral value specified for &e-l");
			    };
			};

			string message =
			(
			    $"&3| :======:&cHOST&3> &e{h}\r\n" +
			    $"&3| :======:&cPORT&3> &e{p}\r\n" +
			    $"&3| :==:&cPROTOCOL&3> &e{t}\r\n" +
			    $"&3| :==:&cREQUESTS&3> &e{n}\r\n" +
			    $"&3| :=====:&cBYTES&3> &e{d}\r\n" +
			    $"&3| :===:&cTIMEOUT&3> &e{l}\r\n" +
			    $"&8(&6!&8) &fAre you satisfied with the above configuration (Y/n) "
			);

			Tool.TranslateColors(message);

			switch (Console.ReadKey().ToString().ToLower())
			{
			    case "y":
				break;
			    default:
				return;
			};
		    };

		    return;
		};
	    }

	    catch { };

	    PingerHelp();
	}
    }
}
