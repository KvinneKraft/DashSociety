
/*

(c) All Rights Reserved, Dashies Software Inc.

Neutral Server List Dialog
 
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
    public partial class Neutral_Servers : Form {
      public string resKey;
      Dash_Lib DashCore = new Dash_Lib();
      Functionality_Not_Added_Yet Not_Available = new Functionality_Not_Added_Yet();
      Button Okay = new Button();

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

        public Neutral_Servers() {
               InitializeComponent();

               Not_Available.ShowDialog();
                 
                 this.Close();

                 resKey = "Pony_Spoofer_GUI.Embeded";
               
                System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 this.MaximumSize = new Size(1, 1);
                 this.MinimumSize = new Size(1, 1);
                 this.MaximizeBox = false;
                 this.MinimizeBox = false;
                 
                 this.Top             = -50; //new Point(137, 35);
                 this.StartPosition   = FormStartPosition.CenterParent; //(137, 35);
                 this.FormBorderStyle = FormBorderStyle.None;
                 this.Size            = new System.Drawing.Size(1, 1); 
                 this.MouseDown += new MouseEventHandler(Move_Window);

                 this.ControlBox = false;
                 this.BackColor  = Color.FromArgb(24, 31, 43);
            
               DashCore.CreateButton(this, true, Okay, true, "X", String.Empty, true, 14, 0, 270, 0, 32, 32, 24, 31, 43, 255, 255, 255);
               
                Okay.Click += (sender, e) => { this.Close(); };
        }
    }
}
