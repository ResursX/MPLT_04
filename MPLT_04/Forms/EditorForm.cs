using MPLT_04;
using MPLT_04.Logic;
using MPLT_04.Logic.Actions;
using MPLT_04.Logic.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPLT_04.Forms
{
    public partial class EditorForm : Form
    {
        private static readonly Color[] initialColors = { Color.Black, Color.White };

        // Графический редактор

        private readonly GraphicalEditor GraphicalEditor;

        // Действия

        private readonly Logic.Actions.Action ActionCreate;
        private readonly ToolStripButton ButtonCreate;

        private readonly Logic.Actions.Action ActionOpen;
        private readonly ToolStripButton ButtonOpen;

        private readonly Logic.Actions.Action ActionSave;
        private readonly ToolStripButton ButtonSave;

        private readonly Logic.Actions.Action ActionClose;
        private readonly ToolStripButton ButtonClose;

        private readonly List<ActionColor> ColorActions;
        private readonly List<ToolStripButton> ColorButtons;

        private readonly Logic.Actions.Action ActionColorCycle;
        private readonly ToolStripButton ButtonColorCycle;

        private readonly ToolStripTextBox BrushSizeTextBox;

        private readonly Logic.Actions.Action ActionBrushSizeInc;
        private readonly ToolStripButton ButtonBrushSizeInc;

        private readonly Logic.Actions.Action ActionBrushSizeDec;
        private readonly ToolStripButton ButtonBrushSizeDec;

        // Инструменты
        private readonly List<Tool> Tools;
        private readonly List<ToolStripButton> ToolButtons;

        public EditorForm()
        {
            InitializeComponent();

            ColorActions = new List<ActionColor>();
            ColorButtons = new List<ToolStripButton>();

            Tools = new List<Tool>();
            ToolButtons = new List<ToolStripButton>();

            GraphicalEditor = new GraphicalEditor(pictureBox);

            //Действия

            ButtonCreate = AddActionButton(ActionCreate = new ActionCreate(GraphicalEditor), Resources.NewFile);

            ButtonOpen = AddActionButton(ActionOpen = new ActionOpen(GraphicalEditor), Resources.OpenFile);

            ButtonSave = AddActionButton(ActionSave = new ActionSave(GraphicalEditor), Resources.SaveFile);
            ButtonSave.Enabled = false;

            ButtonClose = AddActionButton(ActionClose = new ActionClose(GraphicalEditor), Resources.CloseFile);
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

            foreach (Color c in initialColors)
            {
                ActionColor action = new ActionColor(GraphicalEditor, c);

                ToolStripButton button = AddActionButton(action);

                action.OnPostAction += (s, e) => {
                    button.Image = action.CreateIcon(16, 16);
                };

                button.Image = action.CreateIcon(16, 16);

                ColorActions.Add(action);
                ColorButtons.Add(button);
            }

            ButtonColorCycle = AddActionButton(ActionColorCycle = new ActionColorCycle(GraphicalEditor), Resources.CycleColor);

            ActionColorCycle.OnPostAction += (s, e) => {
                for (int i = 0; i < ColorActions.Count; i++)
                {
                    ColorButtons[i].Image = ColorActions[i].CreateIcon(16, 16);
                }
            };

            actionStrip.Items.Add(new ToolStripSeparator());

            ButtonBrushSizeDec = AddActionButton(ActionBrushSizeDec = new ActionBrushSizeAdd(GraphicalEditor, -1), Resources.BrushSizeDec);

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

                BrushSizeTextBox.TextChanged += (s, e) =>
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

            ButtonBrushSizeInc = AddActionButton(ActionBrushSizeInc = new ActionBrushSizeAdd(GraphicalEditor, 1), Resources.BrushSizeInc);

            // Инструменты

            AddToolButton(new ToolBrush(), Resources.ToolBrush);
            AddToolButton(new ToolLine(), Resources.ToolLine);

            foreach (string lib in Directory.EnumerateFiles(Directory.GetCurrentDirectory(), "*.dll"))
            {
                Debug.WriteLine("Trying to load lib: " + lib);

                try
                {
                    Tool tool = new ToolLoaded(lib);

                    AddToolButton(tool, Resources.ToolBrush);

                    Debug.WriteLine("Loaded succesfully");
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Loading failed! " + e.Message);

                    //throw;
                }
            }
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

            toolStrip.Items.Add(button);
            ToolButtons.Add(button);

            return button;
        }

        private void pictureBox_Resize(object sender, EventArgs e)
        {
            if (pictureBox.Image != null)
            {
                statusStripSize.Text = pictureBox.Width.ToString() + "x" + pictureBox.Height.ToString();
            }
            else
            {
                statusStripSize.Text = "-";
            }
        }
    }
}
