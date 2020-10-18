
// Author: Dashie
// Version: 5.0

using System.Collections.Generic;

namespace SimpleFlood
{
    public class DashOS : HostUT /*Dash -> Operation System*/
    {
	public enum NotiTypes
	{
	    ERROR, WARNING, INFO, SUCCESS, PROCESS
	};

	static readonly Dictionary<NotiTypes, string> nTable = new Dictionary<NotiTypes, string>()
	{
	    { NotiTypes.WARNING, "(!):" },
	    { NotiTypes.SUCCESS, "(+):" },
	    { NotiTypes.PROCESS, "(.):" },
	    { NotiTypes.ERROR, "(-):" },
	    { NotiTypes.INFO, "(?):" },
	};

	public static void Print(string message, NotiTypes type = NotiTypes.INFO)
	{
	    SimpleFlood.LaunchLogger.AppendText($"{nTable[type]} {message.Replace("/n", $"\r\n{nTable[type]} ")} \r\n");
	}
    };
};
