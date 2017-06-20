using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.IO;
using System.Net.Sockets;
using System.Xml;

namespace EasyEncryption
{
    public partial class Home : Form
    {
        const string encryptpath = @"C:\Users\Daryl\Desktop\EncryptedTest\";
        const string username = "Adam";
        public Home()
        {
            InitializeComponent();
            tabPage1.Controls.Add(selectedFiles);
            tabPage2.Controls.Add(myFiles);
            getMyFiles("username");
        }

        private void getMyFiles(string username)
        {
            //Retrieve database info from server regarding my files
        }

        private void addItems(FileInfo fi)
        {
            string[] row = { fi.Name, "" + fi.Length, fi.FullName };
            ListViewItem lvi = new ListViewItem(row);
            selectedFiles.Items.Add(lvi);
        }

        private void AddFiles_Click(object sender, EventArgs e)
        {
            var FD = new OpenFileDialog();
            if (FD.ShowDialog() == DialogResult.OK)
            {
                System.IO.FileInfo file = new System.IO.FileInfo(FD.FileName);
                addItems(file);
            }
        }

        private void transferbtn_Click(object sender, EventArgs e)
        {
            byte[] key = new byte[32];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    AES.Key = key;
                    AES.Mode = CipherMode.CBC;
                    foreach (ListViewItem item in selectedFiles.Items)
                    {
                        string filepath = item.SubItems[2].Text;
                        string filename = item.SubItems[0].Text;
                        FileStream fsInput = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                        FileStream fsEncrypted = new FileStream(encryptpath + filename, FileMode.Create, FileAccess.Write);
                        ICryptoTransform encryptor = AES.CreateEncryptor();
                        CryptoStream cryptostream = new CryptoStream(fsEncrypted, encryptor, CryptoStreamMode.Write);
                        int bytesread;
                        byte[] buffer = new byte[16384];
                        while (true)
                        {
                            bytesread = fsInput.Read(buffer, 0, 16384);
                            if (bytesread == 0)
                                break;
                            cryptostream.Write(buffer, 0, bytesread);
                        }
                    }
                }
            }
        }

        private void groupsbtn_Click(object sender, EventArgs e)
        {
            string ipadd = "fe80::84a1:3136:baa0:6c41%14";
            TcpClient client = new TcpClient(ipadd, 8080);
            NetworkStream stream = client.GetStream();
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine("Retrieve");
            sw.WriteLine(username);
            sw.Flush();

            StreamReader sr = new StreamReader(stream);
            XmlReader xr = XmlReader.Create(sr);
            string result = sr.ReadToEnd();
            DataTable dt = new DataTable();
            dt.ReadXml(xr);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                ListViewItem listitem = new ListViewItem(dr["Filename"].ToString());
                listitem.SubItems.Add(dr["Size"].ToString());
                listitem.SubItems.Add(dr["SharedGroups"].ToString());
                listitem.SubItems.Add(dr["Owner"].ToString());
                myFiles.Items.Add(listitem);
            }



            /*
            TcpListener listener = new TcpListener(IPAddress.IPv6Any, 8081);
            listener.Start();
            while(true)
            {
                client = listener.AcceptTcpClient();
                stream = client.GetStream();
                StreamReader reader = new StreamReader(stream);
                string username = reader.ReadLine();
                myFilename.Text = username;
            }
            */
        }
    }
}


