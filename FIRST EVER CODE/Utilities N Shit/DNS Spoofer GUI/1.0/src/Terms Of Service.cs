
/* (c) All Rights Reserved, Dashies Software Inc. */

using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Pony_Spoofer_GUI {
    public partial class Terms_Of_Service : Form {
        string resKey, TOS_Output;
        
        string[] TOS = new string[5]{
             "the following lines will contain the terms of our services (TOS) that you agree with when making actual use of our applications and services.\r\n\r\n", 
             "please read this all really careful as these terms of services (TOS) may contain things within that you must know in order to use our applications and services properly and safely.\r\n\r\n", // supposed to be 0 like in C, hopefuly it will be the same in here c:
             "IF YOU FIND ANY INCORRECT SPELLING OR AND GRAMMAR INSIDE OF THIS TOS THEN PLEASE LET US KNOW BY PROVIDING US WITH A REPORT OF THE MISTAKE YOU HAVE FOUND WITHIN THIS TOS.\r\n\r\n",
             "all of the official rights of this application are all reserved to us (dashies-software) and the developers that are part of it. \r\n\r\n",
             "\r\n1) WHEN USING OUR SERVICE(S) YOU AUTOMATICLY AGREE WITH ALL OF THE FOLLOWING TEXT.\r\n\r\n2) REPRODUCTION OF ANY OF OUR SERVICE(S) WITHOUT THE RIGHT PERMISSION IS NOT PERMITTED. \r\n\r\n3) WE ARE AND WILL NEVER BE RESPONSIBLE FOR WHAT EVER YOU ARE DOING WITH OUR SERVICE(S). \r\n\r\n4) OUR SOURCES ARE NOT OPEN THUS YOU ARE NOT PERMITTED TO EDIT OR VIEW THEM. \r\n\r\n5) MAKING MONEY OFF OUR SERVICE(S) WITHOUT OUR PERMISSION IS STRICTLY FORBIDDEN." 
        };

        public void Test(object sender, MouseEventArgs e) {
            this.Close();
        }

        Panel TOSContainer = new Panel();
        Button Okay = new Button();
        Dash_Lib DashCore = new Dash_Lib();

        public void ColorPanelBorder(object sender, PaintEventArgs e) {
            Invalidate();
            ControlPaint.DrawBorder(e.Graphics, this.TOSContainer.ClientRectangle, Color.FromArgb(22, 23, 35), ButtonBorderStyle.Solid);
        }

        public void CreateTextContainer(Form Get, Panel TextID, int width, int height, int X, int Y, int br, int bg, int bb, int fr, int fg, int fb) {
                TextID.ForeColor = Color.FromArgb(fr, fg, fb);
                TextID.BackColor = Color.FromArgb(br, bg, bb);
                TextID.BorderStyle = BorderStyle.None;
                TextID.AutoSize = false;
                TextID.AutoScroll = false;
                TextID.HorizontalScroll.Enabled = true;
                TextID.HorizontalScroll.Visible = true;
                TextID.AutoScroll = true;
                TextID.Width = width;
                TextID.Height = height-75;
                TextID.Paint += new PaintEventHandler(ColorPanelBorder);
                TextID.Location = new Point(X, Y);

               Get.Controls.Add(TextID);
           return;
        }

        public Terms_Of_Service() {
            InitializeComponent();

            TOS_Output = TOS[0] + TOS[1] + TOS[2] + TOS[3] + TOS[4];// + TOS[3];
            resKey = "Pony_Spoofer_GUI.Embeded";
            System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());

            this.MaximumSize = new Size(500, 505);
            this.MinimumSize = new Size(500, 505);
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Top = -50; //new Point(137, 35);
            this.StartPosition = FormStartPosition.CenterParent; //(137, 35);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new System.Drawing.Size(500, 505);

            this.ControlBox = false;
            this.BackColor = Color.FromArgb(30, 40, 56);

            Okay.Click += (sender, args) => { this.Close(); };
            
            CreateTextContainer(this, TOSContainer, 590, 500, 0, 0, 229, 229, 229, 37, 79, 74);
            DashCore.InsertTextIntoContainer(this, TOSContainer, TOS_Output, 432, 500, 1, 5, 9, 37, 79, 74);

            TOSContainer.MouseWheel += new MouseEventHandler(Test); 
            
            DashCore.LoadImage(this, "Mage", 430, 429, 82, 82, 0000);
            DashCore.LoadImage(this, "Scary_Rubut", -34, 405, 74, 74, 0000);
            DashCore.CreateButton(this, false, Okay, true, "Okay", String.Empty, false, 9, 0, 182, 460, 135, 30, 79, 58, 109, 255, 255, 255);
        }
	}
}
