using System.Drawing;
using System.Windows.Forms;

namespace MPLT_04.Logic.Actions
{
    class ActionOpen : Action
    {
        private static OpenFileDialog openDialog = new OpenFileDialog()
        {
            Filter = "Bitmap|*.bmp|All Files|*.*"
        };

        public ActionOpen(GraphicalEditor editor) : base(editor, "Открыть")
        {

        }

        protected override void DoActionInternal()
        {
            if(openDialog.ShowDialog() == DialogResult.OK)
            {
                // Load
                editor.LoadImage(Image.FromFile(openDialog.FileName));
            }
        }
    }
}
