using Guna.UI2.WinForms;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.UI.Core.AnimationMetrics;
namespace OwlOS_Utility
{
    public partial class ServicesControl : UserControl
    {
        public ServicesControl()
        {
            InitializeComponent();
            installstore.BorderRadius = 5;
            uninstallstore.BorderRadius = 5;
            windowsdeafult.BorderRadius = 5;
            Owldeafult.BorderRadius = 5;
            this.AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void RunNetshCommand()
        {
            try
            {
                Process process = new();
                process.StartInfo.FileName = "netsh";
                process.StartInfo.Arguments = "interface tcp show global";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();

                string autoTuningStatus = GetAutoTuningStatus(output);

                //MessageBox.Show("AutoTuningLevel Status: " + autoTuningStatus, "AutoTuningLevel Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception)
            {
                //MessageBox.Show("Wystąpił błąd: " + ex.Message, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetAutoTuningStatus(string output)
        {
            output = output.ToLower();

            if (output.Contains("normal") || output.Contains("włączony"))
            {
                guna2ToggleSwitch5.Checked = true;
                label5.Text = "AutoTuningLevel (Normal)";
                return "Normal";
            }
            else if (output.Contains("disabled") || output.Contains("wyłączony"))
            {
                return "Disabled";
            }
            else
            {
                return "Unknown";
            }
        }

        private void RunBcdeditCommand()
        {
            try
            {

                Process process = new();
                process.StartInfo.FileName = "bcdedit";
                process.StartInfo.Arguments = "/enum {current}";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;

                process.Start();

                string output = process.StandardOutput.ReadToEnd();

                process.WaitForExit();

                string depStatus = GetDEPStatus(output);

            }
            catch (Exception)
            {

            }
        }

        private string GetDEPStatus(string output)
        {

            output = output.ToLower();

            if (output.Contains("alwaysoff") || output.Contains("off"))
            {
                return "Disabled";
            }
            else if (output.Contains("optin") || output.Contains("optin"))
            {
                guna2ToggleSwitch8.Checked = true;
                label8.Text = "DEP (Enabled)";
                return "OptIn";
            }
            else
            {
                return "Unknown";
            }
        }

        private void ServicesControl_Load(object sender, EventArgs e)
        {
            guna2Panel1.Hide();
            guna2Transition1.ShowSync(guna2Panel1);
            RunNetshCommand();
            RunBcdeditCommand();

            RegistryKey keywifi = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            keywifi = keywifi.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WlanSvc");
            Int64 valuewifi = Convert.ToInt64(keywifi.GetValue("Start").ToString());
            if (valuewifi == 2)
            {
                guna2ToggleSwitch2.Checked = true;
                label2.Text = "Wi-Fi (Enabled)";
            }

            RegistryKey keyprint = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            keyprint = keyprint.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\Spooler");
            Int64 valueprint = Convert.ToInt64(keyprint.GetValue("Start").ToString());
            if (valueprint == 2)
            {
                guna2ToggleSwitch3.Checked = true;
                label3.Text = "Print (Enabled)";
            }

            RegistryKey keycdrom = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            keycdrom = keycdrom.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\cdrom");
            Int64 valuecdrom = Convert.ToInt64(keycdrom.GetValue("Start").ToString());
            if (valuecdrom == 2)
            {
                guna2ToggleSwitch4.Checked = true;
                label4.Text = "CD-ROM (Enabled)";
            }

            RegistryKey keyvpn = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            keyvpn = keyvpn.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\RasMan");
            Int64 valuevpn = Convert.ToInt64(keyvpn.GetValue("Start").ToString());
            if (valuevpn == 3)
            {
                guna2ToggleSwitch6.Checked = true;
                label6.Text = "VPN (Enabled)";
            }

            RegistryKey keybluetooth = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            keybluetooth = keybluetooth.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\BluetoothUserService");
            Int64 valuebluetooth = Convert.ToInt64(keybluetooth.GetValue("Start").ToString());
            if (valuebluetooth == 3)
            {
                guna2ToggleSwitch7.Checked = true;
                label7.Text = "Bluetooth (Enabled)";
            }

            RegistryKey keyproxy = RegistryKey.OpenBaseKey(Microsoft.Win32.RegistryHive.LocalMachine, RegistryView.Registry64);
            keyproxy = keyproxy.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\WinHttpAutoProxySvc");
            Int64 valueproxy = Convert.ToInt64(keyproxy.GetValue("Start").ToString());
            if (valueproxy == 2)
            {
                guna2ToggleSwitch9.Checked = true;
                label9.Text = "Proxy (Enabled)";
            }

            bool isBitlockerregExists = IsRegistryKeyExists(@"SYSTEM\CurrentControlSet\Services\ShellHWDetection", "Start", 2);

            if (isBitlockerregExists)
            {
                guna2ToggleSwitch11.Checked = true;
                label12.Text = "Bitlocker (Enabled)";
            }

            bool isDefenderregExist = IsRegistryKeyExists(@"SYSTEM\ControlSet001\Services\SgrmBroker", "Start", 2);

            if (isDefenderregExist)
            {
                guna2ToggleSwitch10.Checked = true;
                label11.Text = "Windows Defender \r(Enabled)";
            }

        }

        private const string ValueName = "AutoTuningLevel";
        private IList<string> outputForInvestigation;

        public ServicesControl(IList<string> outputForInvestigation)
        {
            this.outputForInvestigation = outputForInvestigation;
        }

        private static int ExecuteBcdEdit(string arguments, out IList<string> output)
        {
            var cmdFullFileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows),
                                               Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess
                                                   ? @"Sysnative\cmd.exe"
                                                   : @"System32\cmd.exe");

            ProcessStartInfo processStartInfo = new(cmdFullFileName, "/c bcdedit " + arguments) { UseShellExecute = false, RedirectStandardOutput = true, CreateNoWindow = true, WindowStyle = ProcessWindowStyle.Hidden };
            ProcessStartInfo psi = processStartInfo;
            var process = new Process { StartInfo = psi };

            process.Start();
            StreamReader outputReader = process.StandardOutput;
            psi.UseShellExecute = false;
            psi.WindowStyle = ProcessWindowStyle.Hidden;
            process.WaitForExit();
            output = outputReader.ReadToEnd().Split(new[] { Environment.NewLine }, StringSplitOptions.None).ToList();
            return process.ExitCode;
        }
        private void guna2ToggleSwitch2_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch2.Checked == true)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WlanSvc", "Start", "2", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\vwififlt", "Start", "1", RegistryValueKind.DWord);
                label2.Text = "Wi-Fi (Enabled)";
                CustomNotificationManager.Instance.ShowNotification("Wi-Fi has been enabled. Restart PC", this.FindForm());
            }
            if (guna2ToggleSwitch2.Checked == false)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WlanSvc", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\vwififlt", "Start", "4", RegistryValueKind.DWord);
                label2.Text = "Wi-Fi (Disabled)";
                CustomNotificationManager.Instance.ShowNotification("Wi-Fi has been disabled. Restart PC", this.FindForm());
            }
        }

        private void guna2ToggleSwitch3_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch3.Checked == true)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Spooler", "Start", "2", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PrintNotify", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PrintWorkflowUserSvc", "Start", "2", RegistryValueKind.DWord);
                label3.Text = "Print (Enabled)";
                CustomNotificationManager.Instance.ShowNotification("Print has been enabled. Restart PC", this.FindForm());
            }
            if (guna2ToggleSwitch3.Checked == false)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Spooler", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PrintNotify", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PrintWorkflowUserSvc", "Start", "4", RegistryValueKind.DWord);
                label3.Text = "Print (Disabled)";
                CustomNotificationManager.Instance.ShowNotification("Print has been disabled. Restart PC", this.FindForm());
            }
        }

        private void guna2ToggleSwitch4_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch4.Checked == true)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\cdrom", "Start", "2", RegistryValueKind.DWord);
                label4.Text = "CD-ROM (Enabled)";
                CustomNotificationManager.Instance.ShowNotification("CD-ROM has been enabled. Restart PC", this.FindForm());
            }
            if (guna2ToggleSwitch4.Checked == false)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\cdrom", "Start", "4", RegistryValueKind.DWord);
                label4.Text = "CD-ROM (Disabled)";
                CustomNotificationManager.Instance.ShowNotification("CD-ROM has been disabled. Restart PC", this.FindForm());
            }
        }

        private void guna2ToggleSwitch5_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch5.Checked == true)
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\OwlOS", "AutoTuningLevel", "0", RegistryValueKind.DWord);
                Process process = new();
                System.Diagnostics.ProcessStartInfo startInfo = new()
                {
                    CreateNoWindow = true,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    FileName = "cmd.exe",
                    Arguments = "/C netsh int tcp set global autotuninglevel = normal"
                };
                process.StartInfo = startInfo;
                process.Start();
                Registry.SetValue(@"HKEY_CURRENT_USER\OwlOS", "AutoTuningLevel", "1", RegistryValueKind.DWord);
                label5.Text = "AutoTuningLevel (Normal)";
                CustomNotificationManager.Instance.ShowNotification("AutoTuningLevel has been set to Normal. Restart PC", this.FindForm());
            }
            if (guna2ToggleSwitch5.Checked == false)
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\OwlOS", ValueName, "0", RegistryValueKind.DWord);
                System.Diagnostics.Process process = new();
                System.Diagnostics.ProcessStartInfo startInfo = new()
                {
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    CreateNoWindow = true,
                    FileName = "cmd.exe",
                    Arguments = "/C netsh int tcp set global autotuninglevel = disabled"
                };
                process.StartInfo = startInfo;
                process.Start();
                label5.Text = "AutoTuningLevel (Disabled)";
                CustomNotificationManager.Instance.ShowNotification("AutoTuningLevel has been set to Normal. Restart PC", this.FindForm());
            }
        }

        private void guna2ToggleSwitch6_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch6.Checked == true)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasMan", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PptpMiniport", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasAgileVpn", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Rasl2tp", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasSstp", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasPppoe", "Start", "3", RegistryValueKind.DWord);
                label6.Text = "VPN (Enabled)";
                CustomNotificationManager.Instance.ShowNotification("VPN has been enabled. Restart PC", this.FindForm());
            }
            if (guna2ToggleSwitch6.Checked == false)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasMan", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PptpMiniport", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasAgileVpn", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Rasl2tp", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasSstp", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasPppoe", "Start", "4", RegistryValueKind.DWord);
                label6.Text = "VPN (Disabled)";
                CustomNotificationManager.Instance.ShowNotification("VPN has been disabled. Restart PC", this.FindForm());
            }
        }

        private void guna2ToggleSwitch7_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch7.Checked == true)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BluetoothUserService", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTAGService", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthA2dp", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthAvctpSvc", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthEnum", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthHFEnum", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthLEEnum", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthMini", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\SYSTEM\CurrentControlSet\Services\BthPan", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTHPORT", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\bthserv", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTHUSB", "Start", "3", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\HidBth", "Start", "3", RegistryValueKind.DWord);
                label7.Text = "Bluetooth (Enabled)";
                CustomNotificationManager.Instance.ShowNotification("Bluetooth has been enabled. Restart PC", this.FindForm());
            }
            if (guna2ToggleSwitch7.Checked == false)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BluetoothUserService", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTAGService", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthA2dp", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthAvctpSvc", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthEnum", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthHFEnum", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthLEEnum", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthMini", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\SYSTEM\CurrentControlSet\Services\BthPan", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTHPORT", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\bthserv", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTHUSB", "Start", "4", RegistryValueKind.DWord);
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\HidBth", "Start", "4", RegistryValueKind.DWord);
                label7.Text = "Bluetooth (Disabled)";
                CustomNotificationManager.Instance.ShowNotification("Bluetooth has been disabled. Restart PC", this.FindForm());
            }
        }

        private void guna2ToggleSwitch8_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch8.Checked == true)
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\OwlOS", "DEP", "1", RegistryValueKind.DWord);
                _ = ExecuteBcdEdit("/set nx optin", out outputForInvestigation);
                label8.Text = "DEP (Enabled)";
                CustomNotificationManager.Instance.ShowNotification("DEP has been enabled. Restart PC", this.FindForm());
            }
            if (guna2ToggleSwitch8.Checked == false)
            {
                Registry.SetValue(@"HKEY_CURRENT_USER\OwlOS", "DEP", "0", RegistryValueKind.DWord);
                _ = ExecuteBcdEdit("/set nx alwaysoff", out outputForInvestigation);
                label8.Text = "DEP (Disabled)";
                CustomNotificationManager.Instance.ShowNotification("DEP has been disabled. Restart PC", this.FindForm());
            }
        }

        private void guna2ToggleSwitch9_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch9.Checked == true)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WinHttpAutoProxySvc", "Start", "2", RegistryValueKind.DWord);
                label9.Text = "Proxy (Enabled)";
                CustomNotificationManager.Instance.ShowNotification("Proxy has been enabled. Restart PC", this.FindForm());
            }
            if (guna2ToggleSwitch9.Checked == false)
            {
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WinHttpAutoProxySvc", "Start", "4", RegistryValueKind.DWord);
                label9.Text = "Proxy (Disabled)";
                CustomNotificationManager.Instance.ShowNotification("Proxy has been disabled. Restart PC", this.FindForm());
            }
        }

        private int dotCount = 0;

        private void installstore_Click(object sender, EventArgs e)
        {

            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\wuauserv", "Start", "2", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\InstallService", "Start", "2", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BITS", "Start", "2", RegistryValueKind.DWord);
            int red = 148;
            int green = 148;
            int blue = 148;

            Color userColor = Color.FromArgb(red, green, blue);
            CustomNotificationManager.Instance.ShowNotification("Installing Microsoft Store...", this.FindForm());
            timer1.Interval = 700;
            timer1.Start();

            string scriptPath = @"C:\ProgramData\OwlOS Utility\Additional\install_microsoft_store.ps1";

            ProcessStartInfo psi = new()
            {
                FileName = "powershell.exe",
                Arguments = $"-File \"{scriptPath}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new()
            {
                StartInfo = psi,

                EnableRaisingEvents = true
            };
            process.Exited += (sender, e) =>
            {
                {
                    _ = Invoke((MethodInvoker)delegate
                    {
                        int red = 45;
                        int green = 128;
                        int blue = 0;

                        Color userColor = Color.FromArgb(red, green, blue);

                        timer1.Stop();
                        timer2.Start();
                        CustomNotificationManager.Instance.ShowNotification("Microsoft Store has been installed.", this.FindForm());
                        System.Windows.Forms.Timer timer = new()
                        {
                            Interval = 5000
                        };
                        timer.Tick += (s, t) =>
                        {
                            timer.Stop();
                        };
                        timer.Start();
                    });
                };
            };
            process.Start();

        }


        private void uninstallstore_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\wuauserv", "Start", "4", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\InstallService", "Start", "4", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BITS", "Start", "4", RegistryValueKind.DWord);
            int red = 148;
            int green = 148;
            int blue = 148;

            Color userColor = Color.FromArgb(red, green, blue);

            CustomNotificationManager.Instance.ShowNotification("Uninstalling Microsoft store...", this.FindForm());
            timer1.Interval = 700;
            timer1.Start();


            ProcessStartInfo psi = new()
            {
                FileName = "powershell.exe",
                Arguments = "/C Get-AppxPackage *windowsstore* | Remove-AppxPackage",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            Process process = new()
            {
                StartInfo = psi,

                EnableRaisingEvents = true
            };
            process.Exited += (sender, e) =>
            {
                {
                    _ = Invoke((MethodInvoker)delegate
                    {
                        int red = 45;
                        int green = 128;
                        int blue = 0;

                        Color userColor = Color.FromArgb(red, green, blue);

                        timer1.Stop();
                        timer2.Start();
                        CustomNotificationManager.Instance.ShowNotification("Microsoft Store has been uninstalled.", this.FindForm());
                        System.Windows.Forms.Timer timer = new()
                        {
                            Interval = 5000
                        };
                        timer.Tick += (s, t) =>
                        {
                            timer.Stop();
                        };
                        timer.Start();
                    });
                };
            };

            process.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            dotCount++;

            if (dotCount == 4)
            {
                dotCount = 0;
            }
        }

        private void windowsdeafult_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MMCSS", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\bam", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Beep", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BDESVC", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\bowser", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DispBrokerDesktopSvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DPS", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdiServiceHost", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdiSystemHost", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lmhosts", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MsLldp", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PcaSvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RmSvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\rspndr", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SysMain", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Themes", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\VerifierExt", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\TrkWks", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\GpuEnergyDrv", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\KSecPkg", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DusmSvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\FontCache", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\FontCache3.0.0.0", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\LanmanServer", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\LanmanWorkstation", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WSearch", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\ShellHWDetection", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lltdio", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DiagTrack", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\diagsvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\dmwappushservice", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Telemetry", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WerSvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\OneSyncSvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\tzautoupdate", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\udfs", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WpnService", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\cdrom", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WinHttpAutoProxySvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WlanSvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\vwififlt", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BluetoothUserService", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTAGService", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthA2dp", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthAvctpSvc", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthEnum", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthHFEnum", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthLEEnum", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthMini", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthPan", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTHPORT", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\bthserv", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTHUSB", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\HidBth", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Spooler", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PrintNotify", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PrintWorkflowUserSvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PptpMiniport", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasMan", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasAgileVpn", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Rasl2tp", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasSstp", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasPppoe", "Start", "00000003", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MsSecFlt", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Sense", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SecurityHealthService", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdBoot", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdFilter", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdNisDrv", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdNisSvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WinDefend", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SgrmAgent", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SgrmBroker", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\UsoSvc", "Start", "00000002", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WaaSMedicSvc", "Start", "00000002", RegistryValueKind.DWord);
            CustomNotificationManager.Instance.ShowNotification("Windows Deafult Services has been set. Restart PC", this.FindForm());
        }

        private void Owldeafult_Click(object sender, EventArgs e)
        {
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MMCSS", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\bam", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Beep", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BDESVC", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\bowser", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DispBrokerDesktopSvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DPS", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdiServiceHost", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdiSystemHost", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lmhosts", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MsLldp", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PcaSvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RmSvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\rspndr", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SysMain", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Themes", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\VerifierExt", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\TrkWks", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\GpuEnergyDrv", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\KSecPkg", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DusmSvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\FontCache", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\FontCache3.0.0.0", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\LanmanServer", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\LanmanWorkstation", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WSearch", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\ShellHWDetection", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\diagnosticshub.standardcollector.service", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\lltdio", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\DiagTrack", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\diagsvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\dmwappushservice", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Telemetry", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WerSvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\OneSyncSvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\tzautoupdate", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\udfs", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WpnService", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\cdrom", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WinHttpAutoProxySvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WlanSvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\vwififlt", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BluetoothUserService", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTAGService", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthA2dp", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthAvctpSvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthEnum", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthHFEnum", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthLEEnum", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthMini", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BthPan", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTHPORT", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\bthserv", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\BTHUSB", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\HidBth", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Spooler", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PrintNotify", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PrintWorkflowUserSvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\PptpMiniport", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasMan", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasAgileVpn", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Rasl2tp", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasSstp", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\RasPppoe", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\MsSecFlt", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Sense", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SecurityHealthService", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdBoot", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdFilter", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdNisDrv", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WdNisSvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WinDefend", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SgrmAgent", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SgrmBroker", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\UsoSvc", "Start", "00000004", RegistryValueKind.DWord);
            Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\WaaSMedicSvc", "Start", "00000004", RegistryValueKind.DWord);
            CustomNotificationManager.Instance.ShowNotification("OwlOS Services has been set. Restart PC", this.FindForm());
        }

        public override bool Equals(object? obj)
        {
            return obj is ServicesControl control &&
                   EqualityComparer<IList<string>>.Default.Equals(outputForInvestigation, control.outputForInvestigation);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public static bool IsRegistryKeyExists(string keyName, string valueName, object value)
        {
            using RegistryKey key = Registry.LocalMachine.OpenSubKey(keyName);
            if (key == null)
            {
                // Klucz rejestru nie istnieje
                return false;
            }

            // Sprawdź, czy wartość istnieje i czy ma odpowiednią wartość
            object regValue = key.GetValue(valueName);
            return (regValue != null && regValue.Equals(value));
        }

        private static void ExecuteRegCommand(string regExePath, string arguments)
        {
            ProcessStartInfo startInfo = new()
            {
                FileName = regExePath,
                Arguments = arguments,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            Process process = new()
            {
                StartInfo = startInfo
            };

            process.Start();
        }

        private void guna2ToggleSwitch10_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch10.Checked == true)
            {
                CustomNotificationManager.Instance.ShowNotification("Windows Defender has been enabled. Restart PC", this.FindForm());
                label11.Text = "Windows Defender \r(Enabled)";
                string regExePath = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\System32\reg.exe");

                // Wykonaj komendy Reg.exe dla każdego wpisu rejestru
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\MsSecFlt"" /v ""Start"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\Sense"" /v ""Start"" /t REG_DWORD /d ""3"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\SecurityHealthService"" /v ""Start"" /t REG_DWORD /d ""3"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\WdBoot"" /v ""Start"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\WdFilter"" /v ""Start"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\WdNisDrv"" /v ""Start"" /t REG_DWORD /d ""3"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\WdNisSvc"" /v ""Start"" /t REG_DWORD /d ""3"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\WinDefend"" /v ""Start"" /t REG_DWORD /d ""2"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\SgrmAgent"" /v ""Start"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\SgrmBroker"" /v ""Start"" /t REG_DWORD /d ""2"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender"" /v ""DisableAntiSpyware"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection"" /v ""DisableRealtimeMonitoring"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection"" /v ""DisableBehaviorMonitoring"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection"" /v ""DisableScanOnRealtimeEnable"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection"" /v ""DisableOnAccessProtection"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\UX Configuration"" /v ""Notification_Suppress"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender Security Center\Notifications"" /v ""DisableNotifications"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"" /v ""SecurityHealth"" /t REG_EXPAND_SZ /d ""C:\Windows\system32\SecurityHealthSystray.exe"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer"" /v ""SmartScreenEnabled"" /t REG_SZ /d ""On"" /f");
                ExecuteRegCommand(regExePath, @"delete ""HKLM\Software\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\smartscreen.exe"" /f");
                ExecuteRegCommand(regExePath, @"delete ""HKLM\Software\Policies\Microsoft\Windows Defender\SmartScreen"" /f");
                ExecuteRegCommand(regExePath, @"delete ""HKLM\Software\Policies\Microsoft\Windows Defender\Signature Updates"" /f");
            }
            if (guna2ToggleSwitch10.Checked == false)
            {
                CustomNotificationManager.Instance.ShowNotification("Windows Defender has been disabled. Restart PC", this.FindForm());
                label11.Text = "Windows Defender \r(Disabled)";
                string regExePath = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\System32\reg.exe");

                // Wykonaj komendy Reg.exe dla każdego wpisu rejestru
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\MsSecFlt"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\Sense"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\SecurityHealthService"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\WdBoot"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\WdFilter"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\WdNisDrv"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\WdNisSvc"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\WinDefend"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\SgrmAgent"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\ControlSet001\Services\SgrmBroker"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender"" /v ""DisableAntiSpyware"" /t REG_DWORD /d ""1"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection"" /v ""DisableRealtimeMonitoring"" /t REG_DWORD /d ""1"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection"" /v ""DisableBehaviorMonitoring"" /t REG_DWORD /d ""1"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection"" /v ""DisableScanOnRealtimeEnable"" /t REG_DWORD /d ""1"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\Real-Time Protection"" /v ""DisableOnAccessProtection"" /t REG_DWORD /d ""1"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender\UX Configuration"" /v ""Notification_Suppress"" /t REG_DWORD /d ""1"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Policies\Microsoft\Windows Defender Security Center\Notifications"" /v ""DisableNotifications"" /t REG_DWORD /d ""1"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer"" /v ""SmartScreenEnabled"" /t REG_SZ /d ""Off"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Image File Execution Options\smartscreen.exe"" /v ""Debugger"" /t REG_SZ /d ""%%windir%%\System32\taskkill.exe"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\Software\Policies\Microsoft\Windows Defender\SmartScreen"" /v ""ConfigureAppInstallControlEnabled"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\Software\Policies\Microsoft\Windows Defender\SmartScreen"" /v ""ConfigureAppInstallControl"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\Software\Policies\Microsoft\Windows Defender\SmartScreen"" /v ""EnableSmartScreen"" /t REG_DWORD /d ""0"" /f");
                ExecuteRegCommand(regExePath, @"delete ""HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run"" /v ""SecurityHealth"" /f");
            }
        }

        private void guna2ToggleSwitch11_CheckedChanged(object sender, EventArgs e)
        {
            if (guna2ToggleSwitch11.Checked == true)
            {
                CustomNotificationManager.Instance.ShowNotification("Bitlocker has been enabled. Restart PC", this.FindForm());
                label12.Text = "Bitlocker (Enabled)";
                string regExePath = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\System32\reg.exe");

                // Wykonaj komendy Reg.exe dla każdego wpisu rejestru
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\CurrentControlSet\Services\BDESVC"" /v ""Start"" /t REG_DWORD /d ""2"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\CurrentControlSet\Services\ShellHWDetection"" /v ""Start"" /t REG_DWORD /d ""2"" /f");
            }
            if (guna2ToggleSwitch11.Checked == false)
            {
                CustomNotificationManager.Instance.ShowNotification("Bitlocker has been disabled. Restart PC", this.FindForm());
                label12.Text = "Bitlocker (Disabled)";
                string regExePath = Environment.ExpandEnvironmentVariables(@"%SystemRoot%\System32\reg.exe");

                // Wykonaj komendy Reg.exe dla każdego wpisu rejestru
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\CurrentControlSet\Services\BDESVC"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
                ExecuteRegCommand(regExePath, @"add ""HKLM\SYSTEM\CurrentControlSet\Services\ShellHWDetection"" /v ""Start"" /t REG_DWORD /d ""4"" /f");
            }
        }
    }
}
