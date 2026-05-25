using Sales_user.Controllers;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ConfirmSalesOrderForm : Form
    {
        private readonly SalesOrderController _controller = new SalesOrderController();

        public ConfirmSalesOrderForm()
        {
            InitializeComponent();
            Load += ConfirmSalesOrderForm_Load;
        }

        private void ConfirmSalesOrderForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetAllSalesOrders());
            CreateFormHelper.WireCancel(button2, this);
            button3.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            long? orderId = FormGridHelper.GetSelectedId(dataGridView1, "Order ID");
            if (!orderId.HasValue)
            {
                MessageBox.Show("Select a sales order to confirm.", "Validation");
                return;
            }
            string sql = "UPDATE SalesOrder SET status = 1, lastModifyDate = NOW() WHERE salesOrderID = @id";
            if (DatabaseConnect.ExecuteNonQuery(sql,
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", orderId.Value)
                }) > 0)
            {
                MessageBox.Show("Sales order confirmed (status = 1).", "Success");
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetAllSalesOrders());
                DialogResult = DialogResult.OK;
            }
        }
    }
}
