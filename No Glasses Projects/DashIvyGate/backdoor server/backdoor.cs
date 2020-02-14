
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Dashies_Gate
{
    public static class Program
    {
        public static string local_address = Helper.GetLocalIP();
        private static int accessible_port = 25565;

        [STAThread]
        static void Main(string[] args)
        {
            // Hide

            if(local_address == "null")
            {
                Environment.Exit(10);
            };

            try
            {
                Socket command_listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint local_endpoint = new IPEndPoint(IPAddress.Parse(local_address), accessible_port);

                command_listener.Bind(local_endpoint);// Issue is here?
                command_listener.Listen(10);

                while (true)
                {
                    try
                    {
                        Socket gateway_socket = command_listener.Accept();

                        byte[] bytes = new byte[byte.MaxValue];
                        string received_data = string.Empty;

                        received_data = Encoding.ASCII.GetString(bytes, 0, gateway_socket.Receive(bytes));

                        if (received_data.Length > 0)
                        {
                            if (received_data.ToLower() == "close gate!")
                            {
                                throw new Exception("FORCEFUL TERMINATION!");
                            };

                            if((received_data.ToLower().Contains("get:")) || (received_data.ToLower().Contains("post:")))
                            {
                                List<string> arguments_cache = new List<string>();

                                arguments_cache.AddRange(received_data.Split(':'));

                                if (arguments_cache.Count >= 2)
                                {
                                    if (arguments_cache[0].ToLower() == "get")
                                    {
                                        if (!File.Exists(arguments_cache[1]))
                                        {
                                            gateway_socket.Send(Encoding.ASCII.GetBytes($"The by you provided file does not exist!"));
                                        }

                                        else
                                        {
                                            gateway_socket.SendFile(arguments_cache[1], null, null, TransmitFileOptions.ReuseSocket);
                                        };
                                    }

                                    else
                                    {
                                        gateway_socket.Send(Encoding.ASCII.GetBytes($"An invalid option has been specified!"));
                                    };
                                }

                                else
                                {
                                    gateway_socket.Send(Encoding.ASCII.GetBytes($"It seems like you are using an incomplete command?"));
                                };

                                continue;
                            };

                            /*
                            byte[] send_back = System(received_data);

                            if(send_back.Length < 1)
                            {
                                send_back = Encoding.ASCII.GetBytes("[ERROR]: The command has failed!");
                            };

                            gateway_socket.Send(send_back);
                            */
                        };
                    }

                    catch (SocketException e)
                    {
                        if (e.ErrorCode != 10054)
                        {
                            Environment.Exit(9);
                        };
                    };
                };
            }

            catch
            {
                Environment.Exit(8);
            };
        }
    }
};