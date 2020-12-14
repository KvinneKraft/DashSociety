void GetMAC(unsigned char MACDATA[]) {
	char addon[18];
	
	 snprintf(addon,
	          sizeof(addon),
			  TEXT("%02X-%02X-%02X-%02X-%02X-%02X"),
			  juicy_variable.MACData[0],
			  juicy_variable.MACData[1],
			  juicy_variable.MACData[2],
			  juicy_variable.MACData[3],
			  juicy_variable.MACData[4],
			  juicy_variable.MACData[5]);
			  
	 juicy_variable.macid = (addon);
}

bool LocalInformation() {
	juicy_variable.dashWord = UNLEN+1;
	juicy_variable.version  = MAKEWORD(2, 2);
	  
	  if(UuidCreateSequential(&juicy_variable.uuid) != RPC_S_OK) {
		  juicy_variable.errMsg = "Our mac ID Translator failed! \n\tRPC_S_OK was not received but expected."; 
		 return false;
	  } else for(int index = 2; index < 8; index = index + 1) juicy_variable.MACData[index - 2] = juicy_variable.uuid.Data4[index];
	  
	 GetMAC(juicy_variable.MACData);
	  
	  if((juicy_variable.macid.size() < 17) || (juicy_variable.macid.size() > 17)) {
		  juicy_variable.errMsg = "Invalid mac ID Received! \n\tExpected size to be 17 but got " + std::to_string(juicy_variable.macid.size()) + " instead.";
		 return false;
	  }
	  
	  if(GetUserName(juicy_variable.userBuffer, &juicy_variable.dashWord) == 0) {
		  juicy_variable.errMsg = "Username Resolver function failed! \n\tA value greater than 0 was not received but expected.";
		 return false;  
	  } else juicy_variable.username = juicy_variable.userBuffer;
	  
	  if(InternetCheckConnection(TEXT("http://www.dashware-software.co.uk/"), FLAG_ICC_FORCE_CONNECTION, 0) == false) {
		  juicy_variable.errMsg = "Unable to Connect to our Web Server(s)! \n\tIt seems like our website is either being blocked or \n\tyou are not using an accessible internet connection.";
		 return false;
	  }
	  
	  if(WSAStartup(juicy_variable.version, &juicy_variable.wsa) != 0) {
		  juicy_variable.errMsg = "WSA Startup failed! \n\tWe are not sure what caused this issue. \n\tIt may have something to do with your windows version \n\tor the privileges you used to execute this \n\tapplication. \n\n\tThis ERROR comes from the WINSOCK2 Header File!";
		 return false;
	  }
	  
	juicy_variable.retrieve = InternetOpen(TEXT("https://ipinfo.io/ip"), 
	                                       INTERNET_OPEN_TYPE_PRECONFIG, 
										   NULL, NULL, 0);
	
	  if(juicy_variable.retrieve == NULL) {
		  juicy_variable.errMsg = "Unable to Retrieve Public IP API response! \n\tMost likely caused by your firewall or anti-virus.";
		 return false;
	  }
	  
	juicy_variable.connect = InternetOpenUrl(juicy_variable.retrieve, 
	                                         TEXT("https://ipinfo.io/ip"), 
											 NULL, 0, 
											 INTERNET_FLAG_RELOAD, 0);
											 
	  if(juicy_variable.connect == NULL) {
		  juicy_variable.errMsg = "Unable to Connect with Public IP API! \n\tMake sure your firewall allows to connect with \n\thttps://ipinfo.io/ip. \n\tIf it does not add this application to your exclusions.";
		 return false;
	  }
	  
	juicy_variable.reader = InternetReadFile(juicy_variable.connect,
	                                         juicy_variable.netbuff,
											 sizeof(juicy_variable.netbuff)/sizeof(juicy_variable.buffer[0]),
											 &juicy_variable.read);
											 
	  if(juicy_variable.reader != true) {
		  juicy_variable.errMsg = "Unable to Read Retrieved API Data! \n\tThis may be caused by your anti-virus or D.E.P. \n\tTry to start this application with administrative rights. \n\tKnow that this is not required normally.";
		 return false;
	  }

	juicy_variable.retrieve = InternetOpen(TEXT("ip-api.com/line"), 
	                                       INTERNET_OPEN_TYPE_PRECONFIG, 
										   NULL, NULL, 0);
	
	  if(juicy_variable.retrieve == NULL) {
		  juicy_variable.errMsg = "Unable to Retrieve GEO IP API response! \n\tMost likely caused by your firewall or anti-virus.";
		 return false;
	  }
	  
	juicy_variable.connect = InternetOpenUrl(juicy_variable.retrieve, 
	                                         TEXT("http://ip-api.com/line"), 
											 NULL, 0, 
											 INTERNET_FLAG_RELOAD, 0);
											 
	  if(juicy_variable.connect == NULL) {
		  juicy_variable.errMsg = "Unable to Connect with GEO IP API! \n\tMake sure your firewall allows to connect with \n\thttps://ip-api.com/json. \n\tIf it does not add this application to your exclusions.";
		 return false;
	  }
	  
	juicy_variable.reader = InternetReadFile(juicy_variable.connect,
	                                         juicy_variable.geoip,
											 sizeof(juicy_variable.netbuff)/sizeof(juicy_variable.buffer[0]),
											 &juicy_variable.read);
											 
	  if(juicy_variable.reader != true) {
		  juicy_variable.errMsg = "Unable to Read Retrieved API Data! \n\tThis may be caused by your anti-virus or D.E.P. \n\tTry to start this application with administrative rights. \n\tKnow that this is not required normally.";
		 return false;
	  }
	  
	juicy_variable.handler = InternetCloseHandle(juicy_variable.retrieve);
	
	  if(juicy_variable.handler != true) {
		  juicy_variable.errMsg = "Unable to Close Internet Handle! \n\tThis may be caused by interrupting this application while \n\trunning. \n\tthis will not cause any issues, but we must close \n\tthis application to prevent any other error from occurring.";
		 return false;
	  } else {
	  	 juicy_variable.public_ip = juicy_variable.netbuff;
	  	 juicy_variable.geo_ip    = juicy_variable.geoip;
	  	 
	  	 juicy_variable.geo_ip.erase(0,7);
      }
      
	  if(gethostname(juicy_variable.hostbuff, sizeof(juicy_variable.hostbuff)) == SOCKET_ERROR) {
		  juicy_variable.errMsg = "Hostname Resolver function failed! \n\tSOCKET_ERROR was received but not expected.";
		 return false;
      } else juicy_variable.hostname = juicy_variable.hostbuff;
	  
	 struct hostent *ent = gethostbyname(juicy_variable.hostbuff);
	 struct in_addr ip_addr = *(struct in_addr *)(ent->h_addr);
	 
	juicy_variable.private_ip = inet_ntoa(ip_addr);
	
	  if(WSACleanup() != 0) {
		  juicy_variable.errMsg = "WSA Cleanup failed! \n\tThis is most likely caused by interrupting this \n\tapplication while running.";
		 return false;  
	  }
	
  return true;
}
