using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewPurchaseOrderDetailForm : Form
    {
        private readonly PurchaseOrderController _controller = new PurchaseOrderController();
        private readonly long? _purchaseOrderId;

        public ViewPurchaseOrderDetailForm(long? purchaseOrderId = null)
        {
            _purchaseOrderId = purchaseOrderId;
            InitializeComponent();
            Load += ViewPurchaseOrderDetailForm_Load;
        }

        private void ViewPurchaseOrderDetailForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllPurchaseOrders();
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                if (_purchaseOrderId.HasValue && Convert.ToInt64(row["Purchase Order ID"]) != _purchaseOrderId.Value) continue;
                txtPurchasesOrder.Text = row["Purchase Order Code"].ToString();
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetAllPurchaseOrderLines());
                break;
            }
            CreateFormHelper.WireCancel(button2, this);
        }
    }
}
