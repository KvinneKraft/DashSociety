
namespace DashSociety
{
    class Manipulation
    {
    public:
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
	    vector<string> words = split(data, ' ');
	    string result;

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