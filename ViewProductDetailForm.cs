using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewProductDetailForm : Form
    {
        private readonly ProductController _controller = new ProductController();
        private readonly long? _productId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewProductDetailForm(long? productId = null)
        {
            _productId = productId;
            InitializeComponent();
            Load += ViewProductDetailForm_Load;
        }

        private void ViewProductDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllProducts();
            _recordId = ViewDetailLoader.ResolveRecordId(_productId, list, "Product ID", 0);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox22, textBox23, textBox24, textBox21, textBox20 },
                button1, button2, button3,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                @"SELECT productCode, category, styleNumber, size, color, unit, basePriceByCurrency, status
                  FROM Product WHERE productID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                });
            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                textBox1.Text = row["productCode"].ToString();
                textBox22.Text = row["category"].ToString();
                textBox23.Text = row["styleNumber"].ToString();
                textBox24.Text = row["size"].ToString();
                textBox21.Text = row["color"].ToString();
                textBox20.Text = row["unit"].ToString();
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetBomLines(_recordId));
        }

        private bool SaveRecord()
        {
            decimal price = 0;
            int status = 1;
            DataTable cur = DatabaseConnect.ExecuteQuery(
                "SELECT basePriceByCurrency, status FROM Product WHERE productID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                });
            if (cur != null && cur.Rows.Count > 0)
            {
                price = Convert.ToDecimal(cur.Rows[0]["basePriceByCurrency"]);
                status = Convert.ToInt32(cur.Rows[0]["status"]);
            }
            bool ok = EntityUpdateController.UpdateProduct(new Product
            {
                ProductID = _recordId,
                Category = textBox22.Text.Trim(),
                StyleNumber = textBox23.Text.Trim(),
                Size = textBox24.Text.Trim(),
                Color = textBox21.Text.Trim(),
                Unit = textBox20.Text.Trim(),
                BasePriceByCurrency = price,
                Status = status
            });
            ViewDetailLoader.ShowSavedMessage(ok, "Product");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
