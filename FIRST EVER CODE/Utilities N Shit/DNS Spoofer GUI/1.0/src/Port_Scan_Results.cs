
/* (c) All Rights Reserved, Dashies Software Inc. */

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
    public partial class Port_Scan_Results : Form {
       Dashies_Port_Scanner Access = new Dashies_Port_Scanner();
       Dash_Lib DashCore = new Dash_Lib();

       public string resKey, Output, Log, Target;
       public Panel PortResultContainer = new Panel();
       public Button Okay = new Button();

       public const int WM_NCLBUTTONDOWN = 0xA1;
       public const int HT_CAPTION = 0x2;

       [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
       public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
       [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
       public static extern bool ReleaseCapture();

       private void MOVE(object sender, System.Windows.Forms.MouseEventArgs e) {     
          if(e.Button == MouseButtons.Left) {
              ReleaseCapture();
              SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
          }
       }

       public void ColorPanelBorder(object sender, PaintEventArgs e) {
           Invalidate();
           ControlPaint.DrawBorder(e.Graphics, this.PortResultContainer.ClientRectangle, Color.FromArgb(22, 23, 35), ButtonBorderStyle.Solid);
       }
    
        public void CreateTextContainer(Form Get, Panel TextID, int width, int height, int X, int Y, int br, int bg, int bb, int fr, int fg, int fb) {
                TextID.ForeColor = Color.FromArgb(fr, fg, fb);
                TextID.BackColor = Color.FromArgb(br, bg, bb);
                TextID.BorderStyle = BorderStyle.None;
                TextID.AutoSize = false;
                TextID.AutoScroll = false;
                TextID.HorizontalScroll.Enabled = true;
                TextID.HorizontalScroll.Visible = true;
                TextID.AutoScroll = true;
                TextID.Width = width;
                TextID.Height = height-75;
                TextID.Paint += new PaintEventHandler(ColorPanelBorder);
                TextID.Location = new Point(X, Y);

               Get.Controls.Add(TextID);
           return;
       }

        public Port_Scan_Results() {
            InitializeComponent();
                resKey = "Pony_Spoofer_GUI.Embeded";
              // String.Concat(System.IO.File.ReadAllLines(@"Log.txt"));
                 string[] Logg = System.IO.File.ReadAllLines(@"Log.txt");
                
                  for(int Index = 0; Index <= Logg.Length-1; Index = Index + 1)
                       Log = Log + "\r\n" + Logg[Index];       

                Target = String.Concat(System.IO.File.ReadAllLines(@"Target.txt"));
                  
                  System.IO.File.Delete(@"Target.txt");
                  System.IO.File.Delete(@"Log.txt");

                Output = "*** THE PORT SCAN HAS OFFICIALY STARTED ....\r\n*** NOTE : FOR NOW WE ARE ONLY DISPLAYING OPEN PORTS! \r\n*** SCANNING "+Target+" FOR THE GIVEN TCP PORTS ....\r\n======================================"+Log+"\r\n======================================\r\n*** SCAN HAS ENDED SUCCESSFULLY!";     

                System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 this.MaximumSize = new Size(435, 480);
                 this.MinimumSize = new Size(435, 480);
                 this.MaximizeBox = false;
                 this.MinimizeBox = false;
                 
                 this.MouseDown += new MouseEventHandler(MOVE);
                 this.Top             = -50; //new Point(137, 35);
                 this.StartPosition   = FormStartPosition.CenterParent; //(137, 35);
                 this.FormBorderStyle = FormBorderStyle.None;
                 this.Size            = new System.Drawing.Size(435, 480); 
                 
                 this.ControlBox = false;
                 this.BackColor  = Color.FromArgb(30, 40, 56);
                
                 Okay.Click += (sender, e) => { this.Close(); };
                
                DashCore.LoadImage(this, "Port Scanner Dialog", 0, 0, 30, 24, 0000);
                DashCore.WriteText(this, "::: (c) All Rights Reserved, Dashies Software Inc :::", false, 80, 2, 9, 255, 255, 255);
                DashCore.CreateButton(this, true, Okay, true, "X", String.Empty, true, 15, 0, 405, -2, 32, 25, 30, 40, 56, 255, 255, 255);
                CreateTextContainer(this, PortResultContainer, 450, 550, 0, 24, 0, 35, 91, 204, 204, 204);
                DashCore.InsertTextIntoContainer(this, PortResultContainer, Output, 0, 0, 0, 0, 10, 204, 204, 204);
                
               // while(Access.State!=true) {
               //     System.Threading.Thread.Sleep(250);
               // } Access.State = false;

                 this.Update();
        }
    }
}
