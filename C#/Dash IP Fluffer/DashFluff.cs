

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
	private readonly PictureBox ipbox_container = new PictureBox();
	private readonly TextBox ipbox = new TextBox();
	private readonly Button check = new Button();
	private readonly Label iptext = new Label();

	private readonly PictureBox optional_button_container = new PictureBox();
	private readonly Button settings, attack, tools;

	public Interfuce()
	{
	    SetupLayout();

	    /*---[ Initializing Main Controls ]---*/
	    Add.RuImage(this, main_control_container, null, new Size(350, 100), Point.Empty);

	    try
	    {
		Add.RuImage(main_control_container, ipbox_container, null, new Size(175, 26), new Point(100, 1));

		ipbox_container.BackColor = Color.FromArgb(223, 194, 255);
		ipbox_container.Click += (s, e) => ipbox.Select();

		Add.ZeTextBox(ipbox_container, ipbox, Size.Empty, Point.Empty, Color.FromArgb(255, 255, 255), ipbox_container.BackColor, "255.255.255.255:65535", Get.FONT_TYPE_MAIN, 12);

		ipbox.Size = new Size(ipbox.PreferredSize.Width - 8, ipbox.PreferredSize.Height);
		ipbox.Location = new Point((ipbox_container.Width - ipbox.Width) / 2, (ipbox_container.Height - ipbox.Height) / 2);

		Add.ThaLabel(main_control_container, iptext, Point.Empty, Color.FromArgb(255, 255, 255), "Host:", Get.FONT_TYPE_CUTE, 15);
	    
		iptext.MaximumSize = new Size(50, ipbox_container.Height + 2);
		iptext.MinimumSize = new Size(50, ipbox_container.Height + 2);
		iptext.Location = new Point(ipbox_container.Left - iptext.Width, ipbox_container.Top - 1);

		iptext.TextAlign = ContentAlignment.MiddleCenter;
		iptext.BackColor = Get.menu_bar.BackColor;

		main_control_container.Paint += (s, e) => Add.Rectangle(e, Get.menu_bar.BackColor, 2, ipbox_container.Size, ipbox_container.Location);
	    }

	    catch (Exception e)
	    {
		ErrorHandler(e);
	    };
	    
	    main_control_container.Location = new Point((Width - main_control_container.Width) / 2, 45);

	    /*---[ Initializing Optional Buttons ]---*/
	    Add.RuImage(this, optional_button_container, null, new Size(320, 28), new Point(-1, -1));

	    optional_button_container.BackColor = BackColor;

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
