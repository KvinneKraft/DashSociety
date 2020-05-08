

// Author: Dashie
// Version: 1.0


// Add Dark and or Light Borders to Objects
// 


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
	    Application.Run(new Interfuce());
	}
    };

    public partial class Interfuce : Form
    {
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
	    
	    Add.InteractiveToolBar(this);
	}

	public Interfuce()
	{
	    SetupLayout();

	    Label l = new Label();

	    Add.ThaLabel(this, l, new Point(-1, -1), Color.FromArgb(0, 0, 0), "You know this looks fancy ;)", Get.FONT_TYPE_CUTE, 16);
	}
    };
};
