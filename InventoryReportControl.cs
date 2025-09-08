using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MySql.Data.MySqlClient;

namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    public partial class InventoryReportControl : UserControl
    {
        public InventoryReportControl()
        {
            InitializeComponent();
        }

        private void LoadInventory()
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                string query = "SELECT product_id, name, quantity, low_stock_alert, price FROM inventory";

                if (chkLowStockOnly.Checked)
                {
                    query += " WHERE quantity <= low_stock_alert";
                }

                var adapter = new MySqlDataAdapter(query, conn);
                var table = new DataTable();
                adapter.Fill(table);
                dgvInventory.DataSource = table;

                dgvInventory.Columns["product_id"].Visible = false;
                dgvInventory.Columns["name"].HeaderText = "Product";
                dgvInventory.Columns["quantity"].HeaderText = "Quantity";
                dgvInventory.Columns["low_stock_alert"].HeaderText = "Low Threshold";
                dgvInventory.Columns["price"].HeaderText = "Price (₦)";

                CalculateTotalValue();
            }
        }

        private void InventoryReportControl_Load(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void CalculateTotalValue()
        {
            decimal total = 0;
            foreach (DataGridViewRow row in dgvInventory.Rows)
            {
                if (row.Cells["quantity"].Value != DBNull.Value && row.Cells["price"].Value != DBNull.Value)
                {
                    int qty = Convert.ToInt32(row.Cells["quantity"].Value);
                    decimal price = Convert.ToDecimal(row.Cells["price"].Value);
                    total += qty * price;
                }
            }

            lblTotalValue.Text = total.ToString("N2");
        }


        private void ChkLowStockOnly_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
           
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = "PDF files (*.pdf)|*.pdf";
                save.FileName = "InventoryReport.pdf";

                if (save.ShowDialog() == DialogResult.OK)
                {
                    ExportInventoryToPdf(dgvInventory, save.FileName);
                    MessageBox.Show("Exported successfully!", "PDF Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            

        }



        private void ExportInventoryToPdf(DataGridView dgv, string filename)
        {
            Document doc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
            doc.Open();

            PdfPTable table = new PdfPTable(dgv.Columns.Count);
            table.WidthPercentage = 100;

            // Add headers
            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }
            }

            // Add rows
            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.IsNewRow) continue;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.OwningColumn.Visible)
                    {
                        table.AddCell(cell.Value?.ToString());
                    }
                }
            }

            doc.Add(new Paragraph("Inventory Report", FontFactory.GetFont("Arial", 16f, iTextSharp.text.Font.BOLD)));
            doc.Add(new Paragraph("Generated: " + DateTime.Now.ToString("dd MMM yyyy hh:mm tt")));
            doc.Add(new Paragraph("Total Inventory Value: ₦" + lblTotalValue.Text));
            doc.Add(new Paragraph(" "));
            doc.Add(table);
            doc.Close();
        }

        private void ChkLowStockOnly_CheckedChanged(object sender, EventArgs e)
        {
            LoadInventory();
        }
    }
}
