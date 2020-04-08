
// Author: Dashie
// Version: 1.0

using System;
using System.Windows.Forms;

namespace Dash_IP_Fluffer
{
    static class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);
	    Application.Run(new Interfuce());
	}
    };

    public partial class Interfuce : Form
    {
	public Interfuce()
	{

	}
    };
};
