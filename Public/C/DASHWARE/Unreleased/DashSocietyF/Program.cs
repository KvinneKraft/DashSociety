using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DashSocietyF
{   
    public class Program
    {
	static public void Main(string[] args)
	{
	    Console.Title = "( Dash Society Framework 1.0 )";

	    StartScreen.Show();
	    TerminalPak.Show();

	    Console.ReadKey();
	}
    }
}
