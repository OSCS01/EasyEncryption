using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace EasyEncryption
{
    public partial class Login : Form
    {
        const string constring = @"Data Source=CEPHAS\SQLEXPRESS;Initial Catalog = EasyEncryption;Integrated Security = True";
        public Login()
        {
            InitializeComponent();
        }

        private void loginvalidate_Click(object sender, EventArgs e)
        {
            SqlDataAdapter sda = new SqlDataAdapter("Select Count(*) From Users where username ='" + LoginField.Text + "' and pass = '" + PassField.Text + "'", constring);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows[0][0].ToString() == "1")
            {
                this.Hide();
                Home hm = new Home();
                hm.Show();
            }
            else
            {
                MessageBox.Show("Please check your username and password");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void LoginField_TextChanged(object sender, EventArgs e)
        {

        }

        private void PassField_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
