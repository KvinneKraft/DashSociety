
//Perhaps in the future make it able to sort by category. names[<type id>][<speciment id>][<name id>]

// Author: Dashie
// Version: 1.0

#include<string>
#include<vector>
#include<map>

using namespace std;

#define TYPE_COUNT 3

#define ID_NAME_FLOWERS 0 
#define ID_NAME_LEAFS 1
#define ID_NAME_STRAINS 2

#define ID_PRICE_FLOWERS ID_NAME_FLOWERS
#define ID_PRICE_LEAFS ID_NAME_LEAFS
#define ID_PRICE_STRAINS ID_NAME_STRAINS

namespace products
{
    map<int, vector<string>> names;
    map<int, vector<double>> prices;

    vector<string> quantities = 
    {
	"15g", "20g", "1.0g"
    };

    const string &currency = ("$");

    void init()
    {
	names[ID_NAME_FLOWERS] = vector<string>{ "Dandelion", "Daisies", "Corn Poppies", "(White) Clover", "(Red) Clover", "Dog Trot", };
	prices[ID_PRICE_FLOWERS] = vector<double> /* 15g */{ 4.50, 3.75, 8.50, 2.50, 2.50, 6.99 };

	names[ID_NAME_LEAFS] = vector<string>{ "Rosemary", "Nettle", "(White) Dead Nettle", "(Yellow) Dead Nettle", "(Purple) Dead Nettle", "Sage", };
	prices[ID_PRICE_LEAFS] = vector<double> /* 20g */{ 3.75, 2.50, 3.00, 3.25, 3.00, 6.00 };

	names[ID_NAME_STRAINS] = vector<string>{ "Cheese Kush", "Purple Haze", "Silver Haze", "Green Crack", "Buddha Kush", "American Pie", "Purple Skunk" };
	prices[ID_PRICE_STRAINS] = vector<double> /* 1.5g */ { 8.50, 7.50, 8.00, 4.50, 5.50, 9.50, 12.00 };
    };
};


