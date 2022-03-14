using System.Drawing;
using System.Windows.Forms;

namespace MPLT_04.Logic.Actions
{
    class ActionColor : Action
    {
        static ColorDialog colorDialog = new ColorDialog();

        int colorIndex;

        public ActionColor(GraphicalEditor editor, Color initialColor) : base(editor, "Изменить цвет")
        {
            this.colorIndex = editor.AddColorSlot(initialColor);
        }

        public ActionColor(GraphicalEditor editor, int colorSlot) : base(editor, "Изменить цвет")
        {
            this.colorIndex = colorSlot;
        }

        protected override void DoActionInternal()
        {
            colorDialog.Color = editor.GetColorSlotColor(colorIndex);

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                editor.SetColorSlotColor(colorDialog.Color, colorIndex);
            }
        }

        public Image CreateIcon(int width, int height)
        {
            Image img = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(img))
            {
                if (editor.GetSelectedColorSlot() != colorIndex)
                {
                    using (Brush b = new SolidBrush(editor.GetColorSlotColor(colorIndex)))
                    {
                        g.FillRectangle(b, new Rectangle(2, 2, width - 4, height - 4));
                    }

                    using (Pen p = new Pen(Color.Black))
                    {
                        g.DrawRectangle(p, new Rectangle(1, 1, width - 3, height - 3));
                    }
                }
                else
                {
                    g.Clear(editor.GetColorSlotColor(colorIndex));

                    using (Pen p = new Pen(Color.DarkOrchid))
                    {
                        g.DrawRectangle(p, new Rectangle(0, 0, width - 1, height - 1));
                    }
                }
            }

            return img;
        }
    }
}
