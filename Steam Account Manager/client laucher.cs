using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Diagnostics;
using System.Threading;


namespace Steam_Account_Manager
{
    public partial class client_laucher : Form
    {
        public client_laucher()
        {
            InitializeComponent();
        }

        private void client_laucher_Load(object sender, EventArgs e)
        {

        }

       

        private void bnt_wallpaper_Click(object sender, EventArgs e)
        {

            ProcessStartInfo startinfo = new ProcessStartInfo();

            startinfo.FileName = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam", "SteamExe", "null");
            startinfo.Arguments = " steam://rungameid/431960 ";
            Process.Start(startinfo);
            DialogResult = DialogResult.OK;
        }

        private void bnt_apex_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startinfo = new ProcessStartInfo();

            startinfo.FileName = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam", "SteamExe", "null");
            startinfo.Arguments = " steam://rungameid/1172470 ";
            Process.Start(startinfo);
            DialogResult = DialogResult.OK;
        }

        private void bnt_r6_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startinfo = new ProcessStartInfo();

            startinfo.FileName = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam", "SteamExe", "null");
            startinfo.Arguments = " steam://rungameid/359550 ";
            Process.Start(startinfo);
            DialogResult = DialogResult.OK;
        }

        private void Bnt_rust_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startinfo = new ProcessStartInfo();

            startinfo.FileName = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam", "SteamExe", "null");
            startinfo.Arguments = " steam://rungameid/252490 ";
            Process.Start(startinfo);
            DialogResult = DialogResult.OK;
        }

        private void bnt_amongus_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startinfo = new ProcessStartInfo();

            startinfo.FileName = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam", "SteamExe", "null");
            startinfo.Arguments = " steam://rungameid/945360 ";
            Process.Start(startinfo);
            DialogResult = DialogResult.OK;
        }

        private void bnt_stronghold_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startinfo = new ProcessStartInfo();

            startinfo.FileName = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam", "SteamExe", "null");
            startinfo.Arguments = " steam://rungameid/40970 ";
            Process.Start(startinfo);
            DialogResult = DialogResult.OK;
        }

        private void bnt_csgo_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startinfo = new ProcessStartInfo();

            startinfo.FileName = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam", "SteamExe", "null");
            startinfo.Arguments = " steam://rungameid/730 ";
            Process.Start(startinfo);
            DialogResult = DialogResult.OK;
        }

        private void bnt_legostarwars_Click(object sender, EventArgs e)
        {
            ProcessStartInfo startinfo = new ProcessStartInfo();

            startinfo.FileName = (string)Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Valve\Steam", "SteamExe", "null");
            startinfo.Arguments = " steam://rungameid/920210 ";
            Process.Start(startinfo);
            DialogResult = DialogResult.OK;
        }
    }
}
