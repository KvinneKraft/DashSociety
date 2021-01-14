
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
    class StartMenu
    {
	//----Start Menu Objects
	static readonly PictureBox START_MENU_CONTAINER = new PictureBox();
	static readonly PictureBox GAME_TITLE_OBJECT = new PictureBox();

	public static readonly PictureBox START_MENU = new PictureBox();

	static readonly Button GAME_SETTINGS = new Button();
	static readonly Button INVENTORY = new Button();
	static readonly Button PLAY_GAME = new Button();
	
	//----Menu Bar Objects
	static readonly PictureBox BAR_OBJECT = new PictureBox();
	static readonly Button MINIMIZE_OBJECT = new Button();
	static readonly Button QUIT_OBJECT = new Button();
	static readonly Label BAR_TITLE = new Label();

	public static void UpdateBarTitle(string title)
	{
	    BAR_TITLE.Text = title;
	    
	    Size size = TextRenderer.MeasureText(title, BAR_TITLE.Font);

	    BAR_TITLE.MaximumSize = size;
	    BAR_TITLE.MinimumSize = size;

	    BAR_TITLE.Location = new Point(10, (BAR_OBJECT.Height - BAR_TITLE.Height) / 2);
	    BAR_TITLE.Update();
	}

	public void Setup(Form Base)
	{
	    try //---Initialize Custom Bar
	    {
		Base.FormBorderStyle = FormBorderStyle.None;
		Base.BackColor = Color.FromArgb(16, 16, 16);
		Base.Icon = Properties.Resources.icon;
		
		Add.PictureBox(Base, BAR_OBJECT, null, new Size(Base.Width, 26), Point.Empty, Base.BackColor); //BAR_OBJECT.BackColor = Owner.BackColor;
		Mod.Moveable(BAR_OBJECT, Base);

		Base.Paint += (s, e) =>
		{
		    Mod.Rectangle(e, Base.BackColor, 2, new Size(Base.Width, Base.Height - 26), new Point(0, 25));
		};

		Add.Button(BAR_OBJECT, QUIT_OBJECT, "X", Get.Font.NORMAL, 10, new Size(60, BAR_OBJECT.Height), new Point(BAR_OBJECT.Width - 60, 0), BAR_OBJECT.BackColor, Color.FromArgb(255, 255, 255));

		QUIT_OBJECT.Click += (s, e) =>
		{
		    Environment.Exit(-1);
		};

		Add.Button(BAR_OBJECT, MINIMIZE_OBJECT, "-", Get.Font.NORMAL, 10, new Size(60, BAR_OBJECT.Height), new Point(BAR_OBJECT.Width - 120, 0), BAR_OBJECT.BackColor, Color.FromArgb(255, 255, 255));

		MINIMIZE_OBJECT.Click += (s, e) =>
		{
		    Base.SendToBack();
		};

		Add.Label(BAR_OBJECT, BAR_TITLE, string.Empty, 10, Get.Font.NORMAL, Size.Empty, Point.Empty, Color.Empty, Color.White); /*PURPOSELY*/  Mod.Moveable(BAR_TITLE, Base);
		UpdateBarTitle("Main Screen");
	    }

	    catch { };

	    try //---Initialize Start Menu
	    {
		START_MENU.Image = Image.FromFile("data\\gui\\MenuBackground.png");
		START_MENU.BackColor = Color.FromArgb(16, 16, 16);
		START_MENU.Location = new Point(1, 26);
		START_MENU.Size = new Size(Base.Width - 2, Base.Height - 27);

		Base.Controls.Add(START_MENU);

		Add.PictureBox(START_MENU, GAME_TITLE_OBJECT, Image.FromFile("data\\gui\\GameTitle.png"), new Size(463, 55), new Point(-1, 24), Color.Empty);
		GAME_TITLE_OBJECT.BackColor = Color.FromArgb(0, 0, 0, 255);

		// Hide Owner when going to another menu, perhaps the shop or so.

		Add.PictureBox(START_MENU, START_MENU_CONTAINER, null, new Size(415, 56), new Point(-1, 120), Color.Empty);
		START_MENU_CONTAINER.BackColor = Color.FromArgb(0, 0, 0, 255);

		Color OPTION_COLOR = Color.FromArgb(16, 16, 16);
		Size OPTION_SIZE = new Size(125, START_MENU_CONTAINER.Height);
		
		Add.Button(START_MENU_CONTAINER, PLAY_GAME, "Play Game", 14, Get.Font.NORMAL, OPTION_SIZE, Point.Empty, OPTION_COLOR, Color.White);

		PLAY_GAME.Click += (s, e) => PlayGame();//Same ideology for the others.

		Add.Button(START_MENU_CONTAINER, INVENTORY, "Inventory", 14, Get.Font.NORMAL, OPTION_SIZE, new Point(PLAY_GAME.Left + PLAY_GAME.Width + 20, 0), OPTION_COLOR, Color.White);
		Add.Button(START_MENU_CONTAINER, GAME_SETTINGS, "Settings", 14, Get.Font.NORMAL, OPTION_SIZE, new Point(INVENTORY.Left + INVENTORY.Width + 20, 0), OPTION_COLOR, Color.White);
	    }

	    catch { };
	}
	
	static Selector selector = null;

	public void PlayGame()
	{
	    if (selector == null)
	    {
		selector = new Selector(Lunaroc.GetOwner());
	    };

	    VisibilityManager.ShowComponent(Selector.SELECTOR_MENU);
	}
    };
};