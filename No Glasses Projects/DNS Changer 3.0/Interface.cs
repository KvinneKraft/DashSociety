

// Author: Dashie
// Version: 1.0


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DNSChanger
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Interface());
        }
    };


    public partial class Interface : Form
    {
        public class init
        {
            public static Size client_size = new Size(350, 300);

            public void layout(Form obj)
            {
                obj.FormBorderStyle = FormBorderStyle.FixedDialog;
                obj.StartPosition = FormStartPosition.CenterScreen;

                obj.Size = client_size;
                obj.MinimumSize = client_size;
                obj.MaximumSize = client_size;

                obj.MinimizeBox = false;
                obj.MaximizeBox = false;

                obj.BackColor = Color.FromArgb(32, 32, 32);
            }

            public void controls()
            {

            }

            public void events()
            {

            }
        };

	public Interface()
	{
            init inity = new init();

            inity.layout(this);
            inity.controls();
            inity.events();
	}
    }
}
