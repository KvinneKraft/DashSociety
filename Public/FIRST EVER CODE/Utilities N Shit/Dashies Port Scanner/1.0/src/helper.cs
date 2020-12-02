
/* 
 
   (c) All Rights Reserved, Dashies Software Inc.

*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace src
{
    public class DraggableForm
    {
        private Point lastClick;

        public void MiceDown(MouseEventArgs E)
        {
            lastClick = new Point(E.X, E.Y);

            return;
        }

        public void MiceMove(Form Interpret, MouseEventArgs E)
        {
            if(E.Button == MouseButtons.Left)
            {
                Interpret.Left += E.X - lastClick.X;
                Interpret.Top += E.Y - lastClick.Y;
            }

            return;
        }
    }

    public class CustomForm : main
    {
        public class Draw
        {
            public Boolean FilledRectangle(PaintEventArgs E, Form Interpret, int X, int Y, int W, int H, int Thickness, int R, int G, int B)
            {
                try
                {
                    SolidBrush brush = new SolidBrush(Color.FromArgb(R, G, B));
                    Graphics graphics;

                    graphics = Interpret.CreateGraphics();
                    graphics.FillRectangle(brush, new Rectangle(X, Y, W, H));
                    graphics.Dispose();

                    brush.Dispose();
                    return true;
                }

                catch
                {
                    return false;
                }
            } 

            public Boolean Rectangle(PaintEventArgs E, int W, int H, int X, int Y, int Thickness, int R, int G, int B)
            {
                DashBase.Collector database = new DashBase.Collector();

                try
                {
                    Pen Pencil = new Pen(Color.FromArgb(R, G, B), Thickness);
                    Rectangle Rect = new Rectangle(X, Y, W, H);

                    E.Graphics.DrawRectangle(Pencil, Rect);
                    database.drawing.Add(Pencil);
                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }

        public class Inject
        {
            [DllImport("user32.dll")]
            static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);

            public Boolean AnimatedCursor(Control Interpret, byte[] _Cursor)
            {
                IntPtr Get;
                Cursor Set;

                try
                {
                    Get = CreateIconFromResource(_Cursor, (uint)_Cursor.Length, false, 0x00030000);
                    Set = new Cursor(Get);

                    Interpret.Cursor = Set;
                    return true;
                }

                catch
                {
                    return false;
                }
            }

            public Boolean InjectLabel(Form Interpret, bool DoInvokeRequest, Label Inherit, string Contents, bool Centerized, bool Draggable, int FontSize, int X, int Y, int W, int H, int R, int G, int B, int FR, int FG, int FB)
            {
                DashBase.Collector database = new DashBase.Collector();

                try
                {
                    Inherit.Size = new Size(W, H);
                    Inherit.Text = Contents;

                    Inherit.BackColor = Color.FromArgb(R, G, B);
                    Inherit.ForeColor = Color.FromArgb(FR, FG, FB);

                    if(Draggable == true)
                    {
                        DraggableForm drag = new DraggableForm();

                        Inherit.MouseDown += (sender, receiver) =>
                        {
                            drag.MiceDown(receiver);
                        };

                        Inherit.MouseMove += (sender, receiver) =>
                        {
                            drag.MiceMove(Interpret, receiver);
                        };
                    }

                    if(Centerized == true)
                    {
                        X = (Interpret.Width - Inherit.Width) / 2;
                    }

                    Inherit.Font = new Font("Consolas", FontSize);
                    Inherit.Location = new Point(X, Y);

                    database.database.Add(Inherit);

                    SplashScreenConfiguration config = new SplashScreenConfiguration();
                    ResourceManager access_embeded = new ResourceManager((config.Embeded_Key), Assembly.GetExecutingAssembly());

                    AnimatedCursor(Interpret, (byte[])access_embeded.GetObject("cursor"));

                    if(DoInvokeRequest == true)
                    {
                        Interpret.Invoke((MethodInvoker) delegate {
                            Interpret.Controls.Add(Inherit);
                        });
                    }

                    else
                    {
                        Interpret.Controls.Add(Inherit);
                    }

                    return true;
                }

                catch
                {
                    return false;
                }
            }

            public Boolean InjectTextBox(Form Interpret, bool DoInvokeRequest, TextBox Inherit, string Contents, bool DoNotEdit, bool Centerized, bool Draggable, bool Log, int FontSize, int X, int Y, int W, int H, int R, int G, int B, int FR, int FG, int FB)
            {
                DashBase.Collector database = new DashBase.Collector();

                try
                {
                    if(Log == true)
                    {
                        Inherit.Multiline = Log;
                        Inherit.ScrollBars = ScrollBars.Vertical;
                    }

                    Inherit.MaximumSize = new Size(W, H);
                    Inherit.MinimumSize = new Size(W, H);

                    Inherit.Size = new Size(W, H);
                    Inherit.ReadOnly = DoNotEdit;
                    Inherit.BorderStyle = BorderStyle.None;

                    if(Draggable == true)
                    {
                        DraggableForm drag = new DraggableForm();

                        Inherit.MouseDown += (sender, receiver) =>
                        {
                            drag.MiceDown(receiver);
                        };

                        Inherit.MouseMove += (sender, receiver) =>
                        {
                            drag.MiceMove(Interpret, receiver);
                        };
                    }

                    Inherit.BackColor = Color.FromArgb(R, G, B);
                    Inherit.ForeColor = Color.FromArgb(FR, FG, FB);

                    if(Centerized == true)
                    {
                        X = (Interpret.Width - Inherit.Width) / 2;
                    }

                    Inherit.Font = new Font("Consolas", FontSize);
                    Inherit.Location = new Point(X, Y);
                    Inherit.Text = Contents;

                    database.database.Add(Inherit);
                    Inherit.ScrollToCaret();

                    SplashScreenConfiguration config = new SplashScreenConfiguration();
                    ResourceManager access_embeded = new ResourceManager((config.Embeded_Key), Assembly.GetExecutingAssembly());

                    AnimatedCursor(Interpret, (byte[]) access_embeded.GetObject("cursor"));

                    if(DoInvokeRequest == true) {
                        Interpret.Invoke((MethodInvoker)delegate {
                            Interpret.Controls.Add(Inherit);
                        });
                    } 
                    
                    else
                    {
                        Interpret.Controls.Add(Inherit);
                    }

                    return true;
                }

                catch
                {
                    return false;
                }
            }

            public Boolean InjectButton(Form Interpret, bool DoInvokeRequest, Button Inherit, bool Centerized, string Tag, int FontSize, int X, int Y, int W, int H, int R, int G, int B, int FR, int FG, int FB, int HR, int HG, int HB, int HFR, int HFG, int HFB) 
            {
                DashBase.Collector database = new DashBase.Collector();

                try
                {
                    database.database.Add(Inherit);

                    Inherit.AutoSize = false;
                    Inherit.Size = new Size(W, H);

                    if(Centerized == true)
                    {
                        X = (Interpret.Width - W) / 2;
                    }

                    Inherit.BackColor = Color.FromArgb(R, G, B);
                    Inherit.ForeColor = Color.FromArgb(FR, FG, FB);

                    Inherit.MouseEnter += (sender, receiver) => {
                        Inherit.BackColor = Color.FromArgb(HR, HG, HB);
                        Inherit.ForeColor = Color.FromArgb(HFR, HFG, HFB);

                        Inherit.MouseLeave += (sSender, sReceiver) =>
                        {
                            Inherit.BackColor = Color.FromArgb(R, G, B);
                            Inherit.ForeColor = Color.FromArgb(FR, FG, FB);
                        };
                    };

                    Inherit.FlatAppearance.BorderColor = Color.FromArgb(R, G, B);
                    Inherit.FlatAppearance.BorderSize = 0;
                    Inherit.FlatStyle = FlatStyle.Flat;

                    Inherit.Font = new Font("Consolas", FontSize);
                    Inherit.Location = new Point(X, Y);
                    Inherit.Text = Tag;

                    if(DoInvokeRequest == true)
                    {
                        Interpret.Invoke((MethodInvoker) delegate {
                            Interpret.Controls.Add(Inherit);
                        });
                    }

                    else
                    {
                        Interpret.Controls.Add(Inherit);
                    }

                    return true;
                }

                catch
                {
                    return false;
                }
            }

            public Boolean InjectImage(Form Interpret, bool DoInvokeRequest, PictureBox Inherit, bool Centerized, bool IsResource, bool OverwriteOfficialSize, string Source, int X, int Y, int W, int H, int R, int G, int B) 
            {
                DashBase.Collector database = new DashBase.Collector();

                try {
                    database.database.Add(Inherit);

                    if(OverwriteOfficialSize == true)
                    {
                        Inherit.Size = new Size(W, H);
                    }
                    
                    else
                    {
                        Inherit.SizeMode = PictureBoxSizeMode.AutoSize;    
                    }

                    if(Source != String.Empty) {
                        if(IsResource == true)
                        {
                            SplashScreenConfiguration config = new SplashScreenConfiguration();
                            ResourceManager access_emebeded = new ResourceManager((config.Embeded_Key), Assembly.GetExecutingAssembly());

                            Inherit.Image = (Image)access_emebeded.GetObject(Source);
                        }

                        else
                        {
                            if(File.Exists(Source) == true) {
                                Inherit.Image = Image.FromFile(Source);
                            } 
                            
                            else
                            {
                                MessageBox.Show("Image does not exist.");
                                return false;
                            }
                        }
                    }

                    if(Centerized == true)
                    {
                        if(OverwriteOfficialSize == true) {
                            X = (Interpret.Width-W) / 2;
                        } 
                        
                        else
                        {
                            X = (Interpret.Width-Inherit.Width) / 2;
                        }
                    }
                    
                    Inherit.Location = new Point(X, Y);
                    Inherit.BackColor = Color.FromArgb(R, G, B);

                    if(DoInvokeRequest == true)
                    {
                        Interpret.Invoke((MethodInvoker) delegate {
                            Interpret.Controls.Add(Inherit);
                        });
                    }

                    else
                    {
                        Interpret.Controls.Add(Inherit);
                    }

                    return true;
                }

                catch
                {
                    return false;
                }
            }

            public Boolean Menu(Form Interpret, bool Draggable, bool ImageDraggable, bool IsResource, string profile, int iX, int iY, int iW, int iH, int X, int Y, int W, int H, int R, int G, int B)
            {
                PictureBox ProfilePicture = new PictureBox(), MenuBar = new PictureBox();
                DashBase.Collector database = new DashBase.Collector();

                try
                {
                    database.database.Add(ProfilePicture);
                    database.database.Add(MenuBar);

                    if(InjectImage(Interpret, true, ProfilePicture, false, IsResource, false, "Profile_Picture", iX, iY, iW, iH, R, G, B) == false)
                    {
                        return false;
                    }

                    if(InjectImage(Interpret, true, MenuBar, false, false, true, String.Empty, iW, Y, Interpret.Width-iW, iH, R, G, B) == false)
                    {
                        return false;
                    }

                    if(ImageDraggable == true)
                    {
                        DraggableForm drag = new DraggableForm();

                        ProfilePicture.MouseDown += (sender, receiver) =>
                        {
                            drag.MiceDown(receiver);
                        };

                        ProfilePicture.MouseMove += (sender, receiver) =>
                        {
                            drag.MiceMove(Interpret, receiver);
                        };
                    }

                    if(Draggable == true)
                    {
                        DraggableForm drag = new DraggableForm();

                        MenuBar.MouseDown += (sender, receiver) =>
                        {
                            drag.MiceDown(receiver);
                        };

                        MenuBar.MouseMove += (sender, receiver) =>
                        {
                            drag.MiceMove(Interpret, receiver);
                        };
                    }

                    return true;
                }

                catch
                {
                    return false;
                }
            }
        }
    }
}
