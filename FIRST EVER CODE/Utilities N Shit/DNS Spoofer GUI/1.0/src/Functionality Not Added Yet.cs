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
    public partial class Functionality_Not_Added_Yet : Form {
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

        public Functionality_Not_Added_Yet() {
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
              
              DashCore.WriteText(this, "Functionality Unavailable!", false, 27, 5, 13, 255, 255, 255);
              
              DashCore.WriteText(this, "This functionality is currently not", false, 30, 55, 10, 255, 255, 255); // uses steps of 18
              DashCore.WriteText(this, "implemented, this will most likely", false, 31, 55+18, 10, 255, 255, 255);
              DashCore.WriteText(this, "be patched in the next update!", false, 35, 55+(2*18), 10, 255, 255, 255);

              DashCore.LoadImage(this, "Sad Pony", -12, 140, 64, 64, 10000);
              DashCore.LoadImage(this, "Sad Mime", 200, 140, 84, 84, 10000);
               
               Okay.Click += (sender, e) => { this.Close(); };

              DashCore.CreateButton(this, false, Okay, true, "Okay", String.Empty, false, 10, 0, 95, 140, 100, 28, 62, 12, 73, 255, 255, 255);

            return ;
        }
    }
}
