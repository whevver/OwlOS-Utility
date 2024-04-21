using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OwlOS_Utility
{
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
            guna2TextBox1.BorderRadius = 5;
            guna2TextBox2.BorderRadius = 5;
            Reviewbtn.BorderRadius = 5;
        }

        public static void sendWebhook(string url, string msg, string username)
        {
            Http.Post(url, new System.Collections.Specialized.NameValueCollection()
            {
                {
                    "username",
                    username
                },
                {
                    "content",
                    msg
                }
            });
        }

        private bool inCooldown = false;


        private async void Reviewbtn_Click(object sender, EventArgs e) //Send review
        {
            if (inCooldown)
            {

                CustomNotificationManager.Instance.ShowNotification("Looks like you posted a review recently", this.FindForm());
                return;
            }
            if (guna2TextBox2.TextLength <= 30)
            {
                CustomNotificationManager.Instance.ShowNotification("The review must contain at least 30 characters", this.FindForm());
            }
            if (guna2TextBox2.TextLength >= 30)
            {
                if (guna2CheckBox1.Checked == true)
                {
                    await Task.Delay(180);
                    sendWebhook("https://discord.com/api/webhooks/1083004963451568202/F_kWBVAqtDm2jVLu0_O98hT3miy57EEFT-v0UWE_3BkSMwC0c4LJ8DcEM031IZQUPX6o", guna2TextBox2.Text, "Anonymous user");
                    inCooldown = true;
                    CustomNotificationManager.Instance.ShowNotification("Review has been sent anonymously", this.FindForm());
                }
                if (guna2CheckBox1.Checked == false)
                {
                    if (guna2TextBox1.TextLength <= 0)
                    {
                        await Task.Delay(180);
                        string username = System.Windows.Forms.SystemInformation.UserName;
                        sendWebhook("https://discord.com/api/webhooks/1083004963451568202/F_kWBVAqtDm2jVLu0_O98hT3miy57EEFT-v0UWE_3BkSMwC0c4LJ8DcEM031IZQUPX6o", guna2TextBox2.Text, username);
                        inCooldown = true;
                        CustomNotificationManager.Instance.ShowNotification("Review has been sent", this.FindForm());
                    }
                    else
                    {
                        await Task.Delay(180);
                        sendWebhook("https://discord.com/api/webhooks/1083004963451568202/F_kWBVAqtDm2jVLu0_O98hT3miy57EEFT-v0UWE_3BkSMwC0c4LJ8DcEM031IZQUPX6o", guna2TextBox2.Text, guna2TextBox1.Text);
                        inCooldown = true;
                        CustomNotificationManager.Instance.ShowNotification("Review has been sent", this.FindForm());
                    }
                }
            }
            await Task.Delay(300000);
            inCooldown = false;
        }

        private void HomeControl_Load(object sender, EventArgs e)
        {
            guna2Panel1.Hide();
            guna2Transition1.ShowSync(guna2Panel1);
            this.AutoScaleMode = AutoScaleMode.Dpi;
        }
    }
}
    


