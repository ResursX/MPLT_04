#include "MPLT_04_03.h"

#include "pch.h"
#include <windows.h>
#include <objidl.h>
#include <gdiplus.h>
using namespace Gdiplus;
#pragma comment (lib,"Gdiplus.lib")

char name[] = "Обесцвечивание изображения";

extern "C" __declspec(dllexport) char* __stdcall ToolName()
{
    return name;
}
extern "C" __declspec(dllexport) bool __stdcall ToolSelectable()
{
    return false;
}

extern "C" __declspec(dllexport) void __stdcall ToolSelectAction(Bitmap bitmap)
{
    bitmap.RotateFlip(RotateNoneFlipX);
}

extern "C" __declspec(dllexport) void __stdcall ToolExtraAction(Bitmap bitmap)
{
}