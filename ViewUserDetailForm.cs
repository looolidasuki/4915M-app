using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewUserDetailForm : Form
    {
        private readonly StaffController _controller = new StaffController();
        private readonly long? _staffId;

        public ViewUserDetailForm(long? staffId = null)
        {
            _staffId = staffId;
            InitializeComponent();
            Load += ViewUserDetailForm_Load;
        }

        private void ViewUserDetailForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllStaff();
            FormGridHelper.BindReadOnly(dataGridView1, list);
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                if (_staffId.HasValue && Convert.ToInt64(row["Staff ID"]) != _staffId.Value) continue;
                textBox39.Text = row["Name"].ToString();
                textBox40.Text = row["Email"].ToString();
                textBox41.Text = row["Phone"].ToString();
                textBox42.Text = row["Department"].ToString();
                break;
            }
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
