
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
	    
	    Size size = Injector.Get.FontSize(title, BAR_TITLE.Font);

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

		Injector.Add.RuImage(Base, BAR_OBJECT, null, new Size(Base.Width, 26), Point.Empty); //BAR_OBJECT.BackColor = Owner.BackColor;
		Injector.Set.Draggable(BAR_OBJECT, Base);

		Base.Paint += (s, e) =>
		{
		    Injector.Add.Rectangle(e, Base.BackColor, 2, new Size(Base.Width, Base.Height - 26), new Point(0, 25));
		};

		Injector.Add.AButton(BAR_OBJECT, QUIT_OBJECT, new Size(60, BAR_OBJECT.Height), new Point(BAR_OBJECT.Width - 60, 0), BAR_OBJECT.BackColor, Color.FromArgb(255, 255, 255), "X", Injector.Get.FONT_TYPE_MAIN, 10);

		QUIT_OBJECT.Click += (s, e) =>
		{
		    Environment.Exit(-1);
		};

		Injector.Add.AButton(BAR_OBJECT, MINIMIZE_OBJECT, new Size(60, BAR_OBJECT.Height), new Point(BAR_OBJECT.Width - 120, 0), BAR_OBJECT.BackColor, Color.FromArgb(255, 255, 255), "-", Injector.Get.FONT_TYPE_MAIN, 10);

		MINIMIZE_OBJECT.Click += (s, e) =>
		{
		    Base.SendToBack();
		};

		Injector.Add.ThaLabel(BAR_OBJECT, BAR_TITLE, Point.Empty, Color.White, string.Empty, Injector.Get.FONT_TYPE_MAIN, 10); /*PURPOSELY*/  Injector.Set.Draggable(BAR_TITLE, Base);

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

		Injector.Add.RuImage(START_MENU, GAME_TITLE_OBJECT, Image.FromFile("data\\gui\\GameTitle.png"), new Size(463, 55), new Point(-1, 24));
		GAME_TITLE_OBJECT.BackColor = Color.FromArgb(0, 0, 0, 255);

		// Hide Owner when going to another menu, perhaps the shop or so.

		Injector.Add.RuImage(START_MENU, START_MENU_CONTAINER, null, new Size(415, 56), new Point(-1, 120));
		START_MENU_CONTAINER.BackColor = Color.FromArgb(0, 0, 0, 255);

		Color OPTION_COLOR = Color.FromArgb(16, 16, 16);
		Size OPTION_SIZE = new Size(125, START_MENU_CONTAINER.Height);
		
		Injector.Add.AButton(START_MENU_CONTAINER, PLAY_GAME, OPTION_SIZE, Point.Empty, OPTION_COLOR, Color.White, "Play Game", Injector.Get.FONT_TYPE_MAIN, 14);

		PLAY_GAME.Click += (s, e) => PlayGame();//Same ideology for the others.

		Injector.Add.AButton(START_MENU_CONTAINER, INVENTORY, OPTION_SIZE, new Point(PLAY_GAME.Left + PLAY_GAME.Width + 20, 0), OPTION_COLOR, Color.White, "Inventory", Injector.Get.FONT_TYPE_MAIN, 14);
		Injector.Add.AButton(START_MENU_CONTAINER, GAME_SETTINGS, OPTION_SIZE, new Point(INVENTORY.Left + INVENTORY.Width + 20, 0), OPTION_COLOR, Color.White, "Settings", Injector.Get.FONT_TYPE_MAIN, 14);
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