using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewRawMaterailDetailForm : Form
    {
        private readonly RawMaterialController _controller = new RawMaterialController();
        private readonly long? _rawMaterialId;

        public ViewRawMaterailDetailForm(long? rawMaterialId = null)
        {
            _rawMaterialId = rawMaterialId;
            InitializeComponent();
            Load += ViewRawMaterailDetailForm_Load;
        }

        private void ViewRawMaterailDetailForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllRawMaterials();
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                if (_rawMaterialId.HasValue && Convert.ToInt64(row["Raw Material ID"]) != _rawMaterialId.Value) continue;
                long id = Convert.ToInt64(row["Raw Material ID"]);
                txtRawMaterialCode.Text = row["Raw Material Code"].ToString();
                txtSize.Text = row["Size"].ToString();
                txtColor.Text = row["Color"].ToString();
                cbCategory.Text = row["Category"].ToString();
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetSupplierQuotesByMaterial(id));
                break;
            }
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
