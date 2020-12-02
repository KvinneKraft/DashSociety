
// Author: Dashie
// Version: 1.0

using System;

namespace MailSpammer
{
    public class Program
    {
        readonly static ConsoleColor DarkGray = ConsoleColor.DarkGray;
        readonly static ConsoleColor Gray = ConsoleColor.Gray;
        readonly static ConsoleColor White = ConsoleColor.White;
        readonly static ConsoleColor Yellow = ConsoleColor.Yellow;
        readonly static ConsoleColor DarkYellow = ConsoleColor.DarkYellow;
        readonly static ConsoleColor Red = ConsoleColor.Red;


        [STAThread]
        public static void Main()
        {
            Dash.BackColor(ConsoleColor.Black);
            Dash.ForeColor(ConsoleColor.Yellow);

            Dash.Clear();
            Login.AuthenticateUser();

            Dash.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n\r\n", DarkYellow);

            // Some epic TEXT LOGO right here <<<

            Dash.WriteLine("-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n\r\n", DarkYellow);

            Dash.WriteLine(" (a)    -===>    Go and make use of this user-friendly application.\r\n", Yellow);
            Dash.WriteLine(" (b)    -===>    Contact me, Dashie, I has sum urls, yas!\r\n", Yellow);
            Dash.WriteLine(" (c)    -===>    Get some insight of this application.\r\n", Yellow);
            Dash.WriteLine(" (d)    -===>    Close down this application.\r\n", Yellow);

            Dash.WriteLine("\r\n-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n\r\n", DarkYellow);

        retype:

            string option = Dash.ReadLine("Option").ToLower();

            if (option.Equals("a"))
            {

            }

            else if (option.Equals("b"))
            {

            }

            else if (option.Equals("c"))
            {

            }

            else if (option.Equals("d"))
            {
                // Are you sure? Environment.Exit(-1);
            }

            else
            {
                goto retype;
            };
        }
    };
};
