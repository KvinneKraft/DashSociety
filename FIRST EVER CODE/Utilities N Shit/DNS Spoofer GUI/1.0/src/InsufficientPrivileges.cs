
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

namespace Pony_Spoofer_GUI {
    public partial class InsufficientPrivileges : Form {
      public string resKey;
      public Button Mkay = new Button();
      
      Dash_Lib DashCore = new Dash_Lib();

        public InsufficientPrivileges() {
           InitializeComponent();

            resKey = "Pony_Spoofer_GUI.Embeded";
            System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());

            this.MaximumSize = new Size(258, 225);
            this.MinimumSize = new Size(258, 225);
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.Top = -50; //new Point(137, 35);
            this.StartPosition = FormStartPosition.CenterParent; //(137, 35);
            this.FormBorderStyle = FormBorderStyle.None;
            this.Size = new System.Drawing.Size(250, 200);
            this.BackColor = Color.FromArgb(48, 51, 53); //104, 105, 168);
            //this.ForeColor = Color.FromArgb(255, 255, 255);

            Mkay.Click += (sender, e) => { this.Close(); };

            DashCore.WriteText(this, "You are running this application\nas a none administrator!", false, 0, 4, 13, 255, 255, 255);
            DashCore.WriteText(this, "Please retry by running this application\nas an administrator upon next startup\nit may fix the issue!", false, 1, 80, 11, 255, 255, 255);

            DashCore.LoadImage(this, "Privilege Dialog", -10, 168, 64, 64, 000);
            DashCore.LoadImage(this, "Privilege Dialog 2", 210, 163, 64, 64, 000);

            DashCore.CreateButton(this, false, Mkay, true, "Mkay c:", String.Empty, true, 8, 0, 79, 188, 100, 28, 79, 58, 109, 255, 255, 255);
        }
    }
}
