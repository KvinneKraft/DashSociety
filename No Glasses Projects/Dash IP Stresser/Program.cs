

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
        public PictureBox menu_bar_icon = new PictureBox();
        public PictureBox menu_bar = new PictureBox();

        public Button menu_bar_minimize = new Button();
        public Button menu_bar_exit = new Button();

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
                MOON.paint_border(e, Color.FromArgb(8, 8, 8), 2, client_size, new Point(0, 0));
            };


            MOON.install_menubar(menu_bar, this);

            //Image icon_image = Bitmap.FromHicon(new Icon(Properties.resources.program_icon, new Size(28, 26)).Handle);
            MOON.add_image(menu_bar, menu_bar_icon, Properties.resources.menu_bar_icon, new Size(28, 26), new Point(1, 1));

            Size menu_bar_button_size = new Size(65, 26);

            MOON.add_button(menu_bar, menu_bar_minimize, "-", 10, new Color[] { menu_bar.BackColor, Color.FromArgb(255, 255, 255) }, 0, menu_bar_button_size, new Point((menu_bar.Width - menu_bar_button_size.Width * 2) - 1, 1));
            MOON.add_button(menu_bar, menu_bar_exit, "X", 10, new Color[] { menu_bar.BackColor, Color.FromArgb(255, 255, 255) }, 0, menu_bar_button_size, new Point((menu_bar.Width - menu_bar_button_size.Width) - 1, 1));

            Button[] menu_bar_buttons = new Button[] { menu_bar_minimize, menu_bar_exit };

            foreach(Button button in menu_bar_buttons)
            {
                button.MouseDown += (s, e) =>
                    button.BackColor = Color.FromArgb(28, 28, 28);

                button.MouseClick += (s, e) =>
                    button.BackColor = Color.FromArgb(29, 29, 29);

                button.MouseUp += (s, e) =>
                    button.BackColor = menu_bar.BackColor;

                button.MouseEnter += (s, e) =>
                    button.BackColor = Color.FromArgb(28, 28, 28);

                button.MouseLeave += (s, q) =>
                    button.BackColor = menu_bar.BackColor;
            };

            Show();
        }

        public Apple()
        {
            ClientLayout();

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
