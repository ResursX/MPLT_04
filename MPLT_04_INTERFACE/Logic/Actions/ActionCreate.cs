using MPLT_04_INTERFACE.Logic.Actions.ActionCreateLogic;
using System.Windows.Forms;

namespace MPLT_04_INTERFACE.Logic.Actions
{
    class ActionCreate : Action
    {
        private static ImageCreationForm imageCreationForm = new ImageCreationForm();

        public ActionCreate(GraphicalEditor editor) : base(editor, "Создать")
        {

        }

        protected override void DoActionInternal()
        {
            imageCreationForm.Reset();

            if (imageCreationForm.ShowDialog() == DialogResult.OK)
            {
                editor.CreateImage(imageCreationForm.GetWidth(), imageCreationForm.GetHeight());
            }
        }
    }
}
