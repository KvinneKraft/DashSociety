
// Author: Dashie
// Version: 1.0

using System;

namespace DashCore
{
    public static class dush
    {
	public static void say(string message)
	{
	    Console.Write(message);
	}

	public static void halt()
	{
	    Console.ReadKey();
	}
    };
};