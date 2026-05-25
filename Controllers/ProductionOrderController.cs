using MySql.Data.MySqlClient;
using Sales_user.Models;
using System.Data;

namespace Sales_user.Controllers
{
    public class ProductionOrderController
    {
        public DataTable GetAllProductionOrders()
        {
            string sql = @"SELECT productionOrderID AS 'Production Order ID',
                                  productionOrderCode AS 'Production Order Code',
                                  salesOrderID AS 'Sales Order ID',
                                  createDate AS 'Create Date',
                                  estFinishDate AS 'Est. Finish Date',
                                  status AS 'Status'
                           FROM ProductionOrder
                           ORDER BY createDate DESC";
            return DatabaseConnect.ExecuteQuery(sql);
        }

        public long Insert(ProductionOrder order)
        {
            string sql = @"INSERT INTO ProductionOrder
                (productionOrderCode, salesOrderID, staffID, estFinishDate, status, remark)
                VALUES (@code, @soID, @staffID, @finish, @status, @remark)";
            return DatabaseConnect.ExecuteInsertReturnId(sql, new[] {
                new MySqlParameter("@code", order.ProductionOrderCode),
                new MySqlParameter("@soID", order.SalesOrderID),
                new MySqlParameter("@staffID", order.StaffID),
                new MySqlParameter("@finish", order.EstFinishDate),
                new MySqlParameter("@status", order.Status),
                new MySqlParameter("@remark", order.Remark ?? (object)System.DBNull.Value)
            });
        }

        public void UpdateCodeAfterInsert(long id)
        {
            DatabaseConnect.ExecuteNonQuery(
                "UPDATE ProductionOrder SET productionOrderCode = @code WHERE productionOrderID = @id",
                new[] {
                    new MySqlParameter("@code", "PO-" + id),
                    new MySqlParameter("@id", id)
                });
        }

        public DataTable GetProductLines(long productionOrderId)
        {
            string sql = @"SELECT p.productCode AS 'Product', popl.productionQty AS 'Production Qty'
                           FROM ProductionOrderProductLine popl
                           INNER JOIN Product p ON popl.productID = p.productID
                           WHERE popl.ProductionOrderID = @id";
            return DatabaseConnect.ExecuteQuery(sql, new[] { new MySqlParameter("@id", productionOrderId) });
        }
    }
}
