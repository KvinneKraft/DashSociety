using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DashBook
{
    static class Program
    {
	[STAThread]
	static void Main()
	{
	    Application.EnableVisualStyles();
	    Application.SetCompatibleTextRenderingDefault(true);
	    
	    var book_app = new DashBook();

	    Application.DoEvents();
	    Application.Run(book_app);
	}
    }
}
