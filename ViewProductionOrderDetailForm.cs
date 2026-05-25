using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewProductionOrderDetailForm : Form
    {
        private readonly ProductionOrderController _controller = new ProductionOrderController();
        private readonly long? _productionOrderId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewProductionOrderDetailForm(long? productionOrderId = null)
        {
            _productionOrderId = productionOrderId;
            InitializeComponent();
            Load += ViewProductionOrderDetailForm_Load;
        }

        private void ViewProductionOrderDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllProductionOrders();
            _recordId = ViewDetailLoader.ResolveRecordId(_productionOrderId, list, "Production Order ID", 0);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox26, textBox25, comboBox5 },
                button1, button2, button3,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                "SELECT productionOrderCode, salesOrderID, status, remark FROM ProductionOrder WHERE productionOrderID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                });
            if (dt != null && dt.Rows.Count > 0)
            {
                textBox26.Text = dt.Rows[0]["productionOrderCode"].ToString();
                textBox25.Text = dt.Rows[0]["salesOrderID"].ToString();
                comboBox5.Text = dt.Rows[0]["status"].ToString();
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetProductLines(_recordId));
        }

        private bool SaveRecord()
        {
            if (!int.TryParse(comboBox5.Text, out int status)) status = 0;
            bool ok = EntityUpdateController.UpdateProductionOrder(_recordId, status, "");
            ViewDetailLoader.ShowSavedMessage(ok, "Production Order");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
