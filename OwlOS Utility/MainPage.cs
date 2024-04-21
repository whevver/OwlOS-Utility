using Guna.UI2.WinForms;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;


namespace OwlOS_Utility
{
    public partial class MainPage : Form
    {

        [LibraryImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static partial IntPtr CreateRoundRectRgn
       (
           int nLeftRect,     // x-coordinate of upper-left corner
           int nTopRect,      // y-coordinate of upper-left corner
           int nRightRect,    // x-coordinate of lower-right corner
           int nBottomRect,   // y-coordinate of lower-right corner
           int nWidthEllipse, // height of ellipse
           int nHeightEllipse // width of ellipse
       );


        public MainPage()
        {
            if (Process.GetProcessesByName("OwlOS Utility").Length > 1)
            {
                MessageBox.Show("OwlOS Utility is already opened.", "", MessageBoxButtons.OK);
            }
            else
            {
                InitializeComponent();
                this.MaximumSize = new System.Drawing.Size(1050, 500);
                this.FormBorderStyle = FormBorderStyle.None;
                guna2ShadowForm1.SetShadowForm(this);
                Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));
                HomeButton.BorderRadius = 10;
                BrowserButton.BorderRadius = 10;
                Servicesbtn.BorderRadius = 10;
                DriversButton.BorderRadius = 10;
                SettingsButton.BorderRadius = 10;
                AdvancedButton.BorderRadius = 10;
                UpdatesButton.BorderRadius = 10;
                AppsBtn.BorderRadius = 15;
            }

            string username = System.Windows.Forms.SystemInformation.UserName;
            guna2HtmlLabel2.Invoke((MethodInvoker)(() => guna2HtmlLabel2.Text = username));
        }

        [LibraryImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool SetProcessDPIAware();
        private void addUserControl(UserControl userControl)
        {
            CustomNotificationManager.Instance.HideNotification();
            userControl.Dock = DockStyle.Fill;
            UCpanel.Controls.Clear();
            UCpanel.Controls.Add(userControl);
            userControl.BringToFront();
        }

        private void HomeButton_Click(object sender, EventArgs e)
        {
            HomeControl hc = new();
            addUserControl(hc);
            CustomNotificationManager.Instance.HideNotification();
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            BrowserControl bc = new();
            addUserControl(bc);
            CustomNotificationManager.Instance.HideNotification();
        }

        private void DriversButton_Click(object sender, EventArgs e)
        {
            CustomNotificationManager.Instance.HideNotification();
            DriversControl drc = new();
            addUserControl(drc);       
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            SettingsControl stc = new();
            addUserControl(stc);
            CustomNotificationManager.Instance.HideNotification();
        }

        private void AdvancedButton_Click(object sender, EventArgs e)
        {
            AdvancedControl advc = new();
            addUserControl(advc);
            CustomNotificationManager.Instance.HideNotification();
        }

        private void UpdatesButton_Click(object sender, EventArgs e)
        {
            FAQControl updc = new();
            addUserControl(updc);
            CustomNotificationManager.Instance.HideNotification();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HomeControl hc = new();
            addUserControl(hc);
        }

        private void AppsBtn_Click(object sender, EventArgs e)
        {
            AppsControl apc = new();
            addUserControl(apc);
            CustomNotificationManager.Instance.HideNotification();
        }

        private void guna2ImageButton2_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", "https://github.com/0dany/OwlOS");
        }

        private void guna2ImageButton1_Click(object sender, EventArgs e)
        {
            Process.Start("explorer", "https://dsc.gg/owlos");
        }

        private void Servicesbtn_Click(object sender, EventArgs e)
        {
            ServicesControl sc = new();
            addUserControl(sc);
            CustomNotificationManager.Instance.HideNotification();
        }
    }
}