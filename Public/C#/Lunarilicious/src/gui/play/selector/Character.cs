
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Lunarilicious
{
    class Character : Form
    {
	//----Main Objects
	readonly Button EQUIP = new Button();
	readonly Button BUY = new Button();

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

		Size size = new Size(250, 250);
		
		base.MinimumSize = size;
		base.MaximumSize = size;

		base.BackColor = Color.FromArgb(24, 24, 24);

		Paint += (s, e) => Injector.Add.Rectangle(e, Color.FromArgb(8, 8, 8), 2, size, Point.Empty);
	    }

	    catch { };

	    try //----Initialize Buttons
	    {

	    }

	    catch { };

	    try //----Initialize Avatar Preview
	    {

	    }

	    catch { };

	    try //----Initialize Description Box
	    {

	    }

	    catch { };
	}

	public new void Select()
	{
	    ShowDialog();

	    // Setup Config for Characters
	    // Buy Price
	    // Info
	    // Display Name

	    // Update Initialized Controls
	}
    };
};
