
// Author: Dashie
// Version: 1.0

#include "headers\\headers.h"

using namespace std;

unordered_map<string, string> user_session;

int main(void)
{
    if (user_session.size() < 1)
    {
	return -1;//De-Authenticate
    };

    string _help =
    (
	"Available Commands: " 
	"" 
	""
	""
    );

    string _ = "";

    while (true)
    {
	cout << "($): ";
	getline(cin, _);

	if (_.length() < 1)
	{
	    continue;
	}

	else if (_ == "sys")
	{

	}

	else if (_ == "str")
	{

	};
    };

    return -1;
};