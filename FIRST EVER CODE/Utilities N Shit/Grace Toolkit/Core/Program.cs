
/* 

Main Class for the Dash Gracerium Package Toolkit thing.

I wuv you gurl <3
 
*/

using System;
using System.Threading;
using System.Reflection;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    class Program
    {
        private static String Command = String.Empty;
        private static String[] Cmd;

        private static string ZeHeader()
        {
            return
                (
                    "[#####################################################]\r\n" +
                    "|                                                     |\r\n" +
                    "|    Dashies Software 2019 (c) All Rights Reserved    |\r\n" +
                    "|                                                     |\r\n" +
                    "|                                                     |\r\n" +
                    "| for and extended description of help type \'halp\' or |\r\n" +
                    "| or try to type random shit, this thing comes with   |\r\n" +
                    "| a lot of amazing features and bits of advanced      |\r\n" +
                    "| and but simple functionality.                       |\r\n" +
                    "|                                                     |\r\n" +
                    "| and all of those things for a shitty operating      |\r\n" +
                    "| system like windows, like oh my god :rofl:          |\r\n" +
                    "|                                                     |\r\n" +
                    "| ugh, and know that when you abuse this application  |\r\n" +
                    "| I will not be held responsible for your actions.    |\r\n" +
                    "|                                                     |\r\n" +
                    "| this application has been made for educational      |\r\n" +
                    "| purposes only, so yeh, educate yourself, ahaha.     |\r\n" +
                    "|                                                     |\r\n" +
                    "[#####################################################]\r\n"
                );
        }

        private static void Error(string message, ConsoleColor color)
        { Console.ForegroundColor = ConsoleColor.Red; Console.Write(message); Console.ForegroundColor = color; }

        public static void InitializeDashCore()
        { Console.Clear(); Console.WriteLine(ZeHeader()); Console.BackgroundColor = ConsoleColor.Black; Console.Title = ("(c) All Rights Reserved, Dashies Software Inc."); }

        public static void ReadCommand()
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("!:/");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("noot");

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("@");

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("lunarish");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write("> ");

            Command = Console.ReadLine();
            Cmd = Command.Split(' ');
        }

        static void Main(string[] args)
        {
            InitializeDashCore();


            for(;;)
            {
                ReadCommand();

                if (Cmd.Length > 2)
                {
                    if (Cmd[0] == "library")
                    {
                        if ((Cmd[1] == "--grace") && (Cmd[2] != " "))
                        {
                            if (Cmd[2] != String.Empty)
                            {
                                int[] Chapters = { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                                bool Bypass = false;
                                int Validate;

                                foreach (int Chapter in Chapters)
                                {
                                    if (Int32.TryParse(Cmd[2], out Validate) == true)
                                    {
                                        if (Validate == Chapter)
                                        {
                                            ProcessStartInfo s_inf = new ProcessStartInfo()
                                            {
                                                FileName = "Modules\\Book Library\\library.exe",
                                                WorkingDirectory = "Modules\\Book Library\\",
                                                Arguments = $"--view --chapter {Chapter}",

                                                RedirectStandardError = true,
                                                RedirectStandardInput = true,
                                                RedirectStandardOutput = true,

                                                UseShellExecute = false
                                            };

                                            Process Library = new Process() { StartInfo = s_inf };

                                            Library.Start();

                                            Console.WriteLine(Library.StandardOutput.ReadToEnd());

                                            Library.WaitForExit();

                                            Bypass = true;
                                            break;
                                        }
                                    }

                                    else
                                    {
                                        Error($"[:/Noot> Unable to parse the given integer value \"{Cmd[2]}\" :c\r\n", ConsoleColor.Gray);
                                        Bypass = true;
                                        break;
                                    }
                                }

                                if (Bypass == false)
                                {
                                    Error($"[:/Noot> Unable to find the specified Chapter \"{Cmd[2]}\" :c\r\n", ConsoleColor.Gray);
                                }
                            }
                        }

                        else
                        {
                            Error($"[:/Noot> We could not find \"{Cmd[1]}\" in our Library :c\r\n", ConsoleColor.Gray);
                        }
                    }

                    else
                    {
                        Error($"[:/Noot> \"{Cmd[0]}\" is not known as a valid command :c", ConsoleColor.Gray);
                    }
                }

                else
                if ((Cmd.Length == 1) && (Cmd[0] == "library")) Error("[:/Noot> You must specify the book you want to read >.<", ConsoleColor.Gray);

                else
                if (Cmd.Length == 1)
                {
                    if (Cmd[0] == "net_api")
                    {
                        try
                        {
                            ProcessStartInfo inf = new ProcessStartInfo() { UseShellExecute = true, FileName = "Modules\\Networking\\client.exe" };
                            Process execution = new Process() { StartInfo = inf };

                            execution.Start();
                        }

                        catch (Exception e)
                        {
                            Console.WriteLine(e.ToString());
                        }
                    }

                    if (Cmd[0] == "win-cmd")
                    {
                        if (System.IO.File.Exists("C:\\Windows\\System32\\cmd.exe") == true)
                        {
                            ProcessStartInfo proc_inf = new ProcessStartInfo()
                            {
                                FileName = "C:\\Windows\\System32\\cmd.exe",
                                UseShellExecute = true,
                                WorkingDirectory = "C:\\"
                            };

                            Process proc = new Process() { StartInfo = proc_inf };

                            proc.Start();
                        }

                        else
                        {
                            Error($"[:/Noot> Ugh, \"C:\\Windows\\System32\\cmd.exe\" could not be found :c\r\n", ConsoleColor.Gray);
                        }
                    }

                    else
                    if (Cmd[0] == "win-ps")
                    {
                        if (System.IO.File.Exists("C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe") == true)
                        {
                            ProcessStartInfo proc_inf = new ProcessStartInfo()
                            {
                                FileName = "C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe",
                                UseShellExecute = true,
                                WorkingDirectory = "C:\\"
                            };

                            Process proc = new Process() { StartInfo = proc_inf };

                            proc.Start();
                        }
                    }

                    else
                    {
                        Error($"[:/Noot> Aw \"{Cmd[0]}\" is not recognized as a valid command :c\r\n", ConsoleColor.Gray);
                    }
                }
               
                else Error($"[:/Noot> Invalid argument received :c", ConsoleColor.Gray);
            }
        }
    }
}
