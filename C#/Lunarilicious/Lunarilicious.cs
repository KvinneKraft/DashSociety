
// -- The start of something greater than this.

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
    public class Lunaroc : Form
    {
	readonly Entity Creature;
	readonly Sounds Sound;

	protected bool isOnCooldown = false;
	protected bool isInAir = false;

	public static PictureBox EquippedCharacter;

	public void Runtime()
	{
	    Creature.LoadCharacters();
	    Sound.LoadSoundData();

	    EquippedCharacter = Creature.Characters[0]; // Should add in a selector

	    RegisterEvents();
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

			Sound.playSound(Sounds.SoundType.DogWalk);
		    }

		    else if (key == Keys.D && !isInAir)
		    {
			if (x < Width - 74)
			{
			    x += 15;
			};

			Sound.playSound(Sounds.SoundType.DogWalk);
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
				Sound.playSound(Sounds.SoundType.DogAttack);

				Thread.Sleep(10);
			    };

			    Thread.Sleep(2000);

			    isInAir = false;
			})

			{ IsBackground = true }.Start();
		    }

		    else if (key == Keys.F && !isOnCooldown)
		    {
			System.Timers.Timer scheduler = new System.Timers.Timer();

			scheduler.Interval = 3000;
			scheduler.Enabled = true;

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

			Sound.playSound(Sounds.SoundType.DogGrowl);
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

	public Lunaroc()
	{
	    Owner = this;

	    //--- Main GUI
	    try
	    {
		BackColor = Color.FromArgb(16, 16, 16);

		Size sz = new Size(350, 350);

		MinimumSize = sz;
		MaximumSize = sz;

		FormBorderStyle = FormBorderStyle.None;// Custom Border??? Menu Screen??? Sound Track???
		StartPosition = FormStartPosition.CenterScreen;

		BackgroundImage = Image.FromFile("data\\gui\\GameWallpaper.png");

		// Icon = (Icon) icon;
		Text = "Lunarilicious";
	    }

	    catch { };

	    Creature = new Entity();
	    Sound = new Sounds();

	    Runtime();
	}

	static Form Owner;

	public static Form getOwner()
	{
	    return Owner;
	}
    };
};
