
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

	public enum CharacterType
	{
	    Pug
	};

	private readonly Form Owner = Lunaroc.getOwner();

	public void LoadCharacters()
	{
	    // Add in a property read mechanism so it can do all of the work for me.
	    // What about guns and crates?

	    PictureBox character = new PictureBox();

	    character.BackColor = Color.FromArgb(0, 0, 0, 255);
	    character.Image = Image.FromFile("data\\characters\\Pug.gif"); // X - 16 = Actual
	    character.Size = new Size(128, 128);
	    character.Location = new Point(0, Owner.Height - 43 * 2);

	    Owner.Controls.Add(character);
	    Characters.Add(character);
	}
    };
};