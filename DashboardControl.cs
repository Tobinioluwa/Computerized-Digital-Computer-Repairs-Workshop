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
    public partial class DashboardControl : UserControl
    {
        public DashboardControl()
        {
            InitializeComponent();
               }
       
        private void DashboardControl_Load(object sender, EventArgs e)
        { 
            
            lblDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy hh:mm tt");

            LoadDashboardData();
        }

            
        private void LoadDashboardData()
        {

         using (var conn = DB.GetConnection())
          {
            conn.Open();


                // Sales Today
                var cmd1 = new MySqlCommand("SELECT SUM(total_price) FROM sales WHERE DATE(date_of_sale) = CURDATE()", conn);
                var result1 = cmd1.ExecuteScalar();
                decimal totalSales = (result1 == DBNull.Value || result1 == null) ? 0 : Convert.ToDecimal(result1);
                lblSalesToday.Text = "₦" + totalSales.ToString("N2");

                // Pending Repairs
                var cmd2 = new MySqlCommand("SELECT COUNT(*) FROM repair_tickets WHERE status = 'Pending'", conn);
                var result2 = cmd2.ExecuteScalar();
                int pendingRepairs = (result2 == DBNull.Value || result2 == null) ? 0 : Convert.ToInt32(result2);
                lblPendingRepairs.Text = pendingRepairs.ToString();

                // Low Stock
                var cmd3 = new MySqlCommand("SELECT COUNT(*) FROM inventory WHERE quantity <= low_stock_alert", conn);
                var result3 = cmd3.ExecuteScalar();
                int lowStock = (result3 == DBNull.Value || result3 == null) ? 0 : Convert.ToInt32(result3);
                lblLowStock.Text = lowStock.ToString();

                // Staff Count
                var cmd4 = new MySqlCommand("SELECT COUNT(*) FROM users", conn);
                var result4 = cmd4.ExecuteScalar();
                int staffCount = (result4 == DBNull.Value || result4 == null) ? 0 : Convert.ToInt32(result4);
                lblStaffCount.Text = staffCount.ToString();




            }
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
