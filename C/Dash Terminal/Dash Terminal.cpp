
#include<windows.h>
#include<conio.h>

#include<iostream>
#include<string>

using namespace std;

int main(void)
{

    ShellExecution();
};

extern string directory = ("C:\\Windows\\System32");

extern void ShellExecution()
{
    string buff;

    for ( /*count executions*/ ; ; )
    {
	cout << "(" + directory + ")> ";
	getline(cin, buff);

	
    };
};