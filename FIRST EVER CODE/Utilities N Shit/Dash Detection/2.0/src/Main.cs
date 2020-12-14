
/* 

(c) All Rights Reserved, Dashies Software Inc.

Main Window <3
 
*/

using System;
using Microsoft.Win32;
using System.Reflection;
using System.Resources;
using System.Management;
using System.Diagnostics;
using System.Drawing;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace src {
    public partial class Main : Form {
      public const int R = (40), // Window Colour Code 1 (Red)
                       G = (40), // Window Colour Code 2 (Green)
                       B = (40); // Window Colour Code 3 (Blue)

      public string Resource_Access_Key = ("src.embeded");

      core DashCore = new core(); 
   
      public const int WM_NCLBUTTONDOWN = 0xA1;
      public const int HT_CAPTION = 0x2;

      [DllImport("user32.dll")] public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
      [DllImport("user32.dll")] public static extern bool ReleaseCapture();
      [DllImport("dnsapi.dll", EntryPoint ="DnsFlushResolverCache")] private static extern UInt32 DnsFlushResolverCache();

        private void Move_Window(object Magnificant, MouseEventArgs Beautiful) {
              if(Beautiful.Button == MouseButtons.Left) {
                 ReleaseCapture();
                 SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
              }   
        }

        private void Disable_AutoSize(object Magnificant, MouseEventArgs Beautiful) {
              if(!Focused)
                Focus();
        }

        PictureBox Logo = new PictureBox();

        Button Network_Information = new Button(), 
               Hardware_Information = new Button(), 
               Software_Information = new Button(),
               About_Application = new Button(),
               Quit_Application = new Button(),
               Minimize_Application = new Button();

        PictureBox Anubus = new PictureBox();

        Boolean Startup = true;
        public int Id;

        public Main() {
          InitializeComponent();
          Application.DoEvents();

           this.Text = ("I Love You Dash :^)");
           this.Text = ("(c) All Rights Reserved, Dashies Software Inc.");

           this.MaximizeBox = false;
           this.MinimizeBox = false;
           this.DoubleBuffered = false;

           this.StartPosition = FormStartPosition.CenterScreen;
           this.FormBorderStyle = FormBorderStyle.None;

           this.BackColor = Color.FromArgb((R), (G), (B));
           
           this.Size = new Size(650, 450);

           this.MinimumSize = new Size(650, 450);
           this.MaximumSize = new Size(650, 450);

          ResourceManager Access_Resource = new ResourceManager((Resource_Access_Key), Assembly.GetExecutingAssembly());
         
          DashCore.CreateButton(this, Hardware_Information, "Hardware Information", DashCore.DEFAULT_FONT_TYPE, 8, 0, false, 18, 0, 140, 18, 7, 7, 7, 201, 201, 201);
          DashCore.CreateButton(this, Software_Information, "Software Information", DashCore.DEFAULT_FONT_TYPE, 8, 0, false, 18+1*140, 0, 140, 18, 7, 7, 7, 201, 201, 201);
          DashCore.CreateButton(this, Network_Information, "Network Information", DashCore.DEFAULT_FONT_TYPE, 8, 0, false, 18+2*140, 0, 140, 18, 7, 7, 7, 201, 201, 201);
          DashCore.CreateButton(this, About_Application, "About", DashCore.DEFAULT_FONT_TYPE, 8, 0, false, 18+3*140, 0, 55, 18, 7, 7, 7, 201, 201, 201);
          DashCore.CreateButton(this, Minimize_Application, "-", DashCore.DEFAULT_FONT_TYPE, 16, 0, false, this.Width-64, 0, 32, 18, 7, 7, 7, 255, 255, 255);
          DashCore.CreateButton(this, Quit_Application, "X", DashCore.DEFAULT_FONT_TYPE, 12, 0, false, this.Width-32, 0, 32, 18, 7, 7, 7, 255, 255, 255);
            
            this.Anubus.BackColor = Color.FromArgb(7, 7, 7);
            
          DashCore.SetIcon(this, "Sub_Icon");

           if(Startup != false) {
              Startup = false;
              Id = 0;

              Startup startup = new Startup(this);
              startup.TopLevel = false;
              this.Controls.Add(startup);
              startup.Show();
              startup.BringToFront();
           } 

            this.Logo.Click += (sender, archive) => {
               if(Id != 0) {
                  Id = 0;

                  Startup startup = new Startup(this);
                  startup.TopLevel = false;
                  this.Controls.Add(startup);
                  startup.Show();
                  startup.BringToFront();
               }
            };
            
            this.Minimize_Application.Click += (sender, archive) => {
                this.SendToBack();
            };

            this.Quit_Application.Click += (sender, archive) => {
                this.Close();  
            };

            this.About_Application.Click += (sender, archive) => {
               if(Id != 1) {
                  Id = 1;
                  
                  About about = new About(this);
                  about.TopLevel = false;
                  this.Controls.Add(about);
                  about.Show();
                  about.BringToFront();
               }  
            };

            this.Network_Information.Click += (sender, archive) => {
               if(Id != 2) {
                  Id = 2;
                    
                  Network_Information network_information = new Network_Information(this);
                  network_information.TopLevel = false;
                  this.Controls.Add(network_information);
                  network_information.Show();
                  network_information.BringToFront();
               }
            };

            this.Software_Information.Click += (sender, archive) => {
               if(Id != 3) {
                  Id = 3;

                  Software_Information software_information = new Software_Information(this);
                  software_information.TopLevel = false;
                  this.Controls.Add(software_information);
                  software_information.Show();
                  software_information.BringToFront();
               }
            };

            this.Hardware_Information.Click += (sender, archive) => {
               if(Id != 4) {
                  Id = 4;
      
                  Hardware_Information hardware_information = new Hardware_Information(this);
                  hardware_information.TopLevel = false;
                  this.Controls.Add(hardware_information);
                  hardware_information.Show();
                  hardware_information.BringToFront();
               }
            };

             // }

            this.Paint += (Me, Objective) => {
              DashCore.RegisterImage(this, Logo, "Profile", 0, 0, 18, 18);
              DashCore.RegisterImage(this, Anubus, "Bar", 18, 0, this.Width, 18);

               this.Anubus.MouseDown += new MouseEventHandler(Move_Window);
               
               this.Logo.MouseEnter += (sender, archive) => { Logo.BackColor = Color.FromArgb(17, 17, 17); };
               this.Network_Information.MouseEnter += (sender, archive) => { Network_Information.BackColor = Color.FromArgb(17, 17, 17); };
               this.Software_Information.MouseEnter += (sender, archive) => { Software_Information.BackColor = Color.FromArgb(17, 17, 17); };
               this.Hardware_Information.MouseEnter += (sender, archive) => { Hardware_Information.BackColor = Color.FromArgb(17, 17, 17); };
               this.About_Application.MouseEnter += (sender, archive) => { About_Application.BackColor = Color.FromArgb(17, 17, 17); };
               this.Minimize_Application.MouseEnter += (sender, archive) => { Minimize_Application.BackColor = Color.FromArgb(17, 17, 17); };
               this.Quit_Application.MouseEnter += (sender, archive) => { Quit_Application.BackColor = Color.FromArgb(17, 17, 17); };

               this.Logo.MouseLeave += (sender, archive) => { Logo.BackColor = Color.FromArgb(7, 7, 7); };
               this.Anubus.MouseLeave += (sender, archive) => { Anubus.BackColor = Color.FromArgb(7, 7, 7); };
               this.Network_Information.MouseLeave += (sender, archive) => { Network_Information.BackColor = Color.FromArgb(7, 7, 7); };
               this.Software_Information.MouseLeave += (sender, archive) => { Software_Information.BackColor = Color.FromArgb(7, 7, 7); };
               this.Hardware_Information.MouseLeave += (sender, archive) => { Hardware_Information.BackColor = Color.FromArgb(7, 7, 7); };
               this.About_Application.MouseLeave += (sender, archive) => { About_Application.BackColor = Color.FromArgb(7, 7, 7); };
               this.Minimize_Application.MouseLeave += (sender, archive) => { Minimize_Application.BackColor = Color.FromArgb(7, 7, 7); };
               this.Quit_Application.MouseLeave += (sender, archive) => { Quit_Application.BackColor = Color.FromArgb(7, 7, 7); };
            };
        }
    }
}
