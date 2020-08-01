// Author: Dashie
// Version: 1.0

#include <iostream>
#include <string>
#include <vector>

#include <windows.h>
#include <stdlib.h>
#include <stdio.h>
#include <conio.h>

using namespace std;

vector<LPCSTR> books = { "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "" };
vector<LPCSTR> music = { "wl-eVipq5cE", "6XKWz62_9nA", "82XoHLrFzwE", "Y_1e1q2NbKQ", "2OVpXKGs7eE", "ctpOUkzDgvw", "6Arz6KyYoEw" };

int main()
{
    cout << "(System): Hey there, you are probably has a fuck again huh? Just give me a moment, I got ur back Daschieeee!" << endl;
    _sleep(3000);
   
    cout << "[=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-]\n"
	 << "|                                              |\n" 
	 << "|  ::: The High and Fucked Up Helper Menu :::  |\n"
	 << "|                                              |\n"
	 << "|      ----------------------------------      |\n"
	 << "|                                              |\n"
	 << "|  1) Hop onto MoviesJoy.net.                  |\n"
	 << "|  2) Load up Grand Theft Auto V.              |\n"
	 << "|  3) Load up Youtube for some Music.          |\n"
	 << "|  4) Load up some Books.                      |\n"
	 << "|  5) Load up some Dank Music.                 |\n"
	 << "|                                              |\n"
	 << "[=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-]\n";

    while (true)
    {
	cout << "(Selector)> ";

	switch (_getch())
	{
	    case '1':
	    case 1:
	    {
		ShellExecute(NULL, "open", "https://moviesjoy.net", NULL, NULL, SW_SHOW);
		break;
	    };

	    case '2':
	    case 2:
	    {
		ShellExecute(NULL, "runas", "B:\\Steam\\steam.exe", NULL, NULL, SW_SHOW);
		break;
	    };

	    case '3':
	    case 3:
	    {
		ShellExecute(NULL, "open", "https://youtube.co.uk", NULL, NULL, SW_SHOW);
		break;
	    };

	    case '4':
	    case 4:
	    {
		for (const LPCSTR book : books)
		{
		    ShellExecute(NULL, "open", book, NULL, NULL, SW_SHOW);
		};

		break;
	    };

	    case '5':
	    case 5:
	    {
		for (const LPCSTR piece : music)
		{
		    string song = "https://www.youtube.com/watch?v=" + string(piece);
		    ShellExecute(NULL, "open", song.c_str(), NULL, NULL, SW_SHOW);
		};

		break;
	    };

	    default:
	    {
		break;
	    };
	};

	cout << "\n";
    };
};
