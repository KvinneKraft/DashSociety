
// -- The start of something greater than this.

// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Media;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Lunarilicious
{ 
    class Lunaroc : Form
    { 
	bool isOnCooldown = false;
	bool isInAir = false;

	readonly static PictureBox EquippedCharacter;

	Crystal Crystal;
	Entity Creature;

	class SecondaryApp : Form
	{
	    PictureBox bar = new PictureBox();
	    TextBox input = new TextBox();
	    Label label = new Label();

	    public SecondaryApp()
	    {
		Size = new Size(500, 500);

		Add.MenuBar(this, bar, Color.Black, label, "Some App", 1, 10, Color.White, _border:true, _borderColor:Color.Black, _quit:true, _minimize:true, _draggable:true);
		//Add.SecondaryApp sap = new Add.SecondaryApp("Test App", Font, appSize:new Size(350, 350), appLocation:Point.Empty, appBorderStyle:FormBorderStyle.None, showInTaskBar: false, hasStartMenu:true);
		
		//sap.ShowDialog();
	    }
	}

	public void Runtime()
	{
	    // TEST AREA

	    //SecondaryApp app = new SecondaryApp();
	    //app.ShowDialog();

	    //Environment.Exit(-1);

	    // END;

	    Creature = new Entity();
	    Creature.LoadCharacters();

	    Crystal = new Crystal();
	    Crystal.LoadCrystals();

	    // - Work on the Character Selector ( MAKE SURE THAT THE USER CAN PURCHASE UNAVAILABLE
	    //					  CHARACTERS, IT IS IMPORTANT! )
	    // - Work on the Shop G.U.I.
	    // - Work on the Inventory G.U.I.
	    // - Add Catch Statements around Initialization Things

	    //EquippedCharacter = Creature.Characters[0]; // Should add in a selector
	    
	    //Sound.LoadGameTracks();

	    RegisterEvents();
	    this.Show();
	}

	public void RegisterEvents()
	{
	    try
	    {
		KeyDown += (s, e) => 
		{
		    int x = EquippedCharacter.Location.X;
		    int y = EquippedCharacter.Location.Y;

		    Keys key = e.KeyData;

		    if (key == Keys.A && !isInAir)
		    {
			if (x > -16)
			{
			    x -= 15;
			};
		    }

		    else if (key == Keys.D && !isInAir)
		    {
			if (x < Width - 74)
			{
			    x += 15;
			};
		    }

		    else if (key == Keys.S)
		    {

		    }

		    else if ((key == Keys.W || key == Keys.Space) && !isInAir)
		    {
			isInAir = true;

			new Thread(() =>
			{
			    for (int t = 0; t < 10; t += 1)
			    {
				EquippedCharacter.Location = new Point(EquippedCharacter.Location.X, EquippedCharacter.Location.Y - 5);
				Thread.Sleep(10);
			    };

			    Thread.Sleep(250);

			    for (int t = 0; t < 10; t += 1)
			    { 
				EquippedCharacter.Location = new Point(EquippedCharacter.Location.X, EquippedCharacter.Location.Y + 5);
				Thread.Sleep(10);
			    };

			    Thread.Sleep(2000);

			    isInAir = false;
			})

			{ IsBackground = true }.Start();
		    }

		    else if (key == Keys.F && !isOnCooldown)
		    {
			System.Timers.Timer scheduler = new System.Timers.Timer
			{
			    Interval = 3000,
			    Enabled = true
			};

			scheduler.Elapsed += (ss, se) =>
			{
			    if (isOnCooldown)
			    {
				isOnCooldown = false;
			    };

			    scheduler.Dispose();
			};

			isOnCooldown = true;

			scheduler.Start();
		    };

		    Point location = new Point();

		    if (isInAir)
		    {
			location.X = x;
			location.Y = EquippedCharacter.Location.Y;
		    }

		    else
		    {
			location.X = x;
			location.Y = y;
		    };

		    EquippedCharacter.Location = location;
		    EquippedCharacter.Update();
		};
	    }

	    catch { };
	}

	readonly new StartMenu Menu = new StartMenu();
	
	public Lunaroc()
	{
	    Hide();
	    Owner = this;
	    
	    try
	    {
		Size sz = new Size(500, 425);

		MinimumSize = sz;
		MaximumSize = sz;

		Text = "Lunarilicious";

		Menu.Setup(this);
	    }

	    catch { };

	    Runtime();
	}

	new static Form Owner;

	public static Form GetOwner()
	{
	    return Owner;
	}
    };
};
