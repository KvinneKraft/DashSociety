
using System;
using System.Windows.Forms;

namespace Lunarilicious
{
    class Program
    {
	[STAThread]
	static void Main(string[] args)
	{
	    Lunaroc lunaroc = new Lunaroc();

	    Application.Run(lunaroc);
	}
    }
}
