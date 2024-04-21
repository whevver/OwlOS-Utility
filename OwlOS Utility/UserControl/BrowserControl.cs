using Guna.UI2.WinForms;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OwlOS_Utility
{
    public partial class BrowserControl : UserControl
    {
        public BrowserControl()
        {
            InitializeComponent();
            guna2Panel2.Hide();
        }

        private void BrowserControl_Load(object sender, EventArgs e)
        {
            guna2Panel1.Hide();
            guna2Panel3.Hide();
            guna2Panel4.Hide();
            Bravebtn.BorderRadius = 5;
            operagxbtn.BorderRadius = 5;
            chromebtn.BorderRadius = 5;
            firefoxbtn.BorderRadius = 5;
            operabtn.BorderRadius = 5;
            Bravetwbtn.BorderRadius = 5;
            guna2Button1.BorderRadius = 5;
            guna2Button2.BorderRadius = 5;
            guna2Transition1.ShowSync(guna2Panel2);
            this.AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void Bravebtn_Click(object sender, EventArgs e)
        {
            guna2Panel3.Hide();
            guna2Panel4.Hide();
            if (File.Exists(@"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe") ||
                File.Exists(@"C:\Program Files\BraveSoftware\Brave-Browser\Application\brave.exe"))
            {
                CustomNotificationManager.Instance.ShowNotification("Brave is downloaded. You can apply tweaks.", this.FindForm());
                guna2Panel1.Size = new Size(865, 201);
                guna2Panel1.Location = new Point(3, 222);
                guna2Transition1.ShowSync(guna2Panel1);
            }
            else
            {
                Process.Start("Explorer.exe", @"C:\ProgramData\OwlOS Utility\Browsers\BraveSetup.exe");
            }
        }

        static void ExecuteCommand(string command)
        {
            ProcessStartInfo psi = new("cmd.exe", $"/c {command}")
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process.Start(psi).WaitForExit();
        }

        private void operagxbtn_Click(object sender, EventArgs e)
        {
            Process.Start("Explorer.exe", @"C:\ProgramData\OwlOS Utility\Browsers\OperaGXSetup.exe");
        }

        private void chromebtn_Click(object sender, EventArgs e)
        {
            guna2Panel3.Hide();
            guna2Panel1.Hide();
            if (File.Exists(@"C:\Program Files\Google\Chrome\Application\chrome.exe") ||
                File.Exists(@"C:\Program Files\Google\Chrome\Application\chrome.exe"))
            {
                CustomNotificationManager.Instance.ShowNotification("Chrome is downloaded. You can apply tweaks.", this.FindForm());
                guna2Panel3.Size = new Size(865, 201);
                guna2Panel3.Location = new Point(3, 222);
                guna2Transition1.ShowSync(guna2Panel3);
            }
            else
            {
                Process.Start("Explorer.exe", @"C:\ProgramData\OwlOS Utility\Browsers\ChromeSetup.exe");
            }
        }

        private void firefoxbtn_Click(object sender, EventArgs e)
        {
            guna2Panel1.Hide();
            guna2Panel3.Hide();
            if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe") ||
                File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe"))
            {
                CustomNotificationManager.Instance.ShowNotification("Firefox is downloaded. You can apply tweaks.", this.FindForm());
                guna2Panel4.Size = new Size(865, 201);
                guna2Panel4.Location = new Point(3, 222);
                guna2Transition1.ShowSync(guna2Panel4);              
            }
            else
            {         
                Process.Start("Explorer.exe", @"C:\ProgramData\OwlOS Utility\Browsers\Firefox_Installer.exe");
            }
        }

        private void operabtn_Click(object sender, EventArgs e)
        {
            Process.Start("Explorer.exe", @"C:\ProgramData\OwlOS Utility\Browsers\OperaSetup.exe");
        }

        private void Bravetwbtn_Click(object sender, EventArgs e)
        {
            string cmdCommand = @"
net stop brave >nul 2>&1
net stop bravem >nul 2>&1
net stop BraveElevationService >nul 2>&1
sc config ""brave"" start=disabled >nul 2>&1
sc config ""bravem"" start=disabled >nul 2>&1
sc config ""BraveElevationService"" start=disabled >nul 2>&1
Reg.exe delete ""HKLM\Software\Microsoft\Active Setup\Installed Components\{AFE6A462-C574-4B8A-AF43-4CC60DF4563B}"" /f >nul 2>&1
";
            ExecuteCommand(cmdCommand);
            CustomNotificationManager.Instance.ShowNotification("Brave tweaks applied", this.FindForm());
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            string cmdCommand = @"
net stop gupdate >nul 2>&1
net stop gupdatem >nul 2>&1
net stop googlechromeelevationservice >nul 2>&1
sc config ""gupdate"" start=disabled >nul 2>&1
sc config ""gupdatem"" start=disabled >nul 2>&1
sc config ""googlechromeelevationservice"" start=disabled >nul 2>&1
Reg.exe delete ""HKLM\Software\Microsoft\Active Setup\Installed Components\{8A69D345-D564-463c-AFF1-A69D9E530F96}"" /f >nul 2>&1
";

            ExecuteCommand(cmdCommand);
            CustomNotificationManager.Instance.ShowNotification("Chrome tweaks applied.", this.FindForm());
        }

        private void guna2Button2_Click(object sender, EventArgs e) //Firefox tweaks
        {
            string cmdCommand = @"
net stop MozillaMaintenance >nul 2>&1
sc config ""MozillaMaintenance"" start=disabled >nul 2>&1
del /f ""C:\Program Files\Mozilla Firefox\updater.exe"" >nul 2>&1
del /f ""C:\Program Files\Mozilla Firefox\maintenanceservice.exe"" >nul 2>&1
del /f ""C:\Program Files\Mozilla Firefox\maintenanceservice_installer.exe"" >nul 2>&1
del /f ""C:\Program Files (x86)\Mozilla Maintenance Service\uninstall.exe"" >nul 2>&1
del /f ""C:\Program Files (x86)\Mozilla Maintenance Service\maintenanceservice.exe"" >nul 2>&1
";

            ExecuteCommand(cmdCommand);
            CustomNotificationManager.Instance.ShowNotification("Firefox tweaks applied", this.FindForm());
        }
    }
}
