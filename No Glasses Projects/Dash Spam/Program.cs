using System;


namespace Dash_Spam
{
    class Program
    {
        static ConsoleColor CHOICE = ConsoleColor.Yellow;
        static ConsoleColor ERROR = ConsoleColor.Red;
        static ConsoleColor INFO = ConsoleColor.DarkCyan;
        static ConsoleColor LINE = ConsoleColor.Cyan;


        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.Clear();

            write("-[-=-=-=-=-=-=-=-=-====-=-=-=-=-=]-\r\n", LINE);
            write(" |                               | \r\n", INFO);
            write(" |  (1) Use the dash spammer.    | \r\n", INFO);
            write(" |  (2) About the dash spammer.  | \r\n", INFO);
            write(" |  (3) quit this runtime.       | \r\n", INFO);
            write(" |                               | \r\n", INFO);
            write("-[-=-=-=-=-=-=-=-=-====-=-=-=-=-=]-\r\n\r\n", LINE);

        retype:

            string command = input("Option").ToLower();

            if(command.Equals("1"))
            {
                string username, password, target, body;
                int threads, mails;

                username = input("Username");
                password = input("Password");
                target = input("Target");

                DashSMTP smtp = new DashSMTP();

            }

            else if(command.Equals("2"))
            {

            }

            else if(command.Equals("3"))
            {

            }

            else
            {
                goto retype;
            };
        }


        static string input(string str)
        {
            ConsoleColor old_bgcolor = Console.BackgroundColor;
            ConsoleColor old_frcolor = Console.ForegroundColor;

            // (/DashSociety/Spammer/Option):>  

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("(");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write($"/DashSociety/Spammer/{str}");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("):> ");

            Console.ForegroundColor = ConsoleColor.Red;

            string cache = Console.ReadLine();

            Console.BackgroundColor = old_bgcolor;
            Console.ForegroundColor = old_frcolor;

            return cache;
        }

        static void write(string str, ConsoleColor color)
        {
            ConsoleColor old_bgcolor = Console.BackgroundColor;
            ConsoleColor old_frcolor = Console.ForegroundColor;

            Console.ForegroundColor = color;
            Console.Write(str);

            Console.BackgroundColor = old_bgcolor;
            Console.ForegroundColor = old_frcolor;
        }
    };
};
 