/*                                                */
/* (c) All Rights Reserved, Dashies Software Inc. */
/*                                                */
/*             GUI DNS Spoofer 1.0 c:             */
/*                                                */



using System;
using System.Management;
using System.Drawing;
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

namespace Pony_Spoofer_GUI
{
    public partial class Startup : Form {
        public ComboBox METHOD = new ComboBox();
        public ListBox METHODS = new ListBox();
        public MenuStrip MainMenu = new MenuStrip();
        public Ping CheckStatus = new Ping();
        
        public TextBox DNS1 = new TextBox(), 
                       DNS2 = new TextBox();

        public ToolStripMenuItem Tools = new ToolStripMenuItem("Tools"), 
                                 Extras  = new ToolStripMenuItem("Extras");

        public Button ChangeDNS = new Button(), 
                      CheckDNS = new Button(),
                      CleanDNS = new Button();
        
        //Access access = new Access();

        public ToolStripMenuItem MExit, MOptions;
        public PingReply DNS_REPLY;
        public Icon App_Icon;

        DNS_Flush_Failure DNS_Flush_Error = new DNS_Flush_Failure();
        DNS_Flush_Done DNS_Flush_Success = new DNS_Flush_Done();

        DNS_Change_Failure DNS_Change_Error = new DNS_Change_Failure();
        DNS_Change_Done DNS_Change_Success = new DNS_Change_Done();

        Functionality_Not_Added_Yet Functionality_Unavailable = new Functionality_Not_Added_Yet();

        Error Send_Error = new Error();
        Dash_Lib DashCore = new Dash_Lib();

        public string SELECTED_METHOD="NONE", 
                      DNS_SERVER1="8.8.8.8", 
                      DNS_SERVER2="8.8.4.4",
                      resKey;

        public bool ALLOW_CHANGE = false;

        // public Image <image>
        // public Button <button>
        
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
       
       [DllImport("dnsapi.dll", EntryPoint="DnsFlushResolverCache")]
       private static extern UInt32 DnsFlushResolverCache();

