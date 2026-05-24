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
    }
}
