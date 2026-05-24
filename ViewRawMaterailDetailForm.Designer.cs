namespace Sales_user
{
    partial class ViewRawMaterailDetailForm
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
            this.cbCategory = new System.Windows.Forms.ComboBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.cbStatus = new System.Windows.Forms.ComboBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.txtSize = new System.Windows.Forms.TextBox();
            this.txtRawMaterialName = new System.Windows.Forms.TextBox();
            this.txtRawMaterialCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRawMaterialName = new System.Windows.Forms.Label();
            this.lblColor = new System.Windows.Forms.Label();
            this.lblSize = new System.Windows.Forms.Label();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblRawMaterialCode = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // cbCategory
            // 
            this.cbCategory.FormattingEnabled = true;
            this.cbCategory.Items.AddRange(new object[] {
            "Coatings & Glues",
            "Connection Hardware",
            "Cushioning & Foam",
            "Engineered Wood",
            "Fabrics & Leathers",
            "Finishing Layers",
            "Functional Accessories",
            "Metals",
            "Plastics & Composites",
            "Solid Wood",
            "Springs & Webbing",
            "Veneer"});
            this.cbCategory.Location = new System.Drawing.Point(16, 110);
            this.cbCategory.Name = "cbCategory";
            this.cbCategory.Size = new System.Drawing.Size(160, 21);
            this.cbCategory.TabIndex = 65;
            // 
            // lblCategory
            // 
            this.lblCategory.AutoSize = true;
            this.lblCategory.Location = new System.Drawing.Point(14, 92);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(49, 13);
            this.lblCategory.TabIndex = 64;
            this.lblCategory.Text = "Category";
            // 
            // cbStatus
            // 
            this.cbStatus.FormattingEnabled = true;
            this.cbStatus.Items.AddRange(new object[] {
            "Active",
            "",
            "Inactive",
            "",
            "Obsolete"});
            this.cbStatus.Location = new System.Drawing.Point(631, 39);
            this.cbStatus.Name = "cbStatus";
            this.cbStatus.Size = new System.Drawing.Size(103, 21);
            this.cbStatus.TabIndex = 63;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(16, 172);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(553, 107);
            this.txtDescription.TabIndex = 62;
            // 
            // txtColor
            // 
            this.txtColor.Location = new System.Drawing.Point(461, 110);
            this.txtColor.Name = "txtColor";
            this.txtColor.Size = new System.Drawing.Size(164, 20);
            this.txtColor.TabIndex = 61;
            // 
            // txtSize
            // 
            this.txtSize.Location = new System.Drawing.Point(239, 110);
            this.txtSize.Name = "txtSize";
            this.txtSize.Size = new System.Drawing.Size(160, 20);
            this.txtSize.TabIndex = 60;
            // 
            // txtRawMaterialName
            // 
            this.txtRawMaterialName.Location = new System.Drawing.Point(238, 40);
            this.txtRawMaterialName.Multiline = true;
            this.txtRawMaterialName.Name = "txtRawMaterialName";
            this.txtRawMaterialName.Size = new System.Drawing.Size(330, 24);
            this.txtRawMaterialName.TabIndex = 59;
            // 
            // txtRawMaterialCode
            // 
            this.txtRawMaterialCode.Location = new System.Drawing.Point(16, 40);
            this.txtRawMaterialCode.Name = "txtRawMaterialCode";
            this.txtRawMaterialCode.Size = new System.Drawing.Size(160, 20);
            this.txtRawMaterialCode.TabIndex = 58;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(629, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 57;
            this.label4.Text = "Status";
            // 
            // lblRawMaterialName
            // 
            this.lblRawMaterialName.AutoSize = true;
            this.lblRawMaterialName.Location = new System.Drawing.Point(236, 24);
            this.lblRawMaterialName.Name = "lblRawMaterialName";
            this.lblRawMaterialName.Size = new System.Drawing.Size(100, 13);
            this.lblRawMaterialName.TabIndex = 56;
            this.lblRawMaterialName.Text = "Raw Material Name";
            // 
            // lblColor
            // 
            this.lblColor.AutoSize = true;
            this.lblColor.Location = new System.Drawing.Point(459, 94);
            this.lblColor.Name = "lblColor";
            this.lblColor.Size = new System.Drawing.Size(31, 13);
            this.lblColor.TabIndex = 55;
            this.lblColor.Text = "Color";
            // 
            // lblSize
            // 
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new System.Drawing.Point(237, 94);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new System.Drawing.Size(27, 13);
            this.lblSize.TabIndex = 54;
            this.lblSize.Text = "Size";
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(14, 156);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(60, 13);
            this.lblDescription.TabIndex = 53;
            this.lblDescription.Text = "Description";
            // 
            // lblRawMaterialCode
            // 
            this.lblRawMaterialCode.AutoSize = true;
            this.lblRawMaterialCode.Location = new System.Drawing.Point(14, 24);
            this.lblRawMaterialCode.Name = "lblRawMaterialCode";
            this.lblRawMaterialCode.Size = new System.Drawing.Size(97, 13);
            this.lblRawMaterialCode.TabIndex = 52;
            this.lblRawMaterialCode.Text = "Raw Material Code";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(563, 649);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 79;
            this.button3.Text = "Cancel Edit";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(644, 649);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 78;
            this.button2.Text = "Save Record";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(17, 318);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(717, 297);
            this.dataGridView1.TabIndex = 77;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(659, 256);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 76;
            this.button1.Text = "Edit Record";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ViewRawMaterailDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(757, 687);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.cbCategory);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.cbStatus);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtColor);
            this.Controls.Add(this.txtSize);
            this.Controls.Add(this.txtRawMaterialName);
            this.Controls.Add(this.txtRawMaterialCode);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblRawMaterialName);
            this.Controls.Add(this.lblColor);
            this.Controls.Add(this.lblSize);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.lblRawMaterialCode);
            this.Name = "ViewRawMaterailDetailForm";
            this.Text = "ViewRawMaterailDetailForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbCategory;
        private System.Windows.Forms.Label lblCategory;
        private System.Windows.Forms.ComboBox cbStatus;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtColor;
        private System.Windows.Forms.TextBox txtSize;
        private System.Windows.Forms.TextBox txtRawMaterialName;
        private System.Windows.Forms.TextBox txtRawMaterialCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRawMaterialName;
        private System.Windows.Forms.Label lblColor;
        private System.Windows.Forms.Label lblSize;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblRawMaterialCode;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
    }
}