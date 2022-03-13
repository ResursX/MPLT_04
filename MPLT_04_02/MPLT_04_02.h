#pragma once

#include <windows.h>
#include <objidl.h>
#include <gdiplus.h>
using namespace Gdiplus;
#pragma comment (lib,"Gdiplus.lib")

extern "C" __declspec(dllexport) char* ToolName();
extern "C" __declspec(dllexport) bool ToolSelectable();

extern "C" __declspec(dllexport) void ToolSelectAction(Bitmap bitmap);
extern "C" __declspec(dllexport) void ToolExtraAction(Bitmap bitmap);