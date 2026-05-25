using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration; // 記得要在專案的「參考 (References)」裡加入 System.Configuration
using System.Windows.Forms;

namespace Sales_user
{
    public static class DatabaseConnect
    {
        // 從 App.config 中讀取連線字串
        private static readonly string connString = ConfigurationManager.ConnectionStrings["MyERPConnectionString"].ConnectionString;

        // 1. 獲取連線物件
        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connString);
        }

        // 2. 執行 SELECT 查詢並回傳 DataTable（供 DataGridView 顯示）
        public static DataTable ExecuteQuery(string sql, MySqlParameter[] parameters = null)
        {
            DataTable dt = new DataTable();
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(cmd))
                        {
                            adapter.Fill(dt);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database Error: {ex.Message}", "SQL Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }
            }
            return dt;
        }

        // 3. 執行 INSERT, UPDATE, DELETE 操作
        public static int ExecuteNonQuery(string sql, MySqlParameter[] parameters = null)
        {
            int rowsAffected = 0;
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        rowsAffected = cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    // 💡 輸出語句已轉換為英文
                    MessageBox.Show($"Database execution failed: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return rowsAffected;
        }

        public static long ExecuteInsertReturnId(string sql, MySqlParameter[] parameters = null)
        {
            using (MySqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql + "; SELECT LAST_INSERT_ID();", conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        object result = cmd.ExecuteScalar();
                        return result != null ? Convert.ToInt64(result) : 0;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Database execution failed: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return 0;
                }
            }
        }
    }
}