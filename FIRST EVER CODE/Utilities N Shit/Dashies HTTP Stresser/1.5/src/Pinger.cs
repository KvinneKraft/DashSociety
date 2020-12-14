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
    public class Pawng : Form
    {
        private Tools tool = new Tools();
        private Dash_Library get = new Dash_Library();
        private Button quit = new Button();
        public static TextBox Status = new TextBox();
        public static String target = String.Empty, url = String.Empty;
        public static Boolean IsPinging = false, Active = false;

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
            Active = true;

            Task.Run((Action)RunPing);

            quit.Click += (sender, argumentation) =>
            {
                IsPinging = false;
                Tokend.Cancel();
                System.Threading.Thread.Sleep(2000);
                this.Close();
            };
        }

        static void RunPing()
        {
            while (IsPinging == true)
            {
                for (int index = 1; index <= 8; index += 1)
                {
                    if(IsPinging == false) {
                        continue; 
                    }

                    Ping pingah = new Ping();
                    PingReply reply = pingah.Send(target, 1000);

                    if (reply.Status != IPStatus.Success) Status.AppendText($"[{index}] no reply has been received from {url}!\r\n");
                    else { Status.AppendText($"[{index}] we have received a reply from {url} in {reply.RoundtripTime}ms!\r\n"); System.Threading.Thread.Sleep(500); }

                    pingah.Dispose();
                }

                Status.AppendText("\r\n* the results are shown above!\r\n");
                Status.AppendText("* we have successfully pinged the target.");

                IsPinging = false;
            }
        }
    }
}
