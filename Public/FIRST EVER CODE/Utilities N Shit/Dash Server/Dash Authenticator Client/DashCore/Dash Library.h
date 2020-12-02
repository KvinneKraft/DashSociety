#pragma once

/*                                                */
/* (c) All Rights Reserved, Dashies Software Inc. */
/*                                                */
/*           Dash Library 1.0 Advanced            */
/*                                                */

#include<windows.h>
#include<wininet.h>
#include<lmcons.h>
#include<conio.h>
#include<stdio.h>
#include<stdlib.h>
#include<string.h> 
#include<tchar.h>
#include<TlHelp32.h>
#include<time.h>
#include<math.h>
#include<process.h>
#include<winbase.h>
#include<comdef.h>
#include<comip.h>

#include<algorithm>
#include<iostream>
#include<sstream>
#include<fstream>
#include<string>

std::string Prefix_Table[] = { // Message Prefixes
	("[IMA] "),      // 0
	("<> "),        // 1
	("<Error> "),     // 2
	("")
};

std::string Message_Table[] = {
	("insufficient input received, please try again, and or type \"<authenticator> --help\" for a list with valid arguments you may parse upon one."), // < \/ > aka error

	("unknown arguments received, please try again, and or type \"<authenticator> --help\" for a list with valid arguments you may parse upon one.")
};

std::string ToLawerCase(std::string Data) {
	for(int index = 0; index <= Data.length(); index = index + 1) Data[index] = tolower(Data[index]);
	if(Data.length() <= 0) Data = "unable to parse given data.";

	return Data;
}

long int max_addr = 3,
dashiesays_count = 1;

static void DashieSays(int Address, std::string What) {
	if(Address > max_addr) Address = 0; // Error
	else What = ToLawerCase(What);

	if (What.size() <= 0) std::cout << Prefix_Table[Address] << TEXT("insufficient text for message.") << std::endl;
	else std::cout << Prefix_Table[Address] << What << std::endl;
}

namespace Modify_Console {
    extern bool ChangeTitle(std::string Data) {
		std::string Title;

		if(Data.size() <= 0) Title = "insufficient or invalid characters specified.";
		else Title = Data;

		SetConsoleTitle(Data.c_str());

	   return true;
	}
}

namespace System_Tools {
	std::wstring StringToWString(std::string data) {
		std::wstring result(data.length(), L' ');
		std::copy(data.begin(), data.end(), data.begin());
		return result;
	}

	LPWSTR StringToLPWSTR(std::string data) {
		LPWSTR result;
		BSTR conversion;

		conversion = _com_util::ConvertStringToBSTR(data.c_str());
		result = conversion;

		SysFreeString(conversion);

		return result;
	}

	std::string WStringToString(std::wstring data) {
		std::string result(data.begin(), data.end());
		return result;
	}

    char StringToChar(std::string data) {
		char Result;

		return Result;
	}

	wchar_t CharToWCharT(char * data) {
		wchar_t result;

		return result;
	}

	extern bool CheckFile(const char *path) {
		std::ifstream Target(path);
		return (bool)Target;
	}

	extern bool AliveProcess(LPCSTR target, LPCSTR type, int SW) {
		DWORD Status = 0;
		bool Return = false;

		if(sizeof(target) > 0) {
			CoInitializeEx(NULL, COINIT_APARTMENTTHREADED | COINIT_DISABLE_OLE1DDE);
			if(ShellExecute(NULL, type, target, NULL, NULL, SW) > (HINSTANCE)32) Return = true;
		}

		return Status;
	}
	
	std::string GetAllProcesses() {
		std::string FinishingString;
		PROCESSENTRY32 entry;
		entry.dwSize = sizeof(PROCESSENTRY32);

		HANDLE Snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, NULL);

		if(Process32First(Snapshot, &entry)) {
			while(Process32Next(Snapshot, &entry)) {
				if(FinishingString.length() <= 0) FinishingString = entry.szExeFile;
				else FinishingString = FinishingString + "\n" + entry.szExeFile;
			}
		}

