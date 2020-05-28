using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Dash_IP_Fluffer
{
    public class Secondary
    {
	private readonly PictureBox optional_button_container = new PictureBox();

	private readonly Button settings, attack, tools;

	readonly Form owner = Interfuce.interfuce;

	public void Initialize()
	{
	    Add.RuImage(owner, optional_button_container, null, new Size(320, 28), new Point(-1, 82));

	    optional_button_container.BackColor = owner.BackColor;

	    try
	    {
		List<Button> buttons = new List<Button>()
		{
		    settings, attack, tools
		};

		List<string> labels = new List<string>()
		{
		    "Settings", "Attack", "Tools"
		};

		Color button_back_color = Get.menu_bar.BackColor;
		Color button_fore_color = Color.FromArgb(255, 255, 255);

		for (int x = 0, key = 0; key < buttons.Count; x += 110, key += 1)
		{
		    buttons[key] = new Button();

		    Add.AButton(optional_button_container, (Button)buttons[key], new Size(100, 28), new Point(x, 0), button_back_color, button_fore_color, labels[key], Get.FONT_TYPE_MAIN, 10);
		    Add.ControlBorder(buttons[key], 8);
		};
	    }

	    catch (Exception e)
	    {
		Interfuce.ErrorHandler(e);
	    };
	}
    };
};
