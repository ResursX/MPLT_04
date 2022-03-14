namespace MPLT_04.Logic.Actions
{
    class ActionColorCycle : Action
    {
        public ActionColorCycle(GraphicalEditor editor) : base(editor, "Переключить текущий цвет")
        {
            
        }

        protected override void DoActionInternal()
        {
            editor.CycleColorSlotSelection();
        }
    }
}
