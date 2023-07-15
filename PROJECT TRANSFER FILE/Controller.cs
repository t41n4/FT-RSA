using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.VisualBasic;
using Newtonsoft.Json.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PROJECT_TRANSFER_FILE
{
    public partial class Controller : Form
    {
        private string publicPrivateKeyXML = "";

        private string publicOnlyKeyXML = "";

        private delegate void SafeCallDelegate();

        byte[] bData;

        byte[] eData;

        byte[] dData;

        string aesKey = "";

        string aesIV = "";

        string publicKey = "";

        string privateKey = "";

        string publicAESKey = "";

        string publicIV = "";

        string publicPartnerKey = "";

        string bearer_token = "";

        bool auto = false;

      


        private JArray GetFile(string url)
        {
                     HttpClient client = new HttpClient();
       
            var values = new Dictionary<string,
              string>
            {
            };
            var content = new FormUrlEncodedContent(values);
            // header
            bearer_token = Properties.Settings.Default.bearer_token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearer_token);
            var response =  client.GetAsync(url);
            var responseString = response.Result.Content.ReadAsStringAsync();
            // parse json
            JArray jsonArray = JArray.Parse(responseString.Result);
            //JObject json = JObject.Parse(jsonArray[0].ToString());

            return jsonArray;             
        }
        private JArray GetUserstring (string url)
        {
            HttpClient client = new HttpClient();
            bearer_token = Properties.Settings.Default.bearer_token;
            // header
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearer_token);
            var response = client.GetAsync(url);
            var responseString = response.Result.Content.ReadAsStringAsync();
            // parse json
            JArray jsonArray = JArray.Parse(responseString.Result);
            //JObject json = JObject.Parse(jsonArray[0].ToString());

            return jsonArray;
        }

        private void LoadListFile(){
            JArray json = GetFile("http://filetrans.f4koin.cyou/api/getfile");
            // binding data to listview
            if (lwFile.InvokeRequired)
            {
                var d = new SafeCallDelegate(LoadListFile);
                lwFile.Invoke(d, new object[] { });
            }
            else
            {
                lwFile.Items.Clear();
                foreach (JObject item in json)
                {
                    ListViewItem lvi = new ListViewItem(item["file_name"].ToString());
                    lvi.SubItems.Add(item["file_link"].ToString());
                    lvi.SubItems.Add(item["file_signature"].ToString());
                    lvi.SubItems.Add(item["From"].ToString());
                    lvi.SubItems.Add(item["time_upload"].ToString());
                    lvi.SubItems.Add(item["aes_key"].ToString());
                    lvi.SubItems.Add(item["aes_iv"].ToString());
                    lwFile.Items.Add(lvi);
                }
                tbUdtime.Text = DateTime.Now.ToString();
            }   
        }
        private void LoadListUser()
        {
            JArray json = GetFile("http://filetrans.f4koin.cyou/api/getuser");
            // binding data to listview
            if (lw_friend.InvokeRequired)
            {
                var d = new SafeCallDelegate(LoadListUser);
                lw_friend.Invoke(d, new object[] { });
            }
            else
            {
                lw_friend.Items.Clear();
                foreach (var item in json)
                {
                    ListViewItem lvi = new ListViewItem(item["username"].ToString());
                    lvi.SubItems.Add(item["name"].ToString());
                    lvi.SubItems.Add(item["public_key"].ToString());
                    lvi.SubItems.Add(item["status"].ToString());
                    lw_friend.Items.Add(lvi);
                }
                tbUdtime.Text = DateTime.Now.ToString();
            }

        }

        public Controller()
        {
            InitializeComponent();

            // add column
            lwFile.View = View.Details;
            lwFile.Columns.Add("file_name", 100);
            lwFile.Columns.Add("file_link", 100);
            lwFile.Columns.Add("file_signature", 100);     
            lwFile.Columns.Add("From", 100);
            lwFile.Columns.Add("At", 100);
            lwFile.Columns.Add("aes_key", 50);
            lwFile.Columns.Add("aes_iv", 50);

            lw_friend.View = View.Details;
            lw_friend.Columns.Add("username", 100);
            lw_friend.Columns.Add("name", 100);
            lw_friend.Columns.Add("public_key", 100);
            lw_friend.Columns.Add("status", 100);

            // enable select lwUrl subitem
            lwFile.FullRowSelect = true;
            lwFile.GridLines = true;
            lwFile.MultiSelect = false;
            lwFile.HideSelection = false;

            // enable select lwUrl subitem
            lw_friend.FullRowSelect = true;
            lw_friend.GridLines = true;
            lw_friend.MultiSelect = false;
            lw_friend.HideSelection = false;

            // add column lwPartnerKeyStorage
            LoadListUser();
            LoadListFile();
            tbToken.Text = bearer_token;            
        }
        public Controller(string plk, string prk,string username)
        {
            InitializeComponent();

            // add column
            lwFile.View = View.Details;
            lwFile.Columns.Add("file_name", 100);
            lwFile.Columns.Add("file_link", 100);
            lwFile.Columns.Add("file_signature", 100);
            lwFile.Columns.Add("From", 100);
            lwFile.Columns.Add("At", 100);
            lwFile.Columns.Add("aes_key", 50);
            lwFile.Columns.Add("aes_iv", 50);

            lw_friend.View = View.Details;
            lw_friend.Columns.Add("username", 100);
            lw_friend.Columns.Add("name", 100);
            lw_friend.Columns.Add("public_key", 100);
            lw_friend.Columns.Add("status", 100);

            // enable select lwUrl subitem
            lwFile.FullRowSelect = true;
            lwFile.GridLines = true;
            lwFile.MultiSelect = false;
            lwFile.HideSelection = false;

            // enable select lwUrl subitem
            lw_friend.FullRowSelect = true;
            lw_friend.GridLines = true;
            lw_friend.MultiSelect = false;
            lw_friend.HideSelection = false;

            // add column lwPartnerKeyStorage
            LoadListUser();
            LoadListFile();
            tbToken.Text = bearer_token;
            tbUsername.Text = username;
            publicPrivateKeyXML = plk;
            publicOnlyKeyXML = prk;
        }

        private byte[] encryptFile(string aesKey, string aesIV, byte[] data)
        {
            
            using (var aes = new AesCryptoServiceProvider())
            {
                try
                {
                    // import iv and key
                    aes.Key = Convert.FromBase64String(aesKey);
                    aes.IV = Convert.FromBase64String(aesIV);
                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                    using (MemoryStream msEncrypt = new MemoryStream())
                    {
                        using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        {
                            csEncrypt.Write(data, 0, data.Length);
                            csEncrypt.FlushFinalBlock();
                            return msEncrypt.ToArray();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    aes.Clear();
                }
            }
            return null;
        }

        private void btn_filebrowser_Click(object sender, EventArgs e)
        {
            open_file_dialog();
        }
        private void open_file_dialog() {
            // open file
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "All files (*.*)|*.*";
            openFile.FilterIndex = 1;
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                // show file data
                string filePath = openFile.FileName;
                lbFilePath.Text = filePath;
                bData = File.ReadAllBytes(filePath);
            }
        }

        private string showFirstNbytes(byte[] bData, int n)
        {
            string firstN = "";
            int len = bData.Length;
            if (len > n)
            {
                for (int i = 0; i < n; i++)
                {
                    firstN += bData[i].ToString() + " ";
                }
            }
            else
            {
                for (int i = 0; i < len; i++)
                {
                    firstN += bData[i].ToString() + " ";
                }
            }

            return firstN;
        }

        // Async Task auto update every 3s

        private async void autoUpdate()
        {
            while (auto)
            {
                await Task.Delay(3000);
                LoadListFile();
                LoadListUser();
            }
        }


        private void btnRS_Click(object sender, EventArgs e)
        {
            LoadListUser();
            LoadListFile();
        }

        private byte[] decryptFile(string aes_key, string aes_iv, byte[] dData)
        {
            using (var aes = new AesCryptoServiceProvider())
            {
                try
                {
                    // import iv and key
                    aes.Key = Convert.FromBase64String(aes_key);
                    aes.IV = Convert.FromBase64String(aes_iv);

                    ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                    using (MemoryStream msDecrypt = new MemoryStream(dData))
                    {
                        using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                csDecrypt.CopyTo(ms);
                                return ms.ToArray();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    aes.Clear();
                }
            }
            return null;           
        }



        private async Task  GetTask()
        {
                   HttpClient client = new HttpClient();
        var values = new Dictionary<string, string>
                {
                    { "thing1", "hello" },
                    { "thing2", "world" }
                };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://filetrans.f4koin.cyou/api/getfile", content);
            var responseString = await response.Content.ReadAsStringAsync();

            MessageBox.Show(responseString);
        }

        private async Task PostUploadFile(string file_signature, string file_name, string file_link, string user_to, string aes_key,string aes_iv)
        {
            HttpClient client = new HttpClient();
            var values = new Dictionary<string, string>
                {
                    { "file_signature", file_signature },
                    { "file_name", file_name },
                    { "file_link", file_link },
                    { "user_to", user_to },
                    {"aes_key", aes_key},
                    {"aes_iv", aes_iv},

                };                    
            var content = new FormUrlEncodedContent(values);
            bearer_token = Properties.Settings.Default.bearer_token;
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearer_token);
            var response = await client.PostAsync("http://filetrans.f4koin.cyou/api/uploadfile", content);
            var responseString = await response.Content.ReadAsStringAsync();

            MessageBox.Show(responseString);
        }
        private void btn_upload_Click(object sender, EventArgs e)
        {
            try
            {
                string partner_puk = lw_friend.SelectedItems[0].SubItems[2].Text;
                bData = File.ReadAllBytes(lbFilePath.Text);
                // hash file
                SHA256Managed sha = new SHA256Managed();
                byte[] hash = sha.ComputeHash(bData);
                string file_signature = Convert.ToBase64String(hash);  
                // enrypt hash with RSA
                 using (var rsa = new RSACryptoServiceProvider())
                 {
                        try
                        {
                            rsa.FromXmlString (partner_puk);
                            byte[] hashBytes = rsa.Encrypt(hash, true);
                            file_signature = Convert.ToBase64String(hashBytes);
                            MessageBox.Show(file_signature);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        finally
                        {
                            rsa.PersistKeyInCsp = false;
                        }
                 }
                string file_name = Path.GetFileName(lbFilePath.Text);
                string user_to = lw_friend.SelectedItems[0].Text;
                string file_link = uploadFile(lbFilePath.Text);
                if (file_link != "")
                {
                    _ = PostUploadFile(file_signature, file_name, file_link, user_to, aesKey, aesIV);
                }    


            }
            catch (Exception ex)
            {
                if (ex.Message == "InvalidArgument=Value of '0' is not valid for 'index'.\r\nParameter name: index")
                {
                    MessageBox.Show("Please select a friend to send file");
                }
                else
                {
                     open_file_dialog();
                }
               
            }        
        }



        private string uploadFile(string path)
        {
            // generate aes key
            using (var aes = new AesCryptoServiceProvider())
            {
                aes.GenerateKey();
                aes.GenerateIV();
                aesKey = Convert.ToBase64String(aes.Key);
                aesIV = Convert.ToBase64String(aes.IV);
            }
            // read file
            byte[] bData = File.ReadAllBytes(path);
            eData = encryptFile(aesKey, aesIV,bData);      
            // add _encrypted to new filename
            string newFilePath = path.Insert(path.LastIndexOf('.'), "_encrypted");
            File.WriteAllBytes(newFilePath, eData);
            // using curl to upload file to transfer.sh
            string fileName = Path.GetFileName(newFilePath);    
            if (fileName.Contains(" "))
            {
                MessageBox.Show("File name must not contain space");
                return "";
            }
            // upload file
            string uploadCmd =
                "curl -# --upload-file "
                + "\"" + newFilePath + "\"" 
                + " https://transfer.sh/"
                + "\"" + fileName + "\""
                + " -o "
                + "upload.txt";

            runCmd(uploadCmd);

            // get the link from upload.txt
            string link = "";
            using (StreamReader sr = new StreamReader("upload.txt"))
            {
                link = sr.ReadLine();
            }
            return link;
        }

        private void runCmd(string cmd)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/C " + cmd;
            p.StartInfo.UseShellExecute = true;
            p.StartInfo.RedirectStandardInput = false;
            p.StartInfo.RedirectStandardOutput = false;
            p.StartInfo.RedirectStandardError = false;
            p.StartInfo.CreateNoWindow = false;
            p.Start();
            p.WaitForExit();
            p.Close();
        }

        private void lwUrl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lwFile.SelectedItems.Count > 0)
            {
                tbLink.Text = lwFile.SelectedItems[0].SubItems[1].Text;
            }
        }
        private string decryptRSA(string encrypted, string privateKeyXML)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    rsa.FromXmlString(privateKeyXML);
                    byte[] encryptedBytes = Convert.FromBase64String(encrypted);
                    byte[] decryptedBytes = rsa.Decrypt(encryptedBytes, true);
                    return Convert.ToBase64String(decryptedBytes);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
            return null;
        }
        private void btn_download_Click(object sender, EventArgs e)
        {
            // get file name and link from lwUrl
            try
            {
                string file_name = lwFile.SelectedItems[0].Text;
                string file_link = lwFile.SelectedItems[0].SubItems[1].Text;
                string file_signature = lwFile.SelectedItems[0].SubItems[2].Text;
                string aes_key = lwFile.SelectedItems[0].SubItems[5].Text;
                string aes_iv = lwFile.SelectedItems[0].SubItems[6].Text;

                // download file
                dData = downloader(file_link, file_name);
                // decrypt file

                bData = decryptFile(aes_key, aes_iv, dData);
                // save file
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.FileName = file_name;
                saveFileDialog1.ShowDialog();
                string path = saveFileDialog1.FileName;
                File.WriteAllBytes(path, bData);
                // hash file
                SHA256Managed sha = new SHA256Managed();
                byte[] hash = sha.ComputeHash(bData);
                string file_signature_decrypted = Convert.ToBase64String(hash);
                // decrypt hash with RSA
           
                file_signature = decryptRSA(file_signature, File.ReadAllText(tbUsername.Text+".xml"));

                // compare hash
                if (file_signature == file_signature_decrypted)
                {
                    MessageBox.Show("File is valid");
                    // open file
                    Process.Start(path);
                }
                else
                {
                    MessageBox.Show("DeadFile");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private byte[] downloader(string link, string fileName)
        {
            runCmd("curl -# " + link + " -o " + fileName);
            // read bytes from file
           return File.ReadAllBytes(fileName);
        }      

       

        private void lw_partnerKey_SelectedIndexChanged(object sender, EventArgs e) { }

        private void tbPUK_TextChanged(object sender, EventArgs e) { }

        private void Controller_FormClosing(object sender, FormClosingEventArgs e)
        {
 
        }

        private void btnAuto_Click(object sender, EventArgs e)
        {
            if (auto)
            {
                auto = false;
                btnAuto.BackColor = Color.Red;
            }
            else if (!auto)
            {
                auto = true;            
                btnAuto.BackColor = Color.Yellow;
                // start auto async task
                Task.Run(() => autoUpdate());
            }
        }

    }
}
