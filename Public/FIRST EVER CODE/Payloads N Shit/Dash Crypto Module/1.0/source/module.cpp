/* Simple Authentication */

#include<windows.h>
#include<mmsystem.h>

#include<stdlib.h>
#include<stdio.h>

#include<string.h>

#include<tchar.h>
#include<wchar.h>

#include<conio.h>
#include<lmcons.h>
#include<wininet.h>

#include<iostream>

#include<algorithm>

#include<cstdlib>
#include<cstdio>

#include<string>
#include<ctime>
#include<map>

using namespace std;

struct Algorithm{
  std::string result, sBuffer, parse;
  
  HANDLE ExportFile;
  DWORD  WriteBuffer;
  
  char push;
  int buff, address;
}algorithm;

static int charLength=100;

const char *charTable[]={
   (" "),        ("12642501"), ("9e6a31dd"), ("9ca11b2b"), ("12561686"), ("adc35b6d"), // 6
   ("b1610daa"), ("10d2511d"), ("8837a3b6"), ("a8f951b4"), ("663d5a1b"), ("009deffb"), // 12
   ("55f3223b"), ("f9cec468"), ("2ed4e859"), ("70fa99b5"), ("81bcb5af"), ("b91c35a0"), // 18
   ("66efc71d"), ("6a0a1b19"), ("2471dbd0"), ("84be02e5"), ("f278890a"), ("dcf33f10"), // 24
   ("46ad66e7"), ("17ea8c9f"), ("56423b19"), ("05ff233d"), ("6d2ede63"), ("afe05c5a"), // 30
   ("bc21b8a5"), ("e5183aa0"), ("30c86dab"), ("4f96ef5d"), ("b804d5e2"), ("44e7b080"), // 36
   ("b30008fb"), ("de77020f"), ("f3c62550"), ("cf391a46"), ("ed0e426c"), ("68766670"), // 42
   ("7100b91d"), ("9d57a3f4"), ("c0416d28"), ("92b27c95"), ("d144a5ba"), ("6c31c67e"), // 48
   ("99620ae4"), ("25ac75d6"), ("58145d95"), ("82360e16"), ("84861080"), ("d11d3f28"), // 54
   ("f2ea6446"), ("e9044efb"), ("55e72527"), ("4ac71868"), ("ec3a3d86"), ("955cf14c"), // 60
   ("578f0f6d"), ("04f0e5db"), ("63d7f296"), ("903c4a3e"), ("e83b108e"), ("45beae90"), // 66
   ("42846970"), ("a50defe6"), ("73799d84"), (""), (""), ("") // 72
};

const char *BANNER[]={
   ("[(c) All Rights Reserved, Dashies Software Inc.] \n\n"
    "- Simple 123 Algorithm Encryption, 1 and 2 plus 3 = 6 - \n")
};

  int main(int length, char ** argument) {
  	
	std::cout << BANNER[0] << std::endl;
	
  	std::cout << "!/Encrypt Text> ";
  	
  	getline(cin, algorithm.parse);
  	 
	  if((algorithm.parse == "") || (algorithm.parse == " ")) {
		   std::cout << "[Encryptor> Invalid password size received, try again!" << std::endl;
		   
		  getch();
	     exit(0);
	  }
  	
	std::cout << "[Encryptor> Encrypted Password : " << algorithm.result << std::endl;
     
   retry:
	 
	std::cout << "[Encryptor> Would you like save this to a file [Y/n]?";
	
	   switch(getch()){
		  case 'y' :
		  case 'Y' : {
		     algorithm.WriteBuffer = 0;
			 algorithm.ExportFile  = NULL;
			 algorithm.sBuffer     = algorithm.result;
			 
			 algorithm.ExportFile = ::CreateFile(TEXT("Encrypted Password.dcore-text"),
			                                     GENERIC_WRITE,
					    					     0,
										         NULL,
										         CREATE_ALWAYS,
										         FILE_ATTRIBUTE_ENCRYPTED | FILE_ATTRIBUTE_NORMAL,
										         NULL);
										
			   if(::WriteFile(algorithm.ExportFile,
			                  TEXT(algorithm.result.c_str()),
							  TEXT(algorithm.result.size()),
							  &algorithm.WriteBuffer,
							  NULL) == 0) {
					
                    std::cout << "[Encryptor> Unable to write encrypted password file ;\'(" << std::endl;
					   
				   getch();
				  exit(0);
			   }  else {
				   ::CloseHandle(algorithm.ExportFile);
				   
					std::cout << "\n[Encryptor> Successfully exported encrypted password file!" << std::endl;
					std::cout << "=====================================" << std::endl;
					std::cout << "<<<<< Press any Key to Continue >>>>>" << std::endl;
					
				   getch();
                  exit(0);  
			   }
	      } break;
		  
		  case 'n':
		  case 'N': {
		     exit(0);
		  } break;
		  
		  default :
              std::cout << "\n[Encryptor> Invalid input received, try again :-)" << std::endl; 
		    goto retry; 
		  break;
	   }

  	EXIT_SUCCESS;
  }
