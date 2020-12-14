
/* (c) All Rights Reserved, Dashies Software Inc. */

using System;
using System.Windows.Forms;

namespace src
{
    static class program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new main());
        }
    }
}
