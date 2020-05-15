
// Author: Dashie
// Version: 1.0

using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Text;
using System;

namespace Quiz
{
    class Program
    {
	private static readonly List<string> questions = new List<string>()
	{
	    "Question 1", "Question 2", "Question 3", "Question 4", "Question 5"
	};

	private static readonly List<string> answers = new List<string>()
	{
	    "Answer 1", "Answer 2", "Answer 3", "Answer 4", "Answer 5"
	};

	private static readonly List<string> replies = new List<string>();
	
	static void Main(string[] args)
	{
	    int score = 0;

	    for ( int key = 0; key < questions.Count; key += 1 )
	    {
		Console.WriteLine(questions[key]);
		Console.Write($"(Answer): ");

		string reply = Console.ReadLine();

		if (reply.ToLower() == answers[key])
		{
		    score += 1;
		};

		replies.Add(reply);
	    };

	    Console.WriteLine("================================");
	    Console.WriteLine($"Score: {score.ToString()}/{questions.Count}");
	    Console.WriteLine("================================\r\n");

	    for ( int key = 0; key < questions.Count; key += 1 )
	    {
		Console.WriteLine($">>> Question ({key.ToString()}): {questions[key]} \r\n>>> Your Answer ({key.ToString()}): {replies[key]} \r\n>>> Correct Answer ({key.ToString()}): {answers[key]}\r\n\r\n");
	    };

	    Console.WriteLine("================================");
	    Console.ReadLine();
	}
    };
};
