using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPLT_04.Logic.Tools
{
    internal delegate IntPtr LoadedToolName();
    internal delegate bool LoadedToolSelectable();

    internal delegate void LoadedToolAction(IntPtr bitmap, IntPtr hdc);

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
                    Debug.WriteLine(LibraryLoader.GetProcAddress(hLibrary, "ToolName"));
                    Debug.WriteLine(LibraryLoader.GetProcAddress(hLibrary, "ToolSelectable"));
                    Debug.WriteLine(LibraryLoader.GetProcAddress(hLibrary, "ToolSelectAction"));
                    Debug.WriteLine(LibraryLoader.GetProcAddress(hLibrary, "ToolExtraAction"));

                    //Debug.WriteLine(Marshal.GetDelegateForFunctionPointer<Func<IntPtr>>(LibraryLoader.GetProcAddress(hLibrary, "ToolName"))());

                    Name = Marshal.PtrToStringAnsi(Marshal.GetDelegateForFunctionPointer<LoadedToolName> (LibraryLoader.GetProcAddress(hLibrary, "ToolName"))());

                    Selectable = Marshal.GetDelegateForFunctionPointer<LoadedToolSelectable>(LibraryLoader.GetProcAddress(hLibrary, "ToolSelectable"))();

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
                throw new Exception();
            }
        }

        public override void SelectAction(GraphicalEditor editor)
        {
            if (SelectDelegate != null && editor != null && editor.Image != null)
            {
                SelectDelegate(editor.Image.GetHbitmap(), editor.Graphics.GetHdc());
            }
        }

        public override void ExtraAction(GraphicalEditor editor)
        {
            if (ExtraDelegate != null && editor != null && editor.Image != null)
            {
                ExtraDelegate(editor.Image.GetHbitmap(), editor.Graphics.GetHdc());
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
