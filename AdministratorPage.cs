
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    public partial class AdministratorPage : Form
    {
        public AdministratorPage()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        
        private void Button1_Click_1(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void AdministratorPage_Load(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var dashboard = new DashboardControl();
            dashboard.Dock = DockStyle.Fill;

            panelMain.Controls.Add(dashboard);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 loginpage = new Form1();
            loginpage.Show();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var dashboard = new DashboardControl();
            dashboard.Dock = DockStyle.Fill;

            panelMain.Controls.Add(dashboard);


        }

        private void Button3_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var dashboard = new UserManagementControl();
            dashboard.Dock = DockStyle.Fill;

            panelMain.Controls.Add(dashboard);
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var dashboard = new InventoryControl();
            dashboard.Dock = DockStyle.Fill;

            panelMain.Controls.Add(dashboard);
        }

        private void Button5_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var dashboard = new RepairControl();
            dashboard.Dock = DockStyle.Fill;

            panelMain.Controls.Add(dashboard);
        }

        
        private void Button6_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var dashboard = new CustomerManagementControl();
            dashboard.Dock = DockStyle.Fill;

            panelMain.Controls.Add(dashboard);
        }

        private void Button7_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var dashboard = new SalesManagementControl();
            dashboard.Dock = DockStyle.Fill;
             
            panelMain.Controls.Add(dashboard);
        }

        private void Button8_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var dashboard = new RepairsReportControl();
            dashboard.Dock = DockStyle.Fill;

            panelMain.Controls.Add(dashboard);
        }

        private void Button9_Click(object sender, EventArgs e)
        {
            panelMain.Controls.Clear();
            var dashboard = new InventoryReportControl();
            dashboard.Dock = DockStyle.Fill;

            panelMain.Controls.Add(dashboard);
        }
    }
}
