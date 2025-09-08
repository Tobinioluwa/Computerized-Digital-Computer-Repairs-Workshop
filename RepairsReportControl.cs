using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    public partial class RepairsReportControl : UserControl
    {
        public RepairsReportControl()
        {
            InitializeComponent();
        }

        private void RepairsReportControl_Load(object sender, EventArgs e)
        {
            dtFrom.Value = DateTime.Today.AddDays(-7);
            dtTo.Value = DateTime.Today;
            cmbStatus.Items.AddRange(new string[] { "All", "Pending", "In Progress", "Completed" });
            cmbStatus.SelectedIndex = 0;
            LoadRepairs();
        }



        private void LoadRepairs()
        {
            using (var conn = DB.GetConnection())
            {
                conn.Open();

                string query = @"
            SELECT 
                r.ticket_id, 
                c.name AS customer, 
                u.name AS technician, 
                r.device_details, 
                r.issue_description, 
                r.status, 
                r.date_created
            FROM repair_tickets r
            LEFT JOIN customers c ON r.customer_id = c.customer_id
            LEFT JOIN users u ON r.technician_id = u.user_id
            WHERE DATE(r.date_created) BETWEEN @from AND @to";

                if (cmbStatus.SelectedItem != null && cmbStatus.SelectedItem.ToString() != "All")
                {
                    query += " AND r.status = @status";
                }

                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@from", dtFrom.Value.Date);
                cmd.Parameters.AddWithValue("@to", dtTo.Value.Date);

                if (cmbStatus.SelectedItem != null && cmbStatus.SelectedItem.ToString() != "All")
                {
                    cmd.Parameters.AddWithValue("@status", cmbStatus.SelectedItem.ToString());
                }

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgvRepairs.DataSource = table;

                dgvRepairs.Columns["ticket_id"].HeaderText = "Ticket ID";
                dgvRepairs.Columns["customer"].HeaderText = "Customer";
                dgvRepairs.Columns["technician"].HeaderText = "Technician";
                dgvRepairs.Columns["device_details"].HeaderText = "Device";
                dgvRepairs.Columns["issue_description"].HeaderText = "Issue";
                dgvRepairs.Columns["status"].HeaderText = "Status";
                dgvRepairs.Columns["date_created"].HeaderText = "Date";
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
           LoadRepairs();
            

        }

        private void ExportToPdf(DataGridView dgv, string filename)
        {
            Document doc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            PdfWriter.GetInstance(doc, new FileStream(filename, FileMode.Create));
            doc.Open();

            PdfPTable table = new PdfPTable(dgv.Columns.Count);
            table.WidthPercentage = 100;

            foreach (DataGridViewColumn col in dgv.Columns)
            {
                if (col.Visible)
                {
                    PdfPCell cell = new PdfPCell(new Phrase(col.HeaderText));
                    cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                    table.AddCell(cell);
                }
            }

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

            doc.Add(new Paragraph("Repair Report", FontFactory.GetFont("Arial", 16f, iTextSharp.text.Font.BOLD)));
            doc.Add(new Paragraph("Generated: " + DateTime.Now.ToString("dd MMM yyyy hh:mm tt")));
            doc.Add(new Paragraph(" "));
            doc.Add(table);
            doc.Close();
        }


        private void BtnExportPdf_Click(object sender, EventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "PDF files (*.pdf)|*.pdf";
            save.FileName = "RepairReport.pdf";

            if (save.ShowDialog() == DialogResult.OK)
            {
                ExportToPdf(dgvRepairs, save.FileName);
                MessageBox.Show("Exported to PDF successfully!");
            }
        }
    }
}
