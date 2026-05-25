using MySql.Data.MySqlClient;
using Sales_user.Models;

namespace Sales_user
{
    public static class EntityUpdateController
    {
        public static bool UpdateCustomer(Customer c) =>
            new Controllers.CustomerController().Update(c);

        public static bool UpdateSalesOrder(SalesOrder o) =>
            new Controllers.SalesOrderController().Update(o);

        public static bool UpdateProduct(Product p)
        {
            string sql = @"UPDATE Product SET category = @cat, styleNumber = @style, size = @size, color = @color,
                           unit = @unit, basePriceByCurrency = @price, status = @status, lastModifyDate = NOW()
                           WHERE productID = @id";
            return DatabaseConnect.ExecuteNonQuery(sql, new[] {
                new MySqlParameter("@cat", p.Category),
                new MySqlParameter("@style", p.StyleNumber),
                new MySqlParameter("@size", p.Size),
                new MySqlParameter("@color", p.Color),
                new MySqlParameter("@unit", p.Unit),
                new MySqlParameter("@price", p.BasePriceByCurrency),
                new MySqlParameter("@status", p.Status),
                new MySqlParameter("@id", p.ProductID)
            }) > 0;
        }

        public static bool UpdateSupplier(Supplier s)
        {
            string sql = @"UPDATE Supplier SET supplierName = @name, billingAddress = @addr, contactPerson = @contact,
                           phone = @phone, email = @email, paymentTerm = @term WHERE supplierID = @id";
            return DatabaseConnect.ExecuteNonQuery(sql, new[] {
                new MySqlParameter("@name", s.SupplierName),
                new MySqlParameter("@addr", s.BillingAddress ?? (object)System.DBNull.Value),
                new MySqlParameter("@contact", s.ContactPerson ?? (object)System.DBNull.Value),
                new MySqlParameter("@phone", s.Phone ?? (object)System.DBNull.Value),
                new MySqlParameter("@email", s.Email ?? (object)System.DBNull.Value),
                new MySqlParameter("@term", s.PaymentTerm ?? (object)System.DBNull.Value),
                new MySqlParameter("@id", s.SupplierID)
            }) > 0;
        }

        public static bool UpdateWarehouse(Warehouse w)
        {
            string sql = @"UPDATE Warehouse SET warehouseName = @name, warehouseAddress = @addr WHERE warehouseID = @id";
            return DatabaseConnect.ExecuteNonQuery(sql, new[] {
                new MySqlParameter("@name", w.WarehouseName),
                new MySqlParameter("@addr", w.WarehouseAddress),
                new MySqlParameter("@id", w.WarehouseID)
            }) > 0;
        }

        public static bool UpdateStaff(Staff s)
        {
            string sql = @"UPDATE Staff SET firstName = @first, lastName = @last, title = @title, department = @dept,
                           phone = @phone, email = @email WHERE staffID = @id";
            return DatabaseConnect.ExecuteNonQuery(sql, new[] {
                new MySqlParameter("@first", s.FirstName),
                new MySqlParameter("@last", s.LastName),
                new MySqlParameter("@title", s.Title),
                new MySqlParameter("@dept", s.Department),
                new MySqlParameter("@phone", s.Phone),
                new MySqlParameter("@email", s.Email),
                new MySqlParameter("@id", s.StaffID)
            }) > 0;
        }

        public static bool UpdateDeliveryNote(long id, string code, int status, string tracking)
        {
            string sql = @"UPDATE DeliveryNote SET deliveryNoteCode = @code, status = @status,
                           trackingNumber = @track, lastModifyDate = NOW() WHERE deliveryNoteID = @id";
            return DatabaseConnect.ExecuteNonQuery(sql, new[] {
                new MySqlParameter("@code", code),
                new MySqlParameter("@status", status),
                new MySqlParameter("@track", tracking),
                new MySqlParameter("@id", id)
            }) > 0;
        }

        public static bool UpdateInvoice(long id, int status, int invoiceType)
        {
            string sql = @"UPDATE Invoice SET status = @status, invoiceType = @type, lastModifyDate = NOW() WHERE invoiceID = @id";
            return DatabaseConnect.ExecuteNonQuery(sql, new[] {
                new MySqlParameter("@status", status),
                new MySqlParameter("@type", invoiceType),
                new MySqlParameter("@id", id)
            }) > 0;
        }

        public static bool UpdateProductionOrder(long id, int status, string remark)
        {
            string sql = @"UPDATE ProductionOrder SET status = @status, remark = @remark, lastModifyDate = NOW()
                           WHERE productionOrderID = @id";
            return DatabaseConnect.ExecuteNonQuery(sql, new[] {
                new MySqlParameter("@status", status),
                new MySqlParameter("@remark", remark ?? (object)System.DBNull.Value),
                new MySqlParameter("@id", id)
            }) > 0;
        }

        public static bool UpdateRawMaterial(RawMaterial m)
        {
            string sql = @"UPDATE RawMaterial SET category = @cat, size = @size, color = @color,
                           minimumStockLevel = @min, status = @status WHERE rawMaterialID = @id";
            return DatabaseConnect.ExecuteNonQuery(sql, new[] {
                new MySqlParameter("@cat", m.Category),
                new MySqlParameter("@size", m.Size),
                new MySqlParameter("@color", m.Color),
                new MySqlParameter("@min", m.MinimumStockLevel),
                new MySqlParameter("@status", m.Status),
                new MySqlParameter("@id", m.RawMaterialID)
            }) > 0;
        }

        public static bool UpdateRefund(long id, int status, string reason, decimal amount)
        {
            string sql = @"UPDATE RefundRequest SET status = @status, refundReason = @reason,
                           refundAmount = @amount, lastModifyDate = NOW() WHERE refundRequestID = @id";
            return DatabaseConnect.ExecuteNonQuery(sql, new[] {
                new MySqlParameter("@status", status),
                new MySqlParameter("@reason", reason),
                new MySqlParameter("@amount", amount),
                new MySqlParameter("@id", id)
            }) > 0;
        }
    }
}
