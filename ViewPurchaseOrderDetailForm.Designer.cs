namespace Sales_user
{
    partial class ViewPurchaseOrderDetailForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbDiscount = new System.Windows.Forms.CheckBox();
            this.rbtnDiscountPrecent = new System.Windows.Forms.RadioButton();
            this.rbtnDiscountAmount = new System.Windows.Forms.RadioButton();
            this.txtPurchaseOrderDiscount = new System.Windows.Forms.TextBox();
            this.txtTotalAmount = new System.Windows.Forms.TextBox();
            this.lblTotalAmount = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.cbSelectSupplier = new System.Windows.Forms.ComboBox();
            this.txtReference = new System.Windows.Forms.TextBox();
            this.lblReference = new System.Windows.Forms.Label();
            this.lblShipToAddress = new System.Windows.Forms.Label();
            this.dtpRequestDeliveryDate = new System.Windows.Forms.DateTimePicker();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.txtPurchasesOrder = new System.Windows.Forms.TextBox();
            this.lblRequestDeliveryDate = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.lblPurchaseOrder = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDiscount
            // 
            this.cbDiscount.AutoSize = true;
            this.cbDiscount.Location = new System.Drawing.Point(20, 137);
            this.cbDiscount.Name = "cbDiscount";
            this.cbDiscount.Size = new System.Drawing.Size(68, 17);
            this.cbDiscount.TabIndex = 66;
            this.cbDiscount.Text = "Discount";
            this.cbDiscount.UseVisualStyleBackColor = true;
            // 
            // rbtnDiscountPrecent
            // 
            this.rbtnDiscountPrecent.Enabled = false;
            this.rbtnDiscountPrecent.Location = new System.Drawing.Point(91, 133);
            this.rbtnDiscountPrecent.Name = "rbtnDiscountPrecent";
            this.rbtnDiscountPrecent.Size = new System.Drawing.Size(63, 26);
            this.rbtnDiscountPrecent.TabIndex = 67;
            this.rbtnDiscountPrecent.Text = "Amount";
            // 
            // rbtnDiscountAmount
            // 
            this.rbtnDiscountAmount.Enabled = false;
            this.rbtnDiscountAmount.Location = new System.Drawing.Point(160, 133);
            this.rbtnDiscountAmount.Name = "rbtnDiscountAmount";
            this.rbtnDiscountAmount.Size = new System.Drawing.Size(68, 26);
            this.rbtnDiscountAmount.TabIndex = 68;
            this.rbtnDiscountAmount.Text = "Precent";
            // 
            // txtPurchaseOrderDiscount
            // 
            this.txtPurchaseOrderDiscount.Location = new System.Drawing.Point(237, 137);
            this.txtPurchaseOrderDiscount.Name = "txtPurchaseOrderDiscount";
            this.txtPurchaseOrderDiscount.Size = new System.Drawing.Size(100, 20);
            this.txtPurchaseOrderDiscount.TabIndex = 69;
            // 
            // txtTotalAmount
            // 
            this.txtTotalAmount.Location = new System.Drawing.Point(512, 133);
            this.txtTotalAmount.Name = "txtTotalAmount";
            this.txtTotalAmount.Size = new System.Drawing.Size(100, 20);
            this.txtTotalAmount.TabIndex = 70;
            // 
            // lblTotalAmount
            // 
            this.lblTotalAmount.AutoSize = true;
            this.lblTotalAmount.Location = new System.Drawing.Point(433, 137);
            this.lblTotalAmount.Name = "lblTotalAmount";
            this.lblTotalAmount.Size = new System.Drawing.Size(73, 13);
            this.lblTotalAmount.TabIndex = 65;
            this.lblTotalAmount.Text = "Total Amount:";
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "China Warehouse",
            "Philippines Warehouse",
            "Thailand Warehouse",
            "Vietnam Warehouse"});
            this.comboBox2.Location = new System.Drawing.Point(69, 74);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(171, 21);
            this.comboBox2.TabIndex = 64;
            // 
            // cbSelectSupplier
            // 
            this.cbSelectSupplier.FormattingEnabled = true;
            this.cbSelectSupplier.Location = new System.Drawing.Point(346, 24);
            this.cbSelectSupplier.Name = "cbSelectSupplier";
            this.cbSelectSupplier.Size = new System.Drawing.Size(121, 21);
            this.cbSelectSupplier.TabIndex = 63;
            // 
            // txtReference
            // 
            this.txtReference.Location = new System.Drawing.Point(355, 74);
            this.txtReference.Name = "txtReference";
            this.txtReference.Size = new System.Drawing.Size(100, 20);
            this.txtReference.TabIndex = 62;
            // 
            // lblReference
            // 
            this.lblReference.AutoSize = true;
            this.lblReference.Location = new System.Drawing.Point(289, 77);
            this.lblReference.Name = "lblReference";
            this.lblReference.Size = new System.Drawing.Size(60, 13);
            this.lblReference.TabIndex = 61;
            this.lblReference.Text = "Reference:";
            // 
            // lblShipToAddress
            // 
            this.lblShipToAddress.AutoSize = true;
            this.lblShipToAddress.Location = new System.Drawing.Point(16, 82);
            this.lblShipToAddress.Name = "lblShipToAddress";
            this.lblShipToAddress.Size = new System.Drawing.Size(47, 13);
            this.lblShipToAddress.TabIndex = 60;
            this.lblShipToAddress.Text = "Ship To:";
            // 
            // dtpRequestDeliveryDate
            // 
            this.dtpRequestDeliveryDate.Location = new System.Drawing.Point(632, 71);
            this.dtpRequestDeliveryDate.Name = "dtpRequestDeliveryDate";
            this.dtpRequestDeliveryDate.Size = new System.Drawing.Size(187, 20);
            this.dtpRequestDeliveryDate.TabIndex = 59;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(632, 24);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(187, 20);
            this.dateTimePicker2.TabIndex = 58;
            // 
            // txtPurchasesOrder
            // 
            this.txtPurchasesOrder.Enabled = false;
            this.txtPurchasesOrder.Location = new System.Drawing.Point(106, 23);
            this.txtPurchasesOrder.Name = "txtPurchasesOrder";
            this.txtPurchasesOrder.Size = new System.Drawing.Size(100, 20);
            this.txtPurchasesOrder.TabIndex = 57;
            // 
            // lblRequestDeliveryDate
            // 
            this.lblRequestDeliveryDate.AutoSize = true;
            this.lblRequestDeliveryDate.Location = new System.Drawing.Point(509, 77);
            this.lblRequestDeliveryDate.Name = "lblRequestDeliveryDate";
            this.lblRequestDeliveryDate.Size = new System.Drawing.Size(117, 13);
            this.lblRequestDeliveryDate.TabIndex = 56;
            this.lblRequestDeliveryDate.Text = "Request Delivery Date:";
            // 
            // lblSupplier
            // 
            this.lblSupplier.AutoSize = true;
            this.lblSupplier.Location = new System.Drawing.Point(289, 26);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(48, 13);
            this.lblSupplier.TabIndex = 55;
            this.lblSupplier.Text = "Supplier:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(593, 27);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(33, 13);
            this.lblDate.TabIndex = 54;
            this.lblDate.Text = "Date:";
            // 
            // lblPurchaseOrder
            // 
            this.lblPurchaseOrder.AutoSize = true;
            this.lblPurchaseOrder.Location = new System.Drawing.Point(16, 26);
            this.lblPurchaseOrder.Name = "lblPurchaseOrder";
            this.lblPurchaseOrder.Size = new System.Drawing.Size(84, 13);
            this.lblPurchaseOrder.TabIndex = 53;
            this.lblPurchaseOrder.Text = "Purchase Order:";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(744, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 73;
            this.button1.Text = "Edit Record";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(632, 519);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 78;
            this.button3.Text = "Cancel Edit";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(729, 519);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 77;
            this.button2.Text = "Save Record";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(20, 188);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(799, 297);
            this.dataGridView1.TabIndex = 76;
            // 
            // ViewPurchaseOrderDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 576);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbDiscount);
            this.Controls.Add(this.rbtnDiscountPrecent);
            this.Controls.Add(this.rbtnDiscountAmount);
            this.Controls.Add(this.txtPurchaseOrderDiscount);
            this.Controls.Add(this.txtTotalAmount);
            this.Controls.Add(this.lblTotalAmount);
            this.Controls.Add(this.comboBox2);
            this.Controls.Add(this.cbSelectSupplier);
            this.Controls.Add(this.txtReference);
            this.Controls.Add(this.lblReference);
            this.Controls.Add(this.lblShipToAddress);
            this.Controls.Add(this.dtpRequestDeliveryDate);
            this.Controls.Add(this.dateTimePicker2);
            this.Controls.Add(this.txtPurchasesOrder);
            this.Controls.Add(this.lblRequestDeliveryDate);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.lblPurchaseOrder);
            this.Name = "ViewPurchaseOrderDetailForm";
            this.Text = "ViewPurchaseOrderDetailForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox cbDiscount;
        private System.Windows.Forms.RadioButton rbtnDiscountPrecent;
        private System.Windows.Forms.RadioButton rbtnDiscountAmount;
        private System.Windows.Forms.TextBox txtPurchaseOrderDiscount;
        private System.Windows.Forms.TextBox txtTotalAmount;
        private System.Windows.Forms.Label lblTotalAmount;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox cbSelectSupplier;
        private System.Windows.Forms.TextBox txtReference;
        private System.Windows.Forms.Label lblReference;
        private System.Windows.Forms.Label lblShipToAddress;
        private System.Windows.Forms.DateTimePicker dtpRequestDeliveryDate;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.TextBox txtPurchasesOrder;
        private System.Windows.Forms.Label lblRequestDeliveryDate;
        private System.Windows.Forms.Label lblSupplier;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Label lblPurchaseOrder;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
    }
}