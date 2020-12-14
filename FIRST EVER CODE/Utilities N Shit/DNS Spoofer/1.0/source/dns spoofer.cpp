
/* --- (c) All Rights Reserved, Dashies Software Inc --- */


/* FIX DNS Server ISSUE UNABLE TO CONNECT! */


#include<windows.h>
#include<mmsystem.h>
#include<wininet.h>

#include<stdio.h>
#include<stdlib.h>
#include<string.h>

#include<lmcons.h>
#include<conio.h>
#include<math.h>

#include<wchar.h>
#include<tchar.h>

#include<iostream>
#include<string>

#include<algorithm>
#include<vector>

#include<ctime>

#include<cstdlib>
#include<cstdio>

using namespace std;

#include "domain name system api.h"

using namespace DNS_API;

extern bool Insufficient(), Invalid(), Help(), terminate();

bool Invalid(std::string INVALID) {
	std::cout << "[DNS Spoofer]: invalid argument(s) received for " << INVALID << std::endl;
   exit(0);
}

int main(int length, char ** argument) {
   
   if(length <= 1) return Insufficient();
   else dns_spoofer.argument = argument[1];
   
   if(dns_spoofer.argument == "/?") 
	   return Help();
     else
   if((dns_spoofer.argument == "--domain-name-service") || (dns_spoofer.argument == "-D")) {
       if(length <= 2) return Insufficient();
	   else dns_spoofer.argument = argument[2];
	   
	   if((dns_spoofer.argument == "--flush") || (dns_spoofer.argument == "-F")) {
		  std::cout << "[DNS Spoofer]: Flushing Windows DNS Cache ....." << std::endl;
		  
		   if(system("IPCONFIG /flushdns > nul") == true)
			   std::cout << "[DNS Spoofer]: Failure Flushing Windows DNS Cache!" << std::endl;
		   else 
			   std::cout << "[DNS Spoofer]: Successfully Flushed Windows DNS Cache!" << std::endl;
		 
		 exit(0);
	   }

	   if((dns_spoofer.argument == "--change") || (dns_spoofer.argument == "-C")) {
		   if((length <= 4) || (length >= 6)) {
			  std::cout << "[DNS Spoofer]: Invalid or Insufficient IP Addresses Specified." << std::endl;
			 exit(0);
		   } else dns_spoofer.argument = argument[3];
		   
		 std::cout << "[DNS Spoofer]: Changing your IPv4 DNS to [" << argument[3] << "] -> [" << argument[4] << "] ...." << std::endl;
		  
		  dns_spoofer.argument = argument[3];
		  dns_spoofer.command = "wmic nicconfig where DHCPEnabled=TRUE call SetDNSServerSearchOrder (\"" + dns_spoofer.argument + "\", \""; 
		  
		  dns_spoofer.argument = argument[4];
		  dns_spoofer.command = dns_spoofer.command + dns_spoofer.argument + "\")";
		   
		   if(system(dns_spoofer.command.c_str()) == true) {
			  std::cout << "[DNS Spoofer]: Failure Changing the IPv4 DNS names of all your DHCP Enabled Network Adapters!" << std::endl;
			 exit(0);
		   } else {
			  std::cout << "[DNS Spoofer]: Successfully Changed your Old IPv4 DNS to [" << argument[3] << "] -> [" << argument[4] << "] !" << std::endl;
		     exit(0);
		   }
		   
	   } else return Invalid("--domain-name-service or and -D !"); 
   } else return Invalid("the runtime!");
   
}

std::string HelpMessage[] = {
	("Usage : .\\\"Dashies DNS Spoofer.exe\" --domain-name-service [OPTIONS] \n\n"
	 "--domain-name-service or -D \n"
	 "% allows you to manage your DNS. \n"
	 "--domain-name-service --change [IPv4] [IPv4] or -D -C [IPv4] [IPv4] \n"
	 "% allows you to change your DNS. \n"
	 "--domain-name-service --flush or -D -F \n"
	 "% allows you to flush your DNS Cache. \n")
};

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
