using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateProductForm : Form
    {
        private readonly ProductController _controller = new ProductController();

        public CreateProductForm()
        {
            InitializeComponent();
            Load += CreateProductForm_Load;
        }

        private void CreateProductForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1,
                "Product Code", "Category", "Style Number", "Size", "Color",
                "Base Price", "Unit", "Status");
            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                string code = FormGridHelper.GetCellString(row, "Product Code");
                if (string.IsNullOrEmpty(code)) continue;
                if (!FormGridHelper.TryParseDecimal(FormGridHelper.GetCellString(row, "Base Price"), out decimal price))
                    price = 0;
                if (!FormGridHelper.TryParseInt(FormGridHelper.GetCellString(row, "Status"), out int status))
                    status = 1;

                _controller.Insert(new Product
                {
                    ProductCode = code,
                    Category = FormGridHelper.GetCellString(row, "Category"),
                    StyleNumber = FormGridHelper.GetCellString(row, "Style Number"),
                    Size = FormGridHelper.GetCellString(row, "Size"),
                    Color = FormGridHelper.GetCellString(row, "Color"),
                    BasePriceByCurrency = price,
                    Unit = FormGridHelper.GetCellString(row, "Unit"),
                    Status = status,
                    CurrencyID = AppDefaults.CurrencyId,
                    StaffID = AppDefaults.StaffId
                });
                count++;
            }
            CreateFormHelper.ShowSaveResult(this, count, "product");
        }
    }
}
