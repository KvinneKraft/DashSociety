using System;
using System.Windows.Forms;

namespace DashCore
{
    public class Program
    {
	[STAThread]
	static void Main(string[] args)
	{
	    using (var save = new DashCore.SaveFileDialog())
	    {
		save.ShowDialog();
	    };
	}
    }
}
