
/* (c) All Rights Reserved, Dashies Software Inc. */



/* 
 
this project has been created on 13-11-2018 at 8:59 in the morning.

--------------
--------------
--------------

this application will allow you to determine specific information about 
either YOU or your beautiful System Setup!

Hardware and Software related detection technologies are implemented in
order for us to give you the best experience possible.

(c) Dashies Software 2018
 
*/



using System;
using Microsoft.Win32;
using System.Drawing;
using System.Data;
using System.Data.Sql;
using System.Net.NetworkInformation;
using System.Security.Principal;
using System.Text;
using System.Linq;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Resources;
using System.Reflection;
using System.Diagnostics;
using System.Windows.Forms;
using System.Management;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web;



namespace DD_2._0 {
    class Tools {
        [DllImport("user32.dll")]
        static extern IntPtr CreateIconFromResource(byte[] presbits, uint dwResSize, bool fIcon, uint dwVer);

        public void SetFormCursor(Form Inject, byte[] Target) {
            IntPtr GET = CreateIconFromResource(Target, (uint)Target.Length, false, 0x00030000);
            Cursor SET = new Cursor(GET);
            Inject.Cursor = SET;
            return ;
        }
    }



    public partial class Client : Form {
        private Dash_Library get = new Dash_Library();
        private Tools tools = new Tools();
        private Control_Lib control = new Control_Lib();


        private struct Debug {
            public Boolean status;
        };


        public Button hideshow = new Button(), quit = new Button(),
                      software = new Button(), hardware = new Button(), network = new Button(), information = new Button();

        private PictureBox department = new PictureBox();
        private Debug debug;



        private void Startup() {
            ResourceManager resMang = new ResourceManager((get.resourceId), Assembly.GetExecutingAssembly());

            this.Icon = (Icon)resMang.GetObject("Main");
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = Color.FromArgb(165, 199, 255);
            this.Text = "I ❤ D✿SH";

            department.MouseMove += (sender, argumentation) => { control.MiceMove(this, argumentation); };
            department.MouseDown += (sender, argumentation) => { control.MiceDown(argumentation);  };

            tools.SetFormCursor(this, (Byte[])resMang.GetObject("cursor_b"));

            debug.status = get.InjectButton(this, quit, ("✕"), (this.Width - 64), (-5), 64, 29, 16, 83, 145, 244, 255, 255, 255, 119, 169, 249, 255, 255, 255);
            if(debug.status != true) {
                /* Call the Notification_UI : Form here, just because the app should atleast have a quit button. */
            } else {
                quit.Click += (sentBy, msBeauty) => {
                    this.Close();
                };
            }

            debug.status = get.InjectButton(this, hideshow, ("—"), (this.Width - 128), (-4), 64, 28, 14, 84, 145, 244, 255, 255, 255, 119, 169, 249, 255, 255, 255);
            if(debug.status != true) {
                /* Another call to our soon to come Notification_UI : Form */
            } else {
                hideshow.Click += (sentBy, msBeauty) => {
                    this.SendToBack();
                };
            }

            debug.status = get.InjectButton(this, hardware, "HARDWARE", 100, 0, 125, 24, 10, 83, 145, 244, 255, 255, 255, 119, 169, 249, 255, 255, 255);
            debug.status = get.InjectButton(this, software, "SƱFTWARE", 225, 0, 125, 24, 10, 83, 145, 244, 255, 255, 255, 119, 169, 249, 255, 255, 255);
            debug.status = get.InjectButton(this, network, "NETWƱRK", 350, 0, 125, 24, 10, 83, 145, 244, 255, 255, 255, 119, 169, 249, 255, 255, 255);
            debug.status = get.InjectButton(this, information, "INFƱRMATIƱN", 475, 0, 125, 24, 10, 83, 145, 244, 255, 255, 255, 119, 169, 249, 255, 255, 255);

            debug.status = get.CreateMenu(this, 0, 0, this.Width, 24, department, ("Profile"), 28, 24, 1, 83, 145, 244);
            if(debug.status != true) { /* Call the Notification_UI : Form here once ready */ }
        }



        public Client() {
            InitializeComponent();

            this.Hide();
            this.SendToBack();

            Splash_Screen Load = new Splash_Screen();
            Task start = Task.Run((Action)Startup);
           
            Load.ShowDialog();

            this.Show();
            this.BringToFront();
        }
    }



    public class Splash_Screen : Form {
        Dash_Library get = new Dash_Library();
        Tools tools = new Tools();

        PictureBox Splash = new PictureBox();
        ResourceManager resMang;

        void Wait() {
            System.Threading.Thread.Sleep(2500);
            this.Close();
        }

        public Splash_Screen() {
            resMang = new ResourceManager((get.resourceId), Assembly.GetExecutingAssembly());

            this.BackColor = Color.FromArgb(2, 2, 2);
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            this.Text = "PƱniessss";

            this.Size = new Size(500, 281);
            this.MaximumSize = new Size(500, 281);
            this.MinimumSize = new Size(500, 281);

            this.Icon = (Icon)resMang.GetObject("Main");

            tools.SetFormCursor(this, (byte[])resMang.GetObject("cursor_c"));
            get.InjectImage(this, Splash, 0, 0, 500, 281, ("Splash"), (""), 2, 2, 2);

            Task wut = Task.Run((Action)Wait);
        }
    }



    public class Notification_UI : Form {
        Dash_Library get = new Dash_Library();
        Tools tools = new Tools();

        ResourceManager resMang;

        public Notification_UI(Form Inject, String Error) {
            resMang = new ResourceManager((get.resourceId), Assembly.GetExecutingAssembly());

            this.Text = "Ʊ";
            this.Icon = (Icon)resMang.GetObject("Notification_UI");
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.White; // FromArgb(25, 25, 25);
            this.Cursor = (Cursor)resMang.GetObject("cursor_a");

            //tools.SetFormCursor(this, (Byte[])resMang.GetObject(""));
        }
    }
}
