
/*

(c) All Rights Reserved, Dashies Software Inc.

Gaming Server List Dialog
 
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
    public partial class Gaming_Servers : Form {
      public string resKey;
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
      
      public string DNS_List;

        public Gaming_Servers() {
               InitializeComponent();

                 resKey = "Pony_Spoofer_GUI.Embeded";
               
                System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 this.MaximumSize = new Size(330, 250);
                 this.MinimumSize = new Size(330, 250);
                 this.MaximizeBox = false;
                 this.MinimizeBox = false;
                 
                 this.Top             = -50; //new Point(137, 35);
                 this.StartPosition   = FormStartPosition.CenterParent; //(137, 35);
                 this.FormBorderStyle = FormBorderStyle.None;
                 this.Size            = new System.Drawing.Size(425, 250); 
                 
                 this.MouseDown += new MouseEventHandler(Move_Window);

                 this.ControlBox = false;
                 this.BackColor  = Color.FromArgb(24, 31, 43);
                 
                 this.Update();
              
              DashCore.CreateButton(this, true, Okay, true, "X", String.Empty, true, 14, 0, 300, 0, 32, 32, 24, 31, 43, 255, 255, 255);
             
               Okay.Click += (sender, e) => { this.Close(); };
              
              DashCore.WriteText(this, "Google: 8.8.8.8 -> 8.8.4.4", false, 0, 60, 10, 255, 255, 255);
              DashCore.WriteText(this, "OpenDNS: 208.67.222.222 -> 208.67.220.220", false, 0, 78, 10, 255, 255, 255);
              DashCore.WriteText(this, "Level3: 209.244.0.3 -> 209.244.0.4", false, 0, 96, 10, 255, 255, 255);
              DashCore.WriteText(this, "DNS.WATCH: 84.200.70.40 -> 82.200.69.80", false, 0, 114, 10, 255, 255, 255);
              DashCore.WriteText(this, "OpenNIC: 87.98.175.85 -> 87.98.175.85", false, 0, 132, 10, 255, 255, 255);
              DashCore.WriteText(this, "UncensoredDNS: 91.239.100.100 -> 89.233.43.71", false, 0, 150, 10, 255, 255, 255);
              
              DashCore.CreateButton(this, false, SaveAsTXT, true, "Save to File", String.Empty, true, 11, 0, 102, 200, 125, 28, 30, 30, 30, 255, 255, 255);
              
               SaveAsTXT.Click += (sender, e) => { 
                   DNS_List = "::: Dashies Free and Public Gaming Domain Name Server List :::\r\n\r\nGoogle : 8.8.8.8 -> 8.8.4.4\r\nOpenDNS : 208.67.222.222 -> 208.67.220.220\r\nLevel3 : 209.244.0.3 -> 209.244.0.4\r\nDNS.WATCH : 84.200.70.40 -> 82.200.69.80\r\nOpenNIC : 87.98.175.85 -> 87.98.175.85\r\nUncensoredDNS : 91.239.100.100 -> 89.233.43.71\r\n\r\n\r\n(c) All Rights Reserved, Dashies Software Inc."; 
                   DashCore.SaveFileAs(DNS_List); 
               };

              DashCore.LoadImage(this, "Pacman", 275, 195, 64, 64, 10);
              DashCore.LoadImage(this, "Cookie Munster", 0, 173, 64, 64, 10);
        }
    }
}
