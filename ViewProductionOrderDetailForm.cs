using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewProductionOrderDetailForm : Form
    {
        private readonly ProductionOrderController _controller = new ProductionOrderController();
        private readonly long? _productionOrderId;

        public ViewProductionOrderDetailForm(long? productionOrderId = null)
        {
            _productionOrderId = productionOrderId;
            InitializeComponent();
            Load += ViewProductionOrderDetailForm_Load;
        }

        private void ViewProductionOrderDetailForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllProductionOrders();
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                if (_productionOrderId.HasValue && Convert.ToInt64(row["Production Order ID"]) != _productionOrderId.Value) continue;
                long id = Convert.ToInt64(row["Production Order ID"]);
                textBox26.Text = row["Production Order Code"].ToString();
                textBox25.Text = row["Sales Order ID"].ToString();
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetProductLines(id));
                break;
            }
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
