namespace MPLT_04.Logic.Actions
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
