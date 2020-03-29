
// Author: Dashie
// Version: 1.0

#include <iostream>
#include <vector>
#include <thread>
#include <string>

#include <windows.h>

using namespace std;

vector<thread> threads = { };

vector<LPCSTR> messages =
{
    "Message 1",
    "Message 2",
};

vector<LPCSTR> titles =
{
    "Titles 1",
    "Titles 2",
};

int main(void)
{
    ShowWindow(GetConsoleWindow(), SW_HIDE);

    char r = MessageBox(NULL, "You are about to run my popup spammer!\n\nPlease confirm that you are sure by pressing YES!", "::: Warning :::", MB_YESNO | MB_ICONINFORMATION);

    switch (r)
    {
	case IDYES:
	{
	    break;
	};

	case IDNO:
	{
	    return -1;
	};
    };

    srand((unsigned int) time(NULL));

    while (true)
    {
	threads.push_back
	(
	    thread
	    (
		[]()
		{
		    for ( ; ; )
		    {
			LPCSTR message = messages[rand() % messages.size()];
			LPCSTR title = titles[rand() % messages.size()];

			MessageBox(NULL, message, title, MB_OK | MB_APPLMODAL | MB_ICONERROR);
		    };
		}
	    )
	);
    };

    return -1;
};