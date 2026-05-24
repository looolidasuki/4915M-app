namespace Sales_user
{
    partial class ViewRefundDetailForm
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
            this.label76 = new System.Windows.Forms.Label();
            this.dateTimePicker16 = new System.Windows.Forms.DateTimePicker();
            this.label73 = new System.Windows.Forms.Label();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.label74 = new System.Windows.Forms.Label();
            this.dateTimePicker17 = new System.Windows.Forms.DateTimePicker();
            this.label75 = new System.Windows.Forms.Label();
            this.textBox43 = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.Location = new System.Drawing.Point(23, 29);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(79, 13);
            this.label76.TabIndex = 65;
            this.label76.Text = "Customer Code";
            // 
            // dateTimePicker16
            // 
            this.dateTimePicker16.Location = new System.Drawing.Point(98, 121);
            this.dateTimePicker16.Name = "dateTimePicker16";
            this.dateTimePicker16.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker16.TabIndex = 64;
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Location = new System.Drawing.Point(23, 127);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(46, 13);
            this.label73.TabIndex = 63;
            this.label73.Text = "To Date";
            // 
            // comboBox9
            // 
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Items.AddRange(new object[] {
            "Processing",
            "",
            "Completed"});
            this.comboBox9.Location = new System.Drawing.Point(580, 29);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(121, 21);
            this.comboBox9.TabIndex = 62;
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Location = new System.Drawing.Point(537, 35);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(37, 13);
            this.label74.TabIndex = 61;
            this.label74.Text = "Status";
            // 
            // dateTimePicker17
            // 
            this.dateTimePicker17.Location = new System.Drawing.Point(98, 72);
            this.dateTimePicker17.Name = "dateTimePicker17";
            this.dateTimePicker17.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker17.TabIndex = 60;
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(23, 75);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(56, 13);
            this.label75.TabIndex = 59;
            this.label75.Text = "From Date";
            // 
            // textBox43
            // 
            this.textBox43.Location = new System.Drawing.Point(111, 26);
            this.textBox43.Name = "textBox43";
            this.textBox43.Size = new System.Drawing.Size(361, 20);
            this.textBox43.TabIndex = 58;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(638, 507);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 98;
            this.button3.Text = "Cancel Edit";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(735, 507);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 97;
            this.button2.Text = "Save Record";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(26, 176);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(799, 297);
            this.dataGridView1.TabIndex = 96;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(750, 117);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 95;
            this.button1.Text = "Edit Record";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // ViewRefundDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 557);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label76);
            this.Controls.Add(this.dateTimePicker16);
            this.Controls.Add(this.label73);
            this.Controls.Add(this.comboBox9);
            this.Controls.Add(this.label74);
            this.Controls.Add(this.dateTimePicker17);
            this.Controls.Add(this.label75);
            this.Controls.Add(this.textBox43);
            this.Name = "ViewRefundDetailForm";
            this.Text = "ViewRefundDetailForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.DateTimePicker dateTimePicker16;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.ComboBox comboBox9;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.DateTimePicker dateTimePicker17;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.TextBox textBox43;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
    }
}