
  /* (C) All Rights Reserved, Dashies Beautiful Software Inc. */

#include <windows.h>
#include <mmsystem.h>
#include <Gdiplus.h>
#include <wininet.h>

#include <string.h>
#include <wchar.h>
#include <tchar.h>
#include <conio.h>
#include <lmcons.h>

#include <math.h>
#include <time.h>

#include <iostream>
#include <sstream>
#include <fstream>

#include<string>

#include <algorithm>
#include <vector>
#include <ctime>
#include <map>

#define STRING 17

#define _ARG 7

 using namespace std;
 using namespace Gdiplus;
  
 int unlock=0, tiggle=1, _default=00, cuukie=_default, exit_code, visibility_token;
 char URL[350], c_username[UNLEN+1];

 string arg, _arg, pLoad, pLoad1, pLoad2, pPlay, pPlay1, pPlay2, pPlay3, message, username;
 
 HINTERNET hOpenURL, hInternetURL;
 MCIERROR CheckFile;
 HWND window_handle;
 DWORD c_power;
  
  const char info[720]={
	 "!Version...: V1.0 \n"
	 "!Copyright.: (c) All Rights Reserved, Dashware Software Inc. \n"
	 "!Company...: DASHWARE \n"
	 "!Filename..: Dash Player.exe \n"
	 "!Extension.: .cpp/.h \n"
  };

  const char help[720]={
	 "!Play MP3s> --play-mp3(-p) --file-path(-f) <PATH> --option(-o) <repeat/continue/wait> \n"
	 ">Example> ./Player.exe -p -f C:\\Folder\\myMp3.mp3 -o repeat \n\n"
	 
	 "!Website> --launch(-l) <URL> \n"
	 ">Example> ./Player.exe -l http://www.dashware-software.co.uk/ \n\n"
	 
	 "!Copyright Dialog> --dialog(-d) <ID> \n"
	 ">Example> ./Player.exe -d c1sd \n"
  };
  
  static void DashWipe(int _TYPE, int _NAME){
	  switch(_TYPE){
		  case 17:{
			    switch(_NAME){
				    case 7:{
						 _arg = "";
					   break;
					}
				}
			 break;
		  }
	  }
  }

  static void DashPlay(string pPlay1){
	//cout << pPlay1 << endl;
  	mciSendString(pPlay1.c_str(), NULL, 0, 0);
  }
  
  static void Configurate(){
	 CONSOLE_FONT_INFOEX dashFont;
	 
	  dashFont.cbSize = sizeof(dashFont);
	  dashFont.nFont = 0;
	  dashFont.dwFontSize.X = 0;
	  dashFont.dwFontSize.Y = 12;
	  dashFont.FontFamily = FF_DONTCARE;
	  dashFont.FontWeight = FW_NORMAL;

	   wcscpy(dashFont.FaceName, L"Consolas");

	    SetConsoleTitle((TEXT("(c) All Rights Reserved, Dashware Software Inc.")));
         SetCurrentConsoleFontEx(GetStdHandle(STD_OUTPUT_HANDLE), FALSE, &dashFont);
  }

  int main(int argc, char **argv) {
	 Configurate();
       for(int l=1; l<argc; l=l+1){
		 arg = argv[l];

           switch(unlock){
			   case 1:{
				 case '1':{
					 if((arg == "--file-path") || (arg == "-f")){
					    if(argc<=3){
						     cout << "ERROR:(Insufficient Arguments Received)" << endl;
						   exit(0);
					    }
					    
                       arg = argv[l+1];
                       
                        if(CopyFile(arg.c_str(), "temp.dcore", TRUE) != true){
							 cout << "ERROR:(\"" << arg.c_str() << "\" Could not be found) ;\'(" << endl;
						   exit(0); 
					    } else {
					       remove("temp.dcore");
					      
							pPlay1 = "play \"";
							pPlay2 = "\" ";
							pPlay3 = "";

							pLoad1 = "open \"";
							pLoad2 = "\" type mpegvideo alias DashPlayer";

							pLoad  = pLoad1 + arg + pLoad2;
							// add optional option functionality if argument reaches into memory.
							
							CheckFile = mciSendString(pLoad.c_str(), NULL, 0, 0);

							 if(CheckFile != 0){
								  cout << "ERROR:(Could not Open \"" << arg.c_str() << "\")" << endl;
                                exit(0);
							 }   

                             if(argc<6){
							    cout << "MSG:(No optional Options Detected)" << endl;
								cout << "MSG:(Using wait FLAG Instead)" << endl;
								pPlay3 = "wait";
							 } else {
							  _arg = argv[l+2];
							    if((_arg == "--option") || (_arg == "-o")){
								    pPlay3 = argv[l+3];

								     if((pPlay3 == "wait") || (pPlay3 == "repeat") || (pPlay3 == "continue")){
                                         pPlay3 = pPlay3;
								     } else
									 if(pPlay3 == "game-play-hidden-repeat") {
                                         window_handle    = GetConsoleWindow();
										 visibility_token = SW_HIDE;

									       ShowWindow(window_handle, visibility_token);

										 pPlay3 = "repeat";
									 } else
									 if(pPlay3 == "game-play-hidden-wait"){
                                         window_handle    = GetConsoleWindow();
                                         visibility_token = SW_HIDE;
                                           
                                           ShowWindow(window_handle, visibility_token);

                                         pPlay3 = "wait";
									 } else
									 if(pPlay3 == "game-play-hidden-continue"){
                                         window_handle    = GetConsoleWindow();
										 visibility_token = SW_HIDE;

										   ShowWindow(window_handle, visibility_token);

                                         pPlay3 = "continue";
									 } else {
									      cout << "ERROR:(Invalid Argument Received)" << endl;
                                        exit(0);
								     }
							    }	  
							 }

							pPlay1 = pPlay1 + arg + pPlay2 + pPlay3; 
							
							 remove("temp.dcore");
							
			                 cout << "STATUS:(Started Playing " << arg << ")" << endl;
			                
							 DashPlay(pPlay1.c_str());  
						     getch();

							 cout << "STATUS:(Stopped Playing " << arg << ")" << endl;
						}
					 }
				   break;
				 }
			   }
			   
			   case 2:{
				 case '2':{
                     if(argc<=2){
					     cout << "ERROR:(Insufficient Arguments Received)" << endl;
					    exit(0);
					 }

                    _arg = argv[2];
                    
					 if((_arg != "s") || (_arg != "S")){
					    DashWipe(STRING, _ARG);
						 for(int hi=0; hi<=6; hi=hi+1)
						  _arg = _arg + arg[hi];//cout<<"@Post1> "<<_arg<<endl;
					 } else {
                        DashWipe(STRING, _ARG);
						 for(int hi=0; hi<=7; hi=hi+1)
						  _arg = _arg + arg[hi];//cout<<"@Post1> "<<_arg<<endl;
					 }

					 if(_arg == "http://") goto checkURL;
					    
					_arg = _arg + arg[7];

					 if(_arg == "https://") goto checkURL; 
					 else {
						 cout << "ERROR:(Invalid URL Received)" << endl;
						exit(0);
					 }
					   
					checkURL:
				    
				   strcat(URL, arg.c_str());
				    
					 if(InternetCheckConnection(URL, FLAG_ICC_FORCE_CONNECTION, 0) != false){
					 	  cout << "STATUS:(Executing URL in Default Web Browser)" << endl;
						  cout << "STATUS:(Trying Function 1)" << endl;
							   
						  CoInitializeEx(NULL, COINIT_APARTMENTTHREADED | COINIT_DISABLE_OLE1DDE);
						  ShellExecute(NULL, "open", arg.c_str(), NULL, NULL, SW_SHOW);

						  if(GetLastError() != 0){
                                      cout << "ERROR:(Unable to Launch URL)" << endl;
                              exit(0);
						  } else {
                                     cout << "MSG:(Successfully Executed URL in your Default Web Browser)" << endl;
                                   exit(0);
                                }
					 } else {
						 cout << "ERROR:(Unable to Connect with \"" << arg << "\")" << endl;
                        exit(0);
					 }
				   break;
				 }
			   }
				   
               case 3:{
				 case '3':{
                              if(argc<2){
                                   cout << "ERROR:(Invalid Arguments Received)" << endl;
                                 exit(0);
                              }
                              
                              if(arg == "c1sd"){
                                   c_power = UNLEN+1;
                                    
                                    GetUserName(c_username, &c_power);
                                   
                                   username = c_username; 
                                   message  = "\n\n\t(c) All Rights Reserved, Dashware Software Inc. \n\n\n\n\tWelcome " + username + ", to this beautiful Application :D \n\n\n\tThis Application has been made for Personal purposes only. \n\n\tYou should know that this Application is still in Development \n\tand Could use all the help it can get to be more Optimized \n\tStructured, Functional and Compatible. \n\n\n\n\tIf you were to find an Issue with our Software please \n\tthen contact our Help Desk! \n\n\n\n\tThis Application has been written in C and C++ <3"; 

                                   MessageBox(NULL, message.c_str(), "::: DASHWARE :::", MB_OK | MB_SYSTEMMODAL);
                                  exit(0);
                              } else {
                                   cout << "ERROR:(Invalid Or Unknown Dialog ID Received)" << endl;
                                  exit(0);
                              }
				   break;
			   }
               }
           }			   

			if((arg == "--info") || (arg == "-i")){
			   cout << info;
			  exit(0);
			} else
			if((arg == "--help") || (arg == "-h")) {
               cout << help;
              exit(0);
			} else
			if((arg == "--dialog") || (arg == "-d")){
               unlock=3;
			} else 
			if((arg == "--launch") || (arg == "-l")){
               unlock=2;
			} else
			if((arg == "--play-mp3") || (arg == "-p")){
               unlock=1;
			} else {
				cout << "\'./<Executable>.exe --help\' or \'./<Executable>.exe -h' for valid Arguments." << endl;
			   exit(0);
			}
		}
		
	  cout << "ERROR:(Insufficient Arguments Received)" << endl;
	  
	 exit(0);
  }
