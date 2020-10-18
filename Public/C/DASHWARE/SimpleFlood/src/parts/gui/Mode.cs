
// Author: Dashie
// Version: 5.0

using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleFlood
{
    public partial class MODE : Form
    {
	readonly Color DESELECTED_COLOR = Color.FromArgb(24, 24, 24);
	readonly Color SELECTED_COLOR = Color.CornflowerBlue;

	readonly Label HTTP_MODE = new Label(), HTTP_TEXT = new Label(); 
	readonly Label TCP_MODE = new Label(), TCP_TEXT = new Label();
	readonly Label UDP_MODE = new Label(), UDP_TEXT = new Label();

	readonly Button DONE = new Button();

	private Point GetLoc(Label TULIP)
	{
	    return new Point(TULIP.Left + TULIP.Width + 5, TULIP.Top);
	}

	public static int mode = 0;

	public MODE()
	{
	    InitializeComponent();

	    try
	    {
		Shown += (s, e) =>
		{
		    CenterToParent();
		};

		BackColor = Color.FromArgb(8, 8, 8);

		Paint += (s, e) =>
		{
		    Mod.Rectangle(e, Color.MidnightBlue, 2, new Size(Width - 1, Height - 1), new Point(0, 0));
		};

		Mod.Moveable(this, this);
	    }

	    catch (Exception e)
	    {
		SimpleFlood.ErrorHandler(e.StackTrace);
	    };

	    try
	    {
		void CheckBox(Label HOLDER, Point LOCATION)
		{
		    Add.Label(this, HOLDER, "", 1, 1, new Size(16, 16), LOCATION, DESELECTED_COLOR, Color.White);

		    HOLDER.Click += (_, e) =>
		    {
			foreach (Control c in Controls)
			{
			    if (c is Label && ((Label)c).BackColor == SELECTED_COLOR)
			    {
				c.BackColor = DESELECTED_COLOR;
			    };
			};

			if (HOLDER.BackColor == SELECTED_COLOR)
			{
			    HOLDER.BackColor = DESELECTED_COLOR;
			}

			else
			{
			    HOLDER.BackColor = SELECTED_COLOR;
			};
		    };

		    HOLDER.Paint += (_, e) =>
		    {
			Mod.Rectangle(e, Color.MidnightBlue, 1, new Size(HOLDER.Width - 1, HOLDER.Height - 1), new Point(0, 0));
		    };
		};

		void Text(Label HOLDER, Point LOCATION, string TEXT)
		{
		    Add.Label(this, HOLDER, $"{TEXT}", 12, 1, Size.Empty, LOCATION, Color.Transparent, Color.White);
		};

		int x = 5, y = 5, s = 8;

		Text(HTTP_TEXT, new Point(x, y), "HTTP Mode");
		CheckBox(HTTP_MODE, GetLoc(HTTP_TEXT));

		HTTP_MODE.Click += (_, e) =>
		{
		    mode = 0;
		};

		HTTP_MODE.BackColor = SELECTED_COLOR;

		Text(TCP_TEXT, new Point(x, HTTP_TEXT.Top + HTTP_TEXT.Height + s), "TCP Mode");
		CheckBox(TCP_MODE, GetLoc(TCP_TEXT));

		TCP_MODE.Click += (_, e) =>
		{
		    mode = 1;
		};

		Text(UDP_TEXT, new Point(x, TCP_TEXT.Top + TCP_TEXT.Height + s), "UDP Mode");
		CheckBox(UDP_MODE, GetLoc(UDP_TEXT));

		UDP_TEXT.Click += (_, e) =>
		{
		    mode = 2;
		};
	    }

	    catch (Exception e)
	    {
		SimpleFlood.ErrorHandler(e.StackTrace);
	    };

	    Mod.Resize(this, new Size(HTTP_MODE.Left + HTTP_MODE.Width + 8, 130));

	    Paint += (a, e) =>
	    {
		int w = 65;
		int x = (Width - 2 - w) / 2;
		int y = UDP_MODE.Top + UDP_MODE.Height + 10;

		Mod.Line(e, Color.MidnightBlue, 1, new Point(x, y), new Point(x + w, y));
	    };

	    Add.Button(this, DONE, "Done", 11, 1, new Size(95, 24), new Point(-1, UDP_MODE.Top + UDP_MODE.Height + 21), Color.MidnightBlue, Color.White);
	    Mod.Border(DONE, 6);

	    DONE.Click += (s, e) =>
	    {
		Hide();
	    };
	}
    }
}
