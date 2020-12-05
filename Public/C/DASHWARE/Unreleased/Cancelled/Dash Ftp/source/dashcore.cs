
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Net.Sockets;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Net.NetworkInformation;



/*[======================]*/
/*[Dashies Core Namespace]*/
/*[======================]*/
namespace DashCore
{
    /*[======================]*/
    /*[The Womb of the Mother]*/
    /*[======================]*/
    public static class Program
    {
        static DashMain core;
        
        [STAThread]
        static void Main()
        {
            string processName = Process.GetCurrentProcess().ProcessName;
            int processAmount = 0;

            foreach(Process process in Process.GetProcesses())
            {
                if (process.ProcessName.Contains(processName))
                {
                    processAmount += 1;

                    if (processAmount > 1)
                    {
                        MessageBox.Show("There is already another instance of this application running in the background, please close that one first and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(-1);
                    };
                };
            };

            Application.EnableVisualStyles();
            core = new DashMain();

            using (Splashy splash = new Splashy())
            {
                new Thread(() =>
                {
                    Thread.Sleep(4000);

                    splash.Invoke((MethodInvoker)
                        delegate
                        {
                            splash.Close();

                            core.Visible = true;
                            core.WindowState = FormWindowState.Normal;
                            core.BringToFront();
                        }
                    );
                })

                { IsBackground = true };//.Start();

                //splash.ShowDialog();
            };

            Application.Run(core);
        }
    };


    /*[======================]*/
    /*[The Dash Splash Scruun]*/
    /*[======================]*/
    public class Splashy : Form
    {
        public Splashy()
        {
            try
            {
                Hide();

                FormBorderStyle = FormBorderStyle.None;
                StartPosition = FormStartPosition.CenterScreen;

                Size SplashS = new Size(250, 250);

                Size = SplashS;
                MaximumSize = SplashS;
                MinimumSize = SplashS;

                BackgroundImage = (Image)Cawntrols.furball_resources.GetObject("splashy");

                BackColor = Color.FromArgb(17, 77, 255);
                TransparencyKey = BackColor;

                using (Cawntrols cawntrols = new Cawntrols())
                {
                    cawntrols.Drag(this, this, new Point());
                };
            }

            catch (Exception e)
            {
                DashMain.ThrowErr(e);
            };
        }
    };


    /*[==========================================================]*/
    /*[The Dash MAIN CLASS YOU BLIND SHIT, can u not read or sum?]*/
    /*[==========================================================]*/
    public partial class DashMain : Form
    {
        static readonly Cawntrols Dashtolls = new Cawntrols();

        /*[==========================]*/
        /*[The Actual Core of the GUI]*/
        /*[==========================]*/
        public DashMain()
        {
            Hide();

            InitializeFoundation();
            InitializeTerminal();
            InitializeOthers();

            Key.InitializeShortcuts(this);
        }

        /*[===========================]*/
        /*[Setup all the Key Shortcuts]*/
        /*[===========================]*/
        public static class Key
        {
            private static void InjectShortcut(KeyEventArgs q)
            {
                switch (q.KeyData)
                {
                    case Keys.F4://Help Dialog Key
                    {
                        pHelpB.PerformClick();
                        break;
                    };

                    case Keys.F5://Settings Dialog Key
                    {
                        pSettingsB.PerformClick();
                        break;
                    };

                    case Keys.F7://Version Dialog Key
                    {
                        if (!version.Visible)
                        {
                            version.ShowDialog();
                        };

                        break;
                    };

                    case Keys.F8://Clear Log Key
                    {
                        tContainer.Text = "$: Dashies Ftp Client (c) 2020\r\n";
                        break;
                    };
                };
            }

