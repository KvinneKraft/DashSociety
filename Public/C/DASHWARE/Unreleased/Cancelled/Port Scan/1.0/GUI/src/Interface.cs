
/* (c) All Rights Reserved, Dashies Software Inc. */

/* 
 
this little compact, effective, efficient and small port scanner has
been made for good purposes only, perhaps educational.

this tool does not spoof your identity so be careful with it.

if you have no consent of your target then please do not proceed the 
scan as the scan will use packets with your current ip address in them.

it currently works for :
    - Transmission Control Protocol
    - User Datagram Protocol
    - RAW Data
 
*/

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
    public partial class Interface : Form
    {
        private static Inject.Function function = new Inject.Function();
        private static Inject.Cawntrol inject = new Inject.Cawntrol();
        private static Inject.Layout layout = new Inject.Layout();

        private static Scan_TCP tcp_scanner = null;
        private static Scan_UDP udp_scanner = null;
        private static Scan_RAW raw_scanner = null;

        private static ResourceManager embeded = new ResourceManager("src.Embeded", Assembly.GetExecutingAssembly());

        private static int R = 24, G = 24, B = 24;
        private static int W = 500, H = 375;

        private bool InitializeLayout()
        {
            try
            {
                FormBorderStyle = FormBorderStyle.None;
                StartPosition = FormStartPosition.CenterScreen;

                BackColor = Color.FromArgb(R, G, B);
                Font = new Font("Modern", 10);

                Icon = (Icon)embeded.GetObject("Logo");
                Text = "i LoVe GrAcE <3";

                Size = new Size(W, H);
                MaximumSize = new Size(W, H);
                MinimumSize = new Size(W, H);

                function.AnimatedCursor(this, (byte[])embeded.GetObject("cursor"));
                return true;
            }

            catch (Exception E)
            {
                MessageBox.Show(E.ToString());
                return false;
            }
        }

        private string Description_()
        {
            return
                (
                    "Pony Port Scanner has been made for\r\n" +
                    "educational purposes only.\r\n"
                );
        }

        private string Startup_()
        {
            return
                (
                    "-== Dashies Pony Port Scanner 1.0 ==-\r\n" +

                    "--------------------------------------------------------------------------------------\r\n\r\n" +

                    "this little compact, effective, efficient and small port scanner has been made for good purposes only, perhaps educational.\r\n\r\n" +

                    "this tool does not spoof your identity so be careful with it.\r\n\r\n" +

                    "if you do not have the consent of your target then please do not proceed the scan as the scan will use packets with your current ip address in them.\r\n\r\n" +

                    "it currently works for :\r\n" +
                    "    - Transmission Control Protocol\r\n    - User Datagram Protocol\r\n    - RAW Data\r\n\r\n" +

                    "--------------------------------------------------------------------------------------\r\n\r\n"
                );
        }

        private static Button Quit = new Button(), Minimize = new Button(), ClearLog = new Button(), Run = new Button();
        private static PictureBox ChocolateBar = new PictureBox(), Logo = new PictureBox(), Fluff = new PictureBox();

        private static TextBox Target = new TextBox(), Ports = new TextBox(), Status = new TextBox();
        private static Label Description = new Label(), TarBoll = new Label(), PorBoll = new Label(), Copyright = new Label(), TCP = new Label(), UDP = new Label(), RAW = new Label(), cTCP = new Label(), cUDP = new Label(), cRAW = new Label();

        private static int Mode = 0, Scan_Timeout = 150;
        private static bool Cooldown = false;

        private static Image Running = (Image)embeded.GetObject("Running"), Idle = (Image)embeded.GetObject("Idle");

        private static string[] ports, MODES = { "TCP", "UDP", "RAW" };
        private static string target_ipv4 = string.Empty, invParse = "Error";

        private static int[] aPorts = { 0 };
        private static int Type = 0;

        private static bool scan_running = false;



        private static void cool()
        {
            Cooldown = true;
            System.Threading.Thread.Sleep(3000);
            Cooldown = false;
        }



        private static bool isOnline(string target)
        {
            Ping ping = new Ping();
            PingReply reply = ping.Send(target);

            Socket HTTPStatus = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp) { Blocking = false, ReceiveTimeout = 150, SendTimeout = 150 };

            if (reply.Status == IPStatus.Success)
            {
                return true;
            }

            else
            {
                Status.AppendText("> the ordinary ping technique failed, moving on to http over tcp.\r\n");

                try
                {
                    IAsyncResult result = HTTPStatus.BeginConnect(target, 80, null, null);
                    bool success = result.AsyncWaitHandle.WaitOne(Scan_Timeout, true);

                    if(HTTPStatus.Connected)
                    {
                        HTTPStatus.EndConnect(result);
                        HTTPStatus.Close();

                        return true;
                    }

                    else
                    {
                        HTTPStatus.Close();

                        return false;
                    }
                }

                catch
                {
                    return false;
                }
            }
        }



        private static bool isValid(string suspect)
        {
            try
            {
                if (IPAddress.TryParse(suspect, out IPAddress address) != true)
                {
                    if((suspect.ToLower().Contains("https://") == true) || (suspect.ToLower().Contains("http://") == true))
                    {
                        Status.AppendText("> i just detected a url!\r\n> trying to convert it into a valid ipv4 address ....\r\n");
                        target_ipv4 = convert_to_ipv4(suspect);

                        if(target_ipv4 == invParse)
                        {
                            Status.AppendText($"> failed to convert {suspect} to a valid ipv4 address!\r\n");
                            return false;
                        }

                        Status.AppendText($"> successfully converted {Target.Text} to {target_ipv4}!\r\n");
                        Target.Text = target_ipv4;

                        return true;
                    }

                    else
                    {
                        Status.AppendText("> an invalid target has been specified!\r\n");
                        Status.AppendText("> did you forget to add \"http(s)://\"?\r\n");
                        return false;
                    }
                }

                Status.AppendText("> i just detected an ipv4 address!\r\n");
                target_ipv4 = Target.Text;

                return true;
            }

            catch
            {
                return false;
            }
        }



        private static string convert_to_ipv4(string url)
        {
            try
            {
                Uri URI = new Uri(url);
                var IP = Dns.GetHostAddresses(URI.Host)[0];

                string verify = IP.ToString();

                if (verify.Length < 8)
                {
                    verify = "Error";
                }

                return verify;
            }

            catch
            {
                return "Error";
            }
        }



        private static void async_tcpscan()
        {
            tcp_scanner = new Scan_TCP(target_ipv4, Target.Text, aPorts, Type, Scan_Timeout, Status);
            Status.AppendText("> the scan has been finished successfully!\r\n");
            modifyScan(0, Mode);
        }

        private static void async_udpscan()
        {
            udp_scanner = new Scan_UDP(target_ipv4, Target.Text, aPorts, Type, Scan_Timeout, Status);
            Status.AppendText("> the scan has been finished successfully!\r\n");
            modifyScan(0, Mode);
        }

        private static void async_rawscan()
        {
            raw_scanner = new Scan_RAW(target_ipv4, Target.Text, aPorts, Type, Scan_Timeout, Status);
            Status.AppendText("> the scan has been finished successfully!\r\n");
            modifyScan(0, Mode);
        }



        private static void modifyScan(int wut, int type)
        {
            Control[] controls = { Target, Ports, ClearLog, TCP, UDP, RAW };
            object[] classes = { tcp_scanner, udp_scanner, raw_scanner };

            if (wut == 0) // Stop The Scan
            {
                Fluff.Image = Idle;
                Run.Text = "Start Scan";

                if (tcp_scanner != null) tcp_scanner.keep_running = false;
                if (udp_scanner != null) udp_scanner.keep_running = false;
                if (raw_scanner != null) raw_scanner.keep_running = false;

                scan_running = false;

                foreach(Control control in controls) control.Enabled = true;

                Status.AppendText("> received a quit message for the current scan, stopping soon!\r\n");
            }

            else // Start The Scan
            {
                Fluff.Image = Running;
                Run.Text = "Stop Scan";

                scan_running = true;

                foreach (Control control in controls) control.Enabled = false;

                Status.AppendText("> the scan is initiating ....\r\n");
            }
        }

        public Interface()
        {
            InitializeComponent();

            if (InitializeLayout() != true)
            {
                MessageBox.Show("an error occurred while initializing the GUI layout!\n\nplease try again, if this does not solve the issue your\noperating system may just not be compatible ;)", "-o-", MessageBoxButtons.OK);
                Application.Exit();
            }

            else
            {
                this.Enabled = false;

                Notify notify = new Notify();
                notify.ShowDialog();

                this.Enabled = true;

                inject.Button(this, Quit, "X", "Modern", 12, false, this.Width - 101, 1, 100, 28, 16, 16, 16, 255, 255, 255, 30, 30, 30, 255, 255, 255);
                Quit.Click += (s, q) => Application.Exit();

                inject.Button(this, Minimize, "-", "Modern", 12, false, this.Width - 201, 1, 100, 28, 16, 16, 16, 255, 255, 255, 30, 30, 30, 255, 255, 255);
                Minimize.Click += (s, q) => base.WindowState = FormWindowState.Minimized;

                inject.PictureBox(this, Logo, "Logo1", true, false, true, 1, 1, 0, 0, 16, 16, 16);
                inject.PictureBox(this, ChocolateBar, String.Empty, false, false, false, 1, 1, this.Width - 2, 28, 16, 16, 16);

                inject.Label(this, TarBoll, "Target", "Modern", 10, true, false, 10, 50, 0, 0, R, G, B, 155, 90, 188);
                inject.Label(this, PorBoll, "Ports", "Modern", 10, true, false, 10, (TarBoll.Top + TarBoll.Height + 5), 0, 0, R, G, B, 155, 90, 188);

                inject.Label(this, TCP, "TCP", "Modern", 9, true, false, PorBoll.Left, (PorBoll.Top + 40) + 40, 0, 0, R, G, B, 255, 255, 255); // TCP
                inject.Label(this, cTCP, "✓", "Modern", 11, false, false, TCP.Left + TCP.Width + 15, TCP.Top, 16, 16, 8, 8, 8, 53, 99, 91);

                inject.Label(this, UDP, "UDP", "Modern", 9, true, false, PorBoll.Left, (TCP.Top + 20), 0, 0, R, G, B, 255, 255, 255); // UDP
                inject.Label(this, cUDP, "", "Modern", 11, false, false, TCP.Left + TCP.Width + 15, UDP.Top, 16, 16, 8, 8, 8, 53, 99, 91);

                UDP.Enabled = false;
                cUDP.Enabled = false;

                inject.Label(this, RAW, "RAW", "Modern", 9, true, false, PorBoll.Left, (UDP.Top + 20), 0, 0, R, G, B, 255, 255, 255); // RAW
                inject.Label(this, cRAW, "", "Modern", 11, false, false, TCP.Left + TCP.Width + 15, RAW.Top, 16, 16, 8, 8, 8, 53, 99, 91);

                RAW.Enabled = false;
                cRAW.Enabled = false;

                List<Label> c = new List<Label>() { cTCP, cUDP, cRAW };

                for (int index = 0; index <= 2; index += 1)
                {
                    int i = index;

                    c[i].TextAlign = ContentAlignment.MiddleCenter;

                    c[i].Click += (s, q) =>
                    {
                        foreach (Label sId in c)
                            sId.Text = String.Empty;

                        c[i].Text = "✓";
                        Mode = i;
                    };
                }

                inject.Button(this, Run, "Start Scan", "Modern", 9, false, PorBoll.Left - 3, PorBoll.Top + 38, 100, 24, 53, 99, 91, 255, 255, 255, 55, 101, 93, 255, 255, 255);
                ClearLog.TextAlign = ContentAlignment.TopCenter;

                inject.Button(this, ClearLog, "ClearLog", "Modern", 9, false, Run.Left + Run.Width + 5, Run.Top, 100, 24, 53, 99, 91, 255, 255, 255, 55, 101, 93, 255, 255, 255);
                Run.TextAlign = ContentAlignment.TopCenter;

                inject.Label(this, Copyright, "Copyright (c) Dashies Software 2019", "Modern", 7, true, false, Run.Left, (this.Height - 16), 0, 0, R, G, B, 255, 255, 255);
                inject.Label(this, Description, Description_(), "Modern", 8, true, false, Run.Left, (Copyright.Top - 50), 0, 0, R, G, B, 155, 90, 188);

                inject.TextBox(this, Status, Startup_(), "Modern", 7, true, false, (this.Width - (270 + 10)), (PorBoll.Top + 40), 279, 262, 8, 8, 8, 200, 200, 200);
                Status.ReadOnly = true;

                inject.TextBox(this, Target, "https://www.google.co.uk", "Modern", 10, false, false, (TarBoll.Width + TarBoll.Left + 5), (TarBoll.Top + 2), 250, 18, 8, 8, 8, 200, 200, 200);
                inject.TextBox(this, Ports, "80,443,23,22,21,2222,8080", "Modern", 10, false, false, Target.Left, (PorBoll.Top + 2), 250, 18, 8, 8, 8, 200, 200, 200);

                inject.PictureBox(this, Fluff, "Idle", true, false, false, this.Width - 104, Target.Top - 6, 71, 64, R, G, B);

                ClearLog.Click += (s, q) => Status.Text = Startup_();

                Run.Click += (s, q) =>
                {
                    if (Cooldown == false)
                    {
                        if (Run.Text == "Start Scan")
                        {
                            modifyScan(1, Mode);

                            Target.Update();
                            Status.AppendText($"> checking if {Target.Text} is valid ....\r\n");

                            Thread chkUrl = new Thread(
                                () =>
                                {
                                    if (isValid(Target.Text) != true)
                                    {
                                        Status.AppendText("> the scan has been canceled because i failed to validate your target!\r\n");

                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            modifyScan(0, Mode);
                                        });
                                    }
                                });

                            chkUrl.Start();

                            while (chkUrl.ThreadState == ThreadState.Running)
                            {
                                Application.DoEvents();
                            }

                            if (scan_running == true)
                            {
                                Status.AppendText($"> checking if {target_ipv4} is online ....\r\n");

                                Thread chkOnline = new Thread(
                                    () =>
                                    {
                                        if (isOnline(target_ipv4) == false)
                                        {
                                            Status.AppendText("> the scan has been canceled because the target is offline!\r\n");

                                            this.Invoke((MethodInvoker)delegate
                                            {
                                                modifyScan(0, Mode);
                                            });
                                        }
                                    });

                                chkOnline.Start();

                                while (chkOnline.ThreadState == ThreadState.Running)
                                {
                                    Application.DoEvents();
                                }

                                if (scan_running == true)
                                {
                                    Status.AppendText($"> it seems like {target_ipv4} is online!\r\n");

                                    if(Ports.Text.Contains("-") == true)
                                    {
                                        ports = Ports.Text.Split('-');
                                        aPorts = Array.ConvertAll(ports, int.Parse);

                                        Type = 2;
                                    }

                                    else
                                    if(Ports.Text.Contains(",") == true)
                                    {
                                        ports = Ports.Text.Split(',');
                                        aPorts = Array.ConvertAll(ports, int.Parse);

                                        Type = 1;
                                    }

                                    else
                                    {
                                        aPorts[0] = Convert.ToInt32(Ports.Text);
                                        Type = 0;
                                    }

                                    Status.AppendText($"> starting the actual scan using mode ....\r\n");

                                    if (Mode == 0)
                                    {
                                        // TCP
                                        scan_running = true;
                                        Task TCPScan = Task.Run((Action)async_tcpscan);
                                    }

                                    if (Mode == 1)
                                    {
                                        // UDP
                                        scan_running = true;
                                        Task UDPScan = Task.Run((Action)async_udpscan);
                                    }

                                    else
                                    {
                                        // RAW
                                        scan_running = true;
                                        Task RAWScan = Task.Run((Action)async_rawscan);
                                    }

                                    if ((Mode == 0) || (Mode == 1) || (Mode == 2))
                                    {
                                        Status.AppendText("> the scan has been started!\r\n");
                                    }
                                }
                            }
                        }

                        else
                        //if (Run.Text == "Stop Scan")
                        {
                            modifyScan(0, Mode);
                        }

                        //Task cooldown = Task.Run((Action)cool);
                    }
                };

                ChocolateBar.MouseMove += (s, q) => function.Mouse_Move(this, q);
                ChocolateBar.MouseDown += (s, q) => function.Mouse_Down(q);

                Paint += (s, q) =>
                {
                    layout.Border(q, 0, 0, this.Width, this.Height, 2, 8, 8, 8); // Application Border
                    layout.Border(q, 0, 0, this.Width, 29, 2, 8, 8, 8);          // Menu Border
                    layout.Border(q, this.Width - 201, 0, 200, 28, 2, 8, 8, 8);  // Exit and Minimize Button

                    layout.Border(q, Status.Left, Status.Top - 1, Status.Width, Status.Height, 2, 8, 8, 8);   // Status Box Log Thing Border
                    layout.Border(q, Target.Left, Target.Top - 1, Target.Width, Target.Height, 2, 8, 8, 8); // Target Box
                    layout.Border(q, Ports.Left, Ports.Top - 1, Ports.Width, Ports.Height, 2, 8, 8, 8);     // Port Box
                };
            }
        }
    }
}
