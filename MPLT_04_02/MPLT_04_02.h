#pragma once

#include <windows.h>
#include <gdiplus.h>

char* ToolName();
bool ToolSelectable();

void ToolSelectAction(Gdiplus::Bitmap);
void ToolExtraAction(Gdiplus::Bitmap);
void ToolMouseDown(Gdiplus::Bitmap);
void ToolMouseUp(Gdiplus::Bitmap);
void ToolMouseMove(Gdiplus::Bitmap);
