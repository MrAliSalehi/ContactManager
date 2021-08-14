using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using Newtonsoft.Json;
using ClientManager.Models;
using System.Security.Claims;

namespace ClientManager
{
    public partial class Login : Form
    {
        private HttpClient _client;
        public Login()
        {
            InitializeComponent();
            _client = new HttpClient();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text is not null && textBox2.Text is not null)
            {
                var IsValid = LoginUser(new LoginViewModel() { Username = textBox1.Text, Password = textBox2.Text });
                if (IsValid)
                {
                    this.Hide();
                    MainForm fm = new MainForm();
                    fm.Show();
                }
            }
        }
        public bool LoginUser(LoginViewModel login)
        {
            var body = JsonConvert.SerializeObject(login);
            var content = new StringContent(body, Encoding.UTF8, "application/json");
            var responce = _client.PostAsync("http://localhost:44000/api/auth", content).Result;
            if (responce.IsSuccessStatusCode)
            {
                var t = responce.Content.ReadAsStringAsync().Result;
                var j = JsonConvert.DeserializeObject<TokenViewModel>(t);
                List<Claim> clm = new()
                {
                    new Claim(ClaimTypes.NameIdentifier, login.Username),
                    new Claim("AccessToken", j.token)
                };
                var identity = new ClaimsIdentity(clm);
                return true;

            }
            else
            {
                return false;
            }
        }
    }
}
