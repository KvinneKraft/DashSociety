using System;
using System.IO;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

using DashDownloader.Properties;

namespace DashDownloader
{
    public partial class DashDownloader : Form
    {
	public DashDownloader()
	{
	    InitializeComponent();

	    try
	    {
		void SetupGUI()
		{
		    var GUI_COLOR = Color.FromArgb(32, 32, 32);
		    BackColor = GUI_COLOR;

		    var GUI_ICON = Resources.ICON_ICO;
		    Icon = GUI_ICON;
		};

		SetupGUI();

		var MENU_BAR = new MenuBar();
		MENU_BAR.Initialize(this);


	    }

	    catch (Exception e)
	    {
		// Handle Errors Here
	    }
	}
    }
}
