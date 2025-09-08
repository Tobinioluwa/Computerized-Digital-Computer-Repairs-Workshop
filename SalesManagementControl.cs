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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;



namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    public partial class SalesManagementControl : UserControl
    {
        public SalesManagementControl()
        {
            InitializeComponent();
        }


        private void ExportDataGridViewToPdf(DataGridView dgv, string filename)
        {
            Document doc = new Document(PageSize.A4, 10, 10, 10, 10);
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

            doc.Add(new Paragraph("Sales Report", FontFactory.GetFont("Arial", 16f, iTextSharp.text.Font.BOLD)));
            doc.Add(new Paragraph("Generated: " + DateTime.Now.ToString("dd MMM yyyy hh:mm tt")));
            doc.Add(new Paragraph(" "));
            doc.Add(table);
            doc.Close();
        }

        private void LoadSales(DateTime? from = null, DateTime? to = null)
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();
                string query = @"
            SELECT s.sale_id, i.name AS product, s.quantity_sold, s.total_price, s.date_of_sale
            FROM sales s
            INNER JOIN inventory i ON s.product_id = i.product_id
            WHERE 1 = 1";

                if (from.HasValue && to.HasValue)
                {
                    query += " AND DATE(s.date_of_sale) BETWEEN @from AND @to";
                }

                var cmd = new MySqlCommand(query, conn);
                if (from.HasValue && to.HasValue)
                {
                    cmd.Parameters.AddWithValue("@from", from.Value.Date);
                    cmd.Parameters.AddWithValue("@to", to.Value.Date);
                }

                var adapter = new MySqlDataAdapter(cmd);
                var table = new DataTable();
                adapter.Fill(table);
                dgvSales.DataSource = table;

                dgvSales.Columns["sale_id"].HeaderText = "Sale ID";
                dgvSales.Columns["product"].HeaderText = "Product";
                dgvSales.Columns["quantity_sold"].HeaderText = "Qty";
                dgvSales.Columns["total_price"].HeaderText = "Total Price (₦)";
                dgvSales.Columns["date_of_sale"].HeaderText = "Date";

                // Calculate total
                decimal total = 0;
                foreach (DataRow row in table.Rows)
                {
                    if (row["total_price"] != DBNull.Value)
                        total += Convert.ToDecimal(row["total_price"]);
                }

                lblTotalSales.Text = total.ToString("N2");

                LoadChart(table);
            }
        }

        private void SalesManagementControl_Load(object sender, EventArgs e)
        {

            dtFrom.Value = DateTime.Today.AddDays(-7); // default to past week
            dtTo.Value = DateTime.Today;
            LoadSales();

        }

        private void LoadChart(DataTable salesTable)
        {
            chartSales.Series.Clear();
            chartSales.ChartAreas[0].AxisX.Title = "Date";
            chartSales.ChartAreas[0].AxisY.Title = "Total ₦";

            var series = chartSales.Series.Add("Sales");
            series.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;

            var grouped = salesTable.AsEnumerable()
                .GroupBy(r => Convert.ToDateTime(r["date_of_sale"]).ToShortDateString())
                .Select(g => new {
                    Date = g.Key,
                    Total = g.Sum(r => Convert.ToDecimal(r["total_price"]))
                });

            foreach (var data in grouped)
            {
                series.Points.AddXY(data.Date, data.Total);
            }
        }


        private void BtnFilter_Click(object sender, EventArgs e)
        {
            LoadSales(dtFrom.Value, dtTo.Value);
        }

        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF files (*.pdf)|*.pdf";
            save.FileName = "SalesReport.pdf";
            if (save.ShowDialog() == DialogResult.OK)
            {
                ExportDataGridViewToPdf(dgvSales, save.FileName);
                MessageBox.Show("PDF export successful!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
