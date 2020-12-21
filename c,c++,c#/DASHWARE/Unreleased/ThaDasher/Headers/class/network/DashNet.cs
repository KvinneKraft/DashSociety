
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

	readonly static public Dictionary<string, string> target_info = new Dictionary<string, string>();

	public void SendAttack(string shost, int sport, int sdura)
	{
	    var m = SettingsContainer.CURRENT_METHOD.ToLower();

	    target_info.Add("host", $"{shost}");
	    target_info.Add("port", $"{sport}");
	    target_info.Add("dura", $"{sdura}");

	    // Link this one up with the method selector.

	    // Add all option controls individually to dialog and display only the ones that are accepted
	    // by the current method.
	    //
	    // For example, dashloris 4.0 would only show:
	    // connection_delay, connection_live_time, cycle_connection_count and random_user_agent;
	    //
	    // First work on the configuration manager for the methods.  It should be shown right after
	    // the user clicks the attack button. 
	    //

	    switch (m)
	    {
		case "dashloris 4.0": break;
		case "slowloris 1.0": break;
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
