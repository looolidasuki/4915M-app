using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewSupplierDetailForm : Form
    {
        private readonly SupplierController _controller = new SupplierController();
        private readonly long? _supplierId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewSupplierDetailForm(long? supplierId = null)
        {
            _supplierId = supplierId;
            InitializeComponent();
            Load += ViewSupplierDetailForm_Load;
        }

        private void ViewSupplierDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllSuppliers();
            _recordId = ViewDetailLoader.ResolveRecordId(_supplierId, list, "Supplier ID", AppDefaults.SupplierId);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox36, textBox35, textBox34, textBox37, textBox38 },
                button1, button2, button3,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                @"SELECT supplierName, contactPerson, billingAddress, phone, email
                  FROM Supplier WHERE supplierID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                });
            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                textBox36.Text = row["supplierName"].ToString();
                textBox35.Text = row["contactPerson"].ToString();
                textBox34.Text = row["billingAddress"].ToString();
                textBox37.Text = row["phone"].ToString();
                textBox38.Text = row["email"].ToString();
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetRawMaterialQuotesBySupplier(_recordId));
        }

        private bool SaveRecord()
        {
            bool ok = EntityUpdateController.UpdateSupplier(new Supplier
            {
                SupplierID = _recordId,
                SupplierName = textBox36.Text.Trim(),
                ContactPerson = textBox35.Text.Trim(),
                BillingAddress = textBox34.Text.Trim(),
                Phone = textBox37.Text.Trim(),
                Email = textBox38.Text.Trim()
            });
            ViewDetailLoader.ShowSavedMessage(ok, "Supplier");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
