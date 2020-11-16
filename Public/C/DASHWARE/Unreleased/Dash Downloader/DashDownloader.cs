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
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();

	readonly public static PictureBox TOP_CONTAINER = new PictureBox();

	public DashDownloader()
	{
	    InitializeComponent();

	    try
	    {
		void SetupGUI()
		{
		    var GUI_COLOR = Color.FromArgb(26, 26, 26);
		    BackColor = GUI_COLOR;

		    var GUI_ICON = Resources.ICON_ICO;
		    Icon = GUI_ICON;
		};

		SetupGUI();

		var MENU_BAR = new MenuBar();
		MENU_BAR.Initialize(this);

		var URL_SECTION = new UrlSection();
		URL_SECTION.Initialize(this);

		var OPTION_SECTION = new OptionSection();
		OPTION_SECTION.Initialize(this);

		var TOP_CONTAINER_SIZE = new Size(380, UrlSection.URL_OBJECTS.URL_BOX.Height);
		var TOP_CONTAINER_LOCA = new Point(13, MenuBar.MENU_OBJECTS.MENU_BAR.Height + 16 /*+1 for border*/);
		var TOP_CONTAINER_COLA = BackColor;

		CONTROL.Image(this, TOP_CONTAINER, TOP_CONTAINER_SIZE, TOP_CONTAINER_LOCA, null, TOP_CONTAINER_COLA);

		var BORDER_SIZE = new Size(UrlSection.URL_OBJECTS.URL_BOX.Width + 4, UrlSection.URL_OBJECTS.URL_BOX.Height + 3);
		var BORDER_LOCA = new Point(TOP_CONTAINER_LOCA.X - 2, TOP_CONTAINER_LOCA.Y - 2);
		var BORDER_COLA = UrlSection.URL_OBJECTS.URL_BOX.BackColor;

		TOOL.PaintRectangle(this, 6, BORDER_SIZE, BORDER_LOCA, BORDER_COLA);

		var LINE_LOC1 = new Point(UrlSection.URL_OBJECTS.URL_BOX.Width + 3, 0);
		var LINE_LOC2 = new Point(LINE_LOC1.X, UrlSection.URL_OBJECTS.URL_BOX.Height);

		TOOL.PaintLine(TOP_CONTAINER, BORDER_COLA, 4, LINE_LOC1, LINE_LOC2);

		/*
		 Priorities:
		 - MENU BAR
		 - URL Section
		 - Option Section
		 - Status Section
		 */
	    }

	    catch (Exception e)
	    {
		// Handle Errors Here
	    }
	}
    }
}
