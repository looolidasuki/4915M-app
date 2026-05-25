using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateInternalTransferForm : Form
    {
        public CreateInternalTransferForm()
        {
            InitializeComponent();
            Load += CreateInternalTransferForm_Load;
        }

        private void CreateInternalTransferForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1,
                "From Warehouse ID", "To Warehouse ID", "Product ID", "Raw Material ID", "Transfer Qty");
            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
            label1.Text = "Internal transfer is not in SQL schema; adjust WarehouseProduct manually if needed.";
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Internal Transfer table is not defined in 4915M_SQL.sql. Use Warehouse stock screens or add a custom table.",
                "Not Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
