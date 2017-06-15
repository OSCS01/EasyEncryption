using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyEncryption
{
    public partial class Home : Form
    {
        public Home()
        {
            InitializeComponent();
            listView1.Columns.Add("File name",-2);
            listView1.Columns.Add("Size", -2);
            listView1.View = View.Details;
        }

        private void addItems(System.IO.FileInfo fi)
        {
            string[] row = { fi.Name, "" + fi.Length};
            ListViewItem lvi = new ListViewItem(row);
            listView1.Items.Add(lvi);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {

                System.IO.FileInfo file = new System.IO.FileInfo(FD.FileName);
                addItems(file);
                
            }
        }
    }
}
