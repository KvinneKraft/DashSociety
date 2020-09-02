
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

	//---Character Objects
	public static readonly PictureBox CHARACTER_BASE = new PictureBox();

	//---Scrollbar Objects
	public static readonly PictureBox SCROLL_BAR_BASE = new PictureBox();
	public static readonly PictureBox SCROLL_BAR = new PictureBox();

	public Selector(Form Base)
	{
	    try //---Initialize Selector Menu Base
	    {
		SELECTOR_MENU.Hide();

		//SELECTOR_MENU.Image = Image.FromFile(""); // <--- Wallpaper
		SELECTOR_MENU.BackColor = Color.FromArgb(10, 10, 10);
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

	    try //---Initialize Scroll Bar
	    {
		SCROLL_BAR_BASE.BackColor = Color.FromArgb(24, 24, 24);
		SCROLL_BAR_BASE.Size = new Size(20, SELECTOR_MENU.Height - 8);
		SCROLL_BAR_BASE.Location = new Point(SELECTOR_MENU.Width - SCROLL_BAR_BASE.Width - 5, 4);

		Injector.Add.ControlBorder(SCROLL_BAR_BASE, 4);
		SELECTOR_MENU.Controls.Add(SCROLL_BAR_BASE);

		SCROLL_BAR.BackColor = Color.FromArgb(8, 8, 8);
		SCROLL_BAR.Size = new Size(SCROLL_BAR_BASE.Width - 6, SCROLL_BAR_BASE.Height - 5);
		SCROLL_BAR.Location = new Point(3, 2);

		Injector.Add.ControlBorder(SCROLL_BAR, 4);
		SCROLL_BAR_BASE.Controls.Add(SCROLL_BAR);
	    }

	    catch { };

	    try //---Initialize Character Menu Base
	    {
		CHARACTER_BASE.BackColor = Color.FromArgb(24, 24, 24);
		CHARACTER_BASE.Size = new Size(SELECTOR_MENU.Width - (SCROLL_BAR_BASE.Width + 15), SELECTOR_MENU.Height - 10);
		CHARACTER_BASE.Location = new Point(5, 5);

		SELECTOR_MENU.Paint += (s, e) =>
		{
		    Injector.Add.Rectangle(e, Color.FromArgb(20, 20, 20), 2, CHARACTER_BASE.Size, CHARACTER_BASE.Location);
		};

		Injector.Add.ControlBorder(CHARACTER_BASE, 4);
		SELECTOR_MENU.Controls.Add(CHARACTER_BASE);
	    }

	    catch { };
	}

	/*
	 - Autoload characters
	 - Add a back button
	 - Add on hover buy - sell - info
	 - Make scrollbar work
	*/

	public void SelectCharacter()
	{
	    SELECTOR_MENU.Show();

	    SELECTOR_MENU.BringToFront();
	}
    };
};