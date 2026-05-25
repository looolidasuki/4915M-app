using MySql.Data.MySqlClient;
using Sales_user.Controllers;
using System.Collections.Generic;
using System.Data;

namespace Sales_user
{
    /// <summary>
    /// Centralized search queries used by MainMenu filter UI.
    /// </summary>
    public static class EntitySearchController
    {
        public static DataTable Search(string tabName, SearchFilterCriteria filter)
        {
            if (filter == null || !filter.HasAnyFilter)
                return MainMenuSearchService.LoadAll(tabName);

            switch (tabName)
            {
                case "Sales Order": return new SalesOrderController().Search(filter);
                case "Quotation": return SearchQuotations(filter);
                case "Customer": return new CustomerController().Search(filter);
                case "Invoice": return SearchInvoices(filter);
                case "Delivery Note": return SearchDeliveryNotes(filter);
                case "Purchases Order": return SearchPurchaseOrders(filter);
                case "Raw Material Request Note": return SearchRawMaterialRequests(filter);
                case "Product": return SearchProducts(filter);
                case "Production Order": return SearchProductionOrders(filter);
                case "Ware house": return SearchWarehouses(filter);
                case "Goods Recipt Note": return SearchGrn(filter);
                case "Supplier": return SearchSuppliers(filter);
                case "Refund": return SearchRefunds(filter);
                case "User Management": return SearchStaff(filter);
                case "Raw Material": return SearchRawMaterials(filter);
                default: return MainMenuSearchService.LoadAll(tabName);
            }
        }

        private static DataTable SearchQuotations(SearchFilterCriteria filter)
        {
            string sql = @"SELECT q.quotationID AS 'Quotation ID', q.quotationCode AS 'Quotation Code',
                                  c.customerName AS 'Customer', q.createDate AS 'Create Date', q.status AS 'Status'
                           FROM Quotation q LEFT JOIN Customer c ON q.customerID = c.customerID WHERE 1=1";
            return ApplyCommonFilters(sql, filter, "q.createDate", "q.status",
                "(q.quotationCode LIKE @kw OR c.customerName LIKE @kw)");
        }

        private static DataTable SearchInvoices(SearchFilterCriteria filter)
        {
            string sql = @"SELECT invoiceID AS 'Invoice ID', invoiceCode AS 'Invoice Code',
                                  customerID AS 'Customer ID', salesOrderID AS 'Sales Order ID',
                                  invoiceType AS 'Invoice Type', createDate AS 'Create Date', status AS 'Status'
                           FROM Invoice WHERE 1=1";
            return ApplyCommonFilters(sql, filter, "createDate", "status",
                "(invoiceCode LIKE @kw OR CAST(customerID AS CHAR) LIKE @kw OR CAST(salesOrderID AS CHAR) LIKE @kw)");
        }

        private static DataTable SearchDeliveryNotes(SearchFilterCriteria filter)
        {
            string sql = @"SELECT deliveryNoteID AS 'Delivery Note ID', deliveryNoteCode AS 'Delivery Note Code',
                                  customerID AS 'Customer ID', SalesOrderID AS 'Sales Order ID',
                                  trackingNumber AS 'Tracking Number', createDate AS 'Create Date', status AS 'Status'
                           FROM DeliveryNote WHERE 1=1";
            return ApplyCommonFilters(sql, filter, "createDate", "status",
                "(deliveryNoteCode LIKE @kw OR trackingNumber LIKE @kw OR CAST(SalesOrderID AS CHAR) LIKE @kw)");
        }

        private static DataTable SearchPurchaseOrders(SearchFilterCriteria filter)
        {
            string sql = @"SELECT po.purchaseOrderID AS 'Purchase Order ID', po.purchaseOrderCode AS 'Purchase Order Code',
                                  s.supplierName AS 'Supplier', po.createDate AS 'Create Date',
                                  po.requestDeliveryDate AS 'Request Delivery Date', po.status AS 'Status'
                           FROM PurchaseOrder po LEFT JOIN Supplier s ON po.supplierID = s.supplierID WHERE 1=1";
            return ApplyCommonFilters(sql, filter, "po.createDate", "po.status",
                "(po.purchaseOrderCode LIKE @kw OR s.supplierName LIKE @kw)");
        }

        private static DataTable SearchRawMaterialRequests(SearchFilterCriteria filter)
        {
            string sql = @"SELECT rawMaterialRequestNoteID AS 'Request Note ID',
                                  rawMaterialRequestNoteCode AS 'Request Code',
                                  ProductionOrderID AS 'Production Order ID',
                                  createDate AS 'Create Date', requestDate AS 'Request Date'
                           FROM RawMaterialRequestNote WHERE 1=1";
            return ApplyCommonFilters(sql, filter, "createDate", null,
                "(rawMaterialRequestNoteCode LIKE @kw OR CAST(ProductionOrderID AS CHAR) LIKE @kw)");
        }

