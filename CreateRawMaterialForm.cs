using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateRawMaterialForm : Form
    {
        private readonly RawMaterialController _controller = new RawMaterialController();

        public CreateRawMaterialForm()
        {
            InitializeComponent();
            Load += CreateRawMaterialForm_Load;
        }

        private void CreateRawMaterialForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1,
                "Raw Material Code", "Category", "Size", "Color", "Min Stock", "Status");
            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                string code = FormGridHelper.GetCellString(row, "Raw Material Code");
                if (string.IsNullOrEmpty(code)) continue;
                if (!FormGridHelper.TryParseDecimal(FormGridHelper.GetCellString(row, "Min Stock"), out decimal minStock))
                    minStock = 0;
                if (!FormGridHelper.TryParseInt(FormGridHelper.GetCellString(row, "Status"), out int status))
                    status = 1;

                _controller.Insert(new RawMaterial
                {
                    RawMaterialCode = code,
                    Category = FormGridHelper.GetCellString(row, "Category"),
                    Size = FormGridHelper.GetCellString(row, "Size"),
                    Color = FormGridHelper.GetCellString(row, "Color"),
                    MinimumStockLevel = minStock,
                    Status = status
                });
                count++;
            }
            CreateFormHelper.ShowSaveResult(this, count, "raw material");
        }
    }
}
