/* (c) All Rights Reserved, Dashies Software Inc. ~ a little C file :o */


#include<windows.h>
#include<mmsystem.h>
#include<wininet.h>

#include<conio.h>
#include<lmcons.h>
#include<stdlib.h>
#include<stdio.h>
#include<string.h>

#include<iostream>

#include<cstdio>
#include<cstdlib>
#include<string>
#include<fstream>
#include<sstream>
#include<map>
#include<vector>
#include<algorithm>

using namespace std;

std::string ConfirmMessage = "";


// yaaay little banner thingy yup
std::string cute_array[] = {
    ("<=================================================> \n"
     " |console applications by dashies software inc <3|  \n"
     " |-----------------------------------------------|  \n"
     " |  -------------------------------------------  |  \n"
     " |---VERSION 1.0 ~ RELEASE 1.0 ~ STABLE OUTPUT---|  \n"
     " |  -------------------------------------------  |  \n"
     "<=================================================> \n")
};

std::string TheUser() {
     std::string username;
     
     DWORD unlen = UNLEN+1;
     char buffer[UNLEN+1];
     
       if(GetUserName(buffer, &unlen) != true)
           username = "Yannay"; // I know, I am funny >.<
       else
           username = buffer;
     
   return username.c_str();
}

  int main(void) {
     SetConsoleTitle((TEXT("\"~root/tools/reg dns.exe\"")));
     
     ConfirmMessage = "\n\n\tHeyyyy " + TheUser() + "! \n\n\tHold up before you continue! \n\n\tmake sure you have read README.md! \n\n\tand you are about to Register all your DNS Servers. \n\n\tplease confirm that you are sure about \n\tthis decision by pressing OK! \n\n\tif you do not want to proceed press CANCEL!";
     
     std::cout << cute_array[0] << std::endl;
     std::cout << "[DNS Registration]: awaiting message box input..." << std::endl;
     
      if(MessageBox(NULL, TEXT(ConfirmMessage.c_str()), TEXT("::: DASHWARE :::"), MB_OKCANCEL | MB_ICONWARNING | MB_SYSTEMMODAL) == true) {
          std::cout << "[DNS Registration]: received ID_OK, proceeding registration..." << std::endl;
          std::cout << "[DNS Registration]: registering DNS Servers... " << std::endl;
          
         if(system("ipconfig /registerdns > nul") == true) {
             std::cout << "[DNS Registration]: failure registering DNS Servers!" << std::endl;
             std::cout << "[DNS Registration]: this may be caused by insufficient privileges~" << std::endl;
         } else
             std::cout << "[DNS Registration]: successfully registered DNS Servers!" << std::endl;
          
      } else {
         std::cout << "[DNS Registration]: received ID_CANCEL, successfully aborted operation!" << std::endl;
          MessageBox(NULL, TEXT("\n\n\tYou have successfully aborted the DNS Registration~"), TEXT("::: DASHWARE :::"), MB_OK | MB_ICONSTOP | MB_SYSTEMMODAL);
      }
     
     std::cout << "[DNS Registration]: press any key to exit program with return value 0~" << std::endl;
     getch(); 
     
    exit(0);
  }
