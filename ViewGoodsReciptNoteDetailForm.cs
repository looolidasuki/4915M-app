using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewGoodsReciptNoteDetailForm : Form
    {
        private readonly GoodsReceivedNoteController _controller = new GoodsReceivedNoteController();
        private readonly long? _grnId;

        public ViewGoodsReciptNoteDetailForm(long? grnId = null)
        {
            _grnId = grnId;
            InitializeComponent();
            Load += ViewGoodsReciptNoteDetailForm_Load;
        }

        private void ViewGoodsReciptNoteDetailForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllGoodsReceivedNotes();
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                if (_grnId.HasValue && Convert.ToInt64(row["GRN ID"]) != _grnId.Value) continue;
                long id = Convert.ToInt64(row["GRN ID"]);
                textBox1.Text = row["GRN Code"].ToString();
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetReceivedLines(id));
                break;
            }
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
