

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

	private readonly PictureBox main_control_container = new PictureBox();
	private readonly TextBox ipbox = new TextBox();
	private readonly Button check = new Button();

	private readonly PictureBox optional_button_container = new PictureBox();
	private readonly Button settings, attack, tools;

	public Interfuce()
	{
	    SetupLayout();

	    /*---[ Initializing Main Controls ]---*/
	    

	    /*---[ Initializing Optional Buttons ]---*/
	    Add.RuImage(this, optional_button_container, null, new Size(320, 28), new Point(-1, -1));

	    optional_button_container.BackColor = BackColor;
	    optional_button_container.BringToFront();

	    try
	    {
		List<Button> buttons = new List<Button>()
		{
		    settings, attack, tools
		};

		List<string> labels = new List<string>()
		{
		    "Settings", "Attack", "Tools"
		};

		Color button_back_color = Get.menu_bar.BackColor;//Color.FromArgb(255, 255, 255);
		Color button_fore_color = Color.FromArgb(255, 255, 255);

		for (int x = 0, key = 0; key < buttons.Count; x += 110, key += 1)
		{
		    buttons[key] = new Button();

		    Add.AButton(optional_button_container, (Button)buttons[key], new Size(100, 28), new Point(x, 0), button_back_color, button_fore_color, labels[key], Get.FONT_TYPE_MAIN, 10);
		    Add.ControlBorder(buttons[key], 8);
		};
	    }

	    catch (Exception e)
	    {
		ErrorHandler(e);
	    };
	}

	public static void ErrorHandler(Exception e)
	{
	    MessageBox.Show($"Hey there, we are sorry to say but a fatal error has occurred, you may restart the application or simply continue by pressing OK.\r\n\r\nPlease know that this may cause issues in the future.\r\n\r\nDetailed Stack Trace:\r\n{e.ToString()}\r\n\r\nPlease contact the Developer at KvinneKraft@protonmail.com!", "Software Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
	}
    };
};
