
/* (C) All Rights Reserved, Dashies Software Inc. */

using System;
using System.Windows.Forms;

namespace Pony_Spoofer_GUI {
    internal sealed class Program {
        [STAThread]
        private static void Main(string[] args) {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Startup());
        }
        
    }
}
