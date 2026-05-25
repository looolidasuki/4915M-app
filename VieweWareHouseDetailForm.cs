using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class VieweWareHouseDetailForm : Form
    {
        private readonly WarehouseController _controller = new WarehouseController();
        private readonly long? _warehouseId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public VieweWareHouseDetailForm(long? warehouseId = null)
        {
            _warehouseId = warehouseId;
            InitializeComponent();
            Load += VieweWareHouseDetailForm_Load;
        }

        private void VieweWareHouseDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllWarehouses();
            _recordId = ViewDetailLoader.ResolveRecordId(_warehouseId, list, "Warehouse ID", AppDefaults.WarehouseId);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox28, textBox29 },
                button1, button2, button3,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                "SELECT warehouseName, warehouseAddress FROM Warehouse WHERE warehouseID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                });
            if (dt != null && dt.Rows.Count > 0)
            {
                textBox28.Text = dt.Rows[0]["warehouseName"].ToString();
                textBox29.Text = dt.Rows[0]["warehouseAddress"].ToString();
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetWarehouseProducts(_recordId));
        }

        private bool SaveRecord()
        {
            bool ok = EntityUpdateController.UpdateWarehouse(new Warehouse
            {
                WarehouseID = _recordId,
                WarehouseName = textBox28.Text.Trim(),
                WarehouseAddress = textBox29.Text.Trim()
            });
            ViewDetailLoader.ShowSavedMessage(ok, "Warehouse");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
