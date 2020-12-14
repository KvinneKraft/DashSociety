
/* --- (c) All Rights Reserved, Dashies Software Inc --- */

namespace DNS_API {
	struct DomainNameSystem{
		std::string argument, format, command, compiled_address, InterfaceName, access;
		
		ULONG buflen = sizeof(IP_ADAPTER_INFO);
		IP_ADAPTER_INFO *pAdapterInfo = (IP_ADAPTER_INFO *)malloc(buflen);
		
		long int mode;
		bool verbose;
		char address2[417], address1[417], conv[4027];
	
	  struct Options{
		  std::string IP_Puller, IPv, ip1, ip2;
          
          long int IP_Puller_At; 
	  }options;
	}dns_spoofer;
}

std::string FREE_OPEN_DNS_SERVICES[] = {
	("--- Regular DNS Services (IPv4) ---\n"
	 "% Public Google DNS:  8.8.8.8        -> 8.8.4.4 \n"
	 "% Advantage DNS....:  156.154.70.1   -> 156.154.71.1 \n"
	 "% Fool DNS.........:  87.118.111.215 -> 81.174.67.134 \n"
	 "% T3 DNS...........:  80.244.65.130  -> 80.244.65.3 \n"
	 "% Cisco DNS........:  64.102.255.44  -> 128.107.241.185 \n"
	 "% Tiscali DNS......:  195.241.77.53  -> 195.241.77.54 \n"
	 "% Open Nic DNS.....:  58.6.115.42    -> 58.6.115.43 \n"
	 "% Open Home DNS....:  208.67.222.222 -> 208.67.220.220 \n"
	 "% Free DNS.........:  37.235.1.174   -> 37.235.1.177 \n\n"
	 
	 "--- Security DNS Services (IPv4) ---\n"
	 "% Secure Comodo DNS..:  8.26.56.26     -> 8.20.247.20 \n"
	 "% Comodo DNS.........:  156.154.70.22  -> 156.154.71.22 \n"
	 "% Norton ConnectSafe.:  199.85.126.10  -> 199.85.127.10 \n"
	 "% Norton Symantec DNS:  198.153.192.1  -> 198.153.194.1 \n"
	 "% Safe DNS...........:  195.46.39.39   -> 195.46.39.40 \n"
	 "% Cloudflare DNS.....:  1.1.1.1        -> 1.0.0.1 \n\n"
	 
	 "--- Regular DNS Services (IPv6) ---\n"
	 "% Public Google DNS: 2001:4860:4860::8888 \n"
	 "% he.net DNS.......: 2001:470:20::2 \n"
	 "% OpenDNS 1........: 2620:0:ccc::2 \n"
	 "% OpenDNS 2........: 2620:0:ccd::2 \n\n"
	 
	 "--- Security DNS Services (IPv6) \n"
	 "% <NONE> \n")
};

std::string HELP_MESSAGE[] = {
	("Usage : .\\\"Puuny Spuuwfer.exe\" [OPTIONS] \n\n"
	 "<----------------- Puuny Spuuwfer -----------------> \n"
     "\n"
	 "\"--show-list\" or \"-S\" : show a list with free dns services~ \n"
	 "\"--help\" or \"-H\" : show this text message~ \n"
	 "\"--change\" or \"-C\" : change your dns by specifying options~ \n"
     "\"--internet-address\" {IP1} if(IPv4{IP2}) or \"-I\" {IP1} if(IPv4{IP2}) \n ^allows you to set the new IP Address(es) (should be parsed as last parameter!!!!)~ \n"
	 "\"--ipv4\" or \"-4\" : set the IP version to 4~ \n"
	 "\"--ipv6\" or \"-6\" : set the IP version to 6~ \n"
	 "\n"
     "<----------------- Puuny Spuuwfer -----------------> \n")
};

/* --- END --- */
