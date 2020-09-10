
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Media;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lunarilicious
{
    class Crystal
    {
	static class CrystalType
	{
	    public static readonly List<PictureBox> graphics = new List<PictureBox>();

	    public static void AddGraphics(string file)
	    {
		try
		{
		    PictureBox crystal = new PictureBox
		    {
			Image = Image.FromFile(file),
			BackColor = Color.FromArgb(0, 0, 0, 255)
		    };

		    crystal.Size = crystal.Image.Size;
		    graphics.Add(crystal);
		}

		catch /*(Exception e)*/
		{
		    // ERROR HANDLER?
		};
	    }

	    public static readonly List<string> names = new List<string>();
	    public static readonly List<string> files = new List<string>();
	    public static readonly List<string> types = new List<string>();

	    public static readonly List<ulong> sell_p = new List<ulong>();
	    public static readonly List<int> sell_a = new List<int>();

	    public static readonly List<ulong> buy_p = new List<ulong>();
	    public static readonly List<int> buy_a = new List<int>();
	     
	    public static readonly List<int> tier = new List<int>();
	    public static readonly List<int> cid = new List<int>();
	};

	class Info
	{
	    public static readonly int props = 7;
	};

	public void LoadCrystals()
	{
	    /*
		- Work on custom drops
		- Work on selector crystals
	     */
	    
	    try //---Initialize Crystal Configuration:
	    {
		List<string> config = File.ReadAllLines("data\\config\\shop\\crystal.yml").ToList();
		
		for (int k = 0; k < config.Count; k += 1)
		{
		    // Something causes the algorithm to not detect 
		    // all of the put hash tags.

		    if (config[k].Contains('#'))
		    {
			config.Remove(config[k]);
			config.RemoveAt(config.IndexOf(config[k]));
		    };

		    //---Ordering configuration data:
		    if (Integers.IsNumeric(config[k]))
		    {
			List<string> properties = new List<string>();

			k += 1;

			for (int s_k = k; s_k < k + Info.props; s_k += 1)
			{
			    properties.Add(Strings.formatConfigLine(config[s_k]));
			};

			CrystalType.tier.Add(int.Parse(properties[1]));

			string[] sap = properties[2].Split('~');

			CrystalType.sell_p.Add(ulong.Parse(sap[1]));
			CrystalType.sell_a.Add(int.Parse(sap[0]));

			string[] bap = properties[3].Split('~');

			CrystalType.buy_p.Add(ulong.Parse(bap[1]));
			CrystalType.buy_a.Add(int.Parse(bap[0]));

			CrystalType.names.Add(properties[0]);
			CrystalType.cid.Add(k - 1);

			CrystalType.AddGraphics(properties[4]);

			CrystalType.types.Add(properties[5]);

			if (properties[6].Equals("drops:"))
			{
			    properties.Remove("drops:");

			    // Continue Work: (Add in drops)
			};

			k += Info.props;
		    };
		};

		//Strings.toStringDialog(config);
	    }

	    catch (Exception e) { MessageBox.Show($"{e}"); };
	}
    };
};