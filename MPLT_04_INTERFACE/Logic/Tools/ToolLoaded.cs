using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPLT_04_INTERFACE.Logic.Tools
{
    internal delegate void LoadedToolAction(Bitmap bitmap);

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
                    Debug.WriteLine(hLibrary);

                    //Debug.WriteLine(LibraryLoader.GetProcAddress(hLibrary, "ToolName"));

                    //Name = Marshal.PtrToStringAnsi(Marshal.GetDelegateForFunctionPointer<Func<IntPtr>>(LibraryLoader.GetProcAddress(hLibrary, "ToolName"))());
                    //Selectable = Marshal.GetDelegateForFunctionPointer<Func<bool>>(LibraryLoader.GetProcAddress(hLibrary, "ToolSelectable"))();

                    IntPtr hProc = LibraryLoader.GetProcAddress(hLibrary, "ToolSelectAction");

                    Debug.WriteLine("hp " + hProc);

                    SelectDelegate = Marshal.GetDelegateForFunctionPointer<LoadedToolAction>(hProc);

                    hProc = LibraryLoader.GetProcAddress(hLibrary, "ToolExtraAction");

                    Debug.WriteLine("hp " + hProc);

                    ExtraDelegate = Marshal.GetDelegateForFunctionPointer<LoadedToolAction>(hProc);

                    //MouseDownDelegate = Marshal.GetDelegateForFunctionPointer<LoadedToolMouseAction>(LibraryLoader.GetProcAddress(hLibrary, "ToolMouseDown"));
                    //MouseUpDelegate = Marshal.GetDelegateForFunctionPointer<LoadedToolMouseAction>(LibraryLoader.GetProcAddress(hLibrary, "ToolMouseUp"));
                    //MouseMoveDelegate = Marshal.GetDelegateForFunctionPointer<LoadedToolMouseAction>(LibraryLoader.GetProcAddress(hLibrary, "ToolMouseMove"));
                }
                catch
                {
                    Dispose();

                    throw;
                }
            }
            else
            {
                throw new Exception("A");
            }
        }

        public override void SelectAction(GraphicalEditor editor)
        {
            SelectDelegate(editor.Image);
        }

        public override void ExtraAction(GraphicalEditor editor)
        {
            ExtraDelegate(editor.Image);
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
