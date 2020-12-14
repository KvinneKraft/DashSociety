//////////////////////
/* IP Detection 1.0 */
//////////////////////

/* 
  > (c) All Rights Reserved, Dashies Software Inc.
  > DASHIES SOFTWARE & DASHWARE PRESENTS SIMPLE IP DETECTION!
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

#include"VARIABLES.h"
#include"API.h"

  int main(void) {
	 
	 localWindow = GetConsoleWindow();
	  
	  ShowWindow((HWND)localWindow, SW_SHOW);
	   
	   if(LocalInformation() != true) 
		  std::cout << "An Error Occurred ;-\'( \n\nWe have created a report on the error. \n\n\"" + juicy_variable.errMsg + "\" \n\nIf this error procceeds even when the required \nthings are in place, and you can confirm \nthat you are not the one causing this error. \nplease then contact our Administration Team." << std::endl;
	   else {
		  std::cout << "-==::: IP Detection 5.0.0.0 :::==- \n\n" <<  std::endl;
		  std::cout << "Username..........: " << juicy_variable.username << std::endl;
		  //std::cout << "Hostname..........: " << juicy_variable....
		  std::cout << "MAC Address.......: " << juicy_variable.macid << std::endl;
		  std::cout << "Private IP Address: " << juicy_variable.private_ip << std::endl;
		  std::cout << "Public IP Address.: " << juicy_variable.public_ip << std::endl;
		  
		  std::cout << "GEO Lookup 1.0 {\n" << juicy_variable.geo_ip << "\n}" << std::endl;
	   }
	  
	  getch();
	return 0;
  }

/* --- END --- */
