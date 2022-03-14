#include "MPLT_04_02.h"

#include <tchar.h>
#include <windows.h>

char name[] = "Mirror (Horizontal)";

extern "C" __declspec(dllexport) char* __cdecl ToolName()
{
    return name;
}
extern "C" __declspec(dllexport) bool __cdecl ToolSelectable()
{
    return false;
}

extern "C" __declspec(dllexport) void __cdecl ToolSelectAction(HBITMAP bitmap, HDC hdc)
{
    BITMAP bt{};

    GetObject(bitmap, sizeof(BITMAP), (LPSTR)&bt);

    HDC cdc = CreateCompatibleDC(hdc);
    HBITMAP cbm = CreateCompatibleBitmap(cdc, bt.bmWidth, bt.bmHeight);

    BitBlt(cdc, 0, 0, bt.bmWidth, bt.bmHeight, hdc, 0, 0, SRCCOPY);
    StretchBlt(hdc, 0, 0, bt.bmWidth, bt.bmHeight, cdc, 0, 0, -bt.bmWidth, bt.bmHeight, SRCCOPY);

    DeleteDC(cdc);
}

extern "C" __declspec(dllexport) void __cdecl ToolExtraAction(HBITMAP bitmap, HDC hdc)
{
}