using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DashSocietyF
{
    public class DashAuth
    {
	static string GetCaptcha()
	{
	    var rand = new Random();
	    var stri = string.Empty;

	    for (int k = 0; k < 9; k += 1)
	    {
		var c = ((char)rand.Next('a', 'z')).ToString();

		if (rand.Next(0, 2) == 1)
		{
		    c = c.ToUpper();
		};

		stri += c;
	    };

	    return stri;
	}

	public static void Show()
	{
	    Tool.TranslateColors($"&c| \r\n");
	    Tool.TranslateColors($"&c|             &8([&dDash Authentication Challenge&8])            \r\n");
	    Tool.TranslateColors($"&c| \r\n");
	    Tool.TranslateColors($"&c|   &fHey there, in order to access the gateway to Dash Land \r\n");
	    Tool.TranslateColors($"&c| &fyou must first show me that you are able to type properly.\r\n");
	    Tool.TranslateColors($"&c| \r\n");

	    var m = 3; // MAX TRIES

	    for (int k = 1; k <= m; k += 1)
	    {
		var stri = GetCaptcha();

		Tool.TranslateColors($"&c| &fTry to type this one out: &e{stri}\r\n");
		Tool.TranslateColors($"&c[&fSolve Me &8(&7{k}&f/&7{m}&8)&c]: &b");

		var resp = Console.ReadLine();

		if (resp != stri)
		{
		    if (k == m)
		    {
			Tool.TranslateColors("&8(&c-&8) &fYou have exceeded the maximum amount of solve attempts.\r\n");
			Tool.TranslateColors("&8(&c-&8) &fPress any key to exit. ");

			Console.ReadKey();

			Environment.Exit(-1);
		    };
		    
		    continue;
		};

		break;
	    };

	    Tool.TranslateColors("&8(&a+&8) &fYou have passed the test, good job!\r\n");
	    Tool.TranslateColors("&8(&a+&8) &fPlease wait three seconds until I redirect you ....");

	    Thread.Sleep(3000);
	}
    }
}
