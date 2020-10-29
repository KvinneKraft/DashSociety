using System;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("DashReminder")]
[assembly: AssemblyDescription("A structure is key.")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Dashies Softwaries")]
[assembly: AssemblyProduct("DashReminder")]
[assembly: AssemblyCopyright("Copyright © KvinneKraft 2020")]
[assembly: AssemblyTrademark("Kvinne Kraft")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("13f55364-c84b-4bfa-938d-68af16ef7035")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace DashReminder
{
    static class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(true);
	    
	    void SplashTimer()
	    {
		// Show Splash Dialog

		var timer = new System.Timers.Timer()
		{
		    Interval = 2000,
		    Enabled = true,
		};

		timer.Elapsed += (s, e) =>
		{
		    // Hide Splash Dialog
		};

		timer.Start();
	    };

	    SplashTimer();

	    void InitializeApp()
	    {
		var dapp = new DashApp();
		Application.Run(dapp);
	    };

	    InitializeApp();
	}
    }
}
