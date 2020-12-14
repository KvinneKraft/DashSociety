using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Resources;
using System.Reflection;
using System.Runtime.InteropServices;

namespace src
{
    public partial class Notification_UI : Form
    {
        private Dash_Library toolset = new Dash_Library();
        private Tools tools = new Tools();

        private TextBox Log = new TextBox();
        private Button Quit = new Button();

        public String WhatIsNew()
        {
            return 
                (
                    "<[=====================================================]>" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | oof, there are a lot of things that me, dashie has  |" + "\r\n" +
                    " | implemented this update, but there are just a few   |" + "\r\n" +
                    " | things that really matter, as follows lol, yeh!!!   |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | 1) this is probably one of the newest and biggest   |" + "\r\n" +
                    " |    changes of them all, i have managed to implement |" + "\r\n" +
                    " |    my custom version of the .NET Framework for this |" + "\r\n" +
                    " |    update, a custom damn Framework, wew, yeh!       |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | 2) IPv4 Support, it took a darn while but here it   |" + "\r\n" +
                    " |    is, in all of its glory, currently we only have  |" + "\r\n" + 
                    " |    a few methods that support this, but there will  |" + "\r\n" +
                    " |    be waaay more in the near future!                |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | 3) Improved Packet Transfer, yes I have managed to  |" + "\r\n" +
                    " |    implement a technique which allows users to send |" + "\r\n" +
                    " |    a lot of data to a certain extent, while         |" + "\r\n" +
                    " |    maintaining a low ping, i am going to implement  |" + "\r\n" +
                    " |    a caching method very soon, that will allow you  |" + "\r\n" +
                    " |    to basically send 8x as much!                    |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | 4) Optimizations, yes i dashie have optimized the   |" + "\r\n" +
                    " |    codes even more than before, now they are going  |" + "\r\n" +
                    " |    to run as smooth as a hot knife cutting through  |" + "\r\n" +
                    " |    butter.                                          |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | and basically a lot more things that you may go and |" + "\r\n" +
                    " | find by yourself, little functionality extensions   |" + "\r\n" +
                    " | and implementation techniques you have not seen     |" + "\r\n" +
                    " | yet, or maybe just some little tweaks to enchance   |" + "\r\n" +
                    " | your IP Stresser Experience c:                      |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | even tho the updates listed above do not seem that  |" + "\r\n" +
                    " | much like an improvement, but if you were to know   |" + "\r\n" +
                    " | how much time i have put into this shitty project x |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    " | -The Amazing, and beautiful DASHIE!!!!              |" + "\r\n" +
                    " |                                                     |" + "\r\n" +
                    "<[=====================================================]>" + "\r\n"
                );
        }

        public Notification_UI(int Id)
        {
            try
            {
                if(InitializeNotificationUI() == false)
                {
                    MessageBox.Show("we were unable to initialize the dialog that you requested, please report this bug to the developers as this is something that should not happen. sorry!", "FATAL ERROR");
                    this.Close();
                } else
                {
                    toolset.InjectInputBox(this, "", Log, 0, 21, this.Width, this.Height - 21, 9, 22, 22, 22, 180, 180, 180);
                    Log.VisibleChanged += (sender, argumentation) =>
                    {
                        if (Log.Visible)
                        {
                            Log.SelectionStart = Log.Text.Length;
                            Log.ScrollToCaret();
                        }
                    };

                    Log.ScrollBars = ScrollBars.Vertical;
                    Log.Multiline = true;
                    Log.HideSelection = true;
                    Log.AcceptsTab = true;
                    Log.WordWrap = true;
                    Log.ReadOnly = true;

                    toolset.InjectButton(this, Quit, "X", this.Width - 74, -3, 74, 24, 12, 16, 16, 16, 255, 255, 255, 24, 24, 24, 255, 255, 255);
                    Quit.Click += (sent, MsThiccLeg) => 
                    {
                        this.Close();  
                    };

                    switch(Id)
                    {
                        case 1:
                            {
                                Log.AppendText(WhatIsNew());
                                break;
                            }

                        default:
                            {
                                Log.AppendText("* unrecognized id received for text display ;(");
                                break;
                            }
                    }
                }
            }
            catch
            {
                Log.AppendText("* Well, that is unfortunate, we were unable to load the update log ;(");
                Log.AppendText("* Please try again c:");
            }
        }

        private Boolean InitializeNotificationUI()
        {
            this.Size = new Size(425, 500);
            if(this.Size != new Size(425, 500))
            {
                return false;
            }

            ResourceManager Addr = new ResourceManager(("src.embeded"), Assembly.GetExecutingAssembly());
            tools.SetFormCursor(this, (Byte[])Addr.GetObject("main"));

            this.Text = "Ʊ";
            this.Icon = (Icon)Addr.GetObject("icon");

            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = FormStartPosition.CenterParent;

            this.BackColor = Color.FromArgb(16, 16, 16);
            this.ForeColor = Color.FromArgb(255, 255, 255);

            return true;
        }
    }
}
