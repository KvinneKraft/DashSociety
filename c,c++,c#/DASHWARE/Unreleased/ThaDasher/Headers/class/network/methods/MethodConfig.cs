
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
	    public static TextBox BYTES = new TextBox();
	    public static TextBox RANDOM_USER_AGENT = new TextBox();

	    public static class DASHLORIS4
	    {
		public static PictureBox DASHLORIS4_CONTAINER = new PictureBox();

		public static TextBox CONNECTION_BURST_DELAY = new TextBox();
		public static TextBox CYCLE_CONNECTION_COUNT = new TextBox();
		public static TextBox CONNECTION_LIVE_TIME = new TextBox();
	    }

	    public static class SLOWLORIS2
	    {
		public static PictureBox SLOWLORIS2_CONTAINER = new PictureBox();

		public static TextBox CONNECTION_BURST_DELAY = new TextBox();
		public static TextBox CONNECTION_LIVE_TIME = new TextBox();
	    }

	    public static class PUTPOSTGET
	    {
		public static PictureBox PUTPOSTGET_CONTAINER = new PictureBox();

		public static TextBox CONNECTION_TIMEOUT = new TextBox();
		public static TextBox CORE_WORKERS = new TextBox();
		public static TextBox WORKERS = new TextBox();
		public static TextBox HEADER = new TextBox();
	    }

	    public static class HFLOOD
	    {
		public static PictureBox HFLOOD_CONTAINER = new PictureBox();

		public static TextBox CONNECTIONS_PER_THREAD = new TextBox();
		public static TextBox THREADS = new TextBox();
		public static TextBox HEADER = new TextBox();
	    }
	}

	private void InitializeHTTPConfiguration()
	{

	}

	public static readonly Dictionary<string, Dictionary<string, string>> TCP_CONFIGURATION = new Dictionary<string, Dictionary<string, string>>();

	public class TCP
	{
	    // Yet to design
	}
	
	private void InitializeTCPConfiguration()
	{

	}

	public static readonly Dictionary<string, Dictionary<string, string>> UDP_CONFIGURATION = new Dictionary<string, Dictionary<string, string>>();

	public class UDP
	{
	    // Yet to design
	}
	
	private void InitializeUDPConfiguration()
	{

	}

	private void InitializeConfCon()
	{
	    /*Configuration*/ try
	    {
		var CONT_SIZE = new Size(Width - 20, Height - BAR.Height - 20);
		var CONT_LOCA = new Point(-1, -1);
		var CONT_BCOL = Color.FromArgb(255, 229, 153); //255, 229, 153);

		MultiForm(this, CONTAINER, CONT_SIZE, CONT_LOCA, CONT_BCOL);
		
		CONTAINER.VerticalScroll.Visible = true;
		CONTAINER.VerticalScroll.Enabled = true;

		var RECT_SIZE = new Size(CONT_SIZE.Width + 1, CONT_SIZE.Height + 1);
		var RECT_LOCA = new Point(CONTAINER.Left - 1, CONTAINER.Top - 1);
		var RECT_COLA = Color.Navy; //BAR.BackColor;

		TOOL.PaintRectangle(this, 2, RECT_SIZE, RECT_LOCA, RECT_COLA);
	    }

	    catch
	    {
		throw new Exception("InitializeConfCon[CONTAINER]");
	    };

	    /*Configuration*/ try
	    {
		// Dictionary<string[TYPE_METHOD], List<string[VALUES]>>
		// Since we know the method, we also know what values to extract.
		//
		
		try
		{
		    InitializeHTTPConfiguration();
		}

		catch
		{
		    throw new Exception("HTTP_MODULE");
		};

		try
		{
		    InitializeTCPConfiguration();
		}

		catch
		{
		    throw new Exception("TCP_MODULE");
		};

		try
		{
		    InitializeUDPConfiguration();
		}

		catch
		{
		    throw new Exception("UDP_MODULE");
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

		// Add all Controls per Method
		// Hide all.
		// Show the selected one.
	    }

	    catch (Exception e)
	    {
		throw new Exception($"MethodCnfig -> {e.Message}()");
	    };
	}

	new public static void Show()
	{
	    APP.Show();
	}
    }
}