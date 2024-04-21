using Guna.UI2.WinForms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.Media.Playback;

namespace OwlOS_Utility
{
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
            guna2Panel1.Hide();
        }

        private void SettingsControl_Load(object sender, EventArgs e)
        {
            AltTabOld.BorderRadius = 5;
            AltTabNew.BorderRadius = 5;
            TaskbarDisable.BorderRadius = 5;
            TaskbarEnable.BorderRadius = 5;
            FSOdisable.BorderRadius = 5;
            FSOenable.BorderRadius = 5;
            ContextNew.BorderRadius = 5;
            ContextOld.BorderRadius = 5;
            Wallpaperbutton.BorderRadius = 5;
            this.AutoScaleMode = AutoScaleMode.Dpi;
            guna2Transition1.ShowSync(guna2Panel1);
            using RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");
            if (key != null)
            {
                object productName = key.GetValue("ProductName");
                if (productName != null)
                {
                    string productNameString = productName.ToString();
                    if (productNameString.Contains("Windows 10 Pro"))
                    {
                        ContextOld.Enabled = false;
                        ContextNew.Enabled = false;
                        ContextOld.Text = "Context Menu (W11)";
                        ContextNew.Text = "Context Menu (W11)";
                    }
                }
            }
        }


        private void TaskbarEnable_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ExtendedUIHoverTime", "00000100", RegistryValueKind.DWord);
            _ = new Process();
            foreach (Process exe in Process.GetProcesses())
            {
                if (exe.ProcessName == "explorer")
                    exe.Kill();
            }
            CustomNotificationManager.Instance.ShowNotification("Taskbar Preview has been enabled", this.FindForm());
        }

        private void TaskbarDisable_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Advanced", "ExtendedUIHoverTime", "90000000", RegistryValueKind.DWord);
            _ = new Process();
            foreach (Process exe in Process.GetProcesses())
            {
                if (exe.ProcessName == "explorer")
                    exe.Kill();
            }
            CustomNotificationManager.Instance.ShowNotification("Taskbar Preview has been disabled", this.FindForm());
        }

        private void FSOenable_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_FSEBehavior", "00000000", RegistryValueKind.DWord);
            CustomNotificationManager.Instance.ShowNotification("Fullscreen Optimization has been enabled", this.FindForm());
        }

        private void FSOdisable_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\System\GameConfigStore", "GameDVR_FSEBehavior", "00000002", RegistryValueKind.DWord);
            CustomNotificationManager.Instance.ShowNotification("Fullscreen Optimization has been disabled", this.FindForm());
        }

        private void ContextOld_Click(object sender, EventArgs e)
        {
            string keyPath1 = @"SOFTWARE\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}";
            string keyPath2 = @"SOFTWARE\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32";

            Registry.CurrentUser.CreateSubKey(keyPath1);
            Registry.CurrentUser.CreateSubKey(keyPath2);
            _ = new Process();
            foreach (Process exe in Process.GetProcesses())
            {
                if (exe.ProcessName == "explorer")
                    exe.Kill();
            }
            CustomNotificationManager.Instance.ShowNotification("Old context menu has been set", this.FindForm());
        }

        private void ContextNew_Click(object sender, EventArgs e)
        {
            try
            {
                string keyPath1 = @"SOFTWARE\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}";
                string keyPath2 = @"SOFTWARE\Classes\CLSID\{86ca1aa0-34aa-4e8b-a509-50c905bae2a2}\InprocServer32";

                Registry.CurrentUser.DeleteSubKeyTree(keyPath1);
                Registry.CurrentUser.DeleteSubKeyTree(keyPath2);
            }
            catch (Exception)
            {
                
            }

            _ = new Process();
            foreach (Process exe in Process.GetProcesses())
            {
                if (exe.ProcessName == "explorer")
                    exe.Kill();
            }
            CustomNotificationManager.Instance.ShowNotification("New context menu has been set", this.FindForm());
        }

        private const int SPI_SETDESKWALLPAPER = 0x0014;
        private const int SPIF_UPDATEINIFILE = 0x01;
        private const int SPIF_SENDCHANGE = 0x02;

        [LibraryImport("user32.dll", EntryPoint = "SystemParametersInfoW", StringMarshalling = StringMarshalling.Utf16)]
        private static partial int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);


        private async void guna2Button1_Click(object sender, EventArgs e)
        {

            // Uruchom operacje w tle za pomocą Task.Run
            await Task.Run(() =>
            {
                // Tutaj umieść operacje wykonywane po kliknięciu przycisku
                string imagePath = @"C:\ProgramData\OwlOS Utility\owlwallpaper.png";

                if (SetWallpaper(imagePath))
                {
                    // Jeśli udało się ustawić tapetę, wyświetl powiadomienie
                    Invoke(new Action(() =>
                    {
                        CustomNotificationManager.Instance.ShowNotification("OwlOS wallpaper has been set", this.FindForm());
                    }));
                }
                else
                {
                    Invoke(new Action(() =>
                    {
                    }));
                }
            });
        }

        private static bool SetWallpaper(string imagePath)
        {
            // Sprawdź, czy plik obrazu istnieje
            if (!File.Exists(imagePath))
            {
                return false;
            }

            try
            {
                // Ustaw tapetę za pomocą klasy SystemParameters
                _ = SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, imagePath, SPIF_UPDATEINIFILE | SPIF_SENDCHANGE);

                // Zwróć true, jeśli ustawienie tapety zakończyło się sukcesem
                return true;
            }
            catch (Exception ex)
            {
                // Obsłuż błąd
                Console.WriteLine("Error setting wallpaper: " + ex.Message);
                return false;
            }
        }

            private void AltTabOld_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", "00000001", RegistryValueKind.DWord);
            _ = new Process();
            foreach (Process exe in Process.GetProcesses())
            {
                if (exe.ProcessName == "explorer")
                    exe.Kill();
            }
            CustomNotificationManager.Instance.ShowNotification("Old Alt Tab has been set", this.FindForm());
        }

        private void AltTabNew_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer", "AltTabSettings", "00000000", RegistryValueKind.DWord);
            _ = new Process();
            foreach (Process exe in Process.GetProcesses())
            {
                if (exe.ProcessName == "explorer")
                    exe.Kill();
            }
            CustomNotificationManager.Instance.ShowNotification("New Alt Tab has been set", this.FindForm());
        }
    }
}
