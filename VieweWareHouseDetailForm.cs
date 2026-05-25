using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class VieweWareHouseDetailForm : Form
    {
        private readonly WarehouseController _controller = new WarehouseController();
        private readonly long? _warehouseId;

        public VieweWareHouseDetailForm(long? warehouseId = null)
        {
            _warehouseId = warehouseId;
            InitializeComponent();
            Load += VieweWareHouseDetailForm_Load;
        }

        private void VieweWareHouseDetailForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllWarehouses();
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                if (_warehouseId.HasValue && Convert.ToInt64(row["Warehouse ID"]) != _warehouseId.Value) continue;
                long id = Convert.ToInt64(row["Warehouse ID"]);
                textBox28.Text = row["Warehouse Name"].ToString();
                textBox29.Text = row["Address"].ToString();
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetWarehouseProducts(id));
                break;
            }
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
