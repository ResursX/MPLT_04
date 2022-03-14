#include "MPLT_04_03.h"

#include <windows.h>

char name[] = "Grayscale";

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


    //bitmap.RotateFlip(RotateNoneFlipX);
}

extern "C" __declspec(dllexport) void __cdecl ToolExtraAction(HBITMAP bitmap, HDC hdc)
{
}