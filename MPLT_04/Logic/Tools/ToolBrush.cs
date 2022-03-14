using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPLT_04.Logic.Tools
{
    class ToolBrush : Tool
    {
        public ToolBrush() : base ("Кисть", true)
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
            if (args.Button == MouseButtons.Left)
            {
                if (editor.Graphics != null)
                {
                    if (editor.BrushSize > 2)
                    {
                        editor.Graphics.FillEllipse(editor.Brush, args.X - editor.BrushSize / 2, args.Y - editor.BrushSize / 2, editor.BrushSize, editor.BrushSize);
                    }
                    else
                    {
                        editor.Graphics.FillRectangle(editor.Brush, args.X - editor.BrushSize / 2, args.Y - editor.BrushSize / 2, editor.BrushSize, editor.BrushSize);
                    }
                }
            }
        }

        public override void MouseUp(GraphicalEditor editor, MouseEventArgs args)
        {
        }
        
        public override void MouseMove(GraphicalEditor editor, MouseEventArgs args)
        {
            if (args.Button == MouseButtons.Left)
            {
                if (editor.Graphics != null && editor.BrushSize > 0)
                {
                    if (editor.BrushSize > 2)
                    {
                        editor.Graphics.FillEllipse(editor.Brush, args.X - editor.BrushSize / 2, args.Y - editor.BrushSize / 2, editor.BrushSize, editor.BrushSize);
                    }
                    else
                    {
                        editor.Graphics.FillRectangle(editor.Brush, args.X - editor.BrushSize / 2, args.Y - editor.BrushSize / 2, editor.BrushSize, editor.BrushSize);
                    }
                }
            }
        }

        public override void Dispose()
        {
        }
    }
}
