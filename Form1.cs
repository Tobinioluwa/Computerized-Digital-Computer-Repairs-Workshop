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
    public partial class Form1 : Form
    {
        MySql.Data.MySqlClient.MySqlConnection conn = new MySql.Data.MySqlClient.MySqlConnection("server=localhost;userid=root;password=;database=computershopmanagement;");
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtusername.Text) || string.IsNullOrWhiteSpace(txtpassword.Text))
            {
                MessageBox.Show("Please enter both Username and Password", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            progressBar1.Visible = true;
            progressBar1.Style = ProgressBarStyle.Marquee;
            Application.DoEvents();

            string username = txtusername.Text.Trim();
            string password = txtpassword.Text.Trim();

            string connectionString = "server=localhost;userid=root;password=;database=computershopmanagement;";
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    string query = "SELECT user_id, name, role FROM users WHERE username = @username AND password = @password";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);

                    MySqlDataReader reader = cmd.ExecuteReader();

                    progressBar1.Visible = false;

                    if (reader.Read())
                    {
                        int userId = Convert.ToInt32(reader["user_id"]);
                        string fullName = reader["name"].ToString();
                        string role = reader["role"].ToString().ToLower();

                        MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Hide();

                        switch (role)
                        {
                            case "admin":
                                new AdministratorPage().Show();
                                break;
                            case "technician":
                                new Technician(userId, fullName).Show(); 
                                // ✅ pass details
                                break;
                            case "sales":
                                new salespage(userId, fullName).Show();   
                                // ✅ pass details
                                break;
                            default:
                                MessageBox.Show("Unrecognized role.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid credentials.", "Login Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    reader.Close();
                }
                catch (Exception ex)
                {
                    progressBar1.Visible = false;
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
            ////// clear both input boxes
            txtusername.Text = " ";
            txtpassword.Text = "";

        }


        private void ProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void Button4_Click(object sender, EventArgs e)
        {
            this.Hide();

            RegistrationPage Registerpage = new RegistrationPage();
            Registerpage.Show();
        }
    }
    }


    

