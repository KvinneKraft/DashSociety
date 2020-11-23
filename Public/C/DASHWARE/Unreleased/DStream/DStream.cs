
// Author: Dashie
// Version: 1.0

using System;
using System.Drawing;
using System.Windows.Forms;

namespace DStream
{
    public partial class DStream : Form
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();

	readonly public static string ERROR_FORMAT = "Hey there, I am unfortunate to say but this application has ran into a problem.\r\n\r\nYou can either try restarting this application to see if that solves the problem if not though you could perhaps consider reaching out to me at KvinneKraft@protonmail.com.\r\n\r\nIf you do though, please put the following in your message:\r\n%m%\r\n\r\nIf you want to restart this application right now you can click OK if not you can click CANCEL.";

	public static DStream APP;

	private void InitializeMainGUI()
	{
	    try
	    {
		BackColor = Color.FromArgb(12, 12, 12);
	    }

	    catch
	    {
		throw new Exception("--InitializeMainGUI() in DStream.cs <----");
	    };
	}

	private class MENUBAR
	{
	    readonly public static PictureBox CONTAINER = new PictureBox();
	    readonly public static PictureBox ICON = new PictureBox();

	    readonly public static Button MINIMIZE = new Button();
	    readonly public static Button MAXIMIZE = new Button();

	    readonly public static Label TITLE = new Label();
	}

	private void InitializeMenuBar()
	{
	    try
	    {
		var CONTAINER_SIZE = new Size(Width, 28);
		var CONTAINER_LOCA = new Point(0, 0);
		var CONTAINER_BCOL = Color.FromArgb(8, 8, 8);

		CONTROL.Image(this, MENUBAR.CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_BCOL);
	    }

	    catch
	    {
		throw new Exception("--InitializeMenuBar() in DStream.cs <----");
	    };
	}

	public DStream()
	{
	    InitializeComponent();

	    try
	    {
		APP = this;

		InitializeMainGUI();
		InitializeMenuBar();
	    }

	    catch (Exception e)
	    {
		var m = MessageBox.Show(ERROR_FORMAT.Replace("%m%", e.Message), "Oh No!", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

		switch (m)
		{
		    case DialogResult.OK:
		    {
			Application.Restart();
			break;
		    };
		};

		Application.Exit();
	    };
	}
    }
}
