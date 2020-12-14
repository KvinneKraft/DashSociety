
// Author: Dashie
// Version: 1.0

using System;

namespace MailSpammer
{
    public class Dash
    {
        readonly static ConsoleColor DarkGray = ConsoleColor.DarkGray;
        readonly static ConsoleColor Gray = ConsoleColor.Gray;
        readonly static ConsoleColor White = ConsoleColor.White;
        readonly static ConsoleColor Yellow = ConsoleColor.Yellow;
        readonly static ConsoleColor DarkYellow = ConsoleColor.DarkYellow;
        readonly static ConsoleColor Red = ConsoleColor.Red;

        public static void WriteLine(string str, ConsoleColor color)
        {
            ForeColor(color);
            Console.Write($"{str}");
        }

        public static string ReadLine(string str)
        {
            WriteLine("$[", DarkGray);
            WriteLine($"/DashSociety/{str}", Gray);
            WriteLine(")> ", DarkGray);

            ForeColor(Yellow);

            return Console.ReadLine();
        }

        public static void BackColor(ConsoleColor color) 
        { 
            Console.BackgroundColor = color; 
        }

        public static void ForeColor(ConsoleColor color) 
        { 
            Console.ForegroundColor = color; 
        }

        public static void Clear() { Console.Clear(); }
    };
};