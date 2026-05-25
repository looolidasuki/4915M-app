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

        public ViewDeliveryNoteForm(long? deliveryNoteId = null)
        {
            _deliveryNoteId = deliveryNoteId;
            InitializeComponent();
            Load += ViewDeliveryNoteForm_Load;
        }

        private void ViewDeliveryNoteForm_Load(object sender, EventArgs e)
        {
            DataTable list = _controller.GetAllDeliveryNotes();
            if (list == null || list.Rows.Count == 0) return;
            foreach (DataRow row in list.Rows)
            {
                if (_deliveryNoteId.HasValue && Convert.ToInt64(row["Delivery Note ID"]) != _deliveryNoteId.Value) continue;
                long id = Convert.ToInt64(row["Delivery Note ID"]);
                textBox5.Text = row["Delivery Note Code"].ToString();
                textBox6.Text = row["Status"].ToString();
                FormGridHelper.BindReadOnly(dataGridView1, _controller.GetDeliveryLines(id));
                break;
            }
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
