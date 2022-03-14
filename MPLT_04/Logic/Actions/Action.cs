namespace MPLT_04.Logic.Actions
{
    delegate void ActionEventHandler(object sender, GraphicalEditor editor);

    abstract class Action
    {
        protected readonly GraphicalEditor editor;
        public string Name { protected set; get; }

        public Action(GraphicalEditor editor, string name)
        {
            this.editor = editor;
            Name = name;
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

        //События
        public event ActionEventHandler OnPreAction = delegate { };
        public event ActionEventHandler OnPostAction = delegate { };
    }
}
