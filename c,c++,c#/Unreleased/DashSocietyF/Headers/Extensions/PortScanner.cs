using System;
using System.Net;
using System.Linq;
using System.Net.Sockets;
using System.Diagnostics;

namespace DashSocietyF
{
    public class PortScanner
    {
	public static void Run(string[] args)
	{
	    if (args.Length <= 1)
	    {
		Process.Start("");
		// PortScannerHelp();
		return;
	    };

	    var para = args.ToList();

	    para.RemoveAt(0);

	    for (int k = 0; k < para.Count; k += 1)
		para[k] = para[k].ToLower();

	    string Get(string a) =>
		para[para.IndexOf(a) + 1].ToLower();

	    if (para.Contains("-h"))
	    {
		string h = Pinger.IPIsValid(Get("-h"));

		if (h != null)
		{
		    // declare parameter standard values.
		    // start obtaining argument values.
		    // ....
		};
	    };

	    //--: SEE IF GIVEN ARGUMENTS ARE IN LIST
	    //--: REDIRECT TO HELP MENU IF INVALID
		
	    //PortScannerHelp();
	}
    }
}