		return FinishingString;
	}

	extern bool IsProcessAlive(std::string target) {
		bool Status = false;
		PROCESSENTRY32 entry;
		entry.dwSize = sizeof(PROCESSENTRY32);

		HANDLE Snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, NULL);

		if(Process32First(Snapshot, &entry)) {
			while(Process32Next(Snapshot, &entry)) {
				if(ToLawerCase(entry.szExeFile) == target.c_str()) {
					Status = true;
					break;
				}
			}
		}

		return Status;
	}

	extern bool KillProcess(std::string target) {
		HANDLE Snapshot = CreateToolhelp32Snapshot(TH32CS_SNAPALL, NULL);
		
		PROCESSENTRY32 entry;
		entry.dwSize = sizeof(entry);

		const char *exTarget;
		exTarget = target.c_str();
		bool Status = false, Processor = Process32First(Snapshot, &entry);

		if(IsProcessAlive(target) == true) {
			while(Processor) {
				_bstr_t ExeFile(entry.szExeFile);

				if(strcmp(ExeFile, exTarget) == 0) {
					HANDLE Process = OpenProcess(PROCESS_TERMINATE, 0, (DWORD)entry.th32ProcessID);

					if(Process != NULL)
						if (TerminateProcess(Process, 9) == 0) Status = false;
						else Status = true;

					CloseHandle(Process);
				}

				Processor = Process32Next(Snapshot, &entry);
			}
		} else Status = false;

		CloseHandle(Snapshot);
		return Status;
	}

	std::string ReadFromRegistry(HKEY Where, std::string Key, std::string Sub_Key) {
		std::string Result = "NULL", Holder;
		std::wstring SubKey = StringToWString(Key), Value = StringToWString(Sub_Key);

		WCHAR szBuffer[512];
		DWORD dwBufferSize = sizeof(szBuffer);
		HKEY myKey;

		if(RegOpenKeyExW(Where, StringToLPWSTR(Key.c_str()), 0, KEY_READ, &myKey) == ERROR_SUCCESS) {
			if(RegQueryValueExW(myKey, StringToLPWSTR(Sub_Key.c_str()), 0, NULL, (LPBYTE)szBuffer, &dwBufferSize) == ERROR_SUCCESS) {
				std::wstring ws(szBuffer);
				Result = std::string(ws.begin(), ws.end());
			} else Result = "NULL";
		} else Result = "NULL";

		RegCloseKey(myKey);

		return Result;
	}

	extern bool RemoveRegistryKey(HKEY Where, std::string Key) {
		if(RegDeleteKeyExW(Where, StringToLPWSTR(Key.c_str()), REG_SZ, 0) == ERROR_SUCCESS) {
			return true;
		} else {
			return false;
		}
	}

	extern bool RemoveRegistrySubKey(HKEY Where, std::string Key, std::string SubKey) {
		if(RegDeleteKeyValueW(Where, StringToLPWSTR(Key.c_str()), StringToLPWSTR(SubKey.c_str())) == ERROR_SUCCESS) {
			return true;
		} else {
			return false;
		}
	}

	extern bool WriteToRegistry() {
		return true;
	}
}

#include<psapi.h>

namespace Cookie_Authentication {
	struct Cookie_Authentication_Table {
	  
	}; // ... Yes I know I could simplify this .-.

	Cookie_Authentication_Table cookie_component_table;
}

using namespace System_Tools;

namespace Server_Authentication {
	struct Server_Authentication_Table {
		std::string Server_Path, Server_Directory, Server, 
			        Main_Directory = "Server\\", Main_Executable = "Server\\Dash Server.exe";
	};

	Server_Authentication_Table Set;

	/* For some more authentication and advanced technology add a Client Side and Server Side Cookie Generator and Reader, you know, a cookie toolset. */

	extern bool BootServer() {
		if(AliveProcess("open", Set.Main_Executable.c_str(), SW_HIDE) != true) return false;
		
		bool Status = false;

		do {
			if(ReadFromRegistry(HKEY_LOCAL_MACHINE, "SOFTWARE\\Dash Server\\Message\\", "Data").length() != 0)
				if(RemoveRegistryKey(HKEY_LOCAL_MACHINE, "SOFTWARE\\Dash Server\\") != true) return false;
				    else Status = true;
			
			Sleep(1000);
		} while(Status != true);

		return true;
	}

	std::string Server_Executables[] = {(Set.Main_Executable), (""), ("")};

	extern bool Terminate_Server() {
		for(int index = 0; index < sizeof(Server_Executables) / sizeof(Server_Executables[0]); index = index + 1) {
			if((Server_Executables[index] == "") || (Server_Executables[index].length() <= 0)) return false;

			if(KillProcess(Server_Executables[index]) != true) return false;
		}

		return true;
	}

