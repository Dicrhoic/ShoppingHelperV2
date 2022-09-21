using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShoppingHelperV2
{
    public partial class ProgressBar : Form
    {
        public ProgressBar()
        {
            InitializeComponent();
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;
        }

        public void SetCaption(string caption)
        {
            this.caption.Text = caption;
        }

        public void UpdateBarProgress(int percentage)
        {  
            string valueString = percentage.ToString() + "%";
            progressPercentageLabel.Text = (valueString);
            progressBar1.Value = percentage;
        }

        public void BarStepUp()
        {
            progressBar1.PerformStep();
        }
    }
}
