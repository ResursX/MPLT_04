using MPLT_04_INTERFACE.Logic;
using MPLT_04_INTERFACE.Logic.Actions;
using MPLT_04_INTERFACE.Logic.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPLT_04_INTERFACE
{
    public partial class Form1 : Form
    {
        public static readonly Color[] initialColors = { Color.Black, Color.White };

        // Графический редактор

        GraphicalEditor GraphicalEditor;

        // Действия

        Logic.Actions.Action ActionCreate;
        ToolStripButton ButtonCreate;

        Logic.Actions.Action ActionOpen;
        ToolStripButton ButtonOpen;

        Logic.Actions.Action ActionSave;
        ToolStripButton ButtonSave;

        Logic.Actions.Action ActionClose;
        ToolStripButton ButtonClose;

        List<ActionColor> ColorActions;
        List<ToolStripButton> ColorButtons;

        Logic.Actions.Action ActionColorCycle;
        ToolStripButton ButtonColorCycle;

        ToolStripTextBox BrushSizeTextBox;

        Logic.Actions.Action ActionBrushSizeInc;
        ToolStripButton ButtonBrushSizeInc;

        Logic.Actions.Action ActionBrushSizeDec;
        ToolStripButton ButtonBrushSizeDec;

        // Инструменты
        List<Tool> Tools;
        List<ToolStripButton> ToolButtons;

        public Form1()
        {
            InitializeComponent();

            ColorActions = new List<ActionColor>();
            ColorButtons = new List<ToolStripButton>();

            Tools = new List<Tool>();
            ToolButtons = new List<ToolStripButton>();

            GraphicalEditor = new GraphicalEditor(pictureBox1);

            //Действия

            ButtonCreate = AddActionButton(ActionCreate = new ActionCreate(GraphicalEditor), Resou);

            ButtonOpen = AddActionButton(ActionOpen = new ActionOpen(GraphicalEditor), null);

            ButtonSave = AddActionButton(ActionSave = new ActionSave(GraphicalEditor), null);
            ButtonSave.Enabled = false;

            ButtonClose = AddActionButton(ActionClose = new ActionClose(GraphicalEditor), null);
            ButtonClose.Enabled = false;

            GraphicalEditor.OnImageChange += (e) =>
            {
                if (e.Image != null)
                {
                    ButtonSave.Enabled = true;
                    ButtonClose.Enabled = true;
                }
                else
                {
                    ButtonSave.Enabled = false;
                    ButtonClose.Enabled = false;
                }
            };

            actionStrip.Items.Add(new ToolStripSeparator());

            foreach(Color c in initialColors)
            {
                ActionColor action = new ActionColor(GraphicalEditor, c);

                ToolStripButton button = AddActionButton(action);

                action.OnPostAction += (_, _) => {
                    button.Image = action.CreateIcon(16, 16);
                };

                button.Image = action.CreateIcon(16, 16);

                ColorActions.Add(action);
                ColorButtons.Add(button);
            }

            ButtonColorCycle = AddActionButton(ActionColorCycle = new ActionColorCycle(GraphicalEditor), null);

            ActionColorCycle.OnPostAction += (_, _) => {
                for (int i = 0; i < ColorActions.Count; i++)
                {
                    ColorButtons[i].Image = ColorActions[i].CreateIcon(16, 16);
                }
            };

            actionStrip.Items.Add(new ToolStripSeparator());

            ButtonBrushSizeDec = AddActionButton(ActionBrushSizeDec = new ActionBrushSizeAdd(GraphicalEditor, -1), null);

            // Поле изменения размера кисти
            {
                BrushSizeTextBox = new ToolStripTextBox()
                {
                    Size = new Size(50, 25)
                };
                BrushSizeTextBox.TextBoxTextAlign = HorizontalAlignment.Right;

                BrushSizeTextBox.Text = GraphicalEditor.BrushSize.ToString();

                BrushSizeTextBox.KeyPress += (_, e) =>
                {
                    if (!Char.IsDigit(e.KeyChar) && !Char.IsControl(e.KeyChar))
                    {
                        e.Handled = true;
                    }
                };

                BrushSizeTextBox.TextChanged += (_, _) =>
                {
                    if (BrushSizeTextBox.Text.Length > 0 && int.TryParse(BrushSizeTextBox.Text, out int t))
                    {
                        GraphicalEditor.SetBrushSize(t);
                    }
                    else
                    {
                        GraphicalEditor.SetBrushSize(1);
                    }
                };

                GraphicalEditor.OnBrushSizeChange += (editor) => { BrushSizeTextBox.Text = editor.BrushSize.ToString(); };

                actionStrip.Items.Add(BrushSizeTextBox);
            }

            ButtonBrushSizeInc = AddActionButton(ActionBrushSizeInc = new ActionBrushSizeAdd(GraphicalEditor, 1), null);

            // Инструменты

            AddToolButton(new ToolBrush(), null);
            AddToolButton(new ToolLine(), null);
        }

        private ToolStripButton AddActionButton(Logic.Actions.Action action)
        {
            ToolStripButton button = new ToolStripButton()
            {
                Name = action.GetType().ToString(),
                Text = action.Name,
                DisplayStyle = ToolStripItemDisplayStyle.Image
            };

            button.Click += (sender, e) =>
            {
                try
                {
                    action.DoAction();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            actionStrip.Items.Add(button);

            return button;
        }

        private ToolStripButton AddActionButton(Logic.Actions.Action action, Image icon)
        {
            ToolStripButton button = AddActionButton(action);

            button.Image = icon;

            return button;
        }

        private ToolStripButton AddToolButton(Tool tool, Image icon)
        {
            Tools.Add(tool);

            ToolStripButton button = new ToolStripButton()
            {
                Name = tool.GetType().ToString(),
                Text = tool.Name,
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                Image = icon
            };

            button.Click += (sender, e) =>
            {
                try
                {
                    //if (e is MouseEventArgs me)
                    //{
                    //    if ()
                    //}

                    GraphicalEditor.TrySelectTool(tool);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            button.MouseDown += (sender, e) =>
            {
                try
                {
                    if (e.Button == MouseButtons.Right)
                    {
                        tool.ExtraAction(GraphicalEditor);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            //button.DoubleClick += (sender, e) =>
            //{
            //    try
            //    {
            //        tool.ExtraAction(GraphicalEditor);
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(this, ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //};

            toolStrip.Items.Add(button);
            ToolButtons.Add(button);

            return button;
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                toolStripSize.Text = pictureBox1.Width.ToString() + "x" + pictureBox1.Height.ToString();
            }
            else
            {
                toolStripSize.Text = "-";
            }
        }
    }
}
