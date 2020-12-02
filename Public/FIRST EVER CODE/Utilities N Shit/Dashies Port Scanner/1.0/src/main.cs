
/*

(c) All Rights Reserved, Dashies Software Inc. 
 
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Resources;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace src
{
    public partial class main : Form
    {
        Methods method_dialog = new Methods();
        Types type_dialog = new Types();

        public Boolean SetupEvents()
        {
            DashBase.Collector control = new DashBase.Collector();
            CustomForm.Inject inject = new CustomForm.Inject();

            try
            {
                control.Buttons[0].Click += (sender, receiver) =>
                {
                    this.Close();
                };

                control.Buttons[1].Click += (sender, receiver) =>
                {
                    this.WindowState = FormWindowState.Minimized;
                };

                control.Buttons[2].Click += (sender, receiver) =>
                {
                    // start
                };

                control.Buttons[3].Click += (sender, receiver) =>
                {
                    // abort
                };

                control.Buttons[4].Click += (sender, receiver) =>
                {
                    method_dialog.ShowDialog();
                };

                control.Buttons[5].Click += (sender, receiver) =>
                {
                    type_dialog.ShowDialog();
                };

                return true;
            }

            catch
            {
                return false;
            }
        }




        public Boolean DrawEvent()
        {
            CustomForm.Draw draw = new CustomForm.Draw();
            DashBase.Collector.Configurations.Button database = new DashBase.Collector.Configurations.Button();

            try
            {
                this.Paint += (magnificant, dashie) =>
                {
                    draw.Rectangle(dashie, 350, 281, this.Width - 351, 24, 2, 4, 4, 4);
                    draw.Rectangle(dashie, this.Width, this.Height - 24, 0, 24, 2, 8, 8, 8);

                    draw.Rectangle(dashie, 325, 18, 20, 50, 2, 4, 4, 4);
                    draw.Rectangle(dashie, 325, 18, 20, 76, 2, 4, 4, 4);
                };

                return true;
            }

            catch
            {
                return false;
            }
        }




        public Boolean FormSetup()
        {
            MainConfiguration config = new MainConfiguration();

            try
            {
                this.BackgroundImage = null;
                this.Text = config.Title;

                if(config.IsCentered == true)
                {
                    this.StartPosition = FormStartPosition.CenterScreen;
                }

                else
                {
                    this.Location = new Point(config.Coordination[0], config.Coordination[1]);
                }

                if(config.IsBordered == false)
                {
                    this.FormBorderStyle = FormBorderStyle.None;
                }

                else
                {
                    this.FormBorderStyle = FormBorderStyle.Fixed3D;
                }

                ResourceManager access_embeded = new ResourceManager((config.Embeded_Key), Assembly.GetExecutingAssembly());

                this.BackColor = Color.FromArgb(config.RGB[0], config.RGB[1], config.RGB[2]);
                this.Icon = (Icon) access_embeded.GetObject(config.Icon);
                this.Size = new Size(config.Size[0], config.Size[1]);

                CustomForm.Inject inject = new CustomForm.Inject();
                inject.AnimatedCursor(this, (byte[])access_embeded.GetObject("cursor"));

                return true;
            }

            catch
            {
                return false;
            }
        }



        public Boolean ApplyText(TextBox Interpret, string Contents)
        {
            try
            {
                this.Invoke((MethodInvoker) delegate {
                    Interpret.AppendText(Contents);    
                });

                return true;
            }

            catch
            {
                return false;
            }
        }




        public Boolean SetupControls()
        {
            try
            {
                DashBase.Collector.Configurations.Button b = new DashBase.Collector.Configurations.Button();
                DashBase.Collector dashbase = new DashBase.Collector();

                MainConfiguration config = new MainConfiguration();
                CustomForm.Inject control = new CustomForm.Inject();

                int tag = 0, rgb = 0, size = 0, point = 0, fontsize = 0;

                b.Point[0] = Width - (b.Size[0] * 1);
                b.Point[2] = Width - (b.Size[2] * 2);

                foreach (Button Id in dashbase.Buttons)
                {
                    control.InjectButton(this, true, Id, false, b.Tags[tag], b.FontSize[fontsize], b.Point[point], b.Point[point + 1], b.Size[size + 0], b.Size[size + 1], b.RGB[rgb + 0], b.RGB[rgb + 1], b.RGB[rgb + 2], b.RGB[rgb + 3], b.RGB[rgb + 4], b.RGB[rgb + 5], b.RGB[rgb + 6], b.RGB[rgb + 7], b.RGB[rgb + 8], b.RGB[rgb + 9], b.RGB[rgb + 10], b.RGB[rgb + 11]);

                    rgb += 12;
                    tag += 1;
                    size += 2;
                    point += 2;
                }

                control.InjectTextBox(this, true, dashbase.Log, dashbase.LogStartMessage(), true, false, false, true, 8, this.Width-351, 25, 350, 179, 4, 4, 4, 170, 170, 170);

                control.InjectTextBox(this, true, dashbase.Target, "http://www.google.co.uk", false, false, false, false, 11, 20, 50, 325, 18, 8, 8, 8, 255, 255, 255);
                control.InjectLabel(this, true, dashbase._Target, "target", false, false, 10, dashbase.Target.Left + dashbase.Target.Width + 10, dashbase.Target.Top, 75, 24, config.RGB[0], config.RGB[1], config.RGB[2], 255, 255, 255);

                control.InjectTextBox(this, true, dashbase.Port, "80, 443, 22, 21, 2222, 8080", false, false, false, false, 11, 20, 76, 325, 18, 8, 8, 8, 255, 255, 255);
                control.InjectLabel(this, true, dashbase._Port, "port(s)", false, false, 10, dashbase.Port.Left+dashbase.Port.Width+10, dashbase.Port.Top, 75, 24, config.RGB[0], config.RGB[1], config.RGB[2], 255, 255, 255);

                control.Menu(this, true, true, true, "Profile_Picture", 0, 0, 23, 24, 0, 0, this.Width, 24, 16, 16, 16);

                return true;
            }

            catch
            {
                return false;
            }
        }




        public main()
        {
            InitializeComponent();

            SplashScreenConfiguration Config = new SplashScreenConfiguration();
            MainConfiguration config = new MainConfiguration();

            ResourceManager access_embeded = new ResourceManager((Config.Embeded_Key), Assembly.GetExecutingAssembly());

            this.Size = new Size(Config.Size[0], Config.Size[1]);
            this.Icon = (Icon) access_embeded.GetObject(config.Icon);

            CustomForm.Inject inject = new CustomForm.Inject();
            
            inject.AnimatedCursor(this, (byte[]) access_embeded.GetObject("cursor"));

            if(Config.IsBordered != true)
            {
                this.FormBorderStyle = FormBorderStyle.None;
            }

            if(Config.IsCentered == true)
            {
                this.StartPosition = FormStartPosition.CenterScreen;
            }

            else
            {
                this.Location = new Point(Config.Coordination[0], Config.Coordination[1]);
            }

            this.Text = Config.Title;

            if(Config.IsResource == true)
            {
                ResourceManager Embeded_Access = new ResourceManager((Config.Embeded_Key), Assembly.GetExecutingAssembly());

                this.BackgroundImage = (Image)Embeded_Access.GetObject(Config.Image);
            }

            else
            {
                this.BackgroundImage = Image.FromFile(Config.Image);
            }

            MainFrameRuntimeTypeShit();
        }




        public void MainFrameRuntimeTypeShit()
        {
            System.Timers.Timer waitfor = new System.Timers.Timer();

            waitfor.Elapsed += (sender, receiver) =>
            {
                waitfor.Enabled = false;
                waitfor.Dispose();

                try
                {
                    List<Boolean> Check = new List<Boolean>() { FormSetup(), SetupEvents(), DrawEvent(), SetupControls() };

                    foreach(Boolean Id in Check)
                    {
                        if(Id == false)
                        {
                            MessageBox.Show("= oof, " + Id.ToString() + " has returned false!");
                            this.Close();
                        }
                    }
                }

                catch
                {
                    MessageBox.Show("oops, an error occurred.");
                    this.Close();
                }

                finally
                {
                    Closing += (flower, pot) =>
                    {
                        DashBase.Collector database = new DashBase.Collector();

                        foreach(Pen Id in database.drawing)
                        {
                            try
                            {
                                Id.Dispose();
                            }

                            catch
                            {
                                MessageBox.Show("we were unable to safely destroy this process.", "Oof");
                            }
                        }

                        foreach(Control Id in database.database)
                        {
                            try
                            {
                                Id.Dispose();
                            }

                            catch
                            {
                                MessageBox.Show("we were unable to safely destroy this process.", "Oof");
                            }
                        }
                    };
                }
            };


            waitfor.Interval = 1500;
            waitfor.Enabled = true;
        }
    }
}
