using System;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace MPLT_04_INTERFACE.Logic.Actions
{
    class ActionSave : Action
    {
        private static SaveFileDialog saveDialog = new SaveFileDialog()
        {
            Filter = "Bitmap|*.bmp"
        };

        public ActionSave(GraphicalEditor editor) : base(editor, "Сохранить")
        {
        }

        protected override void DoActionInternal()
        {
            if(editor.Image == null)
            {
                throw new Exception();
            }

            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                editor.Image.Save(saveDialog.FileName, ImageFormat.Bmp);
            }
        }
    }
}
