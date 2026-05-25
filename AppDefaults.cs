using System;
using System.Data;

namespace Sales_user
{
    public static class AppDefaults
    {
        public static long StaffId { get; private set; } = 1;
        public static long CurrencyId { get; private set; } = 1;
        public static long WarehouseId { get; private set; } = 1;
        public static long CustomerId { get; private set; } = 1;
        public static long SupplierId { get; private set; } = 1;

        public static void LoadFromDatabase()
        {
            StaffId = GetFirstId("SELECT staffID FROM Staff ORDER BY staffID LIMIT 1");
            CurrencyId = GetFirstId("SELECT currencyID FROM Currency ORDER BY currencyID LIMIT 1");
            WarehouseId = GetFirstId("SELECT warehouseID FROM Warehouse ORDER BY warehouseID LIMIT 1");
            CustomerId = GetFirstId("SELECT customerID FROM Customer ORDER BY customerID LIMIT 1");
            SupplierId = GetFirstId("SELECT supplierID FROM Supplier ORDER BY supplierID LIMIT 1");
        }

        private static long GetFirstId(string sql)
        {
            DataTable dt = DatabaseConnect.ExecuteQuery(sql);
            if (dt != null && dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
            {
                return Convert.ToInt64(dt.Rows[0][0]);
            }
            return 1;
        }
    }
}
