using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewProductDetailForm : Form
    {
        private readonly ProductController _controller = new ProductController();
        private readonly long? _productId;

        public ViewProductDetailForm(long? productId = null)
        {
            _productId = productId;
            InitializeComponent();
            Load += ViewProductDetailForm_Load;
        }

        private void ViewProductDetailForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllProducts();
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                if (_productId.HasValue && Convert.ToInt64(row["Product ID"]) != _productId.Value) continue;
                long id = Convert.ToInt64(row["Product ID"]);
                textBox1.Text = row["Product Code"].ToString();
                textBox22.Text = row["Category"].ToString();
                textBox23.Text = row["Style Number"].ToString();
                textBox24.Text = row["Size"].ToString();
                textBox21.Text = row["Color"].ToString();
                textBox20.Text = row["Unit"].ToString();
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetBomLines(id));
                break;
            }
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
