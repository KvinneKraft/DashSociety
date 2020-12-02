
/* 

(c) All Rights Reserved, Dashies Software Inc. 
 
*/

using System;
using Microsoft.Win32;
using System.Drawing;
using System.Data;
using System.Data.Sql;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Linq;
using Microsoft.VisualBasic.Devices;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Resources;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace src {
    public partial class Startup : Form {
      core DashCore = new core();

      Label Description = new Label(),
            Title = new Label(),
            EndOfDescription = new Label(),
            Copyright = new Label();

      String Startup_Message = ( 
            "we are happy to announce that this is our first stable release of the\r\n" + 
            "Dash Detection Software after a lot of hard work and after dodging a\r\n" +
            "ton of obstacles on the way of acomplishing all of this.\r\n\r\n" +

            "we have finally managed to get it all to work like we truly wanted\r\n" + 
            "to, we have always wanted to bring something like this to you and here\r\n" + 
            "it is in all of its glory and beauty Dash Detection 1.0.\r\n\r\n" +
            
            "we hope you are going to find our application just or maybe even very\r\n" + 
            "useful and we also hope to receive some feedback if you feel like giving\r\n" + 
            "which in that case, you can reach us through our website."
      ), Title_Message = (
            "Dash Detection 1.0"
      ), Copyright_Message = (
            "Copyright © 2018 Dashies Software"
      );

      PictureBox LittleGif = new PictureBox(),
                 Cover = new PictureBox();

        public Startup(Form Parent) {
            InitializeComponent();

            this.Paint += (sender, magic) => {
                DashCore.Draw_Rectangle(this, 1, 175, 115, 300, 1, 40, 40, 40);
                DashCore.Draw_Rectangle(this, 1, 24, 115, this.Width-48, this.Height-195, 7, 7, 7);
                DashCore.Draw_Rectangle(this, 1, 175, 50, 300, 65, 7, 7, 7);

                DashCore.RegisterImage(this, Cover, "", 175, 115, 301, 2);
                Cover.BackColor = Color.FromArgb(40, 40, 40);
                Cover.BringToFront();

                DashCore.RegisterImage(this, LittleGif, "rainbow 24x24", 374, 57, 24, 24);
                LittleGif.BringToFront();
            };

            DashCore.SetWindowProperties(this, 0, 18, Parent.Width+16, Parent.Height+20, "Sub_Icon", 40, 40, 40);

            DashCore.WriteText(this, Title, (Title_Message), DashCore.DEFAULT_FONT_TYPE, 19, 1000, 0, 0, true, 0, 70, 40, 40, 40, 255, 255, 255);
            DashCore.WriteText(this, Description, (Startup_Message), DashCore.DEFAULT_FONT_TYPE, 11, DashCore.DEFAULT_FONT_WEIGHT, 0, 0, true, 0, 135, 40, 40, 40, 255, 255, 255);
            DashCore.WriteText(this, Copyright, (Copyright_Message), DashCore.DEFAULT_FONT_TYPE, 5, DashCore.DEFAULT_FONT_WEIGHT, 0, 0, true, 0, this.Height-12, 40, 40, 40, 255, 255, 255);
        }
    }
}
