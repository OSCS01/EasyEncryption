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

        public LogForm(ListViewItem lvi)
        {
            InitializeComponent();
            retrieveLogs(lvi);
        }

        private void retrieveLogs(ListViewItem lvi)
        {
            TcpClient client = new TcpClient(ipadd, 8080);
            NetworkStream stream = client.GetStream();
            StreamWriter sw = new StreamWriter(stream);
            sw.WriteLine("Logs");
            sw.WriteLine(lvi.SubItems[0].Text);
            sw.WriteLine(lvi.SubItems[2].Text);
            sw.WriteLine(lvi.SubItems[3].Text);
            sw.WriteLine(user);
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
                    }
                }
            }
        }
    }
}