	extern bool DoesServerExist() {
		Server_Authentication_Table table;

		table.Server = Set.Main_Executable;
		table.Server_Path = Set.Main_Directory;
		table.Server_Directory = Set.Main_Directory;

		 if((CheckFile(table.Server.c_str()) != true) || (CheckFile(table.Server_Path.c_str()) != true) || (CheckFile(table.Server_Path.c_str()) != true)) {
			 DashieSays(2, "We were unable to find some of the required server files. Please make sure that you have access to the files or that you have installed this application correctly, If one or more of these files and folders are actually missing from the installation, Even when you installed this from the original website, Then you could request a valid copy through the mail forms at our website(\"http://www.dashware-software.co.uk\")");
			 return false;
		 } else {
			 DashieSays(1, "Server files are OK!");
			 return true;
		 }
	}

	char space[] = " ",
		id1[] = "0ae3372433e2546d56e9989df4bd005e", // A
		id2[] = "e8e24468fec76074d95329c18b17fe06", // a
		id3[] = "8616647f91efa0f4d2f8f1e31f812e2b", // B
		id4[] = "21abcfe8e706c763e366d0f719682610", // b
		id5[] = "ec2cc55266cd3afb98c1b5eccc584a1e", // C
		id6[] = "07b2042bd3990b08bb7dda82d3af8328", // c
		id7[] = "cba68e5256be0b9c80bdae046002c193", // D
		id8[] = "97a9ae1aeafa4a69e27027d74f0be2ae", // d
		id9[] = "5c6d65f64d54d100f9fab5e18d393218", // E
		id10[] = "83f525387ab947846ca8a0ff9d4444ef", // e
		id11[] = "a1fe71b0e775fd76dbe37069877fa11f", // F
		id12[] = "24573732444a1ecb41adec7d9b2f7f71", // f
		id13[] = "4029d0e8c17b82f126661295371ed6be", // G
		id14[] = "7593270ebafb70bebc1dbafd3907e0fc", // g
		id15[] = "0a365473ebccd45a85827801f6c7b84e", // H
		id16[] = "3d4f21ec3c320286351e4269421cbbcf", // h
		id17[] = "d87455f9e85f246a7cb6a4c084f82567", // I
		id18[] = "8dd8054ddbdca6593456f3a4bb1e095c", // i
		id19[] = "2d19f4d01c558eb19ddd03ff9bf07ace", // J
		id20[] = "3f6d4a5d3b9def36b0a36b7e4cf65d73", // j
		id21[] = "3dd2c81b5c2360d3edf8920f1491b446", // K
		id22[] = "70456c2182aeb810ef3ec6645f75eb77", // k
		id23[] = "f053d2f6853d24b60e89a44e9bae3ab8", // L
		id24[] = "74f23eee384cfb4f326dec1a03efda9b", // l
		id25[] = "9dea74777c5b5f7bca8270c9be61934d", // M
		id26[] = "cd2c8075236f3c93f7be172508d0e3e8", // m
		id27[] = "11ce4cc6bf134780aba8e697b6405a0c", // O
		id28[] = "3d82b8b039a4a0f743f84cdc77169a2c", // o
		id29[] = "78f0ff0a960ae957b38e7b367fb7e77c", // P
		id30[] = "dd04e52c171cee5eb4efe71e2943ffc7", // p
		id31[] = "01b37b1f640b8a2273214104fafe9474", // Q
		id32[] = "3a6e5a602be44bd080b5c232dbd9f4b6", // q
		id33[] = "b69f59ed49a5c421d3727e1c298e6b24", // R
		id34[] = "242d95e6772b831e15da0e1b046c6c79", // r
		id35[] = "b91880ac1eeffa375418259fbd711533", // S
		id36[] = "821659437377050dc35e5ae12afde5ca", // s
		id37[] = "5f6488a4a486c7968b4a701016525952", // T
		id38[] = "60075bdf986cf5c4e968ec43b5f13f5b", // t
		id39[] = "31f4c1b8b3299443b6e138507ce4852a", // U 
		id40[] = "04f16c3a826930705807a362a5ae1e19", // u
		id41[] = "5fe07c4249f7eee102174cab0439c29c", // V
		id42[] = "93073d88883d41fd00af35f7b11c7e20", // v
		id43[] = "65badfdc219d7bbddcfd807660f70b30", // W
		id44[] = "f32ecab08b66e815335b1a994ddc5045", // w
		id45[] = "1d57ad150acdf76c510a1a38bef01420", // X
		id46[] = "d4ac6affb94ece5b6b07019a45072a5e", // x
		id47[] = "3895171d448a90a3d910fc41acaf9baa", // Y
		id48[] = "7f66a8e663fe0a79dccac33a62a45e7c", // y
		id49[] = "2b503bf79c47c1ac4eecc87cf262ef36", // Z
		id50[] = "2a0e436ad1976ed1f12a9a1a5f0ad10a", // z
		id51[] = "9f53ed5e70585949ef2cf3768b82000d", // 1
		id52[] = "11d8f50c86ca12ffbb1080139f9c4d4c", // 2
		id53[] = "318f9348b60054034deb102ac5c0ae38", // 3
		id54[] = "29fb3994222c21da23caf5f592f29fae", // 4
		id55[] = "a34230818fd9dc3e976a4e737cfc1f4c", // 5
		id56[] = "0532d42825c0e0bdace6d71e4f4589ea", // 6
		id57[] = "dc228dcac20a99ce599143236987a03c", // 7
		id58[] = "643a05849acc88bf849d2cb9cbbcc380", // 8
		id59[] = "4d13cbe403c9c5eab515d84e8421f8e3", // 9
		id60[] = "05b36df6e252301135617c76616e64b3", // 0
		id61[] = "7b7b42c7e5a7b8acdaae052317281e8d", // !
		id62[] = "d2beab967551a25a232061a2913daf4c", // @
		id63[] = "158d7ce2cce7f74d21ba8fe214b9697d", // #
		id64[] = "d036910fff79929f1333a01a8b904d81", // $
		id65[] = "aa4e118bd1ba4eba6b485c189242e098", // "
		id66[] = "72368783394e84f912eaeecd1624ff01", // '
		id67[] = "a3d4a0d1f14fb58a18c2971777409b2d", // .
		id68[] = "e527cc0178563ed2ecd97bc036426353", // ,
		id69[] = "29ea83304cce6a3532a8afb680ceb520", // /
		id70[] = "cb15d446cf6821b0b945dd0513caf977", // \ 
		id71[] = "fe5c14a7a9b5bbbeedfc88fb9881b015", // (
		id72[] = "4a6c0d9151ec5454b6cbc3148ead4753", // )
		id73[] = "0ef49d7ed9d615830489a280b6131722", // {
		id74[] = "70de2efd0e156d6fe2ad7e583ff5ee74", // }
		id75[] = "b93d70433c4c41cd42719542c446d555", // [
		id76[] = "c83fdb21dc413ddc294b8f60e00766d5", // ]
		id77[] = "7099a58d17a1046d82e95e8c83cfa09e", // ;
		id78[] = "d05c8853bccc8039c45e3a99d5382f15", // :
		id79[] = "0f3e4268dc1c7c841210404efebcf5a7", // <
		id80[] = "e6e6640672f60bcba7aecaf1af9357e6", // >
		id81[] = "f55a9356e1e1db5e3616126cb235e955", // =
		id82[] = "3982d3e5f3d8f622437120617a32e1d4", // +
		id83[] = "af982f052da6e0398cb02d53e4234f82", // -
		id84[] = "a3724a81629bc2c6fb2684e4ee844ef6", // _
		id85[] = "b07c91aa7f006fc19ba4c8845496a833", // ?
	    id86[] = "3c7cf0bb673e3cce4fbd846f466ff107", // N
		id87[] = "453a0e9c0103e8c0d3767b79d97d720e"; // n

