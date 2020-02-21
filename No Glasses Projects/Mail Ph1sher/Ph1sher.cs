
// Author: Dashie
// Version: 1.0


using System;
using System.Drawing;
using System.Windows.Forms;


namespace Mail_Ph1sher
{
    class Program
    {
        public static Ph1sher ph1shy = new Ph1sher();

        [STAThread]
        static void Main()
        {
            ph1shy = new Ph1sher();

            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(ph1shy);
        }
    };



    public partial class Ph1sher : Form
    {
        private void Layout()
        {
            Size client_size = new Size();

            Size = client_size;
            MaximumSize = client_size;
            MinimumSize = client_size;

            MinimizeBox = false;
            MaximizeBox = false;

            //Icon = (Icon) Properties.Resources.Icon;

            StartPosition = FormStartPosition.CenterScreen;

            return;
        }


        public Ph1sher()
        {
                
        }
    };



    public static class Moon
    { 

    };
};
