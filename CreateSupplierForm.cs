using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateSupplierForm : Form
    {
        private readonly SupplierController _controller = new SupplierController();

        public CreateSupplierForm()
        {
            InitializeComponent();
            Load += CreateSupplierForm_Load;
        }

        private void CreateSupplierForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1,
                "Supplier Name", "Billing Address", "Contact Person", "Phone", "Email", "Payment Term", "Status");
            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                string name = FormGridHelper.GetCellString(row, "Supplier Name");
                if (string.IsNullOrEmpty(name)) continue;
                if (!FormGridHelper.TryParseInt(FormGridHelper.GetCellString(row, "Status"), out int status))
                    status = 1;

                _controller.Insert(new Supplier
                {
                    SupplierName = name,
                    BillingAddress = FormGridHelper.GetCellString(row, "Billing Address"),
                    ContactPerson = FormGridHelper.GetCellString(row, "Contact Person"),
                    Phone = FormGridHelper.GetCellString(row, "Phone"),
                    Email = FormGridHelper.GetCellString(row, "Email"),
                    PaymentTerm = FormGridHelper.GetCellString(row, "Payment Term"),
                    Status = status
                });
                count++;
            }
            CreateFormHelper.ShowSaveResult(this, count, "supplier");
        }
    }
}
