using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    public partial class BookRepairs : UserControl
    {
        public BookRepairs()
        {
            
            InitializeComponent();
            cmbRepairPayment.Items.AddRange(new string[] { "Cash", "Transfer", "Card (Flutterwave)" });
            LoadRepairCustomers();

        }


        private void LoadRepairCustomers()
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                string query = "SELECT customer_id, name FROM customers";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                cmbRepairCustomer.DataSource = dt;
                cmbRepairCustomer.DisplayMember = "name";
                cmbRepairCustomer.ValueMember = "customer_id";
            }
        }



        private void SaveRepairToDatabase(int customerId, string device, string issue, decimal amount, string method, string status)
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"INSERT INTO repair_tickets 
            (customer_id, technician_id, device_details, issue_description, status, notes, date_created, date_updated)
            VALUES (@cust, NULL, @device, @issue, 'Pending', CONCAT('Payment: ', @method), NOW(), NOW())", conn);

                cmd.Parameters.AddWithValue("@cust", customerId);
                cmd.Parameters.AddWithValue("@device", device);
                cmd.Parameters.AddWithValue("@issue", issue);
                cmd.Parameters.AddWithValue("@method", method);
                cmd.ExecuteNonQuery();

                // Optionally: Log into a payments table as well
                MessageBox.Show("Repair booked and payment saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Clear fields
                txtDevice.Clear();
                txtIssue.Clear();
                txtRepairAmount.Value = 0;
                cmbRepairCustomer.SelectedIndex = 0;
                cmbRepairPayment.SelectedIndex = 0;
            }
        }


        private void ProcessFlutterwavePayment(int customerId, string device, string issue, decimal amount)
        {
            // 🔒 You’ll need to create a web request to Flutterwave’s API
            // Open payment URL → Wait for user to complete
            // On success:
            bool paymentSuccess = true; // Simulated for now

            if (paymentSuccess)
            {
                SaveRepairToDatabase(customerId, device, issue, amount, "Card (Flutterwave)", "Paid");
            }
            else
            {
                MessageBox.Show("Payment failed or cancelled.", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnPayRepair_Click(object sender, EventArgs e)
        {
            if (cmbRepairCustomer.Text == "" || txtDevice.Text == "" || txtIssue.Text == "" || txtDevice.Text == "" || cmbRepairPayment.Text == "")
            {
                MessageBox.Show("Enter product name and quantity.");
                return;
            }



            if (cmbRepairCustomer.SelectedValue == null || string.IsNullOrWhiteSpace(txtDevice.Text) ||
                    string.IsNullOrWhiteSpace(txtIssue.Text) || string.IsNullOrWhiteSpace(txtRepairAmount.Text) ||
                    cmbRepairPayment.SelectedItem == null)
                {
                    MessageBox.Show("All fields must be filled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int customerId = Convert.ToInt32(cmbRepairCustomer.SelectedValue);
                string device = txtDevice.Text.Trim();
                string issue = txtIssue.Text.Trim();
                string paymentMethod = cmbRepairPayment.SelectedItem.ToString();
                decimal amount = 0;

                if (!decimal.TryParse(txtRepairAmount.Text, out amount) || amount <= 0)
                {
                    MessageBox.Show("Enter a valid repair amount.");
                    return;
                }

                if (paymentMethod == "Transfer" || paymentMethod == "Cash")
                {
                    DialogResult result = MessageBox.Show($"Has the customer paid ₦{amount} by {paymentMethod}?", "Payment Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result != DialogResult.Yes)
                    {
                        return;
                    }

                    SaveRepairToDatabase(customerId, device, issue, amount, paymentMethod, "Paid");
                }
                else if (paymentMethod.Contains("Flutterwave"))
                {
                    // 👇 You will trigger Flutterwave API here
                    ProcessFlutterwavePayment(customerId, device, issue, amount);
                }
            }

        private void BookRepairs_Load(object sender, EventArgs e)
        {

        }
    }
}
