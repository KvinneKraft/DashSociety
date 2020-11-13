
// Author: Dashie
// Version: 1.0

using System;
using System.Windows.Forms;

namespace DashDownloader
{
    static class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(true);

	    var DDownloader = new DashDownloader();

	    Application.Run(DDownloader);
	}
    }
}
