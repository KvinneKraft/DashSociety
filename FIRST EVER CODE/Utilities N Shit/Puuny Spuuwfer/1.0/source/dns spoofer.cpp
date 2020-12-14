
////////////////////////////////////////////////////////// 
////////////////////////////////////////////////////////// 
//                                                      // ##
//  < (c) All Rights Reserved, Dashies Software Inc  >  // ## --
//                                                      // ## -- + 
//                                                      // ## -- + =
//                                                      // ## -- + = >
// a remake of the DNS Spoofer 4.0, this pony based IP  // ## -- + = > ~
// Spoofer has been coded to hide that IP of your DNS.  // ## -- + = > ~ )
//                                                      // ## -- + = > ~ )
//  coming with 3+ different IP Change Methods such as  // ## -- + = > ~
//             WINS, DHCP and many others.              // ## -- + = >
//                                                      // ## -- + = 
//  these awesome features will be applied automaticly  // ## -- +
//  when you are changing your DNS using IPv4 or IPv6~  // ## --
//                                                      // ##
////////////////////////////////////////////////////////// 
////////////////////////////////////////////////////////// 

/* 



################            ###############            ##############     ##             ##
##################        ###################         ###############     ##             ##
##                ##    ##                   ##     ##                    ##             ##
##                ##    ##                   ##     ##                    ##             ##     
##                ##    ##                   ##     ##                    ##             ##
##                ##    ##                   ##     ##                    ##             ##
##                ##    #######################       ###########         #################
##                ##    #######################        ###########        #################
##                ##    ##                   ##                   ##      ##             ##
##                ##    ##                   ##                   ##      ##             ##
##                ##    ##                   ##                   ##      ##             ##
##                ##    ##                   ##                   ##      ##             ##
##################      ##                   ##     ##############        ##             ##
################        ##                   ##     #############         ##             ##



*/


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

using namespace std;

#include "domain name system api.h"

/////// domain name system api.h //////

extern bool usage(long int mode, std::string forwho) {
	
	if(mode == 1)
	    std::cout << "[Puny]: unrecognized argument(s) received for " << forwho << "~" << std::endl;
    else 
	if(mode == 2)
		std::cout << std::endl << "[Puny]: insufficient argument(s) received for " << forwho << "~" << std::endl;
	else
	if(mode == 3)
		std::cout << "[Puny]: a fatal error occurred because " << forwho << "~" << std::endl;
	
   std::cout << "[Puny]: pass \"--help\" or \"-H\" for help~" << std::endl << std::endl;
  exit(0);
}   

static void maybe_to_default_ipv() {
    if(DNS_API::dns_spoofer.options.IPv == "") {
        DNS_API::dns_spoofer.options.IPv = "4";
       
       std::cout << "[Puny]: no Internet Protocol Version has been set, using default one, (4)..." << std::endl;
       std::cout << "[Puny]: Internet Protocol Version has successfully been set to 4 :O" << std::endl;
    }    
}

/////// END //////

using namespace DNS_API;

