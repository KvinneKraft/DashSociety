
/* (c) All Rights Reserved, Dashies Software Inc. */

// Method Dialog for the Port Scanner.

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
    public partial class Methods : Form
    {
        SplashScreenConfiguration config = new SplashScreenConfiguration();

        DashBase.Collector database = new DashBase.Collector();
        
        CustomForm.Inject inject = new CustomForm.Inject();
        CustomForm.Draw draw = new CustomForm.Draw();

        public int Selection = 0, Y = 10, Address = 0;

        private static Button Both_Intensive_Scan = new Button(), Both_Slow_Scan = new Button(), Both_Aggressive_Scan = new Button(), UDP_Flood_Scan = new Button(), UDP_Quick_Scan = new Button(), TCP_Quick_Scan = new Button(); 
        private static TextBox Intensive_Scan = new TextBox(), Slow_Scan = new TextBox(), Aggressive_Scan = new TextBox(), Flood_Scan = new TextBox(), UDP_Quicky_Scan = new TextBox(), TCP_Quicky_Scan = new TextBox();

        private Button[] Modify = { Both_Intensive_Scan, Both_Slow_Scan, Both_Aggressive_Scan, UDP_Flood_Scan, UDP_Quick_Scan, TCP_Quick_Scan };
        private TextBox[] Easy = { Intensive_Scan, Slow_Scan, Aggressive_Scan, Flood_Scan, UDP_Quicky_Scan, TCP_Quicky_Scan }; 

        public Methods()
        {
            InitializeComponent();

            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;

            this.BackColor = Color.FromArgb(235, 235, 235);
            this.ForeColor = Color.FromArgb(230, 230, 230);

            this.Font = new Font("Consolas", 10);
            this.Size = new Size(400, 350);

            this.MinimizeBox = false;
            this.MaximizeBox = false;

            ResourceManager access_embeded = new ResourceManager((config.Embeded_Key), Assembly.GetExecutingAssembly());

            this.Icon = (Icon) access_embeded.GetObject("settings_icon");
            this.Text = "(Method Selection Menu for Dashies Port Scanner 1.0)";

            inject.AnimatedCursor(this, (byte[]) access_embeded.GetObject("cursor"));

            foreach(Button Id in Modify)
            {
                inject.InjectButton(this, false, Id, false, " ", 6, 10, Y, 16, 16, 195, 195, 195, 32, 32, 32, 195, 195, 195, 32, 32, 32);
                Y += 20;
            }

            for(int index = 0; index <= Modify.Length-1; index += 1)
            {
                Modify[index].Click += (sender, receiver) =>
                {
                    //Selection = Array.IndexOf(Modify, Modify[index]);
                    Modify[2].Text = "D";
                    SetText();
                };
            }

            SetText();
        }

        private void SetText()
        {
            foreach(Button Id in Modify)
            {
                Id.Text = "";
                Id.TextAlign = ContentAlignment.TopCenter;
            }

            if(Modify[Selection].Text != "X")
            {
                Modify[Selection].Text = "X";
            }
        }
    }
}
