using MySql.Data.MySqlClient;
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
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewGoodsReciptNoteDetailForm(long? grnId = null)
        {
            _grnId = grnId;
            InitializeComponent();
            Load += ViewGoodsReciptNoteDetailForm_Load;
        }

        private void ViewGoodsReciptNoteDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllGoodsReceivedNotes();
            _recordId = ViewDetailLoader.ResolveRecordId(_grnId, list, "GRN ID", 0);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox1, comboBox3 },
                button1, button2, button3,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                "SELECT goodsReceivedNoteCode, status, remark FROM GoodsReceivedNote WHERE goodsReceivedNoteID = @id",
                new MySqlParameter[] { new MySqlParameter("@id", _recordId) });
            if (dt != null && dt.Rows.Count > 0)
            {
                textBox1.Text = dt.Rows[0]["goodsReceivedNoteCode"].ToString();
                comboBox3.Text = dt.Rows[0]["status"].ToString();
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetReceivedLines(_recordId));
        }

        private bool SaveRecord()
        {
            if (!int.TryParse(comboBox3.Text, out int status)) status = 0;
            bool ok = DatabaseConnect.ExecuteNonQuery(
                "UPDATE GoodsReceivedNote SET status = @status, lastModifyDate = NOW() WHERE goodsReceivedNoteID = @id",
                new MySqlParameter[] {
                    new MySqlParameter("@status", status),
                    new MySqlParameter("@id", _recordId)
                }) > 0;
            ViewDetailLoader.ShowSavedMessage(ok, "Goods Received Note");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
