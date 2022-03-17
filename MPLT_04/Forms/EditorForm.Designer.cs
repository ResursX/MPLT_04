
using MPLT_04.Logic.Tools;

namespace MPLT_04.Forms
{
    partial class EditorForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            foreach (Tool t in Tools)
            {
                t.Dispose();
            }

            GraphicalEditor.Dispose();

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.actionStrip = new System.Windows.Forms.ToolStrip();
            this.toolStrip = new System.Windows.Forms.ToolStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusStripSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripFormat = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // actionStrip
            // 
            this.actionStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.actionStrip.Location = new System.Drawing.Point(0, 0);
            this.actionStrip.Name = "actionStrip";
            this.actionStrip.Size = new System.Drawing.Size(686, 25);
            this.actionStrip.TabIndex = 0;
            this.actionStrip.Text = "toolStrip1";
            // 
            // toolStrip
            // 
            this.toolStrip.AutoSize = false;
            this.toolStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.toolStrip.Location = new System.Drawing.Point(0, 25);
            this.toolStrip.Name = "toolStrip";
            this.toolStrip.Size = new System.Drawing.Size(48, 341);
            this.toolStrip.TabIndex = 1;
            this.toolStrip.Text = "toolStrip2";
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.pictureBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(48, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(638, 341);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(0, 0);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(100, 50);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            this.pictureBox.Resize += new System.EventHandler(this.pictureBox_Resize);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripSize,
            this.statusStripFormat});
            this.statusStrip1.Location = new System.Drawing.Point(0, 366);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 12, 0);
            this.statusStrip1.Size = new System.Drawing.Size(686, 24);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusStripSize
            // 
            this.statusStripSize.Name = "statusStripSize";
            this.statusStripSize.Size = new System.Drawing.Size(12, 19);
            this.statusStripSize.Text = "-";
            // 
            // statusStripFormat
            // 
            this.statusStripFormat.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.statusStripFormat.Name = "statusStripFormat";
            this.statusStripFormat.Size = new System.Drawing.Size(16, 19);
            this.statusStripFormat.Text = "-";
            // 
            // EditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 390);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip);
            this.Controls.Add(this.actionStrip);
            this.Controls.Add(this.statusStrip1);
            this.Name = "EditorForm";
            this.Text = "EditorForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip actionStrip;
        private System.Windows.Forms.ToolStrip toolStrip;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusStripSize;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.ToolStripStatusLabel statusStripFormat;
    }
}