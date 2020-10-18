
// Author: Dashie
// Version: 5.0

using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;

namespace SimpleFlood
{
    public class HostUT
    {
	public enum AttaccModes
	{
	    HTTP, TCP, UDP, UNKNOWN,
	};

	public static AttaccModes getAttaccMode()
	{
	    AttaccModes mode = AttaccModes.UNKNOWN;

	    switch (MODE.mode)
	    {
		case 0:
		    mode = AttaccModes.HTTP;
		    break;
		case 1:
		    mode = AttaccModes.TCP;
		    break;
		case 2:
		    mode = AttaccModes.UDP;
		    break;
	    };

	    return mode;
	}

	public enum HostTypes
	{
	    IPv4, URI, UNKNOWN,
	};

	public static HostTypes getHostType()
	{
	    try
	    {
		if (!Uri.TryCreate(SimpleFlood.HostBox.Text, UriKind.RelativeOrAbsolute, out var uriResult) 
		    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps)) return HostTypes.IPv4;
		else 
		    return HostTypes.URI;
	    }

	    catch
	    {
		return HostTypes.IPv4;
	    };
	}

	public static bool IsOnline(HostTypes type)
	{
	    string IP = SimpleFlood.HostBox.Text; // assumes that the above was already ran.
	    
	    try
	    {
		switch (type)
		{
		    case HostTypes.URI:
		    {
			IP = Dns.GetHostAddresses(new Uri(SimpleFlood.HostBox.Text).Host)[0].ToString();
			break;
		    };
		};

		Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		IAsyncResult result = sock.BeginConnect(IP, int.Parse(SimpleFlood.PortBox.Text), null, null);

		if (result.AsyncWaitHandle.WaitOne(1000, true) && sock.Connected)
		{
		    sock.Close();
		    return true;
		};

		return false;
	    }

	    catch
	    {
		return false;
	    };
	}

	public static bool IsValidPort()
	{
	    try
	    {
		int PORT = int.Parse(SimpleFlood.PortBox.Text);

		if (PORT < 0 || PORT > 65565)
		{
		    return false;
		}
	    }

	    catch
	    {
		return false;
	    };

	    return true;
	}

	public static string getIP()
	{
	    string IP = SimpleFlood.HostBox.Text;

	    try
	    {
		switch (getHostType())
		{
		    case HostTypes.URI:
		    {
			IP = Dns.GetHostAddresses(new Uri(SimpleFlood.HostBox.Text).Host)[0].ToString();
			break;
		    };
		};
	    }

	    catch
	    {
		IP = SimpleFlood.HostBox.Text; // Dunno if it changes if the above errors.
	    };
		
	    return IP;
	}

	static readonly Dictionary<AttaccModes, byte[]> Data = new Dictionary<AttaccModes, byte[]>()
	{
	    { AttaccModes.HTTP, new byte[] {  } },
	    { AttaccModes.TCP, new byte[] {  } },
	    { AttaccModes.UDP, new byte[] {  } },
	};

	public static byte[] getData()
	{
	    return Data[getAttaccMode()]; // Assuming not NULL or UNKNOWN
	}
    };
}
