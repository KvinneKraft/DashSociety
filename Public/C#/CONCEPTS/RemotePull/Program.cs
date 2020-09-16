
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Security.Principal;
using System.Runtime.InteropServices;

// F#ck the rules, who their rules? I will just poop it out right here ;p
[assembly: AssemblyTitle("RemotePull")]
[assembly: AssemblyDescription("KvinneKraft Dashies Softwaries. Yes.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Dashies Softwaries")]
[assembly: AssemblyProduct("RemotePull")]
[assembly: AssemblyCopyright("Copyright © KvinneKraft 2020")]
[assembly: AssemblyTrademark("Dashies Softwaries")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("07ec0010-b106-4963-91cc-e67d17fe00ee")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace RemotePull
{
    class Program
    {
	public static bool IsAdministrator() 
	{
	    using (WindowsIdentity id = WindowsIdentity.GetCurrent())
	    {
		WindowsPrincipal princess = new WindowsPrincipal(id);

		if (princess.IsInRole(WindowsBuiltInRole.Administrator))
		{
		    return true;
		};
	    };

	    return false;
	}

	static string DescriptionData =
	(
	    "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\n" +
	    " Hey there, if you are stuck simply type \n" +
	    " some random  gibberish, the application \n" +
	    "          will show you da waey.         \n" +
	    "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-"
	);
	static void Description() => Console.WriteLine(DescriptionData);

	static string HelpData =
	(
	    "Available Commands: [cls | clear], [download <url>], [online <host>] or [launch <filepath>]" +
	    ") DOWNLOAD - Make sure that the U.R.L. has no spaces in it." +
	    ") ONLINE - Make sure that the destination server does not block your pings." +
	    ") LAUNCH - Make sure that you have the necessary file permissions."
	);
	static void HelpMe() => Console.WriteLine(HelpData);

	static bool IsOnline(string Host, int Type)
	{
	    if (Type == 0)
	    {
		if (Dns.GetHostAddresses(new Uri(Host).Host)[0].ToString().Length < 1)
		{
		    return false;
		};

		return true;
	    }
	    
	    else if (Type == 1)
	    {
		// IP and or URL
	    };

	    return false;
	}

	[STAThread]
	static void Main(string[] args)
	{
	    Description();

	    Console.Title = ("RemotePull 1.0 - KvinneKraft");

	    do
	    {
		Console.Write("(Program): ");

		string[] arg = Console.ReadLine().Split(' ');
		
		if (arg.Length > 0)
		{
		    string agg = arg[0].ToLower().Replace(" ", string.Empty);

		    if (agg.Equals("download") || agg.Equals("online") || agg.Equals("launch"))
		    {
			if (agg.Equals("download"))
			{
			    if (arg.Length < 2)
			    {
				Console.WriteLine("(!) Perhaps try something like 'download https://web.io/image.jpg' ?");
				continue;
			    };

			    Console.WriteLine("(-) Checking online status ....");

			    string host = arg[1];

			    for (int k = 2; k < arg.Length; host += "%20" + arg[k], k += 1) ;

			    try
			    {
				if (!IsOnline(host, 0))
				{
				    Console.WriteLine("(!) The given host appears to be offline!");
				    continue;
				};
			    }

			    catch
			    {
				Console.WriteLine($"(!) There was an unknown error while trying to connect to {host} !"); 
				continue;
			    };

			    Console.WriteLine("(!) Connection OK!");

			    string output = $@"{Environment.CurrentDirectory}\cache\";

			    using (SaveFileDialog Dialog = new SaveFileDialog())
			    {
				Dialog.Filter = "*.*|Any File";
				Dialog.Title = "Save Directory";

				Dialog.OverwritePrompt = true;
				Dialog.CheckPathExists = true;

				while (true)
				{
				    Dialog.ShowDialog();
				    
				    if (Dialog.FileName != string.Empty)
				    {
					break;
				    };
				};

				output = Dialog.FileName;
			    };

			    if (!Directory.Exists("cache"))
			    {
				Directory.CreateDirectory($@"{Environment.CurrentDirectory}\cache");
			    };

			    Console.WriteLine("(-) Retrieving data ....");

			    try
			    {
				using (var client = new WebClient())
				{
				    client.DownloadFile(host, output);

				    if (!File.Exists(output))
				    {
					throw new Exception("!");
				    };
				};

				Console.WriteLine("(!) Download OK!");
				Console.Write("(!) Would you like me to open the file for you (Y/n)? ");

				reselect:

				string key = Console.ReadKey().KeyChar.ToString().ToLower();

				if (key.Equals("y"))
				{
				    Console.WriteLine($"\n(-) Opening {output} ....");

				    using (Process proc = new Process())
				    {
					proc.StartInfo = new ProcessStartInfo()
					{
					    FileName = output
					};

					proc.Start();
				    };

				    Console.WriteLine("(!) OK!");
				    continue;
				}

				else if (key.Equals("n"))
				{
				    Console.WriteLine("");
				    continue;
				};

				goto reselect;
			    }

			    catch
			    {
				Console.WriteLine("(!) An error has occurred which has made me unable to retrieve your file.");
				Console.WriteLine("(!) Please ensure that you have the required permissions and have not mistyped your U.R.L.");
			    };

			    continue;
			}

			else if (agg.Equals("online"))
			{
			    if (arg.Length < 2)
			    {
				Console.WriteLine("(!) Perhaps try something like 'ping https://www.google.com' ?");
				continue;
			    };

			    string host; // Do-thing

			    if (!IsOnline())
			    {

			    }

			    else
			    {

			    };

			    continue;
			}

			else if (agg.Equals("launch"))
			{
			    if (arg.Length < 2)
			    {
				Console.WriteLine($@"(!) Perhaps try something like 'launch C:\\Users\\{Environment.UserName}\\Desktop\\App.exe'");
				continue;
			    };



			    continue;
			};
		    }

		    else if (agg.Equals("cls") || agg.Equals("clear"))
		    {
			Console.Clear();
			Description();

			continue;
		    };
		};

		HelpMe();
	    }

	    while (true);
	}
    }
}
