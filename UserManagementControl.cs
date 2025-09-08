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
    public partial class UserManagementControl : UserControl
    {
        public UserManagementControl()
        {
            InitializeComponent();
        }

        private void UserManagementControl_Load(object sender, EventArgs e)
        {
            
            LoadUsers();
        }



        private void LoadUsers()
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                var adapter = new MySqlDataAdapter("SELECT user_id, name, username, role, date_created FROM users", conn);
                var table = new DataTable();
                adapter.Fill(table);
                dgvUsers.DataSource = table;
            }
        }






        private void BtnAddUser_Click(object sender, EventArgs e)
        {
           
                if (string.IsNullOrWhiteSpace(txtFullName.Text) ||
                    string.IsNullOrWhiteSpace(txtUsername.Text) ||
                    string.IsNullOrWhiteSpace(txtPassword.Text) ||
                    cmbRole.SelectedIndex == -1)
                {
                    MessageBox.Show("All fields are required.");
                    return;
                }

                using (var conn = DB.GetConnection())
                {
                    conn.Open();

                    if (btnAddUser.Tag == null)
                    {
                        // ADD new user
                        string query = "INSERT INTO users (name, username, password, role) VALUES (@name, @username, @password, @role)";
                        var cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@name", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@role", cmbRole.Text);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("User added successfully!");
                    }
                    else
                    {
                        // UPDATE existing user
                        int userId = Convert.ToInt32(btnAddUser.Tag);
                        string query = "UPDATE users SET name=@name, username=@username, password=@password, role=@role WHERE user_id=@id";
                        var cmd = new MySqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@name", txtFullName.Text);
                        cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                        cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                        cmd.Parameters.AddWithValue("@role", cmbRole.Text);
                        cmd.Parameters.AddWithValue("@id", userId);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("User updated successfully!");
                    }

                    // Reset form
                    txtFullName.Clear();
                    txtUsername.Clear();
                    txtPassword.Clear();
                    cmbRole.SelectedIndex = -1;
                    btnAddUser.Tag = null;
                    btnAddUser.Text = "Add User";

                    LoadUsers();
                }
            }


        

        private void BtnEditUser_Click(object sender, EventArgs e)
        {
           
                if (dgvUsers.SelectedRows.Count > 0)
                {
                    DataGridViewRow row = dgvUsers.SelectedRows[0];
                    txtFullName.Text = row.Cells["name"].Value.ToString();
                    txtUsername.Text = row.Cells["username"].Value.ToString();
                    cmbRole.Text = row.Cells["role"].Value.ToString();

                    // Store user_id in Tag or a variable for later use when updating
                    btnAddUser.Tag = row.Cells["user_id"].Value;

                    btnAddUser.Text = "Update User"; // Change button text to indicate it's an update
                }
                else
                {
                    MessageBox.Show("Please select a user to edit.");
                }
            

        }
    }
}
