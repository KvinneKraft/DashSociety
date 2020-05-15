
//#include<windows.h>//Windows Only
//#include<conio.h>//Windows Only

#include<iostream>
#include<string>
#include<vector>
#include<map>

using namespace std;

namespace DashSociety
{
    class Authentication
    {
        private:
        
        // As of now stored as an open string, soon will be run-time generated using
        // a custom algorithm designed by me Dashieee.
        map<int, vector<string>> verify = {
            { 0, vector<string> { "DashSociety", "DashSec", "kvinnekraft@protonmail.com" } },
            { 1, vector<string> { "Gertje", "Gerdanus", "gertje@protonmail.com" } }
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

            for (auto key = 0; key < verify.size(); key += 1)
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

auto error = -1;
auto succe = 1;

int main(void)
{
    auto auth = DashSociety::Authentication();
    auto perm = auth.SignIn();

    if (!perm)
    {
        cout << "(!) Odin has closed the gates of Valhala infront of you!\n";
        cout << "(!) You will have to close this application in order to retry, press enter to continue.";
        
        string key;
        getline(cin, key);
        
        return error;
    };

    cout << "(~) Welcome back " + auth.username + ", blessed be )o(!\n";

    return succe;
};