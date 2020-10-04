
// Author: Dashie
// Version: 5.0

using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleFlood
{
    namespace Parts
    {
	public class SHARE
	{
	    public static void ShowObject(Object obj)
	    {
		if (obj is Control && obj is PictureBox)
		{
		    if (((PictureBox) obj).Visible)
		    {
			((PictureBox)obj).Hide();
			((PictureBox)obj).SendToBack();
		    }

		    else
		    {
			((PictureBox)obj).Show();
			((PictureBox)obj).BringToFront();
		    };
		};
	    }
	};
    };
};
