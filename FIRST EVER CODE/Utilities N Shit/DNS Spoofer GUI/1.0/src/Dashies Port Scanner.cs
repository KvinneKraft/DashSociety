
/* (c) All Rights Reserved, Dashies Software Inc. */

using System;
using System.Net;
using System.Net.Sockets;
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
    public partial class Dashies_Port_Scanner : Form {
       public string resKey, Result = String.Empty, AimFor;
       public bool Allow_Access = true, State = false;
       Dash_Lib DashCore = new Dash_Lib();

       public TextBox FromPort = new TextBox(), 
                      ToPort = new TextBox(),
                      Target = new TextBox(),
                      Status = new TextBox(),
                      Timeout = new TextBox();

       public Button SCAN = new Button(), 
                     Quit = new Button();

        public bool ConnectIt(int Port, string Attack, int Timeout) {
          var Connect = new TcpClient(); 
          var Get = Connect.BeginConnect(Attack, Port, null, null);
           Get.AsyncWaitHandle.WaitOne(TimeSpan.FromMilliseconds(Timeout));
            try {
              if(!Connect.Connected) return false;
              else return true;
            } catch(SocketException) { return false; }
        }

        public void ScanPort(int From, int To, string Target, int Timeout) {
            PingReply DNS_REPLY;
            Ping CheckStatus = new Ping();

            this.AimFor = Target;

			 try {
                DNS_REPLY = CheckStatus.Send(Target);			   
                 if(DNS_REPLY.Status != IPStatus.Success)
                     { Result = "*** Unable to Connect with " + Target + "!"; State = false; }
                 else 
                     { State = true; }
			 } catch(PingException) {
                Result = "*** Unable to Connect with " + Target + "!";
			    State = false;
			 }
             
             if(State == true) {
                 for(int index = From; index <= To; index = index + 1) {
                      if(ConnectIt(index, Target, Timeout) == true)
                        this.Result = Result + "TCP Port: " + index.ToString() + " seems Open!\r\n";
                 }

                 if(this.Result == String.Empty)
                   this.Result = "No Open TCP Ports found!";
             }

             System.IO.File.WriteAllText(@"Log.txt", this.Result);
             System.IO.File.WriteAllText(@"Target.txt", this.AimFor);
             
            this.Result = String.Empty;
            this.AimFor = String.Empty;
             
            this.Update();
        }

		public Dashies_Port_Scanner() {
               InitializeComponent();
                
                resKey = "Pony_Spoofer_GUI.Embeded";
                System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 this.MaximumSize = new Size(300, 200);
                 this.MinimumSize = new Size(300, 200);
                 this.MaximizeBox = false;
                 this.MinimizeBox = false;
                 
                 this.Top             = -50; //new Point(137, 35);
                 this.StartPosition   = FormStartPosition.CenterParent; //(137, 35);
                 this.FormBorderStyle = FormBorderStyle.None;
                 this.Size            = new System.Drawing.Size(300, 200); 
                 
                 this.ControlBox = false;
                 this.BackColor  = Color.FromArgb(30, 40, 56);

                  DashCore.WriteText(this, "Scan From", false, 5, 30, 9, 255, 255, 255);
                  DashCore.WriteText(this, "Until", false, 126, 30, 9, 255, 255, 255);
                  DashCore.WriteText(this, "Timeout", false, 5, 60, 9, 255, 255, 255);
                  DashCore.WriteText(this, "Target", false, 5, 90, 9, 255, 255, 255);   
                  DashCore.WriteText(this, "Status", false, 5, 120, 9, 255, 255, 255);

                  DashCore.SetTextBox(this, FromPort, "1", 73, 30, 50, 24, 11, 5, 13, 18, 25, 255, 255, 255);
                  DashCore.SetTextBox(this, ToPort, "80", 160, 30, 50, 24, 11, 5, 13, 18, 25, 255, 255, 255);
                  DashCore.SetTextBox(this, Timeout, "25", 73, 60, 50, 24, 11, 5, 13, 18, 25, 255, 255, 255);
                  DashCore.SetTextBox(this, Target, "172.217.17.68", 73, 90, 135, 24, 11, 15, 13, 18, 25, 255, 255, 255);      
                  DashCore.SetTextBox(this, Status, "Awaiting Input ....", 73, 120, 168, 35, 11, 100, 13, 18, 25, 255, 255, 255);

                   FromPort.TextChanged += (sender, e) => {
                      string Checksum, Validate;
                       Checksum = FromPort.Text;
                        for(int Index = 0; Index <= FromPort.Text.Length-1; Index = Index + 1) {
                            Validate = Checksum[Index].ToString();
                             if(Validate != "1" && Validate != "2" && Validate != "3" && Validate != "4" && Validate != "5" && Validate != "6" && Validate != "7" && Validate != "8" && Validate != "9" && Validate != "0") {
                                 Allow_Access  = false;
                                 FromPort.Text = "";
                                 
                                 DashCore.ShowToolTip(this, FromPort, "ERROR :", "invalid input received!", 4, 16, 1000, 255, 0, 0, 37, 40, 45);
                             } else Allow_Access = true;
                        }
                   };

                   ToPort.TextChanged += (sender, e) => {
                      string Checksum, Validate;
                       Checksum = ToPort.Text;
                        for(int Index = 0; Index <= ToPort.Text.Length-1; Index = Index + 1) {
                            Validate = Checksum[Index].ToString();
                             if(Validate != "1" && Validate != "2" && Validate != "3" && Validate != "4" && Validate != "5" && Validate != "6" && Validate != "7" && Validate != "8" && Validate != "9" && Validate != "0") {
                                 Allow_Access  = false;
                                 ToPort.Text = "";
                                 
                                 DashCore.ShowToolTip(this, FromPort, "ERROR :", "invalid input received!", 4, 16, 1000, 255, 0, 0, 37, 40, 45);
                             } else Allow_Access = true;
                        }
                   };

                   Timeout.TextChanged += (sender, e) => {
                      string Checksum, Validate;
                       Checksum = Timeout.Text;
                        for(int Index = 0; Index <= Timeout.Text.Length-1; Index = Index + 1) {
                            Validate = Checksum[Index].ToString();
                             if(Validate != "1" && Validate != "2" && Validate != "3" && Validate != "4" && Validate != "5" && Validate != "6" && Validate != "7" && Validate != "8" && Validate != "9" && Validate != "0") {
                                 Allow_Access  = false;
                                 Timeout.Text = "";
                                 
                                 DashCore.ShowToolTip(this, Timeout, "ERROR :", "invalid input received!", 4, 16, 1000, 255, 0, 0, 37, 40, 45);
                             } else Allow_Access = true;
                        }
                   };
                   
                   Target.TextChanged += (sender, e) => {
                      string Checksum;
                       Checksum = Target.Text;
                        if(Checksum == "8.8.8.8") Allow_Access = true; else {
                            if(Checksum.Length > 15) { // Change back to 15 when dun with dis
                                DashCore.ShowToolTip(this, Target, "ERROR :", "too much input received!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
                                Allow_Access = false;
                            } else for(int In = 0; In <= Checksum.Length-1; In = In + 1) {
                                string Conv = Checksum[In].ToString();
                                  if(Conv != "." && Conv != "0" && Conv != "1" && Conv != "2" && Conv != "3" && Conv != "4" && Conv != "5" && Conv != "6" && Conv != "7" && Conv != "8" && Conv != "9") {
                                     Target.Text = "";
                                     Allow_Access = false;

                                     DashCore.ShowToolTip(this, Target, "ERROR :", "invalid input received!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);
                                  } else Allow_Access = true;
                            }
                        }
                   };
                  
                  DashCore.LoadImage(this, "Turp Rurp Durp", 244, 145, 64, 64, 10001);

                  DashCore.CreateButton(this, true, Quit, true, "X", String.Empty, false, 14, 0, 269, 0, 32, 25, 30, 40, 56, 255, 255, 255);
                  DashCore.CreateButton(this, true, SCAN, true, "Scan It", String.Empty, false, 13, 0, 58, 160, 150, 28, 79, 58, 109, 255, 255, 255);
                  
                   Quit.Click += (sender, e) => { this.Close(); };
                    
                  // MessageBox.Show(Int32.Parse(ToPort.Text).ToString());

                   SCAN.Click += (sender, e) => {
                      string TextLup = FromPort.Text.ToString();
                       if(Allow_Access != true) {
                           if(TextLup.Length <= 0 && TextLup.Length <= 0)
                               TextLup = "insufficient input received!";
                           else
                               TextLup = "invalid input received!";

                          DashCore.ShowToolTip(this, FromPort, "ERROR :", TextLup, 0, 16, 1000, 255, 0, 0, 37, 40, 45);
                       } else {
                           if((FromPort.Text.Length <= 0) || (ToPort.Text.Length <= 0))
                               DashCore.ShowToolTip(this, FromPort, "ERROR :", "insufficient input received!", 0, 16, 1000, 255, 0, 0, 37, 40, 45);                           
                           else 
                               { Status.Text = "Scanning Target ...."; ScanPort(Int32.Parse(FromPort.Text), Int32.Parse(ToPort.Text), Target.Text, Int32.Parse(Timeout.Text)); Status.Text = "Results Are Being Shown"; Port_Scan_Results Show_Dialog = new Port_Scan_Results(); Show_Dialog.ShowDialog(); Status.Text = "Awaiting Input ...."; }
                       }
                   };
        }
    }
}
