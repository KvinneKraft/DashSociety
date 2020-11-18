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
		try//MAIN GUI SETUP:
		{
		    var GUI_COLOR = Color.FromArgb(48, 48, 48);
		    BackColor = GUI_COLOR;

		    var GUI_ICON = Resources.ICON_ICO;
		    Icon = GUI_ICON;

		    TOOL.Round(this, 6);

		    Hide();
		}

		catch
		{
		    throw new Exception("ERROR WHILE LOADING CORE!");
		};

		try//CONTROL POTATOR:
		{
		    var BAR = new MenuBar();
		    BAR.Initialize(this);

		    var URL_SECTION = new UrlSection();
		    URL_SECTION.Initialize(this);

		    var OPTION_SECTION = new OptionSection();
		    OPTION_SECTION.Initialize(this);

		    var STATUS_SECTION = new StatusSection();
		    STATUS_SECTION.Initialize(this);
		}

		catch (Exception e)
		{
		    throw new Exception($"ERROR WHILE LOADING LAYOUT FOR: {e.Message}!");
		};

		try//LAST TOUCHES:
		{
		    var TOP_CONTAINER_SIZE = new Size(384, UrlSection.ContainerHeight());
		    var TOP_CONTAINER_LOCA = new Point(10, MenuBar.MENU_OBJECTS.BAR.Height + 9 /*+1 for border*/);
		    var TOP_CONTAINER_COLA = BackColor;

		    CONTROL.Image(this, TOP_CONTAINER, TOP_CONTAINER_SIZE, TOP_CONTAINER_LOCA, null, TOP_CONTAINER_COLA);

		    PerformLayout();
		    Update();
		    Show();
		}

		catch
		{
		    throw new Exception("ERROR WHILE PERFORMING LAST TOUCHES!");
		};
	    }

	    catch (Exception e)
	    {
		var resu = MessageBox.Show($"An exception was thrown while you were making use of this application.\r\n\r\nThe error code is as follows: {e.Message}\r\n\r\nIf you want to help me fix this problem then you could send it to my email at KvinneKraft@protonmail.com.", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Error);

		if (resu == DialogResult.Yes)
		{
		    Application.Restart();
		}

		else
		{
		    Application.Exit();
		};
	    }
	}
    }
}
