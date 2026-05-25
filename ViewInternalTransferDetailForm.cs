using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewInternalTransferDetailForm : Form
    {
        public ViewInternalTransferDetailForm()
        {
            InitializeComponent();
            Load += ViewInternalTransferDetailForm_Load;
        }

        private void ViewInternalTransferDetailForm_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