int main(int length, char ** argument) {
	if(length <= 1) usage(2, "main(int length, char ** argument) or and \"--change\" or \"-C\"");
	else std::cout << std::endl; 
	  dns_spoofer.options.IPv = "";
	
     for(int indexThingy = 1; indexThingy <= length-1; indexThingy = indexThingy + 1) {
		dns_spoofer.argument = argument[indexThingy];
		
	     if(dns_spoofer.access == "denied") 
		 { 
			 // version determination
			 if((dns_spoofer.argument == "--ipv4") || (dns_spoofer.argument == "-4")) {
				if((dns_spoofer.options.IPv == "4") || (dns_spoofer.options.IPv == "6")) goto Continue;
				 dns_spoofer.options.IPv = "4";
				   std::cout << "[Puny]: Internet Protocol Version has successfully been set to 4 c:" << std::endl;
			 } else
			 if((dns_spoofer.argument == "--ipv6") || (dns_spoofer.argument == "-6")) {
				if((dns_spoofer.options.IPv == "4") || (dns_spoofer.options.IPv == "6")) goto Continue;
				 dns_spoofer.options.IPv = "6";
				   std::cout << "[Puny]: Internet Protocol Version has successfully been set to 6 :D" << std::endl;
			 } else Continue:
            
			 // ip determination
			 if((dns_spoofer.argument == "--internet-address") || (dns_spoofer.argument == "-I")) {
                   if(indexThingy >= length-1)
                        usage(2, "\"--internet-address\" or \"-I\"");
                 
				  dns_spoofer.options.IP_Puller = "now";
                  dns_spoofer.options.IP_Puller_At = indexThingy;
                 
                 maybe_to_default_ipv();

                   if(dns_spoofer.options.IPv == "4") {
                        if(dns_spoofer.options.IP_Puller_At+2 > length-1)
                             usage(2, "\"--internet-address [IP 1] [IP 2]\" or \"-I [IP 1] [IP 2]\"");
                       
                       dns_spoofer.options.ip1 = argument[dns_spoofer.options.IP_Puller_At+1];
                       dns_spoofer.options.ip2 = argument[dns_spoofer.options.IP_Puller_At+2];
                       
                       indexThingy += 1;
                   } else 
                   if(dns_spoofer.options.IPv == "6") {
                        if(dns_spoofer.options.IP_Puller_At+1 > length-1)
                             usage(2, "\"--internet-address [IP 1]\" or \"-I [IP 1]\"");
                        
                       dns_spoofer.options.ip1 = argument[dns_spoofer.options.IP_Puller_At+1];
                       
                       indexThingy = indexThingy + 1;
                   }
			 }
             
			 // ip changing
			 if(dns_spoofer.options.IP_Puller == "now") {
                maybe_to_default_ipv();
                 
				 if(dns_spoofer.options.IPv == "6") {
					  if(GetAdaptersInfo(dns_spoofer.pAdapterInfo, &dns_spoofer.buflen) == ERROR_BUFFER_OVERFLOW) {
                         free(dns_spoofer.pAdapterInfo);
                          dns_spoofer.pAdapterInfo = (IP_ADAPTER_INFO *)malloc(dns_spoofer.buflen);
                      }
                    
                    std::cout << "[Puny]: changing your IPv6 DNS Server(s) to {" << dns_spoofer.options.ip1 << "} ..." << std::endl;
                    
                      if(GetAdaptersInfo(dns_spoofer.pAdapterInfo, &dns_spoofer.buflen) == NO_ERROR) {
                         for(IP_ADAPTER_INFO *pAdapter = dns_spoofer.pAdapterInfo; pAdapter; pAdapter = pAdapter -> Next) {
                             snprintf(dns_spoofer.conv, sizeof(dns_spoofer.conv), pAdapter->Description);
                             
                              dns_spoofer.InterfaceName = (dns_spoofer.conv);
                              dns_spoofer.command = "netsh interface ipv6 add address \"" + dns_spoofer.InterfaceName + "\" " + dns_spoofer.argument + " > nul";
                             
                               if(system(dns_spoofer.command.c_str()) == true) {std::cout << "[Puny]: failure changing your IPv6 DNS Server for " << dns_spoofer.InterfaceName <<  "!" << std::endl; std::cout << "[Puny]: please make sure that you are running this with administrative privileges and also make sure that you have entered a valid IPv6 Address. the next Update will make sure that you do not have to care about that~" << std::endl;}
                         } std::cout << "[Puny]: successfully changed your IPv6 DNS Server(s) to {" << dns_spoofer.options.ip1 << "} !" << std::endl;
                      } else
                          if(dns_spoofer.pAdapterInfo) free(dns_spoofer.pAdapterInfo);
                      
                       std::cout << "[Puny]: you still have to register these new DNS Server(s), would you like us to do so [Y/n]?";
                           switch(getch()) {
                             case 'y' : {
                             case 'Y' : 
                                std::cout << "\n [Puny]: registering your new DNS Server(s)..." << std::endl;
                                
                                 if(system("IPCONFIG /registerdns > nul") == true) {
                                     std::cout << "[Puny]: failure registering your DNS Server(s)" << std::endl;
                                    goto end;
                                 } else {
                                    if(system("IPCONFIG /flushdns > nul") == true) {
                                        std::cout << "[Puny]: failure flushing your DNS Cache!" << std::endl;
                                       goto end;
                                    } else {std::cout << "[Puny]: successfully registered your new DNS Server(s)!" << std::endl;}
                                 }
                                 
                               break;
                             }
                           }                     
                     exit(0);
				 } else 
				 if(dns_spoofer.options.IPv == "4") {
                     std::cout << "[Puny]: changing your IPv4 IP Enabled DNS Servers to {" << dns_spoofer.options.ip1 << "}->{" << dns_spoofer.options.ip2 << "} ..." << std::endl;
                      Sleep(3000);
                      
                    dns_spoofer.command = "wmic nicconfig where (IPEnabled=TRUE) call SetDNSServerSearchOrder (\"" + dns_spoofer.options.ip1 + "\", \"" + dns_spoofer.options.ip2  + "\")";
                    
                      if(system(dns_spoofer.command.c_str()) == true) {
                           std::cout << "[Puny]: failure changing your IP Enabled DNS Server(s)!" << std::endl;
                         goto end;
                      } else
                          dns_spoofer.command = "wmic nicconfig where (IPEnabled=TRUE) call SetWINSServer (\"" + dns_spoofer.options.ip1 + "\", \"" + dns_spoofer.options.ip2 + "\")";
                      
                      if(system(dns_spoofer.command.c_str()) == true) {
                           std::cout << "[Puny]: failure changing your IP Enabled DNS Server(s)!" << std::endl;
                         goto end;
                      } else
                          std::cout << "[Puny]: successfully changed your IPv4 IP Enabled DNS Servers to {" << dns_spoofer.options.ip1 << "}->{" << dns_spoofer.options.ip2 << "} !" << std::endl;
                      
                     std::cout << "[Puny]: changing your IPv4 DHCP Enabled DNS Servers to {" << dns_spoofer.options.ip1 << "}->{" << dns_spoofer.options.ip2 << "} ..." << std::endl;
                      Sleep(3000);
                      
                    dns_spoofer.command = "wmic nicconfig where (DHCPEnabled=TRUE) call SetDNSServerSearchOrder (\"" + dns_spoofer.options.ip1 + "\", \"" + dns_spoofer.options.ip2 + "\")";
                      
                      if(system(dns_spoofer.command.c_str()) == true) {
                           std::cout << "[Puny]: failure changing your DHCP Enabled DNS Server(s)!" << std::endl;
                         goto end;
                      } else
                          dns_spoofer.command = "wmic nicconfig where (DHCPEnabled=TRUE) call SetWINSServer (\"" + dns_spoofer.options.ip1 + "\", \"" + dns_spoofer.options.ip2 + "\")";
                          
                      if(system(dns_spoofer.command.c_str()) == true) {
                           std::cout << "[Puny]: failure changing your DHCP Enabled DNS Server(s)!" << std::endl;
                         goto end;
                      } else
                          std::cout << "[Puny]: successfully changed your IPv4 DHCP Enabled DNS Server(s) to {" << dns_spoofer.options.ip1 << "}->{" << dns_spoofer.options.ip2 << "} !"<< std::endl;
                      
                     std::cout << "[Puny]: you still have to register these new DNS Server(s), would you like us to do so [Y/n]?";
                       switch(getch()) {
                         case 'y' : {
                         case 'Y' :
                            std::cout << "\n [Puny]: registering your new DNS Server(s)..." << std::endl;
                             
                             if(system("IPCONFIG /registerdns > nul") == true) {
                                  std::cout << "[Puny]: failure registering your DNS Server(s)" << std::endl;
                                goto end;
                             } else {
                               if(system("IPCONFIG /flushdns > nul") == true) {
                                    std::cout << "[Puny]: failure flushing your DNS Cache!" << std::endl;
                                  goto end;
                               } else 
                                   std::cout << "[Puny]: successfully registered your new DNS Server(s)!" << std::endl;
                             }
                             
                           break;
                         }
                       }
                      
                    end:
                   exit(0);
				 } else usage(3, "no or an invalid IP address has been specified");
			 }
		 }
		 
		 if(indexThingy == 1)
		     if((dns_spoofer.argument == "--change") || (dns_spoofer.argument == "-C")) dns_spoofer.access = "denied";         
         
		 
		 
		 if((dns_spoofer.argument == "--show-list") || (dns_spoofer.argument == "-S")) {
		 	 std::cout << FREE_OPEN_DNS_SERVICES[0];
		   exit(0);
		 } else
		 if((dns_spoofer.argument == "--help") || (dns_spoofer.argument == "-H")) {
		 	 std::cout << HELP_MESSAGE[0];
		   exit(0);
		 } else {
             if(indexThingy == length-1)
                 usage(1, "main(int length, char ** argument)");
		 }
	 } 
}

/* --- END --- */

