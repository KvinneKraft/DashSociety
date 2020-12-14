using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Security.Principal;
using System.Resources;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    class Program
    {
        public static Optical optic = null;
        public static Optical.Utilities optic_util = null;
        public static Optical.Information optic_info = null;

        public static Networking.UDP udp_library = null;
        public static Networking.TCP tcp_library = null;
        public static Networking.SSH ssh_library = null;
        public static Networking.FTP ftp_library = null;

        public static Networking.HTTP http_library = null;
        public static Networking.HTTP.HttpStresser http_stresser_library = null;

        public static Networking.SMTP smtp_library = null;

        public static Misc.Exploit exploit_library = null;
        public static Misc.Payload payload_library = null;
        public static Misc.Utility utility_library = null;



        public static readonly String[] Udp_Options = { "Dash Flood", "Normal Flood", "Intensive Flood", "About this Option" };
        public static readonly String[] Tcp_Options = { "Connectivity Flood", "TCP Port Scanner", "About this Option" };
        public static readonly String[] Ssh_Options = { "Auth Dixy Attack", "Auth Brute-Dash Attack", "About this Option" };
        public static readonly String[] Ftp_Options = { "Auth Dixy Attack", "Auth Brute-Dash Attack", "About this Option" };
        public static readonly String[] Http_Options = { "Form Dixy Attack", "Form Brute-Dash Attack", "Server Stresser", "Header Manager", "About this Option" };
        public static readonly String[] Smtp_Options = { "Gmail Bomber", "Outlook Bomber", "About this Option" };
        public static readonly String[] Exploit_Options = { "SMS Bomber", "Bloatware Manager", "File Downloader", "About this Option" };
        public static readonly String[] Payload_Options = { "Payload Generator", "About this Option" };
        public static readonly String[] Utility_Options = { "DNS Changer", "Proxy Manager", "User Manager", "Browser Manager", "About this Option" };

        public static readonly String[] Access_Options = { "udp", "tcp", "ssh", "ftp", "http", "smtp", "payload", "exploit", "utility" };
        public static readonly List<String[]> Categories = new List<String[]>() { Udp_Options, Tcp_Options, Ssh_Options, Ftp_Options, Http_Options, Smtp_Options, Exploit_Options, Payload_Options, Utility_Options };

        private static int GetModuleCount()
        { int result = 0;  foreach (String[] Array in Categories) { result += Array.Length-1; } return result; }

        public static readonly int Libraries = 13;
        public static readonly int Modules = (GetModuleCount());

        public static readonly string AppFullName = "DASH_NET_API_MODULE";
        private static bool ClearConsole = false, LoadMenu = true;



        private static void InitMsg(String msg, String prefix, ConsoleColor restorecolour, ConsoleColor msgcolour, ConsoleColor prefixcolour)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("[");

            Console.ForegroundColor = prefixcolour;
            Console.Write($"{prefix}");

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write(":/> ");

            Console.ForegroundColor = msgcolour;
            Console.Write($"{msg}");

            Console.ForegroundColor = restorecolour;
        }



        private static void InitializeConsole()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.Title = ("All Rights Reserved (c) Dashies Software Inc");

            String[] users = { "dashie", "grace" };
            String[] pswds = { "dashie", "grace" };

            ConsoleColor a = ConsoleColor.DarkGray, b = ConsoleColor.DarkCyan, c = ConsoleColor.Gray,
                         d = ConsoleColor.Red, e = ConsoleColor.DarkGreen, f = ConsoleColor.White;

            username:
            InitMsg($"Username(User): ", "dash login", b, f, f);
            string uname = Console.ReadLine();

            InitMsg($"scanning our DataBase(s) for the username \"{uname}\" ....\r\n", "dash login", a, e, f);
            foreach(string usr in users)
            {
                if((usr == users[users.Length - 1]) && (usr != uname))
                {
                    InitMsg($"the username \"{uname}\" has not been found in our DataBase :c\r\n", "dash login", a, d, f);
                    goto username;
                }

                else
                if(usr == uname)
                {
                    InitMsg($"the username \"{uname}\" has been found in our DataBase c:\r\n", "dash login", a, e, f);
                    break;
                }
            }

            password:
            InitMsg($"Password({uname}): ", "dash login", b, f, f);
            string pword = Console.ReadLine();

            InitMsg($"scanning our DataBase(s) for the password \"{pword}\" for the user \"{uname}\" ....\r\n", "dash login", a, e, f);
            foreach (string pwd in pswds)
            {
                if ((pwd == pswds[users.Length - 1]) && (pwd != uname))
                {
                    InitMsg($"the password \"{pword}\" has not been found in our DataBase :c\r\n", "dash login", a, d, f);
                    goto password;
                }

                else
                if (pwd == pword)
                {
                    InitMsg($"the password \"{pword}\" has been found in our DataBase c:\r\n", "dash login", a, e, f);
                    break;
                }
            }

            InitMsg($"Logging in as {uname} ....", "dash login", a, f, f);

            System.Threading.Thread.Sleep(5000);
            Console.Clear();


            InitMsg($"Currently initializing {Libraries} Dash Libraries ....\r\n", "dashcore", f, f, d);
            
            using (var identity = WindowsIdentity.GetCurrent())
            {
                var principal = new WindowsPrincipal(identity);

                if(!principal.IsInRole(WindowsBuiltInRole.Administrator))
                {
                    InitMsg($"It seems like you are not running this application as an administrator, This can cause issues!\r\n", "dashcore", ConsoleColor.White, ConsoleColor.Red, ConsoleColor.Red);
                    InitMsg($"Do you still wish to proceed even though this is unrecommended [Y/n]? ", "dashcore", ConsoleColor.White, ConsoleColor.Red, ConsoleColor.Red);

                    String Decision = Console.ReadKey().Key.ToString().ToLower();
                    Console.WriteLine();

                    if(Decision != "y")
                    {
                        Environment.Exit(666);
                    }
                }
            }

            udp_library = new Networking.UDP();
            tcp_library = new Networking.TCP();
            ssh_library = new Networking.SSH();
            ftp_library = new Networking.FTP();

            http_library = new Networking.HTTP();
            http_stresser_library = new Networking.HTTP.HttpStresser();

            smtp_library = new Networking.SMTP();

            exploit_library = new Misc.Exploit();
            payload_library = new Misc.Payload();
            utility_library = new Misc.Utility();

            optic = new Optical();
            optic_util = new Optical.Utilities();
            optic_info = new Optical.Information();

            InitMsg($"Successfully initialized all {Libraries} Dash Libraries!", "dashcore", ConsoleColor.White, ConsoleColor.White, ConsoleColor.Red);
            System.Threading.Thread.Sleep(2000);

            Console.Clear();
        }



        private static String IndexTypes()
        {
            return
                (
                    $"Options : \r\n" + 
                    $"\r\n  -=> udp       (has {Udp_Options.Length} modules)\r\n" +
                    $"  -=> tcp       (has {Tcp_Options.Length} modules)\r\n" +
                    $"  -=> ssh       (has {Ssh_Options.Length} modules)\r\n" +
                    $"  -=> ftp       (has {Ftp_Options.Length} modules)\r\n" +
                    $"  -=> http      (has {Http_Options.Length} modules)\r\n" +
                    $"  -=> smtp      (has {Smtp_Options.Length} modules)\r\n" +
                    $"  -=> payload   (has {Payload_Options.Length} modules)\r\n" +
                    $"  -=> exploit   (has {Exploit_Options.Length} modules)\r\n" +
                    $"  -=> utility   (has {Utility_Options.Length} modules)\r\n\r\n" +

                    $"Total Loaded Libraries: {Libraries}\r\n" +
                    $"Total Loaded Modules..: {Modules}\r\n\r\n" +
                    
                    $"All Rights Reserved (c) Dashies Software 2019\r\n\r\n" +

                    "----------------------------------------------------------\r\n\r\n" 
                );
        }

 

        private static void InitType(String[] Array, String Type)
        {
            for(int index = 0, id = 1; index <= Array.Length - 1; index += 1, id += 1)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{Type} {id})");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($" {Array[index]}\r\n");
            }
        }



        static void Main(string[] args)
        {
            InitializeConsole();

            while (true)
            {
                if (LoadMenu == true)
                {
                    LoadMenu = false;

                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.DarkGray;

                    InitMsg($"{IndexTypes()}", "dashcore", ConsoleColor.White, ConsoleColor.Yellow, ConsoleColor.Red);
                }

                else
                if (ClearConsole == true)
                {
                    ClearConsole = false;
                    Console.Clear();
                }

                optic_util.function_module("select");

                Console.ForegroundColor = ConsoleColor.Magenta;
                String Q = Console.ReadLine().ToLower();

                Console.ForegroundColor = ConsoleColor.White;

                if (Q != String.Empty)
                {
                    if (Q == "udp") { InitType(Udp_Options, Q); }
                    else
                    if (Q == "tcp") { InitType(Tcp_Options, Q); }
                    else
                    if (Q == "ssh") { InitType(Ssh_Options, Q); }
                    else
                    if (Q == "ftp") { InitType(Ftp_Options, Q); }
                    else
                    if (Q == "http") { InitType(Http_Options, Q); }
                    else
                    if (Q == "smtp") { InitType(Smtp_Options, Q); }
                    else
                    if (Q == "payload") { InitType(Payload_Options, Q); }
                    else
                    if (Q == "exploit") { InitType(Exploit_Options, Q); }
                    else
                    if (Q == "utility") { InitType(Utility_Options, Q); }

                    else
                    if (Q == "!clear") { ClearConsole = true; }
                    else
                    if (Q == "!reload") { LoadMenu = true; }
                    else
                    if (Q == "!bye")
                    {
                        InitMsg($"Please confirm that you are aware of exiting {AppFullName}, Are you sure [Y/n]? ", "dashcore", ConsoleColor.White, ConsoleColor.DarkRed, ConsoleColor.DarkRed);

                        String Key = Console.ReadKey().Key.ToString().ToLower();
                        Console.WriteLine();

                        if (Key == "y") Environment.Exit(666);
                    }

                    else
                    if ((Q == "!restart") || (Q == "reboot"))
                    {
                        InitMsg($"Please confirm that you are aware of restarting {AppFullName}, Are you sure [Y/n]? ", "dashcore", ConsoleColor.White, ConsoleColor.DarkRed, ConsoleColor.DarkRed);

                        String Key = Console.ReadKey().Key.ToString().ToLower();
                        Console.WriteLine();

                        if (Key == "y")
                        {
                            InitMsg($"Restarting {AppFullName}, Please wait ....", "dashCore", ConsoleColor.White, ConsoleColor.DarkRed, ConsoleColor.DarkRed);

                            System.Diagnostics.ProcessStartInfo proc_info = new System.Diagnostics.ProcessStartInfo()
                            {
                                FileName = Assembly.GetExecutingAssembly().Location.ToString(),
                                UseShellExecute = true
                            };

                            System.Diagnostics.Process proc = new System.Diagnostics.Process() { StartInfo = proc_info };
                            System.Threading.Thread.Sleep(3000);

                            proc.Start();
                            Environment.Exit(0);
                        }
                    }

                    else
                    if (Q == $"udp {Udp_Options.Length}") { optic_info.information_dictionary(1); }

                    else
                    if (Q == $"tcp {Tcp_Options.Length}") { optic_info.information_dictionary(2); }

                    else
                    if (Q == $"ssh {Ssh_Options.Length}") { optic_info.information_dictionary(3); }

                    else
                    if (Q == $"ftp {Ftp_Options.Length}") { optic_info.information_dictionary(4); }

                    else
                    if (Q == $"http {Http_Options.Length}") { optic_info.information_dictionary(5); }

                    else
                    if (Q == $"smtp {Smtp_Options.Length}") { optic_info.information_dictionary(6); }

                    else
                    if (Q == $"payload {Payload_Options.Length}") { optic_info.information_dictionary(7); }

                    else
                    if (Q == $"exploit {Exploit_Options.Length}") { optic_info.information_dictionary(8); }

                    else
                    if (Q == $"utility {Utility_Options.Length}") { optic_info.information_dictionary(9); }

                    else
                    if (Q == $"tcp 2")
                    {
                        if(tcp_library.TcpPortScanner() != true)
                        {

                        }

                        else
                        {

                        }
                    }

                    else
                    if (Q == "smtp 1")
                    {
                        if (smtp_library.GmailSpammer() != true)
                        {

                        }

                        else
                        {

                        }
                    }

                    else
                    if (Q == "smtp 2")
                    {
                        if (smtp_library.OutlookSpammer() != true)
                        {

                        }

                        else
                        {

                        }
                    }

                    else
                    if (Q == "tcp 1")
                    {
                        if (tcp_library.ConnectivityFlooder() != true)
                        {

                        }

                        else
                        {

                        }
                    }

                    else
                    if (Q == "udp 1")
                    {
                        if (udp_library.DashLoad() != true)
                        {

                        }

                        else
                        {

                        }
                    }

                    else
                    if (Q == "udp 2")
                    {
                        if (udp_library.NormalFlood() != true)
                        {

                        }

                        else
                        {

                        }
                    }

                    else
                    if (Q == "udp 3")
                    {
                        if (udp_library.IntensiveFlood() != true)
                        {

                        }

                        else
                        {

                        }
                    }

                    else
                    if (Q == "http 3")
                    {
                        if (http_stresser_library.ServerStresser() != true)
                        {
                            //function_error();
                        }

                        else
                        {

                        }
                    }

                    else
                    {
                        if (Q != " ")
                        {
                            InitMsg($"The argument \'{Q}\' was not recognized :c\r\n", "error", ConsoleColor.White, ConsoleColor.Red, ConsoleColor.DarkRed);
                        }
                    }
                }
            }
        }
    }
}
