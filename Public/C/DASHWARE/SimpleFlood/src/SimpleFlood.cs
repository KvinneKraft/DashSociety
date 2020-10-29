
// Author: Dashie
// Version: 5.0

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SimpleFlood.Parts;

[assembly: AssemblyTitle("SimpleFlood")]
[assembly: AssemblyDescription("Pony Flooder")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Dashies Softwaries")]
[assembly: AssemblyProduct("SimpleFlood")]
[assembly: AssemblyCopyright("Copyright © KvinneKraft 2020")]
[assembly: AssemblyTrademark("KvinneKraft")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("8da9673b-2139-4248-94f1-fecabe807ee8")]
[assembly: AssemblyVersion("5.0.0.0")]
[assembly: AssemblyFileVersion("5.0.0.0")]

namespace SimpleFlood
{
    // Put things into separate classes
    public partial class SimpleFlood : Form
    {
	readonly PictureBox SPECIFICATION_AREA = new PictureBox();

	private void ResetLaunchLogger() => LaunchLogger.Text = new string[] 
	{ 
	    "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n" + 
	    "Hey there, this tool was made for personal use and educational purposes only.\r\n\r\n" +

	    "Any in proper use of this application is considered to be your responsibility and thus makes me unable to be held responsible for such.\r\n\r\n" +
	    
	    "Remember that things are not more to us than that what we think of such. -Dashie\r\n" +
	    "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n" +
	    "[Status]: Waiting ....\r\n"
	} [0];

	public static void ErrorHandler(string stack)
	{
	    MessageBox.Show($"Oh no, it seems as if my application has ran into an issue.\n\nThe error has caused the application to malfunction, this may cause lasting effects during its runtime.\n\nIf you would like to help me out with fixing this bug then please send the following to me at KvinneKraft@protonmail.com\n\n<BEGIN type=\"STACK\">{stack}<END type=\"STACK\">\n\nThank you for helping if you did.\n\nIf you would like to continue with using this application press OK else CANCEL.", "Simple Flood 5.0", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
	}

	public static Form CTR;

	readonly ABOUT ABOUT_FUNCTION = new ABOUT();
	readonly MODE MODE_FUNCTION = new MODE();

	readonly PictureBox TargetArea = new PictureBox();
	readonly PictureBox ToolArea = new PictureBox();

	readonly Label HostText = new Label();

	public static readonly TextBox LaunchLogger = new TextBox();
	public static readonly TextBox HostBox = new TextBox();
	public static readonly TextBox PortBox = new TextBox();

	public static readonly Button LauncherButton = new Button();

	readonly Button OptionsButton = new Button();
	readonly Button ModesButton = new Button();
	readonly Button AboutButton = new Button();

	private void InterfaceSetup()
	{
	    Add.PictureBox(this, SPECIFICATION_AREA, null, new Size(Width / 2 - (2 + 25), Height - 3), new Point(2, 1), _bar.BackColor); CTR = this;

	    try
	    {
		Add.PictureBox(SPECIFICATION_AREA, TargetArea, null, new Size(SPECIFICATION_AREA.Width - 4, 100/*Update this*/), new Point(0, _bar.Top + _bar.Height + 20), _bar.BackColor);
		Add.Label(TargetArea, HostText, "Host", 14, 1, Size.Empty, new Point(0, 1), Color.Transparent, Color.White);

		Add.InputBox(TargetArea, HostBox, new Size(128, 27), new Point(HostText.Left + HostText.Width, 0), "DAG.DAG.DAG.DAG", 10, 1, Color.FromArgb(16, 16, 16), Color.White, _centerText:true); HostBox.Text = "255.255.255.255";  HostBox.TextAlign = HorizontalAlignment.Center;
		Add.InputBox(TargetArea, PortBox, new Size(60, 27), new Point(HostBox.Left + HostBox.Width + 55, 0), "25555", 10, 1, HostBox.BackColor, HostBox.ForeColor, _centerText:true);

		Mod.Border(TargetArea.Controls[TargetArea.Controls.Count - 2], 6);
		Mod.Border(TargetArea.Controls[TargetArea.Controls.Count - 1], 6);

		Mod.Resize(TargetArea, new Size(TargetArea.Width, HostBox.Top + HostBox.Height + 10));
	    }
	    catch (Exception e)
	    {
		ErrorHandler(e.StackTrace);
	    };

	    try
	    { 
		Size buttonSize = new Size(155, 27);

		int y = TargetArea.Top + TargetArea.Height + 25;
		int x = (SPECIFICATION_AREA.Width - 4 - buttonSize.Width) / 2;

		Add.PictureBox(SPECIFICATION_AREA, ToolArea, null, new Size(buttonSize.Width, buttonSize.Height * 3 + 16), new Point(x, y), _bar.BackColor);

		Color buttonColor = Color.MidnightBlue;

		Add.Button(ToolArea, OptionsButton, "Options", 12, 1, buttonSize, new Point(0, 0), buttonColor, Color.White);
		Add.Button(ToolArea, ModesButton, "Modes", 12, 1, buttonSize, new Point(0, 30), buttonColor, Color.White);
		Add.Button(ToolArea, AboutButton, "About", 12, 1, buttonSize, new Point(0, 60), buttonColor, Color.White);
		
		foreach (Control c in ToolArea.Controls)
		{
		    if (c is Button)
		    {
			Mod.Border(c, 4);
		    };
		};

		OptionsButton.Click += (s, e) =>
		{

		};

		ModesButton.Click += (s, e) =>
		{
		    if (MODE_FUNCTION.Visible)
		    {
			MODE_FUNCTION.Hide();
		    }

		    else
		    {
			MODE_FUNCTION.Show(this);
		    };
		};

		AboutButton.Click += (s, e) =>
		{
		    if (ABOUT_FUNCTION.Visible)
		    {
			ABOUT_FUNCTION.Hide();
		    }

		    else
		    {
			ABOUT_FUNCTION.Show(this);
		    };
		};
	    }
	    catch (Exception e)
	    {
		ErrorHandler(e.StackTrace);
	    };

	    try
	    {
		Add.Button(SPECIFICATION_AREA, LauncherButton, "Initiate", 11, 1, new Size(180, 26), new Point((SPECIFICATION_AREA.Width - 4 - 180) / 2, ToolArea.Top + ToolArea.Height + 21), OptionsButton.BackColor, Color.White);
		Mod.Border(LauncherButton, 4);

		LauncherButton.Click += (s, e) =>
		{
		    if (LauncherButton.Text == "Initiate")
		    {
			LAUNCH.Start();
		    }

		    else
		    {
			LAUNCH.Stop();
		    };
		};

		Add.InputBox(this, LaunchLogger, new Size(Width - (SPECIFICATION_AREA.Width + 2), Height - (_bar.Height + 3)), new Point(SPECIFICATION_AREA.Left + SPECIFICATION_AREA.Width - 2, _bar.Height + 1), "sdogpfdjkgjkg", 10, 1, Color.MidnightBlue, Color.FromArgb(247, 255, 163), _readOnly:true, _centerText:false, _border:false, Color.Empty);

		LaunchLogger.ScrollBars = ScrollBars.Vertical;
		LaunchLogger.Multiline = true;

		ResetLaunchLogger();

		Add.PictureBox(SPECIFICATION_AREA, new PictureBox(), null, new Size(3, Height), new Point(LaunchLogger.Left - 3, 0), LaunchLogger.BackColor);
	    }
	    catch (Exception e)
	    {
		ErrorHandler(e.StackTrace);
	    };

	    SPECIFICATION_AREA.Paint += (s, e) =>
	    {
		int w = 120;
		int x = (SPECIFICATION_AREA.Width - 4 - w) / 2;
		int y = TargetArea.Top + TargetArea.Height + 10;

		Mod.Line(e, Color.MidnightBlue, 1, new Point(x, y), new Point(x + w, y));

		y = ToolArea.Top + ToolArea.Height + 5;

		Mod.Line(e, Color.MidnightBlue, 1, new Point(x, y), new Point(x + w, y));
	    };

	    void AddListener(Control control)
	    {
		control.KeyDown += (s, e) =>
		{
		    switch (e.KeyData)
		    {
			case Keys.Enter: LauncherButton.PerformClick();  break;
			case Keys.O: /*Option Dialog*/ break;
			case Keys.F1: /*Helper Dialog*/ break;
			case Keys.F2: /*Copyright Dialog*/ break;
			case Keys.F3: ResetLaunchLogger(); break;
			case Keys.F4: MessageBox.Show("You like smashing your keyboard, huh?", "Simple Flooder", MessageBoxButtons.OK, MessageBoxIcon.Information); break;
		    };
		};
	    };

	    for (int k1 = 0; k1 < Controls.Count; k1 += 1)
	    {
		AddListener(Controls[k1]);

		for (int k2 = 0; k2 < Controls[k1].Controls.Count; k2 += 1)
		{
		    AddListener(Controls[k1].Controls[k2]);

		    for (int k3 = 0; k3 < Controls[k1].Controls[k2].Controls.Count; k3 += 1)
		    {
			AddListener(Controls[k1].Controls[k2].Controls[k3]);

			for (int k4 = 0; k4 < Controls[k1].Controls[k2].Controls[k3].Controls.Count; k4 += 1)
			{
			    AddListener(Controls[k1].Controls[k2].Controls[k3].Controls[k4]);
			};
		    };
		};
	    };
	}

	private readonly PictureBox _logo = new PictureBox();
	private readonly PictureBox _bar = new PictureBox();

	private readonly Label _title = new Label();

	public SimpleFlood()
	{
	    try
	    {
		Size size = new Size(550, 265);

		MinimumSize = size;
		MaximumSize = size;
		Size = size;

		Add.MenuBar(this, _bar, Color.FromArgb(8, 8, 8), _title, $"Simple Flood 5.0", 1, 10, Color.White, _border: true, _borderColor: Color.MidnightBlue, _quit: true, _minimize: true, _draggable: true); _title.Left = 32;

		Icon = Properties.Resources.icon;
		Text = "Simple Flood 5.0";

		BackColor = Color.FromArgb(16, 16, 16);

		Add.PictureBox(_bar, _logo, Properties.Resources.logo, new Size(28, 45), new Point(2, 2), Color.Transparent);
		Add.PictureBox(this, new PictureBox(), Properties.Resources.logo, new Size(28, 45), new Point(2, 2), _bar.BackColor);

		CenterToScreen();
	    } 
	    catch (Exception e) 
	    {
		ErrorHandler(e.StackTrace);
	    };

	    InterfaceSetup();
	}
    };
};
