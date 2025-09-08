using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace All_in_One_CSM_Software_with_Point_of_Sales___Latest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
