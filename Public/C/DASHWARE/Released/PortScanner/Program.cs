
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

namespace PortScanner
{
    class Program
    {
	public static readonly List<string> cmds = new List<string>();
	public static readonly List<string> args = new List<string>();

	static void ParseArguments(string[] _)
	{
	    foreach (var stri in _)
	    {
		if (stri.StartsWith("--") || stri.StartsWith("/"))
		{
		    cmds.Add(stri.ToLower());
		}

		else
		{
		    args.Add(stri);
		};
	    };
	}

	static void ErrorHandler(int k)
	{
	    string m = "";

	    switch ( k )
	    {
		case 1: m = "You can not specify --ports and --port-list at the same time."; break;
		case 2: m = "The given string value is not a valid integral value."; break;
		case 3: m = "The given host address is invalid and thus could not be converted."; break;
		case 4: m = "The specified timeout must be greater than 0."; break;
		case 5: m = "It appears that an error occurred while trying to scan one or more ports."; break;
		case 6: dash_angel(); break;
	    };

	    Console.WriteLine($"[!]: {m}");

	    if (k != 5)
		Environment.Exit(-1);
	}

	static int GetInteger(string _)
	{
	    try
	    {
		return int.Parse(_);
	    }

	    catch
	    {
		ErrorHandler(2);
	    };

	    return int.MinValue;
	}

	static void print(string _) =>
	    Console.WriteLine($"[+]: {_}");

	static string ToHost(string _)
	{
	    try
	    {
		try
		{
		    _ = IPAddress.Parse(_).ToString();
		}

		catch
		{
		    print($"Hm, {_} does not appear to be an IPv4 address.");
		    print("Let me try to convert it as a Url ....");

		    try
		    {
			if (!_.ToLower().Contains("http://") && !_.ToLower().Contains("https://"))
			{
			    print("Found no HTTP:// or HTTPS:// adding it ....");
			    _ = _.Insert(0, "http://");
			};

			var uri = new Uri(_);

			_ = Dns.GetHostAddresses(uri.Host)[0].ToString();
		    }

		    catch
		    {
			throw new Exception("1");
		    };
		};
	    }

	    catch
	    {
		ErrorHandler(3);
	    };

	    return _;
	}

