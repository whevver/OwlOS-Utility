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
    public partial class AdvancedControl : UserControl
    {
        public AdvancedControl()
        {
            InitializeComponent();
            guna2Panel2.Hide();
        }


        private void guna2TrackBar1_Scroll(object sender, ScrollEventArgs e)
        {
            if (guna2TrackBar1.Value == 0)
            {
                NumberLabel.Text = "16";
            }
            if (guna2TrackBar1.Value == 1)
            {
                NumberLabel.Text = "20";
            }
            if (guna2TrackBar1.Value == 2)
            {
                NumberLabel.Text = "25";
            }
            if (guna2TrackBar1.Value == 3)
            {
                NumberLabel.Text = "50";
            }
            if (guna2TrackBar1.Value == 4)
            {
                NumberLabel.Text = "100*";
            }
        }

        private void AdvancedControl_Load(object sender, EventArgs e)
        {
            ApplyAffbtn.BorderRadius = 5;
            Applyformouse.BorderRadius = 5;
            Applyforkeyboard.BorderRadius = 5;
            guna2Transition1.ShowSync(guna2Panel2);
            this.AutoScaleMode = AutoScaleMode.Dpi;
        }

        private void ApplyAffbtn_Click(object sender, EventArgs e)
        {
            Process.Start("Explorer.exe", @"C:\ProgramData\OwlOS Utility\Additional\AutoGpuAffinity\AutoGpuAffinity.exe");
        }

        private void Applyformouse_Click(object sender, EventArgs e)
        {
            if (guna2TrackBar1.Value == 0)
            {
                //NumberLabel.Text = "16";
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\mouclass\Parameters", "MouseDataQueueSize", "00000010", RegistryValueKind.DWord);
                CustomNotificationManager.Instance.ShowNotification("Queue size 16 for mouse has been set", this.FindForm());
            }
            if (guna2TrackBar1.Value == 1)
            {
                //NumberLabel.Text = "20";
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\mouclass\Parameters", "MouseDataQueueSize", "00000014", RegistryValueKind.DWord);
                CustomNotificationManager.Instance.ShowNotification("Queue size 20 for mouse has been set", this.FindForm());
            }
            if (guna2TrackBar1.Value == 2)
            {
                //NumberLabel.Text = "25";
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\mouclass\Parameters", "MouseDataQueueSize", "00000019", RegistryValueKind.DWord);
                CustomNotificationManager.Instance.ShowNotification("Queue size 25 for mouse has been set", this.FindForm());
            }
            if (guna2TrackBar1.Value == 3)
            {
                //NumberLabel.Text = "50";
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\mouclass\Parameters", "MouseDataQueueSize", "00000032", RegistryValueKind.DWord);
                CustomNotificationManager.Instance.ShowNotification("Queue size 50 for mouse has been set", this.FindForm());
            }
            if (guna2TrackBar1.Value == 4)
            {
                //NumberLabel.Text = "100*";
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\mouclass\Parameters", "MouseDataQueueSize", "00000064", RegistryValueKind.DWord);
                CustomNotificationManager.Instance.ShowNotification("Queue size 100 (Deafult) for mouse has been set", this.FindForm());
            }
        }

        private void Applyforkeyboard_Click(object sender, EventArgs e)
        {
            if (guna2TrackBar1.Value == 0)
            {
                //NumberLabel.Text = "16";
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\kbdclass\Parameters", "KeyboardDataQueueSize", "00000010", RegistryValueKind.DWord);
                CustomNotificationManager.Instance.ShowNotification("Queue size 16 for keyboard has been set", this.FindForm());
            }
            if (guna2TrackBar1.Value == 1)
            {
                //NumberLabel.Text = "20";
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\kbdclass\Parameters", "KeyboardDataQueueSize", "00000014", RegistryValueKind.DWord);
                CustomNotificationManager.Instance.ShowNotification("Queue size 20 for keyboard has been set", this.FindForm());
            }
            if (guna2TrackBar1.Value == 2)
            {
                //NumberLabel.Text = "25";
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\kbdclass\Parameters", "KeyboardDataQueueSize", "00000019", RegistryValueKind.DWord);
                CustomNotificationManager.Instance.ShowNotification("Queue size 25 for keyboard has been set", this.FindForm());
            }
            if (guna2TrackBar1.Value == 3)
            {
                //NumberLabel.Text = "50";
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\kbdclass\Parameters", "KeyboardDataQueueSize", "00000032", RegistryValueKind.DWord);
                CustomNotificationManager.Instance.ShowNotification("Queue size 50 for keyboard has been set", this.FindForm());
            }
            if (guna2TrackBar1.Value == 4)
            {
                //NumberLabel.Text = "100*";
                Registry.SetValue(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\services\kbdclass\Parameters", "KeyboardDataQueueSize", "00000064", RegistryValueKind.DWord);
                CustomNotificationManager.Instance.ShowNotification("Queue size 100 (Deafult) for keyboard has been set", this.FindForm());
            }
        }
    }
}
