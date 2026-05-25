using MySql.Data.MySqlClient;
using Sales_user.Models;
using System.Data;

namespace Sales_user.Controllers
{
    public class SalesOrderController
    {
        public DataTable GetAllSalesOrders()
        {
            string sql = @"SELECT so.salesOrderID AS 'Order ID',
                                  so.salesOrderCode AS 'Order Code',
                                  c.customerName AS 'Customer',
                                  so.deliveryAddress AS 'Delivery Address',
                                  so.createDate AS 'Create Date',
                                  so.status AS 'Status'
                           FROM SalesOrder so
                           LEFT JOIN Customer c ON so.customerID = c.customerID
                           ORDER BY so.createDate DESC";
            return DatabaseConnect.ExecuteQuery(sql);
        }

        public long Insert(SalesOrder order)
        {
            string sql = @"INSERT INTO SalesOrder
                (salesOrderCode, customerID, staffID, currencyCurrencyID, deliveryAddress,
                 discountType, discount, status, remark)
                VALUES (@code, @customerID, @staffID, @currencyID, @address,
                        @discountType, @discount, @status, @remark)";
            return DatabaseConnect.ExecuteInsertReturnId(sql, new[] {
                new MySqlParameter("@code", order.SalesOrderCode),
                new MySqlParameter("@customerID", order.CustomerID),
                new MySqlParameter("@staffID", order.StaffID),
                new MySqlParameter("@currencyID", order.CurrencyCurrencyID),
                new MySqlParameter("@address", order.DeliveryAddress),
                new MySqlParameter("@discountType", order.DiscountType ?? (object)System.DBNull.Value),
                new MySqlParameter("@discount", order.Discount),
                new MySqlParameter("@status", order.Status),
                new MySqlParameter("@remark", order.Remark ?? (object)System.DBNull.Value)
            });
        }

        public void UpdateCodeAfterInsert(long salesOrderId)
        {
            DatabaseConnect.ExecuteNonQuery(
                "UPDATE SalesOrder SET salesOrderCode = @code WHERE salesOrderID = @id",
                new[] {
                    new MySqlParameter("@code", "SO-" + salesOrderId),
                    new MySqlParameter("@id", salesOrderId)
                });
        }

        public bool InsertProductLine(long salesOrderId, long productId, decimal price, decimal orderQty, decimal discount)
        {
            string sql = @"INSERT INTO SalesOrderProductLine
                (salesOrderID, productID, price, orderQuantity, discountAmount)
                VALUES (@soID, @productID, @price, @qty, @discount)";
            return DatabaseConnect.ExecuteNonQuery(sql, new[] {
                new MySqlParameter("@soID", salesOrderId),
                new MySqlParameter("@productID", productId),
                new MySqlParameter("@price", price),
                new MySqlParameter("@qty", orderQty),
                new MySqlParameter("@discount", discount)
            }) > 0;
        }

        public DataTable GetProductLines(long salesOrderId)
        {
            string sql = @"SELECT p.productCode AS 'Product Code', spl.price AS 'Price',
                                  spl.orderQuantity AS 'Order Qty', spl.discountAmount AS 'Discount'
                           FROM SalesOrderProductLine spl
                           INNER JOIN Product p ON spl.productID = p.productID
                           WHERE spl.salesOrderID = @id";
            return DatabaseConnect.ExecuteQuery(sql, new[] { new MySqlParameter("@id", salesOrderId) });
        }

        public DataTable GetProductionOrdersBySalesOrder(long salesOrderId)
        {
            string sql = @"SELECT productionOrderCode AS 'Production Code',
                                  estFinishDate AS 'Est. Finish', status AS 'Status'
                           FROM ProductionOrder WHERE salesOrderID = @id";
            return DatabaseConnect.ExecuteQuery(sql, new[] { new MySqlParameter("@id", salesOrderId) });
        }
    }
}
