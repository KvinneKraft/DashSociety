/////////////////////////////
/* INTERNAL Declaration(s) */
/////////////////////////////

std::string APP_VERSION  = "4.0";

#define APPLICATION_ICON 9001

#define EX_VERSION "4.0.0.0"

//////////////////////////
/* Loose Declaration(s) */
//////////////////////////

HWND localWindow;
LPCSTR dialogTitle = "Pony Dialog <3";

//////////////////
/* Structure(s) */
//////////////////

struct JuicyDashie {
	DWORD dashWord, read;
	WORD version;
	WSADATA wsa;
	
	UUID uuid;
	
	HINTERNET connect, retrieve;
	
	std::string public_ip, private_ip, geo_ip, 
	            username, hostname, 
				macid, buffer, 
				dialog_message, errMsg;
	
	bool reader, 
	     handler;
	
    char userBuffer[UNLEN+1],
	     netbuff[4056],
		 geoip[4056],
		 hostbuff[127];
	
	unsigned char MACData[6];
	int errValue;
}juicy_variable;
