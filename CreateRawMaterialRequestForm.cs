using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateRawMaterialRequestForm : Form
    {
        private readonly RawMaterialRequestNoteController _controller = new RawMaterialRequestNoteController();

        public CreateRawMaterialRequestForm()
        {
            InitializeComponent();
            Load += CreateRawMaterialRequestForm_Load;
        }

        private void CreateRawMaterialRequestForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1,
                "Production Order ID", "Request Date (yyyy-MM-dd)", "Remark");
            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                if (!long.TryParse(FormGridHelper.GetCellString(row, "Production Order ID"), out long poId)) continue;
                DateTime requestDate = DateTime.Today;
                DateTime.TryParse(FormGridHelper.GetCellString(row, "Request Date (yyyy-MM-dd)"), out requestDate);

                long id = _controller.Insert(new RawMaterialRequestNote
                {
                    RawMaterialRequestNoteCode = "TEMP",
                    ProductionOrderID = poId,
                    StaffID = AppDefaults.StaffId,
                    RequestDate = requestDate,
                    Remark = FormGridHelper.GetCellString(row, "Remark")
                });
                if (id > 0)
                {
                    _controller.UpdateCodeAfterInsert(id);
                    count++;
                }
            }
            CreateFormHelper.ShowSaveResult(this, count, "raw material request");
        }
    }
}
