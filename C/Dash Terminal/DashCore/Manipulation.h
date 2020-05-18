
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

		void setCursorPos(int const& x, int const& y)
		{
			auto const& handle = GetStdHandle(STD_OUTPUT_HANDLE);
			COORD posixy;

			if (x < 0 || y < 0)
			{
				CONSOLE_SCREEN_BUFFER_INFO info;

				GetConsoleScreenBufferInfo(handle, &info);

				if (x < 0)
				{
					x = (info.srWindow.Right - info.srWindow.Left + 1) / 2;
				};
				
				if (y < 0)
				{
					y = (info.srWindow.Bottom - info.srWindow.Top + 1) / 2;				
				};
			};

			posixy.x = x;
			posixy.y = y;

			SetConsoleCursorPosition(handle, posixy);
		};

		void play()
		{
			CONSOLE_SCREEN_BUFFER_INFO info;

			GetConsoleScreenBufferInfo(handle, &info);

			auto const& buff_widthy = (info.srWindow.Right - info.srWindow.Left + 1);
			auto const& buff_height = (info.srWindow.Bottom - info.srWindow.Top + 1);			

			int x = 0, y = 0;

			auto const& STICK_MAN = (
				"  ###" 
				"  ###"
				"   #"//Perhaps make a pony animation?
				" #####"
				"#  #  #"
				"#  #  #"
				"  ###"
				" #   #"
				" #   #"				
			);

			while (true)
			{
				switch (_getch())
				{
					case 'W':
					case 'w':
					{
						if (y < buff_height)
						{
							y += 1;
						};

						break;
					};

					case 'S':
					case 's':
					{
						if (y > 0)
						{
							y -= 1;
						};

						break;
					};

					case 'D':
					case 'd':
					{
						if (x < buff_widthy)
						{
							x += 1;
						};

						break;
					};

					case 'A':
					case 'a':
					{
						if (x > 0)
						{
							x -= 1;
						};

						break;
					};															
				};

				setCursorPos(x, y);
				cout << STICK_MAN;// Create a Wipe Character function 
			};
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