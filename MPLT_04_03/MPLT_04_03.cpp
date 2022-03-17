#include "MPLT_04_03.h"

#include "pch.h"
#include <windows.h>

char name[] = "Grayscale";

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


    //bitmap.RotateFlip(RotateNoneFlipX);
}

extern "C" __declspec(dllexport) void __cdecl ToolExtraAction(HBITMAP bitmap, HDC hdc)
{
}