using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dash_IP_Fluffer
{
    public static class Primary
    {
	static readonly PictureBox main_control_container = new PictureBox();
	static readonly PictureBox ipbox_container = new PictureBox();

	public static readonly TextBox ipbox = new TextBox();

	static readonly Button check = new Button();

	static readonly Label checkip = new Label();
	static readonly Label iptext = new Label();

	static readonly Form owner = (Form) Interfuce.interfuce;

	public static void Initialize()
	{
	    Add.RuImage(owner, main_control_container, null, new Size(275, 28), Point.Empty); main_control_container.BackColor = Color.FromArgb(1, 1, 1);

	    try
	    {
		Add.RuImage(main_control_container, ipbox_container, null, new Size(175, 26), new Point((main_control_container.Width - 175) / 2, (main_control_container.Height - 26) / 2)); ipbox_container.BackColor = Color.FromArgb(250, 170, 210); ipbox_container.Click += (s, e) => ipbox.Select();

		Add.ZeTextBox(ipbox_container, ipbox, Size.Empty, Point.Empty, Color.FromArgb(255, 255, 255), ipbox_container.BackColor, "255.255.255.255:65535", Get.FONT_TYPE_MAIN, 12); ipbox.TextAlign = HorizontalAlignment.Center; ipbox.Size = new Size(ipbox.PreferredSize.Width - 8, ipbox.PreferredSize.Height); ipbox.Location = new Point((ipbox_container.Width - ipbox.Width) / 2, (ipbox_container.Height - ipbox.Height) / 2);

		Add.ThaLabel(main_control_container, iptext, Point.Empty, Color.FromArgb(255, 255, 255), "Tango:", Get.FONT_TYPE_MAIN, 11); iptext.MaximumSize = new Size(50, ipbox_container.Height + 2); iptext.MinimumSize = new Size(50, ipbox_container.Height + 2); iptext.TextAlign = ContentAlignment.MiddleCenter; iptext.Location = new Point(ipbox_container.Left - iptext.Width, ipbox_container.Top - 1); iptext.BackColor = (Color) Get.menu_bar.BackColor;
		Add.ThaLabel(main_control_container, checkip, Point.Empty, Color.FromArgb(255, 255, 255), "Check\nStatus", Get.FONT_TYPE_MAIN, 9); checkip.MaximumSize = new Size(50, ipbox_container.Height + 2); checkip.MinimumSize = new Size(50, ipbox_container.Height + 2); checkip.TextAlign = ContentAlignment.MiddleCenter; checkip.Location = new Point(ipbox_container.Left + ipbox_container.Width, ipbox_container.Top - 1); checkip.BackColor = (Color)Get.menu_bar.BackColor;

		checkip.MouseLeave += (s, e) => checkip.BackColor = Color.FromArgb(217, 145, 181); checkip.MouseHover += (s, e) => checkip.BackColor = Color.FromArgb(230, 158, 194); checkip.MouseEnter += (s, e) => checkip.BackColor = Color.FromArgb(230, 158, 194); checkip.MouseDown += (s, e) => checkip.BackColor = Color.FromArgb(250, 184, 217); checkip.MouseUp += (s, e) => checkip.BackColor = Color.FromArgb(230, 158, 194);

		main_control_container.Paint += (s, e) => Add.Rectangle(e, Get.menu_bar.BackColor, 2, ipbox_container.Size, ipbox_container.Location);
	    }

	    catch (Exception e)
	    {
		Interfuce.ErrorHandler(e);
	    };

	    main_control_container.Location = new Point((owner.Width - main_control_container.Width) / 2, 40);
	}
    };
};
