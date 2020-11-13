
// Author: Dashie
// Version: 1.0

#include<iostream>
#include<string>

int main(void)
{
    std::cout << "Hey there, how have you been?" << std::endl;
    std::cout << "(Answer): ";

    std::string answer;
    std::getline(std::cin, answer);

    if (answer.length() < 1)
    {
	std::cout << "Aw ;c" << std::endl;
    }

    else
    {
	std::cout << "That is nice." << std::endl;
    };

    return - 1;
}