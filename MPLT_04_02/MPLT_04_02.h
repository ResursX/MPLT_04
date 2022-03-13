#pragma once

#include <windows.h>
#include <objidl.h>
#include <gdiplus.h>
using namespace Gdiplus;
#pragma comment (lib,"Gdiplus.lib")

char* ToolName();
bool ToolSelectable();

void ToolSelectAction(Bitmap);
void ToolExtraAction(Bitmap);