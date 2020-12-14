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

/*

HTTP(s) Attack Methods {
    - Post
    - Get
    - Slowloris
    - SlowDash
}

    Slowloris and SlowDash may not be active at the same time
    IPv4 Attack Types may not be used with the HTTP Attack Methods.

Attack Types {
    - HTTP Long Socks
    - UDP Juices
    - UDP Ham
    - UDP Tsunami
    - TCP Shits
    - TCP Waves
    - RAW Meat
}
 
*/

namespace src
{
    public partial class Attack_Methods : Form
    {
        Tools tools = new Tools();
        Dash_Library dash = new Dash_Library();

        /*
            
            # caching may only be applied to get
            Caching :
                - 0 = no-store
                - 1 = no-cache
                - 2 = none

            # connection may be applied to both post and get
            Connection :
                - 0 = keep-alive
                - 1 = close

            # http version may be applied to post and get
            HTTP Version :
                - 0 = 1.0
                - 1 = 1.1
                - 2 = 1.2
            
            # pragma may only be applied to post
            Pragma :
                - 0 = no-cache
                - 1 = none

        */

        public int AttackType = 0, MethodType = 2, Caching = 2, Connection = 1, HTTPVersion = 1, Pragma = 1;

        public Label ATTACK_METHODS = new Label(), ATTACK_TYPES = new Label(),
            SLOW_LORIS = new Label(), SLOW_DASH = new Label(), POST_SPAM = new Label(), GET_SPAM = new Label(), HEAD_SPAM = new Label(),
            HTTP_LONG_SOCKS = new Label(), UDP_JUICES = new Label(), UDP_HAM = new Label(), UDP_TSUNAMI = new Label(), TCP_SHITS = new Label(), TCP_WAVES = new Label(), RAW_MEAT = new Label();

        private Button quit = new Button(),
            httplongsocks = new Button(), udpjuices = new Button(), udpham = new Button(), udptsunami = new Button(), tcpshits = new Button(), tcpwaves = new Button(), rawmeat = new Button(),
            slowloris = new Button(), dashloris = new Button(), postspam = new Button(), getspam = new Button(), headspam = new Button();
        
        public static bool isHttpEnabled = true;

        public static ToolTip tip = new ToolTip
        {
            IsBalloon = false,
            UseAnimation = true,
            UseFading = true,
            ForeColor = Color.FromArgb(48, 48, 48)
        };

