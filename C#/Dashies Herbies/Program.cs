
// Author: Dashie
// Version: 1.0

using System;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DashCore
{
    public class Program
    {
	private static readonly Captcha captcha = new Captcha();
	private static readonly Product product = new Product();

	[STAThread]
	static void Main(string[] args)
	{
	    product.initdb();

	    initapp();

	    dush.halt();
	}

	private static void initapp()
	{
	    Console.SetWindowSize(50, 20);
	    Console.SetBufferSize(50, 20);

	    new Thread
	    (() => {
		List<string> messages = new List<string>()
		{
		    "I got the good stuff.", "You can buy things!", "We have an economy.", "by Dashieeee"
		};

		Random rand = new Random();

		while (true)
		{
		    Console.Title = $"Dashies Herbies 1.0 - {messages[rand.Next(messages.Count - 1)]}";
		    Thread.Sleep(4000);
		};
	    })
	    { IsBackground = true }.Start();
	}
    };
};
