using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewCustomerDetailForm : Form
    {
        private readonly CustomerController _controller = new CustomerController();
        private readonly long? _customerId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewCustomerDetailForm(long? customerId = null)
        {
            _customerId = customerId;
            InitializeComponent();
            Load += ViewCustomerDetailForm_Load;
        }

        private void ViewCustomerDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllCustomers();
            _recordId = ViewDetailLoader.ResolveRecordId(_customerId, list, "Customer ID", AppDefaults.CustomerId);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox1, textBox2, textBox3 },
                button1, button3, button2,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            var customer = _controller.GetById(_recordId);
            if (customer != null)
            {
                textBox1.Text = customer.CustomerName;
                textBox2.Text = customer.BillingAddress;
                textBox3.Text = customer.PaymentTerm;
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetSalesOrdersByCustomer(_recordId));
            FormGridHelper.BindReadOnly(dataGridView2, _controller.GetQuotationsByCustomer(_recordId));
        }

        private bool SaveRecord()
        {
            bool ok = _controller.Update(new Customer
            {
                CustomerID = _recordId,
                CustomerName = textBox1.Text.Trim(),
                BillingAddress = textBox2.Text.Trim(),
                PaymentTerm = textBox3.Text.Trim()
            });
            ViewDetailLoader.ShowSavedMessage(ok, "Customer");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
