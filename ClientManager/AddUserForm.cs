using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppConfig.Status;
using ClientManager.AppConfig;
using ClientManager.Models;

namespace ClientManager
{
    public partial class AddUserForm : Form
    {
        public AddUserForm()
        {
            InitializeComponent();
        }

        #region Control Handler

        #region Buttons
        private void BTN_Cancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            MainForm mainfm = new MainForm();
            mainfm.Show();
        }
        private async void BTN_Submit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(TB_FirstN.Text) && !string.IsNullOrEmpty(TB_LastN.Text) && !string.IsNullOrEmpty(TB_Num.Text))
            {
                ApiHandler api = new();
                ReadToken token = new();
                var send= await api.AddUserAysnc(new UserModel()
                {
                    FullName = $"{TB_FirstN.Text}:{TB_LastN.Text}",
                    Phone = TB_Num.Text,
                    Email = $"{TB_Email.Text}",
                    Note = $"{TB_Note.Text}"
                },
                    await token.TokenReaderAsync());
                if (send.status == ResponceStatus.success)
                {
                    MessageBox.Show("done", "user added successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("failed", $"cant add user:\n{send.status}", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                groupBox1.Text = "cant be null";
                groupBox1.ForeColor = Color.Red;
            }
        }

        #endregion

        #endregion

    }
}
