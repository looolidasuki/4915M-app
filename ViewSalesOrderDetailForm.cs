using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewSalesOrderDetailForm : Form
    {
        private readonly SalesOrderController _controller = new SalesOrderController();
        private readonly long? _salesOrderId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewSalesOrderDetailForm(long? salesOrderId = null)
        {
            _salesOrderId = salesOrderId;
            InitializeComponent();
            Load += ViewSalesOrderDetailForm_Load;
        }

        private void ViewSalesOrderDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllSalesOrders();
            _recordId = ViewDetailLoader.ResolveRecordId(_salesOrderId, list, "Order ID", 0);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox1, textBox2, textBox3, textBox4 },
                button1, button3, button2,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            var order = _controller.GetById(_recordId);
            if (order == null) return;
            textBox2.Text = order.SalesOrderCode;
            textBox3.Text = order.Discount.ToString();
            textBox4.Text = order.Status.ToString();
            textBox1.Text = order.DeliveryAddress;
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetProductLines(_recordId));
            FormGridHelper.BindReadOnly(dataGridView2, _controller.GetProductionOrdersBySalesOrder(_recordId));
        }

        private bool SaveRecord()
        {
            if (!int.TryParse(textBox4.Text, out int status)) status = 0;
            if (!decimal.TryParse(textBox3.Text, out decimal discount)) discount = 0;
            bool ok = _controller.Update(new SalesOrder
            {
                SalesOrderID = _recordId,
                DeliveryAddress = textBox1.Text.Trim(),
                Discount = discount,
                Status = status
            });
            ViewDetailLoader.ShowSavedMessage(ok, "Sales Order");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
