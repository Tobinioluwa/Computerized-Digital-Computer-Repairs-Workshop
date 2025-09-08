using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.IO;


namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    

    public partial class salespage : Form
    {
        private int salesId;
        private string salesName;
        private int loggedInsalesId;

        public salespage(int id, string name)
        {
            salesId = id;
            salesName = name;
            loggedInsalesId = salesId;
            InitializeComponent();

            label3.Text = salesName;
            
        }

        private void Salespage_Load(object sender, EventArgs e)
        {

            panel1.Controls.Clear();
            var dashboard = new SellItemControl(loggedInsalesId);
            dashboard.Dock = DockStyle.Fill;

            panel1.Controls.Add(dashboard);

        }
        
        private void Label3_Click(object sender, EventArgs e)
        {

        }

        private void Panel3_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void Button1_Click_1(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            var dashboard = new SellItemControl(loggedInsalesId);
            dashboard.Dock = DockStyle.Fill;

            panel1.Controls.Add(dashboard);
        }

        private void Button2_Click(object sender, EventArgs e)
        {


            panel1.Controls.Clear();
            var dashboard = new CustomerManagementControl();
            dashboard.Dock = DockStyle.Fill;

            panel1.Controls.Add(dashboard);
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            var dashboard = new BookRepairs();
            dashboard.Dock = DockStyle.Fill;

            panel1.Controls.Add(dashboard);
        }

        private void Button11_Click(object sender, EventArgs e)
        {
            this.Hide();

            Form1 loginpage = new Form1();
            loginpage.Show();
        }

        private void Button4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}


