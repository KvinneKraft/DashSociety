
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Text;
using System.Collections.Generic;

namespace DashCore
{
    public class Product
    {
	public readonly Dictionary<int, List<string>> speciments = new Dictionary<int, List<string>>();
	public readonly Dictionary<int, List<string>> quantities = new Dictionary<int, List<string>>();
	public readonly Dictionary<int, List<double>> prices = new Dictionary<int, List<double>>();

	public readonly int ID_SPECIMENT_FLOWERS = 0;
	public readonly int ID_SPECIMENT_LEAFS = 1;
	public readonly int ID_SPECIMENT_WEED = 2;
	public readonly int ID_SPECIMENT_TEA = 3;

	public readonly List<string> categories = new List<string>() { "Flowers", "Leafs", "Gangja Strains", "Teas" };

	public readonly string currency = "$";

	public void initdb()
	{
	    for (int key = 0; key < categories.Count; key += 1)
	    {
		speciments[key] = new List<string>();
		quantities[key] = new List<string>();
		prices[key] = new List<double>();
	    };

	    try
	    {
		List<string> configs = new List<string>()
		{
		    Encoding.ASCII.GetString(Properties.Resources.flowers),
		    Encoding.ASCII.GetString(Properties.Resources.leafs),
		    Encoding.ASCII.GetString(Properties.Resources.weed),
		    Encoding.ASCII.GetString(Properties.Resources.tea),
		};

		for (int key = 0; key < configs.Count; key += 1)
		{
		    string[] t_arr = configs[key].Replace(Environment.NewLine, string.Empty).Split(';');
		    
		    for (int s_key = 0; s_key < t_arr.Length - 1; s_key += 1)
		    {
			string[] m_arr = t_arr[s_key].Split(',');
			
			double price = Double.Parse(m_arr[1].Replace("price: ", ""));
			string quanta = m_arr[2].Replace("quantity: ", "");
			string strains = m_arr[0].Replace("type: ", "");

			speciments[key].Add(strains);
			quantities[key].Add(quanta);
			prices[key].Add(price);
		    };
		};
	    }

	    catch
	    {
		dush.say("# The configuration file is corrupt.\n");
		dush.say("# Press any key to exit.");
		dush.halt();

		Environment.Exit(-1);
	    };
	}
    };
};