            public static void InitializeShortcuts(Form Core)
            {
                foreach (Control control in Core.Controls)
                {
                    control.KeyDown += (s, q) => InjectShortcut(q);

                    if (control.Controls.Count > 0)
                    {
                        foreach (Control s1 in control.Controls)
                        {
                            s1.KeyDown += (s, q) => InjectShortcut(q);

                            if (s1.Controls.Count > 0)
                            {
                                foreach (Control s2 in s1.Controls)
                                {
                                    s2.KeyDown += (s, q) => InjectShortcut(q);
                                };
                            };
                        };
                    };
                };
            }
        };

        /*[===============================]*/
        /*[All the Separate Dialog Classes]*/
        /*[===============================]*/
        public static Diamonds.HelpDiamond help = new Diamonds.HelpDiamond();
        public static Diamonds.SettingsDiamond settings = new Diamonds.SettingsDiamond();
        public static Diamonds.VersionDiamond version = new Diamonds.VersionDiamond();

        public class Diamonds
        {
            private static List<Button> DIAMOND_BUTTONS = new List<Button>();

            public static void InitializeStandards(Form DIAMOND)
            {
                if (DIAMOND_BUTTONS.Count == 3)
                    return;

                DIAMOND.BackColor = Color.FromArgb(27, 0, 59);
                DIAMOND.ShowInTaskbar = false;

                DIAMOND.Icon = (Icon)Cawntrols.furball_resources.GetObject("icon");

                DIAMOND.FormBorderStyle = FormBorderStyle.None;
                DIAMOND.StartPosition = FormStartPosition.CenterParent;

                DIAMOND.MinimizeBox = false;
                DIAMOND.MaximizeBox = false;

                DIAMOND_BUTTONS.Add(new Button());

                Int32 BUTTON_ID = DIAMOND_BUTTONS.Count - 1;

                Dashtolls.FluffyMagicalButton(DIAMOND, DIAMOND_BUTTONS[BUTTON_ID], "Close", 12, Point.Empty, new Size(135, 28), 8, new Color[] { Color.FromArgb(52, 63, 158) }, new Color[] { Color.FromArgb(255, 255, 255) });
                
                DIAMOND_BUTTONS[BUTTON_ID].Click += (s, q) => DIAMOND.Close();
                DIAMOND_BUTTONS[BUTTON_ID].TextAlign = ContentAlignment.MiddleCenter;
                DIAMOND_BUTTONS[BUTTON_ID].Update();

                Size size = new Size(DIAMOND.PreferredSize.Width + 50, DIAMOND.PreferredSize.Height + 40);

                DIAMOND.MinimumSize = size;
                DIAMOND.MaximumSize = size;
                DIAMOND.Size = size;

                DIAMOND_BUTTONS[BUTTON_ID].Location = new Point((size.Width - DIAMOND_BUTTONS[BUTTON_ID].Width) / 2, size.Height - (DIAMOND_BUTTONS[BUTTON_ID].Height + 12));

                DIAMOND.Paint += (s, q) => Dashtolls.FloofyBorder(q, Color.FromArgb(8, 8, 8), Point.Empty, DIAMOND.Size);
            }

            public class HelpDiamond : Form
            {
                public HelpDiamond()
                {
                    Text = ("Dash Helpiez");

                    InitializeStandards(this);
                }
            };

            public class SettingsDiamond : Form
            {
                public SettingsDiamond()
                {
                    Text = ("Dash Suttings");

                    InitializeStandards(this);
                }
            };


            public class VersionDiamond : Form
            {
                private readonly Label Title = new Label();
                private readonly TextBox Content = new TextBox();

