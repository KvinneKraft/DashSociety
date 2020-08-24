
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
    public class Lunaroc : Form
    { 
	protected bool isOnCooldown = false;
	protected bool isInAir = false;

	public static PictureBox EquippedCharacter;
	protected Entity Creature;

	public static bool isNumeric(string k)
	{
	    return int.TryParse(k.Replace(":", ""), out int t);
	}

	public void Runtime()
	{
	    // Configuration File Loader ;)
	    List<string> config = File.ReadAllLines("data\\config\\crystal.yml").ToList();

	    for (int k = 0; k < config.Count; k += 1)
	    {
		if (k != 0) k -= 1;
		
		if (config[k].StartsWith("#") || config[k] == string.Empty)
		{
		    config.RemoveAt(k);
		    continue;
		};

		break;
	    };

	    for (int k = 0 ; k < config.Count ; k += 1)
	    {
		if (isNumeric(config[k]))
		{
		    for (int s_k = k + 1; s_k < 8; s_k += 1)
		    {
			string setting = config[s_k].ToLower();

			MessageBox.Show(setting);
		    };

		    k += 6;

		    continue;
		};
	    };

	    Creature = new Entity();
	    Creature.LoadCharacters();

	    //EquippedCharacter = Creature.Characters[0]; // Should add in a selector
	    
	    Sound.LoadGameTracks();

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

	StartMenu Menu = new StartMenu();

	public Lunaroc()
	{
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

	static Form Owner;

	public static Form getOwner()
	{
	    return Owner;
	}
    };
};
