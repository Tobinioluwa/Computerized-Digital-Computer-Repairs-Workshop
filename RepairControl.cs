using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using MySql.Data;


namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    public partial class RepairControl : UserControl
    {
        public RepairControl()
        {
            InitializeComponent();
        }

        private void RepairControl_Load(object sender, EventArgs e)
        {
            cmbStatus.Items.AddRange(new string[] { "Pending", "In Progress", "Completed" });
            LoadTechnicians();
            LoadRepairs();
        }

        private void LoadTechnicians()
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                string query = "SELECT user_id, name FROM users WHERE role = 'Technician'";
                var cmd = new MySqlCommand(query, conn);
                var reader = cmd.ExecuteReader();

                Dictionary<int, string> techList = new Dictionary<int, string>();
                while (reader.Read())
                {
                    techList.Add(reader.GetInt32("user_id"), reader.GetString("name"));
                }

                cmbTechnicians.DataSource = new BindingSource(techList, null);
                cmbTechnicians.DisplayMember = "Value";
                cmbTechnicians.ValueMember = "Key";
            }
        }


        private void LoadRepairs()
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT r.ticket_id, c.name AS customer, r.device_details, r.issue_description, 
                   r.status, u.name AS technician, r.date_created 
            FROM repair_tickets r
            LEFT JOIN customers c ON r.customer_id = c.customer_id
            LEFT JOIN users u ON r.technician_id = u.user_id";

                var adapter = new MySqlDataAdapter(query, conn);
                var table = new DataTable();
                adapter.Fill(table);
                dgvRepairs.DataSource = table;

                dgvRepairs.Columns["ticket_id"].HeaderText = "Ticket ID";
                dgvRepairs.Columns["customer"].HeaderText = "Customer";
                dgvRepairs.Columns["device_details"].HeaderText = "Device";
                dgvRepairs.Columns["issue_description"].HeaderText = "Issue";
                dgvRepairs.Columns["status"].HeaderText = "Status";
                dgvRepairs.Columns["technician"].HeaderText = "Technician";
                dgvRepairs.Columns["date_created"].HeaderText = "Date Created";
            }
        }

        private void BtnUpdateRepair_Click(object sender, EventArgs e)
        {
            if (cmbStatus.Text == "")
            {
                MessageBox.Show("Please select a Status also");
                return; }


                           if (dgvRepairs.SelectedRows.Count > 0)
                {
                    int ticketId = Convert.ToInt32(dgvRepairs.SelectedRows[0].Cells["ticket_id"].Value);
                    int technicianId = ((KeyValuePair<int, string>)cmbTechnicians.SelectedItem).Key;
                    string status = cmbStatus.SelectedItem.ToString();

                    using (var conn = DB.GetConnection())
                    {
                        conn.Open();
                        string query = "UPDATE repair_tickets SET technician_id = @tech, status = @status WHERE ticket_id = @id";
                        var cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@tech", technicianId);
                        cmd.Parameters.AddWithValue("@status", status);
                        cmd.Parameters.AddWithValue("@id", ticketId);
                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show("Repair ticket updated.");
                    LoadRepairs();
                }
                else
                {
                    MessageBox.Show("Please select a repair ticket.");
                }
            
        }
    }
}
