
/* (c) All Rights Reserved, Dashies Software Inc. */

using System;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace Pony_Spoofer_GUI {
	public partial class Website_Dialog : Form {
		public Label TOS = new Label();
		public string resKey;

        Dash_Lib DashCore = new Dash_Lib();
		
		public Website_Dialog() {
                InitializeComponent();
                // remove all the Startup() thingies
               Button Okay = new Button(), Nuuu = new Button();
                 
                 resKey = "Pony_Spoofer_GUI.Embeded";
               
                System.Resources.ResourceManager loadRes = new System.Resources.ResourceManager(resKey, System.Reflection.Assembly.GetExecutingAssembly());
                
                 this.MaximumSize = new Size(350, 295);
                 this.MinimumSize = new Size(350, 295);
                 this.MaximizeBox = false;
                 this.MinimizeBox = false;
                 
                 this.Top             = -50; //new Point(137, 35);
                 this.StartPosition   = FormStartPosition.CenterParent; //(137, 35);
                 this.FormBorderStyle = FormBorderStyle.None;
                 this.Size            = new System.Drawing.Size(350, 295); 
                 
                 this.ControlBox = false;
                 this.BackColor  = Color.FromArgb(30, 40, 56);
                 
                 Okay.Click += (sender, args) => { ExecGoFundMe("http://www.dashware-software.co.uk"); this.Close(); };
                 TOS.Click  += (sender, args) => { Terms_Of_Service showTOS = new Terms_Of_Service(); showTOS.ShowDialog(); };
                 
                DashCore.LoadImage(this, "puggy", 306, 10, 50, 50, 1);
                DashCore.LoadImage(this, "Website Dialog", -15, 245, 64, 64, 0);
                DashCore.LoadImage(this, "Website Dialog 2", 285, 246, 48, 48, 0);
                
                DashCore.WriteText(this, "Dashies Software Repository", false, 50, 15, 14, 255, 255, 255);
                DashCore.WriteText(this, "our website has been made purely to share our\namazing and interesting projects and we are not\nresponsible for what you do with the things that\nwe offer on our website, please keep that in mind!", false, 16, 65, 10, 255, 255, 255);
                DashCore.WriteText(this, "press Okay to launch the website in your default\nweb browser or press Nuuu to do the opposite.", false, 42, 145, 9, 255, 255, 255);
                DashCore.WriteTextEx(this, TOS, "when visiting our website you automaticly accept the TOS.", false, 33, 200, 7, 109, 165, 255, 255, 255, 255);
                
                DashCore.CreateButton(this, false, Okay, true, "Okay", String.Empty, false, 9, 0, 110, 250, 135, 30, 79, 58, 109, 255, 255, 255);
        }
        
        public void ExecGoFundMe(string URL) {
               Process execURL = new Process();
               
                 execURL.StartInfo.UseShellExecute = true;
                 execURL.StartInfo.FileName        = URL;
                 execURL.Start();
                
            return ;
        }		
    }
}
