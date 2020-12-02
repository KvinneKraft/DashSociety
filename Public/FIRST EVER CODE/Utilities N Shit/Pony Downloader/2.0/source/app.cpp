
/* =====[ (c) All Rights Reserved, Dashies Software Inc ]===== */

#include<windows.h>
#include<wininet.h>
#include<lmcons.h>
#include<mmsystem.h>

#include<string.h>
#include<stdio.h>
#include<stdlib.h>

#include<tchar.h>
#include<wchar.h>
#include<conio.h>

#include<iostream>
#include<string>

#include<map>
#include<vector>
#include<algorithm>

using namespace std;

std::string EX_MESSAGE[] = {
	("% .\\\"Pony Downloader.exe\" [OPTIONS] \n\n"
	 
	 "----[ OPTIONS ]---- \n"
	 ") \"--download [URL] [OUTPUT]\" and \"-D [URL] [OUTPUT]\" \n"
	 ") will allow you to download files :o \n"
	 ") \"--create [DIRECTORY]\" and \"-C [DIRECTORY]\" \n"
	 ") will allow you to create a directory to maybe store your files. \n")
};

#include "api.h"

using namespace Downloader_API;

int main(long int length, char ** argument) {
     if(length <= 1) {
		 std::cout << "[Poney]: insufficient arguments received!" << std::endl;
	   exit(0); 
	 } else api.arg = argument[1];
	 
	 if((api.arg == "--help") || (api.arg == "-H")) {
	 	
	     std::cout << "[Poney]: loading help menu ...." << std::endl;
	     std::cout << EX_MESSAGE[0];
	     std::cout << "[Poney]: successfully loaded help menu!" << std::endl;
	     
	   exit(0);	
	 }

	 if((api.arg == "--download") || (api.arg == "-D")) {
	     if((length <= 3) || (length >= 5)) {
	     	 std::cout << "[Poney]: insuffcient or too many argument(s) received for parameter \"--download\" or \"-D\" !" << std::endl; 
	       exit(0);
		 } else {
		 	
		 	  if((sizeof(argument[2]) <= 0) || (sizeof(argument[3]) <= 0)) {
		 	      std::cout << "[Poney]: insufficient argument(s) received for \"--download\" or \"-D\" !" << std::endl;
				exit(0);
			  } else
				  std::cout << "[Poney]: connecting with " << argument[2] << "....." << std::endl;
		 	
		 	 api.url    = argument[2];
		 	 api.output = argument[3];
		 	  
		 	strcat(api.network_buffer, api.url.c_str());
		 	  
		 	  if(InternetCheckConnection(api.network_buffer, FLAG_ICC_FORCE_CONNECTION, 0) != true) {
		 	  	  std::cout << "[Poney]: unable to connect with " << argument[2] << "!" << std::endl;
				exit(0);
			  } else
				  std::cout << "[Poney]: successfully connected with " << argument[2] << "!" << std::endl;
			  
			 std::cout << "[Poney]: downloading " << argument[2] << "....." << std::endl;
		 	  
			 api.download_filter = URLDownloadToFile(0, api.url.c_str(), api.output.c_str(), 0, 0);
			 api.buffer          = "OUTPUT_INVALID_DESTINATION";
			 
               switch(api.download_filter) {
				   case S_OK : { api.token = true; break; }
				   case E_OUTOFMEMORY : { api.token = false; api.buffer = "E_OUTOFMEMORY"; }
				   case INET_E_DOWNLOAD_FAILURE : { api.token = false; api.buffer = "INET_E_DOWNLOAD_FAILURE"; }
				   
				   default : {
					   if(api.token != true) {
						   std::cout << "[Poney]: download failed, error code [" << api.buffer << "] !" << std::endl;
						 exit(0);
					   }
				   }
			   }
			  
			 std::cout << "[Poney]: successfully downloaded " << argument[2] << " !" << std::endl; 
		   exit(0);
		 }
	 }
	 
	 if((api.arg == "--create") || (api.arg == "-C")) {
	 	 if((length <= 2) || (length >= 4)) {
			 std::cout << "[Poney]: insufficient argument(s) received for \"--create\" or \"-C\" !" << std::endl;
		   exit(0);
		 } else {
			 std::cout << "[Poney]: creating " << argument[2] << "....." << std::endl;
			 
			 api.arg    = argument[2];
			 api.token  = CreateDirectory(api.arg.c_str(), NULL);
			 api.buffer = "Unknown";
			 
			  if(GetLastError() == ERROR_ALREADY_EXISTS) { api.token = false; api.buffer = "ERROR_ALREADY_EXISTS"; }
		      if(GetLastError() == ERROR_PATH_NOT_FOUND) { api.token = false; api.buffer = "ERROR_PATH_NOT_FOUND"; }
				  
			  if(api.token == false) 
			      std::cout << "[Poney]: failure creating \"" << api.arg << "\", error code[" << api.buffer << "] !" << std::endl;
			  else
				  std::cout << "[Poney]: successfully created \"" << api.arg << "\" !" << std::endl;
		   
		   exit(0);
		 }
	 } else {
	 	 std::cout << "[Poney]: invalid argument(s) received for usage!" << std::endl;
	 	 std::cout << "[Poney]: usage : \".\\\"Pony Downloader.exe\" [OPTIONS]\" \"--help\" or and \"-H\" for help!" << std::endl;
	   exit(0);
	 }
}

/* =====[ END ]===== */
