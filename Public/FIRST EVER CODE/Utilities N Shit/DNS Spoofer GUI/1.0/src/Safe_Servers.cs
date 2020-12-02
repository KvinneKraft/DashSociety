
/*
 
(c) All Rights Reserved, Dashies Software Inc.

Safe Server List Dialog

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
    public partial class Safe_Servers : Form {
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

        public Safe_Servers() {
               InitializeComponent();

                 resKey = "Pony_Spoofer_GUI.Embeded";
               
                System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 this.MaximumSize = new Size(350, 375);
                 this.MinimumSize = new Size(350, 375);
                 this.MaximizeBox = false;
                 this.MinimizeBox = false;
                 
                 this.Top             = -50; //new Point(137, 35);
                 this.StartPosition   = FormStartPosition.CenterParent; //(137, 35);
                 this.FormBorderStyle = FormBorderStyle.None;
                 this.Size            = new System.Drawing.Size(425, 375); 
                 
                 this.MouseDown += new MouseEventHandler(Move_Window);

                 this.ControlBox = false;
                 this.BackColor  = Color.FromArgb(24, 31, 43);
              
               DashCore.CreateButton(this, true, Okay, true, "X", String.Empty, true, 14, 0, 320, 0, 32, 32, 24, 31, 43, 255, 255, 255);
                
                Okay.Click += (sender, e) => { this.Close(); };
             
               DashCore.WriteText(this, "Comodo: 8.26.56.26 -> 8.20.247.20", false, 0, 50, 10, 255, 255, 255);
               DashCore.WriteText(this, "Cloudflare: 1.1.1.1 -> 1.0.0.1", false, 0, 50+(1*18), 10, 255, 255, 255);
               DashCore.WriteText(this, "Quad9: 9.9.9.9 -> 149.112.112.112", false, 0, 50+(2*18), 10, 255, 255, 255);
               DashCore.WriteText(this, "Norton 1: 199.85.126.10 -> 199.85.126.10", false, 0, 50+(3*18), 10, 255, 255, 255);
               DashCore.WriteText(this, "Norton 2: 199.85.126.20 -> 199.85.126.20", false, 0, 50+(4*18), 10, 255, 255, 255);
               DashCore.WriteText(this, "Norton 3: 199.85.126.30 -> 199.85.126.30", false, 0, 50+(5*18), 10, 255, 255, 255);
               DashCore.WriteText(this, "OpenNIC 1: 193.183.98.66 -> 193.183.98.66", false, 0, 50+(6*18), 10, 255, 255, 255);
               DashCore.WriteText(this, "OpenNIC 2: 188.165.200.156 -> 188.165.200.156", false, 0, 50+(7*18), 10, 255, 255, 255);
               DashCore.WriteText(this, "OpenNIC 3: 51.254.25.115 -> 51.254.25.115", false, 0, 50+(8*18), 10, 255, 255, 255);
               DashCore.WriteText(this, "OpenNIC 4: 206.125.173.29 -> 206.125.173.29", false, 0, 50+(9*18), 10, 255, 255, 255);
               DashCore.WriteText(this, "OpenNIC 5: 45.56.117.188 -> 45.56.117.188", false, 0, 50+(10*18), 10, 255, 255, 255);
               DashCore.WriteText(this, "OpenNIC 6: 96.90.175.167 -> 96.90.175.167", false, 0, 50+(11*18), 10, 255, 255, 255);
               DashCore.WriteText(this, "OpenNIC 7: 45.32.230.225 -> 45.32.230.225", false, 0, 50+(12*18), 10, 255, 255, 255);
              
               DashCore.CreateButton(this, false, SaveAsTXT, true, "Save to File", String.Empty, true, 11, 0, 102, 324, 125, 28, 30, 30, 30, 255, 255, 255);
              
                SaveAsTXT.Click += (sender, e) => { 
                    DNS_List = "::: Dashies Free and Public Secure Domain Name Server List :::\r\n\r\nComodo: 8.26.56.26 -> 8.20.247.20\r\nCloudflare: 1.1.1.1 -> 1.0.0.1\r\nQuad9: 9.9.9.9 -> 149.112.112.112\r\nNorton 1: 199.85.126.10 -> 199.85.126.10\r\nNorton 2: 199.85.126.20 -> 199.85.126.20\r\nNorton 3: 199.85.126.30 -> 199.85.126.30\r\nOpenNIC 1: 193.183.98.66 -> 193.183.98.66\r\nOpenNIC 2: 188.165.200.156 -> 188.165.200.156\r\nOpenNIC 3: 51.254.25.115 -> 51.254.25.115\r\nOpenNIC 4: 206.125.173.29 -> 206.125.173.29\r\nOpenNIC 5: 45.56.117.188 -> 45.56.117.188\r\nOpenNIC 6: 96.90.175.167 -> 96.90.175.167\r\nOpenNIC 7: 45.32.230.225 -> 45.32.230.225\r\n\r\n(c) All Rights Reserved, Dashies Software Inc.";
                    DashCore.SaveFileAs(DNS_List); 
                };

               DashCore.LoadImage(this, "Pixel Monster 1", 275, 320, 64, 64, 10);
               DashCore.LoadImage(this, "Pixel Monster 2", -3, 321, 64, 64, 10);

                 this.Update();
        }
    }
}
