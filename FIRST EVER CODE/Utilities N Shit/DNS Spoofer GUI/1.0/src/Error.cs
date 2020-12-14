
/* 

  (c) All Rights Reserved, Dashies Software Inc.
 
  Error Dialog Module Thingy xD
 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pony_Spoofer_GUI {
    public partial class Error : Form {
      public string Error_Text, Error_Title, Button_Text;

      public const int WM_NCLBUTTONDOWN = 0xA1;
      public const int HT_CAPTION = 0x2;

      [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
      public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

      [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
      public static extern int ReleaseCapture();

      public int Button_Type = 0,
                 MD_OKCANCEL = 1,
                 MD_YESNO = 2, 
                 MD_NAW = 3,
                 MD_OK = 4,
                 MD_OKAY = 5,
                 MD_CLOSE = 6,
                 MD_CUSTOM = 7;

        private void Move_Window(object sender, System.Windows.Forms.MouseEventArgs e) {
            if(e.Button == MouseButtons.Left) {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        public void Modify_Dialog(string Title, string Text, int Button, string B_Title) {
            InitializeComponent();
             
             this.ShowDialog();

              if(Button == 0) Button_Type = 0;
              else Button_Type = Button;

              if((Title == String.Empty) || (Title == "")) Error_Title = "No Title Set";
              else Error_Title = Title;

              if((Text == String.Empty) || (Text == "")) Error_Text = "No Error Set";
              else Error_Text = Text;

             this.MaximumSize = new Size(250, 200);
             this.MinimumSize = new Size(250, 200);

             this.MaximizeBox = false;
             this.MinimizeBox = false;
             this.ControlBox  = false;
               
             this.Top =- 50;

             this.StartPosition   = FormStartPosition.CenterParent;
             this.FormBorderStyle = FormBorderStyle.None;

             this.MouseDown += new MouseEventHandler(Move_Window);
             this.BackColor = Color.FromArgb(24, 31, 43);
             this.Size = new System.Drawing.Size(250, 200);

              if(Button == MD_OKCANCEL)
                  
              if(Button == MD_YESNO)
                  
              if(Button == MD_NAW)
                  
              if(Button == MD_OKAY)
                  
              if(Button == MD_CLOSE)
                  
              if(Button == MD_OK)
                  
              if(Button == MD_CUSTOM)
               
            return ;
        }
 
        public Error() {}
    }
}
