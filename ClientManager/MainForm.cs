using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        
        #endregion

    }
}
