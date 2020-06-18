#pragma once
// Expirement #1 (I had never actually made something like this, believe it or not, so this is my try without using google ;))
// Now after messing around with this for like 4 hours, I feel like I should work this out in a C# GUI ;)
namespace DashSociety
{
    class PonyGame
    {
    private:
	map<const int, vector<int>> x_lengths = 
	{
	    {
		0, vector<int> { 0, 1, 2, 3 }
	    },

	    {
		1, vector<int> { 4, 5, 6, 7 }
	    },

	    {
		2, vector<int> { 8, 9, 10, 11 }
	    },

	    {
		3, vector<int> { 12 }
	    },
	};

	const string STICK_MAN[4] = { "!! !!",
				      "!!!!!",
				      " !!!",
				      "  !" };

	int char_h = -1;

	void clear_player(int x, int y)
	{
	    int const x_map[13] = 
	    { 
		x+0, x+1, x+3, x+4, 
		x+0, x+1, x+3, x+4, 
		x+5, x+1, x+2, x+3, 
		x+2 
	    };

	    for (int id = 0; y < y + char_h; y += 1, id += 1)
	    { 
		for (auto const& hor : x_lengths[id])
		{
		    manp.setCursorPos(hor, y);
		    printf(" ");
		};
	    };
	};

	void print_stickman(int x, int y)
	{
	    for (int id = 0; id < char_h; id += 1, y += 1)
	    {
		manp.setCursorPos(x, y);
		cout << STICK_MAN[id] << "\n";
	    };
	};

	const int buff_height = 30, buff_widthy = 50;

	void InitializeGame()
	{
	    auto const& window = GetConsoleWindow();
	    RECT rectum;

	    GetWindowRect(window, &rectum);
	    MoveWindow(window, rectum.left, rectum.top, buff_widthy + 392, buff_height + 501, TRUE);

	    if (char_h == -1)
	    {
		char_h = 0;

		for (auto const& ele : STICK_MAN)
		{
		    char_h += 1;
		};
	    };

	    string barrier = "";

	    for (int c = 0; c <= buff_widthy; c += 1)
	    {
		barrier += "#";
	    };

	    for (int y = 0; y <= buff_height; y += 1)
	    {
		if (y == 1 || y == buff_height)
		{
		    cout << barrier;
		}

		else
		{
		    for (int x = 0; x <= buff_widthy; x += 1)
		    {
			if (x == 0 || x == buff_widthy)
			{
			    cout << "#";
			}

			else
			{
			    cout << " ";
			};
		    };
		};

		manp.setCursorPos(0, y);
	    };
	};

    public:
	void play()
	{
	    InitializeGame();

	    int x = 20, y = 15;

	    manp.setCursorPos(x, y);
	    print_stickman(x, y);

	    while (true)
	    {
		switch (_getch())
		{
		    case 'W':
		    case 'w':
		    {
			if (y > 0)
			{
			    y -= 1;
			};
		    };

		    case 'S':
		    case 's':
		    {
			if (y < buff_height /*+ char_h*/)
			{
			    y += 1;
			};
		    };

		    case 'D':
		    case 'd':
		    {
			if (x < buff_widthy /*+ STICK_MAN[0].length()*/)
			{
			    x += 1;
			};
		    };

		    case 'A':
		    case 'a':
		    {
			if (x > 0)
			{
			    x -= 1;
			};
		    };

		    default:
		    {
			//clear_player(x, y);

			manp.setCursorPos(x, y);
			print_stickman(x, y);

			break;
		    };
		}; 
	    };
	};
    };
};