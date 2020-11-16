using System;
using System.Drawing;
using System.Windows.Forms;

namespace DashDownloader
{
    public class MenuBar
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();

	public static class MENU_OBJECTS
	{
	    readonly public static PictureBox MENU_ICON = new PictureBox();
	    readonly public static PictureBox MENU_BAR = new PictureBox();

	    readonly public static Button MENU_CLOSE = new Button();
	    readonly public static Button MENU_MINIM = new Button();

	    readonly public static Label MENU_TITLE = new Label();

	    public static void SETUP_EVENTS(Control MASTER)
	    {
		MENU_CLOSE.Click += (s, e) =>
		{
		    if (MASTER is Form)
			((Form)MASTER).Close();
		    else
			MASTER.Hide();
		};

		MENU_MINIM.Click += (s, e) =>
		{
		    if (MASTER is Form)
			((Form)MASTER).WindowState = FormWindowState.Minimized;

		    else
			MASTER.SendToBack();
		};
	    }
	}
	// Requires some tweaking....
	public void Initialize(Form CON)
	{
	    var MENUBAR_SIZE = new Size(CON.Width - 2, 26);
	    var MENUBAR_LOCA = new Point(1, 1);
	    var MENUBAR_COLA = Color.FromArgb(8, 8, 8);

	    CONTROL.Image(CON, MENU_OBJECTS.MENU_BAR, MENUBAR_SIZE, MENUBAR_LOCA, null, MENUBAR_COLA);

	    var MENUICON = (Image)Properties.Resources.ICON_PNG;
	    var MENUICON_SIZE = new Size(MENUICON.Width, MENUBAR_SIZE.Height);
	    var MENUICON_LOCA = new Point(0, -1);

	    CONTROL.Image(MENU_OBJECTS.MENU_BAR, MENU_OBJECTS.MENU_ICON, MENUICON_SIZE, MENUICON_LOCA, MENUICON, MENUBAR_COLA);

	    TOOL.Interactive(MENU_OBJECTS.MENU_ICON, CON);

	    var MENUTITLE_LOCA = TOOL.GetCenter(MENU_OBJECTS.MENU_BAR, MENU_OBJECTS.MENU_TITLE, new Point(MENUICON_LOCA.X + 35, -1));
	    var MENUTITLE_FCOL = Color.White;

	    CONTROL.Label(MENU_OBJECTS.MENU_BAR, MENU_OBJECTS.MENU_TITLE, Size.Empty, MENUTITLE_LOCA, MENUTITLE_FCOL, MENUBAR_COLA, 1, 9, $"Dash Downloader");

	    TOOL.Interactive(MENU_OBJECTS.MENU_TITLE, CON);

	    var BORDER_SIZE = new Size(CON.Width - 1, CON.Height - 1);
	    var BORDER_LOCA = new Point(0, 0);
	    var BORDER_COLA = Color.FromArgb(8, 8, 8);

	    TOOL.PaintRectangle(CON, 2, BORDER_SIZE, BORDER_LOCA, BORDER_COLA);

	    var CLOSE_SIZE = new Size(65, MENUBAR_SIZE.Height);
	    var CLOSE_LOCA = new Point(MENUBAR_SIZE.Width - CLOSE_SIZE.Width, 0);
	    var CLOSE_FCOL = Color.White;
	    var CLOSE_BCOL = MENUBAR_COLA;

	    CONTROL.Button(MENU_OBJECTS.MENU_BAR, MENU_OBJECTS.MENU_CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 12, "X", Color.Empty);

	    var MINIM_LOCA = new Point(CLOSE_LOCA.X - CLOSE_SIZE.Width, 0);

	    CONTROL.Button(MENU_OBJECTS.MENU_BAR, MENU_OBJECTS.MENU_MINIM, CLOSE_SIZE, MINIM_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 12, "-", Color.Empty);

	    MENU_OBJECTS.SETUP_EVENTS(CON);

	    TOOL.Interactive(MENU_OBJECTS.MENU_BAR, CON);
	}
    }
}
