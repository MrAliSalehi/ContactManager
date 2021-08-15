using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;
using ClientManager.Models;
using ClientManager;
namespace ClientManager
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text is not null && textBox2.Text is not null)
            {
                ApiHandler api = new();
                var isValid = await api.LoginUserAsync(new LoginViewModel() { username = textBox1.Text, password = textBox2.Text });
                if (isValid)
                {
                    this.Hide();
                    MainForm fm = new MainForm();
                    fm.Show();
                }
                else
                {
                    groupBox1.Text = "Wrong User Or Pass";
                    groupBox1.ForeColor = Color.Red;
                }
            }
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData is Keys.Enter)
            {
                button1_Click(new object(), null);
            }
        }
    }
}
