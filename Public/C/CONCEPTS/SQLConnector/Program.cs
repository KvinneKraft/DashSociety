using System;
using System.Data.SqlClient;

namespace SQLConnector
{
    class Program
    {
	static void Main(string[] args)
	{
	    while (true)
	    {
		Console.Write("Server Name> ");
		var SVName = Console.ReadLine();
		Console.Write("Database Name> ");
		var DBName = Console.ReadLine();
		Console.Write("Username> ");
		var UserID = Console.ReadLine();
		Console.Write("Password> ");
		var Passwd = Console.ReadLine();

		using (var connection = new SqlConnection($"Data Source={SVName}, 3306;Initial Catalog={DBName};User ID={UserID};Password={Passwd};"))
		{
		    try
		    {
			connection.Open();

			while (true)
			{
			    Console.Write("Sql Code> ");
			    var Sql = Console.ReadLine();

			    using (var command = new SqlCommand($"{Sql}", connection))
			    {
				command.ExecuteNonQuery();
			    };

			    Console.WriteLine("Code has been executed!");
			};
		    }

		    catch (Exception e)
		    {
			Console.WriteLine(e.Message);
		    };
		};
	    };
	}
    };
};
