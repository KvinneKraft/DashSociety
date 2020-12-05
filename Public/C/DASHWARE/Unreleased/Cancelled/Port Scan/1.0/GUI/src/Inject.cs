
/* 
 
(c) All Rights Reserved, Dashies Software Inc.

I LOVE YOU GRACE <3333

*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;




namespace src
{
    public class Inject
    {
        public class Cawntrol
        {
            public Boolean TextBox(Form Inherit, TextBox Interpret, string Text, string Font, int Size, bool MultiLine, bool Centerize, int X, int Y, int W, int H, int R, int G, int B, int FR, int FG, int FB)
            {
                try
                {
                    Interpret.Text = Text;
                    Interpret.Font = new Font(Font, Size);

                    if(MultiLine == true)
                    {
                        Interpret.Multiline = true;
                        Interpret.ScrollBars = ScrollBars.Vertical;
                    }

                    Interpret.MinimumSize = new Size(W, H);
                    Interpret.MaximumSize = new Size(W, H);
                    Interpret.Size = new Size(W, H);

                    if(Centerize == true)
                    {
                        X = (Inherit.Width - W)/2;
                    }

                    Interpret.BorderStyle = BorderStyle.None;
                    Interpret.Location = new Point(X, Y);

                    Interpret.ForeColor = Color.FromArgb(FR, FG, FB);
                    Interpret.BackColor = Color.FromArgb(R, G, B);

                    Inject.Function func = new Inject.Function();
                    ResourceManager embeded = new ResourceManager("src.Embeded", Assembly.GetExecutingAssembly());

                    func.AnimatedCursor(Interpret, (byte[])embeded.GetObject("cursor"));
                    
                    Inherit.Controls.Add(Interpret);
                    return true;
                }

                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                    return false;
                }
            }

            public Boolean Label(Form Inherit, Label Interpret, string Text, string Font, int Size, bool AutoSizing, bool Centerize, int X, int Y, int W, int H, int R, int G, int B, int FR, int FG, int FB)
            {
                try
                {

                    Interpret.Text = Text;
                    Interpret.Font = new Font(Font, Size);

                    if(AutoSizing == true)
                    {
                        W = Interpret.PreferredWidth;
                        H = Interpret.PreferredHeight;
                    }

                    Interpret.Size = new Size(W, H);

                    if(Centerize == true)
                    {
                        X = (Inherit.Width  - W) / 2;
                    }

                    Interpret.FlatStyle = FlatStyle.Flat;
                    Interpret.BorderStyle = BorderStyle.None;

                    Interpret.Location = new Point(X, Y);

                    Interpret.ForeColor = Color.FromArgb(FR, FG, FB);
                    Interpret.BackColor = Color.FromArgb(R, G, B);

                    Inherit.Controls.Add(Interpret);
                    return true;
                }

                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                    return false;
                }
            }

            public Boolean Button(Form Inherit, Button Interpret, string Text, string Font, int Size, bool Centerize, int X, int Y, int W, int H, int R, int G, int B, int FR, int FG, int FB, int HR, int HG, int HB, int HFR, int HFG, int HFB)
            {
                try
                {
                    Interpret.Text = Text;
                    Interpret.Font = new Font(Font, Size);

                    if (Centerize == true)
                    {
                        X = (Inherit.Width - W) / 2;
                    }

                    Interpret.FlatAppearance.BorderColor = Color.FromArgb(R, G, B);
                    Interpret.FlatAppearance.BorderSize = 0;
                    Interpret.FlatStyle = FlatStyle.Flat;

                    Interpret.Location = new Point(X, Y);
                    Interpret.Size = new Size(W, H);

                    Interpret.BackColor = Color.FromArgb(R, G, B);
                    Interpret.ForeColor = Color.FromArgb(FR, FG, FB);

                    Interpret.MouseEnter += (s, q) => Interpret.BackColor = Color.FromArgb(HR, HG, HB);
                    Interpret.MouseLeave += (s, q) => Interpret.BackColor = Color.FromArgb(R, G, B);

                    Inherit.Controls.Add(Interpret);
                    return true;
                }

                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                    return false;
                }
            }

            public Boolean PictureBox(Form Inherit, PictureBox Interpret, string src, bool Embeded, bool Centerized, bool AutoSize, int X, int Y, int W, int H, int R, int G, int B)
            {
                try
                {
                    if (src != String.Empty)
                    {
                        if (Embeded == true)
                        {
                            ResourceManager embeded = new ResourceManager("src.Embeded", Assembly.GetExecutingAssembly());
                            Interpret.Image = (Image)embeded.GetObject(src);
                        }

                        else
                        {
                            Interpret.Image = Image.FromFile(src);
                        }
                    }

                    if (AutoSize == true)
                    {
                        W = Interpret.PreferredSize.Width;
                        H = Interpret.PreferredSize.Height;
                    }

                    if (Centerized == true)
                    {
                        X = (Inherit.Width - W)/2;
                    }

                    Interpret.Size = new Size(W, H);
                    Interpret.Location = new Point(X, Y);

                    Interpret.BackColor = Color.FromArgb(R, G, B);
                    Inherit.Controls.Add(Interpret);
                    return true;
                }

                catch(Exception E)
                {
                    MessageBox.Show(E.ToString());
                    return false;
                }
            }
        }

        public class Function
        {
            public static Point lastPosition;
            
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

            public void Mouse_Move(Control Interpret, MouseEventArgs E)
            {
                try
                {
                    if(E.Button == MouseButtons.Left)
                    {
                        Interpret.Left += E.X - lastPosition.X;
                        Interpret.Top += E.Y - lastPosition.Y;
                    }

                    return;
                }

                catch (Exception Q)
                {
                    MessageBox.Show(Q.ToString());
                }
            }

            public void Mouse_Down(MouseEventArgs Er)
            {
                try
                {
                    lastPosition = new Point(Er.X, Er.Y);
                    return;
                }

                catch (Exception E)
                {
                    MessageBox.Show(E.ToString());
                }
            }
        }

        public class Layout
        {
            public bool Border(PaintEventArgs e, int X, int Y, int W, int H, float Thiccness, int R, int G, int B)
            {
                try
                {
                    Pen Pencil = new Pen(Color.FromArgb(R, G, B), Thiccness);
                    Rectangle Rect = new Rectangle(X, Y, W, H);

                    e.Graphics.DrawRectangle(Pencil, Rect);
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