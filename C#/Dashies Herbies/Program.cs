
// Author: Dashie
// Version: 1.0

using System;
using System.Windows.Forms;

namespace DashCore
{
    public class Program
    {
	private static readonly CAPTCHA captcha = new CAPTCHA();
	private static readonly Product product = new Product();

	[STAThread]
	static void Main(string[] args)
	{
	    product.initdb();
	    
	    dush.halt();
	}
    };
};
