using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;

namespace DashSocietyF
{
    public class Pinger
    {
	public static void PingerHelp()
	{
	    string message =
	    (
		"&f-==:  &6-T  &8[TYPE]     &f-=-  &7THE PROTOCOL TO USE. PROTOCOL TO USE.  &8(TCP, UDP and ICMP)\r\n" +
		"&f-==:  &6-P  &8[INTEGER]  &f-=-  &7THE PORT TO USE.\r\n" +
		"&f-==:  &6-N  &8[INTEGER]  &f-=-  &7AMOUNT OF REQUESTS TO SEND.\r\n" +
		"&f-==:  &6-D  &8[INTEGER]  &f-=-  &7AMOUNT OF BYTES TO SEND PER REQUEST.\r\n" +
		"&f-==:  &6-L  &8[INTEGER]  &f-=-  &7REQUEST TIMEOUT.  &8(IN MILISECONDS)\r\n" +
		"&f-==:  &6-H  &8[HOST]     &f-=-  &7THE HOST TO PING.\r\n" +
		"&f-==:  &6-V             &f-=-  &7VALIDATE YOUR CONFIGURATION.\r\n" +
		"&f-==============================================-\r\n" +
		"&fSyntax: &6!ping -H &e8.8.8.8 &6-T &eTCP &6-P &e80 &6-N &e4 &6-D &e75 &6-L &e500\r\n"
	    );

	    Tool.TranslateColors(message);
	}

	public static string IPIsValid(string r_host)
	{
	    try
	    {
		string host = string.Empty;

		if (!IPAddress.TryParse(r_host, out IPAddress ham))
		{
		    if (!r_host.Contains("http://") && !r_host.Contains("https://")) r_host = "https://" + r_host;

		    if (!Uri.TryCreate(r_host, UriKind.RelativeOrAbsolute, out Uri bacon))
		    {
			Tool.TranslateColors("&8(&c-&8) &fInvalid URI or host specified.\r\n");
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

		    if (ham.AddressFamily != AddressFamily.InterNetwork)
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

	public static void Run(string[] args)
	{
	    try
	    {
		var para = args.ToList();

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
			    
			    if (!Tool.isInteger(d, out int b) || b < 1)
			    {
				Tool.TranslateColors("&8(&c-&8) &fYou must specify an integral value for the packet size!\r\n");
			    };

			    if (b > 800)
			    {
				Tool.TranslateColors("&8(&6!&8) &fYou have specified more than 800 bytes!  Are you sure you want to do this &7(Y/n)&f? ");

				switch (Console.ReadLine().ToString().ToLower())
				{ 
				    case "y":
					break;

				    default:
					Tool.TranslateColors("&8(&c!&8) &fSession aborted!");
					return;
				};
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
				    break;

				default:
				    Tool.TranslateColors("&8(&c-&8) &fYou must specify a valid protocol man.  &eTCP&f, &eUDP &for &eICMP!\r\n");
				    return;
			    };
			};

			if (para.Contains("-p"))
			{
			    p = Get("-p");
			    
			    if (!Tool.isInteger(p, out int b))
			    {
				Tool.TranslateColors("&8(&c-&8) &fInvalid integral value specified for &e-p\r\n");
				return;
			    };

			    if (b < 1 || b > 65535)
			    {
				Tool.TranslateColors("&8(&c-&8) &fPort must be in range &e1-65535&f!\r\n");
				return;
			    };
			};

			if (para.Contains("-l"))
			{
			    l = Get("-l");
			    
			    if (!int.TryParse(l, out int b) || b < 1)
			    {
				Tool.TranslateColors("&b(&c-&8) &fInvalid integral value specified for &e-l");
				return;
			    };
			};

			if (para.Contains("-v"))
			{
			    string message =
			    (
				$"&3(Current Settings)\r\n" +
				$"&3Host=&b{h}\r\n" +
				$"&3Port=&b{p}\r\n" +
				$"&3Protocol=&b{t}\r\n" +
				$"&3Requests=&b{n}\r\n" +
				$"&3Bytes=&b{d}\r\n" +
				$"&3Timeout=&b{l}\r\n" +
				$"&8(&6!&8) &fAre you satisfied with the above configuration (Y/n) "
			    );

			    Tool.TranslateColors(message);

			    switch (Console.ReadLine().ToString().ToLower())
			    {
				case "y":
				    break;

				default:
				    return;
			    };
			};

			//move this to previous switch statement
			var prot = ProtocolType.Tcp;
			var type = SocketType.Stream;

			switch (t)
			{
			    case "udp":
				prot = ProtocolType.Udp;
				type = SocketType.Dgram;
				break;
			    case "tcp":
				prot = ProtocolType.Tcp;
				break;
			};

			if (p == "none") p = "80";

			var port = int.Parse(p);
			var reqe = int.Parse(n);
			var data = int.Parse(d);
			var timo = int.Parse(l);
			var host = h;

			Tool.TranslateColors($"&8(&a+&8) &fStarted pinging &3{h}&b:&3{p} &fwith &3{data} &bbytes &fof &bdata &fper request...\r\n");
			
			for (int k = 0, r = 1; k < reqe; k += 1, r += 1)
			{
			    try
			    {
				using (var sock = new Socket(AddressFamily.InterNetwork, type, prot) { SendBufferSize = data })
				{
				    var timer = new Stopwatch();

				    timer.Start();

				    var resu = sock.BeginConnect(host, port, null, null);
				    var succ = resu.AsyncWaitHandle.WaitOne(timo, true);

				    if (sock.Connected)
				    {
					Tool.TranslateColors($"&8----: &3Received a reply from &b{h} &3req=&b{r} &3res=&b{timer.ElapsedMilliseconds}&3ms  &8---  &3(&b{r}&3/&b{reqe}&3)\r\n");

					if (prot != ProtocolType.Udp)
					{
					    sock.Disconnect(false);
					};
				    }

				    else
				    {
					Tool.TranslateColors($"&8----: &3Request to connect to &b{h} &3timed out.  &8-=-  &3(&b{r}&3/&b{reqe}&3)\r\n");
				    };

				    sock.Close();
				    timer.Stop();
				};
			    }

			    catch (Exception e)
			    {
				Console.WriteLine(e.Message);
				Console.ReadKey();
			    };
			};

			Tool.TranslateColors($"&8(&a+&8) &fFinished pinging &3{h}&b:&3{p} &f!\r\n");
		    };

		    return;
		};
	    }

	    catch { };

	    PingerHelp();
	}
    }
}
