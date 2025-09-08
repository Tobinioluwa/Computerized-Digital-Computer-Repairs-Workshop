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
    public partial class InventoryControl : UserControl
    {
        private int? editingProductId = null;
        
        public InventoryControl()
        {
            InitializeComponent();
        }

        private void InventoryControl_Load(object sender, EventArgs e)
        {
            LoadInventory();
        }


        private void LoadInventory()
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                string query = "SELECT product_id, name, description, price, quantity, low_stock_alert FROM inventory";
                var adapter = new MySqlDataAdapter(query, conn);
                var table = new DataTable();
                adapter.Fill(table);
                dgvInventory.DataSource = table;

                dgvInventory.Columns["product_id"].Visible = false;
                dgvInventory.Columns["name"].HeaderText = "Product Name";
                dgvInventory.Columns["description"].HeaderText = "Description";
                dgvInventory.Columns["price"].HeaderText = "Price (₦)";
                dgvInventory.Columns["quantity"].HeaderText = "Stock";
                dgvInventory.Columns["low_stock_alert"].HeaderText = "Low Stock Threshold";
            }
        }


        private void ResetForm()
        {
            txtProductName.Clear();
            txtDescription.Clear();
            txtPrice.Value = 0;
            txtQuantity.Value = 0;
            txtLowStock.Clear();
            editingProductId = null;
            btnAddProduct.Text = "Add Product";
        }



        private void BtnAddProduct_Click(object sender, EventArgs e)
        {


            // Validate inputs
            if (string.IsNullOrWhiteSpace(txtProductName.Text) ||
                string.IsNullOrWhiteSpace(txtDescription.Text) ||
                string.IsNullOrWhiteSpace(txtPrice.Text) ||
                string.IsNullOrWhiteSpace(txtQuantity.Text) ||
                string.IsNullOrWhiteSpace(txtLowStock.Text))
            {
                MessageBox.Show("All fields are required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string name = txtProductName.Text.Trim();
                string description = txtDescription.Text.Trim();
                decimal price = Convert.ToDecimal(txtPrice.Text);
                int quantity = Convert.ToInt32(txtQuantity.Text);
                int lowStock = Convert.ToInt32(txtLowStock.Text);

                using (var conn = DB.GetConnection())
                {
                    conn.Open();

                    if (editingProductId == null)
                    {
                        // ADD new product
                        string insertQuery = "INSERT INTO inventory (name, description, price, quantity, low_stock_alert) " +
                                             "VALUES (@name, @desc, @price, @qty, @alert)";
                        var cmd = new MySqlCommand(insertQuery, conn);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@desc", description);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@qty", quantity);
                        cmd.Parameters.AddWithValue("@alert", lowStock);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Product added successfully!", "Success");
                    }
                    else
                    {
                        // UPDATE existing product
                        string updateQuery = "UPDATE inventory SET name=@name, description=@desc, price=@price, quantity=@qty, low_stock_alert=@alert WHERE product_id=@id";
                        var cmd = new MySqlCommand(updateQuery, conn);
                        cmd.Parameters.AddWithValue("@name", name);
                        cmd.Parameters.AddWithValue("@desc", description);
                        cmd.Parameters.AddWithValue("@price", price);
                        cmd.Parameters.AddWithValue("@qty", quantity);
                        cmd.Parameters.AddWithValue("@alert", lowStock);
                        cmd.Parameters.AddWithValue("@id", editingProductId);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Product updated successfully!", "Updated");
                        editingProductId = null;
                        btnAddProduct.Text = "Add Product";
                    }
                }

                // Reset form and reload data
                txtProductName.Clear();
                txtDescription.Clear();
                txtPrice.Value = 0;
                txtQuantity.Value = 0;
                txtLowStock.Clear();
                LoadInventory();
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numbers for Price, Quantity, and Low Stock.", "Input Error");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Database Error");
            }




        }

        private void BtnEditItem_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                var row = dgvInventory.SelectedRows[0];
                editingProductId = Convert.ToInt32(row.Cells["product_id"].Value);

                txtProductName.Text = row.Cells["name"].Value.ToString();
                txtDescription.Text = row.Cells["description"].Value.ToString();
                txtPrice.Text = row.Cells["price"].Value.ToString();
                txtQuantity.Text = row.Cells["quantity"].Value.ToString();
                txtLowStock.Text = row.Cells["low_stock_alert"].Value.ToString();

                btnAddProduct.Text = "Update Product";
            }
            else
            {
                MessageBox.Show("Please select a product to edit.");
            }
        }

        private void BtnDeleteUser_Click(object sender, EventArgs e)
        {
            
                if (dgvInventory.SelectedRows.Count > 0)
                {
                    int productId = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["product_id"].Value);
                    var result = MessageBox.Show("Are you sure you want to delete this product?", "Confirm", MessageBoxButtons.YesNo);

                    if (result == DialogResult.Yes)
                    {
                        using (var conn = DB.GetConnection())
                        {
                            conn.Open();
                            string query = "DELETE FROM inventory WHERE product_id = @id";
                            var cmd = new MySqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@id", productId);
                            cmd.ExecuteNonQuery();
                            LoadInventory();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Please select a product to delete.");
                }
            
        }
    }
}
