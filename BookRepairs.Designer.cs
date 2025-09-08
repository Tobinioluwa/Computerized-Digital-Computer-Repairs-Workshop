namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    partial class BookRepairs
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label4 = new System.Windows.Forms.Label();
            this.cmbRepairCustomer = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnPayRepair = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtRepairAmount = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbRepairPayment = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIssue = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtDevice = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepairAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(30, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(135, 20);
            this.label4.TabIndex = 21;
            this.label4.Text = "Select Customer :";
            // 
            // cmbRepairCustomer
            // 
            this.cmbRepairCustomer.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.cmbRepairCustomer.FormattingEnabled = true;
            this.cmbRepairCustomer.Location = new System.Drawing.Point(172, 104);
            this.cmbRepairCustomer.Name = "cmbRepairCustomer";
            this.cmbRepairCustomer.Size = new System.Drawing.Size(523, 37);
            this.cmbRepairCustomer.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(292, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(189, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Book Repairs";
            // 
            // btnPayRepair
            // 
            this.btnPayRepair.BackColor = System.Drawing.Color.SteelBlue;
            this.btnPayRepair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayRepair.ForeColor = System.Drawing.Color.White;
            this.btnPayRepair.Location = new System.Drawing.Point(172, 397);
            this.btnPayRepair.Name = "btnPayRepair";
            this.btnPayRepair.Size = new System.Drawing.Size(523, 43);
            this.btnPayRepair.TabIndex = 37;
            this.btnPayRepair.Text = "Pay and Book Repair";
            this.btnPayRepair.UseVisualStyleBackColor = false;
            this.btnPayRepair.Click += new System.EventHandler(this.BtnPayRepair_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.txtRepairAmount);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.cmbRepairPayment);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.txtIssue);
            this.panel1.Controls.Add(this.btnPayRepair);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.txtDevice);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.cmbRepairCustomer);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(7, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(735, 489);
            this.panel1.TabIndex = 35;
            // 
            // txtRepairAmount
            // 
            this.txtRepairAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.txtRepairAmount.Location = new System.Drawing.Point(172, 270);
            this.txtRepairAmount.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.txtRepairAmount.Name = "txtRepairAmount";
            this.txtRepairAmount.Size = new System.Drawing.Size(523, 33);
            this.txtRepairAmount.TabIndex = 38;
            this.txtRepairAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtRepairAmount.ThousandsSeparator = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(30, 341);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(133, 20);
            this.label8.TabIndex = 29;
            this.label8.Text = "Payment Method:";
            // 
            // cmbRepairPayment
            // 
            this.cmbRepairPayment.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.cmbRepairPayment.FormattingEnabled = true;
            this.cmbRepairPayment.Location = new System.Drawing.Point(172, 335);
            this.cmbRepairPayment.Name = "cmbRepairPayment";
            this.cmbRepairPayment.Size = new System.Drawing.Size(523, 37);
            this.cmbRepairPayment.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(21, 271);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(146, 20);
            this.label7.TabIndex = 26;
            this.label7.Text = "Repair Amount (₦):";
            // 
            // txtIssue
            // 
            this.txtIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.txtIssue.Location = new System.Drawing.Point(172, 214);
            this.txtIssue.Name = "txtIssue";
            this.txtIssue.Size = new System.Drawing.Size(523, 33);
            this.txtIssue.TabIndex = 25;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(28, 220);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 20);
            this.label5.TabIndex = 24;
            this.label5.Text = "Issue Description:";
            // 
            // txtDevice
            // 
            this.txtDevice.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F);
            this.txtDevice.Location = new System.Drawing.Point(172, 156);
            this.txtDevice.Name = "txtDevice";
            this.txtDevice.Size = new System.Drawing.Size(523, 33);
            this.txtDevice.TabIndex = 23;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(43, 164);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(114, 20);
            this.label3.TabIndex = 22;
            this.label3.Text = "Device Details:";
            // 
            // BookRepairs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "BookRepairs";
            this.Size = new System.Drawing.Size(753, 505);
            this.Load += new System.EventHandler(this.BookRepairs_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRepairAmount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbRepairCustomer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnPayRepair;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbRepairPayment;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIssue;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtDevice;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown txtRepairAmount;
    }
}
