
/* (c) All Rights Reserved, Dashies Software Inc. */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace src {
    public partial class Dialog : Form {
        Button Okay = new Button(),
               SaveToFile = new Button();

        Label Output = new Label();

        core Dashies_Core = new core();

        int Form_Width=0, Form_Height=0;

        String Text_Header = (
           "(c) All Rights Reserved, Dashies Software Inc.\r\n\r\n"+
           "IP Detection Results in a file \\(^o^)/\r\n\r\n\r\n"
        );

        public Dialog(String Data, String sDialogTitle, bool Center, int X, int Width) {
            InitializeComponent();

             Output.Text = (Data);
             Output.AutoSize = true;
             Output.Update();
             
            this.Controls.Add(Output);

             Form_Height = ((Output.Height)+65);
             Form_Width = (350+Width);


            this.Paint += (Magnificant, Objectivionas) => {
            
            };

            this.BackColor = Color.FromArgb(35, 31, 31);
            
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            this.Size = new Size(Form_Width, Form_Height);

            this.MaximumSize = new Size(Form_Width, Form_Height);
            this.MinimumSize = new Size(Form_Width, Form_Height);

            this.MinimizeBox = false;
            this.MaximizeBox = false;

            Okay.Click += (Beautiful_Angel, Objective) => {
                this.Close();
            };
            
            SaveToFile.Click += (Beautiful_Shineyah, Objective) => { 
                Dashies_Core.SaveFileAs((Text_Header+
                    Data), (sDialogTitle), (Dashies_Core.DEFAULT_FILTER_SET));
            };

            Okay.MouseEnter += (Beautiful, Entrance) => {
                Okay.BackColor = Color.FromArgb(40, 40, 40);
            };

            Okay.MouseLeave += (Enchantment, Magnificant) => { 
                Okay.BackColor = Color.FromArgb(7, 7, 7); 
            };

            SaveToFile.MouseEnter += (Dash_Shit, BV) => { 
                SaveToFile.BackColor = Color.FromArgb(40, 40, 40);
            };

            SaveToFile.MouseLeave += (Die, Live) => { 
                SaveToFile.BackColor = Color.FromArgb(7, 7, 7); 
            };

            if(Center == true) Dashies_Core.WriteText(this, Output, (Data), Dashies_Core.DEFAULT_FONT_TYPE, 8, Dashies_Core.DEFAULT_FONT_WEIGHT, 0, 0, true, 0, 15, 35, 31, 31, 255, 255, 255);
            else Dashies_Core.WriteText(this, Output, (Data), Dashies_Core.DEFAULT_FONT_TYPE, 8, Dashies_Core.DEFAULT_FONT_WEIGHT, 0, 0, false, X, 15, 35, 31, 31, 255, 255, 255);

            Dashies_Core.CreateButton(this, Okay, ("Okay"), Dashies_Core.DEFAULT_FONT_TYPE, 10, 0, false, (this.Width-(this.Width-20)), this.Height-30, 135, 24, 7, 7, 7, 255, 255, 255);
            Dashies_Core.CreateButton(this, SaveToFile, ("Save As"), Dashies_Core.DEFAULT_FONT_TYPE, 10, 0, false, (this.Width-155), this.Height-30, 135, 24, 7, 7, 7, 255, 255, 255);
        }
    }
}
