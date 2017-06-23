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
        const string decryptpath = @"C:\Users\Daryl\Desktop\DecryptedTest\";
        const string encryptpath = @"C:\Users\Daryl\Desktop\EncryptedTest\";
        const string username = "Adam";
        const string base64key = "E1N+Jz9sd5DbpXFMCe1eE+U1ZKrhtf7mm9PCeUV+ps4=";
        const string base64IV = "4wW7sdztKzhBtuJF34A+qw==";
        public Home()
        {
            InitializeComponent();
            tabPage1.Controls.Add(selectedFiles);
            tabPage2.Controls.Add(myFiles);
            getMyFiles(username);
        }

        private void getMyFiles(string username)
        {
            try
            {
                //Retrieve database info from server regarding my files
                string ipadd = "fe80::84a1:3136:baa0:6c41%14";
                TcpClient client = new TcpClient(ipadd, 8080);
                NetworkStream stream = client.GetStream();
                StreamWriter sw = new StreamWriter(stream);
                sw.WriteLine("Retrieve");
                sw.WriteLine(username);
                sw.Flush();

                myFiles.Items.Clear();

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
                xr.Close();
                sr.Close();
                sw.Close();
            }
            catch (SocketException e)
            {
                myFilename.Text = "Error connecting to server";
            }
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

        private void uploadbtn_Click(object sender, EventArgs e)
        {
            string ipadd = "fe80::84a1:3136:baa0:6c41%14";
            byte[] key = new byte[32];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    using (SHA1Managed sha1 = new SHA1Managed())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;
                        AES.Mode = CipherMode.CBC;
                        string pubkey = getPubKey();
                        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                        {
                            rsa.FromXmlString(pubkey);
                            foreach (ListViewItem item in selectedFiles.Items)
                            {
                                using (TcpClient client = new TcpClient(ipadd, 8080))
                                {
                                    using (NetworkStream stream = client.GetStream())
                                    {
                                        using (StreamWriter sw = new StreamWriter(stream))
                                        {
                                            rng.GetBytes(key);
                                            AES.Key = key;
                                            AES.GenerateIV();
                                            string filepath = item.SubItems[2].Text;
                                            FileInfo fi = new FileInfo(filepath);
                                            string fileext = fi.Extension;
                                            string filename = fi.Name.Substring(0, fi.Name.Length - fileext.Length);
                                            string hashedfilename = Convert.ToBase64String(sha1.ComputeHash(Encoding.ASCII.GetBytes(filename)));

                                            FileStream fsInput = new FileStream(filepath, FileMode.Open, FileAccess.Read);
                                            FileStream fsEncrypted = new FileStream(encryptpath + hashedfilename + ".ee", FileMode.Create, FileAccess.Write);
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
                                            string encryptedkey = Convert.ToBase64String(rsa.Encrypt(key, false));
                                            cryptostream.Close();
                                            sw.WriteLine("Upload");
                                            sw.WriteLine(hashedfilename);
                                            sw.WriteLine(fi.Length);
                                            sw.WriteLine(username);
                                            sw.WriteLine("Personal");
                                            sw.WriteLine(filename);
                                            sw.WriteLine(fileext);
                                            sw.WriteLine(encryptedkey);
                                            sw.WriteLine(Convert.ToBase64String(AES.IV));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void groupsbtn_Click(object sender, EventArgs e)
        {

        }

        private void Refreshbtn_Click(object sender, EventArgs e)
        {
            getMyFiles(username);
        }

        private string getPubKey()
        {
            string ipadd = "fe80::84a1:3136:baa0:6c41%14";
            using (TcpClient client = new TcpClient(ipadd, 8080))
            {
                using (NetworkStream stream = client.GetStream())
                {
                    using (StreamWriter sw = new StreamWriter(stream))
                    {
                        sw.WriteLine("Pubkey");
                        sw.Flush();
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabControl1.TabPages["tabPage2"])
            {
                Refreshbtn.Visible = true;
                DownloadBtn.Visible = true;
                UploadBtn.Visible = false;
                clearBtn.Visible = false;
            }
            else
            {
                Refreshbtn.Visible = false;
                DownloadBtn.Visible = false;
                UploadBtn.Visible = true;
                clearBtn.Visible = true;
            }
        }

        private void DownloadBtn_Click(object sender, EventArgs e)
        {
            /*
            string ipadd = "fe80::84a1:3136:baa0:6c41%14";
            TcpClient client = new TcpClient(ipadd, 8080);
            NetworkStream stream = client.GetStream();
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine("Download");
            sw.WriteLine(username);
            */

            //Download files from server 

            using (RijndaelManaged AES = new RijndaelManaged())
            {
                AES.BlockSize = 128;
                AES.KeySize = 256;
                AES.Mode = CipherMode.CBC;
                using (SHA1Managed sha1 = new SHA1Managed())
                {
                    byte[] key = new byte[32];
                    foreach (ListViewItem item in selectedFiles.Items)
                    {
                        string filepath = item.SubItems[2].Text;
                        FileInfo fi = new FileInfo(filepath);
                        string fileext = fi.Extension;
                        string filename = fi.Name.Substring(0, fi.Name.Length - fileext.Length);
                        string hashedfilename = Convert.ToBase64String(sha1.ComputeHash(Encoding.ASCII.GetBytes(filename)));
                        key = Convert.FromBase64String(base64key);
                        AES.Key = key;
                        AES.IV = Convert.FromBase64String(base64IV);
                        FileStream fsEncrypted = new FileStream(encryptpath + hashedfilename + ".ee", FileMode.Open, FileAccess.Read);
                        FileStream fsDecrypted = new FileStream(decryptpath + fi.Name, FileMode.Create, FileAccess.Write);
                        ICryptoTransform decryptor = AES.CreateDecryptor();
                        CryptoStream cryptostream = new CryptoStream(fsDecrypted, decryptor, CryptoStreamMode.Write);
                        int bytesread;
                        byte[] buffer = new byte[16384];
                        while (true)
                        {
                            bytesread = fsEncrypted.Read(buffer, 0, 16384);
                            if (bytesread == 0)
                                break;
                            cryptostream.Write(buffer, 0, bytesread);
                        }
                        cryptostream.Close();
                    }
                }
            }


            //Access Logs


        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            selectedFiles.Items.Clear();
        }
    }
}


