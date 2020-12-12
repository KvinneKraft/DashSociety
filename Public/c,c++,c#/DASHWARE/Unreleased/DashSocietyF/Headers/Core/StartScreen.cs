using System;
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
		 "&3[########################################]",
		$"&3|                                        &3|",
		$"&3|      &f(Dash Society Framework 1.0)      &3|",
		$"&3|                                        &3|",
		$"&3|     &fType '!help' for help mah dude     &3|",
		$"&3|                                        &3|",
		 "&3[########################################]"
	    };

	    foreach (var m in message)
	    {
		Tool.TranslateColors($" {m}\r\n");
	    };
	}
    }
}
