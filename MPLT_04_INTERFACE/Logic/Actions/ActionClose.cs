namespace MPLT_04_INTERFACE.Logic.Actions
{
    class ActionClose : Action
    {
        public ActionClose(GraphicalEditor editor) : base(editor, "Закрыть")
        {

        }

        protected override void DoActionInternal()
        {
            editor.ClearImage();
        }
    }
}
