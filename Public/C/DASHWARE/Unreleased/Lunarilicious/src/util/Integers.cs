
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
	public static int CenterOf(Control b, Control t) => (b.Width - t.Width) / 2;

	public static bool IsNumeric(string k)
	{
	    return int.TryParse(k.Replace(":", string.Empty).Replace(" ", string.Empty), out int _);
	}

	public static int ToInt(string v)
	{
	    return int.Parse(v);
	}

	public static ulong ToUInt(string v)
	{
	    return ulong.Parse(v);
	}
    };
};
