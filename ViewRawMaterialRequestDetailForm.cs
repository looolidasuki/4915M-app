using MySql.Data.MySqlClient;
using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewRawMaterialRequestDetailForm : Form
    {
        private readonly RawMaterialRequestNoteController _controller = new RawMaterialRequestNoteController();
        private readonly long? _noteId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewRawMaterialRequestDetailForm(long? noteId = null)
        {
            _noteId = noteId;
            InitializeComponent();
            Load += ViewRawMaterialRequestDetailForm_Load;
        }

        private void ViewRawMaterialRequestDetailForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllRequestNotes();
            _recordId = ViewDetailLoader.ResolveRecordId(_noteId, list, "Request Note ID", 0);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox18, textBox19 },
                button1, button2, button3,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                "SELECT rawMaterialRequestNoteCode, remark, requestDate FROM RawMaterialRequestNote WHERE rawMaterialRequestNoteID = @id",
                new MySqlParameter[] { new MySqlParameter("@id", _recordId) });
            if (dt != null && dt.Rows.Count > 0)
            {
                textBox19.Text = dt.Rows[0]["rawMaterialRequestNoteCode"].ToString();
                textBox18.Text = dt.Rows[0]["remark"]?.ToString();
                if (dt.Rows[0]["requestDate"] != DBNull.Value)
                    dateTimePicker12.Value = Convert.ToDateTime(dt.Rows[0]["requestDate"]);
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetRequestLines(_recordId));
        }

        private bool SaveRecord()
        {
            bool ok = DatabaseConnect.ExecuteNonQuery(
                "UPDATE RawMaterialRequestNote SET remark = @remark WHERE rawMaterialRequestNoteID = @id",
                new MySqlParameter[] {
                    new MySqlParameter("@remark", textBox18.Text.Trim()),
                    new MySqlParameter("@id", _recordId)
                }) > 0;
            ViewDetailLoader.ShowSavedMessage(ok, "Raw Material Request");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
