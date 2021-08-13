﻿using ClientManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClientManager.Config;
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
            if (NetConnection() is not true)
            {
                MessageBox.Show("Need Net Connection", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }
        #endregion

        #region Buttons
        private void BTN_ADD_Click(object sender, EventArgs e)
        {
            AddUserForm adduser = new AddUserForm();
            //Initial AddUser Form
            adduser.ShowDialog();
            //Temperory Hide Main Form ,For Fun))
            this.Hide();
        }
        #endregion


        #region TextBox
        private async void TB_seach_TextChanged(object sender, EventArgs e)
        {
            if (TB_seach.Text.Length > 4)
            {
                var Text = TB_seach.Text;
                #region Search For User
                //await Config.Config.api.GetUser(new UserModel() { });
                #endregion
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
