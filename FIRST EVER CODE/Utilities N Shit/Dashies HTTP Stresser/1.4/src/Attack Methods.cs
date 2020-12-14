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

        public Label ATTACK_METHODS = new Label(), ATTACK_TYPES = new Label(),
                            HTTP_LONG_SOCKS = new Label(), UDP_JUICES = new Label(), UDP_HAM = new Label(), UDP_TSUNAMI = new Label(), TCP_SHITS = new Label(), TCP_WAVES = new Label(), RAW_MEAT = new Label();

        public Boolean SLOWLORIS = false, DASHLORIS = false, POSTSPAM = true, POSTGET = false,
                       HTTPLONGSOCKS = true, UDPJUICES = false, UDPHAM = false, UDPTSUNAMI = false, TCPSHITS = false, TCPWAVES = false, RAWMEAT = false;

        private Button quit = new Button(),
            httplongsocks = new Button(), udpjuices = new Button(), udpham = new Button(), udptsunami = new Button(), tcpshits = new Button(), tcpwaves = new Button(), rawmeat = new Button(),
            slowloris = new Button(), dashloris = new Button(), postspam = new Button(), getspam = new Button();

        public Attack_Methods()
        {
            InitializeComponent();
            InitializeAttackMethods();

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            dash.InjectText(this, ATTACK_TYPES, 20, 35, 200, 24, "Attack Type Modules", 11, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, httplongsocks, "", 25, 75, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, HTTP_LONG_SOCKS, (httplongsocks.Left + httplongsocks.Width) + 10, httplongsocks.Top+2, 150, 20, "HTTP Long Socks", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, udpjuices, "", 25, 100, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, UDP_JUICES, (udpjuices.Left + udpjuices.Width) + 10, udpjuices.Top+2, 150, 20, "UDP Juices", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, udpham, "", 25, 125, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, UDP_HAM, (udpham.Left + udpham.Width) + 10, udpham.Top+2, 150, 20, "UDP Ham Spam", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, udptsunami, "", 25, 150, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, UDP_TSUNAMI, (udptsunami.Left + udptsunami.Width) + 10, udptsunami.Top+2, 150, 20, "UDP Tsunami", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, tcpshits, "", 25, 175, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, TCP_SHITS, (tcpshits.Left + tcpshits.Width) + 10, tcpshits.Top+2, 150, 20, "TCP Shits", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, tcpwaves, "", 25, 200, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, TCP_WAVES, (tcpwaves.Left + tcpwaves.Width) + 10, tcpwaves.Top+2, 150, 20, "TCP Waves", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, rawmeat, "", 25, 225, 20, 20, 10, 48, 48, 48, 255, 255, 255, 48, 48, 48, 255, 255, 255);
            dash.InjectText(this, RAW_MEAT, (rawmeat.Left + rawmeat.Width) + 10, rawmeat.Top+2, 150, 20, "RAW Meat", 9, 16, 16, 16, 255, 255, 255);

            dash.InjectText(this, ATTACK_METHODS, this.Width-200, 35, 225, 24, "Attack Method Modules", 11, 16, 16, 16, 255, 255, 255);

            dash.InjectButton(this, quit, "X", this.Width - 74, -3, 74, 24, 12, 16, 16, 16, 255, 255, 255, 24, 24, 24, 255, 255, 255);
            quit.Click += (sender, argument) =>
            {
                this.Close();
            };
        }

        public Boolean InitializeAttackMethods()
        {
            /*
            
            Set the X and Y to the Center of the Parent ^-^

            */

            this.Size = new Size(450, 300);
            if(this.Size != new Size(450, 300))
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

                Write.DrawRectangle(Pawn, 0, 0, this.Width-1, this.Height-1);
            };
            
            this.BackColor = Color.FromArgb(16, 16, 16);
            this.ForeColor = Color.FromArgb(255, 255, 255);

            return true;
        }
    }
}
