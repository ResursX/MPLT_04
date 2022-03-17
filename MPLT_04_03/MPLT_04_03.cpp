#include "MPLT_04_03.h"

#include "pch.h"
#include <windows.h>

char name[] = "Grayscale";

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
    int bypp = bitsPerPixel / 8;

    int t;

    for (int j = 0; j < height; j++)
    {
        for (int i = 0; i < width; i++)
        {
            t = 0;

            for (int k = 0; k < bypp && k < 3; k++)
            {
                t += bitmap[j * bypp * width + i * bypp + k];
            }

            t /= 3;

            for (int k = 0; k < bypp && k < 3; k++)
            {
                bitmap[j * bypp * width + i * bypp + k] = t;
            }
        }
    }
}

extern "C" __declspec(dllexport) void __cdecl ToolExtraAction(unsigned char* bitmap, int width, int height, int bitsPerPixel)
{
}