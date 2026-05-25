using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewSalesOrderDetailForm : Form
    {
        private readonly SalesOrderController _controller = new SalesOrderController();
        private readonly long? _salesOrderId;

        public ViewSalesOrderDetailForm(long? salesOrderId = null)
        {
            _salesOrderId = salesOrderId;
            InitializeComponent();
            Load += ViewSalesOrderDetailForm_Load;
        }

        private void ViewSalesOrderDetailForm_Load(object sender, EventArgs e)
        {
            DataTable orders = _controller.GetAllSalesOrders();
            if (orders != null && orders.Rows.Count > 0)
            {
                long id = _salesOrderId ?? Convert.ToInt64(orders.Rows[0]["Order ID"]);
                foreach (DataRow row in orders.Rows)
                {
                    if (_salesOrderId.HasValue && Convert.ToInt64(row["Order ID"]) != _salesOrderId.Value) continue;
                    textBox2.Text = row["Order Code"].ToString();
                    textBox1.Text = row["Customer"].ToString();
                    textBox3.Text = row["Delivery Address"].ToString();
                    FormGridHelper.BindReadOnly(dataGridView1, _controller.GetProductLines(id));
                    FormGridHelper.BindReadOnly(dataGridView2, _controller.GetProductionOrdersBySalesOrder(id));
                    break;
                }
            }
            CreateFormHelper.WireCancel(button2, this);
        }
    }
}
