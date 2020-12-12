using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Collections.Generic;

namespace DashSocietyF
{
    public class Mailer
    {
	private static void HandleMailError() =>
	    Tool.TranslateColors("&8(&c-&8) &fYou may not use this functionality.  You lack requirements.");
	
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

		    for (var k = 0; k < emails.Count; k += 250)
		    {
			using (var client = new SmtpClient(host))
			{
			    var password = File.ReadAllText(@"data\mail\password.txt");
			    var username = File.ReadAllText(@"data\mail\username.txt");

			    Console.WriteLine(password + "!" + username);

			    client.Credentials = new NetworkCredential(username, password);
			    client.EnableSsl = true;

			    using (var message = new MailMessage())
			    {
				message.Sender = new MailAddress($"{username}");
				message.From = new MailAddress($"{username}");

				message.Priority = MailPriority.High;
				message.Subject = "Hey there, I am Dashie!";

				message.IsBodyHtml = true;
				message.Body = html_body;

				message.To.Add($"yes{new Random().Next(1000, 5000)}@gmail.com");

				var recipients = new List<string>();
				var recips = "";

				for (int s = k; s < 250; s += 1)
				{
				    if (IsEmail(emails[s]))
				    {
					message.To.Add(emails[s]);
					recips += $"{emails[s]};";
				    };
				};

				client.Send(message);
				
				Tool.TranslateColors($"&8(&a+&8) &fEmail sent to: {recips} | {message.CC.Count} | {message.To.Count}\r\n");
			    };
			};
		    };

		    return;
		};

		throw new Exception("!1");
	    }

	    catch (Exception e)
	    {
		Console.WriteLine(e.Message);
		Tool.TranslateColors($"&8(&6!&8) &fOn cooldown, waiting....");
		System.Threading.Thread.Sleep(60000);
		goto redo;
		//HandleMailError();
	    };
	}
    }
}
