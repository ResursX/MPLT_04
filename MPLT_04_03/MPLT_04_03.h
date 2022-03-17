#pragma once

#include <windows.h>

extern "C" __declspec(dllexport) char* __cdecl ToolName();
extern "C" __declspec(dllexport) int __cdecl ToolSelectable();

extern "C" __declspec(dllexport) void __cdecl ToolSelectAction(unsigned char* bitmap, int width, int height, int bitsPerPixel);
extern "C" __declspec(dllexport) void __cdecl ToolExtraAction(unsigned char* bitmap, int width, int height, int bitsPerPixel);