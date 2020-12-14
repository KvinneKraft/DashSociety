/* (c) All Rights Reserved, Dashies Software Inc. */

#include<windows.h>
#include<wininet.h>
#include<mmsystem.h>

#include<conio.h>
#include<lmcons.h>
#include<stdio.h>
#include<stdlib.h>

#include<wchar.h>
#include<tchar.h>
#include<string.h>

#include<math.h>

#include<iostream>

#include<ctime>
#include<vector>
#include<algorithm>

using namespace std;

struct API_STORAGE {
	long int dir_token;
	bool pass;
	
    std::string arg, command,
	            rule_port,
				rule_name,
				rule_direction;
}api;

std::string help_map[] = {
	("$ usage : .\\\"WBP.exe\" [PARAMETERS] \n\n"
	 "% add a firewall rule : \n"
	 " -> --firewall --add-rule --name \"My Name\" --port 80 (optional setting)[--in | --out | --both] \n"
	 " -> -F -A -N \"My Name\" -P 80 (optional setting)[-I | -O | -B] \n"
	 "% reset your firewall to its defaults : \n"
	 " -> --firewall --reset \n"
	 " -> -F -R \n"
	 "% our nice copyrights : \n"
	 " -> --copyright \n"
	 " -> -C \n")
};

 int main(int length, char ** argument) {
    if(length <= 1) {
		std::cout << "[Console->Output]: insufficient input received!" << std::endl;
		std::cout << "[Console->Output]: usage \".\\\"Port Blocker.exe\" [OPTIONS]\"" << std::endl;
		std::cout << "[Console->Output]: help \".\\\"Port Blocker.exe\" [/? | --help | -h]\"" << std::endl;
      exit(0);
	} else api.arg = argument[1];
	
	if((api.arg == "--help") || ("-H") || ("/?")) {
	    std::cout << "[Console->Output]: loading help map ....." << std::endl;
	    std::cout << help_map[0];
	    std::cout << "[Console->Output]: successfully loaded help map!]" << std::endl;
	  exit(0);
	}
	
	if((api.arg == "--copyright") || (api.arg == "-C")) {
		std::cout << "*****************************************************" << std::endl;
	    std::cout << "*** (c) All Rights Reserved, Dashies Software Inc ***" << std::endl;
	    std::cout << "*****************************************************" << std::endl;
	  exit(0);
	}
	
	if((api.arg == "--firewall") || (api.arg == "-F")) {
		if(length < 3) {
			std::cout << "[Console->Output]: insufficient input received for \"--firewall\" or \"-F\" !" << std::endl;
		  exit(0);	
		} else api.arg = argument[2];
		
		if((api.arg == "--add-rule") || (api.arg == "-A")) {
		    if(length < 4) {
				std::cout << "[Console->Output]: insufficient input received for \"--add-rule\" or \"-A\" !" << std::endl;
			  exit(0);
			} else api.arg = argument[3];
			
			if((api.arg == "--name") || (api.arg == "-N")) {
				if(length < 5) {
					std::cout << "[Console->Output]: parameter(s) missing for \"--name\" or \"-N\" !" << std::endl;
				  exit(0);
				} else api.rule_name = argument[4];
				
				if(length > 6)
				    api.arg = argument[5];

				if((api.arg == "--port") || (api.arg == "-P")) {
					if(length < 7) {
						std::cout << "[Console->Output]: parameter(s) missing for \"--port\" or \"-P\" !" << std::endl;
					  exit(0);	
					}
					
					std::cout << "[Console->Output]: running firewall commands ...." << std::endl;
					
                    if(length >= 8) {
                       api.arg = argument[7];
                    	
					  if((api.arg == "--in") || (api.arg == "-I"))
					     api.dir_token = 1;
					   else
				      if((api.arg == "--out") || (api.arg == "-O"))
					     api.dir_token = 2;
					   else
					  if((api.arg == "--both") || (api.arg == "-B"))
					     api.dir_token = 3;
					  else {
					  	  std::cout << "[Console->Output]: invalid direction option has been received!" << std::endl;
					     exit(0);
					  }
					} else {
					    std::cout << "[Console->Output]: no in/out/both has been specified. defaults(in) will be used!" << std::endl;
					   api.dir_token = 1;
					}
				   
				  api.rule_port = argument[6];
				     
					 if(api.rule_port.size() > 5) {
					     std::cout << "[Console->Output]: you have given us a too high port!" << std::endl;
						 std::cout << "[Console->Output]: please choose a port between 1 and 65535. (f.e: 8080)" << std::endl;
					   exit(0);
					 }
					 
					 for(long int dbIndex = 0; dbIndex <= api.rule_port.size()-1; dbIndex = dbIndex + 1) {
					 	 if((api.rule_port[dbIndex] == '1') ||
					 	   (api.rule_port[dbIndex] == '2') ||
					 	   (api.rule_port[dbIndex] == '3') ||
					 	   (api.rule_port[dbIndex] == '4') ||
					 	   (api.rule_port[dbIndex] == '5') ||
					 	   (api.rule_port[dbIndex] == '6') ||
					 	   (api.rule_port[dbIndex] == '7') ||
					 	   (api.rule_port[dbIndex] == '8') ||
					 	   (api.rule_port[dbIndex] == '9') ||
					 	   (api.rule_port[dbIndex] == '0')) {
						 } else {
						     std::cout << "[Console->Output]: invalid port selection has been read!" << std::endl;
						     std::cout << "[Console->Output]: please use numeric characters only!" << std::endl;
                           exit(0);							 	
						 }
					 }
				   
				      switch(api.dir_token) {
						 case 1:{
							 api.rule_direction = "in";
							 api.command = "netsh advfirewall firewall add rule name=\"" + api.rule_name + "\" protocol=tcp dir=" + api.rule_direction + " localport=" + api.rule_port + " remoteport=" + api.rule_port + " action=block > nul";
							
							  if(system(api.command.c_str()) == true)
								  std::cout << "[Console->Output]: failed to block the specified port(" << api.rule_port << ") for TCP!" << std::endl;
							  else
								  std::cout << "[Console->Output]: successfully blocked the specified port(" << api.rule_port << ") for TCP!" << std::endl;
							  
							 api.command = "netsh advfirewall firewall add rule name=\"" + api.rule_name + "\" protocol=udp dir=" + api.rule_direction + " localport=" + api.rule_port + " remoteport=" + api.rule_port + " action=block > nul";
							  
							  if(system(api.command.c_str()) == true)
								  std::cout << "[Console->Output]: failed to block the specified port(" << api.rule_port << ") for UDP!" << std::endl;
							  else
								  std::cout << "[Console->Output]: successfully blocked the specified port(" << api.rule_port << ") for UDP!" << std::endl;
							  
							 api.dir_token = 0;
							break;
						 }
						 
						 case 2:{
							 api.rule_direction = "out";
							 api.command = "netsh advfirewall firewall add rule name=\"" + api.rule_name + "\" protocol=tcp dir=" + api.rule_direction + " localport=" + api.rule_port + " remoteport=" + api.rule_port + " action=block > nul";
							
							  if(system(api.command.c_str()) == true)
								  std::cout << "[Console->Output]: failed to block the specified port(" << api.rule_port << ") for TCP!" << std::endl;
							  else
								  std::cout << "[Console->Output]: successfully blocked the specified port(" << api.rule_port << ") for TCP!" << std::endl;
							  
							 api.command = "netsh advfirewall firewall add rule name=\"" + api.rule_name + "\" protocol=udp dir=" + api.rule_direction + " localport=" + api.rule_port + " remoteport=" + api.rule_port + " action=block > nul";
							  
							  if(system(api.command.c_str()) == true)
								  std::cout << "[Console->Output]: failed to block the specified port(" << api.rule_port << ") for UDP!" << std::endl;
							  else
								  std::cout << "[Console->Output]: successfully blocked the specified port(" << api.rule_port << ") for UDP!" << std::endl;							 
							 
							break;
						 }
						 
						 case 3:{
							 api.rule_direction = "in";
							 api.command = "netsh advfirewall firewall add rule name=\"" + api.rule_name + "\" protocol=tcp dir=" + api.rule_direction + " localport=" + api.rule_port + " remoteport=" + api.rule_port + " action=block > nul";
							
							  if(system(api.command.c_str()) == true)
								  std::cout << "[Console->Output]: #1 failed to block the specified port(" << api.rule_port << ") for TCP!" << std::endl;
							  else
								  std::cout << "[Console->Output]: #1 successfully blocked the specified port(" << api.rule_port << ") for TCP!" << std::endl;
							  
							 api.command = "netsh advfirewall firewall add rule name=\"" + api.rule_name + "\" protocol=udp dir=" + api.rule_direction + " localport=" + api.rule_port + " remoteport=" + api.rule_port + " action=block > nul";
							  
							  if(system(api.command.c_str()) == true)
								  std::cout << "[Console->Output]: #1 failed to block the specified port(" << api.rule_port << ") for UDP!" << std::endl;
							  else
								  std::cout << "[Console->Output]: #1 successfully blocked the specified port(" << api.rule_port << ") for UDP!" << std::endl;							 
							 
							 api.rule_direction = "out";
							 api.command = "netsh advfirewall firewall add rule name=\"" + api.rule_name + "\" protocol=tcp dir=" + api.rule_direction + " localport=" + api.rule_port + " remoteport=" + api.rule_port + " action=block > nul";
							
							  if(system(api.command.c_str()) == true)
								  std::cout << "[Console->Output]: #2 failed to block the specified port(" << api.rule_port << ") for TCP!" << std::endl;
							  else
								  std::cout << "[Console->Output]: #2 successfully blocked the specified port(" << api.rule_port << ") for TCP!" << std::endl;
							  
							 api.command = "netsh advfirewall firewall add rule name=\"" + api.rule_name + "\" protocol=udp dir=" + api.rule_direction + " localport=" + api.rule_port + " remoteport=" + api.rule_port + " action=block > nul";
							  
							  if(system(api.command.c_str()) == true)
								  std::cout << "[Console->Output]: #2 failed to block the specified port(" << api.rule_port << ") for UDP!" << std::endl;
							  else 
								  std::cout << "[Console->Output]: #2 successfully blocked the specified port(" << api.rule_port << ") for UDP!" << std::endl;							 
							break;
						 }
					  }
				   
				     std::cout << "[Console->Output]: successfully ran firewall commands!" << std::endl;
				   
				   exit(0);
				} else {
					std::cout << "[Console-Output]: invalid or no port has been set!" << std::endl;
				  exit(0);
				}
			} else {
				std::cout << "[Console->Output]: invalid or no rule name has been set!" << std::endl;
			  exit(0);
			}
		} 
		
		if((api.arg == "--remove-rule") || (api.arg == "-R")) {
			std::cout << "[Console->Output]: this will be added in the next update!" << std::endl;
		  exit(0);
		} 
		
		if((api.arg == "--reset") || (api.arg == "-R")) {
		    std::cout << "[Console->Output]: resetting firewall ....." << std::endl;
			  
			  if(system("netsh advfirewall reset > nul") == true)
				  std::cout << "[Console->Output]: failure resetting your firewall!" << std::endl;
			  else 
				  std::cout << "[Console->Output]: successfully reset your firewall!" << std::endl;
			
		  exit(0);
		} else {
			std::cout << "[Console->Output]: invalid arguments received for \"--firewall\" or \"-F\" !" << std::endl;
		  exit(0);	
		}
	} else {
		std::cout << "[Console->Output]: invalid input received!" << std::endl;
		std::cout << "[Console->Output]: usage \".\\\"Port Blocker.exe\" [OPTIONS]\"" << std::endl;
		std::cout << "[Console->Output]: help \".\\\"Port Blocker.exe\" [/? | --help | -h]\"" << std::endl;
	  exit(0);
	}
 }

/* ===== END ===== */
