
 /* (c) All Rights Reserved, Dashies Software Inc. */
 
 //
 // User Datagram Protocol Port Scanner Class for the Dashies Port Scanner 1.0
 //
  
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;

using System.Net.NetworkInformation;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Net.Sockets;
using System.Net.Http;
using System.Net;

using System.Resources;
using System.Threading;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace src 
{
	public partial class Scan_UDP
	{
        public bool keep_running = true;

		public Scan_UDP(String target_ipv4, String Host, Int32[] aPorts, int Type, int Scan_Timeout, TextBox Status)
        {
            System.Threading.Thread.Sleep(500);
            Status.AppendText("--------------------------------------------------------\r\n");

            if(Type == 0)
            {
                if (keep_running == false)
                {
                    Status.AppendText("+ the scan has been canceled by the user!\r\n");
                    return;
                }

                Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
                {
                    Blocking = true,
                    ReceiveTimeout = 150,
                    SendTimeout = 150,
                    SendBufferSize = 75
                };

                IAsyncResult result = client.BeginConnect(target_ipv4, aPorts[0], null, null);
                bool success = result.AsyncWaitHandle.WaitOne(500, true);

                if(client.Connected)
                {
                    Status.AppendText($"+ port {aPorts[0]} seems open!\r\n");

                    client.EndConnect(result);
                    client.Close();
                }

                else
                {
                    Status.AppendText($"- port {aPorts[0]} seems closed!\r\n");

                    client.Close();
                }
            }

            else
            if(Type == 1)
            {
                foreach(int Port in aPorts)
                {
                    if(keep_running == false)
                    {
                        Status.AppendText("+ the scan has been canceled by the user!\r\n");
                        return;
                    }

                    Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
                    {
                        Blocking = true,
                        ReceiveTimeout = 150,
                        SendTimeout = 150,
                        SendBufferSize = 75
                    };

                    IAsyncResult result = client.BeginConnect(target_ipv4, Port, null, null);
                    bool success = result.AsyncWaitHandle.WaitOne(Scan_Timeout, true);

                    if(client.Connected)
                    {
                        Status.AppendText($"+ port {Port} seems open!\r\n");

                        client.EndConnect(result);
                        client.Close();
                    }

                    else
                    {
                        Status.AppendText($"- port {Port} seems closed!\r\n");

                        client.Close();
                    }
                }
            }

            else
            if(Type == 2)
            {
                for (int index = aPorts[0]; index <= aPorts[1]; index += 1)
                {
                    if (keep_running == false)
                    {
                        Status.AppendText("+ the scan has been canceled by the user!\r\n");
                        return;
                    }

                    Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp)
                    {
                        Blocking = true,
                        ReceiveTimeout = 150,
                        SendTimeout = 150,
                        SendBufferSize = 75
                    };

                    IAsyncResult result = client.BeginConnect(target_ipv4, index, null, null);
                    bool success = result.AsyncWaitHandle.WaitOne(Scan_Timeout, true);

                    if(client.Connected)
                    {
                        Status.AppendText($"+ port {index} seems open!\r\n");

                        client.EndConnect(result);
                        client.Close();
                    }

                    else
                    {
                        Status.AppendText($"- port {index} seems closed!\r\n");

                        client.Close();
                    }
                }
            }

            Status.AppendText("--------------------------------------------------------\r\n");
        }
	}
}