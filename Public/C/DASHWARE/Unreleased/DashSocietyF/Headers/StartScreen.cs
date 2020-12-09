using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DashSocietyF
{
    public class StartScreen
    {
	static public void Show(string Len = "")
	{
	    //Console.SetWindowSize(42, 10);

	    for (int k = 0; k < "%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%".Length; k += 1) Len += " ";

	    Console.ForegroundColor = ConsoleColor.DarkGray;
	    Console.BackgroundColor = ConsoleColor.Black;

	    Console.Clear();

	    var message = new List<string>()
	    {
		 "&c%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%",
		$"&c%                                        &c%",
		$"&c% &f( Dash Society Framework 1.0 ) &c%",
		$"&c%                                        &c%",
		$"&c% &fType '!help' for help mah dude. &c%",
		$"&c%                                        &c%",
		 "&c%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%\r\n"
	    };

	    foreach (var m in message)
	    {
		Tool.TranslateColors($" {m}\r\n");
	    };
	}
    }
}
