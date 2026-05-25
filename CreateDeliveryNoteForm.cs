using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateDeliveryNoteForm : Form
    {
        private readonly DeliveryNoteController _controller = new DeliveryNoteController();

        public CreateDeliveryNoteForm()
        {
            InitializeComponent();
            Load += CreateDeliveryNoteForm_Load;
        }

        private void CreateDeliveryNoteForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1,
                "Customer ID", "Sales Order ID", "Warehouse ID", "Ship Method", "Tracking Number", "Status");
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
                long whId = AppDefaults.WarehouseId;
                long.TryParse(FormGridHelper.GetCellString(row, "Warehouse ID"), out whId);
                if (!int.TryParse(FormGridHelper.GetCellString(row, "Status"), out int status)) status = 0;

                long id = _controller.Insert(new DeliveryNote
                {
                    DeliveryNoteCode = "TEMP",
                    CustomerID = customerId,
                    SalesOrderID = soId,
                    StaffID = AppDefaults.StaffId,
                    WarehouseID = whId,
                    ShipMethod = FormGridHelper.GetCellString(row, "Ship Method"),
                    TrackingNumber = FormGridHelper.GetCellString(row, "Tracking Number"),
                    Status = status
                });
                if (id > 0)
                {
                    _controller.UpdateCodeAfterInsert(id);
                    count++;
                }
            }
            CreateFormHelper.ShowSaveResult(this, count, "delivery note");
        }
    }
}
