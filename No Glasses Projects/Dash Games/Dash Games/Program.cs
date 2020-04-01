
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
	public static void Main(string[] args)
	{

	}

	private const readonly int TYPE_INFORMATION = 0;
	private static readonly int TYPE_DECISION = 1;
	private static readonly int TYPE_WARNING = 2;
	private static readonly int TYPE_ERROR = 3;

	private static void print(int type, string str)
	{
	    ConsoleColor fc = ConsoleColor.White;

	    switch ( type )
	    {
		case TYPE_ERROR:
		{
		    break;
		};
	    };

	    Console.ForegroundColor = fc;
	}
    };
};

/*
    Coding this while stoned, feels guud. 
*/