        private static DataTable SearchProducts(SearchFilterCriteria filter)
        {
            string sql = @"SELECT productID AS 'Product ID', productCode AS 'Product Code', category AS 'Category',
                                  styleNumber AS 'Style Number', size AS 'Size', color AS 'Color',
                                  basePriceByCurrency AS 'Base Price', unit AS 'Unit', status AS 'Status'
                           FROM Product WHERE 1=1";
            var conditions = new List<string>();
            var parameters = new List<MySqlParameter>();
            SearchQueryHelper.AddLike(conditions, parameters, "productCode", filter.Keyword ?? filter.Name, "@code");
            SearchQueryHelper.AddLike(conditions, parameters, "category", filter.Category, "@cat");
            SearchQueryHelper.AddLike(conditions, parameters, "styleNumber", filter.StyleNumber, "@style");
            SearchQueryHelper.AddLike(conditions, parameters, "color", filter.Color, "@color");
            SearchQueryHelper.AddLike(conditions, parameters, "unit", filter.Unit, "@unit");
            SearchQueryHelper.AddLike(conditions, parameters, "size", filter.Size, "@size");
            if (conditions.Count > 0) sql += " AND " + string.Join(" AND ", conditions);
            sql += " ORDER BY createDate DESC";
            return DatabaseConnect.ExecuteQuery(sql, parameters.ToArray());
        }

        private static DataTable SearchProductionOrders(SearchFilterCriteria filter)
        {
            string sql = @"SELECT productionOrderID AS 'Production Order ID', productionOrderCode AS 'Production Order Code',
                                  salesOrderID AS 'Sales Order ID', createDate AS 'Create Date',
                                  estFinishDate AS 'Est. Finish Date', status AS 'Status'
                           FROM ProductionOrder WHERE 1=1";
            return ApplyCommonFilters(sql, filter, "createDate", "status",
                "(productionOrderCode LIKE @kw OR CAST(salesOrderID AS CHAR) LIKE @kw)");
        }

        private static DataTable SearchWarehouses(SearchFilterCriteria filter)
        {
            string sql = @"SELECT warehouseID AS 'Warehouse ID', warehouseName AS 'Warehouse Name',
                                  warehouseAddress AS 'Address' FROM Warehouse WHERE 1=1";
            var conditions = new List<string>();
            var parameters = new List<MySqlParameter>();
            SearchQueryHelper.AddLike(conditions, parameters, "warehouseName", filter.Keyword ?? filter.Name, "@name");
            SearchQueryHelper.AddLike(conditions, parameters, "warehouseAddress", filter.Name, "@addr");
            if (conditions.Count > 0) sql += " AND " + string.Join(" AND ", conditions);
            sql += " ORDER BY warehouseName";
            return DatabaseConnect.ExecuteQuery(sql, parameters.ToArray());
        }

        private static DataTable SearchGrn(SearchFilterCriteria filter)
        {
            string sql = @"SELECT grn.goodsReceivedNoteID AS 'GRN ID', grn.goodsReceivedNoteCode AS 'GRN Code',
                                  s.supplierName AS 'Supplier', grn.PurchaseOrderID AS 'Purchase Order ID',
                                  grn.createDate AS 'Create Date', grn.status AS 'Status'
                           FROM GoodsReceivedNote grn LEFT JOIN Supplier s ON grn.supplierID = s.supplierID WHERE 1=1";
            return ApplyCommonFilters(sql, filter, "grn.createDate", "grn.status",
                "(grn.goodsReceivedNoteCode LIKE @kw OR s.supplierName LIKE @kw)");
        }

        private static DataTable SearchSuppliers(SearchFilterCriteria filter)
        {
            string sql = @"SELECT supplierID AS 'Supplier ID', supplierName AS 'Supplier Name',
                                  contactPerson AS 'Contact Person', phone AS 'Phone', email AS 'Email',
                                  billingAddress AS 'Billing Address', status AS 'Status'
                           FROM Supplier WHERE 1=1";
            var conditions = new List<string>();
            var parameters = new List<MySqlParameter>();
            SearchQueryHelper.AddLike(conditions, parameters, "supplierName", filter.Keyword ?? filter.Name, "@name");
            SearchQueryHelper.AddLike(conditions, parameters, "contactPerson", filter.Name, "@contact");
            SearchQueryHelper.AddLike(conditions, parameters, "phone", filter.Phone, "@phone");
            SearchQueryHelper.AddLike(conditions, parameters, "email", filter.Email, "@email");
            SearchQueryHelper.AddStatus(conditions, parameters, "status", filter.Status);
            if (conditions.Count > 0) sql += " AND " + string.Join(" AND ", conditions);
            sql += " ORDER BY supplierName";
            return DatabaseConnect.ExecuteQuery(sql, parameters.ToArray());
        }

