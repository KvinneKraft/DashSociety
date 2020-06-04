using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dash_IP_Fluffer
{
    public class MonitorLog
    {
	readonly PictureBox pink_separator = new PictureBox();
	readonly PictureBox log_container = new PictureBox();

	readonly TextBox logtext = new TextBox();

	/*readonly PictureBox scroll_container = new PictureBox();
	readonly PictureBox scroll_bar = new PictureBox();

	private void ScrollBarHook(MouseEventArgs e)
	{
	    if (e.Delta < 0)
	    {
		if (scroll_bar.Top >= 12)
		{
		    scroll_bar.Top -= 10;
		};
	    }

	    else
	    {
		if (scroll_bar.Top <= scroll_container.Height - (12 + scroll_bar.Height))
		{
		    scroll_bar.Top += 10;
		};
	    };
	}*/

	readonly Form owner = (Form) Interfuce.interfuce;

	public void Initialize()
	{
	    Add.RuImage(owner, log_container, null, new Size(owner.Width, 225), new Point(0, owner.Height - 225));

	    log_container.BackColor = (Color)Get.menu_bar.BackColor;

	    try
	    {
		Add.ZeTextBox(log_container, logtext, new Size(log_container.Width - 4, log_container.Height - 4), new Point(2, 2), Color.FromArgb(255, 255, 255), Color.FromArgb(191, 115, 153), "", Get.FONT_TYPE_MAIN, 8);

		if (welcome_message.Contains("{barrier}"))//Lazy Programmer Technique 1.0
		{
		    string buff = "-";

		    for (int k = 0; k < 36; k += 1)
		    {
			buff += "=-";
		    };

		    logtext.Text = welcome_message.Replace("{barrier}", buff);
		};

		logtext.AutoScrollOffset = new Point(-1, -1);
		logtext.ScrollBars = ScrollBars.Vertical;
		logtext.Multiline = true;
		logtext.ReadOnly = true;

		Add.RuImage(logtext, pink_separator, null, new Size(2, log_container.Height), new Point(logtext.Width - 19, 0));
		pink_separator.BackColor = (Color) Get.menu_bar.BackColor;
	    }

	    catch (Exception e)
	    {
		Interfuce.ErrorHandler(e);
	    };
	}

	readonly string welcome_message = (
	    "{barrier}\r\n" +
	    
	    "This version of Dash IP Fluffer is still being developed " +
	    "which means that you may come across issues which have yet " +
	    "to be solved.\r\n\r\n" +

	    "If in anyway you were to come across any issues then in " +
	    "such a case reach out to me Dashie at KvinneKraft@protonmail.com " +
	    "about the matter.\r\n\r\n" + 
	    
	    "Please do add some context rather than just a simple complaint " +
	    "because I am striving towards bettering this creature in the " +
	    "future in my spare time.\r\n" +
	    
	    "{barrier}\r\n" +

	    "I have also implemented a few shortcuts to easen the use of " +
	    "this fluff, you may press F1 to find out what they are.\r\n" +
	
	    "{barrier}\r\n\r\n" +

	    "Waiting ....."
	);
    };
};
