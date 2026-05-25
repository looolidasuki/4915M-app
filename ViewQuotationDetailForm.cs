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
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewQuotationDetailForm(long? quotationId = null)
        {
            _quotationId = quotationId;
            InitializeComponent();
            Load += ViewQuotationDetailForm_Load;
        }

        private void ViewQuotationDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllQuotations();
            _recordId = ViewDetailLoader.ResolveRecordId(_quotationId, list, "Quotation ID", 0);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox1, textBox2, textBox4 },
                button1, button3, button2,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                @"SELECT q.quotationCode, c.customerName, q.status
                  FROM Quotation q LEFT JOIN Customer c ON q.customerID = c.customerID
                  WHERE q.quotationID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                });
            if (dt != null && dt.Rows.Count > 0)
            {
                textBox2.Text = dt.Rows[0]["quotationCode"].ToString();
                textBox1.Text = dt.Rows[0]["customerName"].ToString();
                textBox4.Text = dt.Rows[0]["status"].ToString();
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetProductLines(_recordId));
        }

        private bool SaveRecord()
        {
            if (!int.TryParse(textBox4.Text, out int status)) status = 0;
            bool ok = DatabaseConnect.ExecuteNonQuery(
                "UPDATE Quotation SET status = @status, lastModifyDate = NOW() WHERE quotationID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@status", status),
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                }) > 0;
            ViewDetailLoader.ShowSavedMessage(ok, "Quotation");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
