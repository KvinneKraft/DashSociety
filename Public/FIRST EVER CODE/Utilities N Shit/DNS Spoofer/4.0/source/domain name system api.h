
/* --- (c) All Rights Reserved, Dashies Software Inc --- */

namespace DNS_API {
	struct DomainNameSystem{
		std::string argument, command, compiled_address, InterfaceName;
		
		ULONG buflen = sizeof(IP_ADAPTER_INFO);
		IP_ADAPTER_INFO *pAdapterInfo = (IP_ADAPTER_INFO *)malloc(buflen);
		
		long int mode;
		char address2[417], address1[417], conv[4027];
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
	 "% Safe DNS........:  195.46.39.39   -> 195.46.39.40 \n"
	 "% Cloudflare DNS.....:  1.1.1.1        -> 1.0.0.1 \n\n"
	 
	 "--- Regular DNS Services (IPv6) ---\n"
	 "% Public Google DNS: 2001:4860:4860::8888 \n"
	 "% he.net DNS.......: 2001:470:20::2 \n"
	 "% OpenDNS 1........: 2620:0:ccc::2 \n"
	 "% OpenDNS 2........: 2620:0:ccd::2 \n\n"
	 
	 "--- Security DNS Services (IPv6) \n"
	 "% <NONE> \n")
};

std::string HelpMessage[] = {
	("Usage : .\\\"Dashies DNS Spoofer.exe\" --domain-name-service [OPTIONS] \n\n"
	 
	 "\"--domain-name-service\" or \"-D\" \n"
	 "% allows you to manage your DNS. \n\n"
	 
	 "\"--domain-name-service --change --IPv4 [DNS 1] [DNS 2]\" or \"-D -C -4 [DNS 1] [DNS 2]\" \n"
	 "% allows you to change your IPv4 DNS. \n\n"
	 
	 "\"--domain-name-service --change --IPv6 [DNS]\" or \"-D -C -6 [DNS]\" \n"
	 "% allows you to change your IPv6 DNS. \n\n"
	 
	 "\"--domain-name-service --register-dns\" or \"-D -R\" \n"
	 "% allows you to register your DNS Server(s). \n\n"
	 
	 "\"--domain-name-service --flush\" or \"-D -F\" \n"
	 "% allows you to flush your DNS Cache. \n\n"
	 
	 "\"--domain-name-service --free-dns-services\" or \"-D -O\" \n"
	 "% shows and allows you to use free DNS services.")
};

bool Invalid(std::string INVALID) {
	std::cout << "[DNS Spoofer]: invalid argument(s) received for " << INVALID << std::endl;
   exit(0);
}

bool Help() {
	std::cout << HelpMessage[0];
}

bool Insufficient() {
    std::cout << "[DNS Spoofer]: insufficient argument(s) received!" << std::endl;
	std::cout << "[DNS Spoofer]: usage : .\\\"Dashies DNS Spoofer.exe\" [OPTIONS]" << std::endl;
	std::cout << "[DNS Spoofer]: use /? for argument usage help." << std::endl;
	
   exit(0);
}

/* --- END --- */
