
// Author: Dashie
// Version: 1.0

#include<algorithm>
#include<iostream>
#include<cstdlib>
#include<cstdio>
#include<string>
#include<cmath>
#include<ctime>

#include<conio.h>

using namespace std;

string CHARACTERS = "1234567890qwertyuiopasdfghjklzxcvbnm";
int CAPTCHA_LENGTH = 6;

string getKey()
{
    string KEY;

    for (int k = 0; k < CAPTCHA_LENGTH; k += 1)
    {
	int ID = rand() % CHARACTERS.length() - 1;
	char CHAR = CHARACTERS[ID];

	if (isalpha(CHARACTERS[ID]))
	{
	    if (rand() % 3 >= 2)
	    {
		CHAR = toupper(CHAR);
	    };
	};

	KEY += CHAR;
    };

    return KEY;
}

int main(void)
{
    srand((unsigned int)time(NULL));

    cout << "-= Simple captcha mechanism by Dashie =-\n";

    string INP, KEY = getKey();
    int TRY = 1, MAX_TRIES = 3;

    cout << "Captcha: " + KEY + "\n";

    while (true)
    {
	cout << "Input: ";
	getline(cin, INP);

	if (INP != KEY)
	{
	    if (TRY >= MAX_TRIES)
	    {
		cout << "You entered the wrong value too many times.\n";
		cout << "Press any key to quit. ";

		_getch();

		exit(-1);
	    };

	    cout << "Wrong " << TRY << "\\" << MAX_TRIES << "!\n";

	    TRY += 1;

	    continue;
	};

	cout << "Key is OK!\n";
	cout << "Press any key to quit. ";

	_getch();

	break;
    };
};