        private static DataTable SearchRefunds(SearchFilterCriteria filter)
        {
            string sql = @"SELECT refundRequestCode AS 'Request Code', ReceiptVoucherID AS 'Receipt Voucher ID',
                                  InvoiceID AS 'Invoice ID', createDate AS 'Request Date',
                                  refundAmount AS 'Amount', refundMethod AS 'Refund Method',
                                  refundReason AS 'Reason', status AS 'Status'
                           FROM RefundRequest WHERE 1=1";
            return ApplyCommonFilters(sql, filter, "createDate", "status",
                "(refundRequestCode LIKE @kw OR refundReason LIKE @kw OR CAST(ReceiptVoucherID AS CHAR) LIKE @kw)");
        }

        private static DataTable SearchStaff(SearchFilterCriteria filter)
        {
            string sql = @"SELECT staffID AS 'Staff ID', username AS 'Username',
                                  CONCAT(firstName, ' ', lastName) AS 'Name', title AS 'Title',
                                  department AS 'Department', email AS 'Email', phone AS 'Phone',
                                  employDate AS 'Employ Date', status AS 'Status'
                           FROM Staff WHERE 1=1";
            var conditions = new List<string>();
            var parameters = new List<MySqlParameter>();
            SearchQueryHelper.AddLike(conditions, parameters, "CONCAT(firstName, ' ', lastName)", filter.Name ?? filter.Keyword, "@name");
            SearchQueryHelper.AddLike(conditions, parameters, "email", filter.Email, "@email");
            SearchQueryHelper.AddLike(conditions, parameters, "phone", filter.Phone, "@phone");
            SearchQueryHelper.AddLike(conditions, parameters, "username", filter.Keyword, "@user");
            SearchQueryHelper.AddDateFrom(conditions, parameters, "employDate", filter.FromDate);
            SearchQueryHelper.AddDateTo(conditions, parameters, "employDate", filter.ToDate);
            if (conditions.Count > 0) sql += " AND " + string.Join(" AND ", conditions);
            sql += " ORDER BY employDate DESC";
            return DatabaseConnect.ExecuteQuery(sql, parameters.ToArray());
        }

        private static DataTable SearchRawMaterials(SearchFilterCriteria filter)
        {
            string sql = @"SELECT rawMaterialID AS 'Raw Material ID', rawMaterialCode AS 'Raw Material Code',
                                  category AS 'Category', size AS 'Size', color AS 'Color',
                                  minimumStockLevel AS 'Min Stock', status AS 'Status'
                           FROM RawMaterial WHERE 1=1";
            var conditions = new List<string>();
            var parameters = new List<MySqlParameter>();
            SearchQueryHelper.AddLike(conditions, parameters, "rawMaterialCode", filter.Keyword, "@code");
            SearchQueryHelper.AddLike(conditions, parameters, "category", filter.Category, "@cat");
            SearchQueryHelper.AddLike(conditions, parameters, "color", filter.Color, "@color");
            SearchQueryHelper.AddLike(conditions, parameters, "size", filter.Size, "@size");
            if (conditions.Count > 0) sql += " AND " + string.Join(" AND ", conditions);
            sql += " ORDER BY rawMaterialCode";
            return DatabaseConnect.ExecuteQuery(sql, parameters.ToArray());
        }

        private static DataTable ApplyCommonFilters(string sql, SearchFilterCriteria filter,
            string dateColumn, string statusColumn, string keywordExpression)
        {
            var conditions = new List<string>();
            var parameters = new List<MySqlParameter>();
            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                conditions.Add(keywordExpression);
                parameters.Add(new MySqlParameter("@kw", "%" + filter.Keyword.Trim() + "%"));
            }
            SearchQueryHelper.AddDateFrom(conditions, parameters, dateColumn, filter.FromDate);
            SearchQueryHelper.AddDateTo(conditions, parameters, dateColumn, filter.ToDate);
            if (!string.IsNullOrEmpty(statusColumn))
                SearchQueryHelper.AddStatus(conditions, parameters, statusColumn, filter.Status);
            if (conditions.Count > 0) sql += " AND " + string.Join(" AND ", conditions);
            sql += " ORDER BY " + dateColumn + " DESC";
            return DatabaseConnect.ExecuteQuery(sql, parameters.ToArray());
        }
    }
}
