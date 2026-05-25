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

        public ViewRawMaterialRequestDetailForm(long? noteId = null)
        {
            _noteId = noteId;
            InitializeComponent();
            Load += ViewRawMaterialRequestDetailForm_Load;
        }

        private void ViewRawMaterialRequestDetailForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllRequestNotes();
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                long id = _noteId ?? Convert.ToInt64(row["Request Note ID"]);
                if (_noteId.HasValue && id != _noteId.Value) continue;
                textBox19.Text = row["Request Code"].ToString();
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetRequestLines(id));
                break;
            }
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
