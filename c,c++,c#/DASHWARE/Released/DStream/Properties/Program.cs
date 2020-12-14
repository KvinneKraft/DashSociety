using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DStream
{
    static class Program
    {
	private static DStream App;

	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    App = new DStream();

	    Application.Run(App);
	}
    }
}
