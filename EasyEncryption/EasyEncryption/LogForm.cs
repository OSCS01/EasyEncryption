using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EasyEncryption
{
    public partial class LogForm : Form
    {

        const string ipadd = "fe80::a490:812e:e8c9:b261%9";
        const string user = "Adam";
        EasyEncWS.MainService ms = new EasyEncWS.MainService();

        public LogForm(ListViewItem lvi)
        {
            InitializeComponent();
            retrieveLogs(lvi);
        }

        private void retrieveLogs(ListViewItem lvi)
        {
            string xml = ms.getLogs(lvi.SubItems[0].Text, lvi.SubItems[3].Text, lvi.SubItems[2].Text);
            DataTable dt = new DataTable();
            StringReader sr = new StringReader(xml);
            dt.ReadXml(sr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["OriginalFilename"].ToString());
                listitem.SubItems.Add(dr["Owner"].ToString());
                listitem.SubItems.Add(dr["UserDownload"].ToString());
                listitem.SubItems.Add(dr["Date"].ToString());
                listitem.SubItems.Add(dr["sharedGroup"].ToString());
                LogView.Items.Add(listitem);
            }
        }
    }
}

