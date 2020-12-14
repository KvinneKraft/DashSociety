
// This code as of now is horrific.  
// The newest source files will have a better layout.
// Holy fuck.

// Author: Dashie
// Version: 1.0

using System;
using System.Windows.Forms;

namespace DashDownloader
{
    static class Program
    {
	private static DashDownloader DDownloader = null;

	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    DDownloader = new DashDownloader();

	    Application.Run(DDownloader);
	}
    }
}
