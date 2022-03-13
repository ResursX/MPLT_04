namespace MPLT_04_INTERFACE.Logic.Actions
{
    class ActionCreate : Action
    {
        public ActionCreate(GraphicalEditor editor) : base(editor, "Создать")
        {

        }

        protected override void DoActionInternal()
        {
            editor.CreateImage(400, 300);
        }
    }
}
