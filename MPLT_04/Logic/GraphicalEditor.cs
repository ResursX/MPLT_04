using MPLT_04.Logic.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPLT_04.Logic
{
    delegate void GraphicalEditorEventHandler(GraphicalEditor editor);

    class GraphicalEditor : IDisposable
    {
        public Bitmap Image { private set; get; }
        public Graphics Graphics { private set; get; }

        public Bitmap PreviewImage { private set; get; }
        public Graphics PreviewGraphics { private set; get; }

        public Pen Pen { private set; get; }
        public SolidBrush Brush { private set; get; }

        private Tool selectedTool;

        private readonly List<Color> ColorSlots;
        private int selectedColorSlot;

        private int brushSize;

        public GraphicalEditor(PictureBox control)
        {
            if (control.Image != null)
            {
                Image = new Bitmap(control.Image);
                PreviewImage = new Bitmap(Image.Width, Image.Height);
                control.Image.Dispose();
                control.Image = Image;
            }

            OnImageChange += (editor) => {
                control.Image = editor.Image;
            };

            OnImageUpdate += (editor) => {
                control.Invalidate();
            };

            control.Paint += (_, e) =>
            {
                if (PreviewImage != null)
                {
                    e.Graphics.DrawImage(PreviewImage, 0, 0);
                }
            };

            control.MouseDown += (_, e) =>
            {
                MouseDown(e);
            };

            control.MouseUp += (_, e) =>
            {
                MouseUp(e);
            };

            control.MouseMove += (_, e) =>
            {
                MouseMove(e);
            };

            if (Image != null)
            {
                Graphics = Graphics.FromImage(Image);
            }

            if (PreviewImage != null)
            {
                PreviewGraphics = Graphics.FromImage(Image);
            }

            Pen = new Pen(Color.White);
            Pen.Width = 1;

            Brush = new SolidBrush(Color.White);

            ColorSlots = new List<Color>();
            selectedColorSlot = 0;

            brushSize = 1;
        }

        public int BrushSize => brushSize;

        // Работа с цветами

        public Color GetSelectedColor()
        {
            return ColorSlots[selectedColorSlot];
        }

        public void SetSelectedColor(Color color)
        {
            ColorSlots[selectedColorSlot] = color;
        }

        public int GetSelectedColorSlot()
        {
            return selectedColorSlot;
        }

        public void SetSelectedColorSlot(int index)
        {
            selectedColorSlot = index;

            Pen.Color = ColorSlots[selectedColorSlot];
            Brush.Color = ColorSlots[selectedColorSlot];
        }

        public void CycleColorSlotSelection()
        {
            if (ColorSlots.Count > 0)
            {
                selectedColorSlot = (selectedColorSlot + 1) % ColorSlots.Count;

                Pen.Color = ColorSlots[selectedColorSlot];
                Brush.Color = ColorSlots[selectedColorSlot];
            }
        }

        public Color GetColorSlotColor(int index)
        {
            return ColorSlots[index];
        }

        public void SetColorSlotColor(Color color, int index)
        {
            ColorSlots[index] = color;

            if (index == selectedColorSlot)
            {
                Pen.Color = ColorSlots[index];
                Brush.Color = ColorSlots[index];
            }
        }

        public int AddColorSlot(Color initialColor)
        {
            ColorSlots.Add(initialColor);

            if (ColorSlots.Count - 1 == selectedColorSlot)
            {
                Pen.Color = ColorSlots[selectedColorSlot];
                Brush.Color = ColorSlots[selectedColorSlot];
            }

            return ColorSlots.Count - 1;
        }

        // Работа с кистью

        public void SetBrushSize(int size)
        {
            Pen.Width = size;

            brushSize = size;

            OnBrushSizeChange(this);
        }

        public void BrushSizeAdd(int add)
        {
            if (add >= 0 || brushSize + add > 0)
            {
                Pen.Width += add;

                brushSize += add;

                OnBrushSizeChange(this);
            }
        }

        // Работа с изображением

        public void CreateImage(int width, int height)
        {
            if (Image != null)
            {
                Image.Dispose();
            }

            Image = new Bitmap(width, height);

            if (Graphics != null)
            {
                Graphics.Dispose();
            }
            
            Graphics = Graphics.FromImage(Image);
            Graphics.Clear(Color.White);

            if (PreviewImage != null)
            {
                PreviewImage.Dispose();
            }

            PreviewImage = new Bitmap(width, height);

            if (PreviewGraphics != null)
            {
                PreviewGraphics.Dispose();
            }

            PreviewGraphics = Graphics.FromImage(PreviewImage);
            PreviewGraphics.Clear(Color.Transparent);

            OnImageChange(this);
        }

        public void LoadImage(Image image)
        {
            if (Image != null)
            {
                Image.Dispose();
            }

            Image = new Bitmap(image);

            if (Graphics != null)
            {
                Graphics.Dispose();
            }

            Graphics = Graphics.FromImage(Image);

            if (PreviewImage != null)
            {
                PreviewImage.Dispose();
            }

            PreviewImage = new Bitmap(Image.Width, Image.Height);

            if (PreviewGraphics != null)
            {
                PreviewGraphics.Dispose();
            }

            PreviewGraphics = Graphics.FromImage(PreviewImage);
            PreviewGraphics.Clear(Color.Transparent);

            OnImageChange(this);
        }

        public void ClearImage()
        {
            if (Image != null)
            {
                Image.Dispose();
            }

            Image = null;

            if (Graphics != null)
            {
                Graphics.Dispose();
            }

            Graphics = null;

            if (PreviewImage != null)
            {
                PreviewImage.Dispose();
            }

            PreviewImage = null;

            if (PreviewGraphics != null)
            {
                PreviewGraphics.Dispose();
            }

            PreviewGraphics = null;

            OnImageChange(this);
        }

        // Работа с инструметами

        public void TrySelectTool(Tool tool)
        {
            tool.SelectAction(this);

            if (tool.Selectable)
            {
                selectedTool = tool;
            }

            OnImageUpdate(this);
        }

        public void SelectedToolExtraAction()
        {
            if (selectedTool != null)
            {
                selectedTool.ExtraAction(this);

                OnImageUpdate(this);
            }
        }

        public void ToolExtraAction(Tool tool)
        {
            tool.ExtraAction(this);

            OnImageUpdate(this);
        }

        public void MouseDown(MouseEventArgs args)
        {
            if (selectedTool != null)
            {
                selectedTool.MouseDown(this, args);

                OnImageUpdate(this);
            }
        }

        public void MouseUp(MouseEventArgs args)
        {
            if (selectedTool != null)
            {
                selectedTool.MouseUp(this, args);

                OnImageUpdate(this);
            }

            if (args.Button == MouseButtons.Middle)
            {
                Debug.WriteLine(Brush.Color.ToArgb().ToString("X8"));
            }
        }

        public void MouseMove(MouseEventArgs args)
        {
            if (selectedTool != null)
            {
                selectedTool.MouseMove(this, args);

                OnImageUpdate(this);
            }
        }

        // IDisposable

        public void Dispose()
        {
            if (Image != null)
            {
                Image.Dispose();
            }

            if (Graphics != null)
            {
                Graphics.Dispose();
            }

            if (PreviewImage != null)
            {
                PreviewImage.Dispose();
            }

            if (PreviewGraphics != null)
            {
                PreviewGraphics.Dispose();
            }

            if (Pen != null)
            {
                Pen.Dispose();
            }

            if (Brush != null)
            {
                Brush.Dispose();
            }
        }

        // События

        public event GraphicalEditorEventHandler OnImageChange = delegate { };

        public event GraphicalEditorEventHandler OnImageUpdate = delegate { };

        public event GraphicalEditorEventHandler OnBrushSizeChange = delegate { };
    }
}
