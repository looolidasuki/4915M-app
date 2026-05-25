using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateQuotationForm : Form
    {
        private readonly QuotationController _quotationController = new QuotationController();
        private readonly CustomerController _customerController = new CustomerController();
        private readonly ProductController _productController = new ProductController();
        private long _selectedCustomerId;

        public CreateQuotationForm()
        {
            InitializeComponent();
            Load += CreateQuotationForm_Load;
        }

        private void CreateQuotationForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.BindReadOnly(dataGridView1, _customerController.GetAllCustomers());
            FormGridHelper.BindReadOnly(dataGridView2, _productController.GetAllProducts());
            FormGridHelper.SetupEditableInputGrid(dataGridView3,
                "Product ID", "Price", "Quantity", "Discount");
            dataGridView1.SelectionChanged += DataGridView1_SelectionChanged;
            dataGridView2.CellDoubleClick += ProductGrid_DoubleClick;
            CreateFormHelper.WireCancel(button2, this);
            button3.Click += BtnSave_Click;
        }

        private void ProductGrid_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            long? productId = FormGridHelper.GetSelectedId(dataGridView2, "Product ID");
            if (!productId.HasValue) return;
            int idx = dataGridView3.Rows.Add();
            dataGridView3.Rows[idx].Cells["Product ID"].Value = productId.Value;
            if (dataGridView2.CurrentRow != null && dataGridView2.Columns.Contains("Base Price"))
                dataGridView3.Rows[idx].Cells["Price"].Value = dataGridView2.CurrentRow.Cells["Base Price"].Value;
            dataGridView3.Rows[idx].Cells["Quantity"].Value = 1;
            dataGridView3.Rows[idx].Cells["Discount"].Value = 0;
        }

        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            _selectedCustomerId = FormGridHelper.GetSelectedId(dataGridView1, "Customer ID") ?? 0;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerId <= 0)
            {
                MessageBox.Show("Select a customer first.", "Validation");
                return;
            }

            long qId = _quotationController.Insert(new Quotation
            {
                QuotationCode = "TEMP",
                SequenceNumber = 1,
                StaffID = AppDefaults.StaffId,
                CustomerID = _selectedCustomerId,
                CurrencyID = AppDefaults.CurrencyId,
                Status = 0
            });
            if (qId <= 0) return;
            _quotationController.UpdateCodeAfterInsert(qId);

            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.IsNewRow) continue;
                if (!long.TryParse(FormGridHelper.GetCellString(row, "Product ID"), out long productId)) continue;
                if (!decimal.TryParse(FormGridHelper.GetCellString(row, "Price"), out decimal price)) price = 0;
                if (!decimal.TryParse(FormGridHelper.GetCellString(row, "Quantity"), out decimal qty)) qty = 1;
                if (!decimal.TryParse(FormGridHelper.GetCellString(row, "Discount"), out decimal discount)) discount = 0;
                _quotationController.InsertProductLine(qId, productId, price, qty, discount);
            }
            CreateFormHelper.ShowSaveResult(this, 1, "quotation");
        }
    }
}
