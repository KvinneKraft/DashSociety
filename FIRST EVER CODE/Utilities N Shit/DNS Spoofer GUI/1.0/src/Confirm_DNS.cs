using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

// DNS_Type_GUI FreeDNS_Dialog = new DNS_Type_GUI(); FreeDNS_Dialog.ShowDialog();

namespace Pony_Spoofer_GUI {
    public partial class Confirm_DNS : Form {
      public string resKey;
       
       Button Oki = new Button(), Cancel = new Button();
       Dash_Lib DashCore = new Dash_Lib();
       //DNS_Type_GUI Show_GUI = new DNS_Type_GUI();

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

        public Confirm_DNS() {
               InitializeComponent();

                 resKey = "Pony_Spoofer_GUI.Embeded";
               
                System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 this.MaximumSize = new Size(260, 190);
                 this.MinimumSize = new Size(260, 190);
                 this.MaximizeBox = false;
                 this.MinimizeBox = false;
                 
                 this.Top             = -50; //new Point(137, 35);
                 this.StartPosition   = FormStartPosition.CenterParent; //(137, 35);
                 this.FormBorderStyle = FormBorderStyle.None;
                 this.Size            = new System.Drawing.Size(260, 190);
                
                 this.MouseDown += new MouseEventHandler(Move_Window);     

                 this.ControlBox = false;
                 this.BackColor  = Color.FromArgb(24, 31, 43);
              
              // Add a warning message right here :D

              DashCore.WriteText(this, "You are about to enter the Free Public", false, 6, 15, 10, 255, 255, 255);
              DashCore.WriteText(this, "DNS Server List(s).", false, 68, 15+18, 10, 255, 255, 255);

            // Fix the sizing for the confirmation thingy

              DashCore.WriteText(this, "Please Confirm that you are sure about this by", false, 15, 15+(3*18), 8, 255, 255, 255);  
              DashCore.WriteText(this, "Pressing Oki, or If you are not sure about this", false, 17, 15+(3*18)+14, 8, 255, 255, 255);
              DashCore.WriteText(this, "make sure to Press Nope!", false, 65, 15+(3*18)+(14*2), 8, 255, 255, 255);

              DashCore.CreateButton(this, true, Oki, true, "Oki", String.Empty, true, 9, 0, 25, 140, 100, 28, 30, 30, 30, 255, 255, 255);
              DashCore.CreateButton(this, true, Cancel, true, "Nope", String.Empty, true, 9, 0, 135, 140, 100, 28, 30, 30, 30, 255, 255, 255);
             
               Oki.Click += (sender, e) => { this.Hide(); DNS_Type_GUI Show_GUI = new DNS_Type_GUI(); Show_GUI.ShowDialog(); };             
               Cancel.Click += (sender, e) => { this.Close(); };   

                 this.Update();
        }
    }
}
