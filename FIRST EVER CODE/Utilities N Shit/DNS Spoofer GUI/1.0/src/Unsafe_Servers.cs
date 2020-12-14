
/* 

(c) All Rights Reserved, Dashies Software Inc.

Unsafe Server List Dialog

*/

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
    public partial class Unsafe_Servers : Form {
      public string resKey, DNS_List;
      Dash_Lib DashCore = new Dash_Lib();
      Button Okay = new Button(), SaveAsTXT = new Button();

      public const int WM_NCLBUTTONDOWN = 0xA1;
      public const int HT_CAPTION = 0x2;

      [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
      public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

      [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
      public static extern bool ReleaseCapture();
      
        private void Move_Window(object sender, System.Windows.Forms.MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public Unsafe_Servers() {
               InitializeComponent();

                 resKey = "Pony_Spoofer_GUI.Embeded";
               
                System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 this.MaximumSize = new Size(330, 325);
                 this.MinimumSize = new Size(330, 325);
                 this.MaximizeBox = false;
                 this.MinimizeBox = false;
                 
                 this.Top             = -50; //new Point(137, 35);
                 this.StartPosition   = FormStartPosition.CenterParent; //(137, 35);
                 this.FormBorderStyle = FormBorderStyle.None;
                 this.Size            = new System.Drawing.Size(425, 200);
                
                 this.MouseDown += new MouseEventHandler(Move_Window);     

                 this.ControlBox = false;
                 this.BackColor  = Color.FromArgb(24, 31, 43);
              
            // Start Y == 60
             
              DashCore.CreateButton(this, true, Okay, true, "X", String.Empty, true, 14, 0, 300, 0, 32, 32, 24, 31, 43, 255, 255, 255);

               Okay.Click += (sender, e) => { this.Close(); };   
            
              DashCore.WriteText(this, "FDN: 80.67.169.12 -> 80.67.169.12", false, 0, 60, 10, 255, 255, 255);
              DashCore.WriteText(this, "Free DNS: 37.235.1.174 -> 37.235.1.174", false, 0, 60+(1*18), 10, 255, 255, 255);
              DashCore.WriteText(this, "New Nations: 5.45.96.220 -> 5.45.96.220", false, 0, 60+(2*18), 10, 255, 255, 255);
              DashCore.WriteText(this, "Unknown: 185.82.22.133 -> 185.82.22.133", false, 0, 60+(3*18), 10, 255, 255, 255);
              DashCore.WriteText(this, "Sprintlink: 204.117.214.10 -> 204.117.214.10", false, 0, 60+(4*18), 10, 255, 255, 255);
              DashCore.WriteText(this, "Unknown: 4.2.2.5 -> 4.2.2.6", false, 0, 60+(5*18), 10, 255, 255, 255);
              DashCore.WriteText(this, "Yandex: 77.88.8.88 -> 77.88.8.88", false, 0, 60+(6*18), 10, 255, 255, 255);
              DashCore.WriteText(this, "SkyDNS: 193.58.251.251 -> 193.58.251.251", false, 0, 60+(7*18), 10, 255, 255, 255);
              DashCore.WriteText(this, "Verizon LVL3: 4.2.2.1 -> 4.2.2.2", false, 0, 60+(8*18), 10, 255, 255, 255);
              DashCore.WriteText(this, "Unknown: 95.181.211.6 -> 91.217.137.37", false, 0, 60+(9*18), 10, 255, 255, 255);          
             
              DashCore.CreateButton(this, false, SaveAsTXT, true, "Save to File", String.Empty, true, 11, 0, 102, 278, 125, 28, 30, 30, 30, 255, 255, 255);
              
               SaveAsTXT.Click += (sender, e) => { 
                   DNS_List = "::: Dashies Free and Public Unsafe Domain Name Server List :::\r\n\r\nUnknown: 95.181.211.6 -> 91.217.137.37\r\nVerizon LVL3: 4.2.2.1 -> 4.2.2.2\r\nSkyDNS: 193.58.251.251 -> 193.58.251.251\r\nYandex: 77.88.8.88 -> 77.88.8.88\r\nUnknown: 4.2.2.5 -> 4.2.2.6\r\nNew Nations: 5.45.96.220 -> 5.45.96.220\r\nFree DNS: 37.235.1.174 -> 37.235.1.174\r\nFDN: 80.67.169.12 -> 80.67.169.12\r\n\r\n(c) All Rights Reserved, Dashies Software Inc."; 
                   DashCore.SaveFileAs(DNS_List); 
               };
             
              DashCore.LoadImage(this, "Coin", 275, 270, 64, 64, 10);
              DashCore.LoadImage(this, "Heart", 8, 275, 64, 64, 10);

                 this.Update();
        }
    }
}
