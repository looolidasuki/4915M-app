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

        public ViewCustomerDetailForm(long? customerId = null)
        {
            _customerId = customerId;
            InitializeComponent();
            Load += ViewCustomerDetailForm_Load;
        }

        private void ViewCustomerDetailForm_Load(object sender, EventArgs e)
        {
            long id = _customerId ?? AppDefaults.CustomerId;
            var customer = _controller.GetById(id);
            if (customer != null)
            {
                textBox1.Text = customer.CustomerName;
                textBox2.Text = customer.BillingAddress;
                textBox3.Text = customer.PaymentTerm;
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetSalesOrdersByCustomer(id));
            FormGridHelper.BindReadOnly(dataGridView2, _controller.GetQuotationsByCustomer(id));
            CreateFormHelper.WireCancel(button2, this);
            button3.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            long id = _customerId ?? AppDefaults.CustomerId;
            if (_controller.Update(new Customer
            {
                CustomerID = id,
                CustomerName = textBox1.Text,
                BillingAddress = textBox2.Text,
                PaymentTerm = textBox3.Text
            }))
            {
                MessageBox.Show("Customer updated.", "Success");
                DialogResult = DialogResult.OK;
            }
        }
    }
}