       [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        
       [DllImport("user32.dll")]
        private static extern bool ReleaseCapture();

        private bool ProperPrivileges() {
               WindowsIdentity identity = WindowsIdentity.GetCurrent();
               WindowsPrincipal principal = new WindowsPrincipal(identity);
               
            if(principal.IsInRole(WindowsBuiltInRole.Administrator) != true)
                return false;
            else
                return true;
        }

        private void MainMenu_MouseDown(object sender, MouseEventArgs e) {
             if(e.Button == MouseButtons.Left) {
                 ReleaseCapture();
                 SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
             }
        }
        
        private void MainMenu_MouseMove(object sender, MouseEventArgs e) {
             if(!Focused)
                   Focus();
        }        
        
        private class MyRenderer : ToolStripProfessionalRenderer { public MyRenderer() : base(new MyColors()) {} }
        
        ///
        /// 
        /// CHANGE DNS SECTION
        /// 
        ///
        
       public async void CheckDNSState(string dns1, string dns2) {
                 if(DNS_SERVER1.Length < 7) {
                      DashCore.ShowToolTip(this, DNS1, "ERROR :", "insufficient input received!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
                 } else {
                       if(DNS_SERVER2.Length < 7) {
                            DashCore.ShowToolTip(this, DNS2, "ERROR :", "insufficient input received!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
                       } else {
					   	     try {
                                   DNS_REPLY = CheckStatus.Send(DNS_SERVER1, 450);
								   
                                  if(DNS_REPLY.Status != IPStatus.Success)
                                      DashCore.ShowToolTip(this, DNS1, "ERROR :", "host seems offline :/", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
                                  else 
                                      DashCore.ShowToolTip(this, DNS1, "ERROR :", "host seems online c:", 0, 16, 1000, 26, 142, 0, 37, 40, 45);
                                
                                   DNS_REPLY = CheckStatus.Send(DNS_SERVER2, 450);
								   
                                  if(DNS_REPLY.Status != IPStatus.Success)
                                      DashCore.ShowToolTip(this, DNS2, "ERROR :", "host seems offline :/", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
                                  else
                                      DashCore.ShowToolTip(this, DNS2, "ERROR :", "host seems online c:", 0, 16, 1000, 26, 142, 0, 37, 40, 45);

                             } catch(PingException) {
								  DashCore.ShowToolTip(this, DNS1, "ERROR :", "ping request failed!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
							      DashCore.ShowToolTip(this, DNS2, "ERROR :", "ping request failed!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
						     }
                       }
                 }       
       }

       private bool ChangeMyDNS(string DNS1, string DNS2, string METHOD) {
              ManagementClass MC = new ManagementClass("Win32_NetworkAdapterConfiguration");
              ManagementObjectCollection MOC = MC.GetInstances();  

               foreach(ManagementObject mo in MOC) {
                   if((bool)mo["IPEnabled"]) {
                      ManagementBaseObject objDNS = mo.GetMethodParameters("SetDNSServerSearchOrder");
                       if(objDNS == null) return false;
                       else { string[] Servers = { DNS1, DNS2 };
                           objDNS["DNSServerSearchOrder"] = Servers;
                           mo.InvokeMethod("SetDNSServerSearchOrder", objDNS, null);
                       }
                   } 
               }

           return true;
       }

       public bool CleanDNSCache() {
            UInt32 Return = DnsFlushResolverCache();
              
           if(Return != 1)
              return false;
            else
              return true;
       }

        /// 
        /// END OF SECTION
        ///  

       public bool Runtime() {
               if(ProperPrivileges() != true) {
                      InsufficientPrivileges showInsufficientPrivDialog = new InsufficientPrivileges();
                      showInsufficientPrivDialog.ShowDialog();

                      this.Close();
               } var Exit = new ToolStripMenuItem("X");

                DashCore.WriteText(this, "DNS 1:", false, 28, 55, 10, 255, 255, 255);
                DashCore.SetTextBox(this, DNS1, "8.8.8.8", 80, 55, 135, 185, 11, 15, 52, 55, 63, 255, 255, 255);
                
                DashCore.WriteText(this, "DNS 2:", false, 28, 78, 10, 255, 255, 255);
                DashCore.SetTextBox(this, DNS2, "8.8.4.4", 80, 78, 135, 185, 11, 15, 52, 55, 63, 255, 255, 255);
               
               if(DNS1.Text == "8.8.8.8") 
                  if(DNS2.Text == "8.8.4.4") 
                       ALLOW_CHANGE = true;
             
               DNS1.TextChanged += (sender, args) => { // ShowToolTip(DNS1, "ERROR :", "invalid input received!", 0, 16, 1000, 255, 255, 255, 37, 40, 45);
                        DNS_SERVER1 = DNS1.Text;
                      
                      if(DNS_SERVER1 == "8.8.8.8") ALLOW_CHANGE = true; else {
                          if(DNS_SERVER1.Length > 15) { // Change back to 15 when dun with dis
                              DashCore.ShowToolTip(this, DNS1, "ERROR :", "too much input received!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
                              ALLOW_CHANGE = false;
                          } else for(int In = 0; In <= DNS_SERVER1.Length-1; In = In + 1) {
                              string Conv = DNS_SERVER1[In].ToString();
                                if(Conv != "." && Conv != "0" && Conv != "1" && Conv != "2" && Conv != "3" && Conv != "4" && Conv != "5" && Conv != "6" && Conv != "7" && Conv != "8" && Conv != "9") {
                                   DashCore.ShowToolTip(this, DNS1, "ERROR :", "invalid input received!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
                                   DNS1.Text = "";
                                   ALLOW_CHANGE = false;
                                } else ALLOW_CHANGE = true;
                          }
                      }
               };

               DNS2.TextChanged += (sender, args) => {
                        DNS_SERVER2 = DNS2.Text;

                   if(DNS_SERVER2 == "8.8.4.4") ALLOW_CHANGE = true; else {
                       if (DNS_SERVER2.Length > 15) { // Change back to 15 when dun with dis.
                           DashCore.ShowToolTip(this, DNS2, "ERROR :", "too much input received!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
                           ALLOW_CHANGE = false;
                       } else for(int In = 0; In <= DNS_SERVER2.Length-1; In = In + 1) {
                           string Conv = DNS_SERVER2[In].ToString();
                             if(Conv != "." && Conv != "0" && Conv != "1" && Conv != "2" && Conv != "3" && Conv != "4" && Conv != "5" && Conv != "6" && Conv != "7" && Conv != "8" && Conv != "9") {
                                DashCore.ShowToolTip(this, DNS2, "ERROR :", "invalid input received!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
                                DNS2.Text = "";
                                ALLOW_CHANGE = false;
                             } else ALLOW_CHANGE = true;
                       }
                   }
               };

                DashCore.WriteText(this, "METHODS:", false, 240, 28, 10, 255, 255, 255);
                DashCore.SetListBox(this, METHODS, 230, 45, 100, 65, 52, 55, 63, 255, 255, 255); 

               METHODS.Items.Add("Full"); 
               METHODS.Items.Add("Necessary");
               METHODS.Items.Add("Secure");
               METHODS.Items.Add("Default");

               METHODS.SelectedIndexChanged += (sender, args) => {
                   if((String)METHODS.SelectedItem == "Full") {
                       SELECTED_METHOD = "FULL";
                   } else
                   if((String)METHODS.SelectedItem == "Secure") {
                       SELECTED_METHOD = "SECURE";
                   } else
                   if((String)METHODS.SelectedItem == "Necessary") {
                       SELECTED_METHOD = "NECESSARY";
                   } else
                   if((String)METHODS.SelectedItem == "Default") {
                       SELECTED_METHOD = "DEFAULT";
                   } else
                       SELECTED_METHOD = "NONE";

                   /* Currently Not Supported */

                     Functionality_Unavailable.ShowDialog();

                   /* -- END -- */
               };

                DashCore.CreateSubMenu(this, Tools, MainMenu, "Tools", "Tools", false, false, false, String.Empty, 0, 0, 9, 81, 26, -6, -2, 37, 0, 63, 255, 255, 255); // 0, 0
                DashCore.CreateSubMenu(this, Extras, MainMenu, "Extras", "Extras", false, false, false, String.Empty, 0, 0, 9, 81, 26, 5, -2, 37, 0, 63, 255, 255, 255);
                DashCore.CreateSubMenu(this, Exit, MainMenu, "X", "X", false, false, false, String.Empty, 0, 0, 18, 36, 30, 326, -26, 37, 0, 63, 255, 255, 255);
               
               var ViewAvailableDNS = Tools.DropDownItems.Add("DNS Servers");
               var PortScanner      = Tools.DropDownItems.Add("Port Scanner");

               // Tools.DropDownItems.Add(new ToolStripSeparator());

               var Website   = Extras.DropDownItems.Add("Website");
               var Copyright = Extras.DropDownItems.Add("Copyright");
               var About     = Extras.DropDownItems.Add("About");

               Extras.DropDownItems.Add(new ToolStripSeparator()); //"-"); //3, new ToolStripSeparator());

               var Donate    = Extras.DropDownItems.Add("Donate");
               var TOS       = Extras.DropDownItems.Add("T.O.S");                  
              
                DashCore.SetMenuItem(ViewAvailableDNS, 180, 25, true, 9, 37, 40, 45, 255, 255, 255);
                DashCore.SetMenuItem(PortScanner, 100, 25, true, 9, 37, 40, 45, 255, 255, 255);
                
                DashCore.SetMenuItem(Copyright, 100, 25, true, 9, 37, 40, 45, 255, 255, 255); 
                DashCore.SetMenuItem(Website, 100, 25, true, 9, 37, 40, 45, 255, 255, 255);
                DashCore.SetMenuItem(Donate, 100, 25, true, 9, 37, 40, 45, 255, 255, 255);                
                DashCore.SetMenuItem(About, 100, 25, true, 9, 37, 40, 45, 255, 255, 255);
                DashCore.SetMenuItem(TOS, 100, 25, true, 9, 37, 40, 45, 255, 255, 255);
                
                 CheckDNS.MouseEnter += (sender, e) => { CheckDNS.UseVisualStyleBackColor=false; DashCore.ColorButton(CheckDNS, 90, 36, 91, 255, 255, 255); };
                 CheckDNS.MouseLeave += (sender, e) => { CheckDNS.UseVisualStyleBackColor=true; DashCore.ColorButton(CheckDNS, 62, 12, 73, 255, 255, 255); };
                 
                 ChangeDNS.MouseEnter += (sender, e) => { ChangeDNS.UseVisualStyleBackColor=false; DashCore.ColorButton(ChangeDNS, 90, 36, 91, 255, 255, 255); };
                 ChangeDNS.MouseLeave += (sender, e) => { ChangeDNS.UseVisualStyleBackColor=true; DashCore.ColorButton(ChangeDNS, 62, 12, 73, 255, 255, 255); };
                 
                 CleanDNS.MouseEnter += (sender, e) => { CleanDNS.UseVisualStyleBackColor=false; DashCore.ColorButton(CleanDNS, 90, 36, 91, 255, 255, 255); };
                 CleanDNS.MouseLeave += (sender, e) => { CleanDNS.UseVisualStyleBackColor=true; DashCore.ColorButton(CleanDNS, 62, 12, 73, 255, 255, 255); };                

                DashCore.CreateButton(this, true, CheckDNS, true, "DNS Status", String.Empty, true, 8, 0, 21, 130, 100, 28, 62, 12, 73, 255, 255, 255);
                DashCore.CreateButton(this, true, CleanDNS, true, "Clean DNS", String.Empty, true, 8, 0, 131, 130, 100, 28, 62, 12, 73, 255, 255, 255);
                DashCore.CreateButton(this, true, ChangeDNS, true, "Change DNS", String.Empty, true, 8, 0, 241, 130, 100, 28, 62, 12, 73, 255, 255, 255);

                 Exit.Click      += (sender, args) => { this.Close(); };
                 Copyright.Click += (sender, args) => { Copyright_Dialog showCopyright = new Copyright_Dialog(); showCopyright.ShowDialog(); };
                 Website.Click   += (sender, args) => { Website_Dialog showWebsite = new Website_Dialog(); showWebsite.ShowDialog(); };
                 Donate.Click    += (sender, args) => { Donate_Dialog showDonate = new Donate_Dialog(); showDonate.ShowDialog(); };
                 About.Click     += (sender, args) => { About_Dialog showAbout = new About_Dialog(); showAbout.ShowDialog(); };
                 TOS.Click       += (sender, args) => { Terms_Of_Service showTOS = new Terms_Of_Service(); showTOS.ShowDialog(); };
                 
                 CheckDNS.Click += (sender, args) => { CheckDNSState(DNS_SERVER1, DNS_SERVER2); };

                 ChangeDNS.Click += (sender, args) => {
                      if(DNS1.Text.Length < 7)
                         DashCore.ShowToolTip(this, DNS1, "ERROR : ", "insufficient input received!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);

                      if(DNS2.Text.Length < 7)
                         DashCore.ShowToolTip(this, DNS2, "ERROR : ", "insufficient input received!", 0, 16, 100, 255, 0, 0, 37, 40, 45);

                      if(ALLOW_CHANGE == true) {
                          if(SELECTED_METHOD == "NONE") SELECTED_METHOD = "DEFAULT";
                          else if(ChangeMyDNS(DNS_SERVER1, DNS_SERVER2, SELECTED_METHOD) != true)
                              DNS_Change_Error.ShowDialog();
                          else
                              DNS_Change_Success.ShowDialog();
                      }
                 };

                 CleanDNS.Click += (sender, args) => {
                        if(CleanDNSCache() != true)
                            DNS_Flush_Error.ShowDialog();
                        else
                            DNS_Flush_Success.ShowDialog();

                     return ;
                 };
                 
                ViewAvailableDNS.Click += (sender, e) => { Confirm_DNS DNS_Confirmation = new Confirm_DNS(); DNS_Confirmation.ShowDialog(); };
                PortScanner.Click += (sender, e) => { Dashies_Port_Scanner PortScanner_Dialog = new Dashies_Port_Scanner(); PortScanner_Dialog.ShowDialog(); };

                MainMenu.MouseDown += new MouseEventHandler(MainMenu_MouseDown); //(sender, arg) => {  };
                MainMenu.MouseMove += new MouseEventHandler(MainMenu_MouseMove);
                
                DashCore.WriteText(this, "this DNS Spoofer sadly currently only\nworks for Multi-IPv4 Addresses, this\nwill be updated in the next release!", false, 85, 180, 9, 255, 255, 255);
                DashCore.WriteText(this, "version 1.0 - beta", false, 275, 742, 5, 255, 255, 255); 
                 
           return true;
       }
        
       public void EXIT_APPLICATION(object sender, EventArgs e) { this.Close(); }
        
       private class MyColors : ProfessionalColorTable {
            public MyColors() {
                base.UseSystemColors = false;
            }

            public override Color ImageMarginGradientBegin {
                get { return Color.FromArgb(22, 23, 35); }
            }
 
            public override Color ImageMarginGradientMiddle {
                get { return Color.FromArgb(22, 23, 35); }
            }

            public override Color ImageMarginGradientEnd {
                get { return Color.FromArgb(22, 23, 35); }
            }
    
            public override Color ToolStripDropDownBackground {
                get { return Color.FromArgb(22, 23, 35); }
            }

            public override Color MenuBorder {
                get { return Color.Transparent; }
            }  

            public override Color MenuItemBorder {
                get { return Color.Transparent; }
            }        	      	       	
        	
            public override Color MenuItemSelected {
                get { return Color.FromArgb(64, 74, 91); }
            }

            public override Color MenuItemSelectedGradientBegin {
                get { return Color.FromArgb(64, 74, 91); }
            }
            
            public override Color MenuItemSelectedGradientEnd {
                get { return Color.FromArgb(64, 74, 91); }
            }
           
            public override Color MenuStripGradientBegin {
                get { return Color.FromArgb(37, 40, 45); }
            }
        	
        	public override Color MenuStripGradientEnd {
                get { return Color.FromArgb(37, 40, 45); }
            }

            public override Color MenuItemPressedGradientBegin {
                get { return Color.FromArgb(22, 23, 35); }
            }
  
            public override Color MenuItemPressedGradientEnd {
                get { return Color.FromArgb(22, 23, 35); }
            }

            public override Color SeparatorDark {
                get { return Color.FromArgb(37, 40, 45); }
            }

            public override Color SeparatorLight {
                get { return Color.FromArgb(37, 40, 45); }
            }
        }

        public Startup() {
               InitializeComponent(); //new MyColors();    

                 MainMenu = new MenuStrip();
                 resKey = "Pony_Spoofer_GUI.Embeded";
                 System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                 MainMenu.Renderer = new ToolStripProfessionalRenderer(new MyColors());

                DashCore.CreateMenu(this, MainMenu, "MainMenu", 37, 40, 45); //130, 42, 60);
                
                DashCore.LoadImage(this, "Bottom Left", -20, 200, 64, 64, 8000);
                DashCore.LoadImage(this, "Bottom Right", 300, 200, 64, 64, 8000);
               
               this.MaximumSize = new Size(365, 285);
               this.MinimumSize = new Size(365, 285);
               this.MaximizeBox = false;
               this.MinimizeBox = false; 
               
               this.StartPosition = FormStartPosition.CenterScreen;
               this.Icon   = (Icon) loadRes.GetObject("Application_Icon"); //App_Icon;
               
               this.FormBorderStyle = FormBorderStyle.None; 
               this.DoubleBuffered  = false;
               this.BackColor = System.Drawing.Color.FromArgb(52, 68, 94);
               this.Size      = new System.Drawing.Size(365, 285);
               this.Update();

           Runtime();
        }
        
        void StartupLoad(object sender, EventArgs e) { }
    }
}