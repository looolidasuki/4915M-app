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

        public ViewInvoiceDetailForm(long? invoiceId = null)
        {
            _invoiceId = invoiceId;
            InitializeComponent();
            Load += ViewInvoiceDetailForm_Load;
        }

        private void ViewInvoiceDetailForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllInvoices();
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                if (_invoiceId.HasValue && Convert.ToInt64(row["Invoice ID"]) != _invoiceId.Value) continue;
                long id = Convert.ToInt64(row["Invoice ID"]);
                textBox5.Text = row["Status"].ToString();
                textBox6.Text = row["Invoice Type"].ToString();
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetInvoiceLines(id));
                break;
            }
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
