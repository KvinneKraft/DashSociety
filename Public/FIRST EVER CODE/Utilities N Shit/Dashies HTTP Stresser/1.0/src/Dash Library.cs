
/* (c) All Rights Reserved, Dashies Software Inc. */



using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;



namespace src {
    class Control_Lib {
        public Point lastClick;

        public void MiceDown(MouseEventArgs Entrance) {
            lastClick = new Point(Entrance.X, Entrance.Y);
            return;
        }

        public void MiceMove(Form Inject, MouseEventArgs Entrance) {
            if(Entrance.Button == MouseButtons.Left) {
                Inject.Left += Entrance.X - lastClick.X;
                Inject.Top += Entrance.Y - lastClick.Y;
            }

            return;
        }
    }

    class Dash_Library {
        public struct Menu {
            public int CMA_LEFT, CMA_RIGHT;
        };


        public Menu menu;
        public String resourceId = ("src.embeded");



        public Boolean InjectPanel(Form Inject, Panel Element, int X, int Y, int Width, int Height, int Red, int Green, int Blue) {
            Element.Left = X;
            Element.Top = Y;
            Element.Size = new Size(Width, Height);
            Element.BorderStyle = BorderStyle.None;
            Element.BackColor = Color.FromArgb(Red, Green, Blue);

            // Add Error Checks N Shit

            Inject.Controls.Add(Element);
            return true;
        }



        public Boolean InjectInputBox(Form Inject, String Value, TextBox Element, int X, int Y, int Width, int Height, int FontSize, int BackRed, int BackGreen, int BackBlue, int ForeRed, int ForeGreen, int ForeBlue) {
            Element.Left = X;
            Element.Top = Y;
            Element.Size = new Size(Width, Height);
            Element.Text = Value;
            Element.BackColor = Color.FromArgb(BackRed, BackGreen, BackBlue);
            Element.ForeColor = Color.FromArgb(ForeRed, ForeGreen, ForeBlue);
            Element.ReadOnly = false;
            Element.Multiline = false;
            Element.BorderStyle = BorderStyle.None;
            Element.Font = new Font("Consolas", FontSize);

            ResourceManager ResAdd = new ResourceManager(resourceId, Assembly.GetExecutingAssembly());
            SetTextBoxCursor(Element, (Byte[])ResAdd.GetObject("main"));

            // Add Error Checks N Shit over here as well ^o^

            Inject.Controls.Add(Element);
            return true;
        }



        private static String[] Unicodes = { "☀", "☁", "☂", "★", "☇", "☓",
                                             "☔", "☘", "☙", "☚", "☛", "☜", "☎", "☯", "☢",
                                             "☻", "☽", "☾", "♕", "♖", "♗","♙", "♚", "♛", 
                                             "♜", "♟", "♞", "♠", "♡", "♣", "♥",
                                             "⚔", "⚓", "⚒", "⚖", "⚜", "⚡", "⚸"};

        public String GimmeUnicode() {
            Random getId = new Random();
            String Unicode = Unicodes[getId.Next(0, Unicodes.Length)];
            return "✔";    
        }


        public Boolean InjectText(Form Inject, Label Element, int X, int Y, int Width, int Height, String Value, int FontSize, int BackRed, int BackGreen, int BackBlue, int ForeRed, int ForeGreen, int ForeBlue) {
            Font fonterinos = new Font("Consolas", FontSize);

            Element.Text = Value;
            Element.Font = fonterinos;
            Element.AutoSize = false;

            if ((Width <= 0) || (Height <= 0)) {
                Element.Size = new System.Drawing.Size(Element.PreferredSize.Width, Element.PreferredSize.Height);
            } else {
                Element.Size = new System.Drawing.Size(Width, Height);
            }

            if(Element.Size != new System.Drawing.Size(Width, Height)) {
                return false;
            }

            if(X <= 0) {
                X = (Inject.Width - Element.Width) / 2;
            }

            ResourceManager ResAdd = new ResourceManager(resourceId, Assembly.GetExecutingAssembly());
            SetLabelCursor(Element, (Byte[])ResAdd.GetObject("main"));

            Element.Location = new Point(X, Y);
            Element.BorderStyle = BorderStyle.None;
            Element.BackColor = Color.FromArgb(BackRed, BackGreen, BackBlue);
            Element.ForeColor = Color.FromArgb(ForeRed, ForeGreen, ForeBlue);

            Inject.Controls.Add(Element);
            return true;
        }



