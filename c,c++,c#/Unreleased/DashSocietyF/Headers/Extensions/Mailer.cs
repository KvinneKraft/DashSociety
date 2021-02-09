using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Net.Mail;
using System.Threading;
using System.Collections.Generic;

namespace DashSocietyF
{
    public class Mailer
    {
	private static bool IsEmail(string address)
	{
	    try
	    {
		if (new MailAddress(address).Address != address)
		{
		    throw new Exception("!");
		};

		return true;
	    }

	    catch
	    {
		return false;
	    };
	}

	private static void MailHelp()
	{
	    string message = 
	    (
		"\r\n" +
		"\r\n" +
		"\r\n" +
		"\r\n" +
		"\r\n"
	    );

	    Tool.TranslateColors(message);
	}

	public static void Run(string[] args)
	{
	    // !mail --TARGET person@gmail.com 
	    if (args.Length <= 1)
	    {
		MailHelp();
		return;
	    };

	    var para = args.ToList();

	    para.RemoveAt(0);

	    for (int k = 0; k < para.Count; k += 1)
		para[k] = para[k].ToLower();

	    string Get(string a) =>
		para[para.IndexOf(a) + 1].ToLower();
	    
	}

	private static void HandleMailError() =>
	    Tool.TranslateColors("&8(&c-&8) &fYou may not use this functionality.  You lack requirements.");

	public static void Show()
	{
	    var emails = new List<string>();

	    foreach (string str in File.ReadAllLines(@"data\mail\addresses.txt"))
	    {
		emails.Add(str);
	    };

	    var html_body = string.Empty;

	    foreach (var line in File.ReadAllLines(@"data\mail\body.txt"))
	    {
		html_body += $"{line}\r\n";
	    };

	    int o = 0;

	redo:

	    try
	    {
		var files = new List<string>() { "mail\\password.txt", "mail\\username.txt", "mail\\body.txt", "mail\\addresses.txt", "mail\\host.txt" };

		if (Directory.Exists("data\\") && Directory.Exists("data\\mail\\"))
		{
		    foreach (var file in files)
		    {
			if (!File.Exists($"data\\{file}"))
			{
			    throw new Exception("!2");
			};
		     };

		    var host = File.ReadAllText(@"data\mail\host.txt");

		    for (var k = o; k < emails.Count; o = k, k += 1)
		    {
			using (var client = new SmtpClient(host))
			{
			    var password = File.ReadAllText(@"data\mail\password.txt");
			    var username = File.ReadAllText(@"data\mail\username.txt");

			    client.Credentials = new NetworkCredential(username, password);
			    client.EnableSsl = false;

			    using (var message = new MailMessage())
			    {
				message.Sender = new MailAddress($"{username}");
				message.From = new MailAddress($"{username}");

				message.Priority = MailPriority.High;
				message.Subject = "Pugpawz - Press Release";

				message.IsBodyHtml = true;
				message.Body = html_body;
				
				var recips = "";

				message.To.Add(emails[k]);
				recips += $"{emails[k]};";
				
				client.Send(message);
				
				Tool.TranslateColors($"&8(&a+&8) &fEmail sent to: {recips}\r\n");
			    };
			};
		    };

		    return;
		};

		throw new Exception("!1");
	    }

	    catch (Exception e)
	    {
		Tool.TranslateColors($"&8(&6!&8) &fOn cooldown, waiting [{e.Message}]....");
		Thread.Sleep(60000);

		goto redo;
	    };
	}
    }
}
