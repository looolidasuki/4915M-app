using Sales_user.Controllers;
using Sales_user.Models;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class CreateProductionOrderForm : Form
    {
        private readonly ProductionOrderController _controller = new ProductionOrderController();

        public CreateProductionOrderForm()
        {
            InitializeComponent();
            Load += CreateProductionOrderForm_Load;
        }

        private void CreateProductionOrderForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.SetupEditableInputGrid(dataGridView1,
                "Sales Order ID", "Est Finish Date (yyyy-MM-dd)", "Status", "Remark");
            CreateFormHelper.WireCancel(button1, this);
            button2.Click += BtnSave_Click;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.IsNewRow) continue;
                if (!long.TryParse(FormGridHelper.GetCellString(row, "Sales Order ID"), out long soId)) continue;
                DateTime finish = DateTime.Now.AddDays(7);
                DateTime.TryParse(FormGridHelper.GetCellString(row, "Est Finish Date (yyyy-MM-dd)"), out finish);
                if (!int.TryParse(FormGridHelper.GetCellString(row, "Status"), out int status)) status = 0;

                long id = _controller.Insert(new ProductionOrder
                {
                    ProductionOrderCode = "TEMP",
                    SalesOrderID = soId,
                    StaffID = AppDefaults.StaffId,
                    EstFinishDate = finish,
                    Status = status,
                    Remark = FormGridHelper.GetCellString(row, "Remark")
                });
                if (id > 0)
                {
                    _controller.UpdateCodeAfterInsert(id);
                    count++;
                }
            }
            CreateFormHelper.ShowSaveResult(this, count, "production order");
        }
    }
}
