#include<iostream>
#include<thread>
#include<vector>
#include<string>

#include<windows.h>
#include<conio.h>

using namespace std;

vector<thread> threads = { };

int main(void)
{
    ShowWindow(GetConsoleWindow(), SW_HIDE);

    while (true)
    {
	Sleep(8000);

	threads.push_back
	(
	    thread
	    (
		[]() 
		{
		    for (int k = 0; k < 320; k += 1, Sleep(200))
		    {
			threads.push_back
			(
			    thread
			    (
				[]()
				{
				    MessageBox(NULL, "You go Oof", "Dashie says Oof", MB_OK | MB_ICONINFORMATION);
				}
			    )
			);
		    };
		}
	    )
	);
    };

    return -1;
};