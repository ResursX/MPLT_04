#include "MPLT_04_02.h"

#include "pch.h"
#include <tchar.h>
#include <windows.h>
#include <string>

char name[] = "Mirror (Horizontal)";

extern "C" __declspec(dllexport) char* __cdecl ToolName()
{
    return name;
}
extern "C" __declspec(dllexport) int __cdecl ToolSelectable()
{
    return 0;
}

extern "C" __declspec(dllexport) void __cdecl ToolSelectAction(HBITMAP bitmap, HDC hdc)
{
    BITMAP bm, cbm;

    GetObject(bitmap, sizeof(BITMAP), (LPSTR)&bm);

    std::string s = std::to_string(bm.bmWidth) + "x" + std::to_string(bm.bmHeight);
    MessageBoxA(NULL, s.c_str(), "LOL", MB_OK);

    HDC cdc = CreateCompatibleDC(hdc);
    HBITMAP hcbm = CreateCompatibleBitmap(hdc, bm.bmWidth, bm.bmHeight);
    
    SelectObject(cdc, hcbm);
    
    SelectObject(cdc, CreateSolidBrush(RGB(255, 0, 0)));
    SelectObject(cdc, CreatePen(PS_SOLID, 3, RGB(0, 255, 0)));
    
    //BitBlt(cdc, 0, 0, bm.bmWidth, bm.bmHeight, hdc, 0, 0, SRCCOPY);

    StretchBlt(cdc, 0, 0, bm.bmWidth, bm.bmHeight, hdc, 0, 0, bm.bmWidth, bm.bmHeight, SRCCOPY);

    //Fill(cdc, 0, 0, bm.bmWidth, bm.bmHeight);
    Ellipse(cdc, 10, 10, 100, 100);

    StretchBlt(hdc, bm.bmWidth, 0, -bm.bmWidth, bm.bmHeight, cdc, 0, 0, bm.bmWidth, bm.bmHeight, SRCCOPY);

    DeleteDC(cdc);
    DeleteObject(hcbm);
}

extern "C" __declspec(dllexport) void __cdecl ToolExtraAction(HBITMAP bitmap, HDC hdc)
{
}