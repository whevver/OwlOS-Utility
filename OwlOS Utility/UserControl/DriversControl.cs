using Guna.UI2.WinForms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OwlOS_Utility
{
    public partial class DriversControl : UserControl
    {

        public DriversControl()
        {
            InitializeComponent();
            guna2Panel3.Hide();
        }

        private void AMDButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to open AMD drivers site?", "AMD Drivers Site", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", "https://www.amd.com/en/support");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void NvidiaButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to open Nvidia drivers site?", "Nvidia Drivers site", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", "https://www.nvidia.com/download/index.aspx");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void DriversControl_Load(object sender, EventArgs e)
        {
            NvidiaButton.BorderRadius = 5;
            AMDButton.BorderRadius = 5;
            IntelButton.BorderRadius = 5;
            guna2Transition1.ShowSync(guna2Panel3);
            CheckBrowsers();
            this.AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void CheckBrowsers()
        {
            if (!IsAnyBrowserInstalled())
            {
                AMDButton.Enabled = false;
                NvidiaButton.Enabled = false;
                IntelButton.Enabled = false;
                CustomNotificationManager.Instance.ShowNotification("No browser installed. Please download the browser", this.FindForm());
            }
        }

        private static bool IsAnyBrowserInstalled()
        {
            string[] browserPaths = [
                @"C:\Program Files\Google\Chrome\Application\chrome.exe",
                @"C:\Program Files\Mozilla Firefox\firefox.exe",
                @"C:\Program Files\Opera\launcher.exe",
                @"C:\Program Files\Opera GX\launcher.exe",
                @"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe"
            ];


            foreach (string path in browserPaths)
            {
                if (File.Exists(path))
                {
                    return true;
                }
            }

            return false;
        }

        private void ProfileInspectorbtn_Click(object sender, EventArgs e)
        {

            Process.Start("Explorer.exe", @"C:\ProgramData\OwlOS Utility\Additional\nvidia.bat");
        }

        private void nvcleanbtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to open NVCleanInstall?", "NVCleanInstall", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("Explorer.exe", @"C:\ProgramData\OwlOS Utility\Apps\NVCleanstall_1.15.1.exe");
            }
            else if (dialogResult == DialogResult.No)
            {

            }

        }

        private void IntelButton_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to open Intel drivers site?", "Intel Drivers site", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", "https://www.intel.com/content/www/us/en/download/19364/27988/legacy-intel-graphics-driver-for-windows-10.html?");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", "https://www.amd.com/en/support/kb/release-notes/rn-rad-win-20-8-3");
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", "https://www.amd.com/en/support");
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", "https://www.amd.com/en/support/kb/release-notes/rn-rad-win-20-4-2");
        }

        private void nvidiadriversbtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to open Nvidia drivers site?", "Nvidia Drivers Site", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("Explorer.exe", @"C:\ProgramData\OwlOS Utility\Apps\NVCleanstall_1.15.1.exe");
            }
            else if (dialogResult == DialogResult.No)
            {

            }

        }

        private void guna2Panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
