
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class AboutDialog : Form
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();


	private void LoadGLayout()
	{
	    Text = "Information";

	    FormBorderStyle = FormBorderStyle.None;
	    StartPosition = FormStartPosition.CenterParent;

	    ShowInTaskbar = false;

	    MinimizeBox = false;
	    MaximizeBox = false;

	    TOOL.Resize(this, new Size(350, 350));
	    TOOL.Round(this, 6);

	    var RECT_SIZE = new Size(Width - 1, Height - B.Height - 1);
	    var RECT_LOCA = new Point(1, B.Height);
	    var RECT_BCOL = B.BackColor;

	    TOOL.PaintRectangle(this, 3, RECT_SIZE, RECT_LOCA, RECT_BCOL);
	}

	readonly private PictureBox B = new PictureBox();
	readonly private Button X = new Button();
	readonly private Label T = new Label();

	private void LoadMenuBar()
	{
	    var BAR_SIZE = new Size(Width - 1, 26);
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
		Hide();
	    };

	    var TITLE_TEXT = "DASH Information";
	    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 8);
	    var TITLE_LOCA = new Point(10, (B.Height - TITLE_SIZE.Height) / 2);
	    var TITLE_BCOL = BAR_BCOL;
	    var TITLE_FCOL = Color.White;

	    CONTROL.Label(B, T, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 8, TITLE_TEXT);

	    TOOL.Interactive(B, this);
	}

	private void LoadInforma()
	{

	}

	public AboutDialog()
	{
	    try
	    {
		LoadGLayout();
		LoadMenuBar();
		LoadInforma();
	    }

	    catch
	    {
		Application.Exit();
	    }
	}
    }
}
