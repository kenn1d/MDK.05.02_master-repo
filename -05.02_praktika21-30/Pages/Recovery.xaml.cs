using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using praktika21_30_.Classes;

namespace praktika21_30_.Pages
{
    /// <summary>
    /// Логика взаимодействия для Recovery.xaml
    /// </summary>
    public partial class Recovery : Page
    {
        string OldLogin;
        bool IsCapture = false;
        public Recovery()
        {
            InitializeComponent();

            MainWindow.mainWindow.UserLogIn.HandlerCorrectLogin += CorrectLogin;
            MainWindow.mainWindow.UserLogIn.HandlerInCorrectLogin += InCorrectLogin;
            Capture.HandlerCorrectCapture += CorrectCapture;
        }

        private void CorrectLogin()
        {
            if (OldLogin != TbLogin.Text)
            {
                Functions.Animation(LNameUser, IUser);
                OldLogin = TbLogin.Text;
                SendNewPassword();
            }
        }

        private void InCorrectLogin()
        {
            Functions.Animation(LNameUser, IUser, "pack://application:,,,/Images/profile.png");
            if (TbLogin.Text.Length > 0)
                Functions.SetNotification(LNameUser, "Login is incorrect", Brushes.Red);
        }

        private void CorrectCapture()
        {
            Capture.IsEnabled = false;
            IsCapture = true;
            SendNewPassword();
        }

        private void SetLogin(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
                MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);
        }

        private void SetLogin(object sender, RoutedEventArgs e) =>
            MainWindow.mainWindow.UserLogIn.GetUserLogin(TbLogin.Text);

        public void SendNewPassword()
        {
            if(IsCapture)
            {
                if(MainWindow.mainWindow.UserLogIn.Password != String.Empty)
                {
                    Functions.Animation(LNameUser, IUser, "pack://application:,,,/Images/profile.png");
                    Functions.SetNotification(LNameUser, "An email has been sent to your email.", Brushes.Black);
                    MainWindow.mainWindow.UserLogIn.CreateNewPassword();
                }
            }
        }

        private void OpenLogin(object sender, MouseButtonEventArgs e)
        {
            MainWindow.mainWindow.OpenPage(new Login());
        }
    }
}
