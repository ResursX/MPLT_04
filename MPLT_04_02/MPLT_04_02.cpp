#include "MPLT_04_02.h"

#include <windows.h>
#include <gdiplus.h>

char name[] = "�������������� ����������� �� �����������";

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

void ToolMouseDown(Gdiplus::Bitmap bitmap)
{
}

void ToolMouseUp(Gdiplus::Bitmap bitmap)
{
}

void ToolMouseMove(Gdiplus::Bitmap bitmap)
{
}
