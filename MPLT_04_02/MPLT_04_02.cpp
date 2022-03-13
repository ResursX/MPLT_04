#include "MPLT_04_02.h"

#include "pch.h"
#include <windows.h>
#include <objidl.h>
#include <gdiplus.h>
using namespace Gdiplus;
#pragma comment (lib,"Gdiplus.lib")

char name[] = "«еркалирование изображени€ по горизонтали";

char* ToolName()
{
    return name;
}

bool ToolSelectable()
{
    return false;
}

void ToolSelectAction(Bitmap bitmap)
{
    bitmap.RotateFlip(RotateNoneFlipX);
}

void ToolExtraAction(Bitmap bitmap)
{
}
