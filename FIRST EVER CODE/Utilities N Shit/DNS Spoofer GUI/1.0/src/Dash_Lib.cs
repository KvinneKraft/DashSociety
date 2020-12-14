
/* 

(c) All Rights Reserved, Dashies Software Inc.

Dash Lib 1.0
Function Pack.

*/

using System;
using System.Drawing;
using System.Data;
using System.Data.Sql;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace Pony_Spoofer_GUI {
    class Dash_Lib {
      public string resKey = "Pony_Spoofer_GUI.Embeded";
       public void ShowToolTip(Form Access, TextBox TXTB, string TAGY, string TUXT, int X, int Y, int ShowTime, int FR, int FG, int FB, int BR, int BG, int BB) {
              ToolTip B = new ToolTip();
              
              B.Draw += (sender, e) => {
                  e.DrawBackground();
                  //e.DrawBorder();
                  e.DrawText();
              };

              B.BackColor = Color.Transparent;
              B.Hide(TXTB);
             
               B.IsBalloon = false;
               B.InitialDelay = 0;
               B.ShowAlways = true;
               B.Tag = TAGY;
               B.OwnerDraw = true;
               B.ForeColor = Color.FromArgb(FR, FG, FB);
               B.BackColor = Color.FromArgb(BR, BG, BB);
               B.UseFading = true;
               
              B.Show(TUXT, TXTB, X, Y, ShowTime);
           return ;
       }
    
        public void WriteTextEx(Form MyForm, Label TextID, string Text, bool Center, int X, int Y, int FontSize, int br, int bg, int bb, int fr, int fg, int fb) {
                 TextID.ForeColor = Color.FromArgb(fr, fg, fb);
                 TextID.BackColor = Color.FromArgb(br, bg, bb);
                
                 TextID.BorderStyle = BorderStyle.None;
                 TextID.Text        = Text;
                 TextID.AutoSize    = true;
                
                 TextID.Width = TextID.Size.Width;
                
                if(Center == true)
                        {TextID.Location = new Point((MyForm.ClientSize.Width - TextID.Size.Width)/2, Y); //TextID.Left = (this.ClientSize.Width - TextID.Size.Width) / 2;
                         TextID.Top  = Y;}
                else
                        TextID.Location = new Point(X, Y);
                
                 TextID.TextAlign = ContentAlignment.MiddleCenter;
                 TextID.Font = new Font("Arial", FontSize);
                
                MyForm.Controls.Add(TextID);
            return ;            
		}

       public void WriteText(Form MyForm, string Text, bool Center, int X, int Y, int FontSize, int fr, int fg, int fb) {
             Label TextID = new Label();
                
                TextID.ForeColor = Color.FromArgb(fr, fg, fb);
                TextID.BackColor = Color.Transparent;
                
                TextID.BorderStyle = BorderStyle.None;
                TextID.Text        = Text;
                TextID.AutoSize    = true;
                
                TextID.Width = TextID.Size.Width;
                
               if(Center == true)
                       {TextID.Location = new Point((MyForm.ClientSize.Width - TextID.Size.Width)/2, Y); //TextID.Left = (this.ClientSize.Width - TextID.Size.Width) / 2;
                        TextID.Top  = Y;}
               else
                       TextID.Location = new Point(X, Y);
                
                TextID.TextAlign = ContentAlignment.MiddleCenter;
                TextID.Font = new Font("Arial", FontSize);
                
               MyForm.Controls.Add(TextID);
           return ;
        }

        public void SetListBox(Form MyForm, ListBox item, int X, int Y, int width, int height, int br, int bg, int bb, int fr, int fg, int fb) {
              item.BackColor = Color.FromArgb(br, bg, bb);
              item.ForeColor = Color.FromArgb(fr, fg, fb);
              item.BorderStyle = BorderStyle.None;
              item.Location = new Point(X, Y);
              item.Size = new Size(width, height);
              item.Font = new Font("Dotum", 12);
               
               MyForm.Controls.Add(item);
           return ;
        }

        public void SetComboBox(Form MyForm, string text, ComboBox item, int X, int Y, int width, int height, int fontSize, int br, int bg, int bb, int fr, int fg, int fb) {
              item.BackColor = Color.FromArgb(br, bg, bb);
              item.ForeColor = Color.FromArgb(fr, fg, fb);
              item.FlatStyle = FlatStyle.Flat;              
              item.Resize += (s, e) => { if (!item.IsHandleCreated) return; item.BeginInvoke(new Action(() => item.SelectionLength = 0)); };
              item.Font = new Font("Consolas", fontSize, FontStyle.Italic);
              item.Text = text;
              item.Location = new System.Drawing.Point(X, Y);
              item.Width = width;
              item.Height = height;
              item.DrawMode = DrawMode.OwnerDrawFixed;

              item.DrawItem += (s, e) => {
                  e.DrawBackground();
                   if(e.Index == 1 && (e.State & DrawItemState.Selected) == DrawItemState.Selected)
                      e.Graphics.DrawString(item.Items[e.Index].ToString(), item.Font, Brushes.Red, e.Bounds);
                   else
                      e.Graphics.DrawString(item.Items[e.Index].ToString(), item.Font, Brushes.Black, e.Bounds);
              };

               MyForm.Controls.Add(item);
           return ;
        }

        public void InsertTextIntoContainer(Form Dashie, Panel PanelAdding, string Text, int width, int height, int X, int Y, int FontSize, int fr, int fg, int fb) {
                TextBox TextID = new TextBox();

                 TextID.ForeColor = Color.FromArgb(fr, fg, fb);
                 TextID.BackColor = Color.FromArgb(0, 35, 91);

                 TextID.BorderStyle = BorderStyle.None;

                 TextID.Text      = Text;
                 TextID.AutoSize  = false;
                 TextID.Multiline = true;
                 TextID.ScrollBars = ScrollBars.None;
                 TextID.ReadOnly  = true; 
                
                 TextID.Location = new Point(X, Y);
                 
                 TextID.Width  = (Dashie.Width);
                 TextID.Height = (Dashie.Height-75);  

                 TextID.Font = new Font("Consolas", FontSize, FontStyle.Bold);

                PanelAdding.Controls.Add(TextID);
            return ;
        }

        public void SetTextBox(Form MyForm, TextBox item, string Text, int X, int Y, int width, int height, int fontSize, int maxLength, int br, int bg, int bb, int fr, int fg, int fb) {
              item.BackColor = Color.FromArgb(br, bg, bb);
              item.ForeColor = Color.FromArgb(fr, fg, fb);
              item.Font = new Font("Consolas", fontSize);
              item.Location = new System.Drawing.Point(X, Y);
              item.Width = width;
              item.Height = height;  
              item.BorderStyle = BorderStyle.None;
              item.Text = Text;
              item.MaxLength = maxLength;  

               MyForm.Controls.Add(item);
           return ;
        }

        public void SetMenuItem(ToolStripItem item, int width, int height, bool enableCustomFont, int fontSize, int br, int bg, int bb, int fr, int fg, int fb) {
              item.BackColor = Color.FromArgb(br, bg, bb);
              item.ForeColor = Color.FromArgb(fr, fg, fb);
              item.DisplayStyle = ToolStripItemDisplayStyle.Text;
              item.Font = new Font("Consolas", fontSize); 
              item.BackColor = Color.FromArgb(22, 23, 35); //71, 0, 15);
              item.AutoSize  = true;
              item.TextAlign = ContentAlignment.MiddleCenter;
           return ;
        }

        public void ColorButton(Button theButton, int br, int bg, int bb, int fr, int fg, int fb) {
            theButton.ForeColor = Color.FromArgb(fr, fg, fb);
            theButton.BackColor = Color.FromArgb(br, bg, bb);
            theButton.FlatAppearance.BorderColor = Color.FromArgb(br, bg, bb);
            theButton.FlatAppearance.BorderSize = 0;
        }

        public void CreateButton(Form MyForm, bool bImageAlign, Button myButton, bool bNameAlign, string Name, string bIcon, bool bIconAlign, int fSize,int bBorder, int X,int Y,int bWidth, int bHeight, int br, int bg, int bb, int fr, int fg, int fb) {
                 myButton.BackColor = System.Drawing.Color.FromArgb(br, bg, bb);
                 myButton.ForeColor = System.Drawing.Color.FromArgb(fr, fg, fb);
                 myButton.Width    = bWidth;
                 myButton.Height   = bHeight;
                 myButton.Location = new System.Drawing.Point(X, Y);
            
                if(bIcon != String.Empty) {
                         System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                         myButton.Image = (Image) loadRes.GetObject(bIcon);
                     
                        if(bIconAlign == true)
                            myButton.ImageAlign = ContentAlignment.MiddleCenter;
                }
                
                 myButton.Text = Name;
            
                if(bNameAlign == true)
                    myButton.TextAlign = ContentAlignment.MiddleCenter;
            
                 myButton.FlatStyle = FlatStyle.Flat;
                 myButton.Font      = new Font("Arial", fSize);
                 myButton.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(br, bg, bb);
                 myButton.FlatAppearance.BorderSize  = 0;
            
                MyForm.Controls.Add(myButton);
            return ;
        }
    
        public void CreateSubMenu(Form MyForm, ToolStripMenuItem SubMenu, MenuStrip MainMenu, string ItemName, string ToolTipText, bool alignLeft, bool alignRight, bool enableImage, string image, int X, int Y, int font, int width, int height, int Xx, int Yy, int br, int bg, int bb, int fr, int fg, int fb) {
                 SubMenu.AutoSize = false;
                 SubMenu.Margin  = new System.Windows.Forms.Padding(Xx, Yy, Xx, Yy);
                 MainMenu.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
                
                if(alignLeft == true)
                         SubMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Left;
                
                if(alignRight == true)
                         SubMenu.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
                
                if(enableImage == true) {
                         System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                         Image myImg   = (Image) loadRes.GetObject(image);
                         // Bitmap bitmup = new Bitmap(myImg, new System.Drawing.Size(X, Y)); // new Size(X, Y);
                         
                         SubMenu.Image        = myImg;
                         SubMenu.ImageScaling = ToolStripItemImageScaling.SizeToFit;
                         SubMenu.Size         = new Size(X-1, Y);
                } else {
                         SubMenu.ForeColor = Color.FromArgb(fr, fg, fb);
                         SubMenu.Width  = width;
                         SubMenu.Height = height;                         
                         SubMenu.Text = ItemName;
                         SubMenu.Font = new Font("Arial", font);
                         SubMenu.TextAlign = ContentAlignment.MiddleCenter;
                }
            
                 MainMenu.Items.Add(SubMenu);
               MyForm.Update();
            return ;
        }
    
        public void CreateMenu(Form MyForm, MenuStrip MainMenu, string MenuName, int r, int g, int b) {
                 MainMenu.BackColor = System.Drawing.Color.FromArgb(r, g, b);
             
                 MainMenu.AutoSize = false;
                 MainMenu.CanOverflow = true;
                 MainMenu.Height   = 26; //26; //25;
                 MainMenu.Name     = MenuName;
                 MyForm.MainMenuStrip = MainMenu;
                
                MainMenu.Update();
                MyForm.Controls.Add(MainMenu);
            return ;
        }
        
        public void WriteToFile(string Out, string FileName) {
               StreamWriter Outgoing = new StreamWriter(FileName);

                Outgoing.WriteLine(Out);
                Outgoing.Close();
        }

        public void SaveFileAs(string Data) {
               SaveFileDialog SaveFileAs = new SaveFileDialog();
              
              SaveFileAs.Filter = "Normal Text File|*.txt|DashCore File|*.dcore|DashText File|*.dtxt|DashSource File|*.dsrc|Anything|*.*";
              SaveFileAs.Title  = "Save the given DNS Server List to a File!";
              SaveFileAs.ShowDialog();

                if((SaveFileAs.FileName != "") || (SaveFileAs.FileName != String.Empty))
                    WriteToFile(Data, Path.GetFullPath(SaveFileAs.FileName));
                else
                    WriteToFile(Data, "Dashies PGDNSSL.txt");

            return ;
        }

        public void LoadImage(Form MyForm, string Resource, int X, int Y, int Width, int Height, int type) {
               PictureBox loadImg = new PictureBox();
               System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 loadImg.Image    = (Image)loadRes.GetObject(Resource);
                 loadImg.Location = new Point(X, Y);
                 loadImg.Size     = new Size(Width, Height);
                 loadImg.SizeMode = PictureBoxSizeMode.AutoSize;
                 loadImg.BringToFront();
                 
                switch(type) {
                   case 1:{
                        loadImg.Click += (sender, args) => { MessageBox.Show("Bottom Left"); };
                     break;
                   }
                   case 2:{
                        loadImg.Click += (sender, args) => { MessageBox.Show("Bottom Right"); };
                     break;
                   }
                }
                
               MyForm.Controls.Add(loadImg);
            return ;
        }
    }
}