	static void Main(string[] _)
	{
	    ParseArguments(_);
	    
	    if (cmds.Contains("--hosts") && cmds.Count == args.Count)
	    {
		if (cmds.Contains("--ports") && cmds.Contains("--port-file"))
		    ErrorHandler(1);

		if (cmds.Contains("--ports") || cmds.Contains("--port-file"))
		{
		    print("Validating the specified host(s) ....");

		    var HOSTS = new List<string>();
		    var I1 = cmds.IndexOf("--hosts");

		    if (args[I1].Contains(","))
		    {
			print("Depending on the hosts specified, this may take a few seconds ....");

			foreach (var stri in args[I1].Split(','))
			    HOSTS.Add(ToHost(stri));
		    }

		    else HOSTS.Add(ToHost(args[I1]));

		    print("OK!");
		    print("Validating the specified port(s) ....");

		    var PORTS = new List<int>();
		    var I2 = cmds.IndexOf("--ports");

		    if (args[I2].Contains(","))
		    {
			print("Depending on the ports specified, this may take a few seconds ....");

			foreach (var port in args[I2].Split(','))
			    PORTS.Add(GetInteger(port));
		    }

		    else if (args[I2].Contains("-"))
		    {
			var set = args[I2].Split('-');

			var a1 = GetInteger(set[0]);
			var a2 = GetInteger(set[1]);
			
			print("Depending on the port range, this may take a few seconds ....");

			for (int k = a1 - 1; k <= a2; PORTS.Add(k), k += 1);
		    }

		    else PORTS.Add(GetInteger(args[I2]));

		    PORTS.RemoveAll(x => x < 0 || x > 65535);
		    print("OK!");

		    var TIMEOUT = 500;

		    if (cmds.Contains("--timeout"))
		    {
			var I3 = cmds.IndexOf("--timeout");
			var TI = GetInteger(args[I3]);

			if (TI < 0)
			    ErrorHandler(4);

			if (TI > 5000)
			    print("Going for 5000ms or above may decrease performance drastically.");

			TIMEOUT = TI;
		    };

		    Console.Write("Do you want to see your configuration (Y/n)? ");

		    switch (Console.ReadLine().ToString().ToLower())
		    {
			case "y":
			{
			    var a1 = $"{args[I1].Replace(",", ", ")}";
			    var a2 = $"{args[I2].Replace(",", ", ")}";
			    var a3 = $"{TIMEOUT}ms";

			    var configuration =
			    (
				"[CURRENT CONFIGURATION]\r\n" +
				$"--HOST(s): {a1}\r\n" + 
				$"--PORT(s): {a2}\r\n" +
				$"--TIMEOUT: {a3}" 
			    );

			    Console.WriteLine($"{configuration}");
			    Console.Write("Press any key to continue.");

			    Console.ReadKey();
			
			    break;
			};
		    };

		    print("Starting ....");
		    
		    var OPENED = new Dictionary<int, List<int>>();
		    var CLOSED = new Dictionary<int, List<int>>();

		    var SOCK = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		    var ID = 0;

		    foreach (var host in HOSTS)
		    {
			print($"Started scanning {host} ....");

			var opene = new List<int>();
			var close = new List<int>();

			foreach (var port in PORTS)
			{
			    try
			    {
				if (PORTS.IndexOf(port) + 1 == PORTS.Count / 2)
				    print("Half way there, hang on!");

				var RESU = SOCK.BeginConnect(host, port, null, null);
				var SUCC = RESU.AsyncWaitHandle.WaitOne(TIMEOUT, true);

				if (SOCK.Connected)
				    opene.Add(port);
			    }

			    catch
			    {
				close.Add(port);
			    };

			    SOCK.Close();
			};

			OPENED.Add(ID, opene);
			CLOSED.Add(ID, close);

			print($"I am now done scanning {host} !");

			ID += 1;
		    };

		    print("Done!");

		    var options =
		    (
			"----[OUTPUT OPTIONS]\r\n" +
			" (a) Just show open ports.\r\n" +
			" (b) Just show closed ports.\r\n" +
			" (c) Show both open and closed ports."
		    );

		    Console.WriteLine($"{options}");

		    var opened = false;
		    var closed = false;

		    string resp = string.Empty;

		    while (resp != "a" && resp != "b" && resp != "c")
		    {
			Console.Write("(Option): ");
			resp = Console.ReadLine().ToLower();

			switch (resp)
			{
			    case "a":
			    {
				opened = true;
				break;
			    };

			    case "b":
			    {
				closed = true;
				break;
			    };

			    case "c":
			    {
				opened = true;
				closed = true;
				break;
			    };
			};
		    };

		    for (int k = 0; k < HOSTS.Count; k += 1)
		    {
			if (opened)
			{
			    print($"Loading opened ports for {HOSTS[k]} ....");

			    if (OPENED[k].Count > 500)
				print("This may take a few seconds, there are quite a lot of open ports.");

			    Console.Write("OPEN PORTS: [");

			    for (int __ = 0; __ < OPENED[k].Count; __ += 1)
			    {
				Console.Write($"{OPENED[k][__]}");

				if (__ < OPENED[k].Count - 1)
				    Console.Write("; ");
			    };

			    Console.Write("]\r\n");
			};

			if (closed)
			{
			    print($"Loading closed ports for {HOSTS[k]} ....");

			    if (CLOSED[k].Count > 500)
				print("This may take a few seconds, there are quite a lot of closed ports.");

			    Console.Write("CLOSED PORTS: [");

			    for (int __ = 0; __ < CLOSED[k].Count; __ += 1)
			    {
				Console.Write($"{CLOSED[k][__]}");

				if (__ < CLOSED[k].Count - 1)
				    Console.Write("; ");
			    };

			    Console.Write("]\r\n");
			};
		    };

		    return;
		};
	    };

	    ErrorHandler(6);
	}

	public static readonly string dash_message =
	(
	    "\r\n" +
	    "\r\n" +
	    "\r\n" +
	    "\r\n"
	);

	static void dash_angel() =>
	    Console.WriteLine(dash_message);
    }
}
