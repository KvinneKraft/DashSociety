
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace ControlAddExample
{
    public partial class Form1 : Form
    {
	List<Button> buttons = new List<Button>();
	PictureBox container = new PictureBox();

	public Form1()
	{
	    Mod.Moveable(this, this);

	    FormBorderStyle = FormBorderStyle.None;
	    BackColor = Color.Black;

	    Size appSize = new Size(350, 350);

	    MinimumSize = appSize;
	    MaximumSize = appSize;

	    Add.PictureBox(this, container, null, new Size(300, 26 * 3 + 20), new Point(-1, -1), Color.Black);

	    Size buttonSize = new Size(93, 26);

	    for (int t = 0, y = 0; t < 3; y += 36, t += 1)
	    {
		for (int k = 0, x = 0; k < 3; x += 103, k += 1)
		{
		    buttons.Add(new Button());
		    Add.Button(container, buttons[buttons.Count - 1], $"{buttons.Count}", 10, 1, buttonSize, new Point(x, y), Color.FromArgb(8, 8, 8), Color.White);
		};
	    };
	}
    }
}
