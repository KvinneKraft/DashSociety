using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace DashSocietyF
{
    public class TerminalPak
    {
	static void GetCommander()
	{
	    string creature = "&f(&c/&dusr&c/&dplot&c/&dDashSociety&f)>&c ";
	    Tool.TranslateColors(creature, r: false);
	}

	static void ExecuCommand(string data)
	{
	    if (data.Length < 1)
	    {
		Tool.TranslateColors("&8(&c-&8) &fYou must type something man.");
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
