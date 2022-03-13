using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MPLT_04_INTERFACE.Logic.Actions.ActionCreateLogic
{
    public partial class ImageCreationForm : Form
    {
        public ImageCreationForm()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            widthBox.Value = 360;
            heightBox.Value = 240;
        }

        public int GetWidth()
        {
            return (int)widthBox.Value;
        }

        public int GetHeight()
        {
            return (int)heightBox.Value;
        }
    }
}
