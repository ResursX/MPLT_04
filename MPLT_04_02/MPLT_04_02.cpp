#include "MPLT_04_02.h"

#include "pch.h"
#include <tchar.h>
#include <windows.h>
#include <objidl.h>
#include <gdiplus.h>
using namespace Gdiplus;
#pragma comment (lib,"Gdiplus.lib")

char name[] = "«еркалирование изображени€ по горизонтали";

extern "C" __declspec(dllexport) char* __stdcall ToolName()
{
    return name;
}
extern "C" __declspec(dllexport) bool __stdcall ToolSelectable()
{
    return false;
}

extern "C" __declspec(dllexport) void __stdcall ToolSelectAction(HBITMAP bitmap, HDC hdc)
{

}

extern "C" __declspec(dllexport) void __stdcall ToolExtraAction(HBITMAP bitmap, HDC hdc)
{
}
