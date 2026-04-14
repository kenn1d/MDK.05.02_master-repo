using praktika21_30_.Classes;
using System;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace praktika21_30_.Pages
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        string OldLogin;
        int CountSetPassword = 2;
        bool IsCapture = false;
        Page thisWindow;

        public Login()
        {
            InitializeComponent();
            thisWindow = this;
            MainWindow.mainWindow.UserLogIn.HandlerCorrectLogin += CorrectLogin;
            MainWindow.mainWindow.UserLogIn.HandlerInCorrectLogin += InCorrectLogin;
            Capture.HandlerCorrectCapture += CorrectCapture;
        }

        public void CorrectLogin()
        {
            if (OldLogin != TbLogin.Text)
            {
                Functions.Animation(LNameUser, IUser);
                OldLogin = TbLogin.Text;
            }
        }

        public void InCorrectLogin()
        {
            Functions.Animation(LNameUser, IUser, "pack://application:,,,/Images/profile.png");
            if (TbLogin.Text.Length > 0)
                Functions.SetNotification(LNameUser, "Login is incorrect", Brushes.Red);
        }

        private void SetPassword(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                SetPassword();
        }

        public void SetPassword()
        {
            if (MainWindow.mainWindow.UserLogIn.Password != String.Empty)
            {
                if (IsCapture)
                {
                    if (MainWindow.mainWindow.UserLogIn.Password == TbPassword.Password)
                        MainWindow.mainWindow.OpenPage(new Confirmation(Confirmation.TypeConfirmation.Login));
                }
                else
                {
                    if (CountSetPassword > 0)
                    {
                        Functions.SetNotification(LNameUser, $"Password is incorrect, {CountSetPassword} attempts left", Brushes.Red);
                        CountSetPassword--;
                    }
                    else
                    {
                        Thread TBlockAuthorization = new Thread(BlockAuthorization);
                        TBlockAuthorization.Start();

                        SendMail.SendMessage("An attempt was made to log into your account.",
                            MainWindow.mainWindow.UserLogIn.Login);
                    }
                }
            }
            else
            {
                Functions.SetNotification(LNameUser, $"Enter capture", Brushes.Red);
            }
        }

        public void BlockAuthorization()
        {
            DateTime StartBlock = DateTime.Now.AddMinutes(3);

            Dispatcher.Invoke(() =>
            {
                TbLogin.IsEnabled = false;
                TbPassword.IsEnabled = false;
                Capture.IsEnabled = false;
            });

            for (int i = 0; i < 180; i++)
            {
                TimeSpan TimeIdle = StartBlock.Subtract(DateTime.Now);
                string s_minutes = TimeIdle.Minutes.ToString();
                if (TimeIdle.Minutes < 10)
                    s_minutes = "0" + TimeIdle.Minutes;
                string s_seconds = TimeIdle.Seconds.ToString();
                if (TimeIdle.Seconds < 10)
                    s_seconds = "0" + TimeIdle.Seconds;
                Dispatcher.Invoke(() =>
                {
                    Functions.SetNotification(LNameUser, $"Reauthorization avalivable in: {s_minutes}:{s_seconds}", Brushes.Red);
                });
                Thread.Sleep(1000);
            }

            Dispatcher.Invoke(() =>
            {
                Functions.SetNotification(LNameUser, "Hi, " + MainWindow.mainWindow.UserLogIn.Name, Brushes.Black);
                TbLogin.IsEnabled = true;
                TbPassword.IsEnabled = true;
                Capture.IsEnabled = true;
                Capture.CreateCapture();
                IsCapture = false;
                CountSetPassword = 2;
            });
        }

        private void SetLogin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);

                if (TbPassword.Password.Length > 0)
                    SetPassword();
            }
        }

        private void SetLogin(object sender, System.Windows.RoutedEventArgs e)
        {
            MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);
            if(TbPassword.Password.Length > 0) { SetPassword(); }
        }

        private void RecoveryPassword(object sender, MouseButtonEventArgs e) =>
            MainWindow.mainWindow.OpenPage(new Recovery());

        private void OpenRegin(object sender, MouseButtonEventArgs e) =>
            MainWindow.mainWindow.OpenPage(new Regin());

        public void CorrectCapture()
        {
            Capture.IsEnabled = false;
            IsCapture = true;
        }
    }
}
