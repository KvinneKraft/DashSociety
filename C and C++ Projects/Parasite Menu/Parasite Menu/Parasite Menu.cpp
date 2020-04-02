
// Author: Dashie
// Version: 1.0

#include <iostream>
#include <thread>
#include <vector>
#include <string>

#include <windows.h>
#include <conio.h>

using namespace std;

int main(void)
{
    SetConsoleTitle((":::: Dashie\'s Budz Store ::::"));

    cout << "----------------------------------------------------\n";
    cout << "| Strain Name  |   1g    |   5g  |   10g   | THC % |\n";
    cout << "----------------------------------------------------\n";
    cout << "| Dash Kush    |    10$  |  45$  |   65$   |  27%  |\n";
    cout << "| OG Kush      |  12.5$  |  55$  |   90$   |  23%  |\n";
    cout << "| Buddha Kush  |    8$   |  35$  |   55$   |  17%  |\n";
    cout << "| S Kush       |    9$   |  40$  |   50$   |  24%  |\n";
    cout << "| White Widow  |   15$   |  60$  |  100$   |  27%  |\n";
    cout << "| Pineapple-E  |   17$   |  75$  |  125$   |  32%  |\n";
    cout << "| Lemon Haze   |    7$   |  30$  |   55$   |  22%  |\n";
    cout << "| Purple Haze  |    9$   |  40$  |   75$   |  37%  |\n";
    cout << "| Silver Haze  |   10$   |  45$  |   85$   |  25%  |\n";
    cout << "----------------------------------------------------\n";

    while (true)
    {
	string get;

	cout << "(Strain)> ";
	getline(cin, get);
    };

    return -1;
};