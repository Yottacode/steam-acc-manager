using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Windows.Forms;


namespace Steam_Account_Manager
{
    public partial class MainForm : Form
    {

        static string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + @"\Steam Account Manager\";
        public static MainForm main = null;
        private static byte[] passwordBytes = SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes("123"));
        // List of users
        List<accountInfo> accountList = new List<accountInfo>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            main = this;

            // if client does not have a datafile create a new 

            Directory.CreateDirectory(path);

            if (File.Exists(path + "AccountInfo.smg11"))
            {
                Decrypt();

                UpdateAccountList();
            }

        }
        void Decrypt()
        {
            byte[] bytesDecrypted = AES_Decrypt(File.ReadAllBytes(path + "AccountInfo.smg11"), passwordBytes);

            foreach (string line in Encoding.UTF8.GetString(bytesDecrypted).Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries))
            {
                accountList.Add(new accountInfo(line.Split(',')[0], line.Split(',')[1]));
            }
        }

        void Encrypt()
        {
            if (accountList.Count > 0)
            {

                string data = "";
                foreach (accountInfo account in accountList)
                {
                    data += account.username + "," + account.password + Environment.NewLine;
                }
                File.WriteAllBytes(path + "AccountInfo.smg11", AES_Encrypt(Encoding.UTF8.GetBytes(data), passwordBytes));
            }
            else
            {
                if (File.Exists(path + "AccountInfo.smg11"))
                {
                    File.Delete(path + "AccountInfo.smg11");
                }
            }
        }

        private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            try
            {
                byte[] encryptedBytes = null;
                byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        AES.Mode = CipherMode.CBC;

                        using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                            cs.Close();
                        }
                        encryptedBytes = ms.ToArray();
                    }
                }
                return encryptedBytes;
            }
            catch
            {
                return null;
            }
        }

        public byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {

                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.FlushFinalBlock();
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }


        private void addAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddAccountForm addForm = new AddAccountForm();
            addForm.ShowDialog();

            if (addForm.DialogResult == DialogResult.OK)
            {
                // add new user to datafile 

                accountList.Add(new accountInfo(addForm.newUsername, addForm.newPassword));

                Encrypt();

                UpdateAccountList();
            }
        }

      



        

        private void LoginBtn_Click(object sender, EventArgs e)
        {
            // separate thread to not freeze the window 
            var ds = new Thread(Login) { IsBackground = true };
            ds.Start();
        }
        void Login()
        {
            if (AccountListBox.SelectedIndex != -1)
            {
                // kill current steam process if there is one 
                foreach (var process in Process.GetProcessesByName("steam"))
                {
                    process.Kill();
                }
                // start steam with login
                ProcessStartInfo startinfo = new ProcessStartInfo();

                startinfo.FileName = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam", "SteamExe", "null");
                startinfo.Arguments = " -login " + accountList[AccountListBox.SelectedIndex].username + " " +
                    accountList[AccountListBox.SelectedIndex].password;
                Process.Start(startinfo);
            }
        }
        
        void UpdateAccountList()
        {
            AccountListBox.Items.Clear();

            foreach (accountInfo account in accountList)
            {
                AccountListBox.Items.Add(account.username);
            }
        }

        struct accountInfo
        {
            public accountInfo(string user, string pass)
            {
                username = user;
                password = pass;
            }
            public string username { get; }
            public string password { get; }
        }

        private void DeleteBtn_Click_1(object sender, EventArgs e)
        {
            if (AccountListBox.SelectedIndex != -1)
            {
                accountList.RemoveAt(AccountListBox.SelectedIndex);

                Encrypt();
                UpdateAccountList();
            }
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void bnt_show_pws_Click(object sender, EventArgs e)
        {
            textBox1.Text = accountList[AccountListBox.SelectedIndex].username + "     " +
                    accountList[AccountListBox.SelectedIndex].password;
        }
    }
}