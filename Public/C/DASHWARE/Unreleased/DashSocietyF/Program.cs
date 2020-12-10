using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using System.Net.Mail;

namespace DashSocietyF
{   
    public class Program
    {
	static public void Main(string[] args)
	{
	    Console.Title = "( Dash Society Framework 1.0 )";

	    DashAuth.Show();
	    StartScreen.Show();
	    Terminal.Show();

	    Console.ReadKey();
	}
    }
}
