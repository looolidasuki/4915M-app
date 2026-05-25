using Sales_user.Controllers;
using System;
using System.Windows.Forms;

namespace Sales_user
{
    public partial class ViewRefundDetailForm : Form
    {
        private readonly RefundRequestController _controller = new RefundRequestController();

        public ViewRefundDetailForm()
        {
            InitializeComponent();
            Load += ViewRefundDetailForm_Load;
        }

        private void ViewRefundDetailForm_Load(object sender, EventArgs e)
        {
            FormGridHelper.BindReadOnly(dgvRefundRequests, _controller.GetAllRefundRequests());
            CreateFormHelper.WireCancel(button3, this);
        }
    }
}