	std::string Valid_ASCII_Table =	"aAbBcCdDeEfFgGhHiIjJkKlLmMoOpPqQrRsStTuUvVwWxXyYzZ1234567890!@#$\"\'.,/\\(){}[];:<>=+-_?nN",
		        Encrypted_Username = "None", Encrypted_Password = "None", Encryption_Storage = "None", Encryption_Holder = "None", 
		        String1 = "1234567890", String2 = "1234567890"; // password & username 

	std::string CheckNum(int Number) {
		std::string Result = "Not Set";

		if(Number < 10) Result = "0" + std::to_string(Number);
		else Result = std::to_string(Number);

		return Result;
	}

	std::string GetId(int id) {
		std::string add = "none";
		
		switch(id) {
		    case 0: { add = id1; break; }
			case 1: { add = id2; break; }
			case 2: { add = id3; break; }
			case 3: { add = id4; break; }
			case 4: { add = id5; break; }
			case 5: { add = id6; break; }
			case 6: { add = id7; break; }
			case 7: { add = id8; break; }
			case 8: { add = id9; break; }
			case 9: { add = id10; break; }

			case 10: { add = id11; break; }
			case 11: { add = id12; break; }
			case 12: { add = id13; break; }
			case 13: { add = id14; break; }
			case 14: { add = id15; break; }
			case 15: { add = id16; break; }
			case 16: { add = id17; break; }
			case 17: { add = id18; break; }
			case 18: { add = id19; break; }
			case 19: { add = id20; break; }

			case 20: { add = id21; break; }
			case 21: { add = id22; break; }
			case 22: { add = id23; break; }
			case 23: { add = id24; break; }
			case 24: { add = id25; break; }
			case 25: { add = id26; break; }
			case 26: { add = id27; break; }
			case 27: { add = id28; break; }
			case 28: { add = id29; break; }
			case 29: { add = id30; break; }

			case 30: { add = id31; break; }
			case 31: { add = id32; break; }
			case 32: { add = id33; break; }
			case 33: { add = id34; break; }
			case 34: { add = id35; break; }
			case 35: { add = id36; break; }
			case 36: { add = id37; break; }
			case 37: { add = id38; break; }
			case 38: { add = id39; break; }
			case 39: { add = id40; break; }

			case 40: { add = id41; break; }
			case 41: { add = id42; break; }
			case 42: { add = id43; break; }
			case 43: { add = id44; break; }
			case 44: { add = id45; break; }
			case 45: { add = id46; break; }
			case 46: { add = id47; break; }
			case 47: { add = id48; break; }
			case 48: { add = id49; break; }
			case 49: { add = id50; break; }

			case 50: { add = id51; break; }
			case 51: { add = id52; break; }
			case 52: { add = id53; break; }
			case 53: { add = id54; break; }
			case 54: { add = id55; break; }
			case 55: { add = id56; break; }
			case 56: { add = id57; break; }
			case 57: { add = id58; break; }
			case 58: { add = id59; break; }
			case 59: { add = id60; break; }

			case 60: { add = id61; break; }
			case 61: { add = id62; break; }
			case 62: { add = id63; break; }
			case 63: { add = id64; break; }
			case 64: { add = id65; break; }
			case 65: { add = id66; break; }
			case 66: { add = id67; break; }
			case 67: { add = id68; break; }
			case 68: { add = id69; break; }
			case 69: { add = id70; break; }

			case 70: { add = id71; break; }
			case 71: { add = id72; break; }
			case 72: { add = id73; break; }
			case 73: { add = id74; break; }
			case 74: { add = id75; break; }
			case 75: { add = id76; break; }
			case 76: { add = id77; break; }
			case 77: { add = id78; break; }
			case 78: { add = id79; break; }
			case 79: { add = id80; break; }

			case 80: { add = id81; break; }
			case 81: { add = id82; break; }
			case 82: { add = id83; break; }
			case 83: { add = id84; break; }
			case 84: { add = id85; break; }
			case 85: { add = id86; break; }
			case 86: { add = id87; break; }
		}

		return add;
	}

