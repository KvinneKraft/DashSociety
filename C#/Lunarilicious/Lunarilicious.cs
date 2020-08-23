
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
    public class Entity
    {
	public readonly List<PictureBox> Characters = new List<PictureBox>();
	public readonly List<PictureBox> Effects = new List<PictureBox>();

	public enum CharacterType
	{
	    Pug
	};

	public enum EffectType
	{
	    LandExplosion
	};

	private readonly Form Owner = Lunaroc.getOwner();

	public void LoadCharacters()
	{
	    // Add in a property read mechanism so it can do all of the work for me.

	    PictureBox character = new PictureBox();

	    character.BackColor = Color.FromArgb(0, 0, 0, 255);
	    character.Image = Image.FromFile("data\\characters\\Pug.gif"); // X - 16 = Actual
	    character.Size = new Size(128, 128);
	    character.Location = new Point(0, Owner.Height - 43 * 2);

	    Owner.Controls.Add(character);
	    Characters.Add(character);

	    PictureBox effect = new PictureBox();

	    effect.BackColor = Color.FromArgb(0, 0, 0, 255);
	    effect.Image = Image.FromFile("data\\effects\\LandExplosion.gif");
	    effect.Size = new Size(128, 128);
	    effect.Location = Point.Empty;
	    effect.Visible = false;

	    Owner.Controls.Add(effect);
	    Effects.Add(effect);
	}

	public void playEffect(Point location, int duration, EffectType type)
	{
	    Control control = Effects[(int)type]; // New object?

	    control.Location = new Point(location.X, location.Y);

	    Lunaroc.EquippedCharacter.Visible = false;
	    Lunaroc.EquippedCharacter.Enabled = false;

	    control.Visible = true;

	    System.Timers.Timer scheduler = new System.Timers.Timer(duration);
	    
	    scheduler.Enabled = true;
	    
	    scheduler.Elapsed += (s, e) =>
	    {
		control.Visible = false;

		Lunaroc.EquippedCharacter.Visible = true;
		Lunaroc.EquippedCharacter.Enabled = true;

		scheduler.Dispose();
	    };

	    scheduler.Start();
	}
    };

    public class Sounds
    {
	private readonly List<SoundPlayer> SoundPlayers = new List<SoundPlayer>();

	public void LoadSoundData()
	{
	    List<string> paths = new List<string>();

	    if (SoundPlayers.Count > 0)
	    {
		SoundPlayers.Clear();
	    };

	    foreach (SoundType sound in Enum.GetValues(typeof(SoundType)))
	    {
		SoundPlayers.Add(new SoundPlayer("data\\sounds\\" + sound.ToString() + ".wav"));
	    };
	}

	public enum SoundType
	{
	    DogAttack,
	    DogWalk,
	    DogGrowl
	};

	private readonly Form Owner = Lunaroc.getOwner();

	public void playSound(SoundType sound_id)
	{
	    Owner.Invoke
	    (
		(MethodInvoker)delegate ()
		{
		    SoundPlayers[(int)sound_id].Play();
		}
	    );
	}
    };

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

	    //--- Character Manipulation
	    try
	    {
		KeyDown += (s, e) =>
		{
		    int x = EquippedCharacter.Location.X;
		    int y = EquippedCharacter.Location.Y;

		    Keys key = e.KeyData;

		    if (key == Keys.A && !isInAir && !isOnCooldown)
		    {
			if (x > -16)
			{
			    x -= 15;
			};

			Sound.playSound(Sounds.SoundType.DogWalk);
		    }

		    else if (key == Keys.D && !isInAir && !isOnCooldown)
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

			    Creature.playEffect(EquippedCharacter.Location, 500, Entity.EffectType.LandExplosion);
			    
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

		//FormBorderStyle = FormBorderStyle.None;
		StartPosition = FormStartPosition.CenterScreen;

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
