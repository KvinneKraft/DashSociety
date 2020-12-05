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
    public partial class Notify : Form
    {
        Inject injection = new Inject();
        Inject.Cawntrol control = new Inject.Cawntrol();
        Inject.Function function = new Inject.Function();
        Inject.Layout layout = new Inject.Layout();

        Label Descriptiveness = new Label();
   
        ResourceManager embeded = new ResourceManager("src.Embeded", Assembly.GetExecutingAssembly());

        String Messagea = ("Hey, the current version of this application is a premade\r\n" +
                            "release, We have not yet implemented advanced technologies\r\n" +
                            "besides a simple TCP(Transmision Control Protocol) Scanner\r\n" +
                            "Module which works for both URLs and IPv4 Addresses.");

        public void BackgroundThread()
        {
            System.Threading.Thread.Sleep(5000);
            this.Close();
        }

        public Notify()
        {
            Task sleeper = Task.Run((Action) BackgroundThread);
  
            FormBorderStyle = FormBorderStyle.None;
            StartPosition = FormStartPosition.CenterParent;

            control.Label(this, Descriptiveness, Messagea, "Modern", 14, true, false, 10, 10, 0, 0, 32, 32, 32, 255, 255, 255);

            int W = Descriptiveness.Width + 20, H = Descriptiveness.Height + 20;

            Size = new Size(W, H);
            MaximumSize = new Size(W, H);
            MinimumSize = new Size(W, H);

            BackColor = Color.FromArgb(32, 32, 32);

            Icon = (Icon)embeded.GetObject("Logo");
            Text = "Ze Notification Dialog";

            function.AnimatedCursor(this, (byte[])embeded.GetObject("cursor"));

            
        }
    }
}
