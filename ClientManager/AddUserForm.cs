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
using Newtonsoft.Json;

namespace ClientManager
{
    public partial class AddUserForm : Form
    {
        #region Initial Form
        private FormMode _mode;
        private int _index = 0;
        public AddUserForm(FormMode mode)
        {
            _mode = mode;
            InitializeComponent();
        }
        public AddUserForm(FormMode mode, int index)
        {
            InitializeComponent();
            _mode = mode;
            _index = index;
        }
        #endregion

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
            ApiHandler api = new();
            ReadToken token = new();
            if (_mode is FormMode.Add)
            {
                if (!string.IsNullOrEmpty(TB_FirstN.Text) && !string.IsNullOrEmpty(TB_LastN.Text) && !string.IsNullOrEmpty(TB_Num.Text))
                {
                    var send = await api.AddUserAysnc(new UserModel()
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
            else
            {
                var updateUser = await api.UpdateUserAysnc(new UserModel()
                {
                     FullName = $"{TB_FirstN.Text}:{TB_LastN.Text}",
                     Phone = TB_Num.Text,
                     Email = TB_Email.Text,
                     Note = TB_Note.Text,
                },
                    _index,
                    await token.TokenReaderAsync());
                if (updateUser.status == ResponceStatus.success)
                {
                    MessageBox.Show("Done");
                    Hide();
                    MainForm fm = new();
                    fm.Show();
                }
                else
                {
                    MessageBox.Show("Failed");
                }
            }
        }

        #endregion

        #region Form
        private async  void AddUserForm_Load(object sender, EventArgs e)
        {
            if (_mode is FormMode.Edit)
            {
                ReadToken tok = new();
                ApiHandler api = new();
                var getuser = await api.SearchUserByIDAsync(_index, await tok.TokenReaderAsync());
                if (getuser.status is ResponceStatus.success)
                {
                    BTN_Submit.Text = "Update";
                    UserModel DSData = JsonConvert.DeserializeObject<UserModel>(getuser.Content.ToString());
                    string[] SplitName = DSData.FullName.Split(':');
                    TB_FirstN.Text = SplitName[0];
                    TB_LastN.Text = SplitName[1];
                    TB_Num.Text = DSData.Phone;
                    TB_Email.Text = DSData.Email;
                    TB_Note.Text = DSData.Note;

                }
            }

        }

        #endregion

        #endregion
    }

}
