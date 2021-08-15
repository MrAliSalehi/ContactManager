﻿using System;
using System.Net;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using ClientManager.AppConfig;
using ClientManager.Models;
using Newtonsoft.Json;
namespace ClientManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }


        #region Control Handlers

        #region MainForm
        private void Form1_Load(object sender, EventArgs e)
        {
            //if (NetConnection() is not true)
            //{
            //    MessageBox.Show("Need Net Connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    Application.Exit();
            //}

            var columns = UserItems.ItemNames;
            foreach (var column in columns)
            {
                DGV_1.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = column, HeaderText = column, Name = $"N:{column}" });
            }
        }
        #endregion

        #region Buttons
        private void BTN_ADD_Click(object sender, EventArgs e)
        {
            AddUserForm adduser = new AddUserForm();
            adduser.ShowDialog();
            this.Hide();
        }
        #endregion

        #region TextBox
        private async void TB_seach_TextChanged(object sender, EventArgs e)
        {
            if (TB_seach.Text.Length >= 2)
            {
                ReadToken token = new();
                ApiHandler api = new ApiHandler();
                var res = await api.SearchUserAsync(TB_seach.Text, await token.TokenReaderAsync());
                if (res is not null)
                {
                    DGV_1.Rows.Clear();
                    foreach (var data in res.Content)
                    {
                        var strData = data.ToString();
                        UserModel DSData = JsonConvert.DeserializeObject<UserModel>(strData);
                        string[] SplitName = DSData.FullName.Split(':');
                        DGV_1.Rows.Add(DSData.ID, SplitName[0], SplitName[1], DSData.Phone, DSData.Email, DSData.Note);
                    }
                }
                else
                {
                    DGV_1.Rows.Clear();
                    DGV_1.Rows.Add("no", "content", "found");
                }

            }
            else
            {
                DGV_1.Rows.Clear();
            }
        }
        private void TB_seach_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsLetter(e.KeyChar) == false && char.IsControl(e.KeyChar) == false)
            {
                e.Handled = true;
            }
        }
        #endregion

        #endregion

        #region Functions

        public bool NetConnection()
        {
            try
            {
                var conf = new Config.Config.ConnectionConfig();
                conf.URL ??= CultureInfo.InstalledUICulture switch
                {
                    { Name: var n } when n.StartsWith("fa") => // Iran
                 "http://www.aparat.com",
                    { Name: var n } when n.StartsWith("zh") => // China
                        "http://www.baidu.com",
                    _ =>
                        "http://www.gstatic.com/generate_204",
                };
                var request = (HttpWebRequest)WebRequest.Create(conf.URL);
                request.KeepAlive = false;
                request.Timeout = conf.TimeOut;
                using var response = (HttpWebResponse)request.GetResponse();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }


        #endregion


    }
}
