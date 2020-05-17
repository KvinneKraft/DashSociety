
#include<windows.h>
#include<conio.h>

#include<algorithm>
#include<iostream>
#include<vector>
#include<string>
#include<cctype>
#include<map>

using namespace std;

#include"DashCore/Authentication.h"
#include"DashCore/Manipulation.h"
#include"DashCore/FileSystem.h"

DashSociety::Manipulation manp;
DashSociety::FileSystem fsys;

extern string directory = ("DEFAULT");

extern void ShellExecution()
{
    manp.clear();

    if (directory == "DEFAULT")
    {
	directory = fsys.getdir();
    };

    string buff;

    for ( ; ; )
    {
	cout << "(" + directory + ")> ";
	getline(cin, buff);

	vector<string> args = manp.split(buff, ' ');

	if (args.size() < 1)
	{
	    cout << "\n";
	    continue;
	};

	auto arg = manp.tolower(args[0]);

	if (arg == "mkdir")
	{
	    if (args.size() < 2)
	    {
		cout << "(!) Usage: \'mkdir C:\\Users\\Dashie/Lunare\\Desktop\\Folder\'\n";
		cout << "(!) Note: Adding \'/\' instead of a space is recommended!\n";
		continue;
	    };

	    string dir;

	    for (int id = 1; id < (signed) args.size(); id += 1)
	    {
		dir += args[id];
	    };

	    switch (fsys.makedir(dir.c_str()))
	    {
		case DashSociety::FileSystem::FILE_SYSTEM_ERROR::OTHER_ERROR:
		{
		    cout << "(!) There was an unknown error that had occurred!\n";
		    break;
		};

		case DashSociety::FileSystem::FILE_SYSTEM_ERROR::DIRECTORY_EXISTS:
		{
		    cout << "(!) This directory already exists.\n";
		    break;
		};

		default:
		{
		    cout << "(+) OK.\n";
		    break;
		};
	    };

	    continue;
	};
    };
};

DashSociety::Authentication auth;

int main(void)
{
    if (!auth.SignIn())
    {
	manp.clear();

	cout << "(!) Odin has closed the gates of Valhala infront of you!\n";
	cout << "(!) You will have to close this application in order to retry, press enter to continue.";

	string key; getline(cin, key); exit(-1);
    };

    cout << "(+) Welcome back " + auth.username + ", blessed be )o(\n";
    cout << "(+) Please wait while I am doing some last thingies ....\n";

    SetConsoleTitle(TEXT("Dash Terminal - By Dashie A.K.A. Kvinne Kraft"));

    Sleep(3500);

    ShellExecution();
};