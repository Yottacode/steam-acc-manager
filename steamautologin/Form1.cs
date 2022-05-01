using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Microsoft.Win32;


namespace steamautologin
{
    public partial class Form1 : Form
    {
        static string path = @"C:\Data.txt";
        struct user
        {
            public user(string user, string pass)
            {
                username = user;
                password = pass;
            }
            public string username { get; }
            public string password { get; }
        }

        // List of users
        List<user> userlist = new List<user>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            CheckForIllegalCrossThreadCalls = false;

            // if client does not have a datafile create a new 

            if (!File.Exists(path))
            {

                StreamWriter sw = File.CreateText(path);
                sw.Flush();
                sw.Dispose();
            }

            // Load data to combobox and userlist
            List<string> lines = File.ReadAllLines(path).ToList();

            foreach (var line in lines)
            {
                string[] entries = line.Split(',');
                user newuser = new user(entries[0], entries[1]);
                comboBox1.Items.Add(entries[0]);
                userlist.Add(newuser);
            }

        }

        private void addnew_Click(object sender, EventArgs e)
        {
            // add new user to datafile 
            List<string> lines = File.ReadAllLines(path).ToList();
            var User = new user(textBox1.Text, textBox2.Text);
            userlist.Add(User);
            lines.Add(User.username + "," + User.password);
            File.WriteAllLines(path, lines);

            //refresh combobox
            comboBox1.Items.Clear();
            foreach (user v in userlist)
            {
                comboBox1.Items.Add(v.username);
            }
        }
        
        private void loginbnt_Click(object sender, EventArgs e)
        {
            // separate thread to not freeze the window 
            var ds = new Thread(login) { IsBackground = true };
            ds.Start();
        }

        void login()
        {
            // kill current steam process if there is one 
            foreach (var process in Process.GetProcessesByName("steam"))
            {
                process.Kill();
            }
            // start steam with login
            ProcessStartInfo startinfo = new ProcessStartInfo();

            startinfo.FileName = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam",
                "SteamExe", "null");
            startinfo.Arguments = " -login " + userlist[comboBox1.SelectedIndex].username + " " +
                userlist[comboBox1.SelectedIndex].password;
            Process.Start(startinfo);
        }       
    }
}