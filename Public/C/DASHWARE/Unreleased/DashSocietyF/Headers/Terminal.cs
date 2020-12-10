using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DashSocietyF
{
    public class Terminal
    {
	static void GetCommander()
	{
	    string creature = "&c--:(/&eusr&c/&eplot&c/&eDashSociety&c)>&a ";
	    Tool.TranslateColors(creature, r: false);
	}

	static void ExecuCommand(string data)
	{
	    if (data.Length < 1)
	    {
		Tool.TranslateColors("&8(&c-&8) &fYou must type something man.\r\n");
		return;
	    };

	    CommandHandler.Handler(data.Split(' '));
	}

	public static void Show()
	{
	    while (true)
	    {
		GetCommander();

		var data = Console.ReadLine();

		ExecuCommand(data);
	    };
	}
    }
}
