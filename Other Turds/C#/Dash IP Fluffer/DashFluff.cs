// Author: Dashie
// Version: 1.0

using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;


namespace Dash_IP_Fluffer
{
    static class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(false);

	    using (Greeting greeting = new Greeting())
	    {
		greeting.ShowDialog();
	    };

	    Application.Run(new Interfuce());
	}
    };

    public partial class Interfuce : Form
    {
	public static Interfuce interfuce;

	private void SetupLayout()
	{
	    BackColor = Color.FromArgb(253, 255, 194); // Color.FromArgb(12, 5, 28); 

	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterScreen;

	    Icon = (Icon)Properties.Resources.icon;

	    Size siz = new Size(400, 350);

	    Size = siz;
	    MinimumSize = siz;
	    MaximumSize = siz;

	    interfuce = (Interfuce) this;
	    
	    Add.InteractiveToolBar(this);
	}

	public static readonly MonitorLog monitor_log = new MonitorLog();

	public Interfuce()
	{
	    SetupLayout();

	    Primary.Initialize();
	    MonitorLog.Initialize();
	    new Secondary().Initialize();


	}

	public static void ErrorHandler(Exception e)
	{
	    MessageBox.Show($"Hey there, we are sorry to say but a fatal error has occurred, you may restart the application or simply continue by pressing OK.\r\n\r\nPlease know that this may cause issues in the future.\r\n\r\nDetailed Stack Trace:\r\n{e.ToString()}\r\n\r\nPlease contact the Developer at KvinneKraft@protonmail.com!", "Software Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
	}
    };
};
