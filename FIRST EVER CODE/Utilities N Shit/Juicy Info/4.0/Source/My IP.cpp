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

#include<ctime>
#include<map>
#include<algorithm>
#include<vector>

#include"VARIABLES.h"
#include"API.h"

 using namespace std;

  int main(void) {
	 
	 localWindow = GetConsoleWindow();
	  
	  ShowWindow((HWND)localWindow, SW_HIDE);
	   
	   if(LocalInformation() != true) {
		  juicy_variable.errMsg = "\n\n\tAn Error Occurred ;-\'( \n\n\tWe have created a report on the error. \n\n\t\"" + juicy_variable.errMsg + "\" \n\n\tIf this error procceeds even when the required \n\tthings are in place, and you can confirm \n\tthat you are not the one causing the error. \n\tplease then contact our Administration Team.";
		   
		   MessageBox(NULL,
			          TEXT(juicy_variable.errMsg.c_str()),
					  TEXT(dialogTitle),
					  MB_OK | MB_ICONSTOP);
						 
		 return 0;
	   } 
	   
	 juicy_variable.dialog_message = "IP Detection " + APP_VERSION +
	                  
					  " QUICK! \n\n\n\nUsername..................: " + juicy_variable.username +
	                  " \nHostname.................: " + juicy_variable.hostname + 
	                  
					  " \n\nMac Address.............: " + juicy_variable.macid + 
					  " \n\nPrivate IP Address....: " + juicy_variable.private_ip +
					  " \nPublic IP Address.....: " + juicy_variable.public_ip +
					  
					  " \nGeoIPLookup(){" + juicy_variable.geo_ip +
					  
					  "}; \n\n\n\n(c) All Rights Reserved, Dashies Software Inc.";
	  
	  MessageBox(NULL, TEXT(juicy_variable.dialog_message.c_str()), TEXT(dialogTitle), MB_OK);
	  
	return 0;
  }

/* --- END --- */
