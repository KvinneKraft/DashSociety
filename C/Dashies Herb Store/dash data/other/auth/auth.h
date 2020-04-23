
// Author: Dashie
// Version: 1.0

#include<iostream>
#include<vector>
#include<string>
#include<ctime>

using namespace utilities;
using namespace std;

class auth { 
private:
    const string charset = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";
    const int charlen = 8;

    string gen_captcha()
    {
	srand((unsigned int) time(NULL));

	string cache = "";

	for (int len = charset.length(), id = 0; id < charlen; id += 1)
	{
	    cache += charset[rand()%len];
	};

	return cache;
    };

public:
    void captcha()
    {
	const string cap = gen_captcha();

	say(">>> Please enter the following characters: " + cap + "\n");

	for (int tries = 0; tries < 4; tries += 1)
	{
	    say("(Reply)> ");
	    
	    string buffer;
	    getline(cin, buffer);

	    if (buffer == cap)
	    {
		say(">>> Success, Press any key to continue!");
		halt();

		break;
	    }

	    else if (tries + 1 > 3)
	    {
		say(">>> You have entered the captcha wrong too many times.\n");
		say(">>> Press any key to exit this application.");
		halt();

		exit(-1);
	    };

	    say(">>> invalid key!\n");
	};
    };
};