
// Author: Dashie
// Version: 5.0

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;

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
    public partial class SimpleFlood : Form
    {
	//<BLOCK id="priority-definitions">
	readonly PictureBox SPECIFICATION_AREA = new PictureBox();

	private void ResetLaunchLogger() => LAUNCH_LOGGER.Text = new string[] 
	{ 
	    "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n" + 
	    "Hey there, this tool was made for personal use and educational purposes only.\r\n\r\n" +

	    "Any in proper use of this application is considered to be your responsibility and thus makes me unable to be held responsible for such.\r\n\r\n" +
	    
	    "Remember that things are not more to us than that what we think of such. -Dashie\r\n" +
	    "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n" +
	    "[Status]: Waiting ....\r\n"
	} [0];

	private void ErrorHandler(string stack)
	{
	    MessageBox.Show($"Oh no, it seems as if my application has ran into an issue.\n\nThe error has caused the application to malfunction, this may cause lasting effects during its runtime.\n\nIf you would like to help me out with fixing this bug then please send the following to me at KvinneKraft@protonmail.com\n\n<BEGIN type=\"STACK\">{stack}<END type=\"STACK\">\n\nThank you for helping if you did.\n\nIf you would like to continue with using this application press OK else CANCEL.", "Simple Flood 5.0", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
	}

	public static Form CTR;

	readonly ABT ABOUT_FUNCTION = new ABT();
	//</BLOCK>

	//<BLOCK id="specification-controls">
	readonly PictureBox TARGET_AREA = new PictureBox();
	readonly TextBox HOST = new TextBox();
	readonly TextBox PORT = new TextBox();
	readonly Label HOST_TEXT = new Label();
	//</BLOCK>

	//<BLOCK id="tools-controls">
	readonly PictureBox TOOL_AREA = new PictureBox();
	readonly Button Options = new Button();
	readonly Button Modes = new Button();
	readonly Button About = new Button();
	//</BLOCK>

	//<BLOCK id="launch-controls">
	readonly TextBox LAUNCH_LOGGER = new TextBox();
	readonly Button LAUNCHER = new Button();
	//</BLOCK>

	//<BLOCK id="interface-section">
	private void InterfaceSetup()
	{
	    //<BLOCK id="global-area">
	    Add.PictureBox(this, SPECIFICATION_AREA, null, new Size(Width / 2 - (2 + 25), Height - 3), new Point(2, 1), _bar.BackColor); CTR = this;
	    //</BLOCK>

	    //<BLOCK id="specification-area">
	    try
	    {
		Add.PictureBox(SPECIFICATION_AREA, TARGET_AREA, null, new Size(SPECIFICATION_AREA.Width - 4, 100/*Update this*/), new Point(0, _bar.Top + _bar.Height + 20), _bar.BackColor);
		Add.Label(TARGET_AREA, HOST_TEXT, "Host", 14, 1, Size.Empty, new Point(0, 1), Color.Transparent, Color.White);

		Add.InputBox(TARGET_AREA, HOST, new Size(132, 25), new Point(HOST_TEXT.Left + HOST_TEXT.Width, HOST_TEXT.Top - 1), "255.255.255.255", 12, 1, Color.FromArgb(16, 16, 16), Color.White, _centerText:true); HOST.TextAlign = HorizontalAlignment.Center; HOST.MaxLength = "255.255.255.255".Length;
		Add.InputBox(TARGET_AREA, PORT, new Size(60, 25), new Point(HOST.Left + HOST.Width + 60, HOST.Top - 5), "25555", 12, 1, HOST.BackColor, HOST.ForeColor, _centerText:true);

		TARGET_AREA.Controls[TARGET_AREA.Controls.Count - 3].BringToFront();

		Mod.Border(TARGET_AREA.Controls[TARGET_AREA.Controls.Count - 2], 6);
		Mod.Border(TARGET_AREA.Controls[TARGET_AREA.Controls.Count - 1], 6);

		Mod.Resize(TARGET_AREA, new Size(TARGET_AREA.Width, HOST.Top + HOST.Height + 10));
	    }
	    catch (Exception e)
	    {
		ErrorHandler(e.StackTrace);
	    };
	    //</BLOCK>

	    //<BLOCK id="tools-area"
	    try
	    { 
		Size buttonSize = new Size(155, 27);

		int y = TARGET_AREA.Top + TARGET_AREA.Height + 25;
		int x = (SPECIFICATION_AREA.Width - 4 - buttonSize.Width) / 2;

		Add.PictureBox(SPECIFICATION_AREA, TOOL_AREA, null, new Size(buttonSize.Width, buttonSize.Height * 3 + 16), new Point(x, y), _bar.BackColor);

		Color buttonColor = Color.MidnightBlue;

		Add.Button(TOOL_AREA, Options, "Options", 12, 1, buttonSize, new Point(0, 0), buttonColor, Color.White);
		Add.Button(TOOL_AREA, Modes, "Modes", 12, 1, buttonSize, new Point(0, 30), buttonColor, Color.White);
		Add.Button(TOOL_AREA, About, "About", 12, 1, buttonSize, new Point(0, 60), buttonColor, Color.White);
		
		foreach (Control c in TOOL_AREA.Controls)
		{
		    if (c is Button)
		    {
			Mod.Border(c, 4);
		    };
		};

		About.Click += (s, e) =>
		{
		    if (ABOUT_FUNCTION.Visible) ABOUT_FUNCTION.Hide();
		    else ABOUT_FUNCTION.ShowDialog();
		};
	    }
	    catch (Exception e)
	    {
		ErrorHandler(e.StackTrace);
	    };
	    //</BLOCK>

	    //<BLOCK id="launch-area">
	    try
	    {
		Add.Button(SPECIFICATION_AREA, LAUNCHER, "Initiate", 11, 1, new Size(180, 26), new Point((SPECIFICATION_AREA.Width - 4 - 180) / 2, TOOL_AREA.Top + TOOL_AREA.Height + 21), Options.BackColor, Color.White);
		Mod.Border(LAUNCHER, 4);

		Add.InputBox(this, LAUNCH_LOGGER, new Size(Width - (SPECIFICATION_AREA.Width + 2), Height - (_bar.Height + 3)), new Point(SPECIFICATION_AREA.Left + SPECIFICATION_AREA.Width - 2, _bar.Height + 1), "sdogpfdjkgjkg", 10, 1, Color.MidnightBlue, Color.FromArgb(247, 255, 163), _readOnly:true, _centerText:false, _border:false, Color.Empty);

		LAUNCH_LOGGER.ScrollBars = ScrollBars.Vertical;
		LAUNCH_LOGGER.Multiline = true;

		ResetLaunchLogger();

		Add.PictureBox(SPECIFICATION_AREA, new PictureBox(), null, new Size(3, Height), new Point(LAUNCH_LOGGER.Left - 3, 0), LAUNCH_LOGGER.BackColor);
	    }
	    catch (Exception e)
	    {
		ErrorHandler(e.StackTrace);
	    };
	    //</BLOCK>

	    SPECIFICATION_AREA.Paint += (s, e) =>
	    {
		int w = 120;
		int x = (SPECIFICATION_AREA.Width - 4 - w) / 2;
		int y = TARGET_AREA.Top + TARGET_AREA.Height + 10;

		Mod.Line(e, Color.MidnightBlue, 1, new Point(x, y), new Point(x + w, y));

		y = TOOL_AREA.Top + TOOL_AREA.Height + 5;

		Mod.Line(e, Color.MidnightBlue, 1, new Point(x, y), new Point(x + w, y));
	    };
	}
	//</BLOCK>

	//<BLOCK id="application-core-section" rule="UNORDERED_COMMENT_SECTION">
	private readonly PictureBox _logo = new PictureBox();
	private readonly PictureBox _bar = new PictureBox();
	private readonly Label _title = new Label();

	public SimpleFlood()
	{
	    //<BLOCK id="application-initialization">
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
	    } 
	    catch (Exception e) 
	    {
		ErrorHandler(e.StackTrace);
	    };
	    //</BLOCK>

	    InterfaceSetup();
	}
	//</BLOCK>
    };
};
