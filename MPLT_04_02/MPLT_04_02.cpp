#include "MPLT_04_02.h"

#include "pch.h"
#include <tchar.h>
#include <windows.h>

#include <string>
#include <sstream>

#include <objidl.h>
#include <gdiplus.h>
using namespace Gdiplus;
#pragma comment (lib,"Gdiplus.lib")

char name[] = "Mirror (Horizontal/Vertical)";

extern "C" __declspec(dllexport) char* __cdecl ToolName()
{
    return name;
}
extern "C" __declspec(dllexport) int __cdecl ToolSelectable()
{
    return 0;
}

extern "C" __declspec(dllexport) void __cdecl ToolSelectAction(unsigned char* bitmap, int width, int height, int bitsPerPixel)
{
    //std::stringstream ss;
    //ss << width << "x" << height;
    //ss << std::endl << bitsPerPixel;
    //MessageBoxA(NULL, ss.str().c_str(), "LOL", MB_OK);

    int bypp = bitsPerPixel / 8;

    unsigned char t;

    for (int j = 0; j < height; j++)
    {
        for (int i = 0; i < width / 2; i++)
        {
            for (int k = 0; k < bypp; k++)
            {
                t = bitmap[j * bypp * width + i * bypp + k];
                bitmap[j * bypp * width + i * bypp + k] = bitmap[(j + 1) * bypp * width - (i + 1) * bypp + k];
                bitmap[(j + 1) * bypp * width - (i + 1) * bypp + k] = t;
            }
        }
    }
}

extern "C" __declspec(dllexport) void __cdecl ToolExtraAction(unsigned char* bitmap, int width, int height, int bitsPerPixel)
{
    int bypp = bitsPerPixel / 8;

    unsigned char t;

    for (int j = 0; j < height / 2; j++)
    {
        for (int i = 0; i < width; i++)
        {
            for (int k = 0; k < bypp; k++)
            {
                t = bitmap[j * bypp * width + i * bypp + k];
                bitmap[j * bypp * width + i * bypp + k] = bitmap[(height - j - 1) * bypp * width + i * bypp + k];
                bitmap[(height - j - 1) * bypp * width + i * bypp + k] = t;
            }
        }
    }
}