using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Sales_user
{
    public partial class MainMenuForm : Form
    {
        public MainMenuForm()
        {
            InitializeComponent();
            string mysqlCon = "server=127.0.0.1; user=root; database=4915m; passoword=";
            MySqlConnection mySqlConnection = new MySqlConnection(mysqlCon);
            try
            {
                mySqlConnection.Open();
                MessageBox.Show("成功連接到MySQL資料庫！");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally { mySqlConnection.Close(); 
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
            using (CreateSalesOrderForm secondForm = new CreateSalesOrderForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            using (ConfirmSalesOrderForm secondForm = new ConfirmSalesOrderForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (CreateQuotationForm secondForm = new CreateQuotationForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            using (CreateCustomerForm secondForm = new CreateCustomerForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage4_Click(object sender, EventArgs e)
        {

        }

        private void salesOrderViewDetail_Click(object sender, EventArgs e)
        {
            using (ViewSalesOrderDetailForm secondForm = new ViewSalesOrderDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewCustomerDetail_Click(object sender, EventArgs e)
        {
            using (ViewCustomerDetailForm secondForm = new ViewCustomerDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void ViewQuotationDetail_Click(object sender, EventArgs e)
        {
            using (ViewQuotationDetailForm secondForm = new ViewQuotationDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createInvoice_Click(object sender, EventArgs e)
        {
            using (CreateInvoiceForm secondForm = new CreateInvoiceForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewInvoiceDetail_Click(object sender, EventArgs e)
        {
            using (ViewInvoiceDetailForm secondForm = new ViewInvoiceDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createDeliveryNote_Click(object sender, EventArgs e)
        {
            using (CreateDeliveryNoteForm secondForm = new CreateDeliveryNoteForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewDeliveryNoteDetail_Click(object sender, EventArgs e)
        {
            using (ViewDeliveryNoteForm secondForm = new ViewDeliveryNoteForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewPurchaseOrderDetail_Click(object sender, EventArgs e)
        {
            using (ViewPurchaseOrderDetailForm secondForm = new ViewPurchaseOrderDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createPurchaseOrder_Click(object sender, EventArgs e)
        {
            using (CreatePurchaseOrderForm secondForm = new CreatePurchaseOrderForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createRawMaterialRequest_Click(object sender, EventArgs e)
        {
            using (CreateRawMaterialRequestForm secondForm = new CreateRawMaterialRequestForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewRawMaterailRequestDetail_Click(object sender, EventArgs e)
        {
            using (ViewRawMaterialRequestDetailForm secondForm = new ViewRawMaterialRequestDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createProduct_Click(object sender, EventArgs e)
        {
            using (CreateProductForm secondForm = new CreateProductForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewProductDetail_Click(object sender, EventArgs e)
        {
            using (ViewProductDetailForm secondForm = new ViewProductDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createInternalTransfer_Click(object sender, EventArgs e)
        {
            using (CreateInternalTransferForm secondForm = new CreateInternalTransferForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewInternalTransferDetail_Click(object sender, EventArgs e)
        {
            using (ViewInternalTransferDetailForm secondForm = new ViewInternalTransferDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createRawMaterial_Click(object sender, EventArgs e)
        {
            using (CreateRawMaterialForm secondForm = new CreateRawMaterialForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewRawMaterailDetail_Click(object sender, EventArgs e)
        {
            using (ViewRawMaterailDetailForm secondForm = new ViewRawMaterailDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createProductionOrder_Click(object sender, EventArgs e)
        {
            using (CreateProductionOrderForm secondForm = new CreateProductionOrderForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewProductionDetail_Click(object sender, EventArgs e)
        {
            using (ViewProductionOrderDetailForm secondForm = new ViewProductionOrderDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createWareHouse_Click(object sender, EventArgs e)
        {
            using (CreateWareHouserForm secondForm = new CreateWareHouserForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewWareHouseDetail_Click(object sender, EventArgs e)
        {
            using (VieweWareHouseDetailForm secondForm = new VieweWareHouseDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createGoodsReciptNote_Click(object sender, EventArgs e)
        {
            using (CreateGoodsReciptNoteForm secondForm = new CreateGoodsReciptNoteForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewGoodsReciptNoteDetail_Click(object sender, EventArgs e)
        {
            using (ViewGoodsReciptNoteDetailForm secondForm = new ViewGoodsReciptNoteDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createSupplier_Click(object sender, EventArgs e)
        {
            using (CreateSupplierForm secondForm = new CreateSupplierForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewSupplierDetail_Click(object sender, EventArgs e)
        {
            using (ViewSupplierDetailForm secondForm = new ViewSupplierDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void confirmRefund_Click(object sender, EventArgs e)
        {
            using (ConfirmRefundForm secondForm = new ConfirmRefundForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewRefundDetail_Click(object sender, EventArgs e)
        {
            using (ViewRefundDetailForm secondForm = new ViewRefundDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void createUser_Click(object sender, EventArgs e)
        {
            using (CreateUserForm secondForm = new CreateUserForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }

        private void viewUserDetail_Click(object sender, EventArgs e)
        {
            using (ViewUserDetailForm secondForm = new ViewUserDetailForm())
            {
                // 以對話方塊形式顯示
                secondForm.ShowDialog();
            }
        }
    }
}
