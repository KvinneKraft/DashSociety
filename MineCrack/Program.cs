

//
// Author: Dashie
// Version: 1.0
//


using System;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;


namespace MineCraft
{
    public partial class KvinneKraft : Form
    {
	public KvinneKraft()
	{

	}
    };


    public static class Program
    {
	private static readonly KvinneKraft kvinnekraft = new KvinneKraft();

	[STAThread]
	public static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);
	    Application.Run(kvinnekraft);
	}
    }
}
