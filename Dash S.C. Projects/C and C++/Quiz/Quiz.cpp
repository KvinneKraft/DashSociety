
// Author: Dashie
// Version: 1.0

#include<conio.h>

#include<algorithm>
#include<iostream>
#include<thread>
#include<vector>
#include<string>
#include<cctype>

using namespace std;

vector<string> questions = { "Question 1", "Question 2", "Question 3" };
vector<string> answers = { "Answer 1", "Answer 2", "Answer 3" };
vector<string> replies = { };

string toLower(string str)
{
    transform(str.begin(), str.end(), str.begin(), [](unsigned char c) { return tolower(c); });
    return str;
};

int main(void)
{
    int score = 0;

    for (int k = 0; k < (int) questions.size(); k += 1)
    {
	cout << questions[k] << endl;
	cout << "(Your Answer): ";

	string answer;
	getline(cin, answer);

	if (toLower(answer) == toLower(answers[k]))
	{
	    score += 1;
	};

	replies.push_back(answer);
    };

    cout << "Score: " << to_string(score) << "/" << to_string(questions.size()) << "\n\n";

    for (int k = 0; k < (int) questions.size(); k += 1)
    {
	cout << "----------[" + to_string(k) + "]----------\n";
	cout << "The Question: " << questions[k] << "\n";
	cout << "Your Answer: " << replies[k] << "\n";
	cout << "The Correct Answer: " << answers[k] << "\n";
	cout << "-----------------------\n";
    };

    _getch();

    return -1;
};