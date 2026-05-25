using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateWareHouserForm : Form
    {
        private readonly WarehouseController _controller = new WarehouseController();

        public CreateWareHouserForm()
        {
            InitializeComponent();
            Load += CreateWareHouserForm_Load;
        }

        private void CreateWareHouserForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1, "Warehouse Name", "Address");
            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                string name = FormGridHelper.GetCellString(row, "Warehouse Name");
                if (string.IsNullOrEmpty(name)) continue;
                _controller.Insert(new Warehouse
                {
                    WarehouseName = name,
                    WarehouseAddress = FormGridHelper.GetCellString(row, "Address")
                });
                count++;
            }
            CreateFormHelper.ShowSaveResult(this, count, "warehouse");
        }
    }
}
