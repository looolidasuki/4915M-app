using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreatePurchaseOrderForm : Form
    {
        private readonly PurchaseOrderController _controller = new PurchaseOrderController();

        public CreatePurchaseOrderForm()
        {
            InitializeComponent();
            Load += CreatePurchaseOrderForm_Load;
        }

        private void CreatePurchaseOrderForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1,
                "Supplier ID", "Request Delivery Date", "Status", "Remark",
                "Raw Material ID", "Price", "Order Qty");
            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0 || dataGridView1.Rows[0].IsNewRow) return;
            var row = dataGridView1.Rows[0];
            if (!long.TryParse(FormGridHelper.GetCellString(row, "Supplier ID"), out long supplierId))
                supplierId = AppDefaults.SupplierId;
            DateTime deliveryDate = DateTime.Today.AddDays(14);
            DateTime.TryParse(FormGridHelper.GetCellString(row, "Request Delivery Date"), out deliveryDate);
            if (!int.TryParse(FormGridHelper.GetCellString(row, "Status"), out int status)) status = 0;

            long poId = _controller.Insert(new PurchaseOrder
            {
                PurchaseOrderCode = "TEMP",
                SupplierID = supplierId,
                StaffID = AppDefaults.StaffId,
                RequestDeliveryDate = deliveryDate,
                Status = status,
                Remark = FormGridHelper.GetCellString(row, "Remark")
            });
            if (poId <= 0) return;
            _controller.UpdateCodeAfterInsert(poId);

            int lines = 0;
            foreach (DataGridViewRow lineRow in dataGridView1.Rows)
            {
                if (lineRow.IsNewRow) continue;
                if (!long.TryParse(FormGridHelper.GetCellString(lineRow, "Raw Material ID"), out long rmId)) continue;
                if (!decimal.TryParse(FormGridHelper.GetCellString(lineRow, "Price"), out decimal price)) price = 0;
                if (!decimal.TryParse(FormGridHelper.GetCellString(lineRow, "Order Qty"), out decimal qty)) qty = 1;
                if (_controller.InsertLine(poId, rmId, price, qty)) lines++;
            }
            CreateFormHelper.ShowSaveResult(this, 1, "purchase order");
        }
    }
}
