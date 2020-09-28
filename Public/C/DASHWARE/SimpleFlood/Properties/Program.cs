
// Author: Dashie
// Version: 4.0

using System;
using System.Windows.Forms;

namespace SimpleFlood
{
    static class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    SimpleFlood simpleFlood = new SimpleFlood();

	    Application.Run(simpleFlood);
	}
    }
}
