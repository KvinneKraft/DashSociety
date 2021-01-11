using System;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class PortScanner2 : Form
    {
	readonly private DashControls CONTROL = new DashControls();
	readonly private DashTools TOOL = new DashTools();

	readonly PictureBox BAR = new PictureBox();
	readonly Button CLOSE = new Button();
	readonly Label TITLE = new Label();

	private void LoadMenuBar()
	{
	    var BAR_SIZE = new Size(Width, 26);
	    var BAR_LOCA = new Point(0, 0);
	    var BAR_BCOL = Color.FromArgb(8, 8, 8);

	    CONTROL.Image(this, BAR, BAR_SIZE, BAR_LOCA, null, BAR_BCOL);

	    var CLOSE_SIZE = new Size(65, BAR.Height);
	    var CLOSE_LOCA = new Point(BAR.Width - CLOSE_SIZE.Width, 0);
	    var CLOSE_BCOL = BAR.BackColor;
	    var CLOSE_FCOL = Color.White;

	    CONTROL.Button(BAR, CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 10, "X", Color.Empty);

	    CLOSE.Click += (s, e) =>
	    {
		Hide();
	    };

	    var TITLE_TEXT = "Port Scanner 2.0";
	    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 8);
	    var TITLE_LOCA = new Point(10, (BAR.Height - TITLE_SIZE.Height) / 2);
	    var TITLE_BCOL = BAR_BCOL;
	    var TITLE_FCOL = Color.White;

	    CONTROL.Label(BAR, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 8, TITLE_TEXT);

	    TOOL.Interactive(TITLE, this);
	    TOOL.Interactive(BAR, this);

	    var RECT_SIZE = new Size(Width - 1, Height - BAR.Height);
	    var RECT_LOCA = new Point(0, BAR.Height - 1);
	    var RECT_BCOL = BAR.BackColor;

	    TOOL.PaintRectangle(this, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
	}

	private void LoadGLayout()
	{
	    SuspendLayout();

	    MaximumSize = new Size(250, 192);
	    MinimumSize = new Size(250, 192);

	    StartPosition = FormStartPosition.CenterScreen;
	    FormBorderStyle = FormBorderStyle.None;

	    ShowInTaskbar = false;
	    BackColor = Color.MidnightBlue;

	    MaximizeBox = false;
	    MinimizeBox = false;

	    Name = "Port Scanner 2.0";
	    Text = "Port Scanner 2.0";

	    TOOL.Round(this, 6);
	    ResumeLayout(false);
	}

	readonly PictureBox TARGET_CONTAINER = new PictureBox();

	readonly TextBox HOST_T = new TextBox() { TextAlign = HorizontalAlignment.Center, Text = "https://www.google.co.uk" };
	readonly TextBox PORT_T = new TextBox() { TextAlign = HorizontalAlignment.Center, Text = "43,80,443,23,22,21,8080,2222" };

	readonly Label HOST_L = new Label();
	readonly Label PORT_L = new Label();

	readonly PictureBox INNER_METHOD_CONTAINER = new PictureBox();
	readonly PictureBox METHOD_CONTAINER = new PictureBox();

	readonly PictureBox UDP_S = new PictureBox();
	readonly PictureBox TCP_S = new PictureBox();
	readonly PictureBox RAW_S = new PictureBox();

	readonly Label UDP_L = new Label();
	readonly Label TCP_L = new Label();
	readonly Label RAW_L = new Label();

	readonly Color DISABLED = Color.FromArgb(14, 14, 14);
	readonly Color ENABLED = Color.DarkGreen;

	readonly PictureBox INNER_SELECT_CONTAINER = new PictureBox();
	readonly PictureBox SELECT_CONTAINER = new PictureBox();

	readonly Button OPT_1 = new Button();
	readonly Button OPT_2 = new Button();
	readonly Button OPT_3 = new Button();

	private void LoadScanner()
	{
	    void ThrowError(Exception e)
	    {
		MessageBox.Show($"Something happened while trying to load up Port Scanner 2.0.\r\n\r\nPlease contact me at KvinneKraft@protonmail.com with the following:\r\n\r\n[{e.Message}]\r\n{e.StackTrace}\r\n\r\nThank you very much!", "(Error Occurrence)");
		Environment.Exit(-1);
	    };

	    try
	    {
		try
		{
		    var TCON_SIZE = new Size(Width - 24, 55);
		    var TCON_LOCA = new Point(12, BAR.Height + 12);
		    var TCON_BCOL = Color.MidnightBlue;

		    CONTROL.Image(this, TARGET_CONTAINER, TCON_SIZE, TCON_LOCA, null, TCON_BCOL);

		    try
		    {
			var LBOX_TEXT = "Host(s):";
			var LBOX_SIZE = TOOL.GetFontSize(LBOX_TEXT, 10);
			var LBOX_LOCA = new Point(5, 6);
			var LBOX_BCOL = BackColor;
			var LBOX_FCOL = Color.White;

			CONTROL.Label(TARGET_CONTAINER, HOST_L, LBOX_SIZE, LBOX_LOCA, LBOX_BCOL, LBOX_FCOL, 1, 10, LBOX_TEXT);

			LBOX_SIZE = TOOL.GetFontSize(LBOX_TEXT, 10);
			LBOX_LOCA.Y += 8 + LBOX_SIZE.Height;
			LBOX_TEXT = "Port(s):";

			CONTROL.Label(TARGET_CONTAINER, PORT_L, LBOX_SIZE, LBOX_LOCA, LBOX_BCOL, LBOX_FCOL, 1, 10, LBOX_TEXT);

			var TBOX_SIZE = new Size(TARGET_CONTAINER.Width - LBOX_SIZE.Width - 10 - LBOX_LOCA.X, 20);
			var TBOX_LOCA = new Point(LBOX_LOCA.X + LBOX_SIZE.Width + 4, 5);
			var TBOX_BCOL = Color.FromArgb(14, 14, 14);
			var TBOX_FCOL = Color.White;

			CONTROL.TextBox(TARGET_CONTAINER, HOST_T, TBOX_SIZE, TBOX_LOCA, TBOX_BCOL, TBOX_FCOL, 1, 9, Color.Empty);

			TBOX_LOCA.X = LBOX_LOCA.X + LBOX_SIZE.Width + 4;
			TBOX_LOCA.Y += 5 + TBOX_SIZE.Height;

			CONTROL.TextBox(TARGET_CONTAINER, PORT_T, TBOX_SIZE, TBOX_LOCA, TBOX_BCOL, TBOX_FCOL, 1, 10, Color.Empty);
		    }

		    catch (Exception e)
		    {
			ThrowError(e);
		    }

		    var RECT_SIZE = new Size(TCON_SIZE.Width + 2, TCON_SIZE.Height + 2);
		    var RECT_LOCA = new Point(TCON_LOCA.X - 1, TCON_LOCA.Y - 1);
		    var RECT_BCOL = BAR.BackColor;

		    TOOL.PaintRectangle(this, 1, RECT_SIZE, RECT_LOCA, RECT_BCOL);
		}

		catch (Exception e)
		{
		    ThrowError(e);
		}

		try
		{
		    var MCON_SIZE = new Size(Width - 26, 27);//17 + 10
		    var MCON_LOCA = new Point(-1, TARGET_CONTAINER.Top + TARGET_CONTAINER.Height + 12);
		    var MCON_BCOL = Color.MidnightBlue;

		    CONTROL.Image(this, METHOD_CONTAINER, MCON_SIZE, MCON_LOCA, null, MCON_BCOL);

		    try
		    {
			var IMC_SIZE = new Size(177, 17);
			var IMC_LOCA = new Point(-1, -1);
			var IMC_BCOL = MCON_BCOL;

			CONTROL.Image(METHOD_CONTAINER, INNER_METHOD_CONTAINER, IMC_SIZE, IMC_LOCA, null, IMC_BCOL);

			INNER_METHOD_CONTAINER.Left -= 2;

			var METL_TEXT = "UDP";
			var METL_SIZE = TOOL.GetFontSize(METL_TEXT, 12);
			var METL_LOCA = new Point(0, 0);
			var METL_BCOL = Color.MidnightBlue;
			var METL_FCOL = Color.White;

			CONTROL.Label(INNER_METHOD_CONTAINER, UDP_L, METL_SIZE, METL_LOCA, METL_BCOL, METL_FCOL, 1, 12, METL_TEXT);

			var METI_SIZE = new Size(17, 17);
			var METI_LOCA = new Point(METL_SIZE.Width + METL_LOCA.X, 0);
			var METI_BCOL = Color.FromArgb(14, 14, 14);
			
			CONTROL.Image(INNER_METHOD_CONTAINER, UDP_S, METI_SIZE, METI_LOCA, null, METI_BCOL);

			void UpdateLX() => METL_LOCA.X += UDP_S.Left + UDP_S.Width + 5;
			void UpdatePX() => METI_LOCA.X += METL_SIZE.Width + 22;

			METL_TEXT = "TCP"; UpdateLX();

			CONTROL.Label(INNER_METHOD_CONTAINER, TCP_L, METL_SIZE, METL_LOCA, METL_BCOL, METL_FCOL, 1, 12, "TCP"); UpdatePX();
			CONTROL.Image(INNER_METHOD_CONTAINER, TCP_S, METI_SIZE, METI_LOCA, null, ENABLED); METL_TEXT = "RAW"; UpdateLX();
			CONTROL.Label(INNER_METHOD_CONTAINER, RAW_L, TOOL.GetFontSize(METL_TEXT, 12), METL_LOCA, METL_BCOL, METL_FCOL, 1, 12, "RAW"); UpdatePX();
			CONTROL.Image(INNER_METHOD_CONTAINER, RAW_S, METI_SIZE, METI_LOCA, null, METI_BCOL); RAW_S.Left += 2;

			foreach (Control c1 in INNER_METHOD_CONTAINER.Controls)
			{
			    if (c1 is PictureBox)
			    {
				c1.Click += (s, e) =>
				{
				    foreach (Control c2 in INNER_METHOD_CONTAINER.Controls)
				    {
					if (c2 is PictureBox)
					{
					    c2.BackColor = DISABLED;
					}
				    }

				    c1.BackColor = ENABLED;
				};
			    }
			}
		    }

		    catch (Exception e)
		    {
			ThrowError(e);
		    }

		    var RECT_SIZE = new Size(MCON_SIZE.Width + 2, MCON_SIZE.Height + 2);
		    var RECT_LOCA = new Point(MCON_LOCA.X - 1, MCON_LOCA.Y - 1);
		    var RECT_BCOL = BAR.BackColor;

		    TOOL.PaintRectangle(this, 1, RECT_SIZE, RECT_LOCA, RECT_BCOL);
		}

		catch (Exception e)
		{
		    ThrowError(e);
		}

		try
		{
		    var MCON_SIZE = new Size(Width - 24, 34);//24
		    var MCON_LOCA = new Point(12, METHOD_CONTAINER.Top + METHOD_CONTAINER.Height + 12);
		    var MCON_BCOL = Color.MidnightBlue;

		    CONTROL.Image(this, SELECT_CONTAINER, MCON_SIZE, MCON_LOCA, null, MCON_BCOL);

		    try
		    {
			var ISC_SIZE = new Size(205, 24);
			var ISC_LOCA = new Point(-1, -1);
			var ISC_BCOL = MCON_BCOL;

			CONTROL.Image(SELECT_CONTAINER, INNER_SELECT_CONTAINER, ISC_SIZE, ISC_LOCA, null, ISC_BCOL);

			var PRESS_SIZE = new Size(65, 24);
			var PRESS_LOCA = new Point(0, 0);
			var PRESS_BCOL = Color.FromArgb(10, 10, 10);
			var PRESS_FCOL = Color.White;

			CONTROL.Button(INNER_SELECT_CONTAINER, OPT_1, PRESS_SIZE, PRESS_LOCA, PRESS_BCOL, PRESS_FCOL, 1, 9, "Online", Color.Empty);

			OPT_1.Click += (s, e) =>
			{
			    if (IsOnline())
			    {
				MessageBox.Show("", "");
			    }

			    else
			    {
				MessageBox.Show("", "");
			    }
			};

			PRESS_LOCA.X += PRESS_SIZE.Width + 5;

			CONTROL.Button(INNER_SELECT_CONTAINER, OPT_2, PRESS_SIZE, PRESS_LOCA, PRESS_BCOL, PRESS_FCOL, 1, 9, "Scan", Color.Empty);

			OPT_2.Click += (s, e) =>
			{
			    // Separate Dialog with Log Progress
			};

			PRESS_LOCA.X += PRESS_SIZE.Width + 5;

			CONTROL.Button(INNER_SELECT_CONTAINER, OPT_3, PRESS_SIZE, PRESS_LOCA, PRESS_BCOL, PRESS_FCOL, 1, 9, "Help", Color.Empty);

			OPT_3.Click += (s, e) =>
			{

			};
		    }

		    catch (Exception e)
		    {
			ThrowError(e);
		    }

		    var RECT_SIZE = new Size(MCON_SIZE.Width + 2, MCON_SIZE.Height + 2);
		    var RECT_LOCA = new Point(MCON_LOCA.X - 1, MCON_LOCA.Y - 1);
		    var RECT_BCOL = BAR.BackColor;

		    TOOL.PaintRectangle(this, 1, RECT_SIZE, RECT_LOCA, RECT_BCOL);
		}

		catch (Exception e)
		{
		    ThrowError(e);
		}
	    }

	    catch (Exception e)
	    {
		ThrowError(e);
	    }
	}

	public PortScanner2()
	{
	    try
	    {
		LoadGLayout();
		LoadMenuBar();
		LoadScanner();
	    }

	    catch
	    {
		Environment.Exit(-1);
	    }
	}
    }
}