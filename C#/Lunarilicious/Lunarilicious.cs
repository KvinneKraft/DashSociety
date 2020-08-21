
// -- The start of something greater than this.

// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lunarilicious
{
    public class Lunaroc : Form
    {
	public void Runtime()
	{
	    try
	    {
		BackColor = Color.FromArgb(16, 16, 16);

		Size sz = new Size(350, 350);
		
		MinimumSize = sz;
		MaximumSize = sz;

		FormBorderStyle = FormBorderStyle.None;
		StartPosition = FormStartPosition.CenterScreen;

		// Icon = (Icon) icon;

		Text = "Lunarilicious";
	    }

	    catch { };
	}
    };
};
