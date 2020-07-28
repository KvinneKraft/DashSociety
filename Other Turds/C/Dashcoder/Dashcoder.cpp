// Author: Dashie
// Version: 1.0

#include <iostream>
#include <string>
#include <vector>

#include <conio.h>

std::vector<char> charset;

static class CHARACTER
{
    public: 
    
    int v_sizeof(std::vector<char> d)
    {
	int s = 1;

	for (const char& c : d)
	{
	    s += 1;
	};

	return s;
    };

} _char;

int main(void)
{
    if (charset.size() < 1)
    {
	std::string chars = "qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890!@#$%^&*()~`-=_+[]{};:\'\"\\|/?,.<> ";
	
	for (const char& c : chars)
	{
	    charset.push_back(c);
	};
    };

    for (int s = 1, k = 0; k < _char.v_sizeof(charset) - 1; k += 1, s += 1)
    {
	std::cout << "[(";
	
	if (k < 9)
	{
	    std::cout << "0";
	};

	std::cout << k + 1 << ")" << ":(" << charset[k] << ")] ";

	if (s >= 4)
	{
	    std::cout << std::endl;
	    s = 0;
	};
    };
    
    _getch();

    return -1;
};
