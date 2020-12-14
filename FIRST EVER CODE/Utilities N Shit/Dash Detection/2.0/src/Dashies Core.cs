
/* 

(c) All Rights Reserved, Dashies Software Inc.

Latest Dash Library (Version 2.0)
 
* Yeh Yeh, I know I have made this code pretty messy, excuse me for it lol.
* 
* This style of programming is my own personal preference, so I do not give a living shit about your opinion lol. 
* - Dashie

*/

using System;
using Microsoft.Win32;
using System.Drawing;
using System.Data;
using System.Data.Sql;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Linq;
using Microsoft.VisualBasic.Devices;
using System.IO;
using System.Net;
using System.Web;
using System.Net.Sockets;
using System.Resources;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace src {
    class core {
        public string Resource_Key = ("src.Embeded"),
                      IPAddress;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImport("dnsapi.dll", EntryPoint="DnsFlushRevolverCache")]
        private static extern UInt32 DnsFlushResolverCache();

        // Dashies Standard Definition Table //

        public int DEFAULT_FONT_WEIGHT = (400),
                   DEFAULT_MINIMUM_WINDOW_HEIGHT = (500),
                   DEFAULT_MAXIMUM_WINDOW_HEIGHT = (500),
                   DEFAULT_MAXIMUM_WINDOW_WIDTH = (400),
                   DEFAULT_MINIMUM_WINDOW_WIDTH = (400),
                   DEFAULT_WINDOW_WIDTH = (400),
                   DEFAULT_WINDOW_HEIGHT = (500);
        
        public string DEFAULT_FONT_TYPE = ("Consolas"),
                      DOTUM_FONT_TYPE = ("Dotum"),
                      Contain_MAC = String.Empty;
        
        public string DEFAULT_FILTER_SET = ("Normal Text File|*.txt|DashCore File|*.dcore|DashText File|*.dtxt|DashSource File|*.dsrc|Anything|*.*"),
                      ANYTHING_FILTER_SET = ("Anything|*.*");

        // End Of Dashies Standard Definition Table //
          
          private bool Privilege_Available() {
               WindowsIdentity identity = WindowsIdentity.GetCurrent();
               WindowsPrincipal principal = new WindowsPrincipal(identity);
               
                if(principal.IsInRole(WindowsBuiltInRole.Administrator) != true)
                  return false;

             return true;
          }
          
          public Boolean SetWindowProperties(Form imply, int x, int y, int Width, int Height, String Icon, int r, int g, int b) {
               imply.Size = new Size(Width, Height);
               imply.Location = new Point(x, y);
               imply.FormBorderStyle = FormBorderStyle.None;
               imply.BackColor = Color.FromArgb(r, g, b);  
             
                SetIcon(imply, Icon);

             return true;
          }

          public String GetWindowsEdition() {
             IPAddress = String.Empty;
             
             OperatingSystem OS = Environment.OSVersion;
             Version VS = OS.Version;
             String RegValue = String.Empty;

              if(OS.Platform == PlatformID.Win32Windows) {
                  switch(VS.Minor) {
                    case 0:{
                      IPAddress = ("95");
                     break;       
                    }  

                    case 10:{
                        if(VS.Revision.ToString() == "2222A") IPAddress = ("98se");
                        else IPAddress = ("98");
                     break;        
                    }

                    case 90:{
                      IPAddress = ("ME");
                     break;        
                    }
                  }
              } else if(OS.Platform == PlatformID.Win32NT) {
                  switch(VS.Major) {
                    case 3:{
                      IPAddress = ("NT 3.51");
                     break;       
                    }
                    
                    case 4:{
                      IPAddress = ("NT 4.0");
                     break;        
                    }
                    
                    case 5:{
                        if(VS.Minor == 0) IPAddress = ("2000");
                        else IPAddress = ("XP");
                     break;        
                    }

                    case 6:{
                        if(VS.Minor == 0) IPAddress = ("Hasta La Vista");
                        else if(VS.Minor == 1) IPAddress = ("7");
                        else if(VS.Minor == 2) IPAddress = ("8");
                        else IPAddress = ("8.1");
                     break;        
                    }

                    case 10:{
                        IPAddress = ("10");
                     break;        
                    }

                    default:
                        IPAddress = ("Unknown");
                     break;
                  }
              } RegValue = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ProductName", "").ToString();

             RegValue = (RegValue.Replace("Windows "+IPAddress+" ", ""));

              if(RegValue != String.Empty) IPAddress += (" ") + (RegValue);

              if(IPAddress != String.Empty) {
                 IPAddress = ("Windows ") + IPAddress;
                  if(OS.ServicePack != String.Empty) { 
                     IPAddress += (" ") + OS.ServicePack;
                      if(Environment.Is64BitOperatingSystem == true)
                        IPAddress += (" 64-bit");
                      else
                        IPAddress += (" 32-bit");
                  }
              } RegValue = Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "ReleaseId", "").ToString();

              if(RegValue != "") IPAddress += (" Build ") + (RegValue);
              else {
                 RegValue = (Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", "CurrentBuild", "").ToString());
                  if(RegValue != String.Empty) IPAddress += (" Build ") + (RegValue);
                  else IPAddress += (" Build Unknown ");
              }
                     
            return IPAddress;  
          }
          
          public String GetRegistryValue(int id) {
             IPAddress = String.Empty;
             String RegValue = String.Empty; 

               if(id == 1) RegValue = ("CSDBuildNumber");
               else if(id == 2) RegValue = ("BuildLabEx");
               else if(id == 3) RegValue = ("BuildLab");
               else if(id == 4) RegValue = ("BuildGUID");
               else if(id == 5) RegValue = ("ProductId");
               else if(id == 6) RegValue = ("RegisteredOwner");
              
              RegValue = (Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion", (string)RegValue, "").ToString());      
               
               if((RegValue == String.Empty) || (RegValue == ""))
                   IPAddress = ("Unknown");
               else
                   IPAddress = (RegValue);

            return IPAddress;  
          }

          public String GetWindowsActivationStatus() {
             IPAddress = String.Empty;

            return IPAddress;  
          }

          public String GetWindowsSerialKey() {
             IPAddress = String.Empty;
             String RegValue = (Registry.GetValue("HKEY_LOCAL_MACHINE\\SOFTWARE\\Microsoft\\Windows NT\\CurrentVersion\\DefaultProductKey", "ProductId", "").ToString()); 
              
              if((RegValue == String.Empty) || (RegValue == ""))
                  IPAddress = ("Unknown");
              else
                  IPAddress = (RegValue);

            return IPAddress;  
          }

          public String GetNames(int Id) {
             IPAddress = String.Empty;
              
              if(Id == 1) IPAddress = Environment.UserName; 
              if(Id == 2) IPAddress = Environment.MachineName;

              if(IPAddress.Length <= 0) IPAddress = ("Unknown");

            return IPAddress;
          }

          public String GetWindowsComputerDescription() {
             IPAddress = String.Empty;
              
             using (ManagementClass mc = new ManagementClass("Win32_OperatingSystem"))
             using (ManagementObjectCollection moc = mc.GetInstances()) {
                foreach(ManagementObject mo in moc) {
                   if(mo.Properties["Description"] != null) {
                       IPAddress = mo.Properties["Description"].Value.ToString();
                     break;    
                   }   
                }  
             }

            return IPAddress;  
          }
          
          public String GetWindowsPassword() {
             IPAddress = String.Empty;

              if(IPAddress.Length <= 0) IPAddress = ("Unknown");

            return IPAddress;  
          }
          
          public String GetRAM(int Id) {
             IPAddress = String.Empty; //new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory.ToString(); //String.Empty;
             
             ComputerInfo CI = new ComputerInfo();
             int Type = 0; 

              if(Id == 1) IPAddress = (CI.TotalPhysicalMemory/1048576).ToString(); //(new ComputerInfo().TotalPhysicalMemory.ToString());
              if(Id == 2) IPAddress = ((CI.TotalVirtualMemory/1048576)/1000).ToString(); //(new ComputerInfo().TotalVirtualMemory.ToString());
              
              if((Id == 3)) {
                  Type = 0;
                
                  ConnectionOptions connection = new ConnectionOptions();
                  connection.Impersonation = ImpersonationLevel.Impersonate;

                  ManagementScope scope = new ManagementScope("\\\\.\\root\\CIMV2", connection);
                  scope.Connect();
                 
                  ObjectQuery query = new ObjectQuery("SELECT * FROM Win32_PhysicalMemory");
                  ManagementObjectSearcher searcher = new ManagementObjectSearcher(scope, query);

                  foreach(ManagementObject queryObj in searcher.Get()) Type = Convert.ToInt32(queryObj["MemoryType"]);

                  switch(Type) {
                     case 0x0: IPAddress = "Unknown"; break;
                     case 0x1: IPAddress = "Other"; break;
                     case 0x2: IPAddress = "DRAM"; break;
                     case 0x3: IPAddress = "Synchronous DRAM"; break;
                     case 0x4: IPAddress = "Cache DRAM"; break;
                     case 0x5: IPAddress = "EDO"; break;
                     case 0x6: IPAddress = "EDRAM"; break;
                     case 0x7: IPAddress = "VRAM"; break;
                     case 0x8: IPAddress = "SRAM"; break;
                     case 0x9: IPAddress = "RAM"; break;
                     case 0xa: IPAddress = "ROM"; break;
                     case 0xb: IPAddress = "Flash"; break;
                     case 0xc: IPAddress = "EEPROM"; break;
                     case 0xd: IPAddress = "FEPROM"; break;
                     case 0xe: IPAddress = "EPROM"; break;
                     case 0xf: IPAddress = "CDRAM"; break;
                     case 0x10: IPAddress = "3DRAM"; break;
                     case 0x11: IPAddress = "SDRAM"; break;
                     case 0x12: IPAddress = "SGRAM"; break;
                     case 0x13: IPAddress = "RDRAM"; break;
                     case 0x14: IPAddress = "DDR"; break;
                     case 0x15: IPAddress = "DDR2"; break;
                     case 0x16: IPAddress = "DDR2 FB-DIMM"; break;
                     case 0x17: IPAddress = "Undefined 23"; break;
                     case 0x18: IPAddress = "DDR3"; break;
                     case 0x19: IPAddress = "FBD2"; break;

                     default: IPAddress = "Undefined"; break;
                  }
              }

              if(IPAddress.Length <= 0) IPAddress = (" Unknown");

            return IPAddress;  
          }

          public String GetSystemCPU() {
             IPAddress = String.Empty;
             
             ManagementObjectSearcher myProcessor = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

              foreach(ManagementObject moProcessor in myProcessor.Get()) {
                 if(moProcessor["name"] != null) {
                    IPAddress = moProcessor["name"].ToString();
                 }
              }

            return IPAddress;  
          }
          
          public String GetSystemGPU() {
             IPAddress = String.Empty;

             ManagementObjectSearcher myGraphicCard = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

              foreach(ManagementObject moGpu in myGraphicCard.Get()) {
                 if(moGpu["Name"] != null) {
                     IPAddress = moGpu["Name"].ToString();
                 }

                 if(IPAddress != String.Empty) {
                    if(moGpu["DriverVersion"] != null) {
                        IPAddress += (" ") + moGpu["DriverVersion"].ToString();
                    }

                    if(moGpu["AdapterRam"] != null) {
                        IPAddress += (" @ ") + (Convert.ToInt32(GetRAM(2))/1000).ToString() + (" GB / ") + GetRAM(2) + (" MB / ") + (Convert.ToInt32(GetRAM(2))*1024).ToString() + (" KB");
                    }    
                 }
              }

            return IPAddress;  
          }
          
          public String LoadProcessData(String Command) {
             IPAddress = String.Empty;

             Process eCmd = new Process();

              eCmd.StartInfo.FileName = ("cmd.exe");
              eCmd.StartInfo.RedirectStandardInput = true;
              eCmd.StartInfo.RedirectStandardOutput = true;
              eCmd.StartInfo.CreateNoWindow = true;
              eCmd.StartInfo.UseShellExecute = false;
              eCmd.Start();

              eCmd.StandardInput.WriteLine(Command);
              eCmd.StandardInput.Flush();
              eCmd.StandardInput.Close();
              eCmd.WaitForExit();

              IPAddress = eCmd.StandardOutput.ReadToEnd();

            return IPAddress;
          }

          public String GetDNSPrefix() {
             IPAddress = String.Empty;
             NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces(); 

              foreach(NetworkInterface Interface in Interfaces) {
                IPInterfaceProperties Properties = Interface.GetIPProperties();
                 if(Properties.DnsSuffix.ToString() != String.Empty)
                    if(IPAddress != String.Empty) IPAddress = IPAddress + ("\r\n") + (Properties.DnsSuffix);
                    else IPAddress = IPAddress + (Properties.DnsSuffix);
              }

             IPAddress = ("\r\n") + IPAddress;

            return IPAddress;
          }

          public String GetSubnetmask() {
             IPAddress = String.Empty;
             NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces();

              foreach(NetworkInterface Interface in Interfaces) {
                 if(Interface.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                     continue;
                
                UnicastIPAddressInformationCollection UnicastIPInfoCol = Interface.GetIPProperties().UnicastAddresses;
                 
                 foreach(UnicastIPAddressInformation UnicatIPInfo in UnicastIPInfoCol)
                    if(UnicatIPInfo.IPv4Mask.ToString() != "0.0.0.0")
                       IPAddress = IPAddress + (UnicatIPInfo.IPv4Mask) + ("\r\n");
              }

             IPAddress = ("\r\n") + IPAddress;

            return IPAddress;
          }
          
          public String GetAdapterNames() {
             IPAddress = String.Empty;
             NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces();
              
              foreach(NetworkInterface Interface in Interfaces)
                 if(Interface.NetworkInterfaceType == NetworkInterfaceType.Loopback)
                     continue;
                 else 
                     if(IPAddress == String.Empty) IPAddress = (Interface.Description) + ("\r\n");
                     else IPAddress = IPAddress + (Interface.Description) + ("\r\n");

             IPAddress = ("\r\n") + IPAddress;

            return IPAddress;
          }

          public String GetDNS() {
             IPAddress = String.Empty;
             NetworkInterface[] Interfaces = NetworkInterface.GetAllNetworkInterfaces(); 

              foreach(NetworkInterface Interface in Interfaces) {
                IPInterfaceProperties Properties = Interface.GetIPProperties();
                IPAddressCollection DNS = Properties.DnsAddresses;

                 foreach(IPAddress dnsAddress in DNS)
                   if(IPAddress == String.Empty) IPAddress = IPAddress + (dnsAddress);
                   else IPAddress = IPAddress + (dnsAddress) + ("\r\n");
              } IPAddress = ("\r\n") + IPAddress;

            return IPAddress;
          }

          public String GetMachineIP() {
              IPHostEntry Host = default(IPHostEntry);
              string Hostname = Environment.MachineName;
              
              IPAddress = (String.Empty);
              Host = Dns.GetHostEntry(Hostname);
              
               foreach(IPAddress IP in Host.AddressList)
                  if(IP.AddressFamily == AddressFamily.InterNetwork)
                      IPAddress = IPAddress + Convert.ToString(IP) + ("\r\n");

               IPAddress = ("\r\n") + IPAddress;

            return IPAddress;  
          }
          
          public String ReadResultOf(String Url, int Timeout) {
             try {
                WebRequest request = WebRequest.Create(Url);
                HttpWebResponse response;
                Stream responseData;
                StreamReader reader;

                 IPAddress = String.Empty;  
             
                 request.Credentials = CredentialCache.DefaultCredentials;
                 request.Timeout = Timeout;
                 request.Proxy = null;

                 response = (HttpWebResponse)request.GetResponse();
                 responseData = response.GetResponseStream();
               
                 reader = new StreamReader(responseData);
               
                 IPAddress = reader.ReadToEnd();
              
                reader.Close();
                responseData.Close();
                response.Close();
              
              if((IPAddress == String.Empty) | (IPAddress == "")) IPAddress = "Unknown";
             } catch (Exception E) {
                IPAddress = ("an error occurred, error saved to : \"Error Log.txt\"");
                
                if(File.Exists("Error Log.txt") != true) {
                    MakeFile("Error Log.txt");
                    WriteToFile(E.ToString(), "Error Log.txt");
                } else {
                    WriteToFile(E.ToString(), "Error Log.txt");
                }
             }

            return IPAddress;
          }

          public Boolean CheckWebsite(string Url) {
             var Ping = new Ping(); 
             
             try {
                if(Ping.Send(Url, 250).Status != IPStatus.Success) return false;
                else return true;
             } catch {
                return false;   
             }
          }
          
          public Boolean StartProcess(String Process) {
              Process Target = new Process();
              ProcessStartInfo TargetInfo = new ProcessStartInfo();
             
              TargetInfo.UseShellExecute = true;
              TargetInfo.CreateNoWindow = true;
              TargetInfo.WorkingDirectory = "\\Dash Server";
              TargetInfo.FileName = Process;

              Target.StartInfo = TargetInfo;

               try 
                {
                 Target.Start();
                } 
               catch 
                {
                 return false;
                }

            return true;  
          }

          public String StartServer() {
              if(Directory.Exists("Dash Server") != true) return ("The Directory \"Dash Server\\\" is missing from your installation.");
              if(File.Exists("Dash Server\\Dash Server.exe") != true) return ("\"Dash Server.exe\" is missing from your installation.");
              if(StartProcess("Dash Server\\Dash Server.exe") != true) return ("Unable to Start HTTP Server on Port 8080.");

              else return String.Empty;
          }
        
          public Boolean KillProcess(String Prrocess) {
               foreach(var Target in Process.GetProcessesByName(Prrocess)) {
                  Target.Kill();
               }

            return true;
          }

          public String KillServer() {
              if(KillProcess("Dash Server") != true) {
                MessageBox.Show("Unable to kill HTTP Server on Port 8080!", "[ERROR]");
                return ("-.-");
              } else return String.Empty;
          }

        /*
          public String GetNetworkIP() {
             IPAddress = String.Empty;

               if(System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() == true) {
                  if(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null) 
                    IPAddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
                  else
                  if(HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"] != null && HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"].Length != 0) 
                    IPAddress = HttpContext.Current.Request.ServerVariables["HTTP_CLIENT_IP"].ToString();
                  else
                  if(HttpContext.Current.Request.UserHostAddress.Length != 0) 
                    IPAddress = HttpContext.Current.Request.UserHostName;
                  else IPAddress = ("Unknown");

               } else IPAddress = "NO Internet Connection Available.";

            return IPAddress;  
          }
          */

          public String GetMacID() {
               IPAddress = (String.Empty);
               StringBuilder Sr;   

                foreach (NetworkInterface NIC in NetworkInterface.GetAllNetworkInterfaces()) {
                    Contain_MAC = String.Empty;
                    Contain_MAC = NIC.GetPhysicalAddress().ToString();
                    Sr = new StringBuilder(Contain_MAC); // Fix the Reader Function    

                     if(Contain_MAC.Length == 12) {

                         for(int Index = 2; Index <= 14; Index = Index + 3) {
                           Sr.Insert(Index, "-");
                         }

                       Contain_MAC = Sr.ToString();
                         
                         if(IPAddress != String.Empty) {
                            IPAddress = IPAddress + (", ") + Contain_MAC;
                         } else {
                            IPAddress = Contain_MAC;
                         }                 
                     }// else IPAddress = IPAddress + ("<Unassigned Device> 00-00-00-00-00-00") + ("\n");
                }

                if(IPAddress == String.Empty)
                    IPAddress = ("None to be Found!");
                else IPAddress = ("\r\n") + IPAddress;

            return IPAddress;  
          }

          public bool ShowToolTip(Form Imply, TextBox textBox, string Tag, string Text, int X, int Y, int Delay, int fr, int fg, int fb, int br, int bg, int bb) {
              ToolTip Target = new ToolTip();
               
               Target.Hide(textBox);   

               Target.BackColor = Color.FromArgb((br), (bg), (bb));
               Target.ForeColor = Color.FromArgb((fr), (fg), (fb));
               
               Target.OwnerDraw = true;
               Target.UseFading = true;
               Target.IsBalloon = false;
               Target.ShowAlways = true;   

               Target.InitialDelay = 0;
               Target.Show((Text), (textBox), (X), (Y), (Delay));
              
               Target.Draw += (sender, magicificant) => {
                   magicificant.DrawBackground();
                   magicificant.DrawText();
               };
            
            return true;
          }
       
          public Boolean AddListItem(ListView Target, String Type, String Value) {
              string[] Items = new string[4];
              ListViewItem Item;

              Items[0] = (Type);
              Items[1] = (Value);   

              Item = new ListViewItem(Items);
              Target.Items.Add(Item);
            return true;  
          }

          public bool Draw_Rectangle(Form Imply, int Thiccness, int X, int Y, int Width, int Height, int R, int G, int B) {
              Pen Brushner = new Pen(Color.FromArgb(R, G, B));
              Graphics Graphichy = Imply.CreateGraphics();
              
               Brushner.Width = Thiccness;

               Graphichy.DrawRectangle(Brushner, new Rectangle(X, Y, Width, Height));   
               Graphichy.BeginContainer();
               Graphichy.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

               Brushner.Dispose();
               Graphichy.Dispose();

            return true;
          }

          public bool SetIcon(Form Target, string MyIcon) {
              ResourceManager Resource_Loader = new ResourceManager((Resource_Key), Assembly.GetExecutingAssembly());
               Target.Icon = (Icon) Resource_Loader.GetObject(MyIcon);
            
            return true;
          }

          public bool ColorButton(Button Target, int fr, int fg, int fb, int br, int bg, int bb) {
               Target.BackColor = Color.FromArgb(br, bg, bb);
               Target.ForeColor = Color.FromArgb(fr, fg, fb);

               Target.FlatAppearance.BorderColor = Color.FromArgb(br, bg, bb);
               Target.FlatAppearance.BorderSize = (0);

            return true;  
          }

          public bool SetListBox(Form Imply, ListBox Target, int X, int Y, int Width, int Height, int fr, int fg, int fb, int br, int bg, int bb) { 
               Target.BackColor = Color.FromArgb(br, bg, bb);
               Target.ForeColor = Color.FromArgb(fr, fg, fb);
              
               Target.Location = new Point(X, Y);

               Target.Size = new Size(Width, Height);
               Target.Font = new Font((DOTUM_FONT_TYPE), 12);   

               Target.BorderStyle = BorderStyle.None;   
              
              Imply.Controls.Add(Target);
            return true;
          }
          
          public bool SetComboBox(Form Imply, ComboBox Target, string Text, string Font_Type, int Font_Size, int X, int Y, int Width, int Height, int fr, int fg, int fb, int br, int bg, int bb) {
               Target.DrawItem += (sender, magnificant) => {
                 magnificant.DrawBackground();
                  if(magnificant.Index == 1 && (magnificant.State & DrawItemState.Selected) == DrawItemState.Selected)
                       magnificant.Graphics.DrawString(Target.Items[magnificant.Index].ToString(), Target.Font, Brushes.Red, magnificant.Bounds);
                  else
                       magnificant.Graphics.DrawString(Target.Items[magnificant.Index].ToString(), Target.Font, Brushes.Black, magnificant.Bounds);
               };

               Target.BackColor = Color.FromArgb(br, bg, bb);
               Target.ForeColor = Color.FromArgb(fr, fg, fb);
               
               Target.Font = new Font((Font_Type), (Font_Size), FontStyle.Italic);
               Target.Location = new Point(X, Y);
               Target.Text = (Text);

               Target.DrawMode = DrawMode.OwnerDrawFixed;
               Target.FlatStyle = FlatStyle.Flat;
               
               Target.Width = Width;
               Target.Height = Height;
               
               Target.Resize += (sender, mystical) => {
                  if(!Target.IsHandleCreated) return ;
                  else Target.BeginInvoke(new Action(() => Target.SelectionLength = 0));
               };
              
              Imply.Controls.Add(Target);
            return true;  
          } 
          
          public bool InsertTextIntoContainer(Form Imply, Panel Target, string Text, string Font_Type, int Font_Size, int X, int Y, int Width, int Height, int fr, int fg, int fb, int br, int bg, int bb) {
              TextBox Insert = new TextBox();
               
               Insert.BackColor = Color.FromArgb(br, bg, bb);
               Insert.ForeColor = Color.FromArgb(fr, fg, fb);
               
               Insert.Width = (Imply.Width);
               Insert.Height = (Imply.Height-75);
                
               Insert.BorderStyle = BorderStyle.None;
               Insert.ScrollBars = ScrollBars.None;
               
               Insert.AutoSize = false;
               Insert.ReadOnly = true;
               Insert.Multiline = true;

               Insert.Location = new Point(X, Y);
               Insert.Font = new Font((Font_Type), (Font_Size), FontStyle.Bold);

               Insert.Text = (Text);
               Insert.Update();  
              
              Target.Controls.Add(Insert);
            return true;  
          }
          
          public bool RegisterTextBox(Form Imply, TextBox Target, string Text, string Font_Type, int Font_Size, int maxLength, int X, int Y, int Width, int Height, int fr, int fg, int fb, int br, int bg, int bb) {
               Target.BackColor = Color.FromArgb((br), (bg), (bb));
               Target.ForeColor = Color.FromArgb((fr), (fg), (fb));
             
               Target.BorderStyle = BorderStyle.None;

               Target.Text = (Text);
               Target.MaxLength = (maxLength);

               Target.Location = new Point((X), (Y));
               Target.Font = new Font((Font_Type), (Font_Size));
             
               Target.Width = (Width);
               Target.Height = (Height);
              
              Imply.Controls.Add(Target);
            return true;
          }
         
          
          public bool CreateButton(Form Imply, Button Target, string Text, string Font_Type, int Font_Size, int Border_Size, bool Center_Button, int X, int Y, int Width, int Height, int br, int bg, int bb, int fr, int fg, int fb) {
               Target.BackColor = Color.FromArgb((br), (bg), (bb));
               Target.ForeColor = Color.FromArgb((fr), (fg), (fb));
               
               Target.Font = new Font((Font_Type), (Font_Size));
               
               Target.Text = (Text);
               Target.FlatStyle = FlatStyle.Flat;
               
               Target.UseCompatibleTextRendering = true;
               Target.FlatAppearance.BorderSize = (0);
               Target.FlatAppearance.BorderColor = Color.FromArgb((br), (bg), (bb));

               Target.Width = (Width);
               Target.Height = (Height);

              Imply.Controls.Add(Target);

                if(Center_Button == true) Target.Location = new Point((Imply.Width/2-Target.Width/2), Y); 
                else Target.Location = new Point(X, Y);
            return true;  
          }
          
          public bool CreatePanel(Form Imply, Panel Target, string Font_Type, int Font_Size, int X, int Y, int Width, int Height, int fr, int fg, int fb, int br, int bg, int bb) {
               Target.BackColor = Color.FromArgb((br), (bg), (bb));
               Target.ForeColor = Color.FromArgb((fr), (fg), (fb));

               Target.Font = new Font((Font_Type), (Font_Size));
               Target.Location = new Point((X), (Y));
               Target.Size = new Size((Width), (Height));

              Imply.Controls.Add(Target);
            return true;
          }

          public bool CreateSubMenu(Form Imply, MenuStrip Main_Target, ToolStripMenuItem Insert, string Font_Type, int Font_Size, string Tooltip_Text, string Item_Data, int X, int Y, int Width, int Height, int br, int bg, int bb, int fr, int fg, int fb) {
               Insert.ForeColor = Color.FromArgb((fr), (fg), (fb));
               Insert.Font = new Font((Font_Type), (Font_Size));
               Insert.Size = new Size((Width-1), (Height));
               
               Insert.AutoSize = false;
               Insert.Margin = new Padding((X), (Y), (X), (Y));
               
               Main_Target.LayoutStyle = ToolStripLayoutStyle.Flow;

               Insert.Width = (Width);
               Insert.Height = (Height);
               
               Insert.Text = (Tooltip_Text);
               Insert.TextAlign = ContentAlignment.MiddleCenter;   

              Main_Target.Items.Add(Insert);
              Imply.Update();
            return true;
          }
          
          public bool CreateMenu(Form Imply, MenuStrip Target, string Menu_Text, int fr, int fg, int fb) {
               Target.BackColor = Color.FromArgb((fr), (fg), (fb));
              
               Target.AutoSize = false;
               Target.CanOverflow = true;

               Target.Height = (26);
               Target.Name = (Menu_Text);

              Imply.Controls.Add(Target);
              Target.Update();
            return true;  
          }
          
          public bool WriteToFile(String Data, string Filename) {
              StreamWriter Outgoing = new StreamWriter((Filename));
             
               Outgoing.WriteLine((Data), (Filename));
               Outgoing.Close();
             
            return true;  
          }

          public bool SaveFileAs(string Data, string Title, string Filter) {
              SaveFileDialog Target = new SaveFileDialog();

               Target.Filter = (Filter);
               Target.Title = (Title);  
               Target.ShowDialog();

              if((Target.FileName != "") || (Target.FileName != String.Empty))
                WriteToFile(Data, Path.GetFullPath(Target.FileName));
              else
                WriteToFile((Data), ("Yas"));

            return true;  
          }

          public bool CheckFile(string Filet) {
              if(!File.Exists(Filet))
                return false;
              else
                return true;
          }
          
          public bool RemoveFilet(string Filet) {
              File.Delete(Filet);
            return true;
          }

          public bool CopyFilet(string From_Filet, string To_Filet) {
              File.Copy(From_Filet, To_Filet);
            return true;
          }

          public bool MoveFilet(string From_Filet, string To_Filet) {
              File.Move(From_Filet, To_Filet);
            return true;
          }
          
          public bool MoveDirectory(string From_Dir, string To_Dir) {
              Directory.Move(From_Dir, To_Dir);
            return true;
          }

          public bool RemoveDirectory(string Dir) {
              Directory.Delete(Dir);
            return true;
          }

          public bool MakeFile(string Filet) {
              File.Create(Filet);
            return false;
          }

          public bool CheckDirectory(string Dir) {
              if(!Directory.Exists(Dir))
                return false;
              else
                return true;
          }

          public bool MakeDirectory(string Dir) {
              Directory.CreateDirectory(Dir);
            return true;
          }

          public bool WriteText(Form Imply, Label Target, string Text, string Font_Type, int Font_Size, int Font_Weight, int Width, int Height, bool Center_Text, int X, int Y, int br, int bg, int bb, int fr, int fg, int fb) {
               Target.BackColor = Color.FromArgb((br), (bg), (bb));
               Target.ForeColor = Color.FromArgb((fr), (fg), (fb));

               Target.BorderStyle = BorderStyle.None;
               Target.TextAlign = ContentAlignment.MiddleCenter;

               Target.Text = (Text);
              
              if(Width >= 1) { 
                 Target.AutoSize = false;
                 Target.Size = new Size((Width), (Height));
              } else Target.AutoSize = true;

               Target.Text.Remove(0);
               Target.Font = new Font((Font_Type), (Font_Size));
              
              Imply.Controls.Add((Target));

              if(Center_Text == true) { 
                  Target.Location = new Point(Imply.Width/2-Target.Width/2, Y);
              } else Target.Location = new Point((X), (Y)); 
            return true;  
          }

          public bool RegisterImage(Form Imply, PictureBox Target, string Img, int X, int Y, int Width, int Height) {
              ResourceManager Resource_Loader = new ResourceManager((Resource_Key), Assembly.GetExecutingAssembly());
               
               Target.Image = ((Image)Resource_Loader.GetObject((Img)));
               Target.Location = new Point((X), (Y));
               Target.Size = new Size((Width), (Height));
               
               Target.SizeMode = PictureBoxSizeMode.AutoSize;
               Target.BringToFront();
               
              Imply.Controls.Add((Target));
            return true;  
          }
    }
}
