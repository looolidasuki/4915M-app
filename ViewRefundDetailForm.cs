using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewRefundDetailForm : Form
    {
        private readonly RefundRequestController _controller = new RefundRequestController();
        private long _recordId;
        private string _recordCode;
        private ViewDetailEditHelper _editHelper;

        public ViewRefundDetailForm()
        {
            InitializeComponent();
            Load += ViewRefundDetailForm_Load;
        }

        private void ViewRefundDetailForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.BindReadOnly(dgvRefundRequests, _controller.GetAllRefundRequests());

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox43, comboBox9 },
                button1, button2, button3,
                SaveRecord, LoadSelectedRefund);
            _editHelper.Initialize();

            if (dgvRefundRequests.Rows.Count > 0)
            {
                dgvRefundRequests.SelectionChanged += DgvRefundRequests_SelectionChanged;
                dgvRefundRequests.Rows[0].Selected = true;
                LoadSelectedRefund();
            }
        }

        private void DgvRefundRequests_SelectionChanged(object sender, EventArgs e)
        {
            if (_editHelper == null || !_editHelper.IsEditing)
                LoadSelectedRefund();
        }

        private void LoadSelectedRefund()
        {
            if (dgvRefundRequests.CurrentRow == null) return;
            _recordCode = FormGridHelper.GetCellString(dgvRefundRequests.CurrentRow, "Request Code");
            DataTable dt = DatabaseConnect.ExecuteQuery(
                @"SELECT refundRequestID, refundAmount, refundReason, status
                  FROM RefundRequest WHERE refundRequestCode = @code",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@code", _recordCode)
                });
            if (dt == null || dt.Rows.Count == 0) return;
            var row = dt.Rows[0];
            _recordId = Convert.ToInt64(row["refundRequestID"]);
            textBox43.Text = _recordCode;
            comboBox9.Text = row["status"].ToString();
        }

        private bool SaveRecord()
        {
            if (!int.TryParse(comboBox9.Text, out int status)) status = 0;
            DataTable dt = DatabaseConnect.ExecuteQuery(
                "SELECT refundAmount, refundReason FROM RefundRequest WHERE refundRequestID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                });
            decimal amount = 0;
            string reason = "";
            if (dt != null && dt.Rows.Count > 0)
            {
                amount = Convert.ToDecimal(dt.Rows[0]["refundAmount"]);
                reason = dt.Rows[0]["refundReason"].ToString();
            }
            bool ok = EntityUpdateController.UpdateRefund(_recordId, status, reason, amount);
            ViewDetailLoader.ShowSavedMessage(ok, "Refund");
            if (ok)
            {
                FormGridHelper.BindReadOnly(dgvRefundRequests, _controller.GetAllRefundRequests());
                DialogResult = DialogResult.OK;
            }
            return ok;
        }
    }
}