        public Attack_Methods()
        {
            InitializeAttackMethods();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            List<Button> Types = new List<Button>
            {
                httplongsocks, udpjuices, udpham, udptsunami,
                tcpshits, tcpwaves, rawmeat
            };

            List<Button> Methods = new List<Button>
            {
                postspam, getspam, headspam, slowloris, dashloris
            };

            dash.InjectText(this, ATTACK_TYPES, 20, 35, 200, 24, "Attack Type Modules", 11, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, httplongsocks, "", 25, 75, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, HTTP_LONG_SOCKS, (httplongsocks.Left + httplongsocks.Width) + 10, httplongsocks.Top + 2, 150, 20, "HTTP Long Socks", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, udpjuices, "", 25, 100, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, UDP_JUICES, (udpjuices.Left + udpjuices.Width) + 10, udpjuices.Top + 2, 150, 20, "UDP Juices", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, udpham, "", 25, 125, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, UDP_HAM, (udpham.Left + udpham.Width) + 10, udpham.Top + 2, 150, 20, "UDP Ham Spam", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, udptsunami, "", 25, 150, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, UDP_TSUNAMI, (udptsunami.Left + udptsunami.Width) + 10, udptsunami.Top + 2, 150, 20, "UDP Tsunami", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, tcpshits, "", 25, 175, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, TCP_SHITS, (tcpshits.Left + tcpshits.Width) + 10, tcpshits.Top + 2, 150, 20, "TCP Shits", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, tcpwaves, "", 25, 200, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, TCP_WAVES, (tcpwaves.Left + tcpwaves.Width) + 10, tcpwaves.Top + 2, 150, 20, "TCP Waves", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, rawmeat, "", 25, 225, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, RAW_MEAT, (rawmeat.Left + rawmeat.Width) + 10, rawmeat.Top + 2, 150, 20, "RAW Meat", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectText(this, ATTACK_METHODS, this.Width - 200, 35, 225, 24, "Attack Method Modules", 11, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, postspam, "", 240, 75, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, POST_SPAM, (postspam.Left + postspam.Width) + 10, postspam.Top + 2, 150, 20, "HTTP POST Spam", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, headspam, "", 240, 100, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, HEAD_SPAM, (headspam.Left + headspam.Width) + 10, headspam.Top + 2, 150, 20, "HTTP HEAD Spam", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, getspam, "", 240, 125, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, GET_SPAM, (postspam.Left + postspam.Width) + 10, getspam.Top + 2, 150, 20, "HTTP GET Spam", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, slowloris, "", 240, 150, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, SLOW_LORIS, (postspam.Left + postspam.Width) + 10, slowloris.Top + 2, 150, 20, "Slowloris Based", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, dashloris, "", 240, 175, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, SLOW_DASH, (postspam.Left + postspam.Width) + 10, dashloris.Top + 2, 150, 20, "DashLoris 1.0", 9, 16, 16, 16, 255, 255, 255);

            if (AttackType == 0) httplongsocks.Text = "❤";
            else if (AttackType == 1) udpjuices.Text = "❤";
            else if (AttackType == 2) udpham.Text = "❤";
            else if (AttackType == 3) udptsunami.Text = "❤";
            else if (AttackType == 4) tcpshits.Text = "❤";
            else if (AttackType == 5) tcpwaves.Text = "❤";
            else if (AttackType == 6) rawmeat.Text = "❤";

            if (MethodType == 0) slowloris.Text = "Ʊ";
            else if (MethodType == 1) dashloris.Text = "Ʊ";
            else if (MethodType == 2) postspam.Text = "Ʊ";
            else if (MethodType == 3) getspam.Text = "Ʊ";
            else if (MethodType == 4) headspam.Text = "Ʊ";

            foreach (Button Id in Methods)
            {
                Id.Click += (Amazing, Beauty) =>
                {
                    if ((Id == slowloris) || (Id == dashloris) || (Id == postspam) || (Id == getspam) || (Id == headspam))
                    {
                        if (isHttpEnabled == true)
                        {
                            foreach (Button sId in Methods)
                            {
                                if (sId != Id)
                                {
                                    sId.Text = " ";
                                }
                                else
                                {
                                    if (sId == slowloris) MethodType = 0;
                                    else if (sId == dashloris) MethodType = 1;
                                    else if (sId == postspam) MethodType = 2;
                                    else if (sId == getspam) MethodType = 3;
                                    else if (sId == headspam) MethodType = 4;

                                    sId.Text = "Ʊ";
                                }
                            }
                        }
                        else
                        {
                            tip.ToolTipTitle = ("Error :o");
                            tip.Show("HTTP Options are not compatible with IPv4 Options.", ATTACK_TYPES, 0, 0, 1000);
                        }
                    }
                };
            }

            foreach (Button Id in Types)
            {
                Id.Click += (Amazing, Beauty) =>
                {
                    if (Id == httplongsocks)
                    {
                        isHttpEnabled = true;

                        foreach (Button sId in Methods)
                        {
                            sId.BackColor = Color.FromArgb(48, 48, 48);
                            sId.ForeColor = Color.FromArgb(255, 255, 255);

                            sId.MouseEnter += (sentBy, msValentine) =>
                            {
                                sId.BackColor = Color.FromArgb(48, 48, 48);
                                sId.ForeColor = Color.FromArgb(255, 255, 255);
                            };

                            sId.MouseHover += (sentBy, msValentine) =>
                            {
                                sId.BackColor = Color.FromArgb(48, 48, 48);
                                sId.ForeColor = Color.FromArgb(255, 255, 255);
                            };

                            sId.MouseLeave += (sentBy, msValentine) =>
                            {
                                sId.BackColor = Color.FromArgb(48, 48, 48);
                                sId.ForeColor = Color.FromArgb(255, 255, 255);
                            };
                        }
                    }
                    else
                    {
                        isHttpEnabled = false;

                        foreach (Button sId in Methods)
                        {
                            sId.BackColor = Color.FromArgb(8, 8, 8);
                            sId.ForeColor = Color.FromArgb(190, 190, 190);

                            sId.MouseEnter += (sentBy, msValentine) =>
                            {
                                sId.BackColor = Color.FromArgb(8, 8, 8);
                                sId.ForeColor = Color.FromArgb(190, 190, 190);
                            };

                            sId.MouseHover += (sentBy, msValentine) =>
                            {
                                sId.BackColor = Color.FromArgb(8, 8, 8);
                                sId.ForeColor = Color.FromArgb(190, 190, 190);
                            };

                            sId.MouseLeave += (sentBy, msValentine) =>
                            {
                                sId.BackColor = Color.FromArgb(8, 8, 8);
                                sId.ForeColor = Color.FromArgb(190, 190, 190);
                            };
                        }
                    }

                    foreach (Button eId in Types)
                    {
                        if (eId != Id)
                        {
                            eId.Text = " ";
                        }
                        else
                        {
                            if (eId == httplongsocks) AttackType = 0;
                            else if (eId == udpjuices) AttackType = 1;
                            else if (eId == udpham) AttackType = 2;
                            else if (eId == udptsunami) AttackType = 3;
                            else if (eId == tcpshits) AttackType = 4;
                            else if (eId == tcpwaves) AttackType = 5;
                            else if (eId == rawmeat) AttackType = 6;

                            eId.Text = "❤";
                        }
                    }
                };
            }

            dash.InjectButton(this, quit, "X", this.Width - 74, -3, 74, 24, 12, 16, 16, 16, 255, 255, 255, 24, 24, 24, 255, 255, 255);
            quit.Click += (sender, argument) =>
            {
                this.Hide();
            };
        }

        public Boolean InitializeAttackMethods()
        {
            this.Size = new Size(450, 300);
            if (this.Size != new Size(450, 300))
            {
                return false;
            }

            ResourceManager Addr = new ResourceManager(("src.embeded"), Assembly.GetExecutingAssembly());
            tools.SetFormCursor(this, (Byte[])Addr.GetObject("main"));

            this.Text = "Ʊ";
            this.Icon = (Icon)Addr.GetObject("icon");

            this.Paint += (sent, sexygirl) => {
                Graphics Write = this.CreateGraphics();
                Pen Pawn = new Pen(Color.FromArgb(12, 12, 12));

                Write.DrawRectangle(Pawn, 0, 0, this.Width - 1, this.Height - 1);
            };

            this.BackColor = Color.FromArgb(16, 16, 16);
            this.ForeColor = Color.FromArgb(255, 255, 255);

            return true;
        }
    }
}
