#include "MPLT_04_02.h"

#include "pch.h"
#include <tchar.h>
#include <windows.h>
#include <objidl.h>
#include <gdiplus.h>
using namespace Gdiplus;
#pragma comment (lib,"Gdiplus.lib")

char name[] = "«еркалирование изображени€ по горизонтали";

extern "C" __declspec(dllexport) char* __stdcall ToolName()
{
    return name;
}
extern "C" __declspec(dllexport) bool __stdcall ToolSelectable()
{
    return false;
}

extern "C" __declspec(dllexport) void __stdcall ToolSelectAction(HBITMAP bitmap, HDC hdc)
{
    //HBITMAP hBitmap = bitmap->GetHBITMAP();
    //HDC hDC = graphics->GetHDC();

    SelectObject(hdc, CreatePen(PS_SOLID, 3, RGB(0, 128, 0)));
    SelectObject(hdc, CreateSolidBrush(RGB(255, 0, 0)));

    Ellipse(hdc, (DWORD)(0), (DWORD)(0), (DWORD)(100), (DWORD)(100));

    //graphics->Clear(Color(255, 0, 0));

    //graphics->DrawLine(new Pen(Color(255, 0, 0)), 0, 0, 100, 100);

    MessageBox(NULL, _T("Settings load failure: frame rate can take values from 1 to 1000."), NULL, MB_ICONERROR | MB_OK);

    //Gdiplus::Bitmap btm;

    //btm.FromHBITMAP(bitmap);

    //(*bitmap).SetPixel(10, 10, Color(255, 0, 0));
}

extern "C" __declspec(dllexport) void __stdcall ToolExtraAction(HBITMAP bitmap, HDC hdc)
{
}
