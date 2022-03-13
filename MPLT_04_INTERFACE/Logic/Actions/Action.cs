namespace MPLT_04_INTERFACE.Logic.Actions
{
    delegate void ActionEventHandler(object? sender, GraphicalEditor editor);

    abstract class Action
    {
        protected readonly GraphicalEditor editor;
        private readonly string name;

        public Action(GraphicalEditor editor, string name)
        {
            this.editor = editor;
            this.name = name;
        }

        public void DoAction()
        {
            OnPreAction(this, editor);

            try
            {
                DoActionInternal();
            }
            catch
            {
                throw;
            }

            OnPostAction(this, editor);
        }

        abstract protected void DoActionInternal();

        public string Name => name;

        //События
        public event ActionEventHandler OnPreAction = delegate { };
        public event ActionEventHandler OnPostAction = delegate { };
    }
}
