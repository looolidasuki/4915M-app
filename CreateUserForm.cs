using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateUserForm : Form
    {
        private readonly StaffController _controller = new StaffController();

        public CreateUserForm()
        {
            InitializeComponent();
            Load += CreateUserForm_Load;
        }

        private void CreateUserForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1,
                "Username", "Password", "Title", "Department", "First Name", "Last Name",
                "Phone", "Email", "Employ Date (yyyy-MM-dd)");
            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                string username = FormGridHelper.GetCellString(row, "Username");
                if (string.IsNullOrEmpty(username)) continue;
                DateTime employDate = DateTime.Today;
                DateTime.TryParse(FormGridHelper.GetCellString(row, "Employ Date (yyyy-MM-dd)"), out employDate);

                _controller.Insert(new Staff
                {
                    Username = username,
                    Password = FormGridHelper.GetCellString(row, "Password"),
                    Title = FormGridHelper.GetCellString(row, "Title"),
                    Department = FormGridHelper.GetCellString(row, "Department"),
                    FirstName = FormGridHelper.GetCellString(row, "First Name"),
                    LastName = FormGridHelper.GetCellString(row, "Last Name"),
                    Phone = FormGridHelper.GetCellString(row, "Phone"),
                    Email = FormGridHelper.GetCellString(row, "Email"),
                    EmployDate = employDate,
                    Status = 1
                });
                count++;
            }
            CreateFormHelper.ShowSaveResult(this, count, "user");
        }
    }
}
