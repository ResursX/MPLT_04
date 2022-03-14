#pragma once

#include <windows.h>

extern "C" __declspec(dllexport) char* __cdecl ToolName();
extern "C" __declspec(dllexport) bool __cdecl ToolSelectable();

extern "C" __declspec(dllexport) void __cdecl ToolSelectAction(HBITMAP bitmap, HDC hdc);
extern "C" __declspec(dllexport) void __cdecl ToolExtraAction(HBITMAP bitmap, HDC hdc);