
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
    class Integers
    {
	public static bool isNumeric(string k)
	{
	    return int.TryParse(k.Replace(":", string.Empty).Replace(" ", string.Empty), out int t);
	}

	public static int toInt(string v)
	{
	    return int.Parse(v);
	}

	public static ulong toUInt(string v)
	{
	    return ulong.Parse(v);
	}
    };
};
