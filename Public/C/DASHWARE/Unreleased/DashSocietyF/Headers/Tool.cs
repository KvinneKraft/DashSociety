using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DashSocietyF
{
    public class Tool
    {
	static public string CenterText(string data, string length)
	{
	    var spaces = string.Empty;

	    for (var k = 0; k < (length.Length - data.Length) / 2; k += 1)
	    {
		spaces += " ";
	    }

	    return (spaces + data + spaces);
	}

	static public void TranslateColors(string m, char d = '&', bool r = false)
	{
	    var t = Console.ForegroundColor;
	    var a = m.Split('&');

	    foreach (var l in a)
	    {
		if (l.Length < 1)
		    continue;

		var c = ConsoleColor.White;

		switch (l[0])
		{
		    case 'a':
			c = ConsoleColor.Green;
			break;
		    case 'b':
			c = ConsoleColor.Cyan;
			break;
		    case 'c':
			c = ConsoleColor.Red;
			break;
		    case 'd':
			c = ConsoleColor.Magenta;
			break;
		    case 'e':
			c = ConsoleColor.Yellow;
			break;
		    case '6':
			c = ConsoleColor.DarkYellow;
			break;
		    case '3':
			c = ConsoleColor.DarkCyan;
			break;
		    case '4':
			c = ConsoleColor.DarkRed;
			break;
		    case '7':
			c = ConsoleColor.Gray;
			break;
		    case '8':
			c = ConsoleColor.DarkGray;
			break;
		    case 'f':
			c = ConsoleColor.White;
			break;
		};

		Console.ForegroundColor = c;
		Console.Write(l.Remove(0, 1));
	    };

	    if (r)
	    {
		Console.ForegroundColor = t;
	    };
	}
    }
}
