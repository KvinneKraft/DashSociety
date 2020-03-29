
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
    "You got dashed!",
    "Spammmyyyyyy",
};

vector<LPCSTR> titles =
{
    "I am a title!",
    "Fukka yeyyyyyy",
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

    //while (true)
    for ( int popup = 0; popup < 8; popup += 1 )
    {
	threads.push_back
	(
	    thread
	    (
		[]()
		{
		    for ( int i = 0 ; i < 1 ; i += 1 )
		    {
			LPCSTR message = messages[rand() % messages.size()];
			LPCSTR title = titles[rand() % titles.size()];

			MessageBox(NULL, message, title, MB_OK | MB_APPLMODAL | MB_ICONERROR);
		    };
		}
	    )
	);
    };

    for (thread &t : threads)
    {
	t.join();
    };

    return -1;
};