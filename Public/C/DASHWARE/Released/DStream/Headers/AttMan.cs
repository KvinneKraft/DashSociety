
// Author: Dashie
// Version: 1.0

// A class for obtaining file information.  Ugly Class BEHHH

using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;

namespace DStream
{
    public class ATTMAN
    {
	public enum Properties
	{//Values only for readability.
	    FILE_PATH = 1, FILE_NAME = 2, ORIGINAL_NAME = 3, PRODUCT_NAME = 4, INTERNAL_NAME = 5,
	    FILE_EXSTENSION = 6, FILE_DESCRIPTION = 7, FILE_VERSION = 8, LEGAL_COPYRIGHT = 9,
	    COMPANY_NAME = 10, COMMENTS = 11, CREATION_DATE = 12, LAST_MODIFICATION_DATE = 13,
	    LAST_ACCESS_DATA = 14, NONE = -999
	};

	public enum Attributes
	{//Values only for readability.
	    READ_ONLY = 15, ENCRYPTED = 16, TEMPORARY = 17, SYSTEM = 18, NONE = -999
	};

	public static string GetRawValue(List<string> cache, Properties prop = Properties.NONE, Attributes attr = Attributes.NONE)
	{
	    string raw = string.Empty;

	    if (prop != Properties.NONE)
		raw = cache[(int)prop];

	    else if (attr != Attributes.NONE)
		raw = cache[(int)attr];

	    if (raw.Length > raw.Split('~')[0].Length + 1)
		raw = raw.Remove(0, cache[(int)prop].LastIndexOf("~") + 2);

	    return raw;
	}

	public static List<string> GetProperties(string file, bool throw_error)
	{
	    var result = new List<string>();

	    try
	    {
		var fvin = FileVersionInfo.GetVersionInfo(file);
		var attr = File.GetAttributes(file);
		var info = new FileInfo(file);

		var values = new string[]
		{
			"===FILE DETAILS===",
			$"FILE PATH~ {info.FullName}",
			$"FILE NAME~ {info.Name}",
			$"ORIGINAL NAME~ {fvin.OriginalFilename}",
			$"PRODUCT NAME~ {fvin.ProductName}",
			$"INTERNAL NAME~ {fvin.InternalName}",
			$"FILE EXTENSION~ {info.Extension}",
			$"FILE DESCRIPTION~ {fvin.FileDescription}",
			$"FILE VERSION~ {fvin.FileVersion}",
			$"LEGAL COPYRIGHT~ {fvin.LegalCopyright}",
			$"COMPANY NAME~ {fvin.CompanyName}",
			$"COMMENTS~ {fvin.Comments}",
			$"CREATION DATE~ {info.CreationTime}",
			$"LAST MODIFICATION DATE~ {info.LastWriteTime}",
			$"LAST ACCESS DATE~ {info.LastAccessTime}",
			"===FILE ATTRIBUTES===",
			$"READ-ONLY~ {attr.HasFlag(FileAttributes.ReadOnly)}",
			$"ENCRYPTED~ {attr.HasFlag(FileAttributes.Encrypted)}",
			$"TEMPORARY~ {attr.HasFlag(FileAttributes.Temporary)}",
			$"SYSTEM~ {attr.HasFlag(FileAttributes.System)}"
		};

		foreach (var value in values)
		    result.Add($"{value}");
	    }

	    catch (Exception e)
	    {
		if (throw_error)
		{
		    MessageBox.Show($"Hey there, it seems like an exception has been thrown by this application.\r\n\r\nIn order to fix this, you could reach out to me at KvinneKraft@protonmail.com and send me the following: {e.Message}\r\n\r\nMy sincere apologies, but hey, what do YOU expect? :p");
		    Application.Exit();
		};
	    };

	    return result;
	}
    }
}
