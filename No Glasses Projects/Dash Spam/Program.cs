using System;


namespace Dash_Spam
{
    class Program
    {
        static void Main(string[] args)
        {
            string username, password, target, body;
            int threads, mails;



            Console.ReadKey();
        }

        static string input(string str)
        {
            ConsoleColor old_bgcolor = Console.BackgroundColor;
            ConsoleColor old_frcolor = Console.ForegroundColor;

            // Colors and Stuff

            string cache = Console.ReadLine();

            Console.BackgroundColor = old_bgcolor;
            Console.ForegroundColor = old_frcolor;

            return cache;
        }

        static void write(string str)
        {
            ConsoleColor old_bgcolor = Console.BackgroundColor;
            ConsoleColor old_frcolor = Console.ForegroundColor;

            // Colors and Stuff

            Console.BackgroundColor = old_bgcolor;
            Console.ForegroundColor = old_frcolor;
        }
    };
};
 