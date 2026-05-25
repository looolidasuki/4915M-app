using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewQuotationDetailForm : Form
    {
        private readonly QuotationController _controller = new QuotationController();
        private readonly long? _quotationId;

        public ViewQuotationDetailForm(long? quotationId = null)
        {
            _quotationId = quotationId;
            InitializeComponent();
            Load += ViewQuotationDetailForm_Load;
        }

        private void ViewQuotationDetailForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllQuotations();
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                if (_quotationId.HasValue && Convert.ToInt64(row["Quotation ID"]) != _quotationId.Value) continue;
                long qId = Convert.ToInt64(row["Quotation ID"]);
                textBox1.Text = row["Customer"].ToString();
                textBox2.Text = row["Quotation Code"].ToString();
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetProductLines(qId));
                long customerId = AppDefaults.CustomerId;
                DataTable qDetail = DatabaseConnect.ExecuteQuery(
                    "SELECT customerID FROM Quotation WHERE quotationID = @id",
                    new MySql.Data.MySqlClient.MySqlParameter[] {
                        new MySql.Data.MySqlClient.MySqlParameter("@id", qId)
                    });
                if (qDetail != null && qDetail.Rows.Count > 0)
                    customerId = Convert.ToInt64(qDetail.Rows[0]["customerID"]);
                FormGridHelper.BindReadOnly(dataGridView2, _controller.GetProductionOrdersByQuotationCustomer(customerId));
                break;
            }
            CreateFormHelper.WireCancel(button2, this);
        }
    }
}
