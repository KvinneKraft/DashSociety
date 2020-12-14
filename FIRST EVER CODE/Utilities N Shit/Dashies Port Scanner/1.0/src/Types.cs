
/* (c) All Rights Reserved, Dashies Software Inc. */

// Types Dialog for the Port Scanner.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace src
{
    public partial class Types : Form
    {
        DashBase.Collector database = new DashBase.Collector();

        MainConfiguration config = new MainConfiguration();

        CustomForm.Inject inject = new CustomForm.Inject();
        CustomForm.Draw draw = new CustomForm.Draw();

        Button Both = new Button(), UserDatagramProtocol = new Button(), TransmissionControlProtocol = new Button();
        Label BOTH_INFO = new Label(), UDP_INFO = new Label(), TCP_INFO = new Label();

        public int Selection = 2;

        // 0 : UDP
        // 1 : TCP
        // 2 : Both

        public Types()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            this.BackColor = Color.FromArgb(235, 235, 235);
            this.ForeColor = Color.FromArgb(230, 230, 230); 

            this.Font = new Font("Consolas", 10);
            this.Size = new Size(392, 100);

            this.MinimizeBox = false;
            this.MaximizeBox = false;
            
            ResourceManager access_embeded = new ResourceManager((config.Embeded_Key), Assembly.GetExecutingAssembly());

            this.Icon = (Icon)access_embeded.GetObject("settings_icon");
            this.Text = "(Type Selection Menu for Dashies Port Scanner 1.0)";

            inject.AnimatedCursor(this, (byte[])access_embeded.GetObject("cursor"));

            int R = 195, G = 195, B = 195, FR = 32, FG = 32, FB = 32;

            inject.InjectButton(this, false, UserDatagramProtocol, false, " ", 6, 10, 10, 16, 16, R, G, B, FR, FG, FB, R, G, B, FR, FG, FB);
            inject.InjectLabel(this, false, UDP_INFO, "User Datagram Protocol Port Scanning (UDP)", false, false, 9, UserDatagramProtocol.Left+UserDatagramProtocol.Width+5, UserDatagramProtocol.Top+1, 350, UserDatagramProtocol.Height-1, 235, 235, 235, FR, FG, FB);

            inject.InjectButton(this, false, TransmissionControlProtocol, false, " ", 6, 10, 30, 16, 16, R, G, B, FR, FG, FB, R, G, B, FR, FG, FB);
            inject.InjectLabel(this, false, TCP_INFO, "Transmission Control Protocol Port Scanning (TCP)", false, false, 9, TransmissionControlProtocol.Left + TransmissionControlProtocol.Width + 5, TransmissionControlProtocol.Top + 1, 350, TransmissionControlProtocol.Height - 1, 235, 235, 235, FR, FG, FB);

            inject.InjectButton(this, false, Both, false, " ", 6, 10, 50, 16, 16, R, G, B, FR, FG, FB, R, G, B, FR, FG, FB);
            inject.InjectLabel(this, false, BOTH_INFO, "Or just all of them at once :^)", false, false, 9, Both.Left + Both.Width + 5, Both.Top+1, 350, Both.Height-1, 235, 235, 235, FR, FG, FB);

            Both.Click += (sender, receiver) =>
            {
                if(Selection != 2)
                {
                    Selection = 2;
                    SetText();
                }
            };

            UserDatagramProtocol.Click += (sender, receiver) =>
            {
                if(Selection != 0)
                {
                    Selection = 0;
                    SetText();
                }
            };

            TransmissionControlProtocol.Click += (sender, receiver) =>
            {
                if(Selection != 1)
                {
                    Selection = 1;
                    SetText();
                }
            };

            SetText();
        }

        private void SetText()
        {
            List<Control>Storage = new List<Control>() { Both, UserDatagramProtocol, TransmissionControlProtocol };

            foreach(Button Id in Storage)
            {
                Id.Text = "";
                Id.TextAlign = ContentAlignment.TopCenter;
            }

            if(Selection == 0) UserDatagramProtocol.Text = "X";
            if(Selection == 1) TransmissionControlProtocol.Text = "X";
            if(Selection == 2) Both.Text = "X";
        }
    }
}
