namespace Sales_user
{
    partial class ViewInvoiceDetailForm
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
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.dateTimePicker9 = new System.Windows.Forms.DateTimePicker();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(407, 30);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(211, 20);
            this.textBox10.TabIndex = 71;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(332, 33);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(43, 13);
            this.label21.TabIndex = 70;
            this.label21.Text = "Staff ID";
            // 
            // dateTimePicker9
            // 
            this.dateTimePicker9.Location = new System.Drawing.Point(407, 75);
            this.dateTimePicker9.Name = "dateTimePicker9";
            this.dateTimePicker9.Size = new System.Drawing.Size(211, 20);
            this.dateTimePicker9.TabIndex = 69;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(85, 75);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(211, 20);
            this.textBox5.TabIndex = 68;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(85, 27);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(211, 20);
            this.textBox6.TabIndex = 67;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(41, 78);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(35, 13);
            this.label16.TabIndex = 66;
            this.label16.Text = "status";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(10, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 13);
            this.label17.TabIndex = 65;
            this.label17.Text = "Invoice Type";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(316, 81);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(70, 13);
            this.label19.TabIndex = 64;
            this.label19.Text = "Created Date";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(655, 73);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 72;
            this.button1.Text = "Edit Record";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(13, 140);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(717, 297);
            this.dataGridView1.TabIndex = 73;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(640, 471);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(90, 23);
            this.button2.TabIndex = 74;
            this.button2.Text = "Save Record";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(559, 471);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 75;
            this.button3.Text = "Cancel Edit";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // ViewInvoiceDetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(774, 526);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox10);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.dateTimePicker9);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label19);
            this.Name = "ViewInvoiceDetailForm";
            this.Text = "ViewInvoiceDetailForm";
            this.Load += new System.EventHandler(this.ViewInvoiceDetailForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.DateTimePicker dateTimePicker9;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}