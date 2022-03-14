using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPLT_04.Logic.Actions
{
    class ActionBrushSizeAdd : Action
    {
        int add;

        public ActionBrushSizeAdd(GraphicalEditor editor, int add) : base(editor, "Изменить на " + add)
        {
            this.add = add;
        }

        protected override void DoActionInternal()
        {
            editor.BrushSizeAdd(add);
        }
    }
}
