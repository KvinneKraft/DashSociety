/* (c) All Rights Reserved, Dashies Software Inc. */

using System;
using System.Drawing;
using System.Data;
using System.Data.Sql;
using System.Net.NetworkInformation;
using System.Security.Principal;
//using System.Text;
//using System.IO;
//using System.Resources;
//using System.Reflection;
using System.Windows.Forms;
//using System.Collections.Generic;
//using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Pony_Spoofer_GUI {
    public partial class About_Dialog : Form {
      string resKey;// bTitle = "Ponyyy";
      Dash_Lib DashCore = new Dash_Lib();
        public About_Dialog() {
                InitializeComponent();
               
               //Startup access = new Startup(); 
               Button Okay = new Button();
                 
                 resKey = "Pony_Spoofer_GUI.Embeded";
               
                System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 this.MaximumSize = new Size(350, 305);
                 this.MinimumSize = new Size(350, 305);
                 this.MaximizeBox = false;
                 this.MinimizeBox = false;
                 
                 this.Top             = -50; //new Point(137, 35);
                 this.StartPosition   = FormStartPosition.CenterParent; //(137, 35);
                 this.FormBorderStyle = FormBorderStyle.None;
                 this.Size            = new System.Drawing.Size(350, 305); 
                 
                 this.ControlBox = false;
                 this.BackColor  = Color.FromArgb(30, 40, 56);
                 
                 this.Update();
                 
                 Okay.Click += (sender, args) => { this.Close(); };
                
                DashCore.LoadImage(this, "About Dialog", 286, 245, 64, 64, 22);
                DashCore.LoadImage(this, "About Dialog 2", 0, 259, 50, 50, 22);
                DashCore.LoadImage(this, "Heart", 275, 1, 48, 48, 22);
                
                DashCore.WriteText(this, "About Pony Spoofer GUI", false, 62, 20, 14, 255, 255, 255);
                DashCore.WriteText(this, "this is a full remake of the original console \nPony Spoofer, fully recoded in both C# and C++. \n\nbecause it is this new, you may experience \nsome buggies here and there. \n\nif you do find any buggies then feel free to \nsupply us with a report of the bug or error. \n\n-Kind Regards, Dashies Software", false, 18, 70, 10, 255, 255, 255);
                
                DashCore.CreateButton(this, false, Okay, true, "Okay", String.Empty, false, 9, 0, 110, 260, 135, 30, 79, 58, 109, 255, 255, 255);
        }       
    }
}
