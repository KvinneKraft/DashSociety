
namespace DashSociety
{
    class Manipulation
    {
    public:
	void clear()
	{
	    HANDLE handle = GetStdHandle(STD_OUTPUT_HANDLE);
	    CONSOLE_SCREEN_BUFFER_INFO info;

	    GetConsoleScreenBufferInfo(handle, &info);

	    auto const& widthy = (info.srWindow.Right - info.srWindow.Left + 1);
	    auto const& height = (info.srWindow.Bottom - info.srWindow.Top + 1);

	    COORD posixy;

	    for (int s = 0, x = 0, y = 0; s < (widthy * height); s += 1, x += 1)
	    {
		if (x == widthy)
		{
		    y += 1;
		    x = 0;
		};

		posixy.X = x;
		posixy.Y = y;

		SetConsoleCursorPosition(handle, posixy);
		printf(" ");
	    };

	    posixy.X = 0;
	    posixy.Y = 0;

	    SetConsoleCursorPosition(handle, posixy);
	};

	vector<string> split(const string& data, const char& hit)
	{
	    vector<string> result;
	    string buffer;

	    auto const &len = (const int)data.length();

	    for (auto id = 0; id < len; id += 1)
	    {
		if (data[id] == hit)
		{
		    result.push_back(buffer);
		    buffer = "";

		    continue;
		};

		buffer += data[id];

		if (id == len - 1)
		{
		    result.push_back(buffer);
		    break;
		};
	    };

	    return result;
	};

	string replace(const string& from, const string& to, const string& data)
	{
	    string result;

	    if (from.length() > 1)
	    {
		vector<string> words = split(data, ' ');

		for (auto const& word : words)
		{
		    if (word == from)
		    {
			result += to + " ";
		    }

		    else
		    {
			result += word + " ";
		    };
		};

		return result += "\b";
	    }

	    else//Assuming 1 length
	    {
		result = data;

		for (int id = 0; id < (signed) result.length(); id += 1)
		{
		    if (result[id] == from[0])
		    {
			result[id] = to[0];
		    };
		};

		return result;
	    };
	};

	string tolower(string data)
	{
	    if (data.length() < 1)
	    {
		return "ERROR: NO DATA GIVEN!";
	    };

	    std::transform
	    (
		data.begin(),
		data.end(),
		data.begin(),

		[](unsigned char c)
		{
		    return std::tolower(c);
		}
	    );

	    return data;
	};
    };
};