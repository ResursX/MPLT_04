#pragma once

#include <windows.h>
#include <objidl.h>
#include <gdiplus.h>
using namespace Gdiplus;
#pragma comment (lib,"Gdiplus.lib")

extern "C" __declspec(dllexport) char* __stdcall ToolName();
extern "C" __declspec(dllexport) bool __stdcall ToolSelectable();

extern "C" __declspec(dllexport) void __stdcall ToolSelectAction(Bitmap bitmap);
extern "C" __declspec(dllexport) void __stdcall ToolExtraAction(Bitmap bitmap);