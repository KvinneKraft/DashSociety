
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

	public static class EntityType
	{
	    public static class Pony
	    {
		public static List<PictureBox> pony_types = new List<PictureBox>();
		public static List<string> pony_names = new List<string>();
	    };

	    public static class Pug
	    {
		public static List<PictureBox> pug_types = new List<PictureBox>();
		public static List<string> pug_names = new List<string>();
	    };
	};

	private readonly Form Owner = Lunaroc.getOwner();

	public void LoadCharacters()
	{
	    // Add in a property read mechanism so it can do all of the work for me.
	    // What about guns and crates?

	    try //---Initialize Ponies:
	    {
		if (EntityType.Pony.pony_types.Count > 0)
		{
		    EntityType.Pony.pony_types.Clear();
		    EntityType.Pony.pony_names.Clear();
		};
		
		foreach (var pony_file in new DirectoryInfo("data\\characters\\ponies\\").GetFiles("*.gif"))
		{
		    PictureBox pony = new PictureBox();

		    string pony_file_name = pony_file.Name.Replace(".gif", string.Empty).ToLower();

		    pony.Image = Image.FromFile($"data\\characters\\ponies\\{pony_file_name}.gif");
		    pony.BackColor = Color.FromArgb(0, 0, 0, 255);
		    pony.Size = pony.Image.Size;

		    EntityType.Pony.pony_types.Add(pony);
		    EntityType.Pony.pony_names.Add(pony_file_name);
		};
	    }

	    catch { };

	    try //---Initialize Puggers:
	    {
		if (EntityType.Pug.pug_types.Count > 0)
		{
		    EntityType.Pug.pug_types.Clear();
		    EntityType.Pug.pug_names.Clear();
		};

		foreach (var pug_file in new DirectoryInfo("data\\characters\\pugs\\").GetFiles("*.gif"))
		{
		    PictureBox pug = new PictureBox();

		    string pug_file_name = pug_file.Name.Replace(".gif", string.Empty).ToLower();

		    pug.Image = Image.FromFile($"data\\characters\\pugs\\{pug_file.Name.Replace("gif", string.Empty).ToLower()}.gif");
		    pug.BackColor = Color.FromArgb(0, 0, 0, 255);
		    pug.Size = pug.Image.Size;

		    EntityType.Pug.pug_types.Add(pug);
		    EntityType.Pug.pug_names.Add(pug_file_name);
		};
	    }

	    catch { };
	}
    };
};