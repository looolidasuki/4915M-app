using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewSupplierDetailForm : Form
    {
        private readonly SupplierController _controller = new SupplierController();
        private readonly long? _supplierId;

        public ViewSupplierDetailForm(long? supplierId = null)
        {
            _supplierId = supplierId;
            InitializeComponent();
            Load += ViewSupplierDetailForm_Load;
        }

        private void ViewSupplierDetailForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllSuppliers();
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                if (_supplierId.HasValue && Convert.ToInt64(row["Supplier ID"]) != _supplierId.Value) continue;
                long id = Convert.ToInt64(row["Supplier ID"]);
                textBox36.Text = row["Supplier Name"].ToString();
                textBox35.Text = row["Contact Person"].ToString();
                textBox34.Text = row["Billing Address"].ToString();
                textBox37.Text = row["Phone"].ToString();
                textBox38.Text = row["Email"].ToString();
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetRawMaterialQuotesBySupplier(id));
                break;
            }
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
