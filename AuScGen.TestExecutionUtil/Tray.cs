using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuScGen.TestExecutionUtil
{
    public partial class Tray : Form
    {
        public Tray()
        {
            InitializeComponent();
            this.Width = Screen.PrimaryScreen.Bounds.Size.Width;
            this.ShowInTaskbar = false;
        }

        public string Message 
        { 
            get
            {
                return txtStepMessage.Text;
            }
            set
            {
                
                txtStepMessage.Text = value;
                txtStepMessage.Refresh();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
