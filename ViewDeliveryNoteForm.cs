using Sales_user.Controllers;
using System;
using System.Data;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewDeliveryNoteForm : Form
    {
        private readonly DeliveryNoteController _controller = new DeliveryNoteController();
        private readonly long? _deliveryNoteId;
        private long _recordId;
        private ViewDetailEditHelper _editHelper;

        public ViewDeliveryNoteForm(long? deliveryNoteId = null)
        {
            _deliveryNoteId = deliveryNoteId;
            InitializeComponent();
            Load += ViewDeliveryNoteForm_Load;
        }

        private void ViewDeliveryNoteForm_Load(object sender, EventArgs e)
        {
            var list = _controller.GetAllDeliveryNotes();
            _recordId = ViewDetailLoader.ResolveRecordId(_deliveryNoteId, list, "Delivery Note ID", 0);
            LoadRecord();

            _editHelper = new ViewDetailEditHelper(
                new Control[] { textBox5, textBox6, textBox10 },
                button1, button2, button3,
                SaveRecord, LoadRecord);
            _editHelper.Initialize();
        }

        private void LoadRecord()
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(
                @"SELECT deliveryNoteCode, status, trackingNumber, staffID, createDate
                  FROM DeliveryNote WHERE deliveryNoteID = @id",
                new MySql.Data.MySqlClient.MySqlParameter[] {
                    new MySql.Data.MySqlClient.MySqlParameter("@id", _recordId)
                });
            if (dt != null && dt.Rows.Count > 0)
            {
                var row = dt.Rows[0];
                textBox5.Text = row["deliveryNoteCode"].ToString();
                textBox6.Text = row["status"].ToString();
                textBox10.Text = row["staffID"].ToString();
                if (row["createDate"] != DBNull.Value)
                    dateTimePicker9.Value = Convert.ToDateTime(row["createDate"]);
            }
            FormGridHelper.BindReadOnly(dataGridView1, _controller.GetDeliveryLines(_recordId));
        }

        private bool SaveRecord()
        {
            if (!int.TryParse(textBox6.Text, out int status)) status = 0;
            bool ok = EntityUpdateController.UpdateDeliveryNote(_recordId, textBox5.Text.Trim(), status, "");
            ViewDetailLoader.ShowSavedMessage(ok, "Delivery Note");
            if (ok) DialogResult = DialogResult.OK;
            return ok;
        }
    }
}
