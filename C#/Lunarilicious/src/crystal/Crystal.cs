
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
    public class Crystal
    {
	public static class CrystalType
	{
	    public static List<PictureBox> graphics = new List<PictureBox>();
	    
	    public static List<string> names = new List<string>();
	    
	    public static List<ulong> sell_p = new List<ulong>();
	    public static List<int> sell_a = new List<int>();

	    public static List<ulong> buy_p = new List<ulong>();
	    public static List<int> buy_a = new List<int>();

	    public static List<int> tier = new List<int>();
	    public static List<int> cid = new List<int>();
	};

	private string formatConfigLine(string line) // Put into String class
	{
	    line = line.Replace("name:", string.Empty).Replace("tier:", string.Empty).Replace("sell:", string.Empty).Replace("buy:", string.Empty).Replace("\'", string.Empty).Replace("\"", string.Empty);

	    int length = 0;

	    for (int l = 0; l < line.Length; l += 1)
	    {
		if (!line[l].Equals(' '))
		{
		    break;
		};

		length += 1;
	    };

	    return line.Remove(0, length);
	}

	private void toStringDialog(List<string> list)
	{
	    string result = string.Empty;

	    foreach (string line in list)
	    {
		result += line + "\r\n";
	    };

	    MessageBox.Show(result);
	}

	private string removeEmpty(string str)
	{
	    int length = 0;

	    for (int k = 0; k < str.Length; k += 1)
	    {
		if (str[k].Equals(' '))
		{
		    length += 1;
		    continue;
		};

		break;
	    };

	    return str.Remove(0, length);
	}

	public enum Properties { name, tier, sell, buy };

	public void LoadCrystals()
	{
	    /*
		- Convert the YML config to usable data, put it all into the above lists.
	     */

	    if (CrystalType.graphics.Count > 0)
	    {
		CrystalType.graphics.Clear();
		CrystalType.names.Clear();

		CrystalType.sell_p.Clear();
		CrystalType.sell_a.Clear();

		CrystalType.buy_p.Clear();
		CrystalType.buy_a.Clear();

		CrystalType.tier.Clear();
		CrystalType.cid.Clear();
	    };

	    try //---Initialize Crystal Graphics:
	    {
		foreach (var crystal_file in new DirectoryInfo("data\\crystals\\").GetFiles("*.png"))
		{
		    PictureBox crystal = new PictureBox();
		    
		    crystal.Image = Image.FromFile(crystal_file.FullName);
		    crystal.BackColor = Color.FromArgb(0, 0, 0, 255);
		    crystal.Size = crystal.Image.Size;

		    CrystalType.graphics.Add(crystal);
		};
	    }

	    catch (Exception e) { MessageBox.Show($"{e}"); };

	    try //---Initialize Crystal Configuration:
	    {
		List<string> config = File.ReadAllLines("data\\config\\crystal.yml").ToList();
		
		for (int k = 0; k < config.Count; k += 1)
		{
		    // Something causes the algorithm to not detect 
		    // all of the put hash tags.

		    if (config[k].Contains("#"))
		    {
			config[k].Remove(k);
		    };

		    //---Ordering configuration data:
		    if (Integers.isNumeric(config[k]))
		    {
			List<string> properties = new List<string>();

			for (int s_k = k+1; s_k < k+5; s_k += 1)
			{
			    string setting = formatConfigLine(config[s_k]);
			    properties.Add(setting);
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

			k += 4;
		    };
		};

		toStringDialog(config);
	    }

	    catch (Exception e) { MessageBox.Show($"{e}"); };
	}
    };
};