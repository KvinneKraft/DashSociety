using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public partial class Interface : Form
    {
	readonly static string FORMAT = "Hey there, I am unfortunate to tell you but an error has occurred which has caused this application to be useless right now.\r\n\r\nYou can help me prevent this from happening in the future by sending me the following error code at KvinneKraft@protonmail.com:\r\n{\r\n%m%\r\n}\r\n\r\nRegardless, Thank you.\r\n\r\nClick OK to close this application or click cancel to restart this application.";

	readonly static DashControls CONTROL = new DashControls();
	readonly static DashTools TOOL = new DashTools();

	private void InitializeMBar()
	{
	    try
	    {
		CONTROL.MenuBar(this, (int) MENUBAR.STYLE.THIC, true, Color.FromArgb(12, 12, 12), Color.FromArgb(12, 12, 12));
	    }

	    catch
	    {
		throw new Exception("InitializeMBar()");
	    }
	}
	
	private void InitializeBody()
	{
	    try
	    {
		var GUI_COLOR = Color.MidnightBlue;

		BackColor = GUI_COLOR;
		
		var RECTANGLE_SIZE = new Size(220, 121);
		var RECTANGLE_LOCA = new Point(4, 0);
		var RECTANGLE_COLA = BackColor;

		TOOL.PaintRectangle(this, 4, RECTANGLE_SIZE, RECTANGLE_LOCA, RECTANGLE_COLA);
		TOOL.Round(this, 6);
	    }

	    catch
	    {
		throw new Exception("InitializeBody()");
	    }
	}

	private class TARGETCONTAINER
	{
	    readonly public static PictureBox CONTAINER = new PictureBox();
	    
	    readonly public static TextBox IP_BOX = new TextBox() { Text = "https://pugpawz.com/", TextAlign = HorizontalAlignment.Center };
	    readonly public static TextBox PO_BOX = new TextBox() { Text = "8080", TextAlign = HorizontalAlignment.Center };
	    readonly public static TextBox DR_BOX = new TextBox() { Text = "360", TextAlign = HorizontalAlignment.Center };

	    readonly static Label IP_LAB = new Label();
	    readonly static Label PO_LAB = new Label();
	    readonly static Label DR_LAB = new Label();

	    public static void InitializeHCon(Form TOP)
	    {
		try
		{
		    var CONTAINER_SIZE = new Size(200, /*58*/83);
		    var CONTAINER_LOCA = new Point(8, MENUBAR.BAR.Height + 7);
		    var CONTAINER_COLA = Color.FromArgb(28, 28, 28);

		    CONTROL.Image(TOP, CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);
		    TOOL.Round(CONTAINER, 6);

		    void RoundTextBox() =>
			TOOL.Round(CONTAINER.Controls[CONTAINER.Controls.Count - 1], 6);

		    Size GetFontSize(string TEXT) =>
			TextRenderer.MeasureText(TEXT, TOOL.GetFont(1, 12));

		    int GetAvailableWidth(Size BASE) =>
			CONTAINER.Width - BASE.Width - 15;

		    var IPLAB_TEXT = "IP:";
		    var IPLAB_SIZE = GetFontSize(IPLAB_TEXT);
		    var IPLAB_LOCA = new Point(6, 5);
		    var IPLAB_BCOL = Color.Transparent;
		    var IPLAB_FCOL = Color.White;

		    CONTROL.Label(CONTAINER, IP_LAB, IPLAB_SIZE, IPLAB_LOCA, IPLAB_BCOL, IPLAB_FCOL, 1, 12, IPLAB_TEXT);

		    var IPBOX_SIZE = new Size(GetAvailableWidth(IPLAB_SIZE), 20);
		    var IPBOX_LOCA = new Point(IPLAB_LOCA.X + IPLAB_SIZE.Width, 6);
		    var IPBOX_BCOL = Color.FromArgb(16, 16, 16);
		    var IPBOX_FCOL = Color.White;

		    CONTROL.TextBox(CONTAINER, IP_BOX, IPBOX_SIZE, IPBOX_LOCA, IPBOX_BCOL, IPBOX_FCOL, 1, 10, Color.Empty);
		    RoundTextBox();

		    var POLAB_TEXT = "Port:";
		    var POLAB_SIZE = GetFontSize(POLAB_TEXT);
		    var POLAB_LOCA = new Point(IPLAB_LOCA.X, IPLAB_LOCA.Y + IPLAB_SIZE.Height + 5);
		    var POLAB_BCOL = Color.Transparent;
		    var POLAB_FCOL = Color.White;

		    CONTROL.Label(CONTAINER, PO_LAB, POLAB_SIZE, POLAB_LOCA, POLAB_BCOL, POLAB_FCOL, 1, 12, POLAB_TEXT);

		    var POBOX_SIZE = new Size(GetAvailableWidth(POLAB_SIZE), IPBOX_SIZE.Height);
		    var POBOX_LOCA = new Point(POLAB_LOCA.X + POLAB_SIZE.Width, POLAB_LOCA.Y + 2);
		    var POBOX_BCOL = IPBOX_BCOL;
		    var POBOX_FCOL = IPBOX_FCOL;

		    CONTROL.TextBox(CONTAINER, PO_BOX, POBOX_SIZE, POBOX_LOCA, POBOX_BCOL, POBOX_FCOL, 1, 10, Color.Empty);
		    RoundTextBox();

		    var DULAB_TEXT = "Duration:";
		    var DULAB_SIZE = GetFontSize(DULAB_TEXT);
		    var DULAB_LOCA = new Point(IPLAB_LOCA.X, POLAB_LOCA.Y + POLAB_SIZE.Height + 5);
		    var DULAB_BCOL = Color.Transparent;
		    var DULAB_FCOL = Color.White;

		    CONTROL.Label(CONTAINER, DR_LAB, DULAB_SIZE, DULAB_LOCA, DULAB_BCOL, DULAB_FCOL, 1, 12, DULAB_TEXT);

		    var DUBOX_SIZE = new Size(GetAvailableWidth(DULAB_SIZE), IPBOX_SIZE.Height);
		    var DUBOX_LOCA = new Point(DULAB_LOCA.X + DULAB_SIZE.Width, DULAB_LOCA.Y + 2);
		    var DUBOX_BCOL = IPBOX_BCOL;
		    var DUBOX_FCOL = IPBOX_FCOL;

		    CONTROL.TextBox(CONTAINER, DR_BOX, DUBOX_SIZE, DUBOX_LOCA, DUBOX_BCOL, DUBOX_FCOL, 1, 10, Color.Empty);
		    RoundTextBox();

		    var RECTANGLE_SIZE = CONTAINER_SIZE;
		    var RECTANGLE_LOCA = new Point(0, 0);
		    var RECTANGLE_COLA = Color.FromArgb(CONTAINER_COLA.R - 8, CONTAINER_COLA.G - 8, CONTAINER_COLA.B - 8);

		    TOOL.PaintRectangle(CONTAINER, 4, RECTANGLE_SIZE, RECTANGLE_LOCA, RECTANGLE_COLA);
		}

		catch
		{
		    throw new Exception("IP/Port Section.");
		};
	    }
	};

	private class SETTINGSCONTAINER
	{
	    readonly static Form DIALOG = new Form();

	    private static void InitializeDialogy()
	    {
		try
		{
		    DIALOG.FormBorderStyle = FormBorderStyle.None;
		    DIALOG.BackColor = Color.FromArgb(24, 24, 24);

		    DIALOG.AutoScroll = true;

		    DIALOG.HorizontalScroll.Enabled = false;
		    DIALOG.HorizontalScroll.Visible = false;

		    DIALOG.VerticalScroll.Enabled = true;
		    DIALOG.VerticalScroll.Visible = true;

		    TOOL.Resize(DIALOG, new Size(200, 200));
		    TOOL.Round(DIALOG, 6);

		    DIALOG.StartPosition = FormStartPosition.CenterParent;

		    TOOL.PaintRectangle(DIALOG, 2, DIALOG.Size, new Point(0, 0), Color.FromArgb(8, 8, 8));
		}

		catch (Exception e)
		{
		    throw new Exception("Selector Container Initialization.");
		}
	    }

	    readonly static PictureBox BAR = new PictureBox();

	    readonly static Button CLOSE = new Button();

	    readonly static Label TITLE = new Label();

	    private static void InitializeMenuBar()
	    {
		try
		{
		    var BAR_SIZE = new Size(DIALOG.Width - 18, 26);
		    var BAR_LOCA = new Point(1, 1);
		    var BAR_COLA = Color.FromArgb(12, 12, 12);

		    CONTROL.Image(DIALOG, BAR, BAR_SIZE, BAR_LOCA, null, BAR_COLA);
		    TOOL.Interactive(BAR, DIALOG);

		    var TITLE_SIZE = Size.Empty;
		    var TITLE_LOCA = new Point(5, -1);
		    var TITLE_BCOL = BAR_COLA;
		    var TITLE_FCOL = Color.White;

		    CONTROL.Label(BAR, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 8, "Method Selector");
		    TOOL.Interactive(TITLE, DIALOG);

		    var CLOSE_SIZE = new Size(50, BAR_SIZE.Height);
		    var CLOSE_LOCA = new Point(BAR_SIZE.Width - CLOSE_SIZE.Width, 0);
		    var CLOSE_BCOL = BAR_COLA;
		    var CLOSE_FCOL = Color.White;

		    CONTROL.Button(BAR, CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 12, "X", Color.Empty);

		    CLOSE.Click += (s, e) =>
			DIALOG.Hide();
		}

		catch
		{
		    throw new Exception("Selector Container Menu Bar Initialization.");
		}
	    }

	    readonly static Label HTTP_TITLE = new Label() { Text = "HTTP Methods", TextAlign = ContentAlignment.MiddleCenter };

	    readonly static Button HTTP_DASHLORIS = new Button();
	    readonly static Button HTTP_SLOWLORIS = new Button();
	    readonly static Button HTTP_FLOOD = new Button();
	    readonly static Button HTTP_POST = new Button();
	    readonly static Button HTTP_PUT = new Button();
	    readonly static Button HTTP_GET = new Button();

	    readonly static int httpcount = 6;

	    readonly static Label TCP_TITLE = new Label() { Text = "TCP Methods", TextAlign = ContentAlignment.MiddleCenter };

	    readonly static Button TCP_LONGSOCKS = new Button();
	    readonly static Button TCP_SOCKS = new Button();
	    readonly static Button TCP_FLOOD = new Button();
	    readonly static Button TCP_WAVES = new Button();

	    readonly static int tcpcount = 4;

	    readonly static Label UDP_TITLE = new Label() { Text = "UDP Methods", TextAlign = ContentAlignment.MiddleCenter };

	    readonly static Button UDP_OVERLOAD = new Button();
	    readonly static Button UDP_FLOOD = new Button();
	    readonly static Button UDP_WAVES = new Button();
	    readonly static Button UDP_HAM = new Button();

	    private static void InitializeOptions()
	    {
		try
		{
		    var OPTIO_CONTR = new List<Button>()
		    {
			HTTP_DASHLORIS, HTTP_SLOWLORIS, HTTP_PUT, HTTP_POST, HTTP_GET, HTTP_FLOOD,
			TCP_LONGSOCKS, TCP_FLOOD, TCP_SOCKS, TCP_WAVES,
			UDP_OVERLOAD, UDP_WAVES, UDP_HAM, UDP_FLOOD
		    };

		    var OPTION_TEXT = new List<string>()
		    {
			"Dashloris 4.0", "Slowloris 1.0", "PUT Head", "POST Head", "GET Head", "H-Flood",
			"Long Socks", "Multi Flood", "Multi Socks", "Wavesss",
			"Overload", "Wavesss", "Go Ham", "Insta Flood",
		    };

		    var BUTTON_SIZE = new Size(DIALOG.Width - 19, 24);
		    var BUTTON_LOCA = new Point(2, BAR.Height + 1);
		    var BUTTON_BCOL = Color.FromArgb(150, Color.MidnightBlue.R, Color.MidnightBlue.G, Color.MidnightBlue.B);
		    var BUTTON_FCOL = Color.White;

		    int GetTitleHeight(string param) =>
			TextRenderer.MeasureText(param, TOOL.GetFont(1, 24)).Height;

		    var TITLE_CONTR = new List<Label>()
		    {
			HTTP_TITLE, TCP_TITLE, UDP_TITLE
		    };

		    var TITLE_SIZE = new Size(DIALOG.Width - 18, GetTitleHeight(TITLE_CONTR[0].Text));
		    var TITLE_BCOL = BAR.BackColor;
		    var TITLE_FCOL = Color.White;

		    for (int i = 0, k = 0; i < OPTION_TEXT.Count; i += 1)
		    {
			if (i == 0 || i == httpcount - 1 || i == httpcount + tcpcount - 1)
			{
			    var TITLE_LOCA = new Point(1, BUTTON_LOCA.Y);

			    CONTROL.Label(DIALOG, TITLE_CONTR[k], TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 12, TITLE_CONTR[k].Text);

			    BUTTON_LOCA.Y += TITLE_SIZE.Height; k += 1;
			}

			CONTROL.Button(DIALOG, OPTIO_CONTR[i], BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, OPTION_TEXT[i], Color.Empty);
			BUTTON_LOCA.Y += BUTTON_SIZE.Height;
		    };
		}

		catch (Exception e)
		{
		    throw new Exception("Selector Container Method Dialog Initialization.");
		}
	    }

	    public static void InitializeSelectorContainer(Form TOP)
	    {
		try
		{
		    InitializeDialogy();
		    InitializeMenuBar();
		    InitializeOptions();
		}

		catch (Exception e)
		{
		    throw new Exception(e.Message);
		}
	    }

	    readonly public static PictureBox CONTAINER = new PictureBox();
	    readonly static PictureBox PONY1 = new PictureBox();
	    readonly static PictureBox PONY2 = new PictureBox();

	    readonly public static Button METHODS = new Button();

	    readonly static Label METHOD_TITLE = new Label() { TextAlign = ContentAlignment.MiddleCenter };

	    public static void InitializeMCon(Form TOP)
	    {
		try
		{
		    var CONTAINER_SIZE = new Size(TARGETCONTAINER.CONTAINER.Width, 100);
		    var CONTAINER_LOCA = new Point(TARGETCONTAINER.CONTAINER.Left, TARGETCONTAINER.CONTAINER.Top + TARGETCONTAINER.CONTAINER.Height + 5);
		    var CONTAINER_BCOL = TARGETCONTAINER.CONTAINER.BackColor;

		    CONTROL.Image(TOP, CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_BCOL);

		    var RECTANGLE_SIZE = CONTAINER_SIZE;
		    var RECTANGLE_LOCA = new Point(0, 0);
		    var RECTANGLE_COLA = Color.FromArgb(CONTAINER_BCOL.R - 8, CONTAINER_BCOL.G - 8, CONTAINER_BCOL.B - 8);

		    TOOL.PaintRectangle(CONTAINER, 4, RECTANGLE_SIZE, RECTANGLE_LOCA, RECTANGLE_COLA);
		    TOOL.Round(CONTAINER, 6);

		    var TITLE_SIZE = Size.Empty;
		    var TITLE_LOCA = new Point(0, 5);
		    var TITLE_BCOL = Color.Transparent;
		    var TITLE_FCOL = Color.White;

		    CONTROL.Label(CONTAINER, METHOD_TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 16, "Methods");

		    METHOD_TITLE.Left = (CONTAINER_SIZE.Width - METHOD_TITLE.Width) / 2;

		    var METHOD_SIZE = new Size(175, 28);
		    var METHOD_LOCA = new Point((CONTAINER_SIZE.Width - METHOD_SIZE.Width) / 2, TITLE_LOCA.Y + METHOD_TITLE.Height + 5);
		    var METHOD_BCOL = Color.FromArgb(16, 16, 16);
		    var METHOD_FCOL = Color.White;

		    CONTROL.Button(CONTAINER, METHODS, METHOD_SIZE, METHOD_LOCA, METHOD_BCOL, METHOD_FCOL, 1, 10, "HTTP -- Dashloris 4.0", Color.Empty);
		    TOOL.Round(METHODS, 4);

		    METHODS.Click += (s, e) => DIALOG.ShowDialog();

		    var IMAGE1_IMAG = Properties.Resources.PONY_GIF1;
		    var IMAGE1_SIZE = IMAGE1_IMAG.Size;
		    var IMAGE1_LOCA = new Point(5, CONTAINER_SIZE.Height - IMAGE1_SIZE.Height);
		    var IMAGE1_COLA = Color.Transparent;

		    CONTROL.Image(CONTAINER, PONY1, IMAGE1_SIZE, IMAGE1_LOCA, IMAGE1_IMAG, IMAGE1_COLA);

		    var IMAGE2_IMAG = Properties.Resources.PONY_GIF2;
		    var IMAGE2_SIZE = IMAGE2_IMAG.Size;
		    var IMAGE2_LOCA = new Point(CONTAINER_SIZE.Width - IMAGE2_SIZE.Width - 4, CONTAINER_SIZE.Height - IMAGE1_SIZE.Height);
		    var IMAGE2_COLA = Color.Transparent;

		    CONTROL.Image(CONTAINER, PONY2, IMAGE2_SIZE, IMAGE2_LOCA, IMAGE2_IMAG, IMAGE2_COLA);

		    InitializeSelectorContainer(TOP);
		}

		catch (Exception e)
		{
		    throw new Exception(e.Message);
		};
	    }
	};

	private class LOGCONTAINER
	{
	    readonly public static RichTextBox LOG = new RichTextBox();
	    readonly public static PictureBox CONTAINER = new PictureBox();

	    readonly static Button CLEAR = new Button();

	    public static void SetupLogContainer()
	    {
		LOG.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
	    }

	    public static void InitializeLCon(Form TOP)
	    {
		try
		{
		    var CONTAINER_SIZE = new Size(TOP.Width - 2 - TARGETCONTAINER.CONTAINER.Left - 14 - TARGETCONTAINER.CONTAINER.Width, TARGETCONTAINER.CONTAINER.Height + 5 + SETTINGSCONTAINER.CONTAINER.Height);
		    var CONTAINER_LOCA = new Point(TOP.Width - 1 - CONTAINER_SIZE.Width - 8, TARGETCONTAINER.CONTAINER.Top);
		    var CONTAINER_COLA = TARGETCONTAINER.CONTAINER.BackColor;

		    CONTROL.Image(TOP, CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		    TOOL.Round(CONTAINER, 6);

		    var LOG_SIZE = new Size(CONTAINER_SIZE.Width - 10, CONTAINER_SIZE.Height - 16);
		    var LOG_LOCA = new Point(5, 8);
		    var LOG_COLA = CONTAINER_COLA;

		    CONTROL.RichTextBox(CONTAINER, LOG, LOG_SIZE, LOG_LOCA, LOG_COLA, Color.White, 1, 10, "Yes man.");

		    SetupLogContainer();

		    var CLEAR_SIZE = new Size(100, 26);
		    var CLEAR_LOCA = new Point((LOG_SIZE.Width - CLEAR_SIZE.Width) / 2, LOG_SIZE.Height - CLEAR_SIZE.Height);
		    var CLEAR_BCOL = Color.FromArgb(160, TOP.BackColor.R, TOP.BackColor.G, TOP.BackColor.B);
		    var CLEAR_FCOL = Color.White;

		    CONTROL.Button(LOG, CLEAR, CLEAR_SIZE, CLEAR_LOCA, CLEAR_BCOL, CLEAR_FCOL, 1, 10, "Clear", Color.Empty);

		    string GetLogText()
		    {
			var TEXT =
			(
			    "Waiting for you to do something...."
			);

			return TEXT;
		    }

		    LOG.Text = GetLogText();

		    CLEAR.Click += (s, e) =>
			LOG.Clear();

		    TOOL.Round(CLEAR, 6);

		    var RECTANGLE_SIZE = CONTAINER_SIZE;
		    var RECTANGLE_LOCA = new Point(0, 0);
		    var RECTANGLE_COLA = Color.FromArgb(CONTAINER_COLA.R - 8, CONTAINER_COLA.G - 8, CONTAINER_COLA.B - 8);

		    TOOL.PaintRectangle(CONTAINER, 4, RECTANGLE_SIZE, RECTANGLE_LOCA, RECTANGLE_COLA);
		}

		catch (Exception e)
		{
		    throw new Exception(e.Message);
		};
	    }
	}

	private class TASKBARCONTAINER
	{
	    readonly public static PictureBox BUTTON_CONTAINER = new PictureBox();
	    readonly public static PictureBox CONTAINER = new PictureBox();

	    readonly static Button ABOUT = new Button();
	    readonly static Button START = new Button();
	    readonly static Button PORTS = new Button();

	    public static void InitializeTCon(Form TOP)
	    {
		try
		{
		    var CONTAINER_SIZE = new Size(TOP.Width - 2, 28);
		    var CONTAINER_LOCA = new Point(1, TOP.Height - CONTAINER_SIZE.Height);
		    var CONTAINER_BCOL = MENUBAR.BAR.BackColor;

		    CONTROL.Image(TOP, CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_BCOL);

		    var BCONTAINER_SIZE = new Size(75 * 3 + 5 * 2, 28);
		    var BCONTAINER_LOCA = new Point((CONTAINER_SIZE.Width - BCONTAINER_SIZE.Width) / 2, 0);
		    var BCONTAINER_BCOL = CONTAINER_BCOL;

		    CONTROL.Image(CONTAINER, BUTTON_CONTAINER, BCONTAINER_SIZE, BCONTAINER_LOCA, null, BCONTAINER_BCOL);

		    var BUTTON_SIZE = new Size(75, 28);
		    var BUTTON_LOCA = new Point(0, 0);
		    var BUTTON_BCOL = BCONTAINER_BCOL;
		    var BUTTON_FCOL = Color.White;

		    CONTROL.Button(BUTTON_CONTAINER, ABOUT, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 12, "About", Color.Empty);
		    BUTTON_LOCA.X += BUTTON_SIZE.Width + 5;

		    CONTROL.Button(BUTTON_CONTAINER, START, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 12, "Start", Color.Empty);
		    BUTTON_LOCA.X += BUTTON_SIZE.Width + 5;

		    CONTROL.Button(BUTTON_CONTAINER, PORTS, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 12, "Ports", Color.Empty);
		}

		catch (Exception e)
		{
		    throw new Exception(e.Message);
		};
	    }
	}

	private class LOGINDIALOG
	{
	    readonly static PictureBox BAR_CONTAINER = new PictureBox();

	    readonly static Button CLOSE = new Button();
	    readonly static Button MINIM = new Button();

	    readonly static Label TITLE = new Label();

	    private static void InitializeMEU(Form TOP)
	    {
		var BAR_SIZE = new Size(TOP.Width, 28);
		var BAR_LOCA = new Point(0, 0);
		var BAR_COLA = Color.FromArgb(12, 12, 12);

		CONTROL.Image(TOP, BAR_CONTAINER, BAR_SIZE, BAR_LOCA, null, BAR_COLA);

		var BUTTON_SIZE = new Size(48, BAR_SIZE.Height);
		var BUTTON_LOCA = new Point(BAR_SIZE.Width - BUTTON_SIZE.Width, 0);
		var BUTTON_BCOL = BAR_COLA;
		var BUTTON_FCOL = Color.White;

		CONTROL.Button(BAR_CONTAINER, CLOSE, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "X", Color.Empty);

		BUTTON_LOCA.X -= BUTTON_SIZE.Width;

		CONTROL.Button(BAR_CONTAINER, MINIM, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "-", Color.Empty);

		CLOSE.Click += (s, e) => Application.Exit();
		MINIM.Click += (s, e) => TOP.SendToBack();

		var TITLE_TEXT = "Dash Authenticator";
		var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 9);
		var TITLE_LOCA = new Point((BAR_CONTAINER.Width - TITLE_SIZE.Width - (BUTTON_SIZE.Width * 2)) / 2, (BAR_CONTAINER.Height - TITLE_SIZE.Height) / 2);
		var TITLE_BCOL = BAR_CONTAINER.BackColor;
		var TITLE_FCOL = Color.White;

		CONTROL.Label(BAR_CONTAINER, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 9, TITLE_TEXT);

		TOOL.Interactive(BAR_CONTAINER, TOP);

		foreach (Control CON in BAR_CONTAINER.Controls)
		{
		    if (!(CON is Button))
		    {
			TOOL.Interactive(CON, TOP);
		    }
		}
	    }

	    public static void UndoChanges(Form TOP)
	    {
		foreach (Control Control in TOP.Controls)
		{
		    Control.Hide();
		}

		TOOL.Resize(TOP, new Size(450, 258));
	    }

	    private static void InitializeGUI(Form TOP)
	    {
		TOOL.Resize(TOP, new Size(225, 122));
		TOOL.Round(TOP, 6);
		
		TOP.BackgroundImage = null;
		TOP.BackColor = Color.FromArgb(18, 18, 18);

		TOP.FormBorderStyle = FormBorderStyle.None;
		TOP.StartPosition = FormStartPosition.CenterScreen;

		TOP.Icon = Properties.Resources.ICON_ICO;

		var RECTANGLE_SIZE = new Size(TOP.Width - 1, TOP.Height - 1);
		var RECTANGLE_LOCA = new Point(0, 0);
		var RECTANGLE_COLA = Color.FromArgb(8,8,8);

		TOOL.PaintRectangle(TOP, 2, RECTANGLE_SIZE, RECTANGLE_LOCA, RECTANGLE_COLA);
	    }

	    readonly static TextBox IUSERNAME = new TextBox() { TextAlign = HorizontalAlignment.Center };
	    readonly static TextBox IPASSWORD = new TextBox() { TextAlign = HorizontalAlignment.Center, PasswordChar = ' ' };

	    readonly static Label USERNAME = new Label();
	    readonly static Label PASSWORD = new Label();

	    private static void InitializeCRE(Form TOP)
	    {
		Size GetPreferredSize(string TEXT, int SIZE) =>
		    TextRenderer.MeasureText(TEXT, TOOL.GetFont(1, SIZE));

		var LABEL_SIZE = GetPreferredSize("Username:", 12);
		var LABEL_LOCA = new Point(5, 31);
		var LABEL_BCOL = TOP.BackColor;
		var LABEL_FCOL = Color.White;

		int GetLabelWidth() => TOP.Width - LABEL_SIZE.Width - 15;
		int GetLabelX() => LABEL_SIZE.Width + LABEL_LOCA.X;

		CONTROL.Label(TOP, USERNAME, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 12, "Username:");

		var BOX_SIZE = new Size(GetLabelWidth(), 20);
		var BOX_LOCA = new Point(GetLabelX(), LABEL_LOCA.Y + 1);
		var BOX_BCOL = Color.FromArgb(8, 8, 8);
		var BOX_FCOL = Color.White;

		Control GetAnonControl()=>TOP.Controls[TOP.Controls.Count - 1];

		CONTROL.TextBox(TOP, IUSERNAME, BOX_SIZE, BOX_LOCA, BOX_BCOL, BOX_FCOL, 1, 10, Color.Empty);
		TOOL.Round(GetAnonControl(), 6);

		LABEL_SIZE = GetPreferredSize("Password:", 12);
		LABEL_LOCA.Y += LABEL_SIZE.Height + 5;

		CONTROL.Label(TOP, PASSWORD, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 12, "Password:");

		BOX_SIZE = new Size(GetLabelWidth(), BOX_SIZE.Height);
		BOX_LOCA = new Point(GetLabelX(), LABEL_LOCA.Y + 1);

		CONTROL.TextBox(TOP, IPASSWORD, BOX_SIZE, BOX_LOCA, BOX_BCOL, BOX_FCOL, 1, 10, Color.Empty);
		TOOL.Round(GetAnonControl(), 6);
	    }

	    readonly static PictureBox BUTTON_CONTAINER = new PictureBox();

	    readonly public static Button LOGIN = new Button();
	    readonly public static Button HELP = new Button();

	    private static void InitializeAUT(Form TOP)
	    {
		var CONTAINER_SIZE = new Size(210, 26);
		var CONTAINER_LOCA = new Point((TOP.Width - CONTAINER_SIZE.Width) / 2, TOP.Height - CONTAINER_SIZE.Height - 7);
		var CONTAINER_COLA = TOP.BackColor;

		CONTROL.Image(TOP, BUTTON_CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		var BUTTON_SIZE = new Size(CONTAINER_SIZE.Width / 2 - 5, CONTAINER_SIZE.Height);
		var BUTTON_LOCA = new Point(0, 0);
		var BUTTON_BCOL = Color.MidnightBlue;
		var BUTTON_FCOL = Color.White;

		CONTROL.Button(BUTTON_CONTAINER, LOGIN, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 11, "Login", Color.Empty);
		TOOL.Round(LOGIN, 6);

		BUTTON_LOCA.X += BUTTON_SIZE.Width + 10;

		CONTROL.Button(BUTTON_CONTAINER, HELP, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 11, "Info", Color.Empty);
		TOOL.Round(HELP, 6);
	    }

	    public static void AuthenticatorInterf(Form TOP)
	    {
		try
		{
		    InitializeGUI(TOP);
		    InitializeMEU(TOP);
		    InitializeCRE(TOP);
		    InitializeAUT(TOP);
		}

		catch
		{
		    throw new Exception("AuthenticatorInterf()");
		}
	    }
	}

	readonly INFODIALOG HelpDialog = new INFODIALOG();

	public class INFODIALOG : Form
	{
	    public class GUICONTAINER
	    {
		public static void InitializeGUI(Form TOP)
		{
		    try
		    {
			TOP.StartPosition = FormStartPosition.CenterParent;
			TOP.FormBorderStyle = FormBorderStyle.None;

			var GUI_BCOL = Color.FromArgb(24, 24, 24);

			TOP.BackColor = GUI_BCOL;

			var GUI_SIZE = new Size(200, 200);

			TOOL.Resize(TOP, GUI_SIZE);
			TOOL.Round(TOP, 6);
		    }

		    catch
		    {
			throw new Exception("Help Dialog Initialize GUI.");
		    };
		}
	    }
	
	    public class MENUBARCONTAINER
	    {
		readonly static public PictureBox BAR = new PictureBox();

		readonly static public Label TITLE = new Label();

		readonly static public Button CLOSE = new Button();

		public static void InitializeMEB(Form TOP)
		{
		    var BAR_SIZE = new Size(TOP.Width, 24);
		    var BAR_LOCA = new Point(0, 0);
		    var BAR_COLA = Color.FromArgb(12, 12, 12);

		    CONTROL.Image(TOP, BAR, BAR_SIZE, BAR_LOCA, null, BAR_COLA);

		    var TITLE_TEXT = "Dash Information";
		    var TITLE_SIZE = TOOL.GetFontSize(TITLE_TEXT, 10);
		    var TITLE_LOCA = new Point(10, (BAR.Height - TITLE_SIZE.Height) / 2);
		    var TITLE_BCOL = BAR_COLA;
		    var TITLE_FCOL = Color.White;

		    CONTROL.Label(BAR, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 10, TITLE_TEXT);

		    var CLOSE_SIZE = new Size(50, BAR.Height);
		    var CLOSE_LOCA = new Point(BAR.Width - CLOSE_SIZE.Width, 0);
		    var CLOSE_BCOL = BAR_COLA;
		    var CLOSE_FCOL = TITLE_FCOL;

		    CONTROL.Button(BAR, CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 12, "X", Color.Empty);

		    CLOSE.Click += (s, e) =>
			TOP.Close();

		    var BORDER_SIZE = new Size(TOP.Width - 2, TOP.Height - BAR.Height - 1);
		    var BORDER_LOCA = new Point(1, BAR.Height);
		    var BORDER_COLA = BAR_COLA;

		    TOOL.PaintRectangle(TOP, 2, BORDER_SIZE, BORDER_LOCA, BORDER_COLA);
		    TOOL.Interactive(BAR, TOP);

		    foreach (Control CON in BAR.Controls)
		    {
			TOOL.Interactive(CON, TOP);
		    };
		}
	    }

	    public class MESSAGECONTAINER
	    {
		readonly static public TextBox INFOCONTAINER = new TextBox() { TextAlign = HorizontalAlignment.Center };

		public static void InitializeMEG(Form TOP)
		{
		    var INFO_SIZE = new Size(TOP.Width - 4, TOP.Height - MENUBARCONTAINER.BAR.Height - 2);
		    var INFO_LOCA = new Point(2, MENUBARCONTAINER.BAR.Height);
		    var INFO_BCOL = Color.MidnightBlue;
		    var INFO_FCOL = Color.White;

		    CONTROL.TextBox(TOP, INFOCONTAINER, INFO_SIZE, INFO_LOCA, INFO_BCOL, INFO_FCOL, 1, 9, Color.Empty, FIXEDSIZE:false, READONLY:true, SCROLLBAR:true, MULTILINE:true);

		    INFOCONTAINER.Text = (
			"Hey there, I am Dashie also known as the Developer of this application.\r\n\r\nI am very high right now but I am still coding.\r\n\r\nNice!" +
			"\r\n\r\n" +
			"The intention of this application was to allow people like yourself to test the bandwidth of your server legally.\r\n\r\nThis tool will offer you the ability to pick any method from the list of available methods with just a click.\r\n\r\nThe configuration is not too complicated either, I have taken user-friendly interactivity into mind while coding this." +
			"\r\n\r\n\r\n" +
			"This all sounds nice and all but the application is not done yet.\r\n\r\nThis is just a preview of that what awaits you people, mwahaha!!\r\n\r\nExpect a lot of updates in the future." +
			"\r\n\r\n" +
			"If you do find any issues during the use of this application then please let me know at KvinneKraft@protonmail.com.\r\n\r\nAs I have said, I am still working on developing this application to the fullest extent.  You can also reach out to me if you have any suggestions." +
			"\r\n\r\n" +
			"That is me.....DASHIE" +
			"\r\n\r\n" +
			"Ssshhh, I am still here!\r\n\r\nI am actually going to put more information in the actual utility section. I am soo high.....bye."
		    );
		}
	    }

	    public INFODIALOG()
	    {
		GUICONTAINER.InitializeGUI(this);
		MENUBARCONTAINER.InitializeMEB(this);
		MESSAGECONTAINER.InitializeMEG(this);
	    }
	}

	public class SPLASHSCREEN
	{
	    public static void SHOW(Form APP, int DURATION)
	    {
		APP.BackgroundImage = Properties.Resources.SPLASH_PNG;
		APP.FormBorderStyle = FormBorderStyle.None;
		APP.StartPosition = FormStartPosition.CenterScreen;

		TOOL.Resize(APP, APP.BackgroundImage.Size);

		APP.BackColor = Color.FromArgb(28, 28, 28);
		APP.TransparencyKey = APP.BackColor;
	
		var timer = new System.Timers.Timer(DURATION * 1000);

		timer.AutoReset = false;
		timer.Enabled = true;

		timer.Elapsed += (s, q) =>
		{
		    LOGINDIALOG.AuthenticatorInterf(APP);
		};

		timer.Start();
	    }
	}

	public Interface()
	{
	    try
	    {
		LOGINDIALOG.AuthenticatorInterf(this);

		LOGINDIALOG.LOGIN.Click += (s, e) =>
		{
		    // Do-Login-Work

		    LOGINDIALOG.UndoChanges(this);

		    InitializeComponent();
		    InitializeMBar();
		    InitializeBody();

		    TARGETCONTAINER.InitializeHCon(this);
		    SETTINGSCONTAINER.InitializeMCon(this);
		    LOGCONTAINER.InitializeLCon(this);
		    TASKBARCONTAINER.InitializeTCon(this);
		};
		
		LOGINDIALOG.HELP.Click += (s, e) =>
		{
		    if (!HelpDialog.Visible)
		    {
			HelpDialog.ShowDialog();
		    };
		};
	    }

	    catch (Exception e)
	    {
		var resu = MessageBox.Show(FORMAT.Replace("%m%", e.Message), "ERROR!", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

		switch (resu)
		{
		    case DialogResult.OK:
			Close();
			break;

		    default:
			Application.Restart();
			break;
		};
	    };

	    /*
	     Fix List:

	    - Reformat the class names.
	    - Reformat the method names.  They have to be logically named.
	    - Reorganize some pieces of code.
	    - Use DashTools.cs functionality where applicable.
	     
	     */
	}

	private void Interface_Load(object sender, EventArgs e)
	{

	}
    }
}
