
/* 

(c) All Rights Reserved, Dashies Software Inc.

The ole Standard DNS Failure Dialog!
 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pony_Spoofer_GUI {
    public partial class DNS_Flush_Failure : Form {
      Dash_Lib DashCore = new Dash_Lib();
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

        public DNS_Flush_Failure() {
              InitializeComponent();
            
               this.Top =- 50;

               this.MaximumSize = new System.Drawing.Size(270, 260);
               this.MinimumSize = new System.Drawing.Size(270, 260);
               
               this.ControlBox  = false;
               this.MaximizeBox = false;
               this.MinimizeBox = false;
               
               this.StartPosition = FormStartPosition.CenterParent;
               this.FormBorderStyle = FormBorderStyle.None;
               
               //for now we disable this functionality to prevent the window from being annoying or something.
               //this.MouseDown += new MouseEventHandler(Move_Window);

               this.Size = new System.Drawing.Size(270, 260);
               this.BackColor = Color.FromArgb(24, 31, 43);
              
              DashCore.WriteText(this, "DNS Cleaning Failure Occurred!", false, 1, 5, 13, 255, 255, 255);
              
              DashCore.WriteText(this, "we have encountered an error while", false, 20, 55, 10, 255, 255, 255);
              DashCore.WriteText(this, "trying to flush your DNS Cache(s).", false, 22, 55+18, 10, 255, 255, 255);

              DashCore.WriteText(this, "this can be fixed by retrying to Flush", false, 17, 55+(3*18), 10, 255, 255, 255);
              DashCore.WriteText(this, "your DNS Cache(s), if this does not", false, 20, 55+(4*18), 10, 255, 255, 255);
              DashCore.WriteText(this, "fix this issue then please contact our", false, 16, 55+(5*18), 10, 255, 255, 255);
              DashCore.WriteText(this, "Developer Team through Twitter.", false, 28, 55+(6*18), 10, 255, 255, 255);

              DashCore.LoadImage(this, "Twilight Sparkle", -12, 210, 64, 64, 10000);
              DashCore.LoadImage(this, "Po Po Pony", 212, 210, 84, 84, 10000);
               
               Okay.Click += (sender, e) => { this.Close(); };

              DashCore.CreateButton(this, false, Okay, true, "Okay", String.Empty, false, 10, 0, 95, 210, 100, 28, 62, 12, 73, 255, 255, 255);

            return ;
        }
    }
}
