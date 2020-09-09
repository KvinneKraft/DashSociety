
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
    class Selector
    {
	//----Main Objects
	public static readonly PictureBox SELECTOR_MENU = new PictureBox();

	//---Character Objects
	static readonly PictureBox CHARACTER_BASE_BASE = new PictureBox();
	static readonly PictureBox CHARACTER_BASE = new PictureBox();

	static readonly Character CHARACTER = new Character();

	//---Scrollbar Objects
	static readonly PictureBox SCROLL_BAR_BASE = new PictureBox();
	static readonly PictureBox SCROLL_BAR = new PictureBox();

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

	    try //---Initialize Character Menu Bases
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

		CHARACTER_BASE_BASE.BackColor = CHARACTER_BASE.BackColor;
		CHARACTER_BASE_BASE.Size = new Size(CHARACTER_BASE.Width - 16, CHARACTER_BASE.Height - 16);
		CHARACTER_BASE_BASE.Location = new Point(8, 8);

		CHARACTER_BASE.Controls.Add(CHARACTER_BASE_BASE);

		try //---Initialize Characters
		{
		    for (int o = 0, k = 0, c = 0, y = 0, a = PonySize(k), r = -1, x = 0; k < Entity.EntityType.Pony.ponies.Count; k += 1, x += 64, c += 1)
		    {
			if (k >= o)                     
			{
			    r += 1;

			    CHARACTER_BASE_BASE.Controls.Add(new PictureBox());

			    CHARACTER_BASE_BASE.Controls[r].BackColor = Color.FromArgb(8, 8, 8);
			    CHARACTER_BASE_BASE.Controls[r].Size = new Size(CHARACTER_BASE_BASE.Width, 64);
			    CHARACTER_BASE_BASE.Controls[r].Location = new Point(0, y);
			   
			    y += CHARACTER_BASE_BASE.Controls[r].Height + 5;
			    x = 0; c = 0; o += a;
			};

			CHARACTER_BASE_BASE.Controls[r].Controls.Add(Entity.EntityType.Pony.ponies[k]);

			CHARACTER_BASE_BASE.Controls[r].Controls[c].Location = new Point(x, CHARACTER_BASE_BASE.Controls[r].Controls[c].Height - CHARACTER_BASE_BASE.Controls[r].Height);
			CHARACTER_BASE_BASE.Controls[r].Controls[c].Click += (s, e) => CHARACTER.Select();
		    };
		}

		catch (Exception e) { MessageBox.Show($"{e}"); };
	    }

	    catch { };
	}

	static int PonySize(int k) => 7; // Fix this, please. holy shit.

	public void SelectCharacter()
	{
	    SELECTOR_MENU.Show();
	    SELECTOR_MENU.BringToFront();
	}
    };
};