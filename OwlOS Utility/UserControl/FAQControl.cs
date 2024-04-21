using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OwlOS_Utility
{
    public partial class FAQControl : UserControl
    {
        public FAQControl()
        {
            InitializeComponent();
            guna2Panel1.Hide();
        }

        private void UpdatesControl_Load(object sender, EventArgs e)
        {
            guna2Transition1.ShowSync(guna2Panel1);
            this.AutoScaleMode = AutoScaleMode.Dpi;
        }
    }
}
