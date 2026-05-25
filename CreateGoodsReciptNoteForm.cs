using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateGoodsReciptNoteForm : Form
    {
        private readonly GoodsReceivedNoteController _controller = new GoodsReceivedNoteController();

        public CreateGoodsReciptNoteForm()
        {
            InitializeComponent();
            Load += CreateGoodsReciptNoteForm_Load;
        }

        private void CreateGoodsReciptNoteForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1,
                "Supplier ID", "Purchase Order ID", "Status", "Remark");
            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                if (!long.TryParse(FormGridHelper.GetCellString(row, "Purchase Order ID"), out long poId)) continue;
                long supplierId = AppDefaults.SupplierId;
                long.TryParse(FormGridHelper.GetCellString(row, "Supplier ID"), out supplierId);
                if (!int.TryParse(FormGridHelper.GetCellString(row, "Status"), out int status)) status = 0;

                long id = _controller.Insert(new GoodsReceivedNote
                {
                    GoodsReceivedNoteCode = "TEMP",
                    SupplierID = supplierId,
                    PurchaseOrderID = poId,
                    StaffID = AppDefaults.StaffId,
                    Status = status,
                    Remark = FormGridHelper.GetCellString(row, "Remark")
                });
                if (id > 0)
                {
                    _controller.UpdateCodeAfterInsert(id);
                    count++;
                }
            }
            CreateFormHelper.ShowSaveResult(this, count, "goods received note");
        }
    }
}
