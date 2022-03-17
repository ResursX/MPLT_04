using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPLT_04.Logic.Tools
{
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate IntPtr LoadedToolName();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int LoadedToolSelectable();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //internal delegate void LoadedToolAction(ref Bitmap bitmap, ref Graphics graphics);
    internal delegate void LoadedToolAction(byte[] bitmap, int width, int height, int bitsPerPixel); //IntPtr hdc, int width, int height);

    //internal delegate void LoadedToolMouseAction(Bitmap bitmap, int X, int Y);

    class ToolLoaded : Tool, IDisposable
    {
        private IntPtr hLibrary;

        private LoadedToolAction SelectDelegate;
        private LoadedToolAction ExtraDelegate;

        //private LoadedToolMouseAction MouseDownDelegate;
        //private LoadedToolMouseAction MouseUpDelegate;
        //private LoadedToolMouseAction MouseMoveDelegate;

        public ToolLoaded(string libName) : base(libName, false)
        {
            hLibrary = LibraryLoader.LoadLibrary(libName);

            if (hLibrary != IntPtr.Zero)
            {
                try
                {
                    //Debug.WriteLine(LibraryLoader.GetProcAddress(hLibrary, "ToolName"));
                    //Debug.WriteLine(LibraryLoader.GetProcAddress(hLibrary, "ToolSelectable"));
                    //Debug.WriteLine(LibraryLoader.GetProcAddress(hLibrary, "ToolSelectAction"));
                    //Debug.WriteLine(LibraryLoader.GetProcAddress(hLibrary, "ToolExtraAction"));

                    Selectable = Marshal.GetDelegateForFunctionPointer<LoadedToolSelectable>(LibraryLoader.GetProcAddress(hLibrary, "ToolSelectable"))() > 0;

                    Name = Marshal.PtrToStringAnsi(Marshal.GetDelegateForFunctionPointer<LoadedToolName>(LibraryLoader.GetProcAddress(hLibrary, "ToolName"))());

                    SelectDelegate = Marshal.GetDelegateForFunctionPointer<LoadedToolAction>(LibraryLoader.GetProcAddress(hLibrary, "ToolSelectAction"));
                    ExtraDelegate = Marshal.GetDelegateForFunctionPointer<LoadedToolAction>(LibraryLoader.GetProcAddress(hLibrary, "ToolExtraAction"));

                    //MouseDownDelegate = Marshal.GetDelegateForFunctionPointer<LoadedToolMouseAction>(LibraryLoader.GetProcAddress(hLibrary, "ToolMouseDown"));
                    //MouseUpDelegate = Marshal.GetDelegateForFunctionPointer<LoadedToolMouseAction>(LibraryLoader.GetProcAddress(hLibrary, "ToolMouseUp"));
                    //MouseMoveDelegate = Marshal.GetDelegateForFunctionPointer<LoadedToolMouseAction>(LibraryLoader.GetProcAddress(hLibrary, "ToolMouseMove"));
                }
                catch
                {
                    Dispose();

                    throw new Exception(Marshal.GetLastWin32Error().ToString());
                }
            }
            else
            {
                throw new Exception("Can't load");
            }
        }

        public override void SelectAction(GraphicalEditor editor)
        {
            if (SelectDelegate != null && editor != null && editor.Image != null)
            {
                BitmapData bmpd = editor.Image.LockBits(new Rectangle(0, 0, editor.Image.Width, editor.Image.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, editor.Image.PixelFormat);

                IntPtr ptr = bmpd.Scan0;

                int bytes = Math.Abs(bmpd.Stride) * bmpd.Height;
                byte[] rgbValues = new byte[bytes];

                Marshal.Copy(ptr, rgbValues, 0, bytes);

                SelectDelegate(rgbValues, editor.Image.Width, editor.Image.Height, Image.GetPixelFormatSize(bmpd.PixelFormat));

                Marshal.Copy(rgbValues, 0, ptr, bytes);

                editor.Image.UnlockBits(bmpd);
            }
        }

        public override void ExtraAction(GraphicalEditor editor)
        {
            if (ExtraDelegate != null && editor != null && editor.Image != null)
            {
                BitmapData bmpd = editor.Image.LockBits(new Rectangle(0, 0, editor.Image.Width, editor.Image.Height), System.Drawing.Imaging.ImageLockMode.ReadWrite, editor.Image.PixelFormat);

                IntPtr ptr = bmpd.Scan0;

                int bytes = Math.Abs(bmpd.Stride) * bmpd.Height;
                byte[] rgbValues = new byte[bytes];

                Marshal.Copy(ptr, rgbValues, 0, bytes);

                ExtraDelegate(rgbValues, editor.Image.Width, editor.Image.Height, Image.GetPixelFormatSize(bmpd.PixelFormat));

                Marshal.Copy(rgbValues, 0, ptr, bytes);

                editor.Image.UnlockBits(bmpd);
            }
        }

        public override void MouseDown(GraphicalEditor editor, MouseEventArgs args)
        {
            //MouseDownDelegate(editor.Image, args.X, args.Y);
        }

        public override void MouseUp(GraphicalEditor editor, MouseEventArgs args)
        {
            //MouseUpDelegate(editor.Image, args.X, args.Y);
        }

        public override void MouseMove(GraphicalEditor editor, MouseEventArgs args)
        {
            //MouseMoveDelegate(editor.Image, args.X, args.Y);
        }

        public override void Dispose()
        {
            if (hLibrary != IntPtr.Zero)
            {
                LibraryLoader.FreeLibrary(hLibrary);
            }
        }
    }
}
