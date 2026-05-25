using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateInvoiceForm : Form
    {
        private readonly InvoiceController _controller = new InvoiceController();

        public CreateInvoiceForm()
        {
            InitializeComponent();
            Load += CreateInvoiceForm_Load;
        }

        private void CreateInvoiceForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1,
                "Customer ID", "Sales Order ID", "Invoice Type", "Status", "Remark");
            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                if (!long.TryParse(FormGridHelper.GetCellString(row, "Customer ID"), out long customerId)) continue;
                if (!long.TryParse(FormGridHelper.GetCellString(row, "Sales Order ID"), out long soId)) continue;
                if (!int.TryParse(FormGridHelper.GetCellString(row, "Invoice Type"), out int invType)) invType = 1;
                if (!int.TryParse(FormGridHelper.GetCellString(row, "Status"), out int status)) status = 0;

                long id = _controller.Insert(new Invoice
                {
                    InvoiceCode = "TEMP",
                    CustomerID = customerId,
                    SalesOrderID = soId,
                    StaffID = AppDefaults.StaffId,
                    InvoiceType = invType,
                    Status = status,
                    Remark = FormGridHelper.GetCellString(row, "Remark")
                });
                if (id > 0)
                {
                    _controller.UpdateCodeAfterInsert(id);
                    count++;
                }
            }
            CreateFormHelper.ShowSaveResult(this, count, "invoice");
        }
    }
}
