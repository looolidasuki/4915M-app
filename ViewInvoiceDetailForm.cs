using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewInvoiceDetailForm : Form
    {
        private readonly InvoiceController _controller = new InvoiceController();
        private readonly long? _invoiceId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewInvoiceDetailForm(long? invoiceId = null)
        {
            _invoiceId = invoiceId;
            InitializeComponent();
            Load += ViewInvoiceDetailForm_Load;
        }

        private void ViewInvoiceDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllInvoices();
            _recordId = ViewDetailLoader.ResolveRecordId(_invoiceId, list, "Invoice ID", 0);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox5, textBox6, textBox10 },
                button1, button2, button3,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                "SELECT status, invoiceType, staffID FROM Invoice WHERE invoiceID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                });
            if (dt != null && dt.Rows.Count > 0)
            {
                textBox5.Text = dt.Rows[0]["status"].ToString();
                textBox6.Text = dt.Rows[0]["invoiceType"].ToString();
                textBox10.Text = dt.Rows[0]["staffID"].ToString();
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetInvoiceLines(_recordId));
        }

        private bool SaveRecord()
        {
            if (!int.TryParse(textBox5.Text, out int status)) status = 0;
            if (!int.TryParse(textBox6.Text, out int type)) type = 1;
            bool ok = EntityUpdateController.UpdateInvoice(_recordId, status, type);
            ViewDetailLoader.ShowSavedMessage(ok, "Invoice");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
