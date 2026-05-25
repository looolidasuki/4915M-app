using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateCustomerForm : Form
    {
        private readonly CustomerController _controller = new CustomerController();
        private DataGridView _inputGrid;

        public CreateCustomerForm()
        {
            InitializeComponent();
            Load += CreateCustomerForm_Load;
        }

        private void CreateCustomerForm_Load(object sender, EventArgs e)
        {
            _inputGrid = new DataGridView
            {
                Location = new Point(12, 52),
                Size = new Size(505, 120),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            FormGridHelper.SetupEditableInputGrid(_inputGrid, "Customer Name", "Billing Address", "Payment Term");

            var btnSave = new Button { Text = "Save", Location = new Point(350, 380), Size = new Size(75, 28) };
            var btnCancel = new Button { Text = "Cancel", Location = new Point(260, 380), Size = new Size(75, 28) };
            btnSave.Click += BtnSave_Click;
            btnCancel.Click += (s, ev) => Close();
            Controls.Add(_inputGrid);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);

            dataGridView1.Location = new Point(12, 185);
            dataGridView1.Size = new Size(505, 180);
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetAllCustomers());
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in _inputGrid.Rows)
            {
                if (row.IsNewRow) continue;
                string name = FormGridHelper.GetCellString(row, "Customer Name");
                if (string.IsNullOrEmpty(name)) continue;
                _controller.Insert(new Customer
                {
                    CustomerName = name,
                    BillingAddress = FormGridHelper.GetCellString(row, "Billing Address"),
                    PaymentTerm = FormGridHelper.GetCellString(row, "Payment Term")
                });
                count++;
            }
            CreateFormHelper.ShowSaveResult(this, count, "customer");
            if (count > 0)
            {
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetAllCustomers());
                _inputGrid.Rows.Clear();
                _inputGrid.Rows.Add();
            }
        }
    }
}
