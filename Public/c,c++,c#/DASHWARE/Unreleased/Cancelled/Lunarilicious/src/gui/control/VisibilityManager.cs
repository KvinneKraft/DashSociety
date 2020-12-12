
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Text;
using System.Media;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lunarilicious
{
    class VisibilityManager
    {
	public static void ShowComponent(PictureBox Criteria)
	{
	    List<PictureBox> Components = new List<PictureBox>()
	    {
		Selector.SELECTOR_MENU, StartMenu.START_MENU, Inventory.INVENTORY_MENU
	    };

	    foreach(PictureBox Component in Components)
	    {
		if (Component != Criteria)
		{
		    if (Component.Visible)
		    {
			Component.Hide();
		    };
		};
	    };

	    Criteria.Show();
	}
    };
};