using System;

namespace DashSocietyF
{
    public class Program
    {
	static public void Main(string[] args)
	{
	    Mailer.Show();//Implement command
	    Console.ReadLine();

	    Console.Title = "( Dash Society Framework 1.0 )";

	    DashAuth.Show();
	    StartScreen.Show();
	    Terminal.Show();

	    Console.ReadKey();
	}
    }
}
