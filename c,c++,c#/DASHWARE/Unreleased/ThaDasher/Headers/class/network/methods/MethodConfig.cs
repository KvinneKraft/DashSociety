
//
// Author: Dashie
// Version: 1.0
//

using System;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class MethodConfig : Form
    {
	readonly static DashControls CONTROL = new DashControls();
	readonly static DashTools TOOL = new DashTools();

	private void InitializeComponent()
	{
	    this.SuspendLayout();

	    this.BackColor = Color.MidnightBlue;

	    this.StartPosition = FormStartPosition.CenterScreen;
	    this.FormBorderStyle = FormBorderStyle.None;

	    TOOL.Resize(this, new Size(250, 300));

	    this.Text = "Method Configurator";
	    this.Name = "MethodConfig";

	    this.ResumeLayout(false);
	}

	readonly static PictureBox BAR = new PictureBox();

	readonly static Button CLOSE = new Button();

	readonly static Label TITLE = new Label();

	private void InitializeMenuBar()
	{
	    try
	    {
		var BAR_SIZE = new Size(Width, 26);
		var BAR_LOCA = new Point(0, 0);
		var BAR_BCOL = Color.FromArgb(8, 8, 8);

		CONTROL.Image(this, BAR, BAR_SIZE, BAR_LOCA, null, BAR_BCOL);
		TOOL.Interactive(BAR, this);

		var CLOSE_SIZE = new Size(65, 26);
		var CLOSE_LOCA = new Point(BAR_SIZE.Width - CLOSE_SIZE.Width - 1, 0);
		var CLOSE_BCOL = BAR_BCOL;
		var CLOSE_FCOL = Color.White;

		CONTROL.Button(BAR, CLOSE, CLOSE_SIZE, CLOSE_LOCA, CLOSE_BCOL, CLOSE_FCOL, 1, 10, "X", Color.Empty);
		CLOSE.Click += (s, e) => Close();

		var TITLE_SIZE = TOOL.GetFontSize(Text, 9);
		var TITLE_LOCA = new Point(10, (BAR_SIZE.Height - TITLE_SIZE.Height) / 2);
		var TITLE_BCOL = BAR_BCOL;
		var TITLE_FCOL = Color.White;

		CONTROL.Label(BAR, TITLE, TITLE_SIZE, TITLE_LOCA, TITLE_BCOL, TITLE_FCOL, 1, 9, Text);

		foreach (Control control in BAR.Controls)
		{
		    TOOL.Interactive(control, this);
		};

		var RECT_SIZE = new Size(Width - 1, Height - BAR_SIZE.Height);
		var RECT_LOCA = new Point(0, BAR_SIZE.Height - 1);
		var RECT_BCOL = BAR_BCOL;

		TOOL.PaintRectangle(this, 2, RECT_SIZE, RECT_LOCA, RECT_BCOL);
	    }

	    catch
	    {
		throw new Exception("InitializeMenuBar");
	    }
	}

	readonly static Form CONTAINER = new Form();

	public void MultiForm(Form TOP, Form FORM, Size SIZE, Point LOCA, Color BCOL)
	{
	    TOOL.Resize(FORM, SIZE);

	    if (LOCA.X < 0)
	    {
		LOCA.X = (TOP.Width - SIZE.Width) / 2;
	    };

	    if (LOCA.Y < 0)
	    {
		LOCA.Y = ((TOP.Height + BAR.Height) - SIZE.Height) / 2;
	    };

	    FORM.BackColor = BCOL;
	    FORM.Location = LOCA;

	    FORM.FormBorderStyle = FormBorderStyle.None;
	    FORM.TopLevel = false;

	    TOP.Controls.Add(FORM);

	    FORM.Show();
	}

	// METHOD_CONFIGURATION["HTTP->DASHLORIS4"]["CONNECTION_BURST_DELAY"];
	public static readonly Dictionary<string, Dictionary<string, string>> HTTP_CONFIGURATION = new Dictionary<string, Dictionary<string, string>>();

	public class HTTP
	{
	    public static class DASHLORIS4
	    {
		public static PictureBox DASHLORIS4_CONTAINER = new PictureBox() { Visible = false };

		public static TextBox CONNECTION_BURST_DELAY = new TextBox() { Text = "5000" };
		public static TextBox CYCLE_CONNECTION_COUNT = new TextBox() { Text = "32" };
		public static TextBox CONNECTION_LIVE_TIME = new TextBox() { Text = "5000" };
		public static TextBox RANDOM_USER_AGENT = new TextBox() { Text = "True" };
		public static TextBox BYTES = new TextBox() { Text = "1000" };

		public static TextBox LABEL_1 = new TextBox() { Text = "C.B.D > (Connection Burst Delay)" };
		public static TextBox LABEL_2 = new TextBox() { Text = "C.C.C > (Cycle Connection Count)" };
		public static TextBox LABEL_3 = new TextBox() { Text = "C.L.T > (Connection Live Time)" };
		public static TextBox LABEL_4 = new TextBox() { Text = "R.U.A > (Random User-Agent[true/false])" };
		public static TextBox LABEL_5 = new TextBox() { Text = "BYTES > (Header Size Basically)" };

		public static Button IAMDONE = new Button();
	    }

	    public static class SLOWLORIS2
	    {
		public static PictureBox SLOWLORIS2_CONTAINER = new PictureBox() { Visible = false };

		public static TextBox CONNECTION_BURST_DELAY = new TextBox() { Text = "3500" };
		public static TextBox CONNECTION_LIVE_TIME = new TextBox() { Text = "3500" };
		public static TextBox RANDOM_USER_AGENT = new TextBox() { Text = "True" };
		public static TextBox HEADER = new TextBox() { Text = "Click Me To Change" };
		public static TextBox BYTES = new TextBox() { Text = "1000" };

		public static TextBox LABEL_1 = new TextBox() { Text = "C.B.D > (Connection Burst Delay)" };
		public static TextBox LABEL_2 = new TextBox() { Text = "C.L.T > (Connection Live Time)" };
		public static TextBox LABEL_3 = new TextBox() { Text = "R.U.A > (Random User-Agent[true/false])" };
		public static TextBox LABEL_4 = new TextBox() { Text = "H.T.S > (Header To Send)" };
		public static TextBox LABEL_5 = new TextBox() { Text = "BYTES > (Header Size Basically)" };

		public static Button IAMDONE = new Button();
	    }

	    public static class PUTPOSTGET
	    {
		public static PictureBox PUTPOSTGET_CONTAINER = new PictureBox() { Visible = false };

		public static TextBox CONNECTION_TIMEOUT = new TextBox() { Text = "500" };
		public static TextBox RANDOM_USER_AGENT = new TextBox() { Text = "True" };
		public static TextBox CORE_WORKERS = new TextBox() { Text = "4" };
		public static TextBox WORKERS = new TextBox() { Text = "16" };
		public static TextBox HEADER = new TextBox() { Text = "Click Me To Change" };
		public static TextBox BYTES = new TextBox() { Text = "1000" };

		public static TextBox LABEL_1 = new TextBox() { Text = "C.T.. > (Connection Timeout)" };
		public static TextBox LABEL_2 = new TextBox() { Text = "R.U.A > (Random User-Agent[true/false])" };
		public static TextBox LABEL_3 = new TextBox() { Text = "C.C.W > (Core Connection Workers)" };
		public static TextBox LABEL_4 = new TextBox() { Text = "W.... > (Workers)" };
		public static TextBox LABEL_5 = new TextBox() { Text = "H.T.S > (Header To Send)" };
		public static TextBox LABEL_6 = new TextBox() { Text = "BYTES > (Header Size Basically)" };

		public static Button IAMDONE = new Button();
	    }

	    public static class HFLOOD
	    {
		public static PictureBox HFLOOD_CONTAINER = new PictureBox() { Visible = false };

		public static TextBox CONNECTIONS_PER_THREAD = new TextBox() { Text = "32" };
		public static TextBox RANDOM_USER_AGENT = new TextBox() { Text = "True" };
		public static TextBox WORKERS = new TextBox() { Text = "32" };
		public static TextBox HEADER = new TextBox() { Text = "Click Me To Change" };
		public static TextBox BYTES = new TextBox() { Text = "1000" };

		public static TextBox LABEL_1 = new TextBox() { Text = "C.P.T > (Connections Per Thread)" };
		public static TextBox LABEL_2 = new TextBox() { Text = "R.U.A > (Random User-Agent[true/false])" };
		public static TextBox LABEL_3 = new TextBox() { Text = "W.... > (Workers)" };
		public static TextBox LABEL_4 = new TextBox() { Text = "H.T.S > (Header To Send)" };
		public static TextBox LABEL_5 = new TextBox() { Text = "BYTES > (Header Size Basically)" };

		public static Button IAMDONE = new Button();
	    }

	    public void InitializeHTTPConfiguration()
	    {
		//
		// Add HTTP Options to Containers. Start with Dashloris
		// 
		//
		// Dictionary<string[TYPE_METHOD], List<string[VALUES]>>
		// Since we know the method, we also know what values to extract.
		//
	
		var CONTAINER_SIZE = new Size(CONTAINER.Width - 20, CONTAINER.Height);
		var CONTAINER_LOCA = new Point(0, 0);
		var CONTAINER_COLA = Color.FromArgb(64, 25, 112);

		var OPTION_SIZE = new Size(CONTAINER_SIZE.Width - 165, 26);
		var OPTION_LOCA = new Point(165, 0);
		var OPTION_BCOL = Color.FromArgb(64, 13, 53);//112, 25, 45);
		var OPTION_FCOL = Color.White;

		var LABEL_SIZE = new Size(OPTION_LOCA.X, OPTION_SIZE.Height);
		var LABEL_LOCA = new Point(0, 0);
		var LABEL_BCOL = Color.FromArgb(40, 13, 64);//64, 13, 25);
		var LABEL_FCOL = Color.White;

		void UpdateY(PictureBox container)
		{
		    OPTION_LOCA.Y = container.Controls[container.Controls.Count - 1].Height + container.Controls[container.Controls.Count - 1].Top;
		    LABEL_LOCA.Y = OPTION_LOCA.Y;
		};

		try
		{
		    CONTROL.Image(CONTAINER, DASHLORIS4.DASHLORIS4_CONTAINER, CONTAINER_SIZE, CONTAINER_LOCA, null, CONTAINER_COLA);

		    CONTROL.TextBox(DASHLORIS4.DASHLORIS4_CONTAINER, DASHLORIS4.LABEL_1, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		    CONTROL.TextBox(DASHLORIS4.DASHLORIS4_CONTAINER, DASHLORIS4.CYCLE_CONNECTION_COUNT, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		    UpdateY(DASHLORIS4.DASHLORIS4_CONTAINER);

		    CONTROL.TextBox(DASHLORIS4.DASHLORIS4_CONTAINER, DASHLORIS4.LABEL_2, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		    CONTROL.TextBox(DASHLORIS4.DASHLORIS4_CONTAINER, DASHLORIS4.CONNECTION_BURST_DELAY, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		    UpdateY(DASHLORIS4.DASHLORIS4_CONTAINER);

		    CONTROL.TextBox(DASHLORIS4.DASHLORIS4_CONTAINER, DASHLORIS4.LABEL_3, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		    CONTROL.TextBox(DASHLORIS4.DASHLORIS4_CONTAINER, DASHLORIS4.CONNECTION_LIVE_TIME, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		    UpdateY(DASHLORIS4.DASHLORIS4_CONTAINER);

		    CONTROL.TextBox(DASHLORIS4.DASHLORIS4_CONTAINER, DASHLORIS4.LABEL_4, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		    CONTROL.TextBox(DASHLORIS4.DASHLORIS4_CONTAINER, DASHLORIS4.RANDOM_USER_AGENT, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		    UpdateY(DASHLORIS4.DASHLORIS4_CONTAINER);

		    CONTROL.TextBox(DASHLORIS4.DASHLORIS4_CONTAINER, DASHLORIS4.LABEL_5, LABEL_SIZE, LABEL_LOCA, LABEL_BCOL, LABEL_FCOL, 1, 10, Color.Empty);
		    CONTROL.TextBox(DASHLORIS4.DASHLORIS4_CONTAINER, DASHLORIS4.BYTES, OPTION_SIZE, OPTION_LOCA, OPTION_BCOL, OPTION_FCOL, 1, 10, Color.Empty);

		    var BUTTON_SIZE = new Size(DASHLORIS4.DASHLORIS4_CONTAINER.Width, 26);
		    var BUTTON_LOCA = new Point(0, OPTION_LOCA.Y + OPTION_SIZE.Height);
		    var BUTTON_BCOL = Color.FromArgb(13, 64, 30);
		    var BUTTON_FCOL = Color.White;

		    CONTROL.Button(DASHLORIS4.DASHLORIS4_CONTAINER, DASHLORIS4.IAMDONE,  BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 10, "I am done mate.", Color.Empty);

		    DASHLORIS4.IAMDONE.Click += (s, e) =>
		    {

		    };

		    DASHLORIS4.DASHLORIS4_CONTAINER.Visible = true;
		}

		catch
		{
		    throw new Exception("DASHLORIS4");
		};
	    }
	}

	public static readonly HTTP http = new HTTP();

	public static readonly Dictionary<string, Dictionary<string, string>> TCP_CONFIGURATION = new Dictionary<string, Dictionary<string, string>>();

	public class TCP
	{
	    public void InitializeTCPConfiguration()
	    {

	    }
	}

	public static readonly TCP tcp = new TCP();

	public static readonly Dictionary<string, Dictionary<string, string>> UDP_CONFIGURATION = new Dictionary<string, Dictionary<string, string>>();

	public class UDP
	{
	    public void InitializeUDPConfiguration()
	    {

	    }
	}

	public static readonly UDP udp = new UDP();

	private void InitializeConfCon()
	{
	    /*Configuration*/ try
	    {
		var CONT_SIZE = new Size(Width - 20, Height - BAR.Height - 20);
		var CONT_LOCA = new Point(-1, -1);
		var CONT_BCOL = BAR.BackColor;

		MultiForm(this, CONTAINER, CONT_SIZE, CONT_LOCA, CONT_BCOL);
		
		CONTAINER.VerticalScroll.Visible = true;
		CONTAINER.VerticalScroll.Enabled = true;

		var RECT_SIZE = new Size(CONT_SIZE.Width + 1, CONT_SIZE.Height + 1);
		var RECT_LOCA = new Point(CONTAINER.Left - 1, CONTAINER.Top - 1);
		var RECT_COLA = BAR.BackColor;

		TOOL.PaintRectangle(this, 2, RECT_SIZE, RECT_LOCA, RECT_COLA);
	    }

	    catch
	    {
		throw new Exception("InitializeConfCon[CONTAINER]");
	    };

	    /*Configuration*/ try
	    {
		try
		{
		    http.InitializeHTTPConfiguration();
		}

		catch (Exception e)
		{
		    throw new Exception($"HTTP_MODULE -> {e.Message}");
		};

		try
		{
		    tcp.InitializeTCPConfiguration();
		}

		catch (Exception e)
		{
		    throw new Exception($"TCP_MODULE -> {e.Message}");
		};

		try
		{
		    udp.InitializeUDPConfiguration();
		}

		catch (Exception e)
		{
		    throw new Exception($"UDP_MODULE -> {e.Message}");
		};

		foreach (Control control in CONTAINER.Controls)
		{
		    foreach (Control scontrol in control.Controls)
		    {
			foreach (Control sscontrol in scontrol.Controls)
			{
			    if (sscontrol is TextBox)
			    {
				if (!sscontrol.Text.Contains(")"))
				{
				    ((TextBox)sscontrol).TextAlign = HorizontalAlignment.Center;
				};
			    };
			};
		    };
		};
	    }

	    catch (Exception e)
	    {
		throw new Exception($"InitializeConfCon[CONFIGURATION -> {e.Message}]");
	    };
	}

	static Form APP;

	public MethodConfig()
	{
	    InitializeComponent();

	    APP = this;

	    try
	    {
		InitializeMenuBar();
		InitializeConfCon();
	    }

	    catch (Exception e)
	    {
		throw new Exception($"MethodConfig -> {e.Message}()");
	    };
	}

	new public static void Show()
	{
	    // When called check method and show container based off of that:
	    // Also make sure to resize the configurator dialog to the fit size.
	    APP.Show();
	}
    }
}