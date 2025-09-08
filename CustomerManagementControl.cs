using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    public partial class CustomerManagementControl : UserControl
    {
        public CustomerManagementControl()
        {
            InitializeComponent();
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void LoadCustomers(string searchTerm = "")
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                string query = @"
                    SELECT 
                        c.customer_id, 
                        c.name, 
                        c.phone, 
                        c.email,
                        (SELECT COUNT(*) FROM repair_tickets r WHERE r.customer_id = c.customer_id) AS repairs,
                        (SELECT COUNT(*) FROM sales s WHERE s.customer_id = c.customer_id) AS purchases,
                        (SELECT COUNT(*) FROM repair_tickets r WHERE r.customer_id = c.customer_id AND r.status != 'Completed') AS unresolved
                    FROM customers c";

                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    query += " WHERE c.name LIKE @term OR c.phone LIKE @term";
                }

                var cmd = new MySqlCommand(query, conn);
                if (!string.IsNullOrWhiteSpace(searchTerm))
                {
                    cmd.Parameters.AddWithValue("@term", "%" + searchTerm + "%");
                }

                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                dgvCustomers.DataSource = table;

                // Column settings
                dgvCustomers.Columns["customer_id"].Visible = false;
                dgvCustomers.Columns["name"].HeaderText = "Name";
                dgvCustomers.Columns["phone"].HeaderText = "Phone";
                dgvCustomers.Columns["email"].HeaderText = "Email";
                dgvCustomers.Columns["repairs"].HeaderText = "Repairs";
                dgvCustomers.Columns["purchases"].HeaderText = "Purchases";
                dgvCustomers.Columns["unresolved"].Visible = false;

                // 🔴 Highlight unresolved customers
                foreach (DataGridViewRow row in dgvCustomers.Rows)
                {
                    if (row.Cells["unresolved"].Value != DBNull.Value)
                    {
                        int unresolved = Convert.ToInt32(row.Cells["unresolved"].Value);
                        if (unresolved > 0)
                        {
                            row.DefaultCellStyle.BackColor = Color.LightSalmon;
                        }
                    }
                }
            }
        }


        private void CustomerManagementControl_Load(object sender, EventArgs e)
        {
            LoadCustomers();

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();
            LoadCustomers(searchTerm);
        }

        private void BtnViewProfile_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                int customerId = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["customer_id"].Value);

                // You can open a modal or another user control to show this customer's repair and sales history
                // CustomerProfileForm profile = new CustomerProfileForm(customerId);
                // profile.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a customer.");
            }

        }

        private void BtnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dgvCustomers.SelectedRows[0].Cells["customer_id"].Value);
                var confirm = MessageBox.Show("Are you sure you want to delete this customer?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    using (var conn = DB.GetConnection())
                    {
                        conn.Open();
                        var cmd = new MySqlCommand("DELETE FROM customers WHERE customer_id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                    LoadCustomers();
                }
            }
        }

        private void BtnAddCustomer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                string.IsNullOrWhiteSpace(txtPhone.Text) ||
                string.IsNullOrWhiteSpace(txtEmail.Text) ||
                string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("All fields must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (var conn = DB.GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO customers (name, phone, email, address) VALUES (@name, @phone, @email, @address)";
                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@name", txtFullName.Text.Trim());
                cmd.Parameters.AddWithValue("@phone", txtPhone.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@address", txtAddress.Text.Trim());

                cmd.ExecuteNonQuery();
            }

            MessageBox.Show("Customer added successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Clear inputs
            txtFullName.Clear();
            txtPhone.Clear();
            txtEmail.Clear();
            txtAddress.Clear();

            LoadCustomers();
        }

       
    }
}
