using Sales_user.Controllers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
            AppDefaults.LoadFromDatabase();
        }

        private long? GetSelectedRecordId()
        {
            DataGridView grid = GetActiveTabGrid();
            if (grid == null) return null;
            return FormGridHelper.GetSelectedId(grid,
                "Order ID", "Quotation ID", "Customer ID", "Invoice ID", "Delivery Note ID",
                "Purchase Order ID", "Request Note ID", "Product ID", "Production Order ID",
                "Warehouse ID", "GRN ID", "Supplier ID", "Request Code", "Staff ID", "Raw Material ID");
        }

        private DataGridView GetActiveTabGrid()
        {
            if (tcMainMenu.SelectedTab == null) return null;
            switch (tcMainMenu.SelectedTab.Text.Trim())
            {
                case "Sales Order": return dataGridView1;
                case "Quotation": return dataGridView2;
                case "Customer": return dataGridView3;
                case "Invoice": return dataGridView5;
                case "Delivery Note": return dataGridView6;
                case "Purchases Order": return dgvPurchaseOrderOrderLine;
                case "Raw Material Request Note": return dataGridView7;
                case "Product": return dataGridView8;
                case "Production Order": return dataGridView4;
                case "Ware house": return dataGridView9;
                case "Goods Recipt Note": return dataGridView10;
                case "Supplier": return dataGridView11;
                case "Refund": return dataGridView13;
                case "User Management": return dataGridView12;
                case "Raw Material": return dgvRawMaterialSupplierQuote;
                default: return null;
            }
        }

        private void ShowChildForm(Form form)
        {
            using (form)
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    tcMainMenu_SelectedIndexChanged(tcMainMenu, EventArgs.Empty);
                }
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void salesOrderAddRecord_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateSalesOrderForm());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ConfirmSalesOrderForm());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateQuotationForm());
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateCustomerForm());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeSearchFilters();
            LoadCurrentTabData();
        }

        private void InitializeSearchFilters()
        {
            WireSearchBox(textBox1);
            WireSearchBox(textBox2);
            WireSearchBox(textBox3);
            WireSearchBox(textBox7);
            WireSearchBox(textBox8);
            WireSearchBox(textBox9);
            WireSearchBox(textBox10);
            WireSearchBox(textBox17);
            WireSearchBox(textBox18);
            WireSearchBox(textBox19);
            WireSearchBox(textBox20);
            WireSearchBox(textBox21);
            WireSearchBox(textBox22);
            WireSearchBox(textBox23);
            WireSearchBox(textBox24);
            WireSearchBox(textBox25);
            WireSearchBox(textBox26);
            WireSearchBox(textBox27);
            WireSearchBox(textBox28);
            WireSearchBox(textBox29);
            WireSearchBox(textBox30);
            WireSearchBox(textBox31);
            WireSearchBox(textBox32);
            WireSearchBox(textBox33);
            WireSearchBox(textBox34);
            WireSearchBox(textBox35);
            WireSearchBox(textBox36);
            WireSearchBox(textBox37);
            WireSearchBox(textBox38);
            WireSearchBox(textBox39);
            WireSearchBox(textBox40);
            WireSearchBox(textBox41);
            WireSearchBox(textBox42);
            WireSearchBox(textBox43);
            WireSearchBox(txtRawMaterialCode);
            WireSearchBox(txtColor);
            WireSearchBox(txtSize);
            WireSearchBox(txtPurchasesOrder);

            WireFilterControl(dateTimePicker4);
            WireFilterControl(dateTimePicker5);
            WireFilterControl(dateTimePicker6);
            WireFilterControl(dateTimePicker7);
            WireFilterControl(dateTimePicker8);
            WireFilterControl(dateTimePicker9);
            WireFilterControl(dateTimePicker10);
            WireFilterControl(dateTimePicker11);
            WireFilterControl(dateTimePicker12);
            WireFilterControl(dateTimePicker13);
            WireFilterControl(dateTimePicker14);
            WireFilterControl(dateTimePicker15);
            WireFilterControl(dateTimePicker16);
            WireFilterControl(dateTimePicker17);
            WireFilterControl(dateTimePicker2);
            WireFilterControl(dateTimePicker3);
            WireFilterControl(dtpRequestDeliveryDate);

            WireFilterControl(comboBox3);
            WireFilterControl(comboBox4);
            WireFilterControl(comboBox5);
            WireFilterControl(comboBox6);
            WireFilterControl(comboBox9);
            WireFilterControl(cbStatus);
            WireFilterControl(cbCategory);
        }

        private void WireSearchBox(TextBox box)
        {
            if (box == null) return;
            box.KeyDown += SearchBox_KeyDown;
        }

        private void WireFilterControl(Control control)
        {
            if (control == null) return;
            if (control is DateTimePicker dtp)
                dtp.ValueChanged += (s, e) => ApplyCurrentTabSearch();
            else if (control is ComboBox cb)
                cb.SelectedIndexChanged += (s, e) => ApplyCurrentTabSearch();
        }

        private void SearchBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                ApplyCurrentTabSearch();
            }
        }

        private SearchFilterCriteria BuildSearchCriteria()
        {
            if (tcMainMenu.SelectedTab == null) return new SearchFilterCriteria();
            string tab = tcMainMenu.SelectedTab.Text.Trim();
            var c = new SearchFilterCriteria();

            switch (tab)
            {
                case "Sales Order":
                    c.Keyword = textBox1.Text;
                    c.FromDate = dateTimePicker5.Value;
                    c.ToDate = dateTimePicker4.Value;
                    c.Status = SearchQueryHelper.ParseStatusCombo(comboBox3.Text);
                    break;
                case "Quotation":
                    c.Keyword = textBox2.Text;
                    c.FromDate = dateTimePicker7.Value;
                    c.ToDate = dateTimePicker6.Value;
                    c.Status = SearchQueryHelper.ParseStatusCombo(comboBox4.Text);
                    break;
                case "Customer":
                    c.Name = textBox3.Text;
                    c.Phone = textBox7.Text;
                    c.Email = textBox8.Text;
                    c.FromDate = dateTimePicker8.Value;
                    c.ToDate = dateTimePicker8.Value;
                    break;
                case "Invoice":
                    c.Keyword = textBox9.Text;
                    c.FromDate = dateTimePicker9.Value;
                    c.ToDate = dateTimePicker9.Value;
                    break;
                case "Delivery Note":
                    c.Keyword = textBox10.Text;
                    c.FromDate = dateTimePicker10.Value;
                    c.ToDate = dateTimePicker10.Value;
                    break;
                case "Purchases Order":
                    c.Keyword = txtPurchasesOrder.Text;
                    c.FromDate = dateTimePicker2.Value;
                    c.ToDate = dtpRequestDeliveryDate.Value;
                    break;
                case "Raw Material Request Note":
                    c.Keyword = textBox19.Text;
                    c.FromDate = dateTimePicker11.Value;
                    c.ToDate = dateTimePicker12.Value;
                    break;
                case "Product":
                    c.Keyword = textBox20.Text;
                    c.Category = textBox21.Text;
                    c.StyleNumber = textBox22.Text;
                    c.Color = textBox23.Text;
                    c.Unit = textBox24.Text;
                    c.Size = textBox17.Text;
                    break;
                case "Production Order":
                    c.Keyword = textBox25.Text;
                    c.FromDate = dateTimePicker3.Value;
                    c.ToDate = dateTimePicker13.Value;
                    c.Status = SearchQueryHelper.ParseStatusCombo(comboBox5.Text);
                    break;
                case "Ware house":
                    c.Keyword = textBox27.Text;
                    c.Name = textBox28.Text;
                    break;
                case "Goods Recipt Note":
                    c.Keyword = textBox30.Text;
                    c.FromDate = dateTimePicker15.Value;
                    c.ToDate = dateTimePicker15.Value;
                    break;
                case "Supplier":
                    c.Keyword = textBox36.Text;
                    c.Name = textBox35.Text;
                    c.Phone = textBox37.Text;
                    c.Email = textBox38.Text;
                    break;
                case "Refund":
                    c.Keyword = textBox43.Text;
                    c.FromDate = dateTimePicker17.Value;
                    c.ToDate = dateTimePicker16.Value;
                    c.Status = SearchQueryHelper.ParseStatusCombo(comboBox9.Text);
                    break;
                case "User Management":
                    c.Name = textBox41.Text;
                    c.Email = textBox40.Text;
                    c.Phone = textBox39.Text;
                    c.FromDate = dateTimePicker14.Value;
                    break;
                case "Raw Material":
                    c.Keyword = txtRawMaterialCode.Text;
                    c.Color = txtColor.Text;
                    c.Size = txtSize.Text;
                    c.Category = cbCategory.Text;
                    if (int.TryParse(cbStatus.Text, out int rmStatus)) c.Status = rmStatus;
                    break;
            }
            return c;
        }

        private void LoadCurrentTabData()
        {
            if (tcMainMenu.SelectedTab == null) return;
            string tab = tcMainMenu.SelectedTab.Text.Trim();
            DataGridView grid = GetActiveTabGrid();
            if (grid == null) return;
            DataTable data = MainMenuSearchService.LoadAll(tab);
            BindDataGridView(grid, data);
        }

        private void ApplyCurrentTabSearch()
        {
            if (tcMainMenu.SelectedTab == null) return;
            string tab = tcMainMenu.SelectedTab.Text.Trim();
            DataGridView grid = GetActiveTabGrid();
            if (grid == null) return;
            SearchFilterCriteria criteria = BuildSearchCriteria();
            DataTable data = EntitySearchController.Search(tab, criteria);
            BindDataGridView(grid, data);
        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void salesOrderViewDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewSalesOrderDetailForm(GetSelectedRecordId()));
        }

        private void viewCustomerDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewCustomerDetailForm(GetSelectedRecordId()));
        }

        private void ViewQuotationDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewQuotationDetailForm(GetSelectedRecordId()));
        }

        private void createInvoice_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateInvoiceForm());
        }

        private void viewInvoiceDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewInvoiceDetailForm(GetSelectedRecordId()));
        }

        private void createDeliveryNote_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateDeliveryNoteForm());
        }

        private void viewDeliveryNoteDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewDeliveryNoteForm(GetSelectedRecordId()));
        }

        private void viewPurchaseOrderDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewPurchaseOrderDetailForm(GetSelectedRecordId()));
        }

        private void createPurchaseOrder_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreatePurchaseOrderForm());
        }

        private void createRawMaterialRequest_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateRawMaterialRequestForm());
        }

        private void viewRawMaterailRequestDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewRawMaterialRequestDetailForm(GetSelectedRecordId()));
        }

        private void createProduct_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateProductForm());
        }

        private void viewProductDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewProductDetailForm(GetSelectedRecordId()));
        }

        private void createInternalTransfer_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateInternalTransferForm());
        }

        private void viewInternalTransferDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewInternalTransferDetailForm());
        }

        private void createRawMaterial_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateRawMaterialForm());
        }

        private void viewRawMaterailDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewRawMaterailDetailForm(GetSelectedRecordId()));
        }

        private void createProductionOrder_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateProductionOrderForm());
        }

        private void viewProductionDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewProductionOrderDetailForm(GetSelectedRecordId()));
        }

        private void createWareHouse_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateWareHouserForm());
        }

        private void viewWareHouseDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new VieweWareHouseDetailForm(GetSelectedRecordId()));
        }

        private void createGoodsReciptNote_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateGoodsReciptNoteForm());
        }

        private void viewGoodsReciptNoteDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewGoodsReciptNoteDetailForm(GetSelectedRecordId()));
        }

        private void createSupplier_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateSupplierForm());
        }

        private void viewSupplierDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewSupplierDetailForm(GetSelectedRecordId()));
        }

        private void confirmRefund_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ConfirmRefundForm());
        }

        private void viewRefundDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewRefundDetailForm());
        }

        private void createUser_Click(object sender, EventArgs e)
        {
            ShowChildForm(new CreateUserForm());
        }

        private void viewUserDetail_Click(object sender, EventArgs e)
        {
            ShowChildForm(new ViewUserDetailForm(GetSelectedRecordId()));
        }


        /// <summary>
        /// 當主選單切換不同的功能分頁時，載入對應 DataGridView 資料
        /// </summary>
        private void tcMainMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TabControl mainTabControl = sender as TabControl ?? tcMainMenu;
                if (mainTabControl?.SelectedTab == null) return;

                LoadCurrentTabData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"System Error: {ex.Message}", "Menu Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BindDataGridView(DataGridView grid, DataTable data)
        {
            ConfigureDataGridView(grid);
            grid.DataSource = data;
        }

        private void ConfigureDataGridView(DataGridView grid)
        {
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.AutoGenerateColumns = true;
        }
    }
}
