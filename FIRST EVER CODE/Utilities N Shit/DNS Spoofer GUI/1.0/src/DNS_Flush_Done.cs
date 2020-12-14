
/*

 (c) All Rights Reserved, Dashies Software Inc.
 
 DNS Flushing Success Dialog :o
 
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
    public partial class DNS_Flush_Done : Form {
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

        public DNS_Flush_Done() {
              InitializeComponent();
            
               this.Top =- 50;

               this.MaximumSize = new System.Drawing.Size(270, 190);
               this.MinimumSize = new System.Drawing.Size(270, 190);
               
               this.ControlBox  = false;
               this.MaximizeBox = false;
               this.MinimizeBox = false;
               
               this.StartPosition = FormStartPosition.CenterParent;
               this.FormBorderStyle = FormBorderStyle.None;
               
               //for now we disable this functionality to prevent the window from being annoying or something.
               //this.MouseDown += new MouseEventHandler(Move_Window);

               this.Size = new System.Drawing.Size(270, 190);
               this.BackColor = Color.FromArgb(24, 31, 43);
              
              DashCore.WriteText(this, "Successfully Flushed your DNS!", false, 1, 5, 13, 255, 255, 255);
              
              DashCore.WriteText(this, "Depending on your System, you may", false, 15, 55, 10, 255, 255, 255); // uses steps of 18
              DashCore.WriteText(this, "need to restart your System in order", false, 16, 55+18, 10, 255, 255, 255);
              DashCore.WriteText(this, "for the Changes to take effect.", false, 32, 55+(2*18), 10, 255, 255, 255);

              DashCore.LoadImage(this, "Twilight Sparkle", -12, 140, 64, 64, 10000);
              DashCore.LoadImage(this, "Po Po Pony", 212, 140, 84, 84, 10000);
               
               Okay.Click += (sender, e) => { this.Close(); };

              DashCore.CreateButton(this, false, Okay, true, "Okay", String.Empty, false, 10, 0, 95, 140, 100, 28, 62, 12, 73, 255, 255, 255);

            return ;
        }
    }
}
