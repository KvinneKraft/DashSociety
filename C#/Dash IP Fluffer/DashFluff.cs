

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
	    BackColor = Color.FromArgb(36, 21, 71); // Color.FromArgb(12, 5, 28); 

	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterScreen;

	    Icon = (Icon)Properties.Resources.icon;

	    Size siz = new Size(550, 300);

	    Size = siz;
	    MinimumSize = siz;
	    MaximumSize = siz;

	    Add.ControlBorder((Form) this, 10);
	    Add.InteractiveToolBar(this);
	}

	private readonly PictureBox ip_addr_container = new PictureBox();
	private readonly PictureBox port_container = new PictureBox();

	private readonly TextBox info_log = new TextBox();
	private readonly TextBox ip_addr = new TextBox();
	private readonly TextBox port = new TextBox();

	private readonly Button send_it = new Button();

	private readonly Label separator_label = new Label();
	private readonly Label host_label = new Label();

	private readonly string default_log_message =
	(
	    $"------------------ \r\n" +
	    $"  Hey there {Environment.UserName} !\r\n" +
	    $"------------------------------ \r\n" +
	    $"  This application possesses powers nobody without the right intentions should ever make use of, therefor am I going to say it right here, right now, that I will not be responsible for anything that you do with this thing when it comes down to illegal actions.\r\n" +
	    $"--------------------------- \r\n" +
	    $"  All the rights of this piece of ponyware are reserved to the Dashie, the one and only, hereby are you prohibited from reproducing, reselling or in any way shape or form put it out on the market.\r\n" +
	    $"------------------------------ \r\n" +
	    $"  For now there are no short-cut keys, they will be implemented soon, for now you will have to do it all by clicking buttons and stuff.\r\n" +
	    $"------------------ \r\n\r\n"
	);

	public Interfuce()
	{
	    SetupLayout();

	    Add.ZeTextBox(this, info_log, new Size(275, Height - 28), new Point(Width - 276, 27), Color.FromArgb(255, 255, 255), Color.FromArgb(12, 5, 28), default_log_message, 9);

	    info_log.ScrollBars = ScrollBars.Vertical;
	    info_log.Multiline = true;
	    info_log.ReadOnly = true;

	    Add.AButton(this, send_it, new Size(175, 28), new Point(((Width - info_log.Width - 1) - 175) / 2, Height - 45), info_log.BackColor, Color.FromArgb(255, 255, 255), "Attack!!", 10);
	    Add.ControlBorder((Button) send_it, 10);

	    Add.ThaLabel(this, host_label, new Point(10, Get.menu_bar.Height + 18), Color.FromArgb(255, 255, 255), "Victim:", 12);

	    Add.RuImage(this, ip_addr_container, null, new Size(126, 26), new Point(host_label.Width + host_label.Left - 5, host_label.Top - 2)); ip_addr_container.BackColor = info_log.BackColor;
	    Add.ZeTextBox(ip_addr_container, ip_addr, new Size(ip_addr_container.Width - 6, 20), new Point(3, 3), Color.FromArgb(255, 255, 255), info_log.BackColor, "255.255.255.255", 12); ip_addr.TextAlign = HorizontalAlignment.Center; ip_addr_container.BringToFront(); ip_addr.MaxLength = 15;

	    Add.ControlBorder((PictureBox) ip_addr_container, 1);
	    Add.ControlBorder((TextBox) ip_addr, 1);

	    Add.ThaLabel(this, separator_label, new Point(ip_addr_container.Left + ip_addr_container.Width - 2, ip_addr_container.Top - 6), Color.FromArgb(255, 255, 255), ":", 20);

	    Add.RuImage(this, port_container, null, new Size(52, 26), new Point(ip_addr_container.Width + ip_addr_container.Left + 12, ip_addr_container.Top)); port_container.BringToFront(); port_container.BackColor = info_log.BackColor;
	    Add.ZeTextBox(port_container, port, new Size(port_container.Width - 6, 20), new Point(3, 3), Color.FromArgb(255, 255, 255), info_log.BackColor, "65535", 12); port.MaxLength = 5;

	    Add.ControlBorder((PictureBox) port_container, 1);
	    Add.ControlBorder((TextBox) port, 1);

	    Paint += (s, e) =>
	    {
		Add.Rectangle(e, Get.menu_bar.BackColor, 2, new Size((Width - info_log.Width) - 1, Height - Get.menu_bar.Height - 1), new Point(0, Get.menu_bar.Height));
		Add.Rectangle(e, Get.menu_bar.BackColor, 2, info_log.Size, info_log.Location);
	    };

	    InitializeSettingsMenus();
	}

	private readonly PictureBox settings_container = new PictureBox();
	private readonly PictureBox module_container = new PictureBox();

	private static int current_method = 0;

	private readonly Dictionary<int, List<Control>> option_controls = new Dictionary<int, List<Control>>();
	
	private void InitializeSettingsMenus()
	{
	    Size size = new Size(((Width - info_log.Width) - 25) / 2, 153);

	    Add.RuImage(this, settings_container, null, size, new Point(10, Get.menu_bar.Height + 58)); settings_container.BackColor = ip_addr.BackColor;
	    Add.RuImage(this, module_container, null, size, new Point(settings_container.Left + settings_container.Width + 5, settings_container.Top)); module_container.BackColor = port.BackColor;

	    settings_container.Paint += (s, e) =>
	    {
		Add.Rectangle(e, Get.menu_bar.BackColor, 2, size, Point.Empty);
	    };

	    module_container.Paint += (s, e) =>
	    {
		Add.Rectangle(e, Get.menu_bar.BackColor, 2, size, Point.Empty);
	    };

	    Add.ControlBorder((PictureBox) settings_container, 6);
	    Add.ControlBorder((PictureBox) module_container, 6);

	    // Add TCP, UDP and HTTP options by list to option_controls, 0 = TCP, 1 = UDP and 2 = HTTP;
	    // option_controls[current_method] = new List<Button>() { new Button(), new Button() ... };
	}
    };
};
