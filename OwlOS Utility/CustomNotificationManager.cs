using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Timer = System.Windows.Forms.Timer;

namespace OwlOS_Utility
{
    public class CustomNotificationManager
    {
        private static CustomNotificationManager? instance;
        private Form ownerForm;
        private readonly Form notificationForm;
        private Timer fadeInTimer;
        private Timer fadeOutTimer;
        private readonly Guna2ProgressBar progressBar;
        private Timer progressBarTimer;
        private bool isNotificationShowing = false;
        private Label notificationLabel;
        private bool ignoreDeactivateEvent = false; // Dodana flaga do ignorowania zdarzenia Deactivate

        private CustomNotificationManager()
        {
            notificationForm = new Form
            {
                FormBorderStyle = FormBorderStyle.None,
                StartPosition = FormStartPosition.Manual,
                Size = new Size(180, 50), // Rozmiar notyfikacji
                BackColor = Color.FromArgb(45, 45, 45),
                TopMost = true,
                ShowInTaskbar = false
            };

            int radius = 5;
            GraphicsPath path = new();
            path.AddArc(new Rectangle(0, 0, radius * 2, radius * 2), 180, 90);
            path.AddLine(radius, 0, notificationForm.Width - radius, 0);
            path.AddArc(new Rectangle(notificationForm.Width - radius * 2, 0, radius * 2, radius * 2), 270, 90);
            path.AddLine(notificationForm.Width, radius, notificationForm.Width, notificationForm.Height - radius);
            path.AddArc(new Rectangle(notificationForm.Width - radius * 2, notificationForm.Height - radius * 2, radius * 2, radius * 2), 0, 90);
            path.AddLine(notificationForm.Width - radius, notificationForm.Height, radius, notificationForm.Height);
            path.AddArc(new Rectangle(0, notificationForm.Height - radius * 2, radius * 2, radius * 2), 90, 90);
            notificationForm.Region = new Region(path);

            progressBar = new Guna2ProgressBar
            {
                Dock = DockStyle.Bottom,
                Maximum = 100,
                Minimum = 0,
                Value = 100,
                Height = 2,
                FillColor = Color.FromArgb(45, 45, 45),
                ProgressColor = Color.FromArgb(51, 153, 255),
                ProgressColor2 = Color.FromArgb(51, 153, 255),
                BorderThickness = 0,
                RightToLeft = RightToLeft.Yes
            };
            notificationForm.Controls.Add(progressBar);

            // Lokalizacja paska postępu
            progressBar.Location = new Point(0, 0);
        }

        public static CustomNotificationManager Instance
        {
            get
            {
                instance ??= new CustomNotificationManager();
                return instance;
            }
        }

        public void ShowNotification(string message, Form owner)
        {
            // Jeśli notyfikacja jest aktualnie pokazywana, zatrzymaj poprzednie animacje i timery
            if (isNotificationShowing)
            {
                fadeInTimer.Stop();
                fadeOutTimer.Stop();
                progressBarTimer.Stop();
                notificationForm.Opacity = 0; // Ukryj notyfikację
                progressBar.Value = 100; // Zresetuj wartość progressbaru
            }

            ownerForm = owner;
            notificationForm.Owner = ownerForm;

            // Tworzymy lub aktualizujemy Label z tekstem notyfikacji
            if (notificationLabel == null)
            {
                notificationLabel = new Label
                {
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter,
                    ForeColor = Color.FromArgb(255, 255, 255)
                };
                notificationForm.Controls.Add(notificationLabel);
            }
            notificationLabel.Text = message; // Aktualizujemy tekst notyfikacji

            // Uruchom animację pojawienia się notyfikacji
            fadeInTimer = new Timer
            {
                Interval = 30
            };
            fadeInTimer.Tick += (sender, e) =>
            {
                if (notificationForm.Opacity < 1)
                {
                    notificationForm.Opacity += 0.05;
                }
                else
                {
                    fadeInTimer.Stop();
                    // Pokaż notyfikację dopiero po zakończeniu animacji
                    notificationForm.Show();
                }
            };
            fadeInTimer.Start();

            // Ustaw czas trwania notyfikacji i uruchom timery dla animacji znikania i paska postępu
            fadeOutTimer = new Timer
            {
                Interval = 3000
            };
            fadeOutTimer.Tick += (sender, e) =>
            {
                fadeOutTimer.Stop();
                progressBarTimer.Stop();
                notificationForm.Hide();
                isNotificationShowing = false;
            };
            fadeOutTimer.Start();

            progressBar.Value = 100;
            progressBarTimer = new Timer
            {
                Interval = 30
            };
            progressBarTimer.Tick += (sender, e) =>
            {
                if (progressBar.Value > 0)
                    progressBar.Value -= 1;
            };
            progressBarTimer.Start();

            // Zaktualizuj flagę oznaczającą, że notyfikacja jest aktualnie pokazywana
            isNotificationShowing = true;

            // Połącz notyfikację z formularzem właściciela, aby poruszała się razem z programem
            owner.LocationChanged += (sender, e) => UpdateNotificationLocation();
            UpdateNotificationLocation();
        }

        private void UpdateNotificationLocation()
        {
            int offset = 20; // Margines od krawędzi formularza
            int x = ownerForm.Location.X + ownerForm.Width - notificationForm.Width - offset;
            int y = ownerForm.Location.Y + ownerForm.Height - notificationForm.Height - offset;
            notificationForm.Location = new Point(x, y);
        }

        public void HandleComboBoxClick(object sender, EventArgs e)
        {
            // Zaznacz, że zdarzenie Deactivate nie powinno być obsługiwane w trakcie klikania w ComboBox
            ignoreDeactivateEvent = true;
        }

        private void NotificationForm_Deactivate(object sender, EventArgs e)
        {
            // Ukryj notyfikację, gdy aplikacja traci aktywność, chyba że kliknięto w ComboBox
            if (!ignoreDeactivateEvent)
            {
                HideNotification();
            }
            else
            {
                ignoreDeactivateEvent = false; // Wyłącz flagę, aby ponownie obsługiwać zdarzenia Deactivate
            }
        }

        public void HideNotification()
        {
            if (isNotificationShowing)
            {
                fadeInTimer.Stop();
                fadeOutTimer.Stop();
                progressBarTimer.Stop();
                notificationForm.Hide();
                isNotificationShowing = false;
            }
        }
    }
}
