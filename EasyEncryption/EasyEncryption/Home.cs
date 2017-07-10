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
        string encryptpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EncryptedTest\\";
        const string username = "Adam";
        const string ipadd = "fe80::a490:812e:e8c9:b261%9";


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
                myFiles.Items.Clear();
                //Retrieve database info from server regarding my files
                using (TcpClient client = new TcpClient(ipadd, 8080))
                {
                    using (NetworkStream stream = client.GetStream())
                    {
                        using (StreamWriter sw = new StreamWriter(stream))
                        {
                            sw.WriteLine("Retrieve");
                            sw.WriteLine(username);
                            sw.Flush();
                            using (StreamReader sr = new StreamReader(stream))
                            {
                                CspParameters csp = new CspParameters();
                                csp.KeyContainerName = "MyEEKeys";
                                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
                                {
                                    string base64key = sr.ReadLine();
                                    string base64IV = sr.ReadLine();
                                    string base64xml = sr.ReadToEnd();
                                    byte[] xmlarray = Convert.FromBase64String(base64xml);
                                    byte[] deckey = rsa.Decrypt(Convert.FromBase64String(base64key), false);

                                    using (RijndaelManaged aes = new RijndaelManaged())
                                    {
                                        aes.KeySize = 256;
                                        aes.Key = deckey;
                                        aes.IV = Convert.FromBase64String(base64IV);
                                        aes.Mode = CipherMode.CBC;
                                        using (var mems = new MemoryStream(xmlarray))
                                        {
                                            using (var decryptor = aes.CreateDecryptor())
                                            {
                                                using (var cryptostream = new CryptoStream(mems, decryptor, CryptoStreamMode.Read))
                                                {
                                                    byte[] decxml = new byte[xmlarray.Length];
                                                    cryptostream.Read(decxml, 0, decxml.Length);
                                                    string decresult = Encoding.UTF8.GetString(decxml);
                                                    StringReader xr = new StringReader(decresult);
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
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
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
            byte[] key = new byte[32];
            byte[] salt = new byte[8];
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(key);
                rng.GetBytes(salt);

                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    string pubkey = getPubKey();
                    rsa.FromXmlString(pubkey);
                    foreach (ListViewItem item in selectedFiles.Items)
                    {
                        using (TcpClient client = new TcpClient(ipadd, 8080))
                        {
                            using (NetworkStream stream = client.GetStream())
                            {
                                string base64salt = Convert.ToBase64String(salt);
                                string filepath = item.SubItems[2].Text;
                                FileInfo fi = new FileInfo(filepath);
                                string fileext = fi.Extension;
                                string filename = fi.Name.Substring(0, fi.Name.Length - fileext.Length);
                                string saltedfilename = String.Concat(filename, base64salt);
                                using (SHA1Managed sha1 = new SHA1Managed())
                                {
                                    string hashedfilename = Convert.ToBase64String(sha1.ComputeHash(Encoding.ASCII.GetBytes(saltedfilename)));

                                    if (hashedfilename.Contains("/"))
                                        hashedfilename = hashedfilename.Replace("/", string.Empty);

                                    using (RijndaelManaged AES = new RijndaelManaged())
                                    {
                                        AES.KeySize = 256;
                                        AES.BlockSize = 128;
                                        AES.Mode = CipherMode.CBC;
                                        AES.Key = key;
                                        AES.GenerateIV();
                                        using (FileStream fsInput = new FileStream(filepath, FileMode.Open, FileAccess.Read))
                                        {
                                            using (FileStream fsEncrypted = new FileStream(encryptpath + hashedfilename + ".ee", FileMode.Create, FileAccess.Write))
                                            {
                                                ICryptoTransform encryptor = AES.CreateEncryptor();
                                                using (CryptoStream cryptostream = new CryptoStream(fsEncrypted, encryptor, CryptoStreamMode.Write))
                                                {
                                                    int bytesread;
                                                    byte[] buffer = new byte[16384];
                                                    while (true)
                                                    {
                                                        bytesread = fsInput.Read(buffer, 0, 16384);
                                                        if (bytesread == 0)
                                                            break;
                                                        cryptostream.Write(buffer, 0, bytesread);
                                                    }

                                                    string encryptedkey = Convert.ToBase64String(rsa.Encrypt(AES.Key, false));
                                                    using (StreamWriter sw = new StreamWriter(stream))
                                                    {
                                                        string intent = "Upload";
                                                        string sharingwith = "MSEC";
                                                        sw.WriteLine(intent);
                                                        sw.WriteLine(Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(hashedfilename), false)));
                                                        sw.WriteLine(fi.Length);
                                                        sw.WriteLine(Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(username), false)));
                                                        sw.WriteLine(Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(sharingwith), false)));
                                                        sw.WriteLine(Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(filename), false)));
                                                        sw.WriteLine(Convert.ToBase64String(rsa.Encrypt(Encoding.UTF8.GetBytes(fileext), false)));
                                                        sw.WriteLine(encryptedkey);
                                                        sw.WriteLine(Convert.ToBase64String(AES.IV));
                                                    }
                                                }
                                            } //Start sending file here
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            selectedFiles.Items.Clear();
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
                viewLogsBtn.Visible = true;
            }
            else
            {
                Refreshbtn.Visible = false;
                DownloadBtn.Visible = false;
                UploadBtn.Visible = true;
                clearBtn.Visible = true;
                viewLogsBtn.Visible = false;
            }
        }

        private void DownloadBtn_Click(object sender, EventArgs e)
        {
            using (var savedpath = new FolderBrowserDialog())
            {
                if (savedpath.ShowDialog() == DialogResult.OK)
                {
                    string decryptpath = savedpath.SelectedPath + "\\";
                    List<string> files = new List<string>();
                    List<string> owners = new List<string>();
                    List<string> sharedGroups = new List<string>();
                    foreach (ListViewItem item in myFiles.Items)
                    {
                        if (item.Checked)
                        {
                            files.Add(item.SubItems[0].Text);
                            owners.Add(item.SubItems[3].Text);
                            sharedGroups.Add(item.SubItems[2].Text);
                        }

                    }

                    TcpClient client = new TcpClient(ipadd, 8080);
                    NetworkStream stream = client.GetStream();
                    StreamWriter sw = new StreamWriter(stream);
                    sw.WriteLine("Download");
                    sw.WriteLine(username);
                    sw.WriteLine(files.Count);
                    for (int i = 0; i < files.Count; i++)
                    {
                        sw.WriteLine(files[i]);
                        sw.WriteLine(owners[i]);
                        sw.WriteLine(sharedGroups[i]);
                    }
                    sw.Flush();

                    CspParameters csp = new CspParameters();
                    csp.KeyContainerName = "MyEEKeys";

                    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(csp))
                    {
                        StreamReader sr = new StreamReader(stream);
                        List<FileItem> fil = new List<FileItem>();
                        FileItem fi = new FileItem();
                        for (int i = 0; i < files.Count; i++)
                        {
                            fi.hashedFilename = Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(sr.ReadLine()), false));
                            fi.IV = sr.ReadLine();
                            fi.Originalfilename = Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(sr.ReadLine()), false));
                            fi.OriginalfileExt = Encoding.UTF8.GetString(rsa.Decrypt(Convert.FromBase64String(sr.ReadLine()), false));
                            fi.EncKey = sr.ReadLine();
                            fil.Add(fi);
                        }


                        using (RijndaelManaged AES = new RijndaelManaged())
                        {
                            AES.BlockSize = 128;
                            AES.KeySize = 256;
                            AES.Mode = CipherMode.CBC;

                            foreach (FileItem fi1 in fil)
                            {
                                string encfilepath = encryptpath + fi1.hashedFilename + ".ee";
                                string decfilepath = decryptpath + fi1.Originalfilename + fi1.OriginalfileExt;
                                byte[] deckey = rsa.Decrypt(Convert.FromBase64String(fi1.EncKey), false);

                                AES.Key = deckey;
                                AES.IV = Convert.FromBase64String(fi1.IV);

                                using (FileStream fsEncrypted = new FileStream(encfilepath, FileMode.Open, FileAccess.Read))
                                {
                                    using (FileStream fsDecrypted = new FileStream(decfilepath, FileMode.Create, FileAccess.Write))
                                    {
                                        ICryptoTransform decryptor = AES.CreateDecryptor();
                                        using (CryptoStream cryptostream = new CryptoStream(fsDecrypted, decryptor, CryptoStreamMode.Write))
                                        {
                                            int bytesread;
                                            byte[] buffer = new byte[16384];
                                            while (true)
                                            {
                                                bytesread = fsEncrypted.Read(buffer, 0, 16384);
                                                if (bytesread == 0)
                                                    break;
                                                cryptostream.Write(buffer, 0, bytesread);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void clearBtn_Click(object sender, EventArgs e)
        {
            selectedFiles.Items.Clear();
        }

        private void deleteBtn_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in myFiles.Items)
            {
                if (item.Checked)
                {
                    try
                    {
                        using (TcpClient client = new TcpClient(ipadd, 8080))
                        {
                            using (NetworkStream stream = client.GetStream())
                            {
                                using (StreamWriter sw = new StreamWriter(stream))
                                {
                                    sw.WriteLine(item.SubItems[0].Text);
                                    sw.WriteLine(item.SubItems[2].Text);
                                    sw.WriteLine(item.SubItems[3].Text);
                                }
                            }
                        }
                        myFiles.Items.Remove(item);
                    }
                    catch (Exception ex)
                    {
                        string message = "An error has occured!";
                        string caption = "Error";
                        MessageBoxButtons button = MessageBoxButtons.OK;
                        DialogResult result;

                        result = MessageBox.Show(message, caption, button, MessageBoxIcon.Error);

                        if (result == DialogResult.OK)
                        {
                            //Do nothing.
                        }
                    }
                }
            }
        }

        private void contactsbtn_Click(object sender, EventArgs e)
        {
            this.Hide();
            Contacts cs = new Contacts();
            cs.Show();
        }

        private void viewLogsBtn_Click(object sender, EventArgs e)
        {
            ListViewItem selecteditem = myFiles.SelectedItems[0];
            LogForm logform = new LogForm(selecteditem);
            logform.Show();
        }
    }
}


