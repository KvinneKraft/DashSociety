
/* (c) All Rights Reserved, Dashies Software Inc. */

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
    class Class {
        public String Resource_ID = ("Dash_Server.embeded");
        public Boolean RunOnce = true, WriteOnce = true, Disabled = true;
        public String Adding = "* ", CreateURL = String.Empty;

        public HttpListenerContext Context;
        public HttpListener Target = new HttpListener();

        public ResourceManager Resource_Manager = new ResourceManager(("Dash_Server.embeded"), Assembly.GetExecutingAssembly());

        [DllImport("user32.dll")]
        static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);

        public void SetAnimatedCursor(Form inject, byte[] resource) {
            IntPtr Animated_Cursor = CreateIconFromResource(resource, (uint)resource.Length, false, 0x00030000);
            Cursor Animated = new Cursor(Animated_Cursor);
            inject.Cursor = Animated;
            
            return ;
        }

        public void SetTextBoxCursor(TextBox Target, Byte[] resource) {
            IntPtr Animated_Cursor = CreateIconFromResource(resource, (uint)resource.Length, false, 0x00030000);
            Cursor Animated = new Cursor(Animated_Cursor);
            Target.Cursor = Animated;

            return ; 
        }

        public void SetLabelCursor(Label Target, Byte[] resource) {
            IntPtr Animated_Cursor = CreateIconFromResource(resource, (uint)resource.Length, false, 0x00030000);
            Cursor Animated = new Cursor(Animated_Cursor);
            Target.Cursor = Animated;

            return ;
        }

        public void SetGroupBoxCursor(GroupBox Target, Byte[] resource) {
            IntPtr Animated_Cursor = CreateIconFromResource(resource, (uint)resource.Length, false, 0x00030000);
            Cursor Animated = new Cursor(Animated_Cursor);
            Target.Cursor = Animated;

            return ;    
        }

        public Boolean isAdministrator() {
            bool returnValue = false;

            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);

            if(principal.IsInRole(WindowsBuiltInRole.Administrator) == true) returnValue = true;
            else returnValue = false;

            return returnValue;
        }

        public void InjectImage(Form Inject, PictureBox Target, String Memory_Address, int X, int Y, int Width, int Height, int fr, int fg, int fb, int br, int bg, int bb) {
            Target.Width = Width;
            Target.Height = Height;

            if(X < 0) Target.Location = new Point((Inject.Width-Width)/2, Y);
            else Target.Location = new Point(X, Y);

            if((Memory_Address != "") || (Memory_Address != String.Empty)) {
                 ResourceManager Resource_Loader = new ResourceManager((Resource_ID), Assembly.GetExecutingAssembly());
                 Target.Image = (Image)Resource_Loader.GetObject(Memory_Address);
            }

            if((br != 00) && (bg != 00) && (bb != 00)) Target.BackColor = Color.FromArgb(br, bg, bb);
            Target.ForeColor = Color.FromArgb(fr, fg, fb);

            Inject.Controls.Add(Target);
        }

        public void InjectCheckBox(Form Inject, CheckBox Target, int X, int Y, int Width, int Height, int r, int g, int b) {
            if(X < 0) Target.Location = new Point((Inject.Width-Width)/2, Y);
            else Target.Location = new Point(X, Y);

            Target.AutoSize = false;
            Target.Size = new Size(Width, Height);
            Target.BackColor = Color.FromArgb(r, g, b);
            Target.ForeColor = Color.FromArgb(25, 0, 255); // Maybe make this multi-functional?
            //Target.Margin ;
            Target.FlatStyle = FlatStyle.Flat;

            Inject.Controls.Add(Target);
        }

        public void InjectButton(Form Inject, Button Target, String Data, int Font_Size, Boolean BorderColor, int X, int Y, int Width, int Height, int fr, int fg, int fb, int br, int bg, int bb, int hr, int hg, int hb) {
            Target.Text = (String)(Data);

            Target.Width = Width;
            Target.Height = Height;
            
            if(X <= 0) Target.Location = new Point((Inject.Width-Width)/2, Y);
            else Target.Location = new Point(X, Y);

            Font Fawnt = new Font("Consolas", Font_Size);

            Target.Font = Fawnt;
            Target.Padding = new Padding(0, 0, 0, 0);

            Target.FlatStyle = FlatStyle.Flat;

            if(BorderColor == true) {
                Target.FlatAppearance.BorderColor = Color.FromArgb(br, bg, bb);
                Target.FlatAppearance.BorderSize = 1;
            } else Target.FlatAppearance.BorderSize = 0;

            Target.BackColor = Color.FromArgb(br, bg, bb);
            Target.ForeColor = Color.FromArgb(fr, fg, fb);

            Target.MouseEnter += (sender, argumentation) => {
                Target.BackColor = Color.FromArgb(hr, hg, hb);
            };

            Target.MouseLeave += (sender, arugmentation) => {
                Target.BackColor = Color.FromArgb(br, bg, bb);
            };

            Inject.Controls.Add(Target);
        }

        public int DEFAULT_FONT_SIZE = 12;

        public void InjectLabel(Form Inject, Label Target, String Data, Boolean ReadOnly, String Cursor, int Font_Size, int Width, int Height, int X, int Y, int fr, int fg, int fb, int br, int bg, int bb) {
            if((Data != "") || (Data != String.Empty)) Target.Text = Data;
            else Target.Text = ("Unknown ey");

            if((Width <= 0) && (Height <= 0)) { 
                Target.AutoSize = true;
            } else {
                Target.Width = Width;
                Target.Height = Height;
            }

            SetLabelCursor(Target, (Byte[])Resource_Manager.GetObject(Cursor));

            if(X < 0) Target.Location = new Point((Inject.Width - Target.Width) / 2, Y);
            else Target.Location = new Point(X, Y);

            Target.BackColor = Color.FromArgb(br, bg, bb);
            Target.ForeColor = Color.FromArgb(fr, fg, fb);

            Font fawnt = new Font("Consolas", Font_Size);

            Target.BorderStyle = BorderStyle.None;
            Target.Font = fawnt;

            Inject.Controls.Add(Target);
        }

        public void InjectGroupBox(Form Inject, GroupBox Target, String Cursor, int X, int Y, int Width, int Height, int r, int g, int b) {
            if((Width <= 0) && (Height <= 0)) Target.Size = Target.PreferredSize;
            else Target.Size = new Size(Width, Height);

            if(X < 0) Target.Location = new Point((Inject.Width - Target.Width) / 2, Y);
            else Target.Location = new Point(X, Y);

            SetGroupBoxCursor(Target, (Byte[])Resource_Manager.GetObject(Cursor));

            Target.Paint += (senders, argumentations) => {
                argumentations.Graphics.Clear(Color.FromArgb(r, g, b));
            };

            Inject.Controls.Add(Target);
        }

        public String ValidExtensions() {
            return (

                "-== Web Extensions ==-" + "|" + "" + "|" +
                "Hyper Text Markup Language" + "|" + "*.html" + "|" +
                "Cascading Style Sheeds " + "|" + "*.css" + "|" +
                "JavaScript" + "|" + "*.js" + "|" +
                "PHP" + "|" + "*.php" + "|" +
                "Perl" + "|" + "*.pl" + "|" +
                "CGI" + "|" + "*.cgi" + "|" +
                "Java" + "|" + "*.jsp" + "|" +
                "ASP Classic" + "|" + "*.asp" + "|" +
                "ASP.NET" + "|" + "*.aspx" + "|" +
                "Erlang" + "|" + "*.yaws" + "|" +

                "" + "|" + "" + "|" +

                "-== Mashup ==-" + "|" + "" + "|" +
                "ASCII Text File" + "|" + "*.txt" + "|" +
                "Other shits(may glitch)" + "|" + "*.*"

            );
        } 

        public void InjectText(Form Inject, TextBox Target, String Data, Boolean ReadOnly, Boolean MultiLine, Boolean ScrollBar, String Cursor, int Font_Size, int Width, int Height, int X, int Y, int fr, int fg, int fb, int br, int bg, int bb) {
            if((Data != "") || (Data != String.Empty)) Target.Text = Data;
            else Target.Text = ("Pursay Unknown");

            if((Width <= 0) && (Height <= 0)) {
                Target.AutoSize = false;
                Target.Width = Target.PreferredSize.Width;
            } else {
                Target.AutoSize = false;
                Target.Width = Width;
                Target.Height = Height;
            }

            SetTextBoxCursor(Target, (Byte[])Resource_Manager.GetObject(Cursor));

            if(X < 0) Target.Location = new Point((Inject.Width - Target.Width) / 2, Y);
            else Target.Location = new Point(X, Y);

            Target.BackColor = Color.FromArgb(br, bg, bb);
            Target.ForeColor = Color.FromArgb(fr, fg, fb);

            Target.ReadOnly = ReadOnly;
            if(ScrollBar == true) Target.ScrollBars = ScrollBars.Vertical;
            Target.Multiline = MultiLine;

            Font fawnt = new Font("Consolas", Font_Size);

            Target.BorderStyle = BorderStyle.None;
            Target.Font = fawnt;

            Inject.Controls.Add(Target);
        }

        public Boolean RunMyHTTPServer(TextBox Notifier, String Text, String URL, String Port, String Path, Boolean encryptMethod, Boolean autoLoad, Boolean autoStart, Boolean safeBoot) {
            Boolean rval = false;
            Target = new HttpListener();

            try {
                CreateURL = URL + ":" + Port + Path;

                Target.Prefixes.Add(CreateURL);
                
                if(Target.IsListening == false) {
                    if(safeBoot == true) {
                        try { Target.Start(); }
                        catch { Notifier.AppendText(Adding + "\r\n"); }
                    } else Target.Start();
                }        

                if(RunOnce == true) {
                    RunOnce = false;
                    Notifier.AppendText(Adding + "The HTTP Server is up and running as [" + CreateURL + "]!\r\n");

                    if(autoStart == true) {
                        if(safeBoot == true) {
                            try { Process.Start(CreateURL); }    
                            catch { Notifier.AppendText(Adding + "We were unable to open the [" + CreateURL + "] in your default web browser!\r\n"); }
                        } else Process.Start(CreateURL);
                    }
                }

                Context = Target.GetContext();
                String webvalLoad = ("Successfully loaded data values!\r\n" + 
                                    Adding +
                                    "please reload the page if the change(s) are not visible yet.\r\n");

                using (Stream Response = Context.Response.OutputStream)
                using (StreamWriter Inject = new StreamWriter(Response)) {
                    if(WriteOnce == true) {
                        Notifier.AppendText(Adding + "Loading data values ....\r\n");
                        Notifier.AppendText(Adding + "this may take up to 1 minute ....\r\n");
                    }
                    
                    if(safeBoot == true) {
                        try { Inject.Write(Text); }
                        catch { webvalLoad = "Failure while loading data values :/\r\n"; }
                    } else Inject.Write(Text);

                    if(WriteOnce == true) {
                        Notifier.AppendText(Adding + webvalLoad);
                        WriteOnce = false;
                    }
                }

                rval = true;
            } catch { 
                if(Disabled == false) {
                    Notifier.AppendText(Adding + "The HTTP Server died out, probably due inconnectivity!\r\n"); 

                    Text = String.Empty;
                    Disabled = true;
                    RunOnce = true;
                    WriteOnce = true;
                    rval = false;
                } 
            }

            if(safeBoot == true) {
                try {
                    //Context.Response.Close();
                    //Target.Prefixes.Remove(CreateURL);
                    Target.Close();
                } catch { Notifier.AppendText(Adding + "An fatal error occurred while flushing cache(s)!\r\n"); }
            } else {
                Target.Close();
            }

            return rval;
        }

        public void StopHTTPServer(TextBox Notifier) {
            Notifier.AppendText(Adding + "Terminating the running HTTP Server ....\r\n");

            Disabled = true;
            RunOnce = true;
            WriteOnce = true;

            String Result = "Successfully terminated the running HTTP Server!\r\n";
            Moar_Options Access = new Moar_Options();

            if(Access.SafeBoot == true) {
                try { Target.Close(); } 
                catch { Result = "An error occurred while terminating the running HTTP Server :/\r\n"; }
            } else Target.Close();

            Notifier.AppendText(Adding + Result);
            return;
        }
    }
}
