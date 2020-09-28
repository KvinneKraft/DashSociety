
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
	private readonly PictureBox _logo = new PictureBox();
	private readonly PictureBox _bar = new PictureBox();

	private readonly Label _title = new Label();

	public SimpleFlood()
	{
	    try
	    {
		Size size = new Size(350, 235);

		MinimumSize = size;
		MaximumSize = size;
		Size = size;

		Add.MenuBar(this, _bar, Color.FromArgb(8, 8, 8), _title, "Simple Flood", 1, 10, Color.White, _border:true, _borderColor:Color.FromArgb(8, 8, 8), _quit:true, _minimize:true, _draggable:true);

		Icon = Properties.Resources.icon;
		Text = "Simple Flood";

		BackColor = Color.FromArgb(12, 12, 12);

		_title.Left = 32;

		Add.PictureBox(_bar, _logo, Properties.Resources.logo, new Size(28, 45), new Point(2, 2), Color.Transparent);
		Add.PictureBox(this, new PictureBox(), Properties.Resources.logo, new Size(28, 45), new Point(2, 2), Color.Transparent);
	    }

	    catch (Exception e)
	    {
		// Error Handling
	    };

	    InterfaceSetup();
	}

	private void InterfaceSetup()
	{
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

	    }

	    catch
	    {
		// ERROR HANDLER
	    };
	    //</BLOCK>
	}
    }
}
