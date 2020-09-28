
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
		Size size = new Size(500, 400);

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
	readonly PictureBox EXECUTION_AREA = new PictureBox();
	//</BLOCK>
	//<BLOCK id="specification-controls">

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
	    Add.PictureBox(this, EXECUTION_AREA, null, new Size(Width - (SPECIFICATION_AREA.Width + 2), Height - 3), new Point(SPECIFICATION_AREA.Left + SPECIFICATION_AREA.Width - 2, 1), Color.FromArgb(32, 32, 32));
	    //</BLOCK>
	    //<BLOCK id="specification-area">
	    try
	    {

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
		Add.InputBox(EXECUTION_AREA, LAUNCH_LOGGER, EXECUTION_AREA.Size, new Point(0, 0), "sdogpfdjkgjkg", 10, 1, Color.FromArgb(32, 32, 32), Color.White, _readOnly:true);

		LAUNCH_LOGGER.ScrollBars = ScrollBars.Vertical;
		LAUNCH_LOGGER.Multiline = true;
	    }

	    catch
	    {
		// ERROR HANDLER
	    };
	    //</BLOCK>
	}
    }
}
