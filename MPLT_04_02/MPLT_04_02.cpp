#include "MPLT_04_02.h"

#include <windows.h>
#include <gdiplus.h>

char name[] = "«еркалирование изображени€ по горизонтали";

char* ToolName()
{
    return name;
}

bool ToolSelectable()
{
    return false;
}

void ToolSelectAction(Gdiplus::Bitmap bitmap)
{
    bitmap.RotateFlip(Gdiplus::RotateNoneFlipX);
}

void ToolExtraAction(Gdiplus::Bitmap bitmap)
{
}

//void ToolMouseDown(Gdiplus::Bitmap bitmap, int X, int Y)
//{
//}
//
//void ToolMouseUp(Gdiplus::Bitmap bitmap, int X, int Y)
//{
//}
//
//void ToolMouseMove(Gdiplus::Bitmap bitmap, int X, int Y)
//{
//}
