using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewUserDetailForm : Form
    {
        private readonly StaffController _controller = new StaffController();
        private readonly long? _staffId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewUserDetailForm(long? staffId = null)
        {
            _staffId = staffId;
            InitializeComponent();
            Load += ViewUserDetailForm_Load;
        }

        private void ViewUserDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllStaff();
            _recordId = ViewDetailLoader.ResolveRecordId(_staffId, list, "Staff ID", AppDefaults.StaffId);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox39, textBox40, textBox41, textBox42 },
                button1, button2, button3,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                @"SELECT firstName, lastName, email, phone, department, employDate
                  FROM Staff WHERE staffID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                });
            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                textBox39.Text = row["firstName"] + " " + row["lastName"];
                textBox40.Text = row["email"].ToString();
                textBox41.Text = row["phone"].ToString();
                textBox42.Text = row["department"].ToString();
                if (row["employDate"] != DBNull.Value)
                    dateTimePicker14.Value = Convert.ToDateTime(row["employDate"]);
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetAllStaff());
        }

        private bool SaveRecord()
        {
            string[] nameParts = textBox39.Text.Trim().Split(new[] { ' ' }, 2);
            bool ok = EntityUpdateController.UpdateStaff(new Staff
            {
                StaffID = _recordId,
                FirstName = nameParts.Length > 0 ? nameParts[0] : "",
                LastName = nameParts.Length > 1 ? nameParts[1] : "",
                Email = textBox40.Text.Trim(),
                Phone = textBox41.Text.Trim(),
                Department = textBox42.Text.Trim(),
                Title = ""
            });
            ViewDetailLoader.ShowSavedMessage(ok, "User");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
