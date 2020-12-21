using System;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ThaDasher
{
    public class TaskbarContainer
    {
	readonly static DashControls CONTROL = new DashControls();
	readonly static DashTools TOOL = new DashTools();

	readonly public static PictureBox BUTTON_CONTAINER = new PictureBox();
	readonly public static PictureBox CONTAINER = new PictureBox();

	readonly public static Button START = new Button();
	readonly static Button ABOUT = new Button();
	readonly static Button PORTS = new Button();

	private static void SendError(string s)
	{
	    void print() =>
    		LogContainer.LOG.AppendText($"(!) {s}\r\n");

	    if (LogContainer.LOG.InvokeRequired)
	    {
		LogContainer.LOG.Invoke(new MethodInvoker(
		    delegate () {
			print();
		    }
		));
	    }

	    else
	    {
		print();
	    }
	}

	private static void SendMessage(string s)
	{
	    void print() =>
		LogContainer.LOG.AppendText($"(+) {s}\r\n");

	    if (LogContainer.LOG.InvokeRequired)
	    {
		LogContainer.LOG.Invoke(new MethodInvoker(
		    delegate () {
			print();
		    }
		));
	    }

	    else
	    {
		print();
	    }
	}

	readonly static DashNet DNet = new DashNet();
	
	private static void HandleStartEvent()
	{
	    try
	    { 
		bool isInteger(string s) => int.TryParse(s, out _);

		var r_host = TargetContainer.IP_BOX.Text.ToLower();
		string host;

		if (!IPAddress.TryParse(r_host, out IPAddress ham))
		{
		    if (!r_host.Contains("http://") && !r_host.Contains("https://")) r_host = "https://" + r_host;

		    if (!Uri.TryCreate(r_host, UriKind.RelativeOrAbsolute, out Uri bacon))
		    {
			SendError("No IPv4 or Uri host value!");
			return;
		    };

		    try
		    {
			host = Dns.GetHostAddresses(bacon.Host)[0].ToString();
		    }

		    catch
		    {
			SendError("Invalid host value!");
			return;
		    };
		}

		else
		{
		    host = ham.ToString();

		    if (ham.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
		    {
			SendError("Invalid IPv4 host value!");
			return;
		    };
		};

		var r_port = TargetContainer.PO_BOX.Text;
		
		if (!isInteger(r_port))
		{
		    SendError("None integral port value!");
		    return;
		};

		var port = int.Parse(r_port);

		if (port < 1 || port > 65355)
		{
		    SendError("Port must be within range (1-65535).");
		    return;
		};

		var r_dura = TargetContainer.DR_BOX.Text;

		if (!isInteger(r_dura))
		{
		    SendError("None integral duration value!");
		    return;
		};

		var dura = int.Parse(r_dura);

		if (dura > 360)
		    SendMessage("You have set your duration above 360 seconds which is not recommended.");

		SendMessage($"Configuration:\r\n    - Host={TargetContainer.IP_BOX.Text}\r\n    - Port={port}\r\n    - Duration={dura}s\r\n");

		DNet.SendAttack(host, port, dura);
	    }

	    catch
	    {
		throw new Exception("HandleStartEvent()");
	    }
	}

	private static void HandleStopEvent()
	{
	    try
	    {
		DNet.StopWorkers();
	    }

	    catch
	    {
		throw new Exception("HandleStopEvent()");
	    }
	}

	readonly static PortScanner PORTSCANNER = new PortScanner();

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

		START.Click += (s, e) =>
		{
		    if (START.Text != "Stop")
		    {
			START.Text = "Stop";

			new Thread(() =>
			{
			    HandleStartEvent();
			})

			{ IsBackground = true }.Start();
		    }

		    else
		    {
			HandleStopEvent();
		    };
		};

		CONTROL.Button(BUTTON_CONTAINER, PORTS, BUTTON_SIZE, BUTTON_LOCA, BUTTON_BCOL, BUTTON_FCOL, 1, 12, "Ports", Color.Empty);

		PORTS.Click += (s, e) =>
		{
		    if (!PORTSCANNER.Visible)
		    {
			PORTSCANNER.ShowDialog();
		    };
		};
	    }

	    catch (Exception e)
	    {
		throw new Exception(e.Message);
	    };
	}
    }
}