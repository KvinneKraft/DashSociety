
/* (c) All Rights Reserved, Dashies Software Inc. */

// Main Source File for the entire Form.
// Optimizations are a thing for the upcoming future.

using System;
using Microsoft.Win32;
using System.Drawing;
using System.Data;
using System.Data.Sql;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Linq;
using System.IO;
using System.Net;
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
using System.Web;

namespace Dash_Server {
    public partial class Client : Form {
        Moar_Options Access = new Moar_Options();
        Class Get = new Class();

        Button AdvancedOptions = new Button(), Exit = new Button(), StartServer = new Button(), StopServer = new Button(), Minimize = new Button(), LoadFile = new Button();
        PictureBox Banner = new PictureBox(), Cross = new PictureBox(), Hello = new PictureBox();
        GroupBox BootMenu = new GroupBox(), OptionMenu = new GroupBox(), Separator = new GroupBox();
        TextBox BootMenuTitle = new TextBox(), OptionMenuTitle = new TextBox(), Title = new TextBox(), Url = new TextBox(), Poth = new TextBox(), Port = new TextBox(), Status = new TextBox();

        String url = String.Empty, path = String.Empty, port = String.Empty, data = String.Empty;

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Move_Window(object Sender, MouseEventArgs Handler) {
            if (Handler.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public Client() {
            InitializeComponent();

            if(Get.isAdministrator() == false) {
                MessageBox.Show("we have detected that you are not running this application as administrator. \n\nthis privilege is required if you want to use our HTTP Server Management Tool.", "error occurrence is showing its presence.");
                this.Close();
            }

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            
            this.BackColor = Color.FromArgb(22, 22, 22);
            
            this.Text = (string)("Dash Server 1.0");

            ResourceManager Resource_Loader = new ResourceManager((Get.Resource_ID), Assembly.GetExecutingAssembly());
        
            this.Icon = (Icon)Resource_Loader.GetObject("profile");            
          
            if(this.IsAccessible != true) Get.SetAnimatedCursor(this, (Byte[])Resource_Loader.GetObject("Cursor"));

            Get.SetAnimatedCursor(this, (Byte[])Resource_Loader.GetObject("Cursor"));
            this.MouseDown += new MouseEventHandler(Move_Window);

            this.Width = 800;
            this.Height = 450;
            this.MaximumSize = new Size(800, 450);
            this.MinimumSize = new Size(800, 450);

            this.Closing += (sender, argumentation) => {
                Status.AppendText("The HTTP Server received your stop signal!\r\n");
                Get.StopHTTPServer(Status);
            };

            Get.InjectText(this, Status, (getText()), true, true, true, "Cursor", 8, 595, this.Height - 20, 205, 20, 185, 185, 185, 40, 40, 40);

            this.Paint += (sender, argumentation) => {
                Get.InjectText(this, Title, ("Dash Server 1.0"), true, false, false, "Cursor", 8, 0, 0, -1, 3, 255, 255, 255, 4, 4, 4);

                Get.InjectImage(this, Banner, ("profile1"), 0, 0, 20, 20, 255, 255, 255, 4, 4, 4);
                Get.InjectImage(this, Cross, (""), 20, 0, this.Width-148, 20, 255, 255, 255, 4, 4, 4);

                Get.InjectButton(this, Exit, ("X"), 14, false, this.Width-64, -5, 64, 25, 255, 255, 255, 4, 4, 4, 40, 40, 40);
                Get.InjectButton(this, Minimize, ("-"), 14, false, this.Width-64*2, -5, 64, 25, 255, 255, 255, 4, 4, 4, 40, 40, 40);

                Get.InjectText(this, OptionMenuTitle, "Options Menu", true, false, false, "Cursor", 9, 0, 0, (196-OptionMenuTitle.Width-8)/2, 43, 255, 255, 255, 8, 8, 8);
                
                Get.InjectText(this, Url, "http://localhost", false, false, false, "Cursor", 12, 178, 20, 12, 75+5, 255, 255, 255, 20, 20, 20);
                Get.InjectText(this, Poth, "/Path/", false, false, false, "Cursor", 12, 178, 20, 12, 75+24+5, 255, 255, 255, 20, 20, 20);
                Get.InjectText(this, Port, "8080", false, false, false, "Cursor", 12, 178, 20, 12, 75+48+5, 255, 255, 255, 20, 20, 20);

                Get.InjectButton(this, LoadFile, "Load Data File", 10, false, 12, 75+72+15, 178, 24, 255, 255, 255, 18, 0, 68, 37, 14, 102);
                Get.InjectButton(this, AdvancedOptions, "Advanced Options", 10, false, 12, 75+96+20, 178, 24, 255, 255, 255, 18, 0, 68, 37, 14, 102);

                Get.InjectText(this, BootMenuTitle, "Startup Menu", true, false, false, "Cursor", 9, 0, 0, (175-BootMenuTitle.Width)/2, this.Height-(204), 255, 255, 255, 8, 8, 8);

                Get.InjectButton(this, StartServer, "boot server", 10, false, 14, this.Height-(170), 154, 24, 255, 255, 255, 18, 0, 68, 37, 14, 102);
                Get.InjectButton(this, StopServer, "kill server", 10, false, 14, this.Height-(141), 154, 24, 255, 255, 255, 18, 0, 68, 37, 14, 102);

                Get.InjectImage(this, Hello, "hi", 1, this.Height-60, 64, 64, 255, 255, 255, 00, 00, 00);

                Get.InjectGroupBox(this, BootMenu, "Cursor", 4, this.Height-(204), 175, 98, 8, 8, 8);
                Get.InjectGroupBox(this, OptionMenu, "Cursor", 4, 43, 196, 182, 8, 8, 8);

                Cross.MouseDown += new MouseEventHandler(Move_Window);
                Cross.BackColor = Color.FromArgb(4, 4, 4);
            };

            Status.VisibleChanged += (sender, argumentation) => {
                if(Status.Visible) {
                    Status.SelectionStart = Status.Text.Length;
                    Status.ScrollToCaret();
                }
            };

            Exit.Click += (sender, argumentation) => {
                Status.AppendText(Get.Adding + "The HTTP Server received your stop signal!\r\n");
                Get.StopHTTPServer(Status);
                this.Close();
            };

            Minimize.Click += (sender, argumentation) => { this.SendToBack(); };

            Url.TextChanged += (sender, argumentation) => { url = Url.Text; };
            Poth.TextChanged += (sender, argumentation) => { path = Poth.Text; };
            Port.TextChanged += (sender, argumentation) => { port = Port.Text; };

            LoadFile.Click += (sender, argumentation) => {
                OpenFileDialog Shell = new OpenFileDialog();

                Status.AppendText(Get.Adding + "Waiting for file ....\r\n");

                Shell.Filter = (Get.ValidExtensions());
                Shell.Title = ("Select File");
                Shell.ShowDialog();
                
                if(Shell.FileName.Length <= 0) {
                    Status.AppendText(Get.Adding + "Operating has been canceled due none/inproper file.\r\n");
                    Status.AppendText(Get.Adding + "Know that the specified file must contain something in order to be read :o\r\n");
                } else {
                    Status.AppendText(Get.Adding + "Successfully loaded the specified file!\r\n");

                    String[] Container = File.ReadAllLines(Path.GetFullPath(Shell.FileName));
                    data = String.Empty;

                    foreach(string LoadDas in Container) {
                        this.data += LoadDas + "\r\n";
                    }
                }
            };

            StartServer.Click +=  (sender, argumentation) => {
                try {
                    if((url == String.Empty) || (path == String.Empty) || (port == String.Empty)) {
                        ToolTip tawltip = new ToolTip();

                        if(port == String.Empty) tawltip.Show("must enter a port", Url, 0, 20, 1000);
                        if(path == String.Empty) tawltip.Show("must enter a path", Poth, 0, 20, 1000);
                        if(url == String.Empty) tawltip.Show("must enter a url", Port, 0, 20, 1000);
                    } else {
                        ToolTip tawltip = new ToolTip();

                        if((path[path.Length-1].ToString() != "/") || (path[0].ToString() != "/")) { tawltip.Show("invalid format", Poth, 0, 20, 1000); } else
                        if(url.Contains("http://") != true) { tawltip.Show("http:// is missing", Url, 0, 20, 1000); } else
                        if((port.Any(char.IsLetter) == true) || (port.Any(char.IsSymbol) == true) || (!port.Any(char.IsNumber) == true)) { tawltip.Show("port is invalid", Port, 0, 20, 1000); } else

                        if(Get.Disabled == true) {
                            if(data == String.Empty) data = "std::cout << \"C++ in a HTTP Server written in C#, wow!\" << std::endl;\"";
                            Task BootHTTPServer = Task.Run((Action)ServerRuntime);
                        } else Status.AppendText(Get.Adding + "But the HTTP Server is already running :-/\r\n");
                    }
                } catch { 
                    Status.AppendText(Get.Adding + "Unable to Start the HTTP Server for some reason!\n");
                    this.Close();
                }
            };

            StopServer.Click += (sender, argumentation) => {
                try {
                    if(Get.Disabled == false) {
                        Get.StopHTTPServer(Status);

                        if(Access.CleanScreen == true) {
                            Status.AppendText(Get.Adding + "Clearing log in ....\r\n");

                            for(int index = 1; index <= 5; index = index + 1) {
                                Status.AppendText(Get.Adding + index.ToString() + "\r\n");
                                System.Threading.Thread.Sleep(1000);
                            }

                            if(Access.CleanScreen == true) Status.Text = getText();
                        }
                    } else Status.AppendText(Get.Adding + "Eh, the HTTP Server is not even running right now. .-.\r\n");                    
                } catch {
                    Status.AppendText(Get.Adding + "Unable to Terminate the HTTP Server for some reason!\r\n");
                    this.Close();
                }
            };

            AdvancedOptions.Click += (sender, argumentations) => {
                Access.ShowDialog();

                if((Access.AutoStart == true) || (Access.AutoLoad == true) || (Access.EncryptMethod == true) || (Access.SafeBoot == true) || (Access.CleanScreen == true)) Access.useAdvanced = true;
                else Access.useAdvanced = false;
            };

            Url.TextChanged += (sender, argumentation) => {
                if(Url.Text.Length >= 0) {
                    url = String.Empty;
                    url = Url.Text.ToLower();
                } else url = String.Empty;
            };

            Poth.TextChanged += (sender, argumentation) => {
                if(Poth.Text.Length >= 0) {
                    path = String.Empty;
                    path = Poth.Text;
                } else path = String.Empty;
            };

            Port.TextChanged += (sender, argumentation) => {
                if(Port.Text.Length >= 0) {
                    port = String.Empty;
                    port = Port.Text;
                } else port = String.Empty;
            };
        }

        private void ServerRuntime() {
            Get.Disabled = false;
            String Holder = String.Empty;

            Status.AppendText(Get.Adding + ("Loading advanced settings ....\r\n"));

            if(Access.EncryptMethod == true) Holder = Get.Adding + ("Encryption Method.: Enabled\r\n");
            else Holder = Get.Adding + ("Encryption Method.: Disabled\r\n");

            if(Access.AutoLoad == true) Holder += Get.Adding + ("Auto Loader.......: Enabled\r\n");
            else Holder += Get.Adding + ("Auto Loader.......: Disabled\r\n");

            if(Access.AutoStart == true) Holder += Get.Adding + ("Auto Starter......: Enabled\r\n");
            else Holder += Get.Adding + ("Auto Starter......: Disabled\r\n");
                                        
            if(Access.SafeBoot == true) Holder += Get.Adding + ("Safe Boot.........: Enabled\r\n");
            else Holder += Get.Adding + ("Safe Boot.........: Disabled\r\n");

            if(Access.CleanScreen == true) Holder += Get.Adding + ("Clean Screen......: Enabled\r\n");
            else Holder += Get.Adding + ("Clean Screen......: Disabled\r\n");

            if(Holder == String.Empty) Status.AppendText(Get.Adding + ("Boolean useAdvanced was true while not being used, it has been restored to its defaults.\r\n"));
            else Status.AppendText(Holder);

            Status.AppendText(Get.Adding + ("Successfully loaded the specified advanced settings!\r\n"));
            Status.Text += Get.Adding + ("Starting the HTTP Server as [" + url + ":" + port + path + "] ....\r\n");

            while(Get.Disabled == false) {
                Boolean Check = Get.RunMyHTTPServer(Status, data, url, port, path, Access.EncryptMethod, Access.AutoLoad, Access.AutoStart, Access.SafeBoot);

                if(Check == false) {
                    if(Get.Disabled == false) { Get.StopHTTPServer(Status); }
                    break;
                }
            }
        }

        private String getText() {
            return (

                "--------------------------------------------------------------\r\n\r\n" +

                "* dash server 1.0 has been made for those who want to test" + "\r\n" +
                "* vulnerabilities, or just host either an external or and" + "\r\n" +
                "* local hypertext transfer protocol web server." + "\r\n\r\n" +

                "* this magnificant application is still being worked on as" + "\r\n" +
                "* we speak, so please be aware that there may still be issues" + "\r\n" +
                "* with the actual runtime environment upon usage." + "\r\n\r\n" +

                "* in order for you to use this program, simply press the button" + "\r\n" +
                "* named \"Boot Server\" and wait for it to prompt you saying " + "\r\n" +
                "* that it has successfully booted your hypertext transfer protocol" + "\r\n" +
                "* web server, it will also tell you where you can find it." + "\r\n\r\n" +

                "* on your left you will see some input boxes, in these boxes" + "\r\n" +
                "* you may specify your settings, such as the host, the path" + "\r\n" +
                "* and the path." + "\r\n\r\n" +

                "* you may change the following with a similar format as present." + "\r\n\r\n" +

                "   -> http://localhost" + "\r\n" +
                "   - this  option will contain the hypertext transfer protocol" + "\r\n" +
                "   - url that will be hosted as your server." + "\r\n\r\n" +

                "   -> /Path/" + "\r\n" +
                "   - this option will contain the path to the main page of your" + "\r\n" +
                "   - hypertext transfer protocol server." + "\r\n\r\n" +

                "   -> 8080" + "\r\n\n" +
                "   - this option will contain the port that the server will be" + "\r\n" +
                "   - hosted on." + "\r\n\r\n" +

                "--------------------------------------------------------------\r\n\r\n\r\n"
                   
            );
        } 
    }
}
