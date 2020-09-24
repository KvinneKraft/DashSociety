using System;
using System.Windows.Forms;

namespace DashAuth
{
    public partial class DashAuth : Form
    {
	public DashAuth()
	{
	    Console.Write("Password: ");
	    string hash1 = Console.ReadLine();

	    Console.Write("\nUsername: ");
	    string hash2 = Console.ReadLine();

	    if ( hash1 == "mypassword" )
	    {
		if ( hash2 == "myusername" )
		{
		    Console.WriteLine($"\nusername: {hash2} | password: {hash1}");
		};
	    };

	    Console.ReadKey();
	}
    };
};
