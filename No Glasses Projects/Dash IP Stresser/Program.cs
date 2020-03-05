

// Author: Dashie
// Version: 1.0


using System;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Collections.Generic;


namespace Dash_IP_Stresser
{
    class Apple : Form
    {
        public TextBox packets_sent = new TextBox();

        private void ClientLayout()
        {
            Hide();
            BringToFront();

            Size client_size = new Size(350, 350);

            Size = client_size;
            MaximumSize = client_size;
            MinimumSize = client_size;

            MaximizeBox = false;
            MinimizeBox = false;

            Icon = Properties.resources.program_icon;

            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.None;

            BackColor = Color.FromArgb(24, 24, 24);
            Text = " Dashies Fluffy IP Stresser ☽⛤☾";

            Paint += (s, e) =>
            {
                MOON.paint_border(e, Color.FromArgb(1, 1, 1), 2, client_size, new Point(0, 0));
            };

            Show();
        }

        public Apple()
        {

        }
    };


    static class Program
    {
        public static Apple apple;
        

        [STAThread]
        static void Main(string[] args)
        {
            //- HTTP, TCP, UDP and ICMPv4 support!

            apple = new Apple();
            Application.Run(apple);
        }
    };
};