                private readonly string[] VERSION_INFO = new string[]
                {
                    $"The Dashies FTP Client version 1.0 has been made for personal use only, but sincerely has been published on the official website https://pugpawz.com/ due to the fact that I myself,  Dashie,  the Developer of this amazing tool had decided to do so.\r\n",
                    $"-=========================-\r\n",
                    $"This tool will come with several piles of functionality, one of these piles is the ability to execute commands using the built in command line and another pile consists out of being able to configure the default directory,  just little neat things, nothing too fancy.\r\n",
                    $"-=========================-\r\n",
                    $"This tool had started as a File Zilla competitor, but I doubt this one will ever overthrow File Zilla,  this is not because I think I myself lack the skill or capability to begin with, no, instead, because I  feel like that File Zilla is just ahead of time and has things I myself would never implement, I have my own style of coding and my own preference of layout which most may not like.\r\n",
                    $"-=========================-\r\n",
                    $"It basically comes down to my tool having less functionality than File Zilla, hehe, but hey, this is a Dash Tool, not a \"I  take your opinion as gold\" tool ;p\r\n",
                    $"-=========================-\r\n",
                    $"I hope you will find the right use for this application, if you so happen to find any buggies or even errors then in such a case please report this to my personal email [Mystical_Cxder_Dashie@protonmail.com] with a message, I would like to keep improving this thing in the future in order to make it look even better and function more user-friendly.\r\n",
                    $"-=========================-\r\n",
                    $"And before you click this dialog away, for the record, please note that I have not used the Designer or whatever, I always write the codes for my G.U.I applications from scratch.\r\n",
                    $"-=========================-\r\n",
                    $"[Version]: 1.0 beta",
                    $"[Author]: Dashieee", 
                };

                public VersionDiamond()
                {
                    Text = ("Dash Vursion");
                    
                    InitializeStandards(this);

                    Size GUI_SIZE = new Size(300, 315);

                    MinimumSize = GUI_SIZE;
                    MaximumSize = GUI_SIZE;
                    Size = GUI_SIZE;

                    Dashtolls.FlubbyLabel(this, Title, "Version Information", 18, new Point(-1, 10), Color.FromArgb(255, 255, 255));
                    Dashtolls.FluffyTextBoxy(this, Content, false, 9, VERSION_INFO, new Point(-1, 10 + Title.Top + Title.Height), new Size(GUI_SIZE.Width - 22, 205), Color.FromArgb(30, 0, 64), Color.FromArgb(255, 255, 255));

                    Content.TextAlign = HorizontalAlignment.Center;

                    const int BUTTON_ID = 2;

                    DIAMOND_BUTTONS[BUTTON_ID].Location = new Point((GUI_SIZE.Width - DIAMOND_BUTTONS[BUTTON_ID].Width) / 2, GUI_SIZE.Height - (DIAMOND_BUTTONS[BUTTON_ID].Height + 15));

                    Paint += (s, q) => Dashtolls.FloofyBorder(q, Color.FromArgb(20, 0, 43), Content.Location, Content.Size);
                }
            };
        };


