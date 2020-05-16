
namespace DashSociety
{
    class Authentication
    {
    private:
	// As of now stored as an open string, soon will be run-time generated using
	// a custom algorithm designed by me Dashieee.
	map<int, vector<string>> verify =
	{
	    {
		0, vector<string> { "DashSociety", "DashSec", "kvinnekraft@protonmail.com" }
	    },
	};

    public:
	string username;
	string password;
	string email;

	bool SignIn()
	{
	    vector<string> creds = { "", "", "" };

	    cout << "(Username): ";
	    getline(cin, creds[0]);

	    cout << "(Password): ";
	    getline(cin, creds[1]);

	    cout << "(Email): ";
	    getline(cin, creds[2]);

	    for (auto key = 0; key < (signed int)verify.size(); key += 1)
	    {
		for (auto const& value : creds)
		{
		    if (value.length() < 3)
		    {
			return false;
		    };
		};

		if (verify[key][0] == creds[0] && verify[key][1] == creds[1] && verify[key][2] == creds[2])
		{
		    cout << "(!) Success, you are now logged in.\n";

		    username = creds[0];
		    password = creds[1];
		    email = creds[2];

		    return true;
		};
	    };

	    return false;
	};
    };
};