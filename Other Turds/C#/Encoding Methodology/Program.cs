
// Author: Dashie
// Version: 1.0

using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace Encoding_Methodology
{
    class Program
    {
	static List<string> iKeys = new List<string>();

	static int[] getEncoded(string data)
	{
	    int[] keys = new int[data.Length];

	    for (int k = 0; k < data.Length; k += 1)
	    {
		if (iKeys.Contains(data[k].ToString()))
		{
		    keys[k] = iKeys.IndexOf(data[k].ToString());
		};
	    };

	    return keys;
	}

	static string getDecoded(int[] data)
	{
	    string decoded = "";

	    foreach (int k in data)
	    {
		if (iKeys.Count > k)
		{
		    decoded += iKeys[k];
		};
	    };

	    return decoded;
	}

	static string getKey(char key)
	{
	    return iKeys[iKeys.IndexOf(key.ToString())];
	}

	static void Main(string[] args)
	{
	    if (iKeys.Count < 1)
	    {
		string data = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890-=_+[]{};:\'\",<.>/? ~`!@#$%^&*()";

		foreach (char c in data)
		{
		    iKeys.Add(c.ToString());
		};
	    };

	    // None-Private Encoding:
	    int[] encoded = getEncoded("Hello");
	    // None-Private Decoding:
	    string decoded = getDecoded(encoded);
	    // Private Encoding/Decoding:
	    string ultimate = getKey('H') + getKey('e') + getKey('l') + getKey('l') + getKey('o') + getKey('!');

	    // Back when I used to only code in C++ I would use this technique to prevent people from reading the raw 
	    // strings in my code, using the above methodology you will find it doing what it should, hide your raw string
	    // values, the values are run-time generated which means that the majority of forensics scanning software
	    // will not be picking up your string data. 
	    // 
	    // This used to be effective back in Windows 7 and I think that it still is.

	    Console.ReadKey();
	}
    }
}
