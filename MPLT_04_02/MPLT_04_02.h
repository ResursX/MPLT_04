#pragma once

#include <windows.h>
#include <gdiplus.h>

char* ToolName();
bool ToolSelectable();

void ToolSelectAction(Gdiplus::Bitmap);
void ToolExtraAction(Gdiplus::Bitmap);
//void ToolMouseDown(Gdiplus::Bitmap, int, int);
//void ToolMouseUp(Gdiplus::Bitmap, int, int);
//void ToolMouseMove(Gdiplus::Bitmap, int, int);
