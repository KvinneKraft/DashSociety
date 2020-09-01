
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
    public class Selector
    {
	//----Main Objects
	public static readonly PictureBox SELECTOR_MENU = new PictureBox();

	public Selector(Form Base)
	{
	    try //---Initialize Selector Menu Base
	    {
		SELECTOR_MENU.Hide();

		//SELECTOR_MENU.Image = Image.FromFile(""); // <--- Wallpaper
		SELECTOR_MENU.BackColor = Color.FromArgb(16, 16, 16);
		SELECTOR_MENU.Size = StartMenu.START_MENU.Size;
		SELECTOR_MENU.Location = new Point(1, 26);

		SELECTOR_MENU.VisibleChanged += (s, e) =>
		{
		    if (SELECTOR_MENU.Visible)
		    {
			StartMenu.UpdateBarTitle("Character Selector");
		    };
		};

		Base.Controls.Add(SELECTOR_MENU);
	    }

	    catch { };
	}

	/*
	 - Autoload characters
	 - Add a back button
	 - Add on hover buy - sell - info
	 - Add custom scroll bar
	*/

	public void SelectCharacter()
	{
	    SELECTOR_MENU.Show();

	    SELECTOR_MENU.BringToFront();
	}
    };
};