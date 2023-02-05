using System;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Steam_Account_Manager
{
    public partial class AddAccountForm : Form
    {

        public string newUsername { get { return UsernameBox.Text; } }
        public string newPassword { get { return PasswordBox.Text; } }

        public AddAccountForm()
        {
            InitializeComponent();
        }

        private void OKBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void AddAccountForm_Load(object sender, EventArgs e)
        {

        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
