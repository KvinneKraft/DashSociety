
// Author: Dashie
// Version: 1.0

using System;
using System.Net;
using System.Windows.Forms;

namespace Uri_API
{
    class Program
    {
	private static bool isOnline(string url)
	{
	    try
	    {
		bool result = Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
		    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

		HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriResult);

		request.Timeout = 1000;
		request.Method = "GET";

		using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
		    result = (response.StatusCode == HttpStatusCode.OK);

		return result;
	    }

	    catch
	    {
		return false;
	    };
	}

	[STAThread]
	static void Main(string[] args)
	{
	    if (args.Length > 0)
	    {
		Console.WriteLine("(-) You may only use this application directly, it does not yet support command arguments through the command line :P");
		Environment.Exit(-1);
	    };

	    Console.Title = ("Simplified File Downloader - Kvinne Kraft (10 Minute Project)");

	    Console.BackgroundColor = ConsoleColor.Black;
	    Console.ForegroundColor = ConsoleColor.Green;

	    Console.Clear();

	    Console.WriteLine("(!) Hey there, me Dashie has made this thing in like 10 minutes");
	    Console.WriteLine("(!) at maximum, which means that it is a fast project. I hope that");
	    Console.WriteLine("(!) you find it useful. -Dashie )o(\r\n");

	    retype_url:

	    string url = string.Empty, outpath = string.Empty;

	    Console.Write("(File Url): ");
	    url = (string) Console.ReadLine();

	    if (!isOnline(url))
	    {
		Console.WriteLine("(-) The specified host appears to be offline, please try again.");
		goto retype_url;
	    };

	    reselect_location:

	    using (SaveFileDialog dialog = new SaveFileDialog())
	    {
		dialog.CheckPathExists = true;
		dialog.DefaultExt = ("*.*");
		dialog.FileName = ("index.html");
		dialog.Title = ("Save File As");

		dialog.ShowDialog();

		if (dialog.FileName.Length < 3)
		{
		    Console.WriteLine("(-) The specified file path is invalid!");
		    goto reselect_location;
		};

		using (WebClient client = new WebClient())
		{
		    client.DownloadFile($"{url}", $"{dialog.FileName}");

		    if (!System.IO.File.Exists(dialog.FileName))
		    {
			Console.WriteLine($"(-) The file could not be downloaded to {dialog.FileName} , please retry!");
		    }

		    else
		    {
			Console.WriteLine($"(+) The file has been downloaded to {dialog.FileName} !");
		    };
		};
	    };

	    Console.Write("(?) Go ahead and download another file [Y/n]? ");

	    switch (Console.ReadKey().KeyChar)
	    {
		case 'Y':
		case 'y':
		{
		    Console.WriteLine("");
		    goto retype_url;
		};

		default:
		{
		    return;
		};
	    };
	}
    }
}
