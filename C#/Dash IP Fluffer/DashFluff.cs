

// Author: Dashie
// Version: 1.0


using System;
using System.Text;
using System.Drawing;
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
	private void SetupLayout()
	{
	    BackColor = Color.FromArgb(24, 24, 24);

	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterScreen;

	    Icon = (Icon)Properties.Resources.icon;

	    Size siz = new Size(350, 300);

	    Size = siz;
	    MinimumSize = siz;
	    MaximumSize = siz;

	    Add.InteractiveToolBar(this);
	}

	public Interfuce()
	{
	    SetupLayout();
	}
    };
};
