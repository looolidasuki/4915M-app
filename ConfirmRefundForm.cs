using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ConfirmRefundForm : Form
    {
        private readonly RefundRequestController _controller = new RefundRequestController();
        private DataGridView _inputGrid;

        public ConfirmRefundForm()
        {
            InitializeComponent();
            Load += ConfirmRefundForm_Load;
        }

        private void ConfirmRefundForm_Load(object sender, EventArgs e)
        {
            _inputGrid = new DataGridView
            {
                Location = new Point(25, 72),
                Size = new Size(752, 100)
            };
            FormGridHelper.SetupEditableInputGrid(_inputGrid,
                "Refund Amount", "Refund Method", "Refund Reason", "Receipt Voucher ID", "Invoice ID", "Refund Ref");
            Controls.Add(_inputGrid);

            dataGridView1.Location = new Point(25, 185);
            dataGridView1.Size = new Size(752, 220);
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetAllRefundRequests());

            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in _inputGrid.Rows)
            {
                if (row.IsNewRow) continue;
                if (!decimal.TryParse(FormGridHelper.GetCellString(row, "Refund Amount"), out decimal amount)) continue;
                if (!int.TryParse(FormGridHelper.GetCellString(row, "Refund Method"), out int method)) method = 1;
                string reason = FormGridHelper.GetCellString(row, "Refund Reason");
                if (string.IsNullOrEmpty(reason)) reason = "other";

                long? receiptId = null;
                long? invoiceId = null;
                if (long.TryParse(FormGridHelper.GetCellString(row, "Receipt Voucher ID"), out long r)) receiptId = r;
                if (long.TryParse(FormGridHelper.GetCellString(row, "Invoice ID"), out long i)) invoiceId = i;

                string tempCode = "RF-TEMP-" + DateTime.Now.Ticks;
                if (_controller.CreateRefundRequest(new RefundRequest
                {
                    RefundRequestCode = tempCode,
                    StaffID = AppDefaults.StaffId,
                    RefundAmount = amount,
                    RefundMethod = method,
                    RefundReason = reason,
                    RefundRef = FormGridHelper.GetCellString(row, "Refund Ref"),
                    ReceiptVoucherID = receiptId,
                    InvoiceID = invoiceId,
                    Status = 0
                }))
                {
                    count++;
                }
            }
            if (count > 0)
            {
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetAllRefundRequests());
                CreateFormHelper.ShowSaveResult(this, count, "refund request");
            }
            else
            {
                string code = InputPrompt.Show("Approve Refund", "Refund Request Code to approve:");
                if (!string.IsNullOrEmpty(code) && _controller.UpdateStatus(code, 2, AppDefaults.StaffId))
                {
                    MessageBox.Show("Refund approved.", "Success");
                    FormGridHelper.BindReadOnly(dataGridView1, _controller.GetAllRefundRequests());
                    DialogResult = DialogResult.OK;
                }
            }
        }
    }
}
