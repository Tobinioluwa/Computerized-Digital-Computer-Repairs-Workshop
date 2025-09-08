using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    public partial class SellItemControl : UserControl
    {
        private int salesPersonId;
        private int _selectedCustomerId; // Declare a private field to store the ID

        
        public SellItemControl(int loggedInsalesId)
        {
            InitializeComponent();
            this.salesPersonId = loggedInsalesId;
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustomer.SelectedValue != null)
            {
                _selectedCustomerId = Convert.ToInt32(cmbCustomer.SelectedValue);
                // Now _selectedCustomerId holds the ID of the selected customer
                Console.WriteLine($"Selected Customer ID: {_selectedCustomerId}");
            }
        }

        private void LoadCustomers()
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                string query = "SELECT customer_id, name FROM customers";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                cmbCustomer.DataSource = dt;
                cmbCustomer.DisplayMember = "name";
                cmbCustomer.ValueMember = "customer_id";
                                
            }
        }


        private void LoadProducts()
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                string query = "SELECT product_id, name FROM inventory";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                DataTable dt = new DataTable();
                dt.Load(reader);

                cmbProducts.DataSource = dt;
                cmbProducts.DisplayMember = "name";
                cmbProducts.ValueMember = "product_id";
            }
        }







        private void SellItemControl_Load(object sender, EventArgs e)
        {

            dgvCart.Columns.Add("ProductID", "Product ID");
            dgvCart.Columns.Add("ProductName", "Product");
            dgvCart.Columns.Add("Price", "Price");
            dgvCart.Columns.Add("Quantity", "Qty");
            dgvCart.Columns.Add("Total", "Total");

            dgvCart.Columns["ProductID"].Visible = false;

            LoadCustomers();
            LoadProducts();

            // cmbPaymentType.Items.AddRange(new string[] { "Cash", "Card (Paystack)" });
            // cmbPaymentType.SelectedIndex = 0;

            lblTotal.Text = "0.00";
            

        }



        private void CalculateTotal()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvCart.Rows)
            {
                total += Convert.ToDecimal(row.Cells["Total"].Value);
            }
            lblTotal.Text = total.ToString("N2");
        }



        private void BtnAddToCart_Click(object sender, EventArgs e)
        {
            
                string keyword = cmbProducts.Text.Trim();
                int quantity = (int)numQty.Value;

                if (string.IsNullOrWhiteSpace(keyword) || quantity <= 0)
                {
                    MessageBox.Show("Enter product name and quantity.");
                    return;
                }

                using (var conn = DB.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT product_id, name, price FROM inventory WHERE name LIKE @keyword LIMIT 1";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@keyword", "%" + keyword + "%");

                    var reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int productId = Convert.ToInt32(reader["product_id"]);
                        string productName = reader["name"].ToString();
                        decimal price = Convert.ToDecimal(reader["price"]);
                        decimal total = price * quantity;

                        dgvCart.Rows.Add(productId, productName, price.ToString("N2"), quantity, total.ToString("N2"));

                        CalculateTotal();
                    cmbProducts.Text = "";
                        numQty.Value = 1;
                    }
                    else
                    {
                        MessageBox.Show("Product not found.");
                    }

                    reader.Close();
                }
            }

        private void BtnPayNow_Click(object sender, EventArgs e)
        {
            if (dgvCart.Rows.Count == 0)
            {
                MessageBox.Show("Cart is empty. Please add products to the cart before paying.");
                return;
            }

            
            // Input validation for payment method and customer selection
            if (cmbPaymentType.SelectedItem == null)
            {
                MessageBox.Show("Please select a payment method.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCustomer.SelectedValue == null) // Assuming cmbCustomer is your customer dropdown
            {
                MessageBox.Show("Please select a customer.", "Missing Information", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string paymentMethod = cmbPaymentType.SelectedItem.ToString();
            decimal grandTotalOverallSale = Convert.ToDecimal(lblTotal.Text); // This is the total for the entire transaction

            int customerId = Convert.ToInt32(cmbCustomer.SelectedValue); // Get the selected customer ID
            int employeeId = this.salesPersonId; // Assume salesPersonId is set during login

            string note = txtNote.Text.Trim(); // Assuming you have a TextBox named txtNote for sale notes

            using (var conn = DB.GetConnection())
            {
                conn.Open();

                // Start a transaction for atomicity (all or nothing)
                MySqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Get the current date and time for date_of_sale
                    DateTime dateOfSale = DateTime.Now;

                    // Loop through each item in the DataGridView (the cart)
                    foreach (DataGridViewRow row in dgvCart.Rows)
                    {
                        // Skip empty rows if any
                        if (row.IsNewRow) continue;

                        // Extract product details from the DataGridView row
                        int productId = Convert.ToInt32(row.Cells["ProductID"].Value);
                        int quantitySold = Convert.ToInt32(row.Cells["Quantity"].Value);
                        decimal unitPrice = Convert.ToDecimal(row.Cells["Price"].Value); // Assuming "Price" is unit price
                        decimal itemTotalPrice = Convert.ToDecimal(row.Cells["Total"].Value); // This is total for this specific product item

                        // Assuming discount and grand_total for each item will be derived from itemTotalPrice or a separate discount field
                        // If you have a per-item discount in your DGV, retrieve it here.
                        // For simplicity, let's assume discount is 0 for individual items if not specified,
                        // and grand_total for the item is the itemTotalPrice unless a specific per-item discount is applied.
                        decimal itemDiscount = 0.00m; // You might need a column for this in your dgvCart
                                                      // If your DGV has a discount column for each item:
                                                      // if (row.Cells.Contains("Discount") && row.Cells["Discount"].Value != null)
                                                      // {
                                                      //     itemDiscount = Convert.ToDecimal(row.Cells["Discount"].Value);
                                                      // }

                        decimal itemGrandTotal = itemTotalPrice - itemDiscount;


                        // The SQL INSERT statement for each product item
                        string insertItemQuery = @"
                    INSERT INTO sales
                    (product_id, quantity_sold, total_price, discount, grand_total, payment_method, customer_id, employee_id, notes, date_of_sale)
                    VALUES
                    (@productId, @quantitySold, @totalPrice, @discount, @grandTotal, @paymentMethod, @customerId, @employeeId, @note, @dateOfSale);";

                        MySqlCommand cmd = new MySqlCommand(insertItemQuery, conn, transaction); // Pass the transaction

                        // Add parameters for the current product item
                        cmd.Parameters.AddWithValue("@productId", productId);
                        cmd.Parameters.AddWithValue("@quantitySold", quantitySold);
                        cmd.Parameters.AddWithValue("@totalPrice", unitPrice); // Store unit price for this product item, or the row's total_price if "Price" in DGV is already total for item
                        cmd.Parameters.AddWithValue("@discount", itemDiscount); // If per-item discount is applied
                        cmd.Parameters.AddWithValue("@grandTotal", itemGrandTotal); // Grand total for this specific item

                        cmd.Parameters.AddWithValue("@paymentMethod", paymentMethod);
                        cmd.Parameters.AddWithValue("@customerId", customerId);
                        cmd.Parameters.AddWithValue("@employeeId", employeeId);
                        cmd.Parameters.AddWithValue("@note", note);
                        cmd.Parameters.AddWithValue("@dateOfSale", dateOfSale); // Current date and time

                        cmd.ExecuteNonQuery();

                        // Optionally, update product stock here if you have a products table
                        // e.g., UpdateProductStock(productId, quantitySold, conn, transaction);
                    }

                    // If all insertions were successful, commit the transaction
                    transaction.Commit();
                    MessageBox.Show("Sale completed successfully. All product details recorded.", "Sale Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Clear the cart and update total after successful sale
                    dgvCart.Rows.Clear();
                    lblTotal.Text = "0.00"; // Reset grand total display
                                            // Also reset customer selection and payment type if needed
                    cmbCustomer.SelectedIndex = -1;
                    cmbPaymentType.SelectedIndex = -1;
                    txtNote.Clear();
                }
                catch (Exception ex)
                {
                    // If any error occurs, roll back the transaction
                    transaction.Rollback();
                    MessageBox.Show("An error occurred during the sale: " + ex.Message, "Sale Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    // Log the exception for debugging
                    // Console.WriteLine(ex.ToString());
                }
            }
        }

        private void BtnPrintReceipt_Click(object sender, EventArgs e)
        {

        }
    }
}
