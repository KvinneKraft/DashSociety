// (WARNING): This is going to be a messy source file ;)
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Dash_IP_Fluffer
{
    public class Greeting : Form
    {
	public static class Customs/*On Spot Implementation 2 months later*/
	{
	    readonly static PictureBox MenuBar = new PictureBox();
	    readonly static PictureBox Logo = new PictureBox();

	    readonly static Button Quit = new Button();

	    readonly static Label Title = new Label();

	    public static void ToolBar(Form App)
	    {
		Add.RuImage(App, MenuBar, null, new Size(App.Width - 2, 24), new Point(1, 0)); MenuBar.BackColor = Color.FromArgb(1, 1, 1);
		Add.AButton(MenuBar, Quit, new Size(36, 22), new Point(MenuBar.Width - 36, 1), Color.FromArgb(1, 1, 1), Color.FromArgb(255, 255, 255), "X", Get.FONT_TYPE_MAIN, 10); Quit.Click += (s, e) => Environment.Exit(-1);

		App.Paint += (s, e) =>
		{
		    Add.Rectangle(e, Color.FromArgb(1, 1, 1), 2, App.Size, Point.Empty);
		};

		Add.ThaLabel(MenuBar, Title, Point.Empty, Color.FromArgb(255, 255, 255), "K v i n n e  -  K r a f t", Get.FONT_TYPE_CUTE, 10); Size t_s = new Size(Title.Width - 18, Title.Height); Title.MaximumSize = t_s; Title.MinimumSize = t_s; Title.Size = t_s; Title.Location = new Point((MenuBar.Width - Title.Width) / 2, (MenuBar.Height - Title.Height) / 2);
		Add.RuImage(MenuBar, Logo, Properties.Resources.logo, new Size(25, 22), new Point(1, 2)); Add.RuImage(App, new PictureBox(), Properties.Resources.logo, new Size(25, 45), new Point(2, 2));

		foreach (Control Cont in MenuBar.Controls)
		{
		    Set.Draggable(Cont, App);
		};

		Set.Draggable(MenuBar, App);
	    }
	};

	readonly Button Continue = new Button();  
	readonly TextBox Greeter = new TextBox();

	public Greeting()
	{
	    try
	    {
		Size size = new Size(200, 250);

		Size = size;
		MinimumSize = size;
		MaximumSize = size;

		FormBorderStyle = FormBorderStyle.None;
		BackColor = Color.FromArgb(28, 28, 28);

		Customs.ToolBar(this);
		
		Add.ZeTextBox(this, Greeter, new Size(198, Height - 25), new Point(1, 24), Color.FromArgb(255, 255, 255), BackColor, GreeterMesg, Get.FONT_TYPE_MAIN, 8); Greeter.TextAlign = HorizontalAlignment.Center; Greeter.ReadOnly = true; Greeter.Multiline = true; Greeter.ScrollBars = ScrollBars.Vertical;
		Add.AButton(Greeter, Continue, new Size(110, 26), new Point((198 - 110) / 2, Greeter.Height - 34), Color.FromArgb(8, 8, 8), Color.FromArgb(255, 255, 255), "Proceed", Get.FONT_TYPE_MAIN, 9); Continue.Click += (s, e) => Close();
	    }

	    catch (Exception e)
	    {
		Interfuce.ErrorHandler(e);
	    };
	}

	readonly string GreeterMesg =
	(
	    $"Hey there {Environment.UserName}!\r\n\r\nbefore moving into the actual application itself would I like to tell you to please keep in mind that this application can cause actual harm when used inproperly.\r\n\r\nI would like to make sure that all of my users know that this application is only for personal use and should only be used with the right intentions.\r\n\r\n" +
	    "I highly suggest that you make sure that what you are planning to do is actually allowed by the country that you are presenting yourself in while making use of this application.\r\n\r\nWith all of the seriousness off the table though would I like to let you know that I hope that you find some proper use for this piece of ware because it took quite some time to put it together.\r\n\r\nIf you ever encounter any issues then please contact me Dashie at KvinneKraft@protonmail.com.\r\n\r\nAlso make sure that you had downloaded this application from https://pugpawz.com rather than any other domain.\r\n\r\nBlessed be Brother and or Sister <3\r\n\r\n\r\n"
	);
    };
};