        /*[========================]*/
        /*[Handle Connection Errors]*/
        /*[========================]*/
        private void ProcessConnectionEventError(String error)
        {
            MessageBox.Show(error, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            pConnectionSwitchB.PerformClick();
        }


        /*[=================================]*/
        /*[Check if a connection can be made]*/
        /*[=================================]*/
        private bool ProcessConnectionEventConnectionStatus(String host)
        {
            using (Ping pClient = new Ping())
            {
                PingReply pReply = pClient.Send(host, 500);

                if (!pReply.Status.Equals(IPStatus.Success))
                {
                    return false;
                };
            };

            return true;
        }


        /*[=======================]*/
        /*[Check if a port is OPEN]*/
        /*[=======================]*/
        private bool ProcessConnectionEventIsPortOpen(string host, int port)
        {
            try
            {
                using (Socket socks = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
                {
                    IAsyncResult result = socks.BeginConnect(host, port, null, null);

                    bool isOpen = result.AsyncWaitHandle.WaitOne(500, true);

                    if (!isOpen)
                    {
                        socks.Close();
                        return false;
                    };

                    socks.EndConnect(result);
                };

                return true;
            }

            catch
            {
                return false;
            };
        }


        /*[=================]*/
        /*[Log Text Injector]*/
        /*[=================]*/
        private void SendBack(Boolean hasTail, String msg)
        { String x = ""; if (hasTail) x = "$: "; tContainer.AppendText($"{x}{msg}"); }


        /*[===========================]*/
        /*[Handle the Connection Event]*/
        /*[===========================]*/
        private string host;
        private int port;

        private readonly List<Thread> ConnectionThreadCache = new List<Thread>();

        private void ProcessConnectionEvent()
        {
            List<string> host_raw = new List<string>();
            host_raw.AddRange(pHostI.Text.Split('@'));

            if (host_raw.Count != 2)
            {
                ProcessConnectionEventError($"The Host format provided is invalid.\r\n\r\nCurrent Format: {pHostI.Text}Correct Format 1: https://www.google.co.uk@21\r\nor\r\nCorrect Format 2: 1.1.1.1@2222");
                return;
            };

            host = host_raw[0].ToLower().Replace(" ", "");
            bool isUrl = Uri.TryCreate(host, UriKind.Absolute, out Uri UriResult) && (UriResult.Scheme == Uri.UriSchemeHttp || UriResult.Scheme == Uri.UriSchemeHttps);

            if (isUrl)
            {
                host = Dns.GetHostAddresses(UriResult.Host)[0].ToString();

                if (host.Length < 1)
                {
                    ProcessConnectionEventError("Unable to retrieve the Host Ip Address from the Url provided.\r\n\r\nPlease make sure that you are using the right Url.");
                    return;
                };
            }

            else
            if (!IPAddress.TryParse(host, out IPAddress _))
            {
                ProcessConnectionEventError("Unable to convert the given input into an Ip Address.\r\n\r\nPlease make sure that you are using an Ipv4 Address or Url rather than something else.");
                return;
            }

            if (!ProcessConnectionEventConnectionStatus(host))
            {
                DialogResult dxResult = MessageBox.Show("The specified host is not replying to our pings, do you want to proceed?", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dxResult.Equals(DialogResult.No))
                    return;
            };

            if (!int.TryParse(host_raw[1], out port))
            {
                ProcessConnectionEventError("The specified port is invalid.\r\n\r\nPlease make sure that your port is numeric and in range.");
                return;
            }

            else
            if ((port < 1) || (port > 65535))
            {
                ProcessConnectionEventError("The specified port is not in range.\r\n\r\nValid ports are those ranging from 1 until 65535.");
                return;
            }

            else
            if (!ProcessConnectionEventIsPortOpen(host, port))
            {
                ProcessConnectionEventError("The given port is not open on the given host.\r\n\r\nPlease make sure that your port is open on the server you are trying to connect to.");
                return;
            };

            FtpAction(WebRequestMethods.Ftp.ListDirectory, "Directory List");

            return;
        }


        /*[============================]*/
        /*[Dashies FTP Action Execution]*/
        /*[============================]*/
        private void FtpAction(string action, string raw_name)
        {
            while (true)
            {
                string ftp_host = host;
                int ftp_port = port;

                string ftp_user = pUserI.Text;
                string ftp_pass = pPassI.Text;

                SendBack(true, $"Connecting to [{host}]:[{port}] ....\r\n");

                FtpWebRequest FtpRequest = (FtpWebRequest)FtpWebRequest.Create($"ftp://{ftp_host}:{ftp_port}/");

                FtpRequest.Credentials = new NetworkCredential(ftp_user, ftp_pass);
                FtpRequest.Method = (string)action;
                FtpRequest.Timeout = 500;

                try
                {
                    WebResponse FtpResponse = FtpRequest.GetResponse();
                    SendBack(true, $"Success, now requesting the {raw_name} ....\r\n");
                }

                catch
                {
                    SendBack(true, $"Failed to authenticate with [{ftp_host}]:[{ftp_port}] !\r\n");
                    break;
                };

                List<String> ResponseCache = new List<String>();

                using (FtpWebResponse FtpResponse = (FtpWebResponse)FtpRequest.GetResponse())
                {
                    using (StreamReader FtpResponseReader = new StreamReader(FtpResponse.GetResponseStream()))
                    {
                        if (ResponseCache.Count > 0)
                            ResponseCache.Clear();

                        ResponseCache.AddRange(FtpResponseReader.
                            ReadToEnd().
                                Split(new string[]
                                {
                                    "\r\n", "\n"
                                },
                            StringSplitOptions.RemoveEmptyEntries)
                        );

                        if (ResponseCache.Count < 1)
                        {
                            SendBack(true, "No response was received.\r\n");
                            break;
                        };

                        SendBack(false, "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n");

                        for (int id = 0, TreeListingUpdate = 1; id < ResponseCache.Count; id += 1, TreeListingUpdate += 1)
                        {
                            string full_response = ResponseCache[id];

                            //the bytes are present in the string, the data just does not get applied to the log. Formula mistake?
                            //for (int space_id = 0; space_id < (34 - Dashtolls.CalculateFontThing(ResponseCache[id], tContainer.Font).Width); space_id += 1) full_response += " ";

                            SendBack(false, $"{full_response}\r\n");
                        };

                        SendBack(false, "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n");

                        SendBack(true, "Success, the result has been printed above ^\r\n");
                        SendBack(true, "You have been disconnected from the host.\r\n");

                        FtpResponseReader.Close();
                    };

                    FtpResponse.Close();
                };

                break;
            };

            pConnectionSwitchB.PerformClick();
        }


        /*[==========================]:*/
        /*[Setup the first few things]:*/
        /*[==========================]:*/
        static readonly List<Label> label_storage = new List<Label>();
        static readonly int labels = 1;

        static readonly List<Button> button_storage = new List<Button>();
        static readonly int buttons = 2;

        static readonly List<PictureBox> container_storage = new List<PictureBox>();
        static readonly int containers = 1;

        static readonly PictureBox png_icon = new PictureBox();

        private static Point FormLocation = Point.Empty;

        void InitializeFoundation()
        {
            try
            {
                //Add border and set Default Properties for Form:
                StartPosition = FormStartPosition.CenterScreen;
                FormBorderStyle = FormBorderStyle.None;
                BackColor = Color.FromArgb(50, 50, 115);

                Text = $"Dashies FTP Client";

                Size application_size = new Size(425, 470);

                MinimumSize = application_size;
                MaximumSize = application_size;
                Size = application_size;

                Icon = (Icon)Cawntrols.furball_resources.GetObject("icon");

                Paint += (s, q) => Dashtolls.FloofyBorder(q, Color.FromArgb(1, 1, 1), new Point(), Size);

                //Add Labels by Id:
                for (int label_id = 0; label_id < labels; label_id += 1)
                    label_storage.Add(new Label());

                Dashtolls.FlubbyLabel(this, label_storage[0], $"{this.Text}", 14, new Point(-1, 2), Color.FromArgb(255, 255, 255));

                label_storage[0].Left = label_storage[0].Left;

                foreach (Label label in label_storage)
                    label.TextAlign = ContentAlignment.MiddleCenter;

                //Add beautiful Logo:
                Dashtolls.Picture(this, png_icon, "icon_png", new Point(5, 2));

                //Add Buttons by Id:
                for (int button_id = 0; button_id < buttons; button_id += 1)
                    button_storage.Add(new Button());

                //Add Exit and Minimize Button:
                Size main_button_size = new Size(40, 27);

                int main_button_border_radius = 0;

                Color[] main_button_fore_colour = new Color[] { Color.FromArgb(255, 255, 255) };
                Color[] main_button_back_colour = new Color[] { BackColor };

                Dashtolls.FluffyMagicalButton(this, button_storage[0], "X", 10, new Point(this.Width - main_button_size.Width - 1, 1), main_button_size, main_button_border_radius, main_button_back_colour, main_button_fore_colour);
                Dashtolls.FluffyMagicalButton(this, button_storage[1], "-", 10, new Point(this.Width - (main_button_size.Width * 2) - 1, 1), main_button_size, main_button_border_radius, main_button_back_colour, main_button_fore_colour);

                button_storage[0].Click += (s, q) => Environment.Exit(-1);
                button_storage[1].Click += (s, q) => WindowState = FormWindowState.Minimized;

                //Add Containers by Id:
                for (int container_id = 0; container_id < containers; container_id += 1)
                    container_storage.Add(new PictureBox());

                //Add and Setup Main Container:
                Dashtolls.Picture(this, container_storage[0], String.Empty, new Point(5, 28));

                Size main_container_size = new Size(Size.Width - 10, Size.Height - 302);

                container_storage[0].BackColor = Color.FromArgb(31, 31, 54);

                container_storage[0].MinimumSize = main_container_size;
                container_storage[0].MaximumSize = main_container_size;
                container_storage[0].Size = main_container_size;

                container_storage[0].Paint += (s, q) => Dashtolls.FloofyBorder(q, Color.FromArgb(1, 1, 1), Point.Empty, container_storage[0].Size);

                //Add Drag Ability to Form and its Controls:
                Dashtolls.Drag(this, this, FormLocation);

                foreach (Control control in this.Controls)
                {
                    Dashtolls.Drag(control, this, FormLocation);
                };
            }

            catch (Exception e)
            {
                ThrowErr(e);
            };

            return;
        }


        /*[=================================]:*/
        /*[Setup the Execution and Log thing]:*/
        /*[=================================]:*/
        static readonly TextBox tContainer = new TextBox();
        static readonly TextBox tInput = new TextBox();

        static readonly Button tExecute = new Button();

        void InitializeTerminal()
        {
            try
            {
                Point tContainerL = new Point(container_storage[0].Left + 1, container_storage[0].Top + container_storage[0].Height + 5);
                Size tContainerS = new Size(container_storage[0].Width - 2, 262);

                Dashtolls.FluffyTextBoxy(this, tContainer, false, 10, new string[] { "", "" }, tContainerL, tContainerS, container_storage[0].BackColor, Color.FromArgb(255, 255, 255));

                tContainer.Text = "$: Dashies Ftp Client (c) 2020\r\n";

                Paint += (s, q) => Dashtolls.FloofyBorder(q, Color.FromArgb(1, 1, 1), tContainerL, tContainerS);

                Point tRunButtonL = new Point(6, tContainerS.Height - 33);
                Color[] tRunButtonC = new Color[] { Color.FromArgb(19, 119, 194) };

                Dashtolls.FluffyMagicalButton(tContainer, tExecute, "Execute", 10, tRunButtonL, new Size(80, 26), 0, tRunButtonC, new Color[] { Color.FromArgb(255, 255, 255) });

                tExecute.TextAlign = ContentAlignment.MiddleCenter;

                Color tInputBoxC = Color.FromArgb(0, 74, 130);

                Dashtolls.FluffyTextBoxy(tContainer, tInput, true, 10, new string[] { "l" }, new Point(tExecute.Left + tExecute.Width, tExecute.Top), new Size(tContainer.Width - 111, tExecute.Height), tInputBoxC, Color.FromArgb(255, 255, 255));

                tInput.Text = "upload script.sh to /root/script.sh";

                tInput.KeyDown += (s, q) =>
                {
                    if (q.KeyCode == Keys.Enter)
                    {
                        tExecute.PerformClick();
                        return;
                    };
                };

                tInput.Click += (s, q) =>
                {
                    if (tInput.Text.ToLower().Equals("upload script.sh to /root/script.sh"))
                    {
                        //What to do if the command is still the same, display a help dialog or sum?
                        return;
                    };

                    //What to do when the user types a command?
                };
            }

            catch (Exception e)
            {
                ThrowErr(e);
            };

            return;
        }


        /*[========================]:*/
        /*[Setup the other Controls]:*/
        /*[========================]:*/
        private readonly TextBox pHostI = new TextBox();
        private readonly TextBox pUserI = new TextBox();
        private readonly TextBox pPassI = new TextBox();

        private readonly Label pHostT = new Label();
        private readonly Label pUserT = new Label();
        private readonly Label pPassT = new Label();

        private readonly Button pConnectionSwitchB = new Button();
        
        public static readonly Button pSettingsB = new Button();
        public static readonly Button pHelpB = new Button();

        void InitializeOthers()
        {//Also think about adding in a Dialog for optional settings.
            try
            {
                List<int> pButtonXCoords = new List<int>();
                int bBeginX, bSepaW, bCount;

                bBeginX = 10;
                bSepaW = 5;
                bCount = 3;

                for (int id = 0; id < bCount; id += 1)
                {
                    int Value = (container_storage[0].Width - bBeginX + 4) / bCount * pButtonXCoords.Count + bBeginX;
                    pButtonXCoords.Add(Value);
                };

                Size pButtonSize = new Size((container_storage[0].Width - bSepaW) / bCount - pButtonXCoords.Count - bBeginX, 28);
                int pButtonStandardY = container_storage[0].Height - (pButtonSize.Height + 10);

                Color[] pButtonBackColour = new Color[]
                { Color.FromArgb(19, 107, 194) };

                Color[] pButtonForeColour = new Color[]
                { Color.FromArgb(255, 255, 255) };

                string pConnectionSwitchSBT = $"Connect";
                string pConnectionSwitchOBT = $"Disconnect";

                Dashtolls.FluffyMagicalButton(container_storage[0], pConnectionSwitchB, pConnectionSwitchSBT, 10, new Point(pButtonXCoords[0], pButtonStandardY), pButtonSize, 8, pButtonBackColour, pButtonForeColour);

                pConnectionSwitchB.Click += (s, q) =>
                {
                    if (pConnectionSwitchB.Text.Equals(pConnectionSwitchSBT))
                    {
                        if (ConnectionThreadCache.Count > 0)
                        {
                            ConnectionThreadCache.Remove(ConnectionThreadCache[0]);
                        };

                        ConnectionThreadCache.Add(new Thread(() =>
                            {
                                ProcessConnectionEvent();
                            }
                        ));

                        ConnectionThreadCache[0].IsBackground = true;
                        ConnectionThreadCache[0].Start();

                        pConnectionSwitchB.Text = pConnectionSwitchOBT;
                    }

                    else
                    if (!pConnectionSwitchB.Text.Equals(pConnectionSwitchSBT))
                    {
                        pConnectionSwitchB.Text = pConnectionSwitchSBT;

                        if (ConnectionThreadCache[0].IsAlive)
                        {
                            ConnectionThreadCache[0].Abort();
                        };
                    };
                };

                Dashtolls.FluffyMagicalButton(container_storage[0], pSettingsB, "Settings", 10, new Point(pButtonXCoords[1], pButtonStandardY), pButtonSize, 8, pButtonBackColour, pButtonForeColour);

                pSettingsB.Enabled = false;

                Dashtolls.FluffyMagicalButton(container_storage[0], pHelpB, "Help", 10, new Point(pButtonXCoords[2], pButtonStandardY), pButtonSize, 8, pButtonBackColour, pButtonForeColour);

                int textBoxFontSize = 13, tTextBoxSE, tTextBoxSP, tTextBoxW, tTextBoxH, tTextBoxSX, tTextBoxSY, tLabelW;

                tLabelW = Dashtolls.CalculateFontThing("PASS:", Cawntrols.FlubbyFont(textBoxFontSize, false)).Width;

                tTextBoxSE = 8;
                tTextBoxSP = 10;

                tTextBoxW = container_storage[0].Width - (tLabelW + ((2 * tTextBoxSP) + tTextBoxSE));
                tTextBoxH = 28;

                tTextBoxSX = container_storage[0].Width - (tTextBoxW + 18);
                tTextBoxSY = 10;

                Color tTextBoxBC = Color.FromArgb(71, 52, 92);
                Color tTextBoxFC = Color.FromArgb(255, 255, 255);

                Size tTextBoxS = new Size(tTextBoxW, tTextBoxH);

                Dashtolls.FluffyTextBoxy(container_storage[0], pHostI, true, textBoxFontSize, new string[] { "https://pugpawz.com@21" }, new Point(tTextBoxSX, tTextBoxSY), tTextBoxS, tTextBoxBC, tTextBoxFC);
                Dashtolls.FluffyTextBoxy(container_storage[0], pUserI, true, textBoxFontSize, new string[] { "pugpawzc" }, new Point(tTextBoxSX, tTextBoxSY + tTextBoxH + tTextBoxSP), tTextBoxS, tTextBoxBC, tTextBoxFC);
                Dashtolls.FluffyTextBoxy(container_storage[0], pPassI, true, textBoxFontSize, new string[] { "HAH" }, new Point(tTextBoxSX, tTextBoxSY + (tTextBoxH + tTextBoxSP) * 2), tTextBoxS, tTextBoxBC, tTextBoxFC);

                pHostI.Text = pHostI.Text.Replace(" ", "");
                pUserI.Text = pUserI.Text.Replace(" ", "");
                pPassI.Text = pPassI.Text.Replace(" ", "");

                foreach (Control control in container_storage[0].Controls)
                {
                    if (control.Controls.Count > 0)
                    {
                        foreach (Control sControl in control.Controls)
                        {
                            if (sControl is TextBox)
                            {
                                sControl.KeyDown += (s, q) =>
                                {
                                    if (q.KeyCode == Keys.Enter)
                                    {
                                        pConnectionSwitchB.PerformClick();
                                    };
                                };

                                control.Paint += (s, q) => Dashtolls.FloofyBorder(q, Color.FromArgb(33, 0, 71), new Point(), control.Size);
                            }
                        }
                    }
                }

                Color lForeColour = Color.FromArgb(255, 255, 255);

                int lStandardX = 13;
                int lStandardY = 12;

                int lSeparation = 8;
                int lFontSize = 14;

                Dashtolls.FlubbyLabel(container_storage[0], pHostT, "Host:", lFontSize, new Point(lStandardX, lStandardY), lForeColour);
                Dashtolls.FlubbyLabel(container_storage[0], pUserT, "User:", lFontSize, new Point(lStandardX, (pHostT.Top + pHostT.Height) + lSeparation), lForeColour);
                Dashtolls.FlubbyLabel(container_storage[0], pPassT, "Pass:", lFontSize, new Point(lStandardX, (pHostT.Top + pHostT.Height) * 2 + lSeparation), lForeColour);

                //Make Label Font Bold instead of Regular

                foreach (Control control in container_storage[0].Controls)
                {
                    if (control is Label)
                    {
                        control.Font = Cawntrols.FlubbyFont(lFontSize, false);
                    }
                }

                pPassI.PasswordChar = '*';
            }

            catch (Exception e)
            {
                ThrowErr(e);
            };

            return;
        }


        /*[=============]*/
        /*[Error Handler]*/
        /*[=============]*/
        public static void ThrowErr(Exception e)
        {
            MessageBox.Show($"Oh no, it seems like an error exception has occurred, a report has been detailed down bellow!\r\n\r\n-------------------------------\r\n\r\n{e.ToString()}\r\n\r\n-------------------------------\r\n\r\nIf the error persists then please consult me Dashie at Mystical_Cxder_Dashie@protonmail.com.", "(Error Exception)", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Environment.Exit(-1);
        }
    };
};
