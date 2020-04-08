
// Author: Dashie
// Version: 1.0

/*
    This project is for my students
    
    -Dashie
*/

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Dash_Games
{
    /// 
    //
    // - Guess the word
    // - Guess the number 
    // - Finish the sentence
    // - Dash Quiz
    // - Information
    // - Colour Library thing
    //
    ///

    public class Program
    {
	private static readonly List<string> game_menu = new List<string>()
	{
	    "Dashies Snake    |",
	    "Dashies Pacman   |",
	    "Dashies Tetris   |",
	    "Guess The Number |",
	    "Guess The Number |",
	    "Guess Quiz       |",
	};

	private static readonly List<string> options = new List<string>();

	public static void Main(string[] args)
	{
	    for ( int i = 1; i <= 6; i += 1 )
	    {
		options.Add(i.ToString());
	    };

	    Console.WriteLine("\r\n[-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-]");

	    for ( int i = 1; i <= game_menu.Count; i += 1 )
	    {
		Console.WriteLine($" | ({i.ToString()})  -==>  {game_menu[i-1]}");
	    };

	    Console.WriteLine("[-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-]\r\n");

	retype:
	    Console.Write("(Option> ");
	    string option = Console.ReadLine();

	    if ( options.Contains(option) )
	    {
		if ( option.Equals("1") )
		{

		}

		else if ( option.Equals("2") )
		{

		}

		else if ( option.Equals("3") )
		{

		}

		else if ( option.Equals("4") )
		{

		}

		else if ( option.Equals("5") )
		{

		}

		else if ( option.Equals("6") )
		{

		};
	    }

	    else
	    {
		goto retype;
	    };
	}

	private const int TYPE_INFORMATION = 0;
	private const int TYPE_DECISION = 1;
	private const int TYPE_WARNING = 2;
	private const int TYPE_ERROR = 3;

	private static void print(int type, string str)
	{
	    ConsoleColor fc = ConsoleColor.White;
	    string px = "";

	    switch ( type )
	    {
		case TYPE_WARNING:
		{
		    fc = ConsoleColor.Yellow;
		    px = "<!>";

		    break;
		};

		case TYPE_DECISION:
		{
		    fc = ConsoleColor.Gray;
		    px = "<?>";

		    break;
		};

		case TYPE_INFORMATION:
		{
		    fc = ConsoleColor.Green;
		    px = "<~>";

		    break;
		};

		case TYPE_ERROR:
		{
		    fc = ConsoleColor.Red;
		    px = "<#>";

		    break;
		};
	    };

	    Console.ForegroundColor = fc;
	    Console.WriteLine($"{px} {str}");
	}
    };
};

/*
    Coding this while stoned, feels guud. 
*/