
//Author: Dashie
//Version: 1.0

using System;
using System.Net;
using System.Net.Sockets;

namespace Dashies_Gate
{
    public static class Helper
    {
        /* ---[METHOD TO CHECK IF TARGET IS REACHABLE]--- */
        public static bool isReachable(string ipv4, int port)
        {
            bool status = false;

            try
            {
                Socket long_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                IAsyncResult connection_result = long_sock.BeginConnect(IPAddress.Parse(ipv4), (port), null, null);
                bool connection_success = connection_result.AsyncWaitHandle.WaitOne(200, true);

                if (long_sock.Connected)
                {
                    status = true;
                };

                if (!long_sock.Connected)
                {
                    status = false;
                };

                long_sock.Close();
            }

            catch
            { status = false; };

            return status;
        }

        /* ---[METHOD TO GRAB THE LOCAL IPv4 ADDRESS]--- */
        public static string GetLocalIP()
        {
            Program.local_address = ($"null");

            var hostname = Dns.GetHostName();
            var host_addresses = Dns.GetHostEntry(hostname);

            foreach (var host_address in host_addresses.AddressList)
            {
                if (host_address.AddressFamily == AddressFamily.InterNetwork)
                {
                    Program.local_address = host_address.ToString();
                };
            };

            return Program.local_address;
        }
    }
}
