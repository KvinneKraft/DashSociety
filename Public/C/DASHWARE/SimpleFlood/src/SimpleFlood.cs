
// Author: Dashie
// Version: 4.0

using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("SimpleFlood")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("SimpleFlood")]
[assembly: AssemblyCopyright("Copyright ©  2020")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]
[assembly: Guid("8da9673b-2139-4248-94f1-fecabe807ee8")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

namespace SimpleFlood
{
    public partial class SimpleFlood : Form
    {
	//<BLOCK id="application-core-section" rule="UNORDERED_COMMENT_SECTION">
	private readonly PictureBox _logo = new PictureBox();
	private readonly PictureBox _bar = new PictureBox();

	private readonly Label _title = new Label();
	public SimpleFlood()
	{
	    //<BLOCK id="application-initialization">
	    try
	    {
		Size size = new Size(550, 400);

		MinimumSize = size;
		MaximumSize = size;
		Size = size;

		Add.MenuBar(this, _bar, Color.FromArgb(8, 8, 8), _title, "Simple Flood", 1, 10, Color.White, _border:true, _borderColor:Color.FromArgb(8, 8, 8), _quit:true, _minimize:true, _draggable:true);

		Icon = Properties.Resources.icon;
		Text = "Simple Flood";

		BackColor = Color.FromArgb(16, 16, 16);

		_title.Left = 32;

		Add.PictureBox(_bar, _logo, Properties.Resources.logo, new Size(28, 45), new Point(2, 2), Color.Transparent);
		Add.PictureBox(this, new PictureBox(), Properties.Resources.logo, new Size(28, 45), new Point(2, 2), _bar.BackColor);
	    }

	    catch 
	    {
		// ERROR HANDLER
	    };
	    //</BLOCK>

	    InterfaceSetup();
	}
	//</BLOCK>
	//<BLOCK id="interface-creation-section">
	//<BLOCK id="global-controls">
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
	//</BLOCK>
	//<BLOCK id="specification-controls">
	readonly PictureBox TARGET_AREA = new PictureBox();
	readonly TextBox HOST = new TextBox();
	readonly TextBox PORT = new TextBox();
	readonly Label HOST_TEXT = new Label();
	readonly Label PORT_TEXT = new Label();
	//</BLOCK>
	//<BLOCK id="tools-controls">

	//</BLOCK>
	//<BLOCK id="launch-controls">
	readonly TextBox LAUNCH_LOGGER = new TextBox();
	//</BLOCK>
	private void InterfaceSetup()
	{
	    //<BLOCK id="global-area">
	    Add.PictureBox(this, SPECIFICATION_AREA, null, new Size(Width / 2 - (2 + 25), Height - 3), new Point(2, 1), _bar.BackColor);
	    //</BLOCK>
	    //<BLOCK id="specification-area">
	    try
	    {
		Add.PictureBox(SPECIFICATION_AREA, TARGET_AREA, null, new Size(SPECIFICATION_AREA.Width - 4, 100/*Update this*/), new Point(0, _bar.Top + _bar.Height + 20), _bar.BackColor);

		Add.Label(TARGET_AREA, HOST_TEXT, "Host", 14, 1, Size.Empty, new Point(0, 0), Color.Transparent, Color.White);

		Add.InputBox(TARGET_AREA, HOST, new Size(132, 25), new Point(HOST_TEXT.Left + HOST_TEXT.Width, HOST_TEXT.Top), "255.255.255.255", 12, 1, Color.FromArgb(56, 44, 69), Color.White, _centerText:true); HOST.TextAlign = HorizontalAlignment.Center; HOST.MaxLength = "255.255.255.255".Length;
		Add.InputBox(TARGET_AREA, PORT, new Size(60, 25), new Point(HOST.Left + HOST.Width + 60, HOST.Top - 5), "25555", 12, 1, HOST.BackColor, HOST.ForeColor, _centerText:true);

		Mod.Border(TARGET_AREA.Controls[TARGET_AREA.Controls.Count - 2], 6);
		Mod.Border(TARGET_AREA.Controls[TARGET_AREA.Controls.Count - 1], 6);

		SPECIFICATION_AREA.Paint += (s, e) =>
		{
		    Mod.Rectangle(e, Color.FromArgb(32, 32, 32), 1, new Size(165, 1), new Point((TARGET_AREA.Width - 165) / 2, TARGET_AREA.Top + TARGET_AREA.Height + 10));
		};

		Mod.Resize(TARGET_AREA, new Size(TARGET_AREA.Width, HOST.Top + HOST.Height + 10));
	    }

	    catch
	    {
		// ERROR HANDLER
	    };
	    //</BLOCK>
	    //<BLOCK id="tools-area"
	    try
	    {

	    }

	    catch
	    {
		// ERROR HANDLER
	    };
	    //<BLOCK id="launch-area">
	    try
	    {
		Add.InputBox(this, LAUNCH_LOGGER, new Size(Width - (SPECIFICATION_AREA.Width + 2), Height - (_bar.Height + 3)), new Point(SPECIFICATION_AREA.Left + SPECIFICATION_AREA.Width - 2, _bar.Height + 1), "sdogpfdjkgjkg", 10, 1, Color.FromArgb(32, 32, 32), Color.White, _readOnly:true, _centerText:false, _border:false, Color.Empty);

		LAUNCH_LOGGER.ScrollBars = ScrollBars.Vertical;
		LAUNCH_LOGGER.Multiline = true;

		ResetLaunchLogger();

		Add.PictureBox(SPECIFICATION_AREA, new PictureBox(), null, new Size(3, Height), new Point(LAUNCH_LOGGER.Left - 3, 0), LAUNCH_LOGGER.BackColor);
	    }

	    catch
	    {
		// ERROR HANDLER
	    };
	    //</BLOCK>
	}
    }
}
