
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lunarilicious
{
    class Character : Form
    {
	//----Button Objects
	readonly PictureBox BUTTON_CONTAINER = new PictureBox();

	readonly Button CANCEL = new Button();
	readonly Button EQUIP = new Button();
	readonly Button BUY = new Button();

	//----Avatar Objects
	readonly PictureBox AVATAR_CONTAINER = new PictureBox();
	readonly PictureBox AVATAR_BOX = new PictureBox();
	readonly PictureBox AVATAR = new PictureBox();

	readonly Label AVATAR_PRICE = new Label();
	readonly Label AVATAR_NAME = new Label();

	public Character()
	{
	    Hide();

	    try //----Initialize Standardized Dialog
	    {
		FormBorderStyle = FormBorderStyle.None;
		StartPosition = FormStartPosition.CenterParent;

		MinimizeBox = false;
		MaximizeBox = false;

		ShowInTaskbar = false;

		Size size = new Size(300, 220);
		
		base.MinimumSize = size;
		base.MaximumSize = size;

		base.BackColor = Color.FromArgb(53, 129, 252);

		Paint += (s, e) => Mod.Rectangle(e, Color.FromArgb(40, 75, 130), 2, new Size(size.Width - 1, size.Height - 1), new Point(0, 0));
	    }

	    catch { };

	    try //----Initialize Avatar Preview
	    {
		// AVATAR , AVATAR_CONTAINER

		AVATAR_CONTAINER.Size = new Size(270, 150);
		AVATAR_CONTAINER.BackColor = Color.FromArgb(40, 75, 130);
		AVATAR_CONTAINER.Location = new Point(15, 15);
		Controls.Add(AVATAR_CONTAINER);

		Add.Label(AVATAR_CONTAINER, AVATAR_PRICE, string.Empty, 16, Get.Font.NORMAL, Size.Empty, Point.Empty, Color.Empty, Color.FromArgb(255, 255, 255));
		Add.Label(AVATAR_CONTAINER, AVATAR_NAME, string.Empty, 16, Get.Font.NORMAL, Size.Empty, Point.Empty, Color.Empty, Color.FromArgb(255, 255, 255));

		AVATAR_BOX.Size = new Size(64, 64);
		AVATAR_BOX.BackColor = Color.FromArgb(0, 0, 0, 255);
		AVATAR_BOX.Location = new Point(Integers.CenterOf(AVATAR_CONTAINER, AVATAR_BOX), 5);
		AVATAR_CONTAINER.Controls.Add(AVATAR_BOX);

		AVATAR.BackColor = AVATAR_BOX.BackColor;
		AVATAR_BOX.Controls.Add(AVATAR);
	    }

	    catch { };

	    try //----Initialize Buttons
	    {
		BUTTON_CONTAINER.Size = new Size(245, 28);
		BUTTON_CONTAINER.BackColor = Color.FromArgb(0, 0, 0, 255);
		BUTTON_CONTAINER.Location = new Point(Integers.CenterOf(this, BUTTON_CONTAINER), Height - 42);

		Controls.Add(BUTTON_CONTAINER);

		Size BUTTON_SIZE = new Size(75, 28);

		Color BUTTON_B_COLOR = Color.FromArgb(40, 75, 130);
		Color BUTTON_F_COLOR = Color.FromArgb(255, 255, 255);

		int BUTTON_FONT = Get.Font.NORMAL;
		int BUTTON_FONT_SIZE = 12;

		Add.Button(BUTTON_CONTAINER, BUY, "Buy", BUTTON_FONT_SIZE, BUTTON_FONT, BUTTON_SIZE, Point.Empty, BUTTON_B_COLOR, BUTTON_F_COLOR);
		BUY.Click += (s, e) => Hide();

		Add.Button(BUTTON_CONTAINER, EQUIP, "Equip", BUTTON_FONT_SIZE, BUTTON_FONT, BUTTON_SIZE, new Point(BUY.Width + 10, 0), BUTTON_B_COLOR, BUTTON_F_COLOR);
		EQUIP.Click += (s, e) => Hide();

		Add.Button(BUTTON_CONTAINER, CANCEL, "Cancel", BUTTON_FONT_SIZE, BUTTON_FONT, BUTTON_SIZE, new Point(EQUIP.Left + EQUIP.Width + 10, 0), BUTTON_B_COLOR, BUTTON_F_COLOR);
		CANCEL.Click += (s, e) => Hide();
	    }

	    catch { };

	    try //----Initialize Description Box
	    {

	    }

	    catch { };

	    try //----Some last touches
	    {
		List<Control> controls = new List<Control>()
		{
		    this, CANCEL, EQUIP, BUY, AVATAR_CONTAINER
		};

		foreach (Control control in controls)
		{
		    Mod.Border(control, 4);
		};
	    }

	    catch { };
	}
	 
	public void Select(PictureBox Pony, string Name, int Price)
	{
	    AVATAR_NAME.MinimumSize = TextRenderer.MeasureText(Name, AVATAR_NAME.Font); // Simplify this shit.
	    AVATAR_NAME.MaximumSize = TextRenderer.MeasureText(Name, AVATAR_NAME.Font);
	    AVATAR_NAME.Location = new Point(Integers.CenterOf(AVATAR_CONTAINER, AVATAR_NAME), 5);
	    AVATAR_NAME.Text = Name;

	    AVATAR_BOX.Top = (AVATAR_NAME.Top + AVATAR_NAME.Height + 10);

	    AVATAR.Size  = Pony.Image.Size;
	    AVATAR.Image = Pony.Image;

	    AVATAR_PRICE.MinimumSize = TextRenderer.MeasureText($"${Price}", AVATAR_PRICE.Font); // Simplify this shit.
	    AVATAR_PRICE.MaximumSize = TextRenderer.MeasureText($"${Price}", AVATAR_PRICE.Font);
	    AVATAR_PRICE.Location = new Point(Integers.CenterOf(AVATAR_CONTAINER, AVATAR_PRICE), AVATAR_BOX.Top + AVATAR_BOX.Height + 10);
	    AVATAR_PRICE.Text = $"${Price}";

	    // Find a way to show description nicely.

	    if (!Visible)
	    {
		ShowDialog();
	    };

	    // Setup Config for Characters
	    // Buy Price
	    // Info
	    // Display Name

	    // Update Initialized Controls
	}
    };
};
