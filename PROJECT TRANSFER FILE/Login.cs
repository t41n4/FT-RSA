using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace PROJECT_TRANSFER_FILE
{
    public partial class Login : Form
    {
        private string publicPrivateKeyXML;

        private string publicOnlyKeyXML;

        private string bearer_token = "";

        private string username = "";

        private bool isvalid = false;

        private static readonly HttpClient client = new HttpClient();

        public Login()
        {
            InitializeComponent();
            bearer_token = Properties.Settings.Default.bearer_token;
            isvalid = TestToken(bearer_token);
            if (isvalid)
            {
                this.Hide();
                Controller frm = new Controller(publicOnlyKeyXML, publicPrivateKeyXML, username);
                frm.ShowDialog();
                this.Show();
            }
        }
        private bool TestToken(string token)
        {
            var values = new Dictionary<string,
              string>
            {

            };
            var content = new FormUrlEncodedContent(values);
            // header
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var response = client.PostAsync("http://filetrans.f4koin.cyou/api/checkToken", content);
            var responseString = response.Result.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseString.Result);
 
            if (json["message"].ToString().Contains("invalid"))
            {
                return false;
            }
            username = json["username"].ToString();
            publicOnlyKeyXML = json["public_key"].ToString();
            try
            {
                publicPrivateKeyXML = File.ReadAllText(json["username"].ToString() + ".xml");
            }
            catch (Exception)
            {

                MessageBox.Show("Private key not found, generating... ");
                updatePublicKey(username);
            }


            return true;

        }

        private async Task TestToken()
        {
            var values = new Dictionary<string,
              string> {
          {
            "thing1",
            "hello"
          },
          {
            "thing2",
            "world"
          }
        };
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://filetrans.f4koin.cyou/api/getfile", content);
            var responseString = await response.Content.ReadAsStringAsync();

            MessageBox.Show(responseString);
        }

        private async Task PostLogin(string username, string pass, string online)
        {
            var values = new Dictionary<string, string> {
                {"username", username },
                {"password", pass },
                {"online", online }         
          
        };
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://filetrans.f4koin.cyou/api/login", content);
            // json
            var responseString = await response.Content.ReadAsStringAsync();

            // splice token from responseString
            var objects = JObject.Parse(responseString); 
            string message = (string)objects["message"]; 
            if (message == "Username or password is empty")
            {

            }
            else if (message == "Invalid username or password")
            {
                MessageBox.Show(message);
            }
            // save token
            else if ((string)objects["token"] != null)
            {
                string save = (string)objects["token"];
                save = save.Substring(2);
                Properties.Settings.Default.bearer_token = save;
                Properties.Settings.Default.Save();
                this.username = username;
                try
                {
                    publicPrivateKeyXML = File.ReadAllText(username + ".xml");
                }
                catch (Exception)
                {
                    MessageBox.Show("Private key not found, generating... ");
                    updatePublicKey(username);
                }

                // get public key
                publicOnlyKeyXML = (string)objects["public_key"];

                MessageBox.Show(message);
                MessageBox.Show(publicOnlyKeyXML);
                MessageBox.Show(publicPrivateKeyXML);

               
                this.Hide();
                Controller frm = new Controller(publicOnlyKeyXML, publicPrivateKeyXML, username);
                frm.ShowDialog();
                this.Show();
            }
        }

        private void updatePublicKey(string username) {
           
            // generate new key
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
                    // save private key to file
                    File.WriteAllText(username + ".xml", publicPrivateKeyXML);
                    // save public key to server
                    PostUpdatePublicKey("http://filetrans.f4koin.cyou/api/updatepublickey", publicOnlyKeyXML);

                }
                finally
                {
                    // Clear the RSA key.
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
        private void PostLogOut(string url)
        {
            HttpClient client = new HttpClient();
            bearer_token = Properties.Settings.Default.bearer_token;
            var values = new Dictionary<string,
              string>
            {
            };
            var content = new FormUrlEncodedContent(values);
            // header
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearer_token);
            var response = client.PostAsync(url, content);
            var responseString = response.Result.Content.ReadAsStringAsync();
            // parse json
            //JArray jsonArray = JArray.Parse(responseString.Result);
            JObject json = JObject.Parse(responseString.Result);

            Properties.Settings.Default.bearer_token = "";
            Properties.Settings.Default.Save();

        }
        private void PostUpdatePublicKey(string url, string publicOnlyKeyXML)
        {
            HttpClient client = new HttpClient();
            bearer_token = Properties.Settings.Default.bearer_token;
            var values = new Dictionary<string,string>
            {
                {"public_key", publicOnlyKeyXML }                
            };
            var content = new FormUrlEncodedContent(values);
            // header
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bearer_token);
            var response = client.PostAsync(url, content);
            var responseString = response.Result.Content.ReadAsStringAsync();
            // parse json
            //JArray jsonArray = JArray.Parse(responseString.Result);
            JObject json = JObject.Parse(responseString.Result);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            _ = PostLogin(txtUsername.Text, txtPassword.Text, "online");
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            SignUp frmsgn = new SignUp();
            this.Hide();
            frmsgn.ShowDialog();
            this.Show();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            PostLogOut("http://filetrans.f4koin.cyou/api/logout");
        }
    }
}