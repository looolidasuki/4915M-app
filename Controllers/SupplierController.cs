using MySql.Data.MySqlClient;
using Sales_user.Models;
using System.Data;

namespace Sales_user.Controllers
{
    public class SupplierController
    {
        public DataTable GetAllSuppliers()
        {
            string sql = @"SELECT supplierID AS 'Supplier ID',
                                  supplierName AS 'Supplier Name',
                                  contactPerson AS 'Contact Person',
                                  phone AS 'Phone',
                                  email AS 'Email',
                                  billingAddress AS 'Billing Address',
                                  status AS 'Status'
                           FROM Supplier
                           ORDER BY supplierName";
            return DatabaseConnect.ExecuteQuery(sql);
        }

        public long Insert(Supplier supplier)
        {
            string sql = @"INSERT INTO Supplier
                (supplierName, billingAddress, contactPerson, phone, email, paymentTerm, bankAccount, status)
                VALUES (@name, @address, @contact, @phone, @email, @term, @bank, @status)";
            return DatabaseConnect.ExecuteInsertReturnId(sql, new[] {
                new MySqlParameter("@name", supplier.SupplierName),
                new MySqlParameter("@address", supplier.BillingAddress ?? (object)System.DBNull.Value),
                new MySqlParameter("@contact", supplier.ContactPerson ?? (object)System.DBNull.Value),
                new MySqlParameter("@phone", supplier.Phone ?? (object)System.DBNull.Value),
                new MySqlParameter("@email", supplier.Email ?? (object)System.DBNull.Value),
                new MySqlParameter("@term", supplier.PaymentTerm ?? (object)System.DBNull.Value),
                new MySqlParameter("@bank", supplier.BankAccount ?? (object)System.DBNull.Value),
                new MySqlParameter("@status", supplier.Status)
            });
        }

        public DataTable GetRawMaterialQuotesBySupplier(long supplierId)
        {
            string sql = @"SELECT rm.rawMaterialCode AS 'Raw Material', rms.basePrice AS 'Price',
                                  rms.unit AS 'Unit', rms.status AS 'Status'
                           FROM RawMaterialSupplier rms
                           INNER JOIN RawMaterial rm ON rms.rawMaterialID = rm.rawMaterialID
                           WHERE rms.supplierID = @id";
            return DatabaseConnect.ExecuteQuery(sql, new[] { new MySqlParameter("@id", supplierId) });
        }
    }
}
