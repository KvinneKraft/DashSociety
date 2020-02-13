
// Author: Dashie
// Version: 1.0


using System;


namespace KvinneKraft
{
    class Program
    {
        //To-Do:
        //
        // - estabilish connection with backdoor on server.

        private static void print(string str)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"{str}");
            Console.ForegroundColor = ConsoleColor.DarkRed;
        }

        static void Main()
        {
            Moony.print_banner();   

            for( ; ; )
            {
                string[] full_command = Moony.print_input().Split(' ');
                string c = full_command[0].ToLower();

                if((c.Equals("cls")) || (c.Equals("clear")))
                {
                    Console.Clear();
                    continue;
                }

                if(c.Equals("backdoor"))
                {
                    if(full_command.Length < 3)
                    {
                        print("[-]: Insufficient parameters received.");
                        print("[-]: Expected usage: backdoor (--connect, --disconnect, --info, --ping) <host | id>");
                        continue;
                    };

                    c = full_command[1].ToLower();

                    if(c.Equals("--connect"))
                    {

                    }

                    else if(c.Equals("--disconnect"))
                    {

                    }

                    else if(c.Equals("--info"))
                    {

                    }

                    else if(c.Equals("--ping"))
                    {

                    }

                    else
                    {
                        print("[-] An invalid parameter has specified.");
                        print("[-] Expected something like --connect, --disconnect, --info or --ping");
                    };

                    continue;
                };
            };
        }
    };

    class Moony
    {
        private static readonly String[] banner_data =
        {
            "",
            "",
            "",
        };


        public static void print_banner()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.BackgroundColor = ConsoleColor.Black;

            foreach(string str in banner_data)
            {
                Console.WriteLine();
            };

            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Blue;
        }


        public static String print_input()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("$");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("(");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("D");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("a");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("s");

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("h");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("I");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("v");

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("y");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(")");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("> ");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            return Console.ReadLine();
        }
    };
};