        public Boolean InjectImage(Form Inject, PictureBox Element, int X, int Y, int Width, int Height, String Resource, String File, int red, int green, int blue) {
            if((Width <= 0) || (Height <= 0)) {
                return false;
            } else Element.Size = new System.Drawing.Size(Width, Height);

            if(Element.Size != new System.Drawing.Size(Width, Height)) {
                return false;
            }

            if(X <= 0) {
                X = (Inject.Width - Width) / 2;
            }

            Element.Location = new System.Drawing.Point(X, Y);

            if(Element.Location != new System.Drawing.Point(X, Y)) {
                return false;
            }

            if((red < 0) && (green < 0) && (blue < 0)) {
                return false;
            } else Element.BackColor = Color.FromArgb(red, green, blue);

            if(Resource.Length <= 0) {
                if(File.Length <= 0) {
                    return false;
                } Element.Image = Image.FromFile(File);
            } else {
                ResourceManager resLoader = new ResourceManager((resourceId), Assembly.GetExecutingAssembly());
                Element.Image = (Image)resLoader.GetObject(Resource);
            }

            Inject.Controls.Add(Element);
            return true;
        }

        

        public Boolean InjectButton(Form Inject, Button Element, String Value, int X, int Y, int Width, int Height, int fontSize, int R, int G, int B, int FR, int FG, int FB, int HR, int HG, int HB, int HFR, int HFG, int HFB) {
            if((Width <= 0) || (Height <= 0)) {
                return false;
            } else Element.Size = new Size(Width, Height);

            if(Element.Size != new Size(Width, Height)) {
                return false;
            }

            if(X <= 0) {
                X = (Inject.Width - Width)/2;
            }

            Element.TextAlign = ContentAlignment.TopCenter;
            Element.UseCompatibleTextRendering = true;
            Element.FlatStyle = FlatStyle.Flat;
            Element.TabStop = false;

            Element.FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            Element.FlatAppearance.BorderSize = 0;

            Element.Font = new Font("Consolas", fontSize);
            Element.Location = new Point(X, Y);

            Element.Text = Value;

            if(Element.Location != new Point(X, Y)) {
                return false;
            }

            /* 
            
            1.
            - R = RED
            - G = GREEN
            - B = BLUE

            2.
            - FR = FRONTAL RED 
            - FG = FRONTAL GREEN
            - FB = FRONTAL BLUE

            3.
            - HR = HOVER RED
            - HG = HOVER GREEN
            - HB = HOVER BLUE

            4.
            - HFR = HOVER FRONTAL RED
            - HFG = HOVER FRONTAL GREEN
            - HFB = HOVER FRONTAL BLUE

            */

            if((R <= 0) && (G <= 0) && (B <= 0) && (FR <= 0) && (FG <= 0) && (FB <= 0) && (HR <= 0) && (HG <= 0) && (HB <= 0) && (HFR <= 0) && (HFG <= 0) && (HFB <= 0)) {
                return false;
            } else {
                try { 
                    Element.MouseEnter += (sentBy, msValentine) => {
                        Element.BackColor = Color.FromArgb(HR, HG, HB);
                        Element.ForeColor = Color.FromArgb(HFR, HFG, HFB);
                    };

                    Element.BackColor = Color.FromArgb(R, G, B);
                    Element.ForeColor = Color.FromArgb(FR, FG, FB);

                    Element.MouseLeave += (sentBy, msValentine) => {
                        Element.BackColor = Color.FromArgb(R, G, B);
                        Element.ForeColor = Color.FromArgb(FR, FG, FB);
                    };
                } catch {
                    return false;
                }
            }

            Inject.Controls.Add(Element);
            return true;
        }



        public Boolean CreateMenu(Form Inject, int X, int Y, int Width, int Height, PictureBox Yas, String Profile, int pWidth, int pHeight, int pAlign, int red, int green, int blue) {
            if(menu.CMA_LEFT != pWidth) menu.CMA_LEFT = pWidth;
            if(menu.CMA_RIGHT != Width-pWidth) menu.CMA_RIGHT = Width-pWidth;

            PictureBox ProfileId = new PictureBox();

            if(Profile.Length > 0) {
                if((pWidth <= 0) || (pHeight <= 0)) {
                    return false;
                } else {
                    if(InjectImage(Inject, ProfileId, 10, 0, pWidth, pHeight, (Profile), (""), red, green, blue) != true) {
                        return false;
                    } else { ProfileId.Left = 0; }
                }
            }

            if(InjectImage(Inject, Yas, X, Y, Width, Height, ("not real"), (""), red, green, blue) != true) {
                return false;
            }

            return false;
        }


        [DllImport("user32.dll")]
        static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);

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
    }
}
