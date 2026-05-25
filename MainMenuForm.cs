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
            tcMainMenu_SelectedIndexChanged(tcMainMenu, EventArgs.Empty);
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

                string selectedTabName = mainTabControl.SelectedTab.Text.Trim();
                DataTable data = null;
                DataGridView grid = null;

                switch (selectedTabName)
                {
                    case "Sales Order":
                        grid = dataGridView1;
                        data = new SalesOrderController().GetAllSalesOrders();
                        break;
                    case "Quotation":
                        grid = dataGridView2;
                        data = new QuotationController().GetAllQuotations();
                        break;
                    case "Customer":
                        grid = dataGridView3;
                        data = new CustomerController().GetAllCustomers();
                        break;
                    case "Invoice":
                        grid = dataGridView5;
                        data = new InvoiceController().GetAllInvoices();
                        break;
                    case "Delivery Note":
                        grid = dataGridView6;
                        data = new DeliveryNoteController().GetAllDeliveryNotes();
                        break;
                    case "Purchases Order":
                        grid = dgvPurchaseOrderOrderLine;
                        data = new PurchaseOrderController().GetAllPurchaseOrders();
                        break;
                    case "Raw Material Request Note":
                        grid = dataGridView7;
                        data = new RawMaterialRequestNoteController().GetAllRequestNotes();
                        break;
                    case "Product":
                        grid = dataGridView8;
                        data = new ProductController().GetAllProducts();
                        break;
                    case "Internal Transfer":
                        grid = dgvProductLine;
                        data = null;
                        break;
                    case "Raw Material":
                        grid = dgvRawMaterialSupplierQuote;
                        data = new RawMaterialController().GetAllRawMaterials();
                        break;
                    case "Production Order":
                        grid = dataGridView4;
                        data = new ProductionOrderController().GetAllProductionOrders();
                        break;
                    case "Ware house":
                        grid = dataGridView9;
                        data = new WarehouseController().GetAllWarehouses();
                        break;
                    case "Goods Recipt Note":
                        grid = dataGridView10;
                        data = new GoodsReceivedNoteController().GetAllGoodsReceivedNotes();
                        break;
                    case "Supplier":
                        grid = dataGridView11;
                        data = new SupplierController().GetAllSuppliers();
                        break;
                    case "Refund":
                        grid = dataGridView13;
                        data = new RefundRequestController().GetAllRefundRequests();
                        break;
                    case "User Management":
                        grid = dataGridView12;
                        data = new StaffController().GetAllStaff();
                        break;
                }

                if (grid != null)
                {
                    BindDataGridView(grid, data);
                }
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
