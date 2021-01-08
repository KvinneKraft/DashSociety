
// Author: Dashie
// Version: 1.0

using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public partial class ToS : Form
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();

	private void LoadGUILayout()
	{
	    Text = "Terms of Service Wall";

	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterScreen;

	    ShowInTaskbar = false;

	    MinimizeBox = false;
	    MaximizeBox = false;
	}

	readonly private PictureBox B = new PictureBox();
	readonly private Button X = new Button();
	readonly private Label T = new Label();

	private void LoadMenuBar()
	{
	    var BAR_SIZE = new Size(Width - 2, 26);
	    var BAR_LOCA = new Point(1, 0);
	    var BAR_BCOL = Color.FromArgb(8, 8, 8);

	    CONTROL.Image(this, B, BAR_SIZE, BAR_LOCA, null, BAR_BCOL);

	    var CLOSE_SIZE = new Size(55, BAR_SIZE.Height);
	    var CLOSE_LOCA = new Point(BAR_SIZE.Width - CLOSE_SIZE.Width, 0);
	    var CLOSE_BCOL = BAR_BCOL;
	    var CLOSE_FCOL = Color.White;

	    CONTROL.Button(B, X, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 10, "X", Color.Empty);

	    X.Click += (s, e) =>
	    {
		MessageBox.Show("You must accept the terms of service in order to use my software.\r\n\r\nClick OK to exit the application.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		Application.Exit();
	    };

	    var TITLE_TEXT = "DASH ToS";
	    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 8);
	    var TITLE_LOCA = new Point(10, (B.Height - TITLE_SIZE.Height) / 2);
	    var TITLE_BCOL = BAR_BCOL;
	    var TITLE_FCOL = Color.White;

	    CONTROL.Label(B, T, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 8, TITLE_TEXT);
	}

	public ToS()
	{
	    try
	    {
		LoadGUILayout();
		LoadMenuBar();
	    }

	    catch
	    {
		Environment.Exit(-1);
	    }
	}
    }
}
