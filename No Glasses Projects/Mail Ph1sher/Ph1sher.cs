
// Author: Dashie
// Version: 1.0


using System;
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
        public Ph1sher()
        {
            
        }
    };
};
