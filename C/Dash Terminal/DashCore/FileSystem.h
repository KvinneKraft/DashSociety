
namespace DashSociety
{
    class FileSystem
    {
    public:
	class FILE_SYSTEM_ERROR
	{
	public:
	    static const int DIRECTORY_EXISTS = 0;
	    static const int OTHER_ERROR = -1;
	    static const int SUCCESS = -2;
	};

	bool direxist(const LPCSTR& dir)
	{
	    auto const& typ = GetFileAttributesA(dir);

	    switch (typ)
	    {// I love switch statements <3
		case INVALID_FILE_ATTRIBUTES:
		{
		    return false;
		};

		default:
		{
		    return (typ && FILE_ATTRIBUTE_DIRECTORY);
		};
	    };
	};

	int makedir(const LPCSTR& dir)
	{
	    if (direxist(dir))
	    {
		return FILE_SYSTEM_ERROR::DIRECTORY_EXISTS;
	    };

	    Manipulation manp;

	    CreateDirectory(manp.replace("/", " ", string(dir)).c_str(), NULL);

	    if (direxist(dir))
	    {
		return FILE_SYSTEM_ERROR::SUCCESS;
	    }

	    else
	    {
		return FILE_SYSTEM_ERROR::OTHER_ERROR;
	    };
	};

	string getdir()
	{
	    TCHAR currdir[MAX_PATH];
	    GetCurrentDirectory(MAX_PATH, currdir);
	    return string(currdir);
	};
    };
};