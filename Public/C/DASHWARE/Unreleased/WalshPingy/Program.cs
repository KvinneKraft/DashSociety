using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WalshPingy
{
    public class StartScreen
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

	static public void Show(string Len = "")
	{
	    //Console.SetWindowSize(33, 15);
	   
	    for (int k = 0; k < 29; k += 1) Len += " ";

	    Console.BackgroundColor = ConsoleColor.Black;
	    Console.ForegroundColor = ConsoleColor.DarkGray;
	    // He is using a cheap phone.  He can no read this.
	    // I am aboutto fuck his entire argument up man.
	    // Bet!
	    var Msg = (
		$"-:=============================:-\r\n" +
		$" |{Len}|\r\n" +
		$" |{CenterText("Walsh Pingy 1.0", Len)}|\r\n" +
		$" |{Len}|\r\n" +
		$" | -=========================- |\r\n" +
		$" |{Len}|\r\n" +
		$" |    Use  this application    |\r\n" +
		$" |  with caution, you are the  |\r\n" +
		$" | one that is responsible for |\r\n" +
		$" | the actions  that you take. |\r\n" +
		$" |{Len}|\r\n" +
		$"-:=============================:-\r\n\r\n"
	    );

	    Console.Clear();
	    Console.Write(Msg);
	}
    }
    
    public class TerminalPak
    {
	static void GetCommander()
	{
	    //$: 
	    Console.ForegroundColor = ConsoleColor.DarkCyan;
	    Console.Write("(");

	    Console.ForegroundColor = ConsoleColor.Gray;
	    Console.Write("/usr/home/DashSociety");

	    Console.ForegroundColor = ConsoleColor.DarkCyan;
	    Console.Write("): ");

	    Console.ForegroundColor = ConsoleColor.Red;
	}

	public class Handler
	{
	    public static void Helper()
	    {
		
	    }
	}

	enum TYPE
	{
	    ERROR, WARNING, SUCCESS, INFO
	}

	static void print(TYPE Type, string data)
	{
	    var colors = ConsoleColor.DarkYellow;
	    var prefix = "(!):";

	    switch (Type)
	    {
		case TYPE.ERROR:
		    colors = ConsoleColor.Red;
		    prefix = "(-):";
		break;

		case TYPE.SUCCESS:
		    colors = ConsoleColor.Green;
		    prefix = "(+):";
		break;

		case TYPE.INFO:
		    colors = ConsoleColor.Gray;
		    prefix = "(?):";
		break;
	    }

	    Console.ForegroundColor = colors;
	    Console.Write(prefix);

	    Console.ForegroundColor = ConsoleColor.White;
	    Console.WriteLine($" {data}");
	}

	static void ExecuCommand(string data)
	{
	    if (data.Length < 1)
	    {
		print(TYPE.ERROR, "You must type something man.");
		return;
	    };

	    var turd = data.Split(' ');

	    if (turd[0] == "!help")
	    {
		Handler.Helper();
		return;
	    };
	}

	public static void Show()
	{
	    while (true)
	    {
		GetCommander();

		var data = Console.ReadLine();

		ExecuCommand(data);
	    };
	}
    }

    public class Program
    {
	static public void Main(string[] args)
	{
	    StartScreen.Show();
	    TerminalPak.Show();

	    Console.ReadKey();
	}
    }
}
