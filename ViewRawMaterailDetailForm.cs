using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewRawMaterailDetailForm : Form
    {
        private readonly RawMaterialController _controller = new RawMaterialController();
        private readonly long? _rawMaterialId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewRawMaterailDetailForm(long? rawMaterialId = null)
        {
            _rawMaterialId = rawMaterialId;
            InitializeComponent();
            Load += ViewRawMaterailDetailForm_Load;
        }

        private void ViewRawMaterailDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllRawMaterials();
            _recordId = ViewDetailLoader.ResolveRecordId(_rawMaterialId, list, "Raw Material ID", 0);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { txtSize, txtColor, cbCategory, cbStatus },
                button1, button2, button3,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                @"SELECT rawMaterialCode, category, size, color, minimumStockLevel, status
                  FROM RawMaterial WHERE rawMaterialID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                });
            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                txtRawMaterialCode.Text = row["rawMaterialCode"].ToString();
                cbCategory.Text = row["category"].ToString();
                txtSize.Text = row["size"].ToString();
                txtColor.Text = row["color"].ToString();
                cbStatus.Text = row["status"].ToString();
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetSupplierQuotesByMaterial(_recordId));
        }

        private bool SaveRecord()
        {
            if (!int.TryParse(cbStatus.Text, out int status)) status = 1;
            bool ok = EntityUpdateController.UpdateRawMaterial(new RawMaterial
            {
                RawMaterialID = _recordId,
                Category = cbCategory.Text.Trim(),
                Size = txtSize.Text.Trim(),
                Color = txtColor.Text.Trim(),
                Status = status
            });
            ViewDetailLoader.ShowSavedMessage(ok, "Raw Material");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
