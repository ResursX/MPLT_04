using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPLT_04.Logic.Tools
{
    class ToolLine : Tool
    {
        bool active = false;
        int startX, startY;

        public ToolLine() : base("Линия", true)
        {

        }

        public override void SelectAction(GraphicalEditor editor)
        {
        }

        public override void ExtraAction(GraphicalEditor editor)
        {
        }

        public override void MouseDown(GraphicalEditor editor, MouseEventArgs args)
        {
            if (!active)
            {
                active = true;
                startX = args.X;
                startY = args.Y;
            }
        }

        public override void MouseUp(GraphicalEditor editor, MouseEventArgs args)
        {
            if (active)
            {
                active = false;

                if (editor.PreviewGraphics != null)
                {
                    editor.PreviewGraphics.Clear(Color.Transparent);
                }

                if (editor.Graphics != null)
                {
                    editor.Graphics.DrawLine(editor.Pen, startX, startY, args.X, args.Y);
                }
            }
        }

        public override void MouseMove(GraphicalEditor editor, MouseEventArgs args)
        {
            if (active)
            {
                if (editor.PreviewGraphics != null)
                {
                    editor.PreviewGraphics.Clear(Color.Transparent);
                    editor.PreviewGraphics.DrawLine(editor.Pen, startX, startY, args.X, args.Y);
                }
            }
        }

        public override void Dispose()
        {
        }
    }
}