	std::string GenerateWall(long int Width, long int Type) {
		std::string Wall = "", Apply = "";

		if((Type > 1) || (Type < 1)) {
			std::cout << Prefix_Table[2] << "unable to create wall because the type applied is invalid, either too big(>= 2) or too small(<= 0).";
			return false;
		}

		switch(Type) {
		    case 1: { Apply = "-";  break; }
		}

		if((Width <= 0) || (Width >= 64)) {
			std::cout << Prefix_Table[2] << "unable to create wall because the width applied is invalid, either too big(>= 65) or too small(<= 0).";
			return false;
		}

		for(int index = 0; index <= Width; index = index + 1)
			if(Wall.length() <= 0) Wall = Apply; 
			else Wall = Wall + Apply;


		if(Wall.length() <= 0)
			std::cout << Prefix_Table[2] << "unable to create wall, most likely because of insufficient building blocks.";

		return Wall;
	}

	extern bool Encrypt(std::string Data, bool Verbose, bool Toggle_Special) {
		std::string add, holder;
		int id = 0000;

		if(Verbose == true) std::cout << GenerateWall(sizeof(id1), 1) << std::endl;

		for(int index = 0; index <= (Data.length())-1; index = index + 1) {
		    for(int sub_index = 0; sub_index <= 87; sub_index = sub_index + 1) {
		        if(Data[index] == Valid_ASCII_Table[sub_index]) {
					id = sub_index;
					continue;
			    }
		    }

			if(id > 87) {
				DashieSays(2, "Encryption Process got interupted because an invalid character was found.");
				return false;
			} else {
				add = GetId(id);

				if(Verbose == true) {
					if(Toggle_Special == true) {
						Sleep(1000);
						std::cout << "identifier[" << CheckNum(index) << "]> " << add << std::endl;
					} else {
						std::cout << "identifier[" << CheckNum(index) << "]> " << add << std::endl;
					}
				}

				if(holder.length() <= 0) holder = add;
				else holder = holder + "-" + add;
			}
		}

		if(Verbose == true) std::cout << GenerateWall(sizeof(id1), 1) << std::endl;

		Encryption_Storage = holder;
		return true;
	}

