using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROJECT_TRANSFER_FILE
{
    public partial class SignUp : Form
    {
        private string publicPrivateKeyXML;
        private string publicOnlyKeyXML;
        private static readonly HttpClient client = new HttpClient();
        public SignUp()
        {
            InitializeComponent();
        }
        private async Task PostReg(string username,string name, string pass, string password_confirmation)
        {
            AssignNewKey(username);
            var values = new Dictionary<string, string>
             {
          {
            "username",            username
          },
           {
            "name",            name
          },
          {
            "password",            pass
          },
          {
            "password_confirmation", password_confirmation
          },
          {
            "public_key", publicOnlyKeyXML
          }
        };
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://filetrans.f4koin.cyou/api/register", content);
            // json
            var responseString = await response.Content.ReadAsStringAsync();

            // splice token from responseString
            var objects = JObject.Parse(responseString); // parse as array
            string message = (string)objects["message"]; // get token
            if ((string)objects["message"] != "User created")
            {
                MessageBox.Show(message);
            }
            // save token
            else if ((string)objects["token"] != null)
            {
                // save to file
                File.WriteAllText(username + ".xml", publicPrivateKeyXML);
                MessageBox.Show(message);
                string save = (string)objects["token"];
                save = save.Substring(2);
                Properties.Settings.Default.bearer_token = save;
                Properties.Settings.Default.Save();
                this.Hide();
                Controller frm = new Controller(publicOnlyKeyXML, publicPrivateKeyXML, username);
                frm.ShowDialog();
                this.Show();
            }
        }


        public void AssignNewKey(string username)
        {
            using (var rsa = new RSACryptoServiceProvider(1024))
            {
                try
                {
                    // To export the key information to an RSAParameters object
                    // first, pass false to export the public key information
                    // or pass true to export public and private key information.
                    RSAParameters Key = rsa.ExportParameters(false);
                    // To export the key information to an XML string, set
                    // includePrivateParameters to false.
                     publicPrivateKeyXML = rsa.ToXmlString(true);
                     publicOnlyKeyXML = rsa.ToXmlString(false);
                    MessageBox.Show(publicPrivateKeyXML);
                    MessageBox.Show(publicOnlyKeyXML);                    
                }
                finally
                {
                    // Clear the RSA key.
                    rsa.PersistKeyInCsp = false;

                }
            }
        }
            private void SignUp_Load(object sender, EventArgs e)
        {

        }

        private void btnCofirm_Click(object sender, EventArgs e)
        {
            _ = PostReg(txtUserName.Text, txtFullName.Text, txtPassword.Text, txtPasswordConf.Text);
        }
    }
}
