using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dash_IP_Fluffer
{
    public class MonitorLog
    {
	readonly PictureBox log_container = new PictureBox();
	readonly Label logtext = new Label();

	readonly PictureBox scroll_container = new PictureBox();
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
	}

	readonly Form owner = Interfuce.interfuce;

	public void Initialize()
	{
	    Add.RuImage(owner, log_container, null, new Size(owner.Width - 2, 225), new Point(1, owner.Height - 226));

	    log_container.BackColor = Get.menu_bar.BackColor;

	    try
	    {
		Add.ThaLabel(log_container, logtext, new Point(1, 3), Color.FromArgb(255, 255, 255), "Yes", Get.FONT_TYPE_MAIN, 8);

		Size log_size = new Size(log_container.Width - 22, log_container.Height - 4);

		logtext.MaximumSize = log_size;
		logtext.MinimumSize = log_size;
		logtext.Size = log_size;

		logtext.BackColor = Color.FromArgb(197, 109, 227);

		Add.RuImage(log_container, scroll_container, null, new Size(20, log_container.Height - 10), new Point(log_container.Width - 20, 7));
		Add.RuImage(scroll_container, scroll_bar, null, new Size(scroll_container.Width - 6, scroll_container.Height - 4), new Point(3, 2));

		scroll_bar.BackColor = owner.BackColor;

		scroll_container.MouseWheel += (s, e) => ScrollBarHook(e);
		log_container.MouseWheel += (s, e) => ScrollBarHook(e);
		scroll_bar.MouseWheel += (s, e) => ScrollBarHook(e);
	    }

	    catch (Exception e)
	    {
		Interfuce.ErrorHandler(e);
	    };
	}
    };
};