	extern bool Decrypt(std::string Data) {
		return true;
	}

	extern bool UserIsLoggedIn() { // Not using this anymore because I have found a better way of handling such things.
		if((String1 == "1234567890") || (String2 == "1234567890")) return false;
		else return true;
	}

	extern bool DestroyAuthenticationData() {
		if(Encrypted_Username.length() > sizeof(id1)) Encrypted_Username = "None";
		if(Encrypted_Password.length() > sizeof(id1)) Encrypted_Password = "None";
		
		Encryption_Storage = "None";
		Encryption_Holder = "None";

		String1 = "1234567890";
		String2 = "1234567890";

		if((Encrypted_Username.length() > sizeof(id1)) ||
			(Encrypted_Password.length() > sizeof(id1)) ||
			(Encryption_Storage != "None") ||
			(Encryption_Holder != "None") ||
			(String1 != "1234567890") ||
			(String2 != "1234567890")) {
		
			DashieSays(2, "unable to reset all data, some traces may be left, take this in mind. you could reboot this application in order to fix this.");
			return false;
		}

		// We do not have any generated files yet, I may come back to this in a bit in the near future.

		return true;
	}

	extern bool GenerateCorrectHash(long int Mode) {

		std::string D = "D", a = "a", s = "s", h = "h", i = "i", e = "e", one = "1", two = "2", three = "3", four = "4", five = "5", questionmark = "?";

		Encrypt(D+a+s+h+i+e, false, false);
		String1 = Encryption_Storage;

		Encrypt(D+a+s+h+i+e+one+two+three+four+five+questionmark, false, false);
		String2 = Encryption_Storage;

		return true;
	}

	extern bool CheckCharset(std::string Data) {
		bool Bypass = false;

		  for(int index = 0; index <= Data.length(); index = index + 1) {
		      for(int sub_index = 0; sub_index <= 87; sub_index = sub_index + 1) { 
			      if(Data[index] == Valid_ASCII_Table[sub_index]) {
					  Bypass = true;
					  continue;
				  }
			  }

			  if(Bypass == false) return false;
			  else Bypass = false;
 		  }

		return true;
	}

	extern bool AuthenticateUser(std::string Username, std::string Password, bool Toggle_Special) {
		std::cout << GenerateWall(sizeof(id1), 1) << std::endl;
		DashieSays(1, "Checking given Username and Password ....");

		if(CheckCharset(Username) != true) return false;
		else std::cout << Prefix_Table[1] << "the given format of the given username \"" + Username + "\" is OK!" << std::endl;

		if(CheckCharset(Password) != true) return false;
		else std::cout << Prefix_Table[1] << "the given format of the given password \"" + Password + "\" is OK!" << std::endl;
		
		std::cout << GenerateWall(sizeof(id1), 1) << std::endl;
		DashieSays(1, "Encrypting given Username and Password ....");

		if(Encrypt(Username, true, Toggle_Special) != true) return false;
		else DashieSays(1, "Encrypted Username is OK!");

		Encrypted_Username = Encryption_Storage;

		if(Encrypt(Password, true, Toggle_Special) != true) return false;
		else DashieSays(1, "Encrypted Password is OK!");

		std::cout << GenerateWall(sizeof(id1), 1) << std::endl;
		Encrypted_Password = Encryption_Storage;
		
		GenerateCorrectHash(1);
		Encryption_Holder = String1;

		if(Encryption_Holder != Encrypted_Username) return false;
		else std::cout << Prefix_Table[1] << "the username is OK, now logging in as " + Username + " ...." << std::endl;
		
		GenerateCorrectHash(2);
		Encryption_Holder = String2;

		if(Encryption_Holder != Encrypted_Password) return false;
		else std::cout << Prefix_Table[1] << "the password is OK, You now officialy have successfully logged in as " + Username + "!" << std::endl;

		std::cout << GenerateWall(sizeof(id1), 1) << std::endl;
		return true;
	}
}