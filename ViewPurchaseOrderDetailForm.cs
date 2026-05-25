using MySql.Data.MySqlClient;
using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewPurchaseOrderDetailForm : Form
    {
        private readonly PurchaseOrderController _controller = new PurchaseOrderController();
        private readonly long? _purchaseOrderId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewPurchaseOrderDetailForm(long? purchaseOrderId = null)
        {
            _purchaseOrderId = purchaseOrderId;
            InitializeComponent();
            Load += ViewPurchaseOrderDetailForm_Load;
        }

        private void ViewPurchaseOrderDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllPurchaseOrders();
            _recordId = ViewDetailLoader.ResolveRecordId(_purchaseOrderId, list, "Purchase Order ID", 0);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { txtReference, comboBox2 },
                button1, button2, button3,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                @"SELECT purchaseOrderCode, status, remark FROM PurchaseOrder WHERE purchaseOrderID = @id",
                new MySqlParameter[] { new MySqlParameter("@id", _recordId) });
            if (dt != null && dt.Rows.Count > 0)
            {
                txtPurchasesOrder.Text = dt.Rows[0]["purchaseOrderCode"].ToString();
                txtReference.Text = dt.Rows[0]["remark"].ToString();
                comboBox2.Text = dt.Rows[0]["status"].ToString();
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetAllPurchaseOrderLines());
        }

        private bool SaveRecord()
        {
            if (!int.TryParse(comboBox2.Text, out int status)) status = 0;
            bool ok = DatabaseConnect.ExecuteNonQuery(
                "UPDATE PurchaseOrder SET remark = @remark, status = @status, lastModifyDate = NOW() WHERE purchaseOrderID = @id",
                new MySqlParameter[] {
                    new MySqlParameter("@remark", txtReference.Text.Trim()),
                    new MySqlParameter("@status", status),
                    new MySqlParameter("@id", _recordId)
                }) > 0;
            ViewDetailLoader.ShowSavedMessage(ok, "Purchase Order");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
