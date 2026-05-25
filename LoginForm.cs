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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 2. Create an instance of your secondary form (e.g., Form2 or MainForm)
            MainMenuForm mainDashboard = new MainMenuForm();

            // 3. Show the main dashboard
            mainDashboard.Show();

            // 4. Hide the login form so it doesn't stay open in the background
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            // 寫一條最簡單的 SQL 語句，查詢剛才導入的 SystemDictionary 表
            string testSql = "SELECT COUNT(*) FROM SystemDictionary";

            // 透過我們寫好的 DatabaseConnect 執行查詢
            System.Data.DataTable dt = DatabaseConnect.ExecuteQuery(testSql);

            // 如果能成功獲取 DataTable 且沒有觸發 DatabaseConnect 內部的 Exception 彈窗
            if (dt != null && dt.Rows.Count > 0)
            {
                // 證明連線與讀取完全正常（這行測試完可以刪除，免得每次啟動都彈窗）
                MessageBox.Show("Database connection test passed successfully!", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Connection test failed. Please check your XAMPP status.", "Connection Test", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
