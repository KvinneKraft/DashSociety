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
    public partial class DNS_Type_GUI : Form {
      public string resKey;
      public Button SafeServers = new Button(), 
                    GamingServers = new Button(),
                    NeutralServers = new Button(),
                    UnsafeServers = new Button(),
                    Okay = new Button(); 
      
      Dash_Lib DashCore = new Dash_Lib();
      Functionality_Not_Added_Yet Not_Available = new Functionality_Not_Added_Yet();

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

        public DNS_Type_GUI() {
               InitializeComponent();   

                 resKey = "Pony_Spoofer_GUI.Embeded";
               
                System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 this.MaximumSize = new Size(450, 170);
                 this.MinimumSize = new Size(450, 170);
                 this.MaximizeBox = false;
                 this.MinimizeBox = false;
                 
                 this.Top             = -50; //new Point(137, 35);
                 this.StartPosition   = FormStartPosition.CenterParent; //(137, 35);
                 this.FormBorderStyle = FormBorderStyle.None;
                 this.Size            = new System.Drawing.Size(450, 155); 
                
                DashCore.WriteText(this, "::: (c) All Rights Reserved, Dashies Software Inc :::", false, 85, 5, 9, 255, 255, 255);
                DashCore.WriteText(this, "Please select one of the given DNS Server categories in order to\nchoose one of our many Public DNS Servers.", false, 18, 45, 10, 255, 255, 255);
                DashCore.WriteText(this, "(c) All Rights Reserved, Dashies Software Inc.", false, 150, 160, 5, 255, 255, 255);

                DashCore.CreateButton(this, true, Okay, true, "X", String.Empty, true, 14, 0, 420, 0, 32, 32, 30, 40, 56, 255, 255, 255);
            
                DashCore.CreateButton(this, true, SafeServers, true, "Safe", String.Empty, true, 10, 0, 10, 105, 105, 28, 30, 30, 30, 255, 255, 255); // Safe Servers 105
                DashCore.CreateButton(this, true, GamingServers, true, "Gaming", String.Empty, true, 10, 0, 120, 105, 100, 28, 30, 30, 30, 255, 255, 255); // Gaming Servers
                DashCore.CreateButton(this, true, UnsafeServers, true, "Unsafe", String.Empty, true, 10, 0, 230, 105, 100, 28, 30, 30, 30, 255, 255, 255); // Unsafe Servers
                DashCore.CreateButton(this, true, NeutralServers, true, "Neutral", String.Empty, true, 10, 0, 340, 105, 100, 28, 30, 30, 30, 255, 255, 255); // Neutral Servers
                
                  SafeServers.Click += (sender, e) => { Safe_Servers Show_SafeServers = new Safe_Servers(); Show_SafeServers.ShowDialog(); };
                  GamingServers.Click += (sender, e) => { Gaming_Servers Show_GamingServers = new Gaming_Servers(); Show_GamingServers.ShowDialog(); };
                  UnsafeServers.Click += (sender, e) => { Unsafe_Servers Show_UnsafeServers = new Unsafe_Servers(); Show_UnsafeServers.ShowDialog(); };
                  NeutralServers.Click += (sender, e) => { Not_Available.ShowDialog(); /*Neutral_Servers Show_NeutralServers = new Neutral_Servers(); Show_NeutralServers.ShowDialog();*/ };
                  
                  Okay.Click += (sender, e) => { this.Close(); };
                 
                 this.MouseDown += new MouseEventHandler(Move_Window);
                 this.ControlBox = false;
                 this.BackColor  = Color.FromArgb(30, 40, 56);
                 this.Update();
        }
    }
}
