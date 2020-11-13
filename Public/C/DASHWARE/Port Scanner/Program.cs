
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;

namespace PortScanner
{
    class Program
    {
	public static readonly List<string> cmds = new List<string>();
	public static readonly List<string> args = new List<string>();

	static void ParseArguments(string[] _)
	{
	    foreach (string stri in _)
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
		case 2: dash_angel(); break;
		case 3: m = "The given string value is not a valid integral value."; break;
		case 4: m = ""; break;
	    };

	    Console.WriteLine($"[!]: {m}");
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
		ErrorHandler(3);
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

		    }

		    catch
		    {
			throw new Exception("1");
		    };
		};
	    }

	    catch
	    {
		ErrorHandler(4);
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

		// PortScan.exe --host 8.8.8.8 --ports 1,2,3,4,5 --timeout 100 
		// PortScan.exe --host 1.1.1.1 --ports 255-510 --timeout 200

		if (cmds.Contains("--ports") || cmds.Contains("--port-file"))
		{
		    print("Validating the specified host(s) ....");

		    var HOSTS = new List<string>();
		    var I1 = cmds.IndexOf("--host");

		    if (args[I1].Contains(","))
		    {
			print("Depending on the hosts specified, this may take a few seconds ....");

			foreach (string stri in args[I1].Split(','))
			    HOSTS.Add(ToHost(args[I1]));
		    }

		    else HOSTS.Add(ToHost(args[I1]));

		    print("OK!");
		    print("Validating the specified port(s) ....");

		    var PORTS = new List<int>();
		    var I2 = cmds.IndexOf("--ports");

		    if (args[I2].Contains(","))
		    {
			print("Depending on the ports specified, this may take a few seconds ....");

			foreach (string port in args[I2].Split(','))
			    PORTS.Add(GetInteger(port));
		    }

		    else if (args[I2].Contains("-"))
		    {
			var set = args[I2].Split('-');

			var a1 = GetInteger(set[0]);
			var a2 = GetInteger(set[1]);
			
			print("Depending on the port range, this may take a few seconds ....");

			for (int k = a1; k <= a2; PORTS.Add(k), k += 1);
		    }

		    else PORTS.Add(GetInteger(args[I2]));

		    PORTS.RemoveAll(x => x < 0 || x > 65535);

		    print("OK!");

		    foreach (int str in PORTS) print($"{str}");
		    foreach (string str in HOSTS) print(str);

		    // Optional Arguments?

		    return;
		};
	    };

	    ErrorHandler(2);
	}

	public static readonly string dash_message =
	(
	    "" +
	    "" +
	    "" +
	    ""
	);

	static void dash_angel() =>
	    Console.WriteLine(dash_message);
    }
}
