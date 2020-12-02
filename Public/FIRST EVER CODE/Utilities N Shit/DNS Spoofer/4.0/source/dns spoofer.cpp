
/* --- (c) All Rights Reserved, Dashies Software Inc --- */


/* FIX DNS Server ISSUE UNABLE TO CONNECT! */


#include<winsock2.h>
#include<windows.h>
#include<lmcons.h>
#include<mmsystem.h>

#include<stdio.h>
#include<stdlib.h>
#include<string.h>

#include<conio.h>
#include<wininet.h>
#include<iphlpapi.h>
#include<rpc.h>
#include<rpcdce.h>
#include<assert.h>
#include<shellapi.h>
#include<objbase.h>

#include<iostream>

#include<ctime>
#include<map>
#include<algorithm>
#include<vector>

extern bool Insufficient(), Invalid(), Help(), terminate();

using namespace std;

#include "domain name system api.h"

using namespace DNS_API;

int main(int length, char ** argument) {
   
   if(length <= 1) return Insufficient();
   else dns_spoofer.argument = argument[1];
   
   if(dns_spoofer.argument == "/?") 
	   return Help();
     else
   if((dns_spoofer.argument == "--domain-name-service") || (dns_spoofer.argument == "-D")) {
       if(length <= 2) return Insufficient();
	   else dns_spoofer.argument = argument[2];
	   
	  /*%%%%%%%%%%%%%%%%% LIST SERVERS %%%%%%%%%%%%%%%%*/
	  
	   if((dns_spoofer.argument == "--free-dns-services") || (dns_spoofer.argument == "-O")) {
		  std::cout << "[DNS Spoofer]: loading DNS Services ....." << std::endl;
		  
		  std::cout << FREE_OPEN_DNS_SERVICES[0];
		  
		  std::cout << "[DNS Spoofer]: successfully loaded DNS Services!" << std::endl;
	     
		 exit(0);
	   }
	   
	  /*%%%%%%%%%%%%%%%%%%%%% END %%%%%%%%%%%%%%%%%%%%%*/
	  
	  /*%%%%%%%%%%%%%%%$% REGISTER DNS %%%%%%%$$%%%%%%%*/
	   
	   if((dns_spoofer.argument == "--register-dns") || (dns_spoofer.argument == "-R")) {
	   	  std::cout << "[DNS Spoofer]: registering DNS Server(s) ....." << std::endl;
	   	  
	   	   if(system("ipconfig /registerdns > nul") == true)
	   	       std::cout << "[DNS Spoofer]: failure while registering your DNS Server(s) !" << std::endl;
		   else
	   	       std::cout << "[DNS Spoofer]: successfully registered your DNS Server(s) !" << std::endl;
	   	       
	   	 exit(0);
	   }
	   
	  /*%%%%%%%%%%%%%%%%%%%%% END %%%%%%%%%%%%%%%%%%%%%*/
	  
	  /*%%%%%%%%%%%%%%%%%% FLUSH DNS %%%%%%%%%%%%%%%%%%*/
	  
	   if((dns_spoofer.argument == "--flush") || (dns_spoofer.argument == "-F")) {
		  std::cout << "[DNS Spoofer]: flushing Windows DNS Cache ....." << std::endl;
		  
		   if(system("IPCONFIG /flushdns > nul") == true)
			   std::cout << "[DNS Spoofer]: failure flushing Windows DNS Cache!" << std::endl;
		   else 
			   std::cout << "[DNS Spoofer]: successfully flushed Windows DNS Cache!" << std::endl;
		 
		 exit(0);
	   }
	   
      /*%%%%%%%%%%%%%%%%%%%%% END %%%%%%%%%%%%%%%%%%%%%*/
	  
	  /*%%%%%%%%%%%%%%%%%% CHANGE DNS %%%%%%%%%%%%%%%%%*/
	  
	   if((dns_spoofer.argument == "--change") || (dns_spoofer.argument == "-C")) {
		   if(length < 4) {
			    std::cout << "[DNS Spoofer]: insufficient parameter(s) received for \"--change\" or \"-C\" !" << std::endl;
			  exit(0);
		   } else dns_spoofer.argument = argument[3];
		   
		   if((dns_spoofer.argument == "--ipv4") || (dns_spoofer.argument == "-V4")) {
			  dns_spoofer.mode = 4;
			   std::cout << "[DNS Spoofer]: mode has been set to IPv4!" << std::endl;
		   } else 
		   if((dns_spoofer.argument == "--ipv6") || (dns_spoofer.argument == "-V6")) {
			  dns_spoofer.mode = 6;
			   std::cout << "[DNS Spoofer]: mode has been set to IPv6" << std::endl;
			   std::cout << "[DNS Spoofer]: Still in beta :o" << std::endl;
		   } else {
			  dns_spoofer.mode = 4;
			   std::cout << "[DNS Spoofer]: no or a invalid mode has been set! using default(IPv4)." << std::endl;
		   }
		   
		    switch(dns_spoofer.mode) {
			   case 4 : {
				   
				     if((length <= 5) || (length >= 7)) {
						 std::cout << "[DNS Spoofer]: invalid or no IPv4 Address(es) specified!" << std::endl;
					   exit(0);
					 } else dns_spoofer.argument = argument[4];
					
					////////////////////////////////////////////////////////////////////////////////////////
					std::cout << "[DNS Spoofer]: changing your IPv4 IP Enabled (WINS) DNS Server(s) to [" << argument[4] << "] -> [" << argument[5] << "] ...." << std::endl;    
					  
					  dns_spoofer.command = "wmic nicconfig where (IPEnabled=TRUE) call SetWINSServer (\"" + dns_spoofer.argument;
					  
					  dns_spoofer.argument = argument[5];
					  dns_spoofer.command  = dns_spoofer.command + "\",\"" + dns_spoofer.argument + "\")";
					  
					 if(system(dns_spoofer.command.c_str()) == true) {
					     std::cout << "[DNS Spoofer]: failure changing your IPv4 DNS Server(s) forall your IP Enabled WINS Network Adapters!" << std::endl;
					   exit(0);
					 } else 
						 std::cout << "[DNS Spoofer]: successfully changed your old IPv4 IP Enabled WINS Adapters DNS Server(s) to [" << argument[4] << "] -> [" << argument[5] << "] !" << std::endl;
					////////////////////////////////////////////////////////////////////////////////////////
					
					////////////////////////////////////////////////////////////////////////////////////////
                    std::cout << "[DNS Spoofer]: changing your IPv4 DHCP (WINS) Enabled DNS Server(s) to [" << argument[4] << "] -> [" << argument[5] << "] ....";					  
					  
					  dns_spoofer.argument = argument[4];
					  dns_spoofer.command = "wmic nicconfig where (DHCPEnabled=TRUE) call SetWINSServer (\"" + dns_spoofer.argument;
					  
					  dns_spoofer.argument = argument[5];
					  dns_spoofer.command  = dns_spoofer.command + "\",\"" + dns_spoofer.argument + "\")";
                     
					 if(system(dns_spoofer.command.c_str()) == true) {
					     std::cout << "[DNS Spoofer]: failure changing the IPv4 DNS Server(s) for all your DHCP Enabled WINS Network Adapters!" << std::endl;
					   exit(0);
					 } else
						 std::cout << "[DNS Spoofer]: successfully changed your old IPv4 DHCP Enabled WINS Adapters DNS Server(s) to [" << argument[4] << "] -> [" << argument[5] << "] !" << std::endl;
					////////////////////////////////////////////////////////////////////////////////////////
					
					////////////////////////////////////////////////////////////////////////////////////////
					std::cout << "[DNS Spoofer]: changing your IPv4 DHCP Enabled DNS Server(s) to [" << argument[4] << "] -> [" << argument[5] << "] ...." << std::endl;
					
					  dns_spoofer.argument = argument[4];
				      dns_spoofer.command = "wmic nicconfig where (DHCPEnabled=TRUE) call SetDNSServerSearchOrder (\"" + dns_spoofer.argument + "\",\"";
					  
					  dns_spoofer.argument = argument[5];
					  dns_spoofer.command  = dns_spoofer.command + dns_spoofer.argument + "\")";
					
					 if(system(dns_spoofer.command.c_str()) == true) {
					     std::cout << "[DNS Spoofer]: failure changing the IPv4 DNS Server(s) for all your DHCP Enabled Network Adapters!" << std::endl;
					   exit(0);
					 } else 
						 std::cout << "[DNS Spoofer]: successfully changed your old IPv4 DHCP Enabled Adapters DNS Server(s) to [" << argument[4] << "] -> [" << argument[5] << "] !" << std::endl;
					////////////////////////////////////////////////////////////////////////////////////////
					
					////////////////////////////////////////////////////////////////////////////////////////
					std::cout << "[DNS Spoofer]: changing your IPv4 IP Enabled DNS Server(s) to [" << argument[4] << "] -> [" << argument[5] << "] ...." << std::endl;
					  
					  dns_spoofer.argument = argument[4];
				      dns_spoofer.command = "wmic nicconfig where (IPEnabled=TRUE) call SetDNSServerSearchOrder (\"" + dns_spoofer.argument + "\",\"";
					  
					  dns_spoofer.argument = argument[5];
					  dns_spoofer.command  = dns_spoofer.command + dns_spoofer.argument + "\")";
					 
					 if(system(dns_spoofer.command.c_str()) == true) {
					     std::cout << "[DNS Spoofer]: failure changing the IPv4 DNS Server(s) for all your IP Enabled Network Adapters!" << std::endl;
					   exit(0);
					 } else {
						 system("ipconfig /registerdns > nul");
						 system("ipconfig /flushdns > nul");
						 
						 std::cout << "[DNS Spoofer]: successfully changed your old IPv4 IP Enabled Adapters DNS Server(s) to [" << argument[4] << "] -> [" << argument[5] << "] !" << std::endl;
					   exit(0);
					 }
					////////////////////////////////////////////////////////////////////////////////////////
					 
				  break;
			   }
			   
			   case 6 : {
				     if((length < 5) || (length > 5)) {
						 std::cout << "[DNS Spoofer]: invalid or no IPv6 Address(es) specified!" << std::endl;
					   exit(0);
					 } else dns_spoofer.argument = argument[4];
					
					std::cout << "[DNS Spoofer]: changing your IPv6 DNS Server(s) to [" << argument[4] << "] ...." << std::endl;
					  
					 if(GetAdaptersInfo(dns_spoofer.pAdapterInfo, &dns_spoofer.buflen) == ERROR_BUFFER_OVERFLOW) {
						 free(dns_spoofer.pAdapterInfo);
						 dns_spoofer.pAdapterInfo = (IP_ADAPTER_INFO *)malloc(dns_spoofer.buflen);
					 } 
					  
					 if(GetAdaptersInfo(dns_spoofer.pAdapterInfo, &dns_spoofer.buflen) == NO_ERROR) {
						 for(IP_ADAPTER_INFO *pAdapter = dns_spoofer.pAdapterInfo; pAdapter; pAdapter = pAdapter->Next) {
							 snprintf(dns_spoofer.conv, sizeof(dns_spoofer.conv), pAdapter->Description);
							  
							  dns_spoofer.InterfaceName = (dns_spoofer.conv);
							  dns_spoofer.command = "netsh interface ipv6 add address \"" + dns_spoofer.InterfaceName + "\" " + dns_spoofer.argument + " > nul";
						    
							if(system(dns_spoofer.command.c_str()) == true)
								std::cout << "[DNS Spoofer]: failure changing your DNS Server to [" << argument[4] << "] on " << dns_spoofer.InterfaceName << " !" << std::endl;
						    else
								std::cout << "[DNS Spoofer]: successfully changed your DNS Server to [" << argument[4] << "] on " << dns_spoofer.InterfaceName << " !" << std::endl;
							
						 } system("ipconfig /register > nul");
						   system("ipconfig /flushdns > nul");
					 }
					 
					 if(dns_spoofer.pAdapterInfo) free(dns_spoofer.pAdapterInfo);
					 
				   exit(0);
				  break;
			   }
			   
			   default : {
				    std::cout << "[DNS Spoofer]: invalid mode id received, please try again!" << std::endl;
				  exit(0);
			   }
			}
	   } else return Invalid("--domain-name-service or and -D !");
	   
	  /*%%%%%%%%%%%%%%%%%%%%% END %%%%%%%%%%%%%%%%%%%%%*/	  
	   
   } else return Invalid("the runtime!");
   
   
}

/* --- END --- */
