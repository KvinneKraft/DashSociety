
// Author: Dashie
// Version: 1.0

#include<windows.h>
#include<conio.h>

#include<iostream>
#include<string>

using namespace std;

namespace utilities
{
    void halt()
    {
	_getch();
    };

    void say(const string& msg)
    {
	cout << msg;
    };

    void clear()
    {
	for (int i = 0; i < 800; i += 1)
	{
	    printf("\n");
	};

	COORD coords;

	coords.X = (unsigned int) 0;
	coords.Y = (unsigned int) 0;

	SetConsoleCursorPosition(GetStdHandle, coords);
    };
};