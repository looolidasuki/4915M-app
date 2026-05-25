using Sales_user.Controllers;
using System.Data;

namespace Sales_user
{
    public static class MainMenuSearchService
    {
        public static DataTable Query(string tabName, SearchFilterCriteria criteria)
        {
            if (criteria == null || !criteria.HasAnyFilter)
            {
                return LoadAll(tabName);
            }

            return EntitySearchController.Search(tabName, criteria);
        }

        public static DataTable LoadAll(string tabName)
        {
            switch (tabName)
            {
                case "Sales Order": return new SalesOrderController().GetAllSalesOrders();
                case "Quotation": return new QuotationController().GetAllQuotations();
                case "Customer": return new CustomerController().GetAllCustomers();
                case "Invoice": return new InvoiceController().GetAllInvoices();
                case "Delivery Note": return new DeliveryNoteController().GetAllDeliveryNotes();
                case "Purchases Order": return new PurchaseOrderController().GetAllPurchaseOrders();
                case "Raw Material Request Note": return new RawMaterialRequestNoteController().GetAllRequestNotes();
                case "Product": return new ProductController().GetAllProducts();
                case "Production Order": return new ProductionOrderController().GetAllProductionOrders();
                case "Ware house": return new WarehouseController().GetAllWarehouses();
                case "Goods Recipt Note": return new GoodsReceivedNoteController().GetAllGoodsReceivedNotes();
                case "Supplier": return new SupplierController().GetAllSuppliers();
                case "Refund": return new RefundRequestController().GetAllRefundRequests();
                case "User Management": return new StaffController().GetAllStaff();
                case "Raw Material": return new RawMaterialController().GetAllRawMaterials();
                default: return null;
            }
        }
    }
}
