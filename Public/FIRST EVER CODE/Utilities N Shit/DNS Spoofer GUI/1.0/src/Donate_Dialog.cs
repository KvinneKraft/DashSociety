
/* (c) All Rights Reserved, Dashies Software Inc. */

using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Pony_Spoofer_GUI {
	public partial class Donate_Dialog : Form {
	  public string resKey;
	  public Label TOS = new Label();
	  Dash_Lib DashCore = new Dash_Lib();

        public void ExecGoFundMe(string URL) {
               Process execURL = new Process();
               
                 execURL.StartInfo.UseShellExecute = true;
                 execURL.StartInfo.FileName        = URL;
                 execURL.Start();
                
            return ;
        }

		public Donate_Dialog() {
               InitializeComponent();
               Button Donate = new Button(), Exit = new Button();
                 
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
                 
                 TOS.Click         += (sender, args) => { Terms_Of_Service TAWS = new Terms_Of_Service(); TAWS.ShowDialog(); };
                 Donate.Click      += (sender, args) => { ExecGoFundMe("https://www.gofundme.com/dashies-software-projects/donate"); }; //new EventHandler(ExecGoFundMe); //(sender, args) => {  }; // temp
                 Exit.Click        += (sender, args) => { this.Close(); };
                 
                DashCore.LoadImage(this, "Donate Dialog", -4, 245, 50, 50, 1);
                DashCore.LoadImage(this, "Donate Dialog 2", 290, 245, 64, 64, 0);
                DashCore.LoadImage(this, "Coin", 300, 1, 48, 48, 0);
                
                DashCore.WriteText(this, "Donate to Dashies Software", false, 51, 20, 14, 255, 255, 255);
                DashCore.WriteText(this, "we do not require you to donate to us but it is always \nwelcome as we will use the donations for our application \ndevelopment and other things related to our projects.", false, -3, 70, 10, 255, 255, 255);
                DashCore.WriteText(this, "you can donate any amount starting from 5 Bucks\n(Dollars) and also please know that donated funds \nare not refundable.", false, 12, 128, 10, 255, 255, 255);
                
                DashCore.WriteTextEx(this, TOS, "by donating you automaticly accept the TOS.", false, 69, 200, 8, 109, 165, 255, 255, 255, 255);
                
                DashCore.CreateButton(this, false, Donate, true, "YASH", String.Empty, false, 9, 0, 60, 250, 115, 30, 79, 58, 109, 255, 255, 255);
                DashCore.CreateButton(this, false, Exit, true, "DUWN", String.Empty, false, 9, 0, 185, 250, 115, 30, 79, 58, 109, 255, 255, 255); // X = 215
        }
	}
}
