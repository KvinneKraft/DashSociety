
// Author: Dashie
// Version: 1.0

using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Lunarilicious
{
    public class Strings
    {
	public static void toStringDialog(List<string> list)
	{
	    string result = string.Empty;

	    foreach (string line in list)
	    {
		result += line + "\r\n";
	    };

	    MessageBox.Show(result);
	}

	public static string removeEmpty(string str)
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

	public static string[] criterias = { "file:", "buy:", "sell:", "tier:", "name:", "type:", "'", "\"" };

	public static string formatConfigLine(string line)
	{
	    foreach (string criteria in criterias)
	    {
		line = removeEmpty(line).Replace(criteria, string.Empty);
	    };
	    
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
    };
};