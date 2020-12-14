/* (c) All Rights Reserved, Dashies Software Inc. */

/* --- Crypto Module 2.0 --- */

#include<windows.h>
#include<mmsystem.h>
#include<lmcons.h>
#include<wininet.h>

#include<stdlib.h>
#include<stdio.h>
#include<string.h>

#include<conio.h>
#include<tchar.h>
#include<wchar.h>

#include<math.h>
#include<time.h>

#include<iostream>
#include<string>

#include<map>
#include<vector>
#include<algorithm>

using namespace std;

/* --- Crypt_API.h --- */

namespace Crypt {
	struct Encryption_Table {
		std::string tag, arg, old_text, new_text;
	}cruption;
	
	std::string MSG_TABLE[][] = {
	    ("default usage : .\\\"Dash Cruption.exe\" [OPTIONS] [TEXT] \n" 
		 
		 "\n"
		 "--=== Encryption(-E|--encryption) Options ===-- \n\n"
		 " \n"
		 
		 "\n\n"
		 "--=== Miscellaneous Options ===-- \n\n"
		 " % load up our website ^^ \n"
		 " : \"-W\" or \"--website\" \n"
		 " % load up our copyrights :o \n"
		 " : \"-C\" or \"--copyright\" \n"
		 " % create a directory to maybe store your encrypted keys?\n"
		 " : \"-M\" or \"--make-directory\" \n"),
		 
		("/###############################################/ \n"
		 "/                                               / \n"
		 "/ (c) All Rights Reserved, Dashies Software Inc / \n"
		 "/                                               / \n"
		 "/###############################################/ \n")
	};
	
	std::string MyCryptoTable[] = {
		()
	}
}

/* --- END --- */

//#include"Crypt_API.h"

using namespace Crypt;

 int main(int length, char ** parameter) {
   cruption.tag = "[Client]:> ";
   
    if(length < 2) {
		std::cout << cruption.tag << "insufficient parameter(s) received for main();" << std::endl;
		std::cout << cruption.tag << "usage : .\\\"Dash Cruption.exe\" [OPTIONS] [TEXT] || Use \"-H\" or \"--help\" for help!" << std::endl;
	  exit(0);
	} else cruption.arg = parameter[1];
	
    /*####################################################################*/
	/*######################### Get Help Command #########################*/
	/*####################################################################*/
	
	if((cruption.arg == "-H") || (cruption.arg == "--help")) {
		std::cout << MSG_TABLE[0] << std::endl;
	  exit(0);
	}
	
	/*####################################################################*/
	/*################################ END ###############################*/
	/*####################################################################*/
	
	
	
    /*####################################################################*/
	/*######################## Cryption Parameters #######################*/
	/*####################################################################*/
    
	// parameter[address]\/
	// A[1] = -M / --make-directory 
	// A[2] = <DIRECTORY>
	
    if((cruption.arg == "-M") || (cruption.arg == "--make-directory")) {
		if(length < 3) {
			std::cout << cruption.tag << "no directory has been specified with \"-M\" or \"--make-directory\"." << std::endl;
		  exit(0);
		} else cruption.arg = parameter[2];
		
		if(CreateDirectory(cruption.arg.c_str(), NULL) != 0) {
		    std::cout << cruption.arg << "successfully created your directory (" << cruption.arg << ") c:" << std::endl;
		  exit(0);
		} else {
			std::cout << cruption.arg << "failure creating your directory (" << cruption.arg << ") :c" << std::endl;
		  exit(0);
		}
	}
	
	
	if((cruption.arg == "-T") || (cruption.arg == "--text")) {
		if(length < 3) {
			std::cout << cruption.tag << "no text has been specified with \"-T\" or \"--text\"." << std::endl;
		  exit(0);
		} else cruption.arg = parameter[2];
		
		 cruption.old_text = cruption.arg;
	}
	
    /*####################################################################*/
	/*############################### END ################################*/
	/*####################################################################*/	
	
	
	
    /*####################################################################*/
	/*########################## Other Commands ##########################*/
	/*####################################################################*/
	
	if((cruption.arg == "-C") || (cruption.arg == "--copyright")) {
	    std::cout << MSG_TABLE[1] << std::endl;
	  exit(0);
	}
	
	if((cruption.arg == "-W") || (cruption.arg == "--website")) {
	   strcat(netBuff, TEXT("http://www.dashware-software.co.uk/Permission_Denied.php"));
	     
		if(InternetCheckConnection(netBuff, FLAG_ICC_FORCE_CONNECTION, 0) != true) {
		    std::cout << cruption.tag << "unable to connect with our website." << std::endl;
		  exit(0);
		} else {
		   ShellExecute(NULL, "open", "http://www.dashware-software.co.uk", NULL, NULL, SW_SHOW);
		   
			std::cout << cruption.tag << "successfully executed the url in your default web browser <3" << std::endl;
		  
		  exit(0);
		}
	} else {
		std::cout << cruption.tag << "unrecognized parameter(s) received for main();" << std::endl;
		std::cout << cruption.tag << "default usage : .\\\"Dash Cruption.exe\" [OPTIONS] [TEXT] || Use \"-H\" or \"--help\" for help!" << std::endl;
	  exit(0);
	}
	
    /*####################################################################*/
	/*############################### END ################################*/
	/*####################################################################*/	
	
   exit(0);
 }

/* --- END --- */