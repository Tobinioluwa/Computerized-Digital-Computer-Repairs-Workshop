using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    public partial class Technician : Form
    {

        private int technicianId;
        private string technicianName;
        private int loggedInTechId;

        public Technician(int id, string name)
        {

            InitializeComponent();

            technicianId = id;
            technicianName = name;
            loggedInTechId = technicianId;
            dgvRepairs.CellFormatting += dgvRepairs_CellFormatting;
            
        }

        private void dgvRepairs_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvRepairs.Columns[e.ColumnIndex].Name == "status")
            {
                string status = e.Value.ToString();

                if (status == "Pending")
                    e.CellStyle.ForeColor = Color.Red;
                else if (status == "In Progress")
                    e.CellStyle.ForeColor = Color.Orange;
                else if (status == "Completed")
                    e.CellStyle.ForeColor = Color.Green;
            }
        }


        private void Technician_Load(object sender, EventArgs e)
        {

            lblWelcome.Text = "Welcome, " + technicianName;
            LoadAssignedRepairs(technicianId); // use technicianId for queries

            cmbStatusFilter.Items.AddRange(new string[] { "All", "Pending", "In Progress", "Completed" });
            cmbStatusFilter.SelectedIndex = 0; // Default to "All"

            dtFrom.Value = DateTime.Today.AddDays(-7);
            dtTo.Value = DateTime.Today;

            LoadFilteredRepairs(); // Initial load

       
        }

        private void LoadAssignedRepairs(int technicianId)
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT ticket_id, device_details, status, date_created
            FROM repair_tickets
            WHERE technician_id = @techId";

                var cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@techId", technicianId);

                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                dgvRepairs.DataSource = table;

                dgvRepairs.Columns["ticket_id"].HeaderText = "ID";
                dgvRepairs.Columns["device_details"].HeaderText = "Device";
                dgvRepairs.Columns["status"].HeaderText = "Status";
                dgvRepairs.Columns["date_created"].HeaderText = "Assigned On";
            }
        }

        private void Button3_Click(object sender, EventArgs e)
        {
           
                if (dgvRepairs.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvRepairs.SelectedRows[0].Cells["ticket_id"].Value);

                    using (var conn = DB.GetConnection())
                    {
                        conn.Open();
                        var cmd = new MySqlCommand("UPDATE repair_tickets SET status = 'In Progress', date_updated = NOW() WHERE ticket_id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    LoadAssignedRepairs(loggedInTechId);
                }
            
        }


        private void LoadFilteredRepairs()
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();

                string query = @"
            SELECT ticket_id, device_details, issue_description, status, date_created
            FROM repair_tickets
            WHERE DATE(date_created) BETWEEN @from AND @to";

                if (cmbStatusFilter.SelectedItem.ToString() != "All")
                {
                    query += " AND status = @status";
                }

                // If technician page, limit to assigned technician
                if (this is Technician)
                {
                    query += " AND technician_id = @techId";
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@from", dtFrom.Value.Date);
                cmd.Parameters.AddWithValue("@to", dtTo.Value.Date);

                if (cmbStatusFilter.SelectedItem.ToString() != "All")
                {
                    cmd.Parameters.AddWithValue("@status", cmbStatusFilter.SelectedItem.ToString());
                }

                if (this is Technician)
                {
                    cmd.Parameters.AddWithValue("@techId", technicianId);
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgvRepairs.DataSource = table;

                // Format
                dgvRepairs.Columns["ticket_id"].HeaderText = "ID";
                dgvRepairs.Columns["device_details"].HeaderText = "Device";
                dgvRepairs.Columns["issue_description"].HeaderText = "Issue";
                dgvRepairs.Columns["status"].HeaderText = "Status";
                dgvRepairs.Columns["date_created"].HeaderText = "Date";
            }
        }


        private void Button4_Click(object sender, EventArgs e)
        {
           
                if (dgvRepairs.SelectedRows.Count > 0)
                {
                    int id = Convert.ToInt32(dgvRepairs.SelectedRows[0].Cells["ticket_id"].Value);

                    using (var conn = DB.GetConnection())
                    {
                        conn.Open();
                        var cmd = new MySqlCommand("UPDATE repair_tickets SET status = 'Completed', date_updated = NOW() WHERE ticket_id = @id", conn);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }

                    LoadAssignedRepairs(loggedInTechId);
                }
                    }

        private void Button2_Click(object sender, EventArgs e)
        {
            
               // if (dgvRepairs.SelectedRows.Count > 0)
               // {
                 //   int id = Convert.ToInt32(dgvRepairs.SelectedRows[0].Cells["ticket_id"].Value);
                   // var form = new RepairDetailsForm(id); // Create a form that shows details
                    //form.ShowDialog();
                }

        private void Button5_Click(object sender, EventArgs e)
        {
                            
                if (txtNewPass.Text != txtConfirmPass.Text)
                {
                    MessageBox.Show("Passwords do not match.");
                    return;
                }

                using (var conn = DB.GetConnection())
                {
                    conn.Open();

                    var cmd = new MySqlCommand("SELECT password FROM users WHERE user_id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", loggedInTechId);

                    string currentPassword = cmd.ExecuteScalar()?.ToString();

                    if (currentPassword != txtOldPass.Text)
                    {
                        MessageBox.Show("Incorrect old password.");
                        return;
                    }

                    cmd = new MySqlCommand("UPDATE users SET password = @new WHERE user_id = @id", conn);
                    cmd.Parameters.AddWithValue("@new", txtNewPass.Text);
                    cmd.Parameters.AddWithValue("@id", loggedInTechId);
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Password updated.");
                }
            

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void Button11_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 loginpage = new Form1();
            loginpage.Show();
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadFilteredRepairs();
        }

        private void Button2_Click_1(object sender, EventArgs e)
        {
            LoadFilteredRepairs();
        }

        private void DgvRepairs_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DtTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void DtFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void Label8_Click(object sender, EventArgs e)
        {

        }

        private void Label7_Click(object sender, EventArgs e)
        {

        }

        private void Label6_Click(object sender, EventArgs e)
        {

        }

        private void TxtConfirmPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label5_Click(object sender, EventArgs e)
        {

        }

        private void TxtNewPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label4_Click(object sender, EventArgs e)
        {

        }

        private void TxtOldPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Label2_Click(object sender, EventArgs e)
        {

        }

        private void LblWelcome_Click(object sender, EventArgs e)
        {

        }
    }
}
