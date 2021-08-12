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

        #endregion

        #endregion
    }
}
