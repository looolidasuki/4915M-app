using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateSalesOrderForm : Form
    {
        private readonly SalesOrderController _soController = new SalesOrderController();
        private readonly CustomerController _customerController = new CustomerController();
        private readonly ProductController _productController = new ProductController();
        private long _selectedCustomerId;

        public CreateSalesOrderForm()
        {
            InitializeComponent();
            Load += CreateSalesOrderForm_Load;
        }

        private void CreateSalesOrderForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.BindReadOnly(dataGridView2, _customerController.GetAllCustomers());
            FormGridHelper.BindReadOnly(dataGridView1, _productController.GetAllProducts());
            FormGridHelper.SetupEditableInputGrid(dataGridView3,
                "Product ID", "Price", "Order Qty", "Discount");
            dataGridView2.SelectionChanged += DataGridView2_SelectionChanged;
            CreateFormHelper.WireCancel(button2, this);
            button1.Click += BtnAddProduct_Click;
            button3.Click += BtnSave_Click;
        }

        private void DataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            _selectedCustomerId = FormGridHelper.GetSelectedId(dataGridView2, "Customer ID") ?? 0;
        }

        private void BtnAddProduct_Click(object sender, EventArgs e)
        {
            long? productId = FormGridHelper.GetSelectedId(dataGridView1, "Product ID");
            if (!productId.HasValue)
            {
                MessageBox.Show("Select a product from the product list.", "Validation");
                return;
            }
            int rowIndex = dataGridView3.Rows.Add();
            dataGridView3.Rows[rowIndex].Cells["Product ID"].Value = productId.Value;
            if (dataGridView1.CurrentRow != null)
            {
                string priceCol = dataGridView1.Columns.Contains("Base Price") ? "Base Price" : null;
                if (priceCol != null)
                    dataGridView3.Rows[rowIndex].Cells["Price"].Value = dataGridView1.CurrentRow.Cells[priceCol].Value;
            }
            dataGridView3.Rows[rowIndex].Cells["Order Qty"].Value = 1;
            dataGridView3.Rows[rowIndex].Cells["Discount"].Value = 0;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (_selectedCustomerId <= 0)
            {
                MessageBox.Show("Select a customer first.", "Validation");
                return;
            }

            long soId = _soController.Insert(new SalesOrder
            {
                SalesOrderCode = "TEMP",
                CustomerID = _selectedCustomerId,
                StaffID = AppDefaults.StaffId,
                CurrencyCurrencyID = AppDefaults.CurrencyId,
                DeliveryAddress = "TBD",
                Discount = 0,
                Status = 0
            });
            if (soId <= 0) return;
            _soController.UpdateCodeAfterInsert(soId);

            int lines = 0;
            foreach (DataGridViewRow row in dataGridView3.Rows)
            {
                if (row.IsNewRow) continue;
                if (!long.TryParse(FormGridHelper.GetCellString(row, "Product ID"), out long productId)) continue;
                if (!decimal.TryParse(FormGridHelper.GetCellString(row, "Price"), out decimal price)) price = 0;
                if (!decimal.TryParse(FormGridHelper.GetCellString(row, "Order Qty"), out decimal qty)) qty = 1;
                if (!decimal.TryParse(FormGridHelper.GetCellString(row, "Discount"), out decimal discount)) discount = 0;
                if (_soController.InsertProductLine(soId, productId, price, qty, discount)) lines++;
            }
            CreateFormHelper.ShowSaveResult(this, 1, "sales order");
        }
    }
}
