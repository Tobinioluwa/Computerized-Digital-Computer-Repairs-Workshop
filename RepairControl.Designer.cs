namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    partial class RepairControl
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.dgvRepairs = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btnUpdateRepair = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbStatus = new System.Windows.Forms.ComboBox();
            this.cmbTechnicians = new System.Windows.Forms.ComboBox();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepairs)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.dgvRepairs);
            this.panel2.Location = new System.Drawing.Point(18, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(777, 350);
            this.panel2.TabIndex = 17;
            // 
            // dgvRepairs
            // 
            this.dgvRepairs.AllowUserToAddRows = false;
            this.dgvRepairs.AllowUserToDeleteRows = false;
            this.dgvRepairs.AllowUserToResizeRows = false;
            this.dgvRepairs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRepairs.BackgroundColor = System.Drawing.Color.LightSkyBlue;
            this.dgvRepairs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRepairs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRepairs.GridColor = System.Drawing.Color.White;
            this.dgvRepairs.Location = new System.Drawing.Point(0, 0);
            this.dgvRepairs.MultiSelect = false;
            this.dgvRepairs.Name = "dgvRepairs";
            this.dgvRepairs.ReadOnly = true;
            this.dgvRepairs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRepairs.Size = new System.Drawing.Size(777, 350);
            this.dgvRepairs.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(371, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(112, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Change Status:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(20, 9);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(139, 16);
            this.label7.TabIndex = 10;
            this.label7.Text = "Assign Technician:";
            // 
            // btnUpdateRepair
            // 
            this.btnUpdateRepair.BackColor = System.Drawing.Color.SteelBlue;
            this.btnUpdateRepair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateRepair.ForeColor = System.Drawing.Color.White;
            this.btnUpdateRepair.Location = new System.Drawing.Point(18, 61);
            this.btnUpdateRepair.Name = "btnUpdateRepair";
            this.btnUpdateRepair.Size = new System.Drawing.Size(744, 40);
            this.btnUpdateRepair.TabIndex = 9;
            this.btnUpdateRepair.Text = "Update";
            this.btnUpdateRepair.UseVisualStyleBackColor = false;
            this.btnUpdateRepair.Click += new System.EventHandler(this.BtnUpdateRepair_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.LightSkyBlue;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(288, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(210, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "REPAIR MANAGEMENT ";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.cmbStatus);
            this.panel1.Controls.Add(this.cmbTechnicians);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.btnUpdateRepair);
            this.panel1.Location = new System.Drawing.Point(17, 405);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(777, 116);
            this.panel1.TabIndex = 16;
            // 
            // cmbStatus
            // 
            this.cmbStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbStatus.FormattingEnabled = true;
            this.cmbStatus.Location = new System.Drawing.Point(368, 28);
            this.cmbStatus.Name = "cmbStatus";
            this.cmbStatus.Size = new System.Drawing.Size(394, 24);
            this.cmbStatus.TabIndex = 13;
            // 
            // cmbTechnicians
            // 
            this.cmbTechnicians.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.cmbTechnicians.FormattingEnabled = true;
            this.cmbTechnicians.Location = new System.Drawing.Point(17, 28);
            this.cmbTechnicians.Name = "cmbTechnicians";
            this.cmbTechnicians.Size = new System.Drawing.Size(309, 24);
            this.cmbTechnicians.TabIndex = 12;
            // 
            // RepairControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label1);
            this.Name = "RepairControl";
            this.Size = new System.Drawing.Size(810, 532);
            this.Load += new System.EventHandler(this.RepairControl_Load);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRepairs)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DataGridView dgvRepairs;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnUpdateRepair;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cmbStatus;
        private System.Windows.Forms.ComboBox cmbTechnicians;
    }
}
