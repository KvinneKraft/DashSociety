
// Author: Dashie
// Version: 5.0

using System;
using System.Drawing;
using System.Windows.Forms;

namespace SimpleFlood
{
    public partial class ABOUT : Form
    {
	readonly TextBox ABOUT_TEXT = new TextBox();
	readonly Button CLOSE = new Button();

	readonly string MESSAGE = 
	(
	    "Hey there, thank you for using the Simple Flood 5.0 technologies, I am (stoned) Dashie and you are about to read my thoughts.\r\n\r\nThe story behind my reason of interest goes back to when I just started programming, when I just starting programming I always wanted to put together something that had to do with sockets in C and C++, as it turned out it was harder than I had expected, so I had decided to look for an alternative.\r\n\r\nAfter several attempts at making a working client that could flood a network on its own I basically gave up on the idea and went to look for an easier way, so I started working with C#, at that point it became childs play and not too long after I also learned the C++ way.\r\n\r\nThough my graphic skills and colouring may not be as pretty as you would like them to be, the efficiency and structural difference between these and the previous codes are so much more than what you would expect of them.\r\n\r\nIn between of the several other projects that I have worked on the in the past few months have I actually improved the control library, which basically makes it easier to add any control really.\r\n\r\nSo yea, I was high when I wrote this, I love you all, and if you encounter any errors just push me a message at KvinneKraft@protonmail.com, preferably with a stack-trace (The application gives you one when an error occurs).\r\n\r\n-Dashie\r\n\r\n"
	);

	public ABOUT()
	{
	    InitializeComponent();

	    try
	    {
		Shown += (s, e) =>
		{
		    ABOUT_TEXT.DeselectAll();
		    CenterToParent();
		};

		Parent = SimpleFlood.CTR;
		StartPosition = FormStartPosition.CenterParent;

		Paint += (s, e) =>
		{
		    Mod.Rectangle(e, Color.MidnightBlue, 1, new Size(Size.Width - 1, Size.Height - 1), new Point(0, 0));
		};
	    }

	    catch (Exception e)
	    {
		SimpleFlood.ErrorHandler(e.StackTrace);
	    };

	    try
	    {
		Add.InputBox(this, ABOUT_TEXT, new Size(Size.Width - 4, Size.Height - 2), new Point(1, 1), MESSAGE, 10, 1, Color.FromArgb(8, 8, 8), Color.White, _readOnly: true);

		ABOUT_TEXT.ScrollBars = ScrollBars.Vertical;
		ABOUT_TEXT.Multiline = true;

		Add.Button(ABOUT_TEXT, CLOSE, "Close", 12, 1, new Size(125, 26), new Point(-1, ABOUT_TEXT.Height - 41), Color.MidnightBlue, Color.White);

		CLOSE.Click += (s, e) => Hide();

		Mod.Moveable(ABOUT_TEXT, this);
		Mod.Border(CLOSE, 4);
	    }

	    catch (Exception e)
	    {
		SimpleFlood.ErrorHandler(e.StackTrace);
	    };
	}
    }
}
