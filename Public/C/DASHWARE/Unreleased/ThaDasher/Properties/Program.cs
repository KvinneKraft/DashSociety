using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThaDasher
{
    static class Program
    {
	private static Interface inern;

	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    inern = new Interface();

	    Application.Run(inern);
	}
    }
}
