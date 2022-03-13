using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPLT_04_INTERFACE.Logic.Tools
{
    delegate void ToolEventHandler(object? sender, Tool tool, GraphicalEditor editor);

    //enum ToolType {
    //    
    //}

    abstract class Tool
    {
        public string Name { protected set; get; }
        public bool Selectable { protected set; get; }

        public Tool(string name, bool selectable)
        {
            Name = name;
            Selectable = selectable;
        }

        abstract public void SelectAction(GraphicalEditor editor);
        abstract public void ExtraAction(GraphicalEditor editor);

        abstract public void MouseDown(GraphicalEditor editor, MouseEventArgs args);
        abstract public void MouseUp(GraphicalEditor editor, MouseEventArgs args);

        abstract public void MouseMove(GraphicalEditor editor, MouseEventArgs args);
    }
}
