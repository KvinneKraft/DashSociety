/* Dashies Software Inc (c) Copyright 2019 */

#include "pch.h"

#include<windows.h>

#include<iostream>

#include<cstdlib>
#include<cstdio>

#include<string>

int main(int l, char ** c)
{
	// The amount of Processes we will use.
	int Processes = 5;


	// The Executable we will use as our Advantage.
	std::string Process = "C:\\Windows\\notepad.exe";


	// Support 32-bit Windows Operating Systems.
	CoInitializeEx(NULL, COINIT_APARTMENTTHREADED | COINIT_DISABLE_OLE1DDE);

	
	// Set a custom Console Title.
	SetConsoleTitle(TEXT("Grace No Disgrace - Malware 1.0"));


	// The Executioner, this loop will execute the 
	// specified "Process" (aka Executable) as many
	// times as you have specifed at "Processes".
	for (int index = 1; index <= Processes; index += 1) 
	{
		// The function I will use to execute the given Process/Executable ShellExecute(NULL, "open", "", NULL, NULL, SW_SHOW);.
		ShellExecute(
			NULL,			 // Not really applyable right now.
			"open",			 // The execution method ("open" or "runas")
			Process.c_str(), // The process/executable we have specified all the way at the top.
			NULL,			 // Not applyable either.
			NULL,			 // Same for this.
			SW_SHOW			 // SW_SHOW will make the target Visible, and SW_HIDE will make the target Invisible.
		);
	}

	return 0;
}
