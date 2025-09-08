using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    public partial class RegistrationPage : Form
    {
        string connStr = "server=localhost;userid=root;password=;database=csm-system;";


        public RegistrationPage()
        {
            
            InitializeComponent();
            progressBar1.Visible = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 loginpage = new Form1();
            loginpage.Show();


            
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            string username = txtusername.Text.Trim();
            string fullname = txtfullname.Text.Trim();
            string password = txtpassword.Text.Trim();
            string role = "";

            // Determine selected role
            if (rdAdmin.Checked)
                role = rdAdmin.Text;
            else if (rdsales.Checked)
                role = rdsales.Text;
            else if (rdTechnician.Checked)
                role = rdTechnician.Text;

            // Validate inputs
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(fullname) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(role))
            {
                MessageBox.Show("All fields including role must be filled.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Start progress bar simulation
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            timer1.Start();

            try
            {
                using (var conn = DB.GetConnection())
                {
                    conn.Open();

                    // Check if username already exists
                    string checkQuery = "SELECT COUNT(*) FROM users WHERE username = @username";
                    MySqlCommand checkCmd = new MySqlCommand(checkQuery, conn);
                    checkCmd.Parameters.AddWithValue("@username", username);
                    int exists = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (exists > 0)
                    {
                        timer1.Stop();
                        progressBar1.Visible = false;
                        MessageBox.Show("Username already exists. Please choose another.", "Registration Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Insert new user
                    string insertQuery = "INSERT INTO users (name, username, password, role) VALUES (@name, @username, @password, @role)";
                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, conn);
                    insertCmd.Parameters.AddWithValue("@name", fullname);
                    insertCmd.Parameters.AddWithValue("@username", username);
                    insertCmd.Parameters.AddWithValue("@password", password); // 🔐 consider hashing
                    insertCmd.Parameters.AddWithValue("@role", role);

                    int result = insertCmd.ExecuteNonQuery();

                    timer1.Stop();
                    progressBar1.Visible = false;

                    if (result > 0)
                    {
                        MessageBox.Show("Registration successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Clear fields
                        txtusername.Clear();
                        txtfullname.Clear();
                        txtpassword.Clear();
                        rdAdmin.Checked = false;
                        rdsales.Checked = false;
                        rdTechnician.Checked = false;
                    }
                    else
                    {
                        MessageBox.Show("Failed to register. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                timer1.Stop();
                progressBar1.Visible = false;
                MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




            ////////////////////////
        }

        private void RegistrationPage_Load(object sender, EventArgs e)
        {

        }

        private void ProgressBar1_Click(object sender, EventArgs e)
        {
            





        }


        private void SaveToDatabase()
        {
            string username = txtusername.Text.Trim();
            string email = txtfullname.Text.Trim();
            string password = txtpassword.Text.Trim();
            string role = rdAdmin.Checked ? rdAdmin.Text : rdsales.Checked ? rdsales.Text : rdTechnician.Text;


            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    string sql = "INSERT INTO user (username, email, password, role) VALUES (@username, @email, @password, @role)";
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", password); // Hash in production
                    cmd.Parameters.AddWithValue("@role", role);

                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0)
                    {
                        MessageBox.Show("User saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Insert failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Database Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    progressBar1.Visible = false;
                }
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 10;
            }
            else
            {
                timer1.Stop();

                // After progress bar completes, save to DB
                SaveToDatabase();

                this.Hide();
                Form1 loginpage = new Form1();
                loginpage.Show();


            }
        }
    }
}



