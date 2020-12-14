/* (c) All Rights Reserved, Dashies Software Inc. */

using System;
using System.Drawing;
//using System.Resources;
//using System.Reflection;
using System.Windows.Forms;
//using System.Collections.Generic;

namespace Pony_Spoofer_GUI {
    public partial class Copyright_Dialog : Form {
      string resKey;
        public Copyright_Dialog() {
                InitializeComponent();
               
               Dash_Lib DashCore = new Dash_Lib();
               Button Okay = new Button();
                 
                 resKey = "Pony_Spoofer_GUI.Embeded";
               
                System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 this.MaximumSize = new Size(350, 305);
                 this.MinimumSize = new Size(350, 305);
                 this.MaximizeBox = false;
                 this.MinimizeBox = false;
                 
                 this.Top             = -50; //new Point(137, 35);
                 this.StartPosition   = FormStartPosition.CenterParent; //(137, 35);
                 this.FormBorderStyle = FormBorderStyle.None;
                 this.Size            = new System.Drawing.Size(350, 305); 
                 
                 this.ControlBox = false;
                 this.BackColor  = Color.FromArgb(30, 40, 56);
                 
                 this.Update();
                 
                 Okay.Click += (sender, args) => { this.Close(); };
                
                DashCore.LoadImage(this, "Copyright Dialog 2", 0, 248, 50, 50, 1);
                DashCore.LoadImage(this, "Copyright Dialog", 286, 245, 64, 64, 0);
                DashCore.LoadImage(this, "Heart", 285, 1, 48, 48, 0);
                
                DashCore.WriteText(this, "Pony Spoofer GUI Copyright", false, 40, 20, 14, 255, 255, 255);
                DashCore.WriteText(this, "this wonderful application has been built by \ndashie and texas pony from Dashies Software. \n\nreproduction of this application without any of \nthe developer(s) their permission is not \npermitted and will surely and truly result in \na horrible unacceptable punishment. \n\n(c) All Rights Reserved, Dashies Software Inc.", false, 24, 70, 10, 255, 255, 255);
                
                DashCore.CreateButton(this, false, Okay, true, "Okay", String.Empty, false, 9, 0, 110, 260, 135, 30, 79, 58, 109, 255, 255, 255);
        }
    }
}
