using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPLT_04_INTERFACE.Logic.Tools
{
    internal delegate void LoadedToolAction(Bitmap bitmap);

    internal delegate void LoadedToolMouseAction(Bitmap bitmap, MouseEventArgs args);

    class ToolLoaded : Tool, IDisposable
    {
        private IntPtr hLibrary;

        public ToolLoaded(string libName) : base(libName, false)
        {
            hLibrary = LibraryLoader.LoadLibrary(libName);

            if (hLibrary != IntPtr.Zero)
            {

            }
        }

        public void Dispose()
        {
            if (hLibrary != IntPtr.Zero)
            {
                LibraryLoader.FreeLibrary(hLibrary);
            }
        }

        public override void SelectAction(GraphicalEditor editor)
        {
            //((Bitmap)editor.Image).

            throw new NotImplementedException();
        }

        public override void ExtraAction(GraphicalEditor editor)
        {
            throw new NotImplementedException();
        }

        public override void MouseDown(GraphicalEditor editor, MouseEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void MouseUp(GraphicalEditor editor, MouseEventArgs args)
        {
            throw new NotImplementedException();
        }

        public override void MouseMove(GraphicalEditor editor, MouseEventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
