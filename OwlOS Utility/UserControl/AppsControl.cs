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
    public partial class AppsControl : UserControl
    {
        public AppsControl()
        {
            InitializeComponent();
        }

        private void AppsControl_Load(object sender, EventArgs e)
        {
            guna2Panel1.Hide();
            Discordbtn.BorderRadius = 5;
            Steambtn.BorderRadius = 5;
            Spotifybtn.BorderRadius = 5;
            Capframexbtn.BorderRadius = 5;
            MSIAfterburner.BorderRadius = 5;
            zipbtn.BorderRadius = 5;
            winrarbtn.BorderRadius = 5;
            Faceitbtn.BorderRadius = 5;
            epicbtn.BorderRadius = 5;
            ubibtn.BorderRadius = 5;
            guna2Transition1.ShowSync(guna2Panel1);
            this.AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void Discordbtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to install Discord?", "Discord Setup", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("Explorer.exe", @"C:\ProgramData\OwlOS Utility\Installers\DiscordSetup.exe");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void Steambtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to install Steam", "Steam Setup", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", @"C:\ProgramData\OwlOS Utility\Installers\SteamSetup.exe");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void Spotifybtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to install Spotify?", "Spotify Setup", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", @"C:\ProgramData\OwlOS Utility\Installers\SpotifySetup.exe");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void Faceitbtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to install FaceIT?", "FaceIT Setup", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", @"C:\ProgramData\OwlOS Utility\Installers\FACEIT-setup-latest.exe");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void MSIAfterburner_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to install MSI Afterburner?", "MSI Afterburner Setup", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", @"C:\ProgramData\OwlOS Utility\Installers\MSIAfterburnerSetup465.exe");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void Capframexbtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to install CapFrameX?", "CapFrameX Setup", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", @"C:\ProgramData\OwlOS Utility\Installers\CapFrameXBootstrapper.exe");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void zipbtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to install 7zip?", "7zip Setup", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", @"C:\ProgramData\OwlOS Utility\Installers\7z2301-x64.exe");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void winrarbtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to install WinRar?", "WinRar Setup", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", @"C:\ProgramData\OwlOS Utility\Installers\winrar-x64-624.exe");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void epicbtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to install Epic Games?", "Epic Games Setup", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", @"C:\ProgramData\OwlOS Utility\Installers\EpicInstaller-15.17.1.msi");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void ubibtn_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to install Ubisoft?", "Ubisoft Setup", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Process.Start("explorer", @"C:\ProgramData\OwlOS Utility\Installers\UbisoftConnectInstaller.exe");
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
    }